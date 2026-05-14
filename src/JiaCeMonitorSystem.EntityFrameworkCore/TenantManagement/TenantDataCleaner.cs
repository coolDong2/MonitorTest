using System.Threading;
using System.Threading.Tasks;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.Extensions.Logging;
using Npgsql;
using Volo.Abp.DependencyInjection;

namespace JiaCeMonitorSystem.EntityFrameworkCore.TenantManagement
{
    /// <summary>
    /// 租户数据清理器，切换失败时删除已创建的独立数据库
    /// </summary>
    public class TenantDataCleaner : ITenantDataCleaner, ITransientDependency
    {
        private readonly ILogger<TenantDataCleaner> _logger;

        public TenantDataCleaner(ILogger<TenantDataCleaner> logger)
        {
            _logger = logger;
        }

        public async Task CleanAsync(string targetConnectionString, CancellationToken cancellationToken = default)
        {
            var builder = new NpgsqlConnectionStringBuilder(targetConnectionString);
            var dbName = builder.Database;

            _logger.LogWarning("开始回滚：准备删除独立数据库 {DatabaseName}", dbName);

            var adminConnectionString = $"Host={builder.Host};Port={builder.Port};Database=postgres;Username={builder.Username};Password={builder.Password}";

            await using var connection = new NpgsqlConnection(adminConnectionString);
            await connection.OpenAsync(cancellationToken);

            // 终止所有连接到目标数据库的会话
            var terminateSql = $"SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{dbName}' AND pid <> pg_backend_pid()";
            await using (var terminateCmd = new NpgsqlCommand(terminateSql, connection))
            {
                await terminateCmd.ExecuteNonQueryAsync(cancellationToken);
            }

            // 删除数据库
            var dropSql = $"DROP DATABASE IF EXISTS \"{dbName}\"";
            await using (var dropCmd = new NpgsqlCommand(dropSql, connection))
            {
                await dropCmd.ExecuteNonQueryAsync(cancellationToken);
            }

            _logger.LogWarning("回滚完成：已删除独立数据库 {DatabaseName}", dbName);
        }
    }
}
