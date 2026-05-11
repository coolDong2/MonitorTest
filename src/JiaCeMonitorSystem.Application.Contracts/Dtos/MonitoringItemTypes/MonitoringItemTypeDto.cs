using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.MonitoringItemTypes
{
    /// <summary>
    /// 监测项目类型数据传输对象
    /// </summary>
    public class MonitoringItemTypeDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 类型编码
        /// </summary>
        public string TypeCode { get; set; } = string.Empty;

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 监测分类
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 监测分类文本
        /// </summary>
        public string CategoryText { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
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
        /// 属性集合
        /// </summary>
        public List<MonitoringItemPropertyDto> Properties { get; set; } = new List<MonitoringItemPropertyDto>();
    }

    /// <summary>
    /// 监测项目属性数据传输对象
    /// </summary>
    public class MonitoringItemPropertyDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 所属监测项目类型ID
        /// </summary>
        public Guid ItemTypeId { get; set; }

        /// <summary>
        /// 属性编码
        /// </summary>
        public string PropertyCode { get; set; } = string.Empty;

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 数据类型
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 数据类型文本
        /// </summary>
        public string DataTypeText { get; set; } = string.Empty;

        /// <summary>
        /// 单位
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
