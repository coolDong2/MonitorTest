using System;
using System.Collections.Generic;

namespace JiaCeMonitorSystem.Dtos.Accounts
{
    /// <summary>
    /// 登录输出参数
    /// </summary>
    public class LoginOutputDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 登录令牌
        /// </summary>
        public string LoginToken { get; set; } = string.Empty;

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// 令牌过期时间（秒）
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 用户显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 当前用户权限列表
        /// </summary>
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
