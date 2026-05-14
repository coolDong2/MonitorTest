using System.Threading;
using System.Threading.Tasks;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户数据清理器接口（回滚用）
    /// </summary>
    public interface ITenantDataCleaner
    {
        /// <summary>
        /// 清理目标独立数据库
        /// </summary>
        Task CleanAsync(string targetConnectionString, CancellationToken cancellationToken = default);
    }
}
