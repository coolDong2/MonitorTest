using System;
namespace JiaCeMonitorSystem.Events
{
    /// <summary>
    /// 项目归档领域事件，监测工程归档时发布
    /// </summary>
    public class ProjectArchivedDomainEvent
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }
    }
}
