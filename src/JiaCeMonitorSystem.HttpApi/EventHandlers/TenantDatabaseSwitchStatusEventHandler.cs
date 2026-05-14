using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.Hubs;
using Microsoft.AspNetCore.SignalR;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace JiaCeMonitorSystem.EventHandlers
{
    /// <summary>
    /// 租户数据库切换状态事件处理器，通过 SignalR 实时推送给前端
    /// </summary>
    public class TenantDatabaseSwitchStatusEventHandler :
        IDistributedEventHandler<TenantDatabaseSwitchStatusEto>,
        ITransientDependency
    {
        private readonly IHubContext<TenantDatabaseSwitchHub> _hubContext;

        public TenantDatabaseSwitchStatusEventHandler(IHubContext<TenantDatabaseSwitchHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task HandleEventAsync(TenantDatabaseSwitchStatusEto eventData)
        {
            await _hubContext.Clients.Group(eventData.TenantId.ToString("N"))
                .SendAsync("DatabaseSwitchStatusChanged", new
                {
                    eventData.TenantId,
                    eventData.TenantName,
                    eventData.Status,
                    eventData.Message,
                    eventData.ConnectionString,
                    eventData.Timestamp
                });
        }
    }
}
