using System;
using JiaCeMonitorSystem.Enums;
namespace JiaCeMonitorSystem.Events
{
    /// <summary>
    /// 设备状态变更领域事件，当设备状态发生流转时发布
    /// </summary>
    public class DeviceStatusChangedDomainEvent
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        /// 变更前状态
        /// </summary>
        public DeviceStatus OldStatus { get; set; }

        /// <summary>
        /// 变更后状态
        /// </summary>
        public DeviceStatus NewStatus { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string? Reason { get; set; }
    }
}
