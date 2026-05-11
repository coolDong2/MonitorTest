using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Tenants
{
    /// <summary>
    /// 创建租户输入参数
    /// </summary>
    public class TenantCreateDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TenantName { get; set; } = string.Empty;

        /// <summary>
        /// 管理员账号
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AdminAccount { get; set; } = string.Empty;

        /// <summary>
        /// 管理员密码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AdminPassword { get; set; } = string.Empty;

        /// <summary>
        /// 管理员邮箱
        /// </summary>
        [StringLength(200)]
        [EmailAddress]
        public string? AdminEmail { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 数据库连接字符串（可选，独立数据库时使用）
        /// </summary>
        public string? ConnectionString { get; set; }
    }
}
