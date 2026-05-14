using System;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.SystemModules;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace JiaCeMonitorSystem.Seeds
{
    /// <summary>
    /// 租户管理数据种子，创建 Host 租户配置、模块授权与用户扩展
    /// </summary>
    public class TenantManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<TenantConfiguration, Guid> _tenantConfigRepository;
        private readonly IRepository<TenantModuleGrant, Guid> _tenantModuleGrantRepository;
        private readonly IRepository<TenantUserExtension, Guid> _tenantUserExtensionRepository;
        private readonly IRepository<SystemModule, Guid> _systemModuleRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IGuidGenerator _guidGenerator;

        public TenantManagementDataSeedContributor(
            IRepository<TenantConfiguration, Guid> tenantConfigRepository,
            IRepository<TenantModuleGrant, Guid> tenantModuleGrantRepository,
            IRepository<TenantUserExtension, Guid> tenantUserExtensionRepository,
            IRepository<SystemModule, Guid> systemModuleRepository,
            UserManager<IdentityUser> userManager,
            IGuidGenerator guidGenerator)
        {
            _tenantConfigRepository = tenantConfigRepository;
            _tenantModuleGrantRepository = tenantModuleGrantRepository;
            _tenantUserExtensionRepository = tenantUserExtensionRepository;
            _systemModuleRepository = systemModuleRepository;
            _userManager = userManager;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedHostTenantConfigurationAsync();
            await SeedHostModuleGrantsAsync();
            await SeedAdminUserExtensionAsync();
        }

        /// <summary>
        /// 为 Host 创建默认租户配置
        /// </summary>
        private async Task SeedHostTenantConfigurationAsync()
        {
            var existing = await _tenantConfigRepository.FirstOrDefaultAsync(x => x.TenantId == null);
            if (existing != null)
            {
                return;
            }

            var config = new TenantConfiguration(
                _guidGenerator.Create(),
                Guid.Empty, // Host 环境无具体租户Id，使用 Empty 占位（实际 TenantId 为 null）
                "HOST"
            )
            {
                TenantId = null,
                Status = TenantStatus.Active,
                IsIndependentDatabase = false,
                ExpireDate = null // Host 永不到期
            };

            await _tenantConfigRepository.InsertAsync(config);
        }

        /// <summary>
        /// 为 Host 授予所有已有系统模块权限
        /// </summary>
        private async Task SeedHostModuleGrantsAsync()
        {
            var modules = await _systemModuleRepository.GetListAsync();
            if (!modules.Any())
            {
                return;
            }

            var existingGrants = await _tenantModuleGrantRepository.GetListAsync(x => x.TenantId == null);
            var grantedModuleIds = existingGrants.Select(g => g.ModuleId).ToHashSet();

            foreach (var module in modules)
            {
                if (grantedModuleIds.Contains(module.Id))
                {
                    continue;
                }

                var grant = new TenantModuleGrant(
                    _guidGenerator.Create(),
                    Guid.Empty, // Host 环境 TenantId 为 null
                    module.Id,
                    true
                )
                {
                    TenantId = null,
                    GrantDate = DateTime.Now
                };

                await _tenantModuleGrantRepository.InsertAsync(grant);
            }
        }

        /// <summary>
        /// 为默认 admin 用户创建租户扩展信息
        /// </summary>
        private async Task SeedAdminUserExtensionAsync()
        {
            var adminUser = await _userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                return;
            }

            var existing = await _tenantUserExtensionRepository.FirstOrDefaultAsync(x => x.UserId == adminUser.Id);
            if (existing != null)
            {
                return;
            }

            var extension = new TenantUserExtension(
                _guidGenerator.Create(),
                adminUser.Id,
                Guid.Empty, // Host 环境 TenantId 为 null
                UserType.SystemAdmin,
                "HOST"
            )
            {
                TenantId = null
            };

            await _tenantUserExtensionRepository.InsertAsync(extension);
        }
    }
}
