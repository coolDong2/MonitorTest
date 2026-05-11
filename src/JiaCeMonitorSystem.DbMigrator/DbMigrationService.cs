using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using JiaCeMonitorSystem.Data;
using JiaCeMonitorSystem.EntityFrameworkCore;
using Npgsql;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace JiaCeMonitorSystem.DbMigrator
{
    public class DbMigrationService : ITransientDependency
    {
        private readonly ILogger<DbMigrationService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITenantRepository _tenantRepository;
        private readonly ICurrentTenant _currentTenant;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly IDataSeeder _dataSeeder;

        public DbMigrationService(
            ILogger<DbMigrationService> logger,
            IServiceProvider serviceProvider,
            ITenantRepository tenantRepository,
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            IDistributedEventBus distributedEventBus,
            IDataSeeder dataSeeder)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _tenantRepository = tenantRepository;
            _currentTenant = currentTenant;
            _unitOfWorkManager = unitOfWorkManager;
            _distributedEventBus = distributedEventBus;
            _dataSeeder = dataSeeder;
        }

        public async Task MigrateAsync()
        {
            _logger.LogInformation("开始数据库迁移...");

            await MigrateHostDatabaseAsync();

            if (MultiTenancyConsts.IsEnabled)
            {
                await MigrateTenantDatabasesAsync();
            }

            _logger.LogInformation("数据库迁移完成！");
        }

        private async Task MigrateHostDatabaseAsync()
        {
            _logger.LogInformation("迁移主机数据库...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<JiaCeMonitorSystemDbContext>();
                
                // 检测数据库一致性：若迁移历史存在但关键表缺失，给出明确提示
                var connectionString = dbContext.Database.GetConnectionString();
                var hasMigrationsHistory = await HasMigrationsHistoryAsync(connectionString);
                var hasSettingDefinitions = await HasTableAsync(connectionString, "AbpSettingDefinitions");
                
                if (hasMigrationsHistory && !hasSettingDefinitions)
                {
                    _logger.LogError("数据库迁移状态不一致：__EFMigrationsHistory 表存在但 AbpSettingDefinitions 等关键表缺失。");
                    _logger.LogError("请手动执行以下修复步骤之一：");
                    _logger.LogError("1. 删除数据库后重新运行 DbMigrator（推荐开发环境）");
                    _logger.LogError("2. 手动清理 __EFMigrationsHistory 表中的记录后重新运行");
                    throw new Exception("数据库迁移状态不一致，请查看日志并按指引修复。");
                }
                
                await dbContext.Database.MigrateAsync();
            }

            _logger.LogInformation("主机数据库迁移完成");
        }

        private async Task<bool> HasMigrationsHistoryAsync(string? connectionString)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                await using var cmd = new NpgsqlCommand(
                    "SELECT 1 FROM information_schema.tables WHERE table_name = '__EFMigrationsHistory'", connection);
                var result = await cmd.ExecuteScalarAsync();
                return result != null;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> HasTableAsync(string? connectionString, string tableName)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                await using var cmd = new NpgsqlCommand(
                    "SELECT 1 FROM information_schema.tables WHERE table_name = @tableName", connection);
                cmd.Parameters.AddWithValue("tableName", tableName);
                var result = await cmd.ExecuteScalarAsync();
                return result != null;
            }
            catch
            {
                return false;
            }
        }

        private async Task MigrateTenantDatabasesAsync()
        {
            _logger.LogInformation("迁移租户数据库...");

            var tenants = await _tenantRepository.GetListAsync();

            foreach (var tenant in tenants)
            {
                using (_currentTenant.Change(tenant.Id))
                {
                    _logger.LogInformation($"迁移租户 '{tenant.Name}' 的数据库...");

                    try 
                    {
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<JiaCeMonitorSystemDbContext>();
                            await dbContext.Database.MigrateAsync();
                        }

                        _logger.LogInformation($"租户 '{tenant.Name}' 的数据库迁移完成");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"迁移租户 '{tenant.Name}' 的数据库时出错");
                    }
                }
            }

            _logger.LogInformation("租户数据库迁移完成");
        }

        public async Task SeedAsync()
        {
            _logger.LogInformation("开始种子数据...");

            // 使用 ABP 内置数据种子器执行所有 IDataSeedContributor
            await _dataSeeder.SeedAsync(new DataSeedContext(null));

            _logger.LogInformation("种子数据完成");
        }
    }

    [Serializable]
    public class ApplyDatabaseMigrationsEto
    {
        public string DatabaseName { get; set; } = string.Empty;
    }
}
