using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Devices;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 单位设备应用服务接口
    /// </summary>
    public interface ICompanyDeviceAppService :
        ICrudAppService<CompanyDeviceDto, Guid, GetDeviceListInput, CompanyDeviceCreateDto, CompanyDeviceUpdateDto>
    {
        /// <summary>
        /// 获取设备列表（非分页，支持筛选）
        /// </summary>
        Task<List<CompanyDeviceDto>> GetListAsync(Guid? companyId, int? deviceType, string? keyword, int? deviceStatus, bool? checkCalibration);

        /// <summary>
        /// 获取设备分配信息（已借出记录）
        /// </summary>
        Task<List<DeviceAssignmentDetailDto>> GetSendDeviceAsync(Guid deviceId);

        /// <summary>
        /// 设备校准
        /// </summary>
        Task CalibrateAsync(CalibrateDeviceInput input);

        /// <summary>
        /// 借出设备
        /// </summary>
        Task<DeviceAssignmentDto> LendAsync(Guid deviceId, Guid projectId, DateTime expectedReturnDate, Guid? receiverId, string? receiverName);

        /// <summary>
        /// 归还设备
        /// </summary>
        Task ReturnAsync(Guid assignmentId, DateTime returnDate, string? condition);

        /// <summary>
        /// 报废设备
        /// </summary>
        Task ScrapAsync(Guid id, string reason);
    }
}
