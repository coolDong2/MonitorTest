using System;
using System.Threading;
using System.Threading.Tasks;
namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户数据导出器接口
    /// </summary>
    public interface ITenantDataExporter
    {
        /// <summary>
        /// 将指定租户的数据从共享库导出到独立库
        /// </summary>
        Task<TenantDataExportResult> ExportAsync(Guid tenantId, string sourceConnectionString, string targetConnectionString, CancellationToken cancellationToken = default);
    }
}
