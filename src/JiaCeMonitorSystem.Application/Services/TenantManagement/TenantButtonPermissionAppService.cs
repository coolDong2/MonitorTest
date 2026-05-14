using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.Dtos.ModuleButtons;
using JiaCeMonitorSystem.ModuleButtons;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using IdentityRole = Volo.Abp.Identity.IdentityRole;

namespace JiaCeMonitorSystem.Application.Services.TenantManagement
{
    /// <summary>
    /// 租户按钮权限应用服务
    /// </summary>
    [Authorize]
    public class TenantButtonPermissionAppService : ApplicationService, ITenantButtonPermissionAppService
    {
        private readonly IRepository<TenantButtonPermission, Guid> _buttonPermissionRepo;
        private readonly IRepository<ModuleButton, Guid> _moduleButtonRepo;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IdentityUserManager _identityUserManager;

        public TenantButtonPermissionAppService(
            IRepository<TenantButtonPermission, Guid> buttonPermissionRepo,
            IRepository<ModuleButton, Guid> moduleButtonRepo,
            IIdentityRoleRepository identityRoleRepository,
            IdentityUserManager identityUserManager)
        {
            _buttonPermissionRepo = buttonPermissionRepo;
            _moduleButtonRepo = moduleButtonRepo;
            _identityRoleRepository = identityRoleRepository;
            _identityUserManager = identityUserManager;
        }

        public async Task GrantButtonsAsync(Guid roleId, List<Guid> buttonIds)
        {
            var tenantId = CurrentTenant.Id;
            var existing = await _buttonPermissionRepo.GetListAsync(
                x => x.TenantId == tenantId && x.RoleId == roleId);
            var existingButtonIds = existing.Select(x => x.ButtonId).ToHashSet();

            foreach (var buttonId in buttonIds)
            {
                if (existingButtonIds.Contains(buttonId))
                {
                    continue;
                }

                await _buttonPermissionRepo.InsertAsync(new TenantButtonPermission(
                    GuidGenerator.Create(),
                    tenantId ?? Guid.Empty,
                    buttonId,
                    true,
                    roleId)
                {
                    TenantId = tenantId
                });
            }
        }

        public async Task RevokeButtonsAsync(Guid roleId, List<Guid> buttonIds)
        {
            var tenantId = CurrentTenant.Id;
            var permissions = await _buttonPermissionRepo.GetListAsync(
                x => x.TenantId == tenantId
                     && x.RoleId == roleId
                     && buttonIds.Contains(x.ButtonId));

            foreach (var permission in permissions)
            {
                await _buttonPermissionRepo.DeleteAsync(permission);
            }
        }

        public async Task<List<ButtonPermissionDto>> GetRoleButtonPermissionsAsync(Guid roleId, Guid? moduleId = null)
        {
            var tenantId = CurrentTenant.Id;

            // 查询按钮（按模块筛选）
            var buttons = await _moduleButtonRepo.GetListAsync(
                b => b.EnabledMark && (!moduleId.HasValue || b.ModuleId == moduleId.Value));

            // 查询该角色已授权的按钮
            var grantedPermissions = await _buttonPermissionRepo.GetListAsync(
                x => x.TenantId == tenantId && x.RoleId == roleId && x.IsGranted);
            var grantedButtonIds = grantedPermissions.Select(x => x.ButtonId).ToHashSet();

            return buttons.Select(b => new ButtonPermissionDto
            {
                ButtonId = b.Id,
                EnCode = b.EnCode,
                FullName = b.FullName,
                ModuleId = b.ModuleId,
                IsGranted = grantedButtonIds.Contains(b.Id)
            }).ToList();
        }

        public async Task<List<ModuleButtonDto>> GetMyAvailableButtonsAsync(Guid moduleId)
        {
            var tenantId = CurrentTenant.Id;
            var userId = CurrentUser.Id ?? throw new UnauthorizedAccessException("当前用户未登录");

            // 获取当前用户所属的所有角色名称
            var user = await _identityUserManager.GetByIdAsync(userId);
            var roleNames = await _identityUserManager.GetRolesAsync(user);

            var buttons = await _moduleButtonRepo.GetListAsync(
                b => b.EnabledMark && b.ModuleId == moduleId);

            // admin 角色返回全部按钮
            var isAdmin = roleNames.Any(r => r.Equals("admin", StringComparison.OrdinalIgnoreCase));
            if (isAdmin)
            {
                return ObjectMapper.Map<List<ModuleButton>, List<ModuleButtonDto>>(buttons);
            }

            // 获取当前用户的角色 ID 列表
            var roleIds = new List<Guid>();
            foreach (var roleName in roleNames)
            {
                var role = await _identityRoleRepository.FindByNormalizedNameAsync(roleName.ToUpperInvariant());
                if (role != null)
                {
                    roleIds.Add(role.Id);
                }
            }

            if (!roleIds.Any())
            {
                // 无角色：只返回公共按钮
                var publicButtons = buttons.Where(b => b.IsPublic).ToList();
                return ObjectMapper.Map<List<ModuleButton>, List<ModuleButtonDto>>(publicButtons);
            }

            // 查询这些角色被授权的按钮
            var grantedButtonIds = (await _buttonPermissionRepo.GetListAsync(
                    x => x.TenantId == tenantId
                         && x.IsGranted
                         && x.RoleId != null
                         && roleIds.Contains(x.RoleId.Value)))
                .Select(x => x.ButtonId)
                .ToHashSet();

            var availableButtons = buttons
                .Where(b => b.IsPublic || grantedButtonIds.Contains(b.Id))
                .ToList();

            return ObjectMapper.Map<List<ModuleButton>, List<ModuleButtonDto>>(availableButtons);
        }
    }
}
