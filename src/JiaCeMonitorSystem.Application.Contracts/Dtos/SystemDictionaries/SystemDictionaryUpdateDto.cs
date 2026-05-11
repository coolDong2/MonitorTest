using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.SystemDictionaries
{
    /// <summary>
    /// 更新系统字典输入参数
    /// </summary>
    public class SystemDictionaryUpdateDto
    {
        /// <summary>
        /// 字典编码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 字典名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// 简拼
        /// </summary>
        [StringLength(200)]
        public string? SimpleSpelling { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
