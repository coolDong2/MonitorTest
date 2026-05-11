using System;
using System.Collections.Generic;

namespace JiaCeMonitorSystem.Dtos.AppRoles
{
    /// <summary>
    /// 权限树数据传输对象
    /// </summary>
    public class PermissionTreeDto
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string? UrlAddress { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<PermissionTreeDto> Children { get; set; } = new();
    }
}
