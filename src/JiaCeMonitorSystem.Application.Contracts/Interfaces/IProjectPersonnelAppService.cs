using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ProjectPersonnels;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 项目人员安排应用服务接口
    /// </summary>
    public interface IProjectPersonnelAppService : IApplicationService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<ProjectPersonnelDto>> GetPageListAsync(GetProjectPersonnelListInput input);

        /// <summary>
        /// 按项目获取列表（非分页）
        /// </summary>
        Task<List<ProjectPersonnelDto>> GetListByProjectAsync(Guid projectId);

        /// <summary>
        /// 获取项目人员列表（非分页，支持角色筛选）
        /// </summary>
        Task<List<ProjectPersonnelDto>> GetListAsync(Guid projectId, int? roleType);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<ProjectPersonnelDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        Task<ProjectPersonnelDto> CreateAsync(ProjectPersonnelCreateDto input);

        /// <summary>
        /// 更新
        /// </summary>
        Task<ProjectPersonnelDto> UpdateAsync(Guid id, ProjectPersonnelUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
