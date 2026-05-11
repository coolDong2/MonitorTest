using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using IdentityRole = Volo.Abp.Identity.IdentityRole;
namespace JiaCeMonitorSystem.Seeds
{
    /// <summary>
    /// 身份数据种子，创建默认管理员角色与用户
    /// </summary>
    public class IdentityDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IdentityUserManager _userManager;
        private readonly IdentityRoleManager _roleManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IGuidGenerator _guidGenerator;

        public IdentityDataSeedContributor(
            IdentityUserManager userManager,
            IdentityRoleManager roleManager,
            IPermissionManager permissionManager,
            IGuidGenerator guidGenerator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _permissionManager = permissionManager;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            // 创建系统管理员角色
            var adminRole = await _roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                adminRole = new IdentityRole(
                    _guidGenerator.Create(),
                    "admin",
                    null
                )
                {
                    IsStatic = true,
                    IsPublic = true
                };

                var result = await _roleManager.CreateAsync(adminRole);
                if (!result.Succeeded)
                {
                    throw new Exception($"创建 admin 角色失败: {string.Join(", ", result.Errors)}");
                }
            }

            // 创建监测员角色
            var monitorRole = await _roleManager.FindByNameAsync("monitor");
            if (monitorRole == null)
            {
                monitorRole = new IdentityRole(
                    _guidGenerator.Create(),
                    "monitor",
                    null
                )
                {
                    IsStatic = false,
                    IsPublic = true
                };

                await _roleManager.CreateAsync(monitorRole);
            }

            // 创建设备管理员角色
            var deviceRole = await _roleManager.FindByNameAsync("device_admin");
            if (deviceRole == null)
            {
                deviceRole = new IdentityRole(
                    _guidGenerator.Create(),
                    "device_admin",
                    null
                )
                {
                    IsStatic = false,
                    IsPublic = true
                };

                await _roleManager.CreateAsync(deviceRole);
            }

            // 为 device_admin 角色分配设备管理、文件管理、监测项目类型权限
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.Devices_Default);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.Devices_Create);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.Devices_Edit);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.Devices_Delete);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.Devices_Calibrate);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.FileManages_Default);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.FileManages_Create);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.FileManages_Edit);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.MonitoringItemTypes_Default);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.MonitoringItemTypes_Create);
            await GrantRolePermissionAsync("device_admin", Permissions.Permissions.MonitoringItemTypes_Edit);
        }

        private async Task GrantRolePermissionAsync(string roleName, string permissionName)
        {
            await _permissionManager.SetAsync(
                permissionName,
                Volo.Abp.Authorization.Permissions.RolePermissionValueProvider.ProviderName,
                roleName,
                true);
        }

        private async Task SeedUsersAsync()
        {
            // 创建默认管理员用户
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                adminUser = new IdentityUser(
                    _guidGenerator.Create(),
                    "admin",
                    "admin@jiace.local",
                    null
                );

                adminUser.SetIsActive(true);
                adminUser.SetEmailConfirmed(true);
                adminUser.SetPhoneNumberConfirmed(true);
                adminUser.Name = "系统管理员";

                var result = await _userManager.CreateAsync(adminUser, "1q2w3E*");
                if (!result.Succeeded)
                {
                    throw new Exception($"创建 admin 用户失败: {string.Join(", ", result.Errors)}");
                }

                // 分配 admin 角色
                await _userManager.AddToRoleAsync(adminUser, "admin");
            }

            // 创建测试监测员
            var monitorUser = await _userManager.FindByNameAsync("monitor");
            if (monitorUser == null)
            {
                monitorUser = new IdentityUser(
                    _guidGenerator.Create(),
                    "monitor",
                    "monitor@jiace.local",
                    null
                );

                monitorUser.SetIsActive(true);
                monitorUser.SetEmailConfirmed(true);
                monitorUser.SetPhoneNumberConfirmed(true);
                monitorUser.Name = "监测员";

                await _userManager.CreateAsync(monitorUser, "1q2w3E*");
                await _userManager.AddToRoleAsync(monitorUser, "monitor");
            }
        }
    }
}
