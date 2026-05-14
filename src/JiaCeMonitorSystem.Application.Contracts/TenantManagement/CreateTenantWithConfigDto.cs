using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 创建租户并附带配置信息的输入参数
    /// </summary>
    public class CreateTenantWithConfigDto
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required]
        [StringLength(TenantConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 单位编码，用于租户用户登录
        /// </summary>
        [Required]
        [StringLength(TenantConsts.MaxUnitCodeLength)]
        public string UnitCode { get; set; } = string.Empty;

        /// <summary>
        /// 管理员邮箱
        /// </summary>
        [EmailAddress]
        public string? AdminEmail { get; set; }

        /// <summary>
        /// 管理员初始密码
        /// </summary>
        [StringLength(100)]
        public string? AdminPassword { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 是否使用独立数据库
        /// </summary>
        public bool UseIndependentDatabase { get; set; }

        /// <summary>
        /// 授予的模块Id列表
        /// </summary>
        public List<Guid> GrantedModuleIds { get; set; } = new();

        /// <summary>
        /// 许可证配额信息
        /// </summary>
        public TenantLicenseDto? License { get; set; }
    }
}
