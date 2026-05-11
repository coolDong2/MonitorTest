using System;
namespace JiaCeMonitorSystem.Events
{
    /// <summary>
    /// 租户初始化领域事件，新租户创建后发布，用于异步初始化默认数据
    /// </summary>
    public class TenantInitializedDomainEvent
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
        /// 管理员用户ID
        /// </summary>
        public Guid AdminUserId { get; set; }

        /// <summary>
        /// 管理员账号
        /// </summary>
        public string AdminAccount { get; set; } = string.Empty;
    }
}
