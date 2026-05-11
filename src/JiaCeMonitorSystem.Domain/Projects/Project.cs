using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JiaCeMonitorSystem.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.Projects
{
    /// <summary>
    /// 监测工程聚合根，管理项目生命周期与测点集合
    /// </summary>
    public class Project : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 项目编号（业务唯一标识）
        /// </summary>
        public string ProjectCode { get; private set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// 项目地点
        /// </summary>
        public string? ProjectLocation { get; private set; }

        /// <summary>
        /// 项目开始日期
        /// </summary>
        public DateTime? StartDate { get; private set; }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// 项目负责人
        /// </summary>
        public string? ResponsiblePerson { get; private set; }

        /// <summary>
        /// 负责人联系方式
        /// </summary>
        public string? ContactInfo { get; private set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public ProjectStatus Status { get; private set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// 项目测点集合（导航属性）
        /// </summary>
        public virtual ICollection<Points.Point> Points { get; private set; }

        private Project()
        {
            ProjectCode = string.Empty;
            ProjectName = string.Empty;
            Points = new Collection<Points.Point>();
        }

        /// <summary>
        /// 创建监测工程项目
        /// </summary>
        public Project(
            Guid id,
            string projectCode,
            string projectName,
            string? projectLocation = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? responsiblePerson = null,
            string? contactInfo = null,
            string? description = null)
            : base(id)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            ProjectLocation = projectLocation;
            StartDate = startDate;
            EndDate = endDate;
            ResponsiblePerson = responsiblePerson;
            ContactInfo = contactInfo;
            Status = ProjectStatus.Preparing;
            Description = description;
            Points = new Collection<Points.Point>();

            AddLocalEvent(new Events.ProjectCreatedDomainEvent
            {
                ProjectId = id,
                ProjectCode = projectCode,
                ProjectName = projectName
            });
        }

        /// <summary>
        /// 变更项目信息
        /// </summary>
        public void UpdateInfo(
            string projectName,
            string? projectLocation = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? responsiblePerson = null,
            string? contactInfo = null,
            string? description = null)
        {
            CheckNotArchived();

            ProjectName = projectName;
            ProjectLocation = projectLocation;
            StartDate = startDate;
            EndDate = endDate;
            ResponsiblePerson = responsiblePerson;
            ContactInfo = contactInfo;
            Description = description;
        }

        /// <summary>
        /// 变更项目状态，包含状态机校验
        /// </summary>
        public void ChangeStatus(ProjectStatus newStatus)
        {
            CheckNotArchived();

            if (!IsValidStatusTransition(Status, newStatus))
            {
                throw new BusinessException(ErrorCodes.Project_InvalidStatusTransition)
                    .WithData("CurrentStatus", Status)
                    .WithData("NewStatus", newStatus);
            }

            Status = newStatus;

            AddLocalEvent(new Events.ProjectStatusChangedDomainEvent
            {
                ProjectId = Id,
                OldStatus = Status,
                NewStatus = newStatus
            });
        }

        /// <summary>
        /// 归档项目
        /// </summary>
        public void Archive()
        {
            if (Status == ProjectStatus.Archived)
            {
                throw new BusinessException(ErrorCodes.Project_ArchivedCannotModify);
            }

            Status = ProjectStatus.Archived;

            AddLocalEvent(new Events.ProjectArchivedDomainEvent
            {
                ProjectId = Id
            });
        }

        /// <summary>
        /// 向项目添加测点
        /// </summary>
        public void AddPoint(Points.Point point)
        {
            CheckNotArchived();
            Points.Add(point);
        }

        /// <summary>
        /// 移除测点
        /// </summary>
        public void RemovePoint(Points.Point point)
        {
            CheckNotArchived();
            Points.Remove(point);
        }

        private void CheckNotArchived()
        {
            if (Status == ProjectStatus.Archived)
            {
                throw new BusinessException(ErrorCodes.Project_ArchivedCannotModify);
            }
        }

        private static bool IsValidStatusTransition(ProjectStatus current, ProjectStatus next)
        {
            return (current, next) switch
            {
                (ProjectStatus.Preparing, ProjectStatus.InProgress) => true,
                (ProjectStatus.Preparing, ProjectStatus.Paused) => true,
                (ProjectStatus.InProgress, ProjectStatus.Completed) => true,
                (ProjectStatus.InProgress, ProjectStatus.Paused) => true,
                (ProjectStatus.Completed, ProjectStatus.Archived) => true,
                (ProjectStatus.Paused, ProjectStatus.InProgress) => true,
                (ProjectStatus.Paused, ProjectStatus.Preparing) => true,
                _ => false
            };
        }
    }
}
