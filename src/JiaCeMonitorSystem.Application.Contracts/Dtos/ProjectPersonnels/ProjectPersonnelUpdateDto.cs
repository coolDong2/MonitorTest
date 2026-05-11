using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.ProjectPersonnels
{
    /// <summary>
    /// 更新项目人员安排输入参数
    /// </summary>
    public class ProjectPersonnelUpdateDto
    {
        /// <summary>
        /// 角色类型
        /// </summary>
        public int RoleType { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 职责描述
        /// </summary>
        [StringLength(500)]
        public string? Responsibility { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [StringLength(200)]
        public string? ContactInfo { get; set; }

        /// <summary>
        /// 工作状态
        /// </summary>
        public int WorkStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        public string? Remark { get; set; }
    }
}
