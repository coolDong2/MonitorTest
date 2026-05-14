using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Devices;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.Equipment
{
    /// <summary>
    /// 单位设备控制器
    /// </summary>
    [Route("api/app/company-device")]
    public class CompanyDeviceController : JiaCeMonitorSystemController
    {
        private readonly ICompanyDeviceAppService _companyDeviceAppService;

        /// <summary>
        /// 初始化设备控制器
        /// </summary>
        public CompanyDeviceController(ICompanyDeviceAppService companyDeviceAppService)
        {
            _companyDeviceAppService = companyDeviceAppService;
        }

        /// <summary>
        /// 获取设备分页列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<CompanyDeviceDto>> GetListAsync([FromQuery] GetDeviceListInput input)
        {
            return _companyDeviceAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取设备列表（非分页，支持筛选）
        /// </summary>
        [HttpGet("list")]
        public virtual Task<List<CompanyDeviceDto>> GetDeviceListAsync(
            [FromQuery] Guid? companyId,
            [FromQuery] int? deviceType,
            [FromQuery] string? keyword,
            [FromQuery] int? deviceStatus,
            [FromQuery] bool? checkCalibration)
        {
            return _companyDeviceAppService.GetListAsync(companyId, deviceType, keyword, deviceStatus, checkCalibration);
        }

        /// <summary>
        /// 获取设备分配信息（已借出记录）
        /// </summary>
        [HttpGet("send-device/{deviceId}")]
        public virtual Task<List<DeviceAssignmentDetailDto>> GetSendDeviceAsync(Guid deviceId)
        {
            return _companyDeviceAppService.GetSendDeviceAsync(deviceId);
        }

        /// <summary>
        /// 获取单个设备
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<CompanyDeviceDto> GetAsync(Guid id)
        {
            return _companyDeviceAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建设备
        /// </summary>
        [HttpPost]
        public virtual Task<CompanyDeviceDto> CreateAsync([FromBody] CompanyDeviceCreateDto input)
        {
            return _companyDeviceAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新设备
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<CompanyDeviceDto> UpdateAsync(Guid id, [FromBody] CompanyDeviceUpdateDto input)
        {
            return _companyDeviceAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companyDeviceAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 设备校准
        /// </summary>
        [HttpPost("calibrate")]
        public virtual Task CalibrateAsync([FromBody] CalibrateDeviceInput input)
        {
            return _companyDeviceAppService.CalibrateAsync(input);
        }

        /// <summary>
        /// 借出设备
        /// </summary>
        [HttpPost("{deviceId}/lend")]
        public virtual Task<DeviceAssignmentDto> LendAsync(
            Guid deviceId,
            [FromQuery] Guid projectId,
            [FromQuery] DateTime expectedReturnDate,
            [FromQuery] Guid? receiverId,
            [FromQuery] string? receiverName)
        {
            return _companyDeviceAppService.LendAsync(deviceId, projectId, expectedReturnDate, receiverId, receiverName);
        }

        /// <summary>
        /// 归还设备
        /// </summary>
        [HttpPost("{assignmentId}/return")]
        public virtual Task ReturnAsync(
            Guid assignmentId,
            [FromQuery] DateTime returnDate,
            [FromQuery] string? condition)
        {
            return _companyDeviceAppService.ReturnAsync(assignmentId, returnDate, condition);
        }

        /// <summary>
        /// 报废设备
        /// </summary>
        [HttpPost("{id}/scrap")]
        public virtual Task ScrapAsync(Guid id, [FromQuery] string reason)
        {
            return _companyDeviceAppService.ScrapAsync(id, reason);
        }
    }
}
