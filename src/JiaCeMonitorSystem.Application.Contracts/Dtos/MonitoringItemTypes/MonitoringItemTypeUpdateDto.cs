using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.MonitoringItemTypes
{
    /// <summary>
    /// 更新监测项目类型输入参数
    /// </summary>
    public class MonitoringItemTypeUpdateDto
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
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public List<MonitoringItemPropertyCreateDto> PropertyList { get; set; } = new List<MonitoringItemPropertyCreateDto>();
    }
}
