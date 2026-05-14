using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.MonitoringItemTypes;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.Monitoring
{
    /// <summary>
    /// 监测项目类型控制器
    /// </summary>
    [Route("api/app/monitoring-item-type")]
    public class MonitoringItemTypeController : JiaCeMonitorSystemController
    {
        private readonly IMonitoringItemTypeAppService _monitoringItemTypeAppService;

        /// <summary>
        /// 初始化监测项目类型控制器
        /// </summary>
        public MonitoringItemTypeController(IMonitoringItemTypeAppService monitoringItemTypeAppService)
        {
            _monitoringItemTypeAppService = monitoringItemTypeAppService;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet("page-list")]
        public virtual Task<PagedResultDto<MonitoringItemTypeDto>> GetPageListAsync([FromQuery] GetMonitoringItemTypeListInput input)
        {
            return _monitoringItemTypeAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        [HttpGet]
        public virtual Task<List<MonitoringItemTypeDto>> GetListAsync([FromQuery] GetMonitoringItemTypeListInput input)
        {
            return _monitoringItemTypeAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<MonitoringItemTypeDto> GetModelAsync(Guid id)
        {
            return _monitoringItemTypeAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 创建（级联保存属性）
        /// </summary>
        [HttpPost]
        public virtual Task<MonitoringItemTypeDto> CreateAsync([FromBody] MonitoringItemTypeCreateDto input)
        {
            return _monitoringItemTypeAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新（级联更新属性）
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<MonitoringItemTypeDto> UpdateAsync(Guid id, [FromBody] MonitoringItemTypeUpdateDto input)
        {
            return _monitoringItemTypeAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _monitoringItemTypeAppService.DeleteAsync(id);
        }
    }
}
