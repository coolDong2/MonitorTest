using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.MonitoringItemTypes
{
    /// <summary>
    /// 创建监测项目类型输入参数
    /// </summary>
    public class MonitoringItemTypeCreateDto
    {
        /// <summary>
        /// 类型编码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TypeCode { get; set; } = string.Empty;

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 监测分类
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; } = true;

        /// <summary>
        /// 属性列表
        /// </summary>
        public List<MonitoringItemPropertyCreateDto> PropertyList { get; set; } = new List<MonitoringItemPropertyCreateDto>();
    }

    /// <summary>
    /// 创建监测项目属性输入参数
    /// </summary>
    public class MonitoringItemPropertyCreateDto
    {
        /// <summary>
        /// 属性编码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PropertyCode { get; set; } = string.Empty;

        /// <summary>
        /// 属性名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 数据类型
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(50)]
        public string? Unit { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
