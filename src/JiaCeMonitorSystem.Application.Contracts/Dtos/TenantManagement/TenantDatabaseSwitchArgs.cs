using System;

namespace JiaCeMonitorSystem.Dtos.TenantManagement
{
    /// <summary>
    /// 租户数据库切换任务参数
    /// </summary>
    [Serializable]
    public class TenantDatabaseSwitchArgs
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; } = string.Empty;
    }
}
