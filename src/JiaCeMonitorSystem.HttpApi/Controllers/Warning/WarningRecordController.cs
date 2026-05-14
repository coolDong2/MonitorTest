using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.WarningRecords;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.Warning
{
    /// <summary>
    /// 预警记录控制器
    /// </summary>
    [Route("api/app/warning-record")]
    public class WarningRecordController : JiaCeMonitorSystemController
    {
        private readonly IWarningRecordAppService _warningRecordAppService;

        /// <summary>
        /// 初始化预警记录控制器
        /// </summary>
        public WarningRecordController(IWarningRecordAppService warningRecordAppService)
        {
            _warningRecordAppService = warningRecordAppService;
        }

        /// <summary>
        /// 获取预警记录列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<WarningRecordDto>> GetListAsync([FromQuery] GetWarningListInput input)
        {
            return _warningRecordAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单条预警记录
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<WarningRecordDto> GetAsync(Guid id)
        {
            return _warningRecordAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建预警记录
        /// </summary>
        [HttpPost]
        public virtual Task<WarningRecordDto> CreateAsync([FromBody] HandleWarningInput input)
        {
            return _warningRecordAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新预警记录
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<WarningRecordDto> UpdateAsync(Guid id, [FromBody] ConfirmWarningInput input)
        {
            return _warningRecordAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除预警记录
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _warningRecordAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 分配处理人
        /// </summary>
        [HttpPost("{id}/assign-handler")]
        public virtual Task AssignHandlerAsync(
            Guid id,
            [FromQuery] Guid handlerId,
            [FromQuery] string handlerName)
        {
            return _warningRecordAppService.AssignHandlerAsync(id, handlerId, handlerName);
        }

        /// <summary>
        /// 处理预警记录
        /// </summary>
        [HttpPost("handle")]
        public virtual Task HandleAsync([FromBody] HandleWarningInput input)
        {
            return _warningRecordAppService.HandleAsync(input);
        }

        /// <summary>
        /// 确认预警处理结果
        /// </summary>
        [HttpPost("confirm")]
        public virtual Task ConfirmAsync([FromQuery] ConfirmWarningInput input)
        {
            return _warningRecordAppService.ConfirmAsync(input);
        }

        /// <summary>
        /// 关闭预警
        /// </summary>
        [HttpPost("{id}/close")]
        public virtual Task CloseAsync(Guid id, [FromQuery] string reason)
        {
            return _warningRecordAppService.CloseAsync(id, reason);
        }

        /// <summary>
        /// 获取预警统计
        /// </summary>
        [HttpGet("statistics")]
        public virtual Task<WarningStatisticsDto> GetStatisticsAsync(
            [FromQuery] Guid? projectId,
            [FromQuery] Guid? pointId)
        {
            return _warningRecordAppService.GetStatisticsAsync(projectId, pointId);
        }
    }
}
