using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.SystemModules
{
    /// <summary>
    /// 创建系统菜单模块输入参数
    /// </summary>
    public class SystemModuleCreateDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(100)]
        public string? Icon { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [StringLength(500)]
        public string? UrlAddress { get; set; }

        /// <summary>
        /// 打开目标
        /// </summary>
        [StringLength(50)]
        public string? Target { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool IsMenu { get; set; } = true;

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand { get; set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 是否字段
        /// </summary>
        public bool IsFields { get; set; }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool AllowEdit { get; set; } = true;

        /// <summary>
        /// 允许删除
        /// </summary>
        public bool AllowDelete { get; set; } = true;

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; } = true;

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// 授权
        /// </summary>
        [StringLength(500)]
        public string? Authorize { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
