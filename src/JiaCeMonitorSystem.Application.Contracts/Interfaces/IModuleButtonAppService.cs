using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ModuleButtons;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 系统菜单按钮应用服务接口
    /// </summary>
    public interface IModuleButtonAppService : IApplicationService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<ModuleButtonDto>> GetPageListAsync(GetModuleButtonListInput input);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<ModuleButtonDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        Task<ModuleButtonDto> CreateAsync(ModuleButtonCreateDto input);

        /// <summary>
        /// 更新
        /// </summary>
        Task<ModuleButtonDto> UpdateAsync(Guid id, ModuleButtonUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
