using System;
using System.Collections.Generic;

namespace JiaCeMonitorSystem.Dtos.Accounts
{
    /// <summary>
    /// 当前用户信息数据传输对象
    /// </summary>
    public class CurrentUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string? RealName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string? HeadIcon { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string? MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
