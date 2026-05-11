using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Accounts
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
    public class LoginInputDto
    {
        /// <summary>
        /// 用户名/账号
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Account { get; set; } = string.Empty;

        /// <summary>
        /// 密码（支持 AES 加密传输，密文为 Base64 格式）
        /// </summary>
        /// <remarks>
        /// 前端应使用 AES 加密后传输，加密密钥通过 /api/app/account/encrypt-key 获取。
        /// 也可直接传输明文（兼容模式），但生产环境强烈建议加密。
        /// </remarks>
        [Required]
        [StringLength(512)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 租户标识（Header优先级高于此字段）
        /// </summary>
        public string? TenantName { get; set; }
    }
}
