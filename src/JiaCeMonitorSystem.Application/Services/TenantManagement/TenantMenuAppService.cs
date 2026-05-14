using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.AppRoles;
using JiaCeMonitorSystem.Dtos.TenantManagement;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.SystemModules;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Services.TenantManagement
{
    /// <summary>
    /// 租户菜单应用服务
    /// </summary>
    [Authorize]
    public class TenantMenuAppService : ApplicationService, ITenantMenuAppService
    {
        private readonly IRepository<TenantModuleGrant, Guid> _moduleGrantRepository;
        private readonly IRepository<SystemModule, Guid> _systemModuleRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IPermissionAppService _permissionAppService;

        public TenantMenuAppService(
            IRepository<TenantModuleGrant, Guid> moduleGrantRepository,
            IRepository<SystemModule, Guid> systemModuleRepository,
            IIdentityRoleRepository identityRoleRepository,
            IPermissionAppService permissionAppService)
        {
            _moduleGrantRepository = moduleGrantRepository;
            _systemModuleRepository = systemModuleRepository;
            _identityRoleRepository = identityRoleRepository;
            _permissionAppService = permissionAppService;
        }

        /// <summary>
        /// 获取当前租户可用菜单树
        /// </summary>
        public async Task<List<TenantMenuDto>> GetCurrentTenantMenusAsync()
        {
            if (!CurrentTenant.IsAvailable)
            {
                // Host 端返回全部菜单
                var allModules = await _systemModuleRepository.GetListAsync(m => m.EnabledMark && m.IsMenu);
                return BuildMenuTree(allModules);
            }

            var tenantId = CurrentTenant.Id.Value;

            // 1. 查询租户被授予的模块
            var grants = await _moduleGrantRepository.GetListAsync(
                g => g.TenantId == tenantId && g.IsGranted);
            var grantedModuleIds = grants.Select(g => g.ModuleId).ToHashSet();

            // 2. 查询启用的菜单模块
            var modules = await _systemModuleRepository.GetListAsync(
                m => m.EnabledMark && m.IsMenu && grantedModuleIds.Contains(m.Id));

            // 3. 如果模块有父节点但父节点不在授权列表中，需要补充父节点以保持树结构
            var resultModuleIds = modules.Select(m => m.Id).ToHashSet();
            var missingParentIds = modules
                .Where(m => m.ParentId.HasValue && !resultModuleIds.Contains(m.ParentId.Value))
                .Select(m => m.ParentId!.Value)
                .Distinct()
                .ToList();

            while (missingParentIds.Any())
            {
                var parents = await _systemModuleRepository.GetListAsync(
                    m => missingParentIds.Contains(m.Id));
                modules.AddRange(parents);
                resultModuleIds = modules.Select(m => m.Id).ToHashSet();
                missingParentIds = modules
                    .Where(m => m.ParentId.HasValue && !resultModuleIds.Contains(m.ParentId.Value))
                    .Select(m => m.ParentId!.Value)
                    .Distinct()
                    .ToList();
            }

            return BuildMenuTree(modules);
        }

        /// <summary>
        /// 获取角色的模块权限树（替代原 AppRole 权限树）
        /// </summary>
        public async Task<List<PermissionTreeDto>> GetRolePermissionTreeAsync(Guid roleId)
        {
            var role = await _identityRoleRepository.GetAsync(roleId);
            var modules = await _systemModuleRepository.GetListAsync(m => m.EnabledMark);

            // 查询角色已授权的权限（通过 ABP PermissionGrant）
            var permissionTree = await _permissionAppService.GetPermissionTreeAsync("Role", roleId.ToString());
            var grantedNames = permissionTree.Children
                .SelectMany(g => g.Children)
                .Where(p => p.IsGranted)
                .Select(p => p.Name)
                .ToHashSet();

            // 将 ABP 权限名称映射到 SystemModule 的编码（简化处理：使用模块编码匹配权限前缀）
            var allowedModuleIds = modules
                .Where(m => grantedNames.Any(g => g.Contains(m.EnCode, StringComparison.OrdinalIgnoreCase)))
                .Select(m => m.Id.ToString())
                .ToHashSet();

            return BuildPermissionTree(modules, allowedModuleIds);
        }

        /// <summary>
        /// 获取角色的字段权限树（替代原 AppRole 字段权限树）
        /// </summary>
        public async Task<List<PermissionTreeDto>> GetRolePermissionFieldsTreeAsync(Guid roleId, string? moduleIds)
        {
            var role = await _identityRoleRepository.GetAsync(roleId);
            var targetModuleIds = new HashSet<string>(
                (moduleIds ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries),
                StringComparer.OrdinalIgnoreCase);

            var modules = await _systemModuleRepository.GetListAsync(
                m => m.EnabledMark && targetModuleIds.Contains(m.Id.ToString()));

            // 从 ABP PermissionGrant 获取角色已授权的权限名称
            var permissionTree = await _permissionAppService.GetPermissionTreeAsync("Role", roleId.ToString());
            var grantedNames = permissionTree.Children
                .SelectMany(g => g.Children)
                .Where(p => p.IsGranted)
                .Select(p => p.Name)
                .ToHashSet();

            // 将 ABP 权限名称映射到 SystemModule 的编码（与模块权限使用相同的匹配逻辑）
            var allowedModuleIds = modules
                .Where(m => grantedNames.Any(g => g.Contains(m.EnCode, StringComparison.OrdinalIgnoreCase)))
                .Select(m => m.Id.ToString())
                .ToHashSet();

            return BuildPermissionTree(modules, allowedModuleIds);
        }

        private static List<TenantMenuDto> BuildMenuTree(List<SystemModule> modules)
        {
            var allDtos = modules.Select(m => new TenantMenuDto
            {
                Id = m.Id,
                ParentId = m.ParentId,
                EnCode = m.EnCode,
                FullName = m.FullName,
                Icon = m.Icon,
                UrlAddress = m.UrlAddress,
                Target = m.Target,
                IsMenu = m.IsMenu,
                SortCode = m.SortCode
            }).ToList();

            var lookup = allDtos.ToLookup(d => d.ParentId);
            var roots = allDtos
                .Where(d => d.ParentId == null || !allDtos.Any(x => x.Id == d.ParentId))
                .OrderBy(r => r.SortCode)
                .ToList();

            foreach (var root in roots)
            {
                FillMenuChildren(root, lookup);
            }

            return roots;
        }

        private static void FillMenuChildren(TenantMenuDto parent, ILookup<Guid?, TenantMenuDto> lookup)
        {
            parent.Children = lookup[parent.Id].OrderBy(c => c.SortCode).ToList();
            foreach (var child in parent.Children)
            {
                FillMenuChildren(child, lookup);
            }
        }

        private static List<PermissionTreeDto> BuildPermissionTree(List<SystemModule> modules, HashSet<string> allowedIds)
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
            var roots = allDtos
                .Where(d => d.ParentId == null || !allDtos.Any(x => x.Id == d.ParentId))
                .OrderBy(r => r.SortCode)
                .ToList();

            foreach (var root in roots)
            {
                FillPermissionChildren(root, lookup);
            }

            return roots;
        }

        private static void FillPermissionChildren(PermissionTreeDto parent, ILookup<Guid?, PermissionTreeDto> lookup)
        {
            parent.Children = lookup[parent.Id].OrderBy(c => c.SortCode).ToList();
            foreach (var child in parent.Children)
            {
                FillPermissionChildren(child, lookup);
            }
        }
    }
}
