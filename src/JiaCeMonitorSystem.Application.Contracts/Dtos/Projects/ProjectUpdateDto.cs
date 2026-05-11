using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Projects
{
    /// <summary>
    /// 更新监测工程输入参数
    /// </summary>
    public class ProjectUpdateDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 项目地点
        /// </summary>
        [StringLength(500)]
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
        [StringLength(100)]
        public string? ResponsiblePerson { get; set; }

        /// <summary>
        /// 负责人联系方式
        /// </summary>
        [StringLength(200)]
        public string? ContactInfo { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
