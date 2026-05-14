using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.EntityFrameworkCore;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace JiaCeMonitorSystem.EntityFrameworkCore.TenantManagement
{
    /// <summary>
    /// 租户数据库管理器，负责独立数据库的创建与迁移
    /// </summary>
    public class TenantDatabaseManager : ITenantDatabaseManager, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly IDataSeeder _dataSeeder;

        public TenantDatabaseManager(
            IConfiguration configuration,
            IDataSeeder dataSeeder)
        {
            _configuration = configuration;
            _dataSeeder = dataSeeder;
        }

        public async Task<string> CreateDatabaseAsync(Guid tenantId, string tenantName)
        {
            var dbName = $"jcmonitor_tenant_{tenantId:N}";
            var hostConnectionString = _configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Default connection string is not configured.");

            var builder = new NpgsqlConnectionStringBuilder(hostConnectionString);

            var adminConnectionString = $"Host={builder.Host};Port={builder.Port};Database=postgres;Username={builder.Username};Password={builder.Password}";
            await using var connection = new NpgsqlConnection(adminConnectionString);
            await connection.OpenAsync();

            var checkSql = $"SELECT 1 FROM pg_database WHERE datname = '{dbName}'";
            await using var checkCmd = new NpgsqlCommand(checkSql, connection);
            var exists = await checkCmd.ExecuteScalarAsync();

            if (exists == null)
            {
                var createSql = $"CREATE DATABASE \"{dbName}\" WITH OWNER = \"{builder.Username}\" ENCODING = 'UTF8'";
                await using var createCmd = new NpgsqlCommand(createSql, connection);
                await createCmd.ExecuteNonQueryAsync();
            }

            var tenantConnectionString = $"Host={builder.Host};Port={builder.Port};Database={dbName};Username={builder.Username};Password={builder.Password}";

            var options = new DbContextOptionsBuilder<JiaCeMonitorSystemDbContext>()
                .UseNpgsql(tenantConnectionString)
                .Options;

            await using var tenantDbContext = new JiaCeMonitorSystemDbContext(options);
            await tenantDbContext.Database.MigrateAsync();

            await _dataSeeder.SeedAsync(new DataSeedContext(tenantId));

            return tenantConnectionString;
        }
    }
}
