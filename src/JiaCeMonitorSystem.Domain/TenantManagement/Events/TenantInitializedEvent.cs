using System;
using Volo.Abp.EventBus;

namespace JiaCeMonitorSystem.TenantManagement.Events
{
    /// <summary>
    /// 租户初始化完成领域事件
    /// </summary>
    [EventName("JiaCeMonitorSystem.Tenant.Initialized")]
    public class TenantInitializedEvent
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public string UnitCode { get; set; } = string.Empty;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public TenantDatabaseType DatabaseType { get; set; }
    }
}
