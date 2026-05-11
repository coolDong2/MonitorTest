using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.Projects;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace JiaCeMonitorSystem.DomainServices
{
    /// <summary>
    /// 项目管理领域服务，负责项目状态机校验与跨实体业务逻辑
    /// </summary>
    public class ProjectManager : DomainService
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Points.Point, Guid> _pointRepository;

        /// <summary>
        /// 初始化项目管理领域服务
        /// </summary>
        public ProjectManager(
            IRepository<Project, Guid> projectRepository,
            IRepository<Points.Point, Guid> pointRepository)
        {
            _projectRepository = projectRepository;
            _pointRepository = pointRepository;
        }

        /// <summary>
        /// 创建监测工程
        /// </summary>
        public async Task<Project> CreateProjectAsync(
            string projectCode,
            string projectName,
            string? projectLocation = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? responsiblePerson = null,
            string? contactInfo = null,
            string? description = null)
        {
            // 校验项目编号唯一性
            if (await _projectRepository.AnyAsync(p => p.ProjectCode == projectCode))
            {
                throw new BusinessException(ErrorCodes.Project_DuplicateCode)
                    .WithData("ProjectCode", projectCode);
            }

            var project = new Project(
                GuidGenerator.Create(),
                projectCode,
                projectName,
                projectLocation,
                startDate,
                endDate,
                responsiblePerson,
                contactInfo,
                description);

            await _projectRepository.InsertAsync(project);
            return project;
        }

        /// <summary>
        /// 删除项目（仅筹备中且下无任何测点时允许）
        /// </summary>
        public async Task DeleteProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);

            if (project.Status != ProjectStatus.Preparing)
            {
                throw new BusinessException(ErrorCodes.Project_InvalidStatusTransition)
                    .WithData("CurrentStatus", project.Status)
                    .WithData("Reason", "仅筹备中的项目可以删除");
            }

            var hasPoints = await _pointRepository.AnyAsync(p => p.ProjectId == projectId);
            if (hasPoints)
            {
                throw new BusinessException(ErrorCodes.Project_HasPointsCannotDelete);
            }

            await _projectRepository.DeleteAsync(project);
        }

        /// <summary>
        /// 归档项目
        /// </summary>
        public async Task ArchiveProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            project.Archive();
            await _projectRepository.UpdateAsync(project);
        }

        /// <summary>
        /// 校验项目是否允许添加测点
        /// </summary>
        public async Task<bool> CanAddPointAsync(Guid projectId)
        {
            var project = await _projectRepository.GetAsync(projectId);
            return project.Status == ProjectStatus.Preparing
                || project.Status == ProjectStatus.InProgress;
        }
    }
}
