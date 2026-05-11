using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Points;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 测点管理应用服务接口
    /// </summary>
    public interface IPointAppService :
        ICrudAppService<PointDto, Guid, GetPointListInput, PointCreateDto, PointUpdateDto>
    {
        /// <summary>
        /// 配置测点阈值
        /// </summary>
        Task ConfigureThresholdAsync(Guid id, decimal? warningThreshold, decimal? alarmThreshold, decimal? changeRateThreshold, decimal? cumulativeThreshold);

        /// <summary>
        /// 获取测点列表（非分页，按项目筛选）
        /// </summary>
        Task<List<PointDto>> GetListAsync(Guid? projectId);

        /// <summary>
        /// 获取测点历史数据简要
        /// </summary>
        Task<List<PointDto>> GetHistoryAsync(Guid projectId);

        /// <summary>
        /// 获取测点可用属性列表
        /// 根据测点关联的监测项目类型，返回所有属性
        /// </summary>
        Task<List<JiaCeMonitorSystem.Dtos.MonitoringItemTypes.MonitoringItemPropertyDto>> GetPropertiesAsync(Guid pointId);
    }
}
