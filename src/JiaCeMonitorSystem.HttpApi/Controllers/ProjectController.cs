using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Projects;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 监测工程控制器
    /// </summary>
    [Route("api/app/project")]
    public class ProjectController : JiaCeMonitorSystemController
    {
        private readonly IProjectAppService _projectAppService;

        /// <summary>
        /// 初始化工程控制器
        /// </summary>
        public ProjectController(IProjectAppService projectAppService)
        {
            _projectAppService = projectAppService;
        }

        /// <summary>
        /// 获取工程列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<ProjectDto>> GetListAsync([FromQuery] GetProjectListInput input)
        {
            return _projectAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个工程
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<ProjectDto> GetAsync(Guid id)
        {
            return _projectAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建工程
        /// </summary>
        [HttpPost]
        public virtual Task<ProjectDto> CreateAsync([FromBody] ProjectCreateDto input)
        {
            return _projectAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新工程
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<ProjectDto> UpdateAsync(Guid id, [FromBody] ProjectUpdateDto input)
        {
            return _projectAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除工程
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _projectAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取已参加项目列表
        /// </summary>
        [HttpGet("participated-list")]
        public virtual Task<List<ProjectDto>> GetParticipatedListAsync()
        {
            return _projectAppService.GetParticipatedListAsync();
        }

        /// <summary>
        /// 归档项目
        /// </summary>
        [HttpPost("{id}/archive")]
        public virtual Task ArchiveAsync(Guid id)
        {
            return _projectAppService.ArchiveAsync(id);
        }

        /// <summary>
        /// 变更项目状态
        /// </summary>
        [HttpPost("{id}/change-status")]
        public virtual Task ChangeStatusAsync(Guid id, [FromQuery] int status)
        {
            return _projectAppService.ChangeStatusAsync(id, status);
        }
    }
}
