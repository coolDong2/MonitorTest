using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Permissions;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.PermissionManagement;

namespace JiaCeMonitorSystem.Services.PermissionManagement
{
    /// <summary>
    /// 权限管理应用服务
    /// </summary>
    [Authorize]
    public class PermissionAppService : ApplicationService, JiaCeMonitorSystem.Interfaces.IPermissionAppService
    {
        private readonly IPermissionManager _permissionManager;
        private readonly IStaticPermissionDefinitionStore _permissionDefinitionStore;

        public PermissionAppService(
            IPermissionManager permissionManager,
            IStaticPermissionDefinitionStore permissionDefinitionStore)
        {
            _permissionManager = permissionManager;
            _permissionDefinitionStore = permissionDefinitionStore;
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        public async Task<PermissionTreeDto> GetPermissionTreeAsync(string providerName, string providerKey)
        {
            var root = new PermissionTreeDto
            {
                Name = "Root",
                DisplayName = "所有权限",
                Children = new List<PermissionTreeDto>()
            };

            var groups = await _permissionDefinitionStore.GetGroupsAsync();
            foreach (var group in groups)
            {
                var groupNode = new PermissionTreeDto
                {
                    Name = group.Name,
                    DisplayName = group.DisplayName?.Localize(StringLocalizerFactory) ?? group.Name,
                    Children = new List<PermissionTreeDto>()
                };

                foreach (var permission in group.GetPermissionsWithChildren())
                {
                    if (permission.Parent == null)
                    {
                        var permissionNode = await BuildPermissionNodeAsync(permission, providerName, providerKey);
                        groupNode.Children.Add(permissionNode);
                    }
                }

                if (groupNode.Children.Any())
                {
                    root.Children.Add(groupNode);
                }
            }

            return root;
        }

        /// <summary>
        /// 保存权限授权
        /// </summary>
        [Authorize(Permissions.Permissions.Permissions_Grant)]
        public async Task GrantAsync(PermissionGrantDto input)
        {
            // 先获取当前已授权的权限
            var currentGranted = await _permissionManager.GetAllAsync(input.ProviderName, input.ProviderKey);
            var currentNames = currentGranted.Select(p => p.Name).ToList();

            // 撤销不再授权的权限
            foreach (var permissionName in currentNames)
            {
                if (!input.Permissions.Contains(permissionName))
                {
                    await _permissionManager.SetAsync(permissionName, input.ProviderName, input.ProviderKey, false);
                }
            }

            // 授予新权限
            foreach (var permissionName in input.Permissions)
            {
                if (!currentNames.Contains(permissionName))
                {
                    await _permissionManager.SetAsync(permissionName, input.ProviderName, input.ProviderKey, true);
                }
            }
        }

        private async Task<PermissionTreeDto> BuildPermissionNodeAsync(
            PermissionDefinition permission,
            string providerName,
            string providerKey)
        {
            var grant = await _permissionManager.GetAsync(permission.Name, providerName, providerKey);
            var isGranted = grant != null;

            var node = new PermissionTreeDto
            {
                Name = permission.Name,
                DisplayName = permission.DisplayName?.Localize(StringLocalizerFactory) ?? permission.Name,
                ParentName = permission.Parent?.Name,
                IsGranted = isGranted,
                Children = new List<PermissionTreeDto>()
            };

            foreach (var child in permission.Children)
            {
                var childNode = await BuildPermissionNodeAsync(child, providerName, providerKey);
                node.Children.Add(childNode);
            }

            return node;
        }
    }
}
