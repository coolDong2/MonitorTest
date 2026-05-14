using System;
using System.Collections.Generic;
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
    /// 租户数据验证器，对比共享库与独立库的数据一致性
    /// </summary>
    public class TenantDataValidator : ITenantDataValidator, ITransientDependency
    {
        private readonly ILogger<TenantDataValidator> _logger;

        public TenantDataValidator(ILogger<TenantDataValidator> logger)
        {
            _logger = logger;
        }

        public async Task<TenantDataValidationResult> ValidateAsync(Guid tenantId, string sourceConnectionString, string targetConnectionString, CancellationToken cancellationToken = default)
        {
            var result = new TenantDataValidationResult();
            var items = new List<TenantDataValidationItem>();

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

                // 验证 Identity 数据
                items.Add(await ValidateTableAsync<IdentityUser>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<IdentityRole>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<IdentityUserRole>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<IdentityUserClaim>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<IdentityRoleClaim>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<IdentityUserLogin>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<IdentityUserToken>(sourceContext, targetContext, tenantId, cancellationToken));

                // 验证 Permission 数据
                items.Add(await ValidateTableAsync<PermissionGrant>(sourceContext, targetContext, tenantId, cancellationToken));

                // 验证 TenantManagement 数据
                items.Add(await ValidateTableAsync<TenantModuleGrant>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<TenantButtonPermission>(sourceContext, targetContext, tenantId, cancellationToken));
                items.Add(await ValidateTableAsync<TenantUserExtension>(sourceContext, targetContext, tenantId, cancellationToken));

                result.Items = items;
                result.IsValid = items.All(i => i.IsMatched);

                if (result.IsValid)
                {
                    _logger.LogInformation("租户 {TenantId} 数据验证通过", tenantId);
                }
                else
                {
                    var failed = items.Where(i => !i.IsMatched).Select(i => $"{i.TableName}(源:{i.SourceCount}/目标:{i.TargetCount})");
                    result.ErrorMessage = $"以下表数据不一致: {string.Join(", ", failed)}";
                    _logger.LogWarning("租户 {TenantId} 数据验证失败: {Message}", tenantId, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ErrorMessage = ex.Message;
                _logger.LogError(ex, "租户 {TenantId} 数据验证异常", tenantId);
            }

            return result;
        }

        private static async Task<TenantDataValidationItem> ValidateTableAsync<TEntity>(
            DbContext source,
            DbContext target,
            Guid tenantId,
            CancellationToken cancellationToken)
            where TEntity : class
        {
            var tableName = source.Model.FindEntityType(typeof(TEntity))?.GetTableName() ?? typeof(TEntity).Name;

            var sourceCount = await source.Set<TEntity>()
                .AsNoTracking()
                .Where(e => EF.Property<Guid?>(e, "TenantId") == tenantId)
                .CountAsync(cancellationToken);

            var targetCount = await target.Set<TEntity>()
                .AsNoTracking()
                .Where(e => EF.Property<Guid?>(e, "TenantId") == tenantId)
                .CountAsync(cancellationToken);

            return new TenantDataValidationItem
            {
                TableName = tableName,
                SourceCount = sourceCount,
                TargetCount = targetCount,
                IsMatched = sourceCount == targetCount
            };
        }
    }
}
