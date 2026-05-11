using System;
using JiaCeMonitorSystem.Enums;
namespace JiaCeMonitorSystem.Events
{
    /// <summary>
    /// 预警触发领域事件，当测点监测值超过阈值时发布
    /// </summary>
    public class WarningTriggeredDomainEvent
    {
        /// <summary>
        /// 预警记录ID
        /// </summary>
        public Guid WarningRecordId { get; set; }

        /// <summary>
        /// 测点ID
        /// </summary>
        public Guid PointId { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 监测属性ID
        /// </summary>
        public Guid PropertyId { get; set; }

        /// <summary>
        /// 监测属性名称
        /// </summary>
        public string PropertyName { get; set; } = string.Empty;

        /// <summary>
        /// 预警级别
        /// </summary>
        public WarningLevel WarningLevel { get; set; }

        /// <summary>
        /// 预警类型
        /// </summary>
        public WarningType WarningType { get; set; }

        /// <summary>
        /// 触发监测值
        /// </summary>
        public decimal MonitoringValue { get; set; }

        /// <summary>
        /// 触发时间
        /// </summary>
        public DateTime TriggerTime { get; set; }

        /// <summary>
        /// 阈值设定值
        /// </summary>
        public decimal ThresholdValue { get; set; }
    }
}
