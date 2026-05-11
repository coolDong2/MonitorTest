using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace JiaCeMonitorSystem
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> Main(string[] args)
        {
            // 允许 Npgsql 处理非 UTC 的 DateTime（兼容 ABP 审计字段的 Local Kind）
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.Console())
                .WriteTo.Async(c => c.File(
                    "Logs/logs.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 31
                ))
                .CreateLogger();

            try
            {
                Log.Information("Starting JiaCeMonitorSystem.HttpApi.Host.");
                var builder = WebApplication.CreateBuilder(args);
                  builder.Host.UseAutofac();
                builder.Host.UseSerilog();
                builder.Services.AddApplication<JiaCeMonitorSystemHttpApiHostModule>();
                
                var app = builder.Build();
                
                await app.InitializeApplicationAsync();
                
                await app.RunAsync();
                
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "JiaCeMonitorSystem.HttpApi.Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
