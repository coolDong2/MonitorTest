using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户用户扩展实体，存储用户的租户相关扩展信息
    /// </summary>
    public class TenantUserExtension : AuditedEntity<Guid>, IMultiTenant
    {
        /// <summary>
        /// 关联用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public string? UnitCode { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 关联租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

        protected TenantUserExtension()
        {
        }

        public TenantUserExtension(Guid id, Guid userId, Guid tenantId, UserType userType = UserType.TenantUser, string? unitCode = null)
        {
            Id = id;
            UserId = userId;
            TenantId = tenantId;
            UserType = userType;
            UnitCode = unitCode;
        }
    }
}
