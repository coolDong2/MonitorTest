using System;
using System.Text.Json;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.MonitoringData
{
    /// <summary>
    /// 监测数据传输对象
    /// </summary>
    public class MonitoringDataDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 测点ID
        /// </summary>
        public Guid PointId { get; set; }

        /// <summary>
        /// 测点名称
        /// </summary>
        public string PointName { get; set; } = string.Empty;

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 监测项目类型ID
        /// </summary>
        public Guid? ItemTypeId { get; set; }

        /// <summary>
        /// 监测属性ID（外键，关联MonitoringItemProperty）
        /// 【重构新增】解决同一测点下多属性数据无法区分的问题
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// 属性编码（冗余，如 DISPLACEMENT_X）
        /// </summary>
        public string PropertyCode { get; set; } = string.Empty;

        /// <summary>
        /// 属性名称（冗余，如"水平位移"）
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 单位（冗余，如 mm）
        /// </summary>
        public string? Unit { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        public DateTime MonitoringTime { get; set; }

        /// <summary>
        /// 监测数值
        /// </summary>
        public decimal MonitoringValue { get; set; }

        /// <summary>
        /// 数据质量
        /// </summary>
        public int DataQuality { get; set; }

        /// <summary>
        /// 数据质量文本
        /// </summary>
        public string DataQualityText { get; set; } = string.Empty;

        /// <summary>
        /// 数据状态
        /// </summary>
        public int DataState { get; set; }

        /// <summary>
        /// 数据状态文本
        /// </summary>
        public string DataStateText { get; set; } = string.Empty;

        /// <summary>
        /// 采集设备ID
        /// </summary>
        public Guid? DeviceId { get; set; }

        /// <summary>
        /// 采集设备名称
        /// </summary>
        public string? DeviceName { get; set; }

        /// <summary>
        /// 采集人ID
        /// </summary>
        public Guid? CollectorId { get; set; }

        /// <summary>
        /// 采集人姓名
        /// </summary>
        public string? CollectorName { get; set; }

        /// <summary>
        /// 采集方式
        /// </summary>
        public int CollectionMethod { get; set; }

        /// <summary>
        /// 采集方式文本
        /// </summary>
        public string CollectionMethodText { get; set; } = string.Empty;

        /// <summary>
        /// 扩展数据
        /// </summary>
        public JsonDocument? ExtendedData { get; set; }

        /// <summary>
        /// 数据备注
        /// </summary>
        public string? DataRemark { get; set; }
    }
}
