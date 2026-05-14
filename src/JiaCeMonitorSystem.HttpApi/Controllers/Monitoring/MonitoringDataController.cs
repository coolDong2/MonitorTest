using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.MonitoringData;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.Monitoring
{
    /// <summary>
    /// 监测数据控制器
    /// </summary>
    [Route("api/app/monitoring-data")]
    public class MonitoringDataController : JiaCeMonitorSystemController
    {
        private readonly IMonitoringDataAppService _monitoringDataAppService;

        /// <summary>
        /// 初始化监测数据控制器
        /// </summary>
        public MonitoringDataController(IMonitoringDataAppService monitoringDataAppService)
        {
            _monitoringDataAppService = monitoringDataAppService;
        }

        /// <summary>
        /// 获取监测数据列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<MonitoringDataDto>> GetListAsync([FromQuery] GetMonitoringDataListInput input)
        {
            return _monitoringDataAppService.GetListAsync(input);
        }

        /// <summary>
        /// 根据测点ID获取历史数据
        /// </summary>
        [HttpGet("history/{pointId}")]
        public virtual Task<PagedResultDto<MonitoringDataHistoryDto>> GetHistoryListByPointIdAsync(
            Guid pointId,
            [FromQuery] int currentPage = 1,
            [FromQuery] int pageSize = 10)
        {
            return _monitoringDataAppService.GetHistoryListByPointIdAsync(pointId, currentPage, pageSize);
        }

        /// <summary>
        /// 获取单条监测数据
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<MonitoringDataDto> GetAsync(Guid id)
        {
            return _monitoringDataAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建监测数据
        /// </summary>
        [HttpPost]
        public virtual Task<MonitoringDataDto> CreateAsync([FromBody] CreateMonitoringDataDto input)
        {
            return _monitoringDataAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新监测数据
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<MonitoringDataDto> UpdateAsync(Guid id, [FromBody] UpdateMonitoringDataDto input)
        {
            return _monitoringDataAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除监测数据
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _monitoringDataAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 批量导入监测数据
        /// </summary>
        [HttpPost("batch-import")]
        public virtual Task BatchImportAsync([FromBody] CreateMonitoringDataDto[] inputs)
        {
            return _monitoringDataAppService.BatchImportAsync(inputs);
        }

        /// <summary>
        /// 审核数据
        /// </summary>
        [HttpPost("{id}/approve")]
        public virtual Task ApproveAsync(Guid id)
        {
            return _monitoringDataAppService.ApproveAsync(id);
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        [HttpGet("export")]
        public virtual Task<byte[]> ExportAsync([FromQuery] GetMonitoringDataListInput input)
        {
            return _monitoringDataAppService.ExportAsync(input);
        }
    }
}
