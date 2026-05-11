using System;

namespace JiaCeMonitorSystem.Dtos.Roles
{
    /// <summary>
    /// 角色用户简要信息DTO
    /// </summary>
    public class RoleUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
