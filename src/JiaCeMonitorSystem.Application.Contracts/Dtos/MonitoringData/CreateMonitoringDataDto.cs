using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace JiaCeMonitorSystem.Dtos.MonitoringData
{
    /// <summary>
    /// 创建监测数据输入参数
    /// </summary>
    public class CreateMonitoringDataDto
    {
        /// <summary>
        /// 测点ID
        /// </summary>
        [Required]
        public Guid PointId { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        [Required]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        [Required]
        public DateTime MonitoringTime { get; set; }

        /// <summary>
        /// 监测数值
        /// </summary>
        [Required]
        public decimal MonitoringValue { get; set; }

        /// <summary>
        /// 监测项目类型ID
        /// </summary>
        public Guid? ItemTypeId { get; set; }

        /// <summary>
        /// 监测属性ID（必填）
        /// 【重构新增】解决同一测点下多属性数据无法区分的问题
        /// </summary>
        [Required]
        public Guid PropertyId { get; set; }

        /// <summary>
        /// 属性编码（冗余）
        /// </summary>
        [StringLength(100)]
        public string PropertyCode { get; set; } = string.Empty;

        /// <summary>
        /// 属性名称（冗余）
        /// </summary>
        [StringLength(100)]
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 单位（冗余）
        /// </summary>
        [StringLength(50)]
        public string? Unit { get; set; }

        /// <summary>
        /// 采集设备ID
        /// </summary>
        public Guid? DeviceId { get; set; }

        /// <summary>
        /// 采集设备名称（冗余）
        /// </summary>
        [StringLength(256)]
        public string? DeviceName { get; set; }

        /// <summary>
        /// 采集人ID
        /// </summary>
        public Guid? CollectorId { get; set; }

        /// <summary>
        /// 采集人姓名（冗余）
        /// </summary>
        [StringLength(128)]
        public string? CollectorName { get; set; }

        /// <summary>
        /// 采集方式
        /// </summary>
        public int CollectionMethod { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public JsonDocument? ExtendedData { get; set; }

        /// <summary>
        /// 数据备注
        /// </summary>
        [StringLength(500)]
        public string? DataRemark { get; set; }
    }
}
