using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户用户登录输入参数
    /// </summary>
    public class TenantUserLoginDto
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        [Required]
        public string UnitCode { get; set; } = string.Empty;

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
