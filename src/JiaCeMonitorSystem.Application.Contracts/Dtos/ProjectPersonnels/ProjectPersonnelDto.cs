using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.ProjectPersonnels
{
    /// <summary>
    /// 项目人员安排数据传输对象
    /// </summary>
    public class ProjectPersonnelDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 项目名称（冗余）
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户姓名（冗余）
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 角色类型
        /// </summary>
        public int RoleType { get; set; }

        /// <summary>
        /// 角色类型文本
        /// </summary>
        public string RoleTypeText { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 职责描述
        /// </summary>
        public string? Responsibility { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string? ContactInfo { get; set; }

        /// <summary>
        /// 工作状态
        /// </summary>
        public int WorkStatus { get; set; }

        /// <summary>
        /// 工作状态文本
        /// </summary>
        public string WorkStatusText { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 是否在职且未过期（计算属性）
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 服务天数（计算属性）
        /// </summary>
        public int ServiceDays { get; set; }

        /// <summary>
        /// 是否已结束（计算属性）
        /// </summary>
        public bool IsEnded { get; set; }

        /// <summary>
        /// 剩余天数（如果设置了结束时间）
        /// </summary>
        public int RemainingDays { get; set; }

        /// <summary>
        /// 是否已过期
        /// </summary>
        public bool IsExpired { get; set; }

        /// <summary>
        /// 当前状态描述
        /// </summary>
        public string StatusDescription { get; set; } = string.Empty;
    }
}
