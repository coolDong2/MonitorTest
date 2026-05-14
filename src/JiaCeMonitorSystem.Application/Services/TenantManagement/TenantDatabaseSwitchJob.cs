using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.Dtos.TenantManagement;
using JiaCeMonitorSystem.Services.TenantManagement;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus.Distributed;

namespace JiaCeMonitorSystem.Application.Services.TenantManagement
{
    /// <summary>
    /// 租户数据库切换后台任务
    /// </summary>
    public class TenantDatabaseSwitchJob : AsyncBackgroundJob<TenantDatabaseSwitchArgs>
    {
        private readonly TenantDatabaseSwitchService _switchService;
        private readonly ILogger<TenantDatabaseSwitchJob> _logger;
        private readonly IDistributedEventBus _eventBus;

        public TenantDatabaseSwitchJob(
            TenantDatabaseSwitchService switchService,
            ILogger<TenantDatabaseSwitchJob> logger,
            IDistributedEventBus eventBus)
        {
            _switchService = switchService;
            _logger = logger;
            _eventBus = eventBus;
        }

        public override async Task ExecuteAsync(TenantDatabaseSwitchArgs args)
        {
            _logger.LogInformation(
                "开始执行租户数据库切换任务，TenantId={TenantId}, TenantName={TenantName}",
                args.TenantId, args.TenantName);

            await PublishStatusAsync(args, "started", $"租户 {args.TenantName} 的独立数据库切换任务已开始");

            try
            {
                var connectionString = await _switchService.SwitchToIndependentDatabaseAsync(
                    args.TenantId, args.TenantName);

                _logger.LogInformation(
                    "租户 {TenantName} 已成功切换到独立数据库，连接串={ConnectionString}",
                    args.TenantName, connectionString);

                await PublishStatusAsync(args, "completed", $"租户 {args.TenantName} 已成功切换到独立数据库", connectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "租户 {TenantName} 切换到独立数据库失败",
                    args.TenantName);

                await PublishStatusAsync(args, "failed", $"租户 {args.TenantName} 切换到独立数据库失败：{ex.Message}");
                throw;
            }
        }

        private async Task PublishStatusAsync(TenantDatabaseSwitchArgs args, string status, string message, string? connectionString = null)
        {
            try
            {
                await _eventBus.PublishAsync(new TenantDatabaseSwitchStatusEto
                {
                    TenantId = args.TenantId,
                    TenantName = args.TenantName,
                    Status = status,
                    Message = message,
                    ConnectionString = connectionString,
                    Timestamp = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "发布租户数据库切换状态事件失败，TenantId={TenantId}", args.TenantId);
            }
        }
    }
}
