using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.MonitoringItemTypes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 监测项目类型应用服务接口
    /// </summary>
    public interface IMonitoringItemTypeAppService : IApplicationService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<MonitoringItemTypeDto>> GetPageListAsync(GetMonitoringItemTypeListInput input);

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        Task<List<MonitoringItemTypeDto>> GetListAsync(GetMonitoringItemTypeListInput input);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<MonitoringItemTypeDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建（级联保存属性）
        /// </summary>
        Task<MonitoringItemTypeDto> CreateAsync(MonitoringItemTypeCreateDto input);

        /// <summary>
        /// 更新（级联更新属性）
        /// </summary>
        Task<MonitoringItemTypeDto> UpdateAsync(Guid id, MonitoringItemTypeUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
