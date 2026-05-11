using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.SystemDictionaries
{
    /// <summary>
    /// 创建系统字典类型输入参数
    /// </summary>
    public class SystemDictionaryTypeCreateDto
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

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
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 是否树形
        /// </summary>
        public bool IsTree { get; set; }

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
    }
}
