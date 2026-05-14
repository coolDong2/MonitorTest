using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户模块授权应用服务接口
    /// </summary>
    public interface ITenantModuleGrantAppService : IApplicationService
    {
        /// <summary>
        /// 批量授予模块权限
        /// </summary>
        Task GrantModulesAsync(Guid tenantId, List<Guid> moduleIds);

        /// <summary>
        /// 撤销单个模块权限
        /// </summary>
        Task RevokeModuleAsync(Guid tenantId, Guid moduleId);

        /// <summary>
        /// 获取租户已授权的菜单列表
        /// </summary>
        Task<List<TenantMenuDto>> GetGrantedMenusAsync(Guid tenantId);
    }
}
