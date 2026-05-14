using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.AppRoles;
using JiaCeMonitorSystem.Dtos.TenantManagement;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 租户菜单应用服务接口
    /// </summary>
    public interface ITenantMenuAppService : IApplicationService
    {
        /// <summary>
        /// 获取当前租户可用菜单树
        /// </summary>
        Task<List<TenantMenuDto>> GetCurrentTenantMenusAsync();

        /// <summary>
        /// 获取角色的模块权限树（基于 SystemModule，替代原 AppRole 权限树）
        /// </summary>
        Task<List<PermissionTreeDto>> GetRolePermissionTreeAsync(Guid roleId);

        /// <summary>
        /// 获取角色的字段权限树（基于 SystemModule，替代原 AppRole 字段权限树）
        /// </summary>
        Task<List<PermissionTreeDto>> GetRolePermissionFieldsTreeAsync(Guid roleId, string? moduleIds);
    }
}
