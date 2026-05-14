using System;
using System.Threading;
using System.Threading.Tasks;
namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户数据验证器接口
    /// </summary>
    public interface ITenantDataValidator
    {
        /// <summary>
        /// 验证共享库与独立库中租户数据的一致性
        /// </summary>
        Task<TenantDataValidationResult> ValidateAsync(Guid tenantId, string sourceConnectionString, string targetConnectionString, CancellationToken cancellationToken = default);
    }
}
