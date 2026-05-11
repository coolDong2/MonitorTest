using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.SystemDictionaries;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 系统字典类型应用服务接口
    /// </summary>
    public interface ISystemDictionaryTypeAppService : IApplicationService
    {
        /// <summary>
        /// 获取字典树
        /// </summary>
        Task<List<SystemDictionaryTypeTreeDto>> GetDictionaryTreeAsync();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<SystemDictionaryTypeDto>> GetPageListAsync(GetSystemDictionaryTypeListInput input);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<SystemDictionaryTypeDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        Task<SystemDictionaryTypeDto> CreateAsync(SystemDictionaryTypeCreateDto input);

        /// <summary>
        /// 更新
        /// </summary>
        Task<SystemDictionaryTypeDto> UpdateAsync(Guid id, SystemDictionaryTypeUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
