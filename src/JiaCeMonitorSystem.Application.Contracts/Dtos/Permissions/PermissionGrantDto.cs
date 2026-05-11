using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Permissions
{
    /// <summary>
    /// 保存权限授权输入参数
    /// </summary>
    public class PermissionGrantDto
    {
        /// <summary>
        /// 权限提供者名称（Role/User）
        /// </summary>
        [Required]
        public string ProviderName { get; set; } = string.Empty;

        /// <summary>
        /// 权限提供者Key（角色ID或用户ID）
        /// </summary>
        [Required]
        public string ProviderKey { get; set; } = string.Empty;

        /// <summary>
        /// 要授权的权限名称列表
        /// </summary>
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
