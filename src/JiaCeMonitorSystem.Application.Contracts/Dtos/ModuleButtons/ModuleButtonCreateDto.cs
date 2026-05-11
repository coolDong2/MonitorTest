using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.ModuleButtons
{
    /// <summary>
    /// 创建系统菜单按钮输入参数
    /// </summary>
    public class ModuleButtonCreateDto
    {
        /// <summary>
        /// 所属模块ID
        /// </summary>
        [Required]
        public Guid ModuleId { get; set; }

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
        /// 按钮位置
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// JS事件
        /// </summary>
        [StringLength(100)]
        public string? JsEvent { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [StringLength(500)]
        public string? UrlAddress { get; set; }

        /// <summary>
        /// 是否有分割线
        /// </summary>
        public bool Split { get; set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; set; }

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
    }
}
