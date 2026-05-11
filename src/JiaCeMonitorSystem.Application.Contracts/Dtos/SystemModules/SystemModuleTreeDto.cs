using System;
using System.Collections.Generic;

namespace JiaCeMonitorSystem.Dtos.SystemModules
{
    /// <summary>
    /// 系统菜单模块树形数据传输对象
    /// </summary>
    public class SystemModuleTreeDto
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public Guid Id { get; set; }

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
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<SystemModuleTreeDto> Children { get; set; } = new List<SystemModuleTreeDto>();
    }
}
