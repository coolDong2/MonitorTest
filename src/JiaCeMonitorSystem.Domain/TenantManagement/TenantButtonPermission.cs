using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户按钮权限实体，记录租户下角色对按钮的权限
    /// </summary>
    public class TenantButtonPermission : AuditedEntity<Guid>, IMultiTenant
    {
        /// <summary>
        /// 关联租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 按钮Id
        /// </summary>
        public Guid ButtonId { get; set; }

        /// <summary>
        /// 角色Id（可选）
        /// </summary>
        public Guid? RoleId { get; set; }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsGranted { get; set; }

        protected TenantButtonPermission()
        {
        }

        public TenantButtonPermission(Guid id, Guid tenantId, Guid buttonId, bool isGranted = true, Guid? roleId = null)
        {
            Id = id;
            TenantId = tenantId;
            ButtonId = buttonId;
            IsGranted = isGranted;
            RoleId = roleId;
        }
    }
}
