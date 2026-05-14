using System;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户数据库切换状态变更事件（用于 SignalR 实时推送）
    /// </summary>
    [Serializable]
    public class TenantDatabaseSwitchStatusEto
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; } = string.Empty;

        /// <summary>
        /// 状态：started / completed / failed
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 状态消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 独立数据库连接字符串（仅在 completed 时有值）
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// 事件时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
