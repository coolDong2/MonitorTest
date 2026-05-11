using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Projects;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 监测工程应用服务接口
    /// </summary>
    public interface IProjectAppService :
        ICrudAppService<ProjectDto, Guid, GetProjectListInput, ProjectCreateDto, ProjectUpdateDto>
    {
        /// <summary>
        /// 获取已参加项目列表
        /// </summary>
        Task<List<ProjectDto>> GetParticipatedListAsync();

        /// <summary>
        /// 归档项目
        /// </summary>
        Task ArchiveAsync(Guid id);

        /// <summary>
        /// 变更项目状态
        /// </summary>
        Task ChangeStatusAsync(Guid id, int status);
    }
}
