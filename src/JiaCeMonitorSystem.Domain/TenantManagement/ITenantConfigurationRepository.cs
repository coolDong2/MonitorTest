using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户配置仓储接口
    /// </summary>
    public interface ITenantConfigurationRepository : IRepository<TenantConfiguration, Guid>
    {
        /// <summary>
        /// 根据租户Id获取配置
        /// </summary>
        Task<TenantConfiguration?> GetByTenantIdAsync(Guid tenantId);

        /// <summary>
        /// 根据单位编码获取配置
        /// </summary>
        Task<TenantConfiguration?> GetByUnitCodeAsync(string unitCode);

        /// <summary>
        /// 获取即将到期的租户列表
        /// </summary>
        Task<List<TenantConfiguration>> GetExpiringTenantsAsync(DateTime expireBefore);
    }
}
