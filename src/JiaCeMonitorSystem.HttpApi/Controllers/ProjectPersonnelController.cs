using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ProjectPersonnels;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 项目人员安排控制器
    /// </summary>
    [Route("api/app/project-personnel")]
    public class ProjectPersonnelController : JiaCeMonitorSystemController
    {
        private readonly IProjectPersonnelAppService _projectPersonnelAppService;

        /// <summary>
        /// 初始化项目人员安排控制器
        /// </summary>
        public ProjectPersonnelController(IProjectPersonnelAppService projectPersonnelAppService)
        {
            _projectPersonnelAppService = projectPersonnelAppService;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<ProjectPersonnelDto>> GetPageListAsync([FromQuery] GetProjectPersonnelListInput input)
        {
            return _projectPersonnelAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 按项目获取列表
        /// </summary>
        [HttpGet("by-project/{projectId}")]
        public virtual Task<List<ProjectPersonnelDto>> GetListByProjectAsync(Guid projectId)
        {
            return _projectPersonnelAppService.GetListByProjectAsync(projectId);
        }

        /// <summary>
        /// 获取项目人员列表（非分页，支持角色筛选）
        /// </summary>
        [HttpGet("list")]
        public virtual Task<List<ProjectPersonnelDto>> GetListAsync(
            [FromQuery] Guid projectId,
            [FromQuery] int? roleType)
        {
            return _projectPersonnelAppService.GetListAsync(projectId, roleType);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<ProjectPersonnelDto> GetModelAsync(Guid id)
        {
            return _projectPersonnelAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        [HttpPost]
        public virtual Task<ProjectPersonnelDto> CreateAsync([FromBody] ProjectPersonnelCreateDto input)
        {
            return _projectPersonnelAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<ProjectPersonnelDto> UpdateAsync(Guid id, [FromBody] ProjectPersonnelUpdateDto input)
        {
            return _projectPersonnelAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _projectPersonnelAppService.DeleteAsync(id);
        }
    }
}
