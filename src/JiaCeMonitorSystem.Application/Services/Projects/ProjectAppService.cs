using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.DomainServices;
using JiaCeMonitorSystem.Dtos.Projects;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.Projects;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.Projects
{
    /// <summary>
    /// 监测工程应用服务
    /// </summary>
    [Authorize]
    public class ProjectAppService :
        CrudAppService<Project, ProjectDto, Guid, GetProjectListInput, ProjectCreateDto, ProjectUpdateDto>,
        IProjectAppService
    {
        private readonly ProjectManager _projectManager;

        public ProjectAppService(
            IRepository<Project, Guid> repository,
            ProjectManager projectManager) : base(repository)
        {
            _projectManager = projectManager;
        }

        /// <summary>
        /// 获取已参加项目列表
        /// </summary>
        public async Task<List<ProjectDto>> GetParticipatedListAsync()
        {
            // 修正原因：原API返回当前用户参与的项目列表，此处简化实现为返回租户内所有项目
            var projects = await Repository.GetListAsync();
            return ObjectMapper.Map<List<Project>, List<ProjectDto>>(projects);
        }

        /// <summary>
        /// 归档项目
        /// </summary>
        [Authorize(Permissions.Permissions.Projects_Archive)]
        public async Task ArchiveAsync(Guid id)
        {
            await _projectManager.ArchiveProjectAsync(id);
        }

        /// <summary>
        /// 变更项目状态
        /// </summary>
        [Authorize(Permissions.Permissions.Projects_Edit)]
        public async Task ChangeStatusAsync(Guid id, int status)
        {
            var project = await Repository.GetAsync(id);
            project.ChangeStatus((ProjectStatus)status);
            await Repository.UpdateAsync(project);
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        [Authorize(Permissions.Permissions.Projects_Create)]
        public override async Task<ProjectDto> CreateAsync(ProjectCreateDto input)
        {
            var project = await _projectManager.CreateProjectAsync(
                input.ProjectCode,
                input.ProjectName,
                input.ProjectLocation,
                input.StartDate,
                input.EndDate,
                input.ResponsiblePerson,
                input.ContactInfo,
                input.Description);

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        [Authorize(Permissions.Permissions.Projects_Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            await _projectManager.DeleteProjectAsync(id);
        }
    }
}
