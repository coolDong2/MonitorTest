using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace JiaCeMonitorSystem.Dtos.Points
{
    /// <summary>
    /// 创建测点输入参数
    /// </summary>
    public class PointCreateDto
    {
        /// <summary>
        /// 所属项目ID
        /// </summary>
        [Required]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 监测点编号
        /// </summary>
        [Required]
        [StringLength(100)]
        public string PointCode { get; set; } = string.Empty;

        /// <summary>
        /// 监测点名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string PointName { get; set; } = string.Empty;

        /// <summary>
        /// 监测项目类型ID
        /// </summary>
        public Guid? ItemTypeId { get; set; }

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
        /// 扩展属性
        /// </summary>
        public JsonDocument? ExtendedProperties { get; set; }

        /// <summary>
        /// 点位描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
