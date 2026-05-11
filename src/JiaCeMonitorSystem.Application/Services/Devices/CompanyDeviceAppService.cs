using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.DomainServices;
using JiaCeMonitorSystem.Dtos.Devices;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.Devices;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.Devices
{
    /// <summary>
    /// 单位设备应用服务
    /// </summary>
    [Authorize]
    public class CompanyDeviceAppService :
        CrudAppService<CompanyDevice, CompanyDeviceDto, Guid, GetDeviceListInput, CompanyDeviceCreateDto, CompanyDeviceUpdateDto>,
        ICompanyDeviceAppService
    {
        private readonly DeviceManager _deviceManager;
        private readonly IRepository<DeviceAssignment, Guid> _assignmentRepository;

        public CompanyDeviceAppService(
            IRepository<CompanyDevice, Guid> repository,
            DeviceManager deviceManager,
            IRepository<DeviceAssignment, Guid> assignmentRepository) : base(repository)
        {
            _deviceManager = deviceManager;
            _assignmentRepository = assignmentRepository;
        }

        /// <summary>
        /// 获取设备列表（非分页，支持筛选）
        /// </summary>
        public async Task<List<CompanyDeviceDto>> GetListAsync(Guid? companyId, int? deviceType, string? keyword, int? deviceStatus, bool? checkCalibration)
        {
            var query = await Repository.GetQueryableAsync();

            if (companyId.HasValue)
                query = query.Where(d => d.CompanyId == companyId.Value);
            if (deviceType.HasValue)
                query = query.Where(d => (int)d.DeviceType == deviceType.Value);
            if (deviceStatus.HasValue)
                query = query.Where(d => (int)d.DeviceStatus == deviceStatus.Value);
            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(d => d.DeviceCode.Contains(keyword) || d.DeviceName.Contains(keyword));
            if (checkCalibration == true)
                query = query.Where(d => d.NextCalibrationDate.HasValue && d.NextCalibrationDate.Value < DateTime.UtcNow.AddMonths(1));

            var devices = await AsyncExecuter.ToListAsync(query.OrderBy(d => d.DeviceCode));
            return ObjectMapper.Map<List<CompanyDevice>, List<CompanyDeviceDto>>(devices);
        }

        /// <summary>
        /// 获取设备分配信息（已借出记录）
        /// </summary>
        public async Task<List<DeviceAssignmentDetailDto>> GetSendDeviceAsync(Guid deviceId)
        {
            var assignments = await _assignmentRepository.GetListAsync(a => a.DeviceId == deviceId);
            var device = await Repository.GetAsync(deviceId);
            var dtos = new List<DeviceAssignmentDetailDto>();
            var today = DateTime.UtcNow;

            foreach (var a in assignments.OrderByDescending(a => a.AssignmentDate))
            {
                var borrowedDays = a.ActualReturnDate.HasValue
                    ? (a.ActualReturnDate.Value - a.AssignmentDate).Days
                    : (today - a.AssignmentDate).Days;
                var remainingDays = a.ActualReturnDate == null && a.ExpectedReturnDate.HasValue
                    ? Math.Max(0, (a.ExpectedReturnDate.Value - today).Days)
                    : 0;
                var isOverdue = a.ActualReturnDate == null && a.ExpectedReturnDate.HasValue && a.ExpectedReturnDate.Value < today;
                var overdueDays = isOverdue && a.ExpectedReturnDate.HasValue ? (today - a.ExpectedReturnDate.Value).Days : 0;

                dtos.Add(new DeviceAssignmentDetailDto
                {
                    Id = a.Id,
                    DeviceId = a.DeviceId,
                    DeviceTypeName = device.DeviceType.ToString(),
                    ProjectId = a.ProjectId,
                    AssignmentDate = a.AssignmentDate,
                    ExpectedReturnDate = a.ExpectedReturnDate,
                    ActualReturnDate = a.ActualReturnDate,
                    AssignerName = a.AssignerName,
                    ReceiverName = a.ReceiverName,
                    UsageDescription = a.UsageDescription,
                    AssignmentStatus = (int)a.AssignmentStatus,
                    AssignmentStatusText = a.AssignmentStatus.ToString(),
                    Remark = a.Remark,
                    CreationTime = a.CreationTime,
                    LastModificationTime = a.LastModificationTime,
                    BorrowedDays = borrowedDays,
                    RemainingDays = remainingDays,
                    IsOverdue = isOverdue,
                    OverdueDays = overdueDays,
                    IsCurrentlyBorrowed = a.ActualReturnDate == null
                });
            }

            // 标记最近一条未归还的记录为当前借用
            var currentBorrowed = dtos.FirstOrDefault(d => d.IsCurrentlyBorrowed);
            if (currentBorrowed != null)
                currentBorrowed.IsCurrentlyBorrowed = true;

            return dtos;
        }

        /// <summary>
        /// 创建设备
        /// </summary>
        [Authorize(Permissions.Permissions.Devices_Create)]
        public override async Task<CompanyDeviceDto> CreateAsync(CompanyDeviceCreateDto input)
        {
            var device = await _deviceManager.CreateDeviceAsync(
                input.DeviceCode,
                input.DeviceName,
                (Enums.DeviceType)input.DeviceType,
                input.CompanyId);

            // 映射额外字段
            device.UpdateInfo(
                input.DeviceName,
                (Enums.DeviceType)input.DeviceType,
                input.Model,
                input.Manufacturer,
                input.SerialNumber,
                input.Location,
                input.Description);

            await Repository.UpdateAsync(device);
            return ObjectMapper.Map<CompanyDevice, CompanyDeviceDto>(device);
        }

        /// <summary>
        /// 设备校准
        /// </summary>
        [Authorize(Permissions.Permissions.Devices_Calibrate)]
        public async Task CalibrateAsync(CalibrateDeviceInput input)
        {
            await _deviceManager.CalibrateDeviceAsync(
                input.DeviceId,
                input.CalibrationDate,
                input.NextCalibrationDate,
                input.Accuracy);
        }

        /// <summary>
        /// 借出设备
        /// </summary>
        [Authorize(Permissions.Permissions.Devices_Lend)]
        public async Task<DeviceAssignmentDto> LendAsync(Guid deviceId, Guid projectId, DateTime expectedReturnDate, Guid? receiverId, string? receiverName)
        {
            var assignment = await _deviceManager.LendDeviceAsync(
                deviceId,
                projectId,
                DateTime.UtcNow,
                expectedReturnDate,
                CurrentUser.Id,
                CurrentUser.UserName,
                receiverId,
                receiverName);

            return ObjectMapper.Map<DeviceAssignment, DeviceAssignmentDto>(assignment);
        }

        /// <summary>
        /// 归还设备
        /// </summary>
        [Authorize(Permissions.Permissions.Devices_Return)]
        public async Task ReturnAsync(Guid assignmentId, DateTime returnDate, string? condition)
        {
            await _deviceManager.ReturnDeviceAsync(assignmentId, returnDate, condition);
        }

        /// <summary>
        /// 报废设备
        /// </summary>
        [Authorize(Permissions.Permissions.Devices_Scrap)]
        public async Task ScrapAsync(Guid id, string reason)
        {
            await _deviceManager.ScrapDeviceAsync(id, reason);
        }
    }
}
