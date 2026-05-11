using System.Collections.Generic;

namespace JiaCeMonitorSystem.Dtos.Permissions
{
    /// <summary>
    /// 权限树节点数据传输对象
    /// </summary>
    public class PermissionTreeDto
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 父级权限名称
        /// </summary>
        public string? ParentName { get; set; }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsGranted { get; set; }

        /// <summary>
        /// 子权限列表
        /// </summary>
        public List<PermissionTreeDto> Children { get; set; } = new List<PermissionTreeDto>();
    }
}
