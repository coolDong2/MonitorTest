using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace JiaCeMonitorSystem.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 允许 Npgsql 处理非 UTC 的 DateTime（兼容 ABP 审计字段的 Local Kind）
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/db-migrator.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting database migration...");

                using (var application = AbpApplicationFactory.Create<JiaCeMonitorSystemDbMigratorModule>(
                    options =>
                    {
                        options.UseAutofac();
                        options.Services.ReplaceConfiguration(BuildConfiguration());
                        options.Services.AddLogging(c => c.AddSerilog());
                    }))
                {
                    application.Initialize();

                    var migrationService = application.ServiceProvider.GetRequiredService<DbMigrationService>();
                    await migrationService.MigrateAsync();
                    await migrationService.SeedAsync();

                    Log.Information("Database migration completed successfully!");
                }

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Database migration failed!");
                Environment.Exit(1);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            // 获取 DbMigrator 项目所在目录（兼容 dotnet run 和直接执行）
            var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location)!;
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
