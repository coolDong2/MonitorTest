using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.AppRoles;
using JiaCeMonitorSystem.Dtos.AppRoles;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.SystemModules;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.AppRoles
{
    /// <summary>
    /// 业务角色应用服务
    /// </summary>
    [Authorize]
    public class AppRoleAppService : ApplicationService, IAppRoleAppService
    {
        private readonly IRepository<AppRole, Guid> _appRoleRepository;
        private readonly IRepository<SystemModule, Guid> _systemModuleRepository;

        public AppRoleAppService(
            IRepository<AppRole, Guid> appRoleRepository,
            IRepository<SystemModule, Guid> systemModuleRepository)
        {
            _appRoleRepository = appRoleRepository;
            _systemModuleRepository = systemModuleRepository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        public async Task<PagedResultDto<AppRoleDto>> GetPageListAsync(GetAppRoleListInput input)
        {
            var query = await BuildRoleQueryAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);
            var roles = await AsyncExecuter.ToListAsync(query.OrderBy(r => r.SortCode).ThenByDescending(r => r.CreationTime).PageBy(input));
            return new PagedResultDto<AppRoleDto>(totalCount, ObjectMapper.Map<List<AppRole>, List<AppRoleDto>>(roles));
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public async Task<List<AppRoleDto>> GetListAsync(string? keyword)
        {
            var query = await _appRoleRepository.GetQueryableAsync();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(r => r.FullName.Contains(keyword) || r.EnCode.Contains(keyword));
            }
            var roles = await AsyncExecuter.ToListAsync(query.OrderBy(r => r.SortCode));
            return ObjectMapper.Map<List<AppRole>, List<AppRoleDto>>(roles);
        }

        /// <summary>
        /// 获取单个角色
        /// </summary>
        public async Task<AppRoleDto> GetModelAsync(Guid id)
        {
            var role = await _appRoleRepository.GetAsync(id);
            return ObjectMapper.Map<AppRole, AppRoleDto>(role);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        [Authorize(Permissions.Permissions.Roles_Create)]
        public async Task<AppRoleDto> AddAndEditRoleAsync(AppRoleCreateDto input)
        {
            var role = new AppRole(
                GuidGenerator.Create(),
                input.EnCode,
                input.FullName,
                input.Category,
                input.SortCode,
                input.EnabledMark,
                input.CompanyId,
                input.CompanyName,
                input.Type,
                input.AllowEdit,
                input.AllowDelete,
                input.Description,
                input.PermissionButtonIds,
                input.PermissionFieldsIds);

            await _appRoleRepository.InsertAsync(role);
            return ObjectMapper.Map<AppRole, AppRoleDto>(role);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        [Authorize(Permissions.Permissions.Roles_Edit)]
        public async Task<AppRoleDto> UpdateAsync(Guid id, AppRoleUpdateDto input)
        {
            var role = await _appRoleRepository.GetAsync(id);
            if (!role.AllowEdit)
            {
                throw new BusinessException("Role.NotAllowEdit").WithData("RoleName", role.FullName);
            }

            role.UpdateInfo(
                input.EnCode,
                input.FullName,
                input.Category,
                input.SortCode,
                input.EnabledMark,
                input.CompanyId,
                input.CompanyName,
                input.Type,
                input.AllowEdit,
                input.AllowDelete,
                input.Description,
                input.PermissionButtonIds,
                input.PermissionFieldsIds);

            await _appRoleRepository.UpdateAsync(role);
            return ObjectMapper.Map<AppRole, AppRoleDto>(role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [Authorize(Permissions.Permissions.Roles_Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var role = await _appRoleRepository.GetAsync(id);
            if (!role.AllowDelete)
            {
                throw new BusinessException("Role.NotAllowDelete").WithData("RoleName", role.FullName);
            }

            await _appRoleRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        public async Task<List<PermissionTreeDto>> GetPermissionTreeAsync(Guid roleId)
        {
            var role = await _appRoleRepository.GetAsync(roleId);
            var allowedModuleIds = new HashSet<string>(
                (role.PermissionButtonIds ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries),
                StringComparer.OrdinalIgnoreCase);

            var modules = await _systemModuleRepository.GetListAsync(m => m.EnabledMark);
            return BuildTree(modules, allowedModuleIds);
        }

        /// <summary>
        /// 获取权限字段树
        /// </summary>
        public async Task<List<PermissionTreeDto>> GetPermissionFieldsTreeAsync(Guid roleId, string? moduleIds)
        {
            var role = await _appRoleRepository.GetAsync(roleId);
            var allowedFieldIds = new HashSet<string>(
                (role.PermissionFieldsIds ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries),
                StringComparer.OrdinalIgnoreCase);

            var targetModuleIds = new HashSet<string>(
                (moduleIds ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries),
                StringComparer.OrdinalIgnoreCase);

            var modules = await _systemModuleRepository.GetListAsync(
                m => m.EnabledMark && targetModuleIds.Contains(m.Id.ToString()));

            return BuildTree(modules, allowedFieldIds);
        }

        private async Task<IQueryable<AppRole>> BuildRoleQueryAsync(GetAppRoleListInput input)
        {
            var query = await _appRoleRepository.GetQueryableAsync();
            if (input.CompanyId.HasValue)
                query = query.Where(r => r.CompanyId == input.CompanyId.Value);
            if (input.Category.HasValue)
                query = query.Where(r => r.Category == input.Category.Value);
            if (input.EnabledMark.HasValue)
                query = query.Where(r => r.EnabledMark == input.EnabledMark.Value);
            if (!string.IsNullOrWhiteSpace(input.Keyword))
                query = query.Where(r => r.FullName.Contains(input.Keyword) || r.EnCode.Contains(input.Keyword));
            return query;
        }

        private static List<PermissionTreeDto> BuildTree(List<SystemModule> modules, HashSet<string> allowedIds)
        {
            var allDtos = modules.Select(m => new PermissionTreeDto
            {
                Id = m.Id,
                ParentId = m.ParentId,
                EnCode = m.EnCode,
                FullName = m.FullName,
                Icon = m.Icon,
                UrlAddress = m.UrlAddress,
                IsMenu = m.IsMenu,
                SortCode = m.SortCode,
                Checked = allowedIds.Contains(m.Id.ToString())
            }).ToList();

            var lookup = allDtos.ToLookup(d => d.ParentId);
            var roots = allDtos.Where(d => d.ParentId == null || !allDtos.Any(x => x.Id == d.ParentId)).ToList();

            foreach (var root in roots)
            {
                FillChildren(root, lookup);
            }

            return roots.OrderBy(r => r.SortCode).ToList();
        }

        private static void FillChildren(PermissionTreeDto parent, ILookup<Guid?, PermissionTreeDto> lookup)
        {
            parent.Children = lookup[parent.Id].OrderBy(c => c.SortCode).ToList();
            foreach (var child in parent.Children)
            {
                FillChildren(child, lookup);
            }
        }
    }
}
