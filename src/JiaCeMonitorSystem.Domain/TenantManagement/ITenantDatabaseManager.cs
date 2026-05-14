using System;
using System.Threading.Tasks;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户数据库管理器接口
    /// </summary>
    public interface ITenantDatabaseManager
    {
        /// <summary>
        /// 为指定租户创建独立数据库
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="tenantName">租户名称</param>
        /// <returns>新数据库的连接字符串</returns>
        Task<string> CreateDatabaseAsync(Guid tenantId, string tenantName);
    }
}
