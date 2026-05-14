using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户模块授权实体，记录租户被授予的系统模块权限
    /// </summary>
    public class TenantModuleGrant : AuditedEntity<Guid>, IMultiTenant
    {
        /// <summary>
        /// 关联租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 系统模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsGranted { get; set; }

        /// <summary>
        /// 授权日期
        /// </summary>
        public DateTime? GrantDate { get; set; }

        /// <summary>
        /// 授权到期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        protected TenantModuleGrant()
        {
        }

        public TenantModuleGrant(Guid id, Guid tenantId, Guid moduleId, bool isGranted = true)
        {
            Id = id;
            TenantId = tenantId;
            ModuleId = moduleId;
            IsGranted = isGranted;
            GrantDate = DateTime.Now;
        }
    }
}
