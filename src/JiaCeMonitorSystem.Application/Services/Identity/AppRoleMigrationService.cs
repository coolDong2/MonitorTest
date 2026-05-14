using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.AppRoles;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Services.Identity
{
    /// <summary>
    /// AppRole 到 IdentityRole 的迁移服务
    /// </summary>
    public class AppRoleMigrationService : ITransientDependency
    {
        private readonly IRepository<AppRole, Guid> _appRoleRepository;
        private readonly IdentityRoleManager _identityRoleManager;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ILogger<AppRoleMigrationService> _logger;

        public AppRoleMigrationService(
            IRepository<AppRole, Guid> appRoleRepository,
            IdentityRoleManager identityRoleManager,
            IIdentityRoleRepository identityRoleRepository,
            IGuidGenerator guidGenerator,
            ILogger<AppRoleMigrationService> logger)
        {
            _appRoleRepository = appRoleRepository;
            _identityRoleManager = identityRoleManager;
            _identityRoleRepository = identityRoleRepository;
            _guidGenerator = guidGenerator;
            _logger = logger;
        }

        /// <summary>
        /// 将所有 AppRole 迁移到 IdentityRole（幂等）
        /// </summary>
        public async Task MigrateAsync()
        {
            _logger.LogInformation("开始 AppRole → IdentityRole 迁移...");

            var appRoles = await _appRoleRepository.GetListAsync();
            var migratedCount = 0;
            var skippedCount = 0;

            foreach (var appRole in appRoles)
            {
                // 检查是否已存在同名 IdentityRole
                var existing = await _identityRoleManager.FindByNameAsync(appRole.FullName);
                if (existing != null)
                {
                    _logger.LogDebug("IdentityRole '{RoleName}' 已存在，跳过", appRole.FullName);
                    skippedCount++;
                    continue;
                }

                // 创建 IdentityRole（AppRole 非多租户，迁移为 Host 级角色）
                var identityRole = new IdentityRole(
                    _guidGenerator.Create(),
                    appRole.FullName,
                    null)
                {
                    IsDefault = false,
                    IsPublic = true
                };

                var result = await _identityRoleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    _logger.LogInformation("已创建 IdentityRole '{RoleName}' (来源 AppRole: {AppRoleId})",
                        appRole.FullName, appRole.Id);
                    migratedCount++;
                }
                else
                {
                    _logger.LogError("创建 IdentityRole '{RoleName}' 失败: {Errors}",
                        appRole.FullName, string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            _logger.LogInformation("AppRole 迁移完成: 迁移 {Migrated} 个, 跳过 {Skipped} 个, 总计 {Total}",
                migratedCount, skippedCount, appRoles.Count);
        }
    }
}
