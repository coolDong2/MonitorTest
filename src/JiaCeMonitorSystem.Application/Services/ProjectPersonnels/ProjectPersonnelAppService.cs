using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.DomainServices;
using JiaCeMonitorSystem.Dtos.ProjectPersonnels;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.ProjectPersonnels;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.ProjectPersonnels
{
    /// <summary>
    /// 项目人员安排应用服务
    /// </summary>
    [Authorize]
    public class ProjectPersonnelAppService :
        CrudAppService<ProjectPersonnel, ProjectPersonnelDto, Guid, GetProjectPersonnelListInput, ProjectPersonnelCreateDto, ProjectPersonnelUpdateDto>,
        IProjectPersonnelAppService
    {
        private readonly ProjectPersonnelManager _projectPersonnelManager;

        public ProjectPersonnelAppService(
            IRepository<ProjectPersonnel, Guid> repository,
            ProjectPersonnelManager projectPersonnelManager) : base(repository)
        {
            _projectPersonnelManager = projectPersonnelManager;
        }

        /// <summary>
        /// 获取分页列表（按项目筛选）
        /// </summary>
        public async Task<PagedResultDto<ProjectPersonnelDto>> GetPageListAsync(GetProjectPersonnelListInput input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(p => p.ProjectId == input.ProjectId);

            if (input.RoleType.HasValue)
                query = query.Where(p => (int)p.RoleType == input.RoleType.Value);

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(p => p.RoleName.Contains(input.Filter) || p.UserId.ToString().Contains(input.Filter));
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            var personnels = await AsyncExecuter.ToListAsync(query.OrderBy(p => p.StartDate).PageBy(input));
            var personnelDtos = ObjectMapper.Map<List<ProjectPersonnel>, List<ProjectPersonnelDto>>(personnels);
            FillComputedFields(personnelDtos);

            return new PagedResultDto<ProjectPersonnelDto>(totalCount, personnelDtos);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<ProjectPersonnelDto> GetModelAsync(Guid id)
        {
            var personnel = await Repository.GetAsync(id);
            var dto = ObjectMapper.Map<ProjectPersonnel, ProjectPersonnelDto>(personnel);
            FillComputedFields(new List<ProjectPersonnelDto> { dto });
            return dto;
        }

        /// <summary>
        /// 按项目获取列表（非分页）
        /// </summary>
        public async Task<List<ProjectPersonnelDto>> GetListByProjectAsync(Guid projectId)
        {
            var personnels = await Repository.GetListAsync(p => p.ProjectId == projectId);
            var dtos = ObjectMapper.Map<List<ProjectPersonnel>, List<ProjectPersonnelDto>>(personnels);
            FillComputedFields(dtos);
            return dtos;
        }

        /// <summary>
        /// 获取项目人员列表（非分页，支持角色筛选）
        /// </summary>
        public async Task<List<ProjectPersonnelDto>> GetListAsync(Guid projectId, int? roleType)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(p => p.ProjectId == projectId);
            if (roleType.HasValue)
                query = query.Where(p => (int)p.RoleType == roleType.Value);

            var personnels = await AsyncExecuter.ToListAsync(query.OrderBy(p => p.StartDate));
            var dtos = ObjectMapper.Map<List<ProjectPersonnel>, List<ProjectPersonnelDto>>(personnels);
            FillComputedFields(dtos);
            return dtos;
        }

        /// <summary>
        /// 创建项目人员安排（校验时段冲突）
        /// </summary>
        [Authorize(Permissions.Permissions.ProjectPersonnels_Create)]
        public override async Task<ProjectPersonnelDto> CreateAsync(ProjectPersonnelCreateDto input)
        {
            await _projectPersonnelManager.ValidateTimeConflictAsync(
                input.ProjectId, input.UserId, input.StartDate, input.EndDate);

            var dto = await base.CreateAsync(input);
            dto.UserName = input.UserName;
            return dto;
        }

        /// <summary>
        /// 填充计算字段
        /// </summary>
        private void FillComputedFields(List<ProjectPersonnelDto> dtos)
        {
            var today = DateTime.UtcNow.Date;
            foreach (var dto in dtos)
            {
                dto.RemainingDays = dto.EndDate.HasValue
                    ? Math.Max(0, (dto.EndDate.Value.Date - today).Days)
                    : 0;
                dto.IsExpired = dto.EndDate.HasValue && dto.EndDate.Value.Date < today;
                dto.StatusDescription = dto.WorkStatus switch
                {
                    (int)WorkStatus.Active => dto.IsExpired ? "已过期" : "在职",
                    (int)WorkStatus.OnLeave => "休假",
                    (int)WorkStatus.Transferred => "调离",
                    (int)WorkStatus.Ended => "已结束",
                    _ => "未知"
                };
            }
        }

        /// <summary>
        /// 更新项目人员安排（校验时段冲突）
        /// </summary>
        [Authorize(Permissions.Permissions.ProjectPersonnels_Edit)]
        public override async Task<ProjectPersonnelDto> UpdateAsync(Guid id, ProjectPersonnelUpdateDto input)
        {
            var existing = await Repository.GetAsync(id);
            await _projectPersonnelManager.ValidateTimeConflictAsync(
                existing.ProjectId, existing.UserId, input.StartDate, input.EndDate, id);

            return await base.UpdateAsync(id, input);
        }
    }
}
