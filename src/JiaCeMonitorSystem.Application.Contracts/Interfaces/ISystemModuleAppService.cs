using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.SystemModules;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 系统菜单模块应用服务接口
    /// </summary>
    public interface ISystemModuleAppService : IApplicationService
    {
        /// <summary>
        /// 获取树形列表
        /// </summary>
        Task<List<SystemModuleTreeDto>> GetTreeListAsync();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<SystemModuleDto>> GetListAsync(GetSystemModuleListInput input);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<SystemModuleDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        Task<SystemModuleDto> CreateAsync(SystemModuleCreateDto input);

        /// <summary>
        /// 更新
        /// </summary>
        Task<SystemModuleDto> UpdateAsync(Guid id, SystemModuleUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
