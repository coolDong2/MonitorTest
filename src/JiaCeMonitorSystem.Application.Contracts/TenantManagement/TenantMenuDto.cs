using System.Collections.Generic;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户菜单数据传输对象
    /// </summary>
    public class TenantMenuDto
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 父级菜单编码
        /// </summary>
        public string? ParentCode { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<TenantMenuDto> Children { get; set; } = new();
    }
}
