using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.SystemDictionaries;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 系统字典应用服务接口
    /// </summary>
    public interface ISystemDictionaryAppService : IApplicationService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<SystemDictionaryDto>> GetPageListAsync(GetSystemDictionaryListInput input);

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        Task<List<SystemDictionaryDto>> GetListAsync(GetSystemDictionaryListInput input);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<SystemDictionaryDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        Task<SystemDictionaryDto> CreateAsync(SystemDictionaryCreateDto input);

        /// <summary>
        /// 更新
        /// </summary>
        Task<SystemDictionaryDto> UpdateAsync(Guid id, SystemDictionaryUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
