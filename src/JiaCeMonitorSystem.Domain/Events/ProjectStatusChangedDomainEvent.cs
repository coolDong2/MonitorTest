using System;
using JiaCeMonitorSystem.Enums;
namespace JiaCeMonitorSystem.Events
{
    /// <summary>
    /// 项目状态变更领域事件，监测工程状态流转时发布
    /// </summary>
    public class ProjectStatusChangedDomainEvent
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 变更前状态
        /// </summary>
        public ProjectStatus OldStatus { get; set; }

        /// <summary>
        /// 变更后状态
        /// </summary>
        public ProjectStatus NewStatus { get; set; }
    }
}
