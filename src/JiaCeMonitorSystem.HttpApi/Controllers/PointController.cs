using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Points;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 测点管理控制器
    /// </summary>
    [Route("api/app/point")]
    public class PointController : JiaCeMonitorSystemController
    {
        private readonly IPointAppService _pointAppService;

        /// <summary>
        /// 初始化测点控制器
        /// </summary>
        public PointController(IPointAppService pointAppService)
        {
            _pointAppService = pointAppService;
        }

        /// <summary>
        /// 获取测点分页列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<PointDto>> GetListAsync([FromQuery] GetPointListInput input)
        {
            return _pointAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取测点列表（非分页，按项目筛选）
        /// </summary>
        [HttpGet("list")]
        public virtual Task<List<PointDto>> GetPointListAsync([FromQuery] Guid? projectId)
        {
            return _pointAppService.GetListAsync(projectId);
        }

        /// <summary>
        /// 获取单个测点
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<PointDto> GetAsync(Guid id)
        {
            return _pointAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建测点
        /// </summary>
        [HttpPost]
        public virtual Task<PointDto> CreateAsync([FromBody] PointCreateDto input)
        {
            return _pointAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新测点
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<PointDto> UpdateAsync(Guid id, [FromBody] PointUpdateDto input)
        {
            return _pointAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除测点
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _pointAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 配置测点阈值
        /// </summary>
        [HttpPost("{id}/configure-threshold")]
        public virtual Task ConfigureThresholdAsync(
            Guid id,
            [FromQuery] decimal? warningThreshold,
            [FromQuery] decimal? alarmThreshold,
            [FromQuery] decimal? changeRateThreshold,
            [FromQuery] decimal? cumulativeThreshold)
        {
            return _pointAppService.ConfigureThresholdAsync(id, warningThreshold, alarmThreshold, changeRateThreshold, cumulativeThreshold);
        }

        /// <summary>
        /// 获取测点历史数据简要
        /// </summary>
        [HttpGet("history")]
        public virtual Task<List<PointDto>> GetHistoryAsync([FromQuery] Guid projectId)
        {
            return _pointAppService.GetHistoryAsync(projectId);
        }

        /// <summary>
        /// 获取测点可用属性列表
        /// 根据测点关联的监测项目类型，返回所有属性
        /// </summary>
        [HttpGet("{id}/properties")]
        public virtual Task<List<JiaCeMonitorSystem.Dtos.MonitoringItemTypes.MonitoringItemPropertyDto>> GetPropertiesAsync(Guid id)
        {
            return _pointAppService.GetPropertiesAsync(id);
        }
    }
}
