using System;
namespace JiaCeMonitorSystem.Events
{
    /// <summary>
    /// 项目创建领域事件，新监测工程创建后发布
    /// </summary>
    public class ProjectCreatedDomainEvent
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; } = string.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;
    }
}
