using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ModuleButtons;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户按钮权限应用服务接口
    /// </summary>
    public interface ITenantButtonPermissionAppService : IApplicationService
    {
        /// <summary>
        /// 批量授予角色按钮权限（幂等）
        /// </summary>
        Task GrantButtonsAsync(Guid roleId, List<Guid> buttonIds);

        /// <summary>
        /// 批量撤销角色按钮权限
        /// </summary>
        Task RevokeButtonsAsync(Guid roleId, List<Guid> buttonIds);

        /// <summary>
        /// 获取角色的按钮权限列表（可按模块筛选）
        /// </summary>
        Task<List<ButtonPermissionDto>> GetRoleButtonPermissionsAsync(Guid roleId, Guid? moduleId = null);

        /// <summary>
        /// 获取当前用户在指定模块下的可用按钮
        /// </summary>
        Task<List<ModuleButtonDto>> GetMyAvailableButtonsAsync(Guid moduleId);
    }
}
