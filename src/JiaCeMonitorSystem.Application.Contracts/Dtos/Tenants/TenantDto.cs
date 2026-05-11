using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Tenants
{
    /// <summary>
    /// 租户数据传输对象
    /// </summary>
    public class TenantDto : EntityDto<Guid>
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; } = string.Empty;

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string? ConnectionString { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
