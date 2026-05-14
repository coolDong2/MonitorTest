using System;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace JiaCeMonitorSystem.Application.Services.TenantManagement
{
    /// <summary>
    /// 租户状态检查后台任务
    /// </summary>
    public class TenantStatusCheckJob : BackgroundJob<Guid?>
    {
        private readonly IRepository<TenantConfiguration, Guid> _configRepo;
        private readonly ILogger<TenantStatusCheckJob> _logger;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TenantStatusCheckJob(
            IRepository<TenantConfiguration, Guid> configRepo,
            ILogger<TenantStatusCheckJob> logger,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _configRepo = configRepo;
            _logger = logger;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public override void Execute(Guid? tenantId)
        {
            ExecuteAsync(tenantId).GetAwaiter().GetResult();
        }

        private async Task ExecuteAsync(Guid? tenantId)
        {
            _logger.LogInformation("开始执行租户到期状态检查...");

            using var uow = _unitOfWorkManager.Begin();

            var now = DateTime.Now;
            var remindThreshold = now.AddDays(7);

            var queryable = await _configRepo.GetQueryableAsync();
            var activeConfigs = queryable
                .Where(c => c.Status == TenantStatus.Active || c.Status == TenantStatus.Trial)
                .ToList();

            foreach (var config in activeConfigs)
            {
                if (config.ExpireDate.HasValue && config.ExpireDate.Value <= now)
                {
                    config.Status = TenantStatus.Expired;
                    _logger.LogWarning("租户 {TenantId} 已到期，状态已变更为 Expired", config.TenantId);
                }
                else if (config.ExpireDate.HasValue && config.ExpireDate.Value <= remindThreshold && config.RemindDate == null)
                {
                    config.RemindDate = now;
                    _logger.LogInformation("租户 {TenantId} 即将到期（{ExpireDate}），已记录提醒", config.TenantId, config.ExpireDate);
                }
            }

            await uow.CompleteAsync();

            _logger.LogInformation("租户到期状态检查完成");
        }
    }
}
