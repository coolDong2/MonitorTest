using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JiaCeMonitorSystem.EntityFrameworkCore;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace JiaCeMonitorSystem.EntityFrameworkCore.TenantManagement
{
    /// <summary>
    /// 租户数据导出器，负责将共享库中的租户数据迁移到独立库
    /// </summary>
    public class TenantDataExporter : ITenantDataExporter, ITransientDependency
    {
        private readonly ILogger<TenantDataExporter> _logger;

        public TenantDataExporter(ILogger<TenantDataExporter> logger)
        {
            _logger = logger;
        }

        public async Task<TenantDataExportResult> ExportAsync(Guid tenantId, string sourceConnectionString, string targetConnectionString, CancellationToken cancellationToken = default)
        {
            var result = new TenantDataExportResult();

            try
            {
                var sourceOptions = new DbContextOptionsBuilder<JiaCeMonitorSystemDbContext>()
                    .UseNpgsql(sourceConnectionString)
                    .Options;
                var targetOptions = new DbContextOptionsBuilder<JiaCeMonitorSystemDbContext>()
                    .UseNpgsql(targetConnectionString)
                    .Options;

                await using var sourceContext = new JiaCeMonitorSystemDbContext(sourceOptions);
                await using var targetContext = new JiaCeMonitorSystemDbContext(targetOptions);

                // 1. 先删除目标库子表（避免外键冲突）
                await DeleteByTenantAsync<IdentityUserRole>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<IdentityUserClaim>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<IdentityRoleClaim>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<IdentityUserLogin>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<IdentityUserToken>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<PermissionGrant>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<TenantModuleGrant>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<TenantButtonPermission>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<TenantUserExtension>(targetContext, tenantId, cancellationToken);

                // 2. 删除目标库父表
                await DeleteByTenantAsync<IdentityUser>(targetContext, tenantId, cancellationToken);
                await DeleteByTenantAsync<IdentityRole>(targetContext, tenantId, cancellationToken);

                // 3. 插入父表
                result.IdentityUserCount = await MigrateAsync<IdentityUser>(sourceContext, targetContext, tenantId, cancellationToken);
                result.IdentityRoleCount = await MigrateAsync<IdentityRole>(sourceContext, targetContext, tenantId, cancellationToken);

                // 4. 插入子表
                result.IdentityUserRoleCount = await MigrateAsync<IdentityUserRole>(sourceContext, targetContext, tenantId, cancellationToken);
                result.IdentityUserClaimCount = await MigrateAsync<IdentityUserClaim>(sourceContext, targetContext, tenantId, cancellationToken);
                result.IdentityRoleClaimCount = await MigrateAsync<IdentityRoleClaim>(sourceContext, targetContext, tenantId, cancellationToken);
                result.IdentityUserLoginCount = await MigrateAsync<IdentityUserLogin>(sourceContext, targetContext, tenantId, cancellationToken);
                result.IdentityUserTokenCount = await MigrateAsync<IdentityUserToken>(sourceContext, targetContext, tenantId, cancellationToken);

                // 5. 插入权限与租户管理数据
                result.PermissionGrantCount = await MigrateAsync<PermissionGrant>(sourceContext, targetContext, tenantId, cancellationToken);
                result.TenantModuleGrantCount = await MigrateAsync<TenantModuleGrant>(sourceContext, targetContext, tenantId, cancellationToken);
                result.TenantButtonPermissionCount = await MigrateAsync<TenantButtonPermission>(sourceContext, targetContext, tenantId, cancellationToken);
                result.TenantUserExtensionCount = await MigrateAsync<TenantUserExtension>(sourceContext, targetContext, tenantId, cancellationToken);

                result.Succeeded = true;
                _logger.LogInformation(
                    "租户 {TenantId} 数据迁移完成：Users={Users}, Roles={Roles}, UserRoles={UserRoles}, " +
                    "PermissionGrants={PermissionGrants}, ModuleGrants={ModuleGrants}, ButtonPermissions={ButtonPermissions}, UserExtensions={UserExtensions}",
                    tenantId,
                    result.IdentityUserCount,
                    result.IdentityRoleCount,
                    result.IdentityUserRoleCount,
                    result.PermissionGrantCount,
                    result.TenantModuleGrantCount,
                    result.TenantButtonPermissionCount,
                    result.TenantUserExtensionCount);
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.ErrorMessage = ex.Message;
                _logger.LogError(ex, "租户 {TenantId} 数据迁移失败", tenantId);
                throw;
            }

            return result;
        }

        private static async Task DeleteByTenantAsync<TEntity>(DbContext context, Guid tenantId, CancellationToken cancellationToken)
            where TEntity : class
        {
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            if (entityType == null) return;

            var tableName = entityType.GetTableName();
            var schema = entityType.GetSchema();
            var fullTableName = string.IsNullOrEmpty(schema)
                ? $"\"{tableName}\""
                : $"\"{schema}\".\"{tableName}\"";

            var sql = $"DELETE FROM {fullTableName} WHERE \"TenantId\" = @p0";
            await context.Database.ExecuteSqlRawAsync(sql, new object[] { tenantId }, cancellationToken);
        }

        private static async Task<int> MigrateAsync<TEntity>(DbContext source, DbContext target, Guid tenantId, CancellationToken cancellationToken)
            where TEntity : class
        {
            var data = await source.Set<TEntity>()
                .AsNoTracking()
                .Where(e => EF.Property<Guid?>(e, "TenantId") == tenantId)
                .ToListAsync(cancellationToken);

            if (!data.Any()) return 0;

            await target.Set<TEntity>().AddRangeAsync(data, cancellationToken);
            await target.SaveChangesAsync(cancellationToken);

            return data.Count;
        }
    }
}
