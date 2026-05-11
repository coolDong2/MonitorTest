using System;
namespace JiaCeMonitorSystem.Events
{
    /// <summary>
    /// 监测数据采集领域事件，新监测数据入库后发布
    /// </summary>
    public class MonitoringDataCollectedDomainEvent
    {
        /// <summary>
        /// 监测数据ID
        /// </summary>
        public Guid MonitoringDataId { get; set; }

        /// <summary>
        /// 测点ID
        /// </summary>
        public Guid PointId { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 监测时间
        /// </summary>
        public DateTime MonitoringTime { get; set; }

        /// <summary>
        /// 监测值
        /// </summary>
        public decimal MonitoringValue { get; set; }
    }
}
