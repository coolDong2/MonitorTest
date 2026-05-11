using System;
using JiaCeMonitorSystem.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.ProjectPersonnels
{
    /// <summary>
    /// 项目人员安排聚合根
    /// 表名：JC_ProjectPersonnels
    /// </summary>
    public class ProjectPersonnel : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; private set; }

        /// <summary>
        /// 用户ID（关联IdentityUser）
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public RoleType RoleType { get; private set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; private set; }

        /// <summary>
        /// 职责描述
        /// </summary>
        public string? Responsibility { get; private set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string? ContactInfo { get; private set; }

        /// <summary>
        /// 工作状态
        /// </summary>
        public WorkStatus WorkStatus { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; private set; }

        private ProjectPersonnel()
        {
            RoleName = string.Empty;
        }

        /// <summary>
        /// 创建项目人员安排
        /// </summary>
        public ProjectPersonnel(
            Guid id,
            Guid projectId,
            Guid userId,
            RoleType roleType,
            string roleName,
            DateTime startDate,
            DateTime? endDate = null,
            string? responsibility = null,
            string? contactInfo = null,
            WorkStatus workStatus = WorkStatus.Active,
            string? remark = null)
            : base(id)
        {
            ProjectId = projectId;
            UserId = userId;
            RoleType = roleType;
            RoleName = roleName;
            StartDate = startDate;
            EndDate = endDate;
            Responsibility = responsibility;
            ContactInfo = contactInfo;
            WorkStatus = workStatus;
            Remark = remark;
        }

        /// <summary>
        /// 更新人员安排信息
        /// </summary>
        public void UpdateInfo(
            RoleType roleType,
            string roleName,
            DateTime startDate,
            DateTime? endDate,
            string? responsibility,
            string? contactInfo,
            WorkStatus workStatus,
            string? remark = null)
        {
            RoleType = roleType;
            RoleName = roleName;
            StartDate = startDate;
            EndDate = endDate;
            Responsibility = responsibility;
            ContactInfo = contactInfo;
            WorkStatus = workStatus;
            Remark = remark;
        }

        /// <summary>
        /// 更新工作状态
        /// </summary>
        public void UpdateWorkStatus(WorkStatus workStatus)
        {
            WorkStatus = workStatus;
        }
    }
}
