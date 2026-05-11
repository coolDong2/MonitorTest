using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Projects
{
    /// <summary>
    /// 监测工程数据传输对象
    /// </summary>
    public class ProjectDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; } = string.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 项目地点
        /// </summary>
        public string? ProjectLocation { get; set; }

        /// <summary>
        /// 项目开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 项目负责人
        /// </summary>
        public string? ResponsiblePerson { get; set; }

        /// <summary>
        /// 负责人联系方式
        /// </summary>
        public string? ContactInfo { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 项目状态文本
        /// </summary>
        public string StatusText { get; set; } = string.Empty;

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 测点数量
        /// </summary>
        public int PointCount { get; set; }
    }
}
