using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.WarningRecords
{
    /// <summary>
    /// 预警记录数据传输对象
    /// </summary>
    public class WarningRecordDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 监测点ID
        /// </summary>
        public Guid PointId { get; set; }

        /// <summary>
        /// 监测点名称
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
        /// 监测数据ID
        /// </summary>
        public Guid? MonitoringDataId { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public int DataState { get; set; }

        /// <summary>
        /// 采集人姓名
        /// </summary>
        public string? CollectorName { get; set; }

        /// <summary>
        /// 数据备注
        /// </summary>
        public string? DataRemark { get; set; }

        /// <summary>
        /// 预警时间
        /// </summary>
        public DateTime WarningTime { get; set; }

        /// <summary>
        /// 触发监测时间
        /// </summary>
        public DateTime MonitoringTime { get; set; }

        /// <summary>
        /// 触发监测值
        /// </summary>
        public decimal MonitoringValue { get; set; }

        /// <summary>
        /// 预警类型
        /// </summary>
        public int WarningType { get; set; }

        /// <summary>
        /// 预警类型文本
        /// </summary>
        public string WarningTypeText { get; set; } = string.Empty;

        /// <summary>
        /// 预警级别
        /// </summary>
        public int WarningLevel { get; set; }

        /// <summary>
        /// 预警级别文本
        /// </summary>
        public string WarningLevelText { get; set; } = string.Empty;

        /// <summary>
        /// 触发值
        /// </summary>
        public decimal TriggerValue { get; set; }

        /// <summary>
        /// 阈值设定值
        /// </summary>
        public decimal ThresholdValue { get; set; }

        /// <summary>
        /// 前次监测值
        /// </summary>
        public decimal? PreviousValue { get; set; }

        /// <summary>
        /// 变化率
        /// </summary>
        public decimal? ChangeRate { get; set; }

        /// <summary>
        /// 累计变化量
        /// </summary>
        public decimal? CumulativeChange { get; set; }

        /// <summary>
        /// 预警内容
        /// </summary>
        public string WarningContent { get; set; } = string.Empty;

        /// <summary>
        /// 建议措施
        /// </summary>
        public string? SuggestedAction { get; set; }

        /// <summary>
        /// 处理负责人ID
        /// </summary>
        public Guid? HandlerId { get; set; }

        /// <summary>
        /// 处理负责人姓名
        /// </summary>
        public string? HandlerName { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public int HandleStatus { get; set; }

        /// <summary>
        /// 处理状态文本
        /// </summary>
        public string HandleStatusText { get; set; } = string.Empty;

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandleTime { get; set; }

        /// <summary>
        /// 处理方案
        /// </summary>
        public string? HandleSolution { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string? HandleResult { get; set; }

        /// <summary>
        /// 确认人ID
        /// </summary>
        public Guid? ConfirmerId { get; set; }

        /// <summary>
        /// 确认人姓名
        /// </summary>
        public string? ConfirmerName { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 确认备注
        /// </summary>
        public string? ConfirmRemark { get; set; }
    }
}
