using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Devices;
using JiaCeMonitorSystem.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace JiaCeMonitorSystem.DomainServices
{
    /// <summary>
    /// 设备管理领域服务，负责设备状态流转校验与业务规则执行
    /// </summary>
    public class DeviceManager : DomainService
    {
        private readonly IRepository<CompanyDevice, Guid> _deviceRepository;
        private readonly IRepository<DeviceAssignment, Guid> _assignmentRepository;

        /// <summary>
        /// 初始化设备管理领域服务
        /// </summary>
        public DeviceManager(
            IRepository<CompanyDevice, Guid> deviceRepository,
            IRepository<DeviceAssignment, Guid> assignmentRepository)
        {
            _deviceRepository = deviceRepository;
            _assignmentRepository = assignmentRepository;
        }

        /// <summary>
        /// 创建设备档案
        /// </summary>
        public async Task<CompanyDevice> CreateDeviceAsync(
            string deviceCode,
            string deviceName,
            DeviceType deviceType,
            Guid? companyId = null)
        {
            // 校验设备编号唯一性
            if (await _deviceRepository.AnyAsync(d => d.DeviceCode == deviceCode))
            {
                throw new BusinessException(ErrorCodes.Device_DuplicateCode)
                    .WithData("DeviceCode", deviceCode);
            }

            var device = new CompanyDevice(
                GuidGenerator.Create(),
                deviceCode,
                deviceName,
                deviceType,
                companyId);

            await _deviceRepository.InsertAsync(device);
            return device;
        }

        /// <summary>
        /// 借出设备到项目
        /// </summary>
        public async Task<DeviceAssignment> LendDeviceAsync(
            Guid deviceId,
            Guid projectId,
            DateTime assignmentDate,
            DateTime? expectedReturnDate,
            Guid? assignerId = null,
            string? assignerName = null,
            Guid? receiverId = null,
            string? receiverName = null,
            string? usageDescription = null)
        {
            var device = await _deviceRepository.GetAsync(deviceId);

            // 执行借出操作（内含状态校验）
            device.LendOut();
            await _deviceRepository.UpdateAsync(device);

            var assignment = new DeviceAssignment(
                GuidGenerator.Create(),
                deviceId,
                projectId,
                assignmentDate,
                assignerId,
                assignerName,
                receiverId,
                receiverName,
                expectedReturnDate,
                usageDescription);

            await _assignmentRepository.InsertAsync(assignment);
            return assignment;
        }

        /// <summary>
        /// 归还设备
        /// </summary>
        public async Task ReturnDeviceAsync(Guid assignmentId, DateTime returnDate, string? condition = null)
        {
            var assignment = await _assignmentRepository.GetAsync(assignmentId);
            var device = await _deviceRepository.GetAsync(assignment.DeviceId);

            assignment.ReturnDevice(returnDate, condition);
            device.Return();

            await _assignmentRepository.UpdateAsync(assignment);
            await _deviceRepository.UpdateAsync(device);
        }

        /// <summary>
        /// 执行设备校准
        /// </summary>
        public async Task CalibrateDeviceAsync(
            Guid deviceId,
            DateTime calibrationDate,
            DateTime nextCalibrationDate,
            string? accuracy = null)
        {
            var device = await _deviceRepository.GetAsync(deviceId);
            device.Calibrate(calibrationDate, nextCalibrationDate, accuracy);
            await _deviceRepository.UpdateAsync(device);
        }

        /// <summary>
        /// 报废设备
        /// </summary>
        public async Task ScrapDeviceAsync(Guid deviceId, string reason)
        {
            var device = await _deviceRepository.GetAsync(deviceId);
            device.Scrap(reason);
            await _deviceRepository.UpdateAsync(device);
        }

        /// <summary>
        /// 检查设备是否允许校准（业务规则校验入口）
        /// </summary>
        public async Task<bool> CanCalibrateAsync(Guid deviceId)
        {
            var device = await _deviceRepository.GetAsync(deviceId);
            return device.DeviceStatus != DeviceStatus.Scrapped
                && device.DeviceStatus != DeviceStatus.LentOut
                && device.DeviceStatus != DeviceStatus.UnderRepair;
        }
    }
}
