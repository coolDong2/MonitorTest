using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Points
{
    /// <summary>
    /// 测点数据传输对象
    /// </summary>
    public class PointDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 所属项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 所属项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 监测点编号
        /// </summary>
        public string PointCode { get; set; } = string.Empty;

        /// <summary>
        /// 监测点名称
        /// </summary>
        public string PointName { get; set; } = string.Empty;

        /// <summary>
        /// 监测项目类型ID
        /// </summary>
        public Guid? ItemTypeId { get; set; }

        /// <summary>
        /// 监测项目类型名称
        /// </summary>
        public string? ItemTypeName { get; set; }

        /// <summary>
        /// 监测项目分类
        /// </summary>
        public int ItemCategory { get; set; }

        /// <summary>
        /// 监测项目分类名称
        /// </summary>
        public string ItemCategoryName { get; set; } = string.Empty;

        /// <summary>
        /// X坐标/经度
        /// </summary>
        public decimal? LocationX { get; set; }

        /// <summary>
        /// Y坐标/纬度
        /// </summary>
        public decimal? LocationY { get; set; }

        /// <summary>
        /// Z坐标/高程
        /// </summary>
        public decimal? LocationZ { get; set; }

        /// <summary>
        /// 当前监测值
        /// </summary>
        public decimal? CurrentValue { get; set; }

        /// <summary>
        /// 最后监测时间
        /// </summary>
        public DateTime? LastMonitoringTime { get; set; }

        /// <summary>
        /// 监测频率（天）
        /// </summary>
        public int? MonitoringFrequency { get; set; }

        /// <summary>
        /// 预警阈值
        /// </summary>
        public decimal? WarningThreshold { get; set; }

        /// <summary>
        /// 报警阈值
        /// </summary>
        public decimal? AlarmThreshold { get; set; }

        /// <summary>
        /// 变化率阈值
        /// </summary>
        public decimal? ChangeRateThreshold { get; set; }

        /// <summary>
        /// 累计变化阈值
        /// </summary>
        public decimal? CumulativeThreshold { get; set; }

        /// <summary>
        /// 当前预警级别
        /// </summary>
        public int CurrentWarningLevel { get; set; }

        /// <summary>
        /// 当前预警级别文本
        /// </summary>
        public string CurrentWarningLevelText { get; set; } = string.Empty;

        /// <summary>
        /// 最后预警时间
        /// </summary>
        public DateTime? LastWarningTime { get; set; }

        /// <summary>
        /// 总预警次数
        /// </summary>
        public int TotalWarningCount { get; set; }

        /// <summary>
        /// 当前活跃预警数
        /// </summary>
        public int ActiveWarningCount { get; set; }

        /// <summary>
        /// 点位描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 测点属性列表（关联的监测项目属性）
        /// </summary>
        public List<JiaCeMonitorSystem.Dtos.MonitoringItemTypes.MonitoringItemPropertyDto> Properties { get; set; } = new();
    }
}
