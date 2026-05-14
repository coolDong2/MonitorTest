using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace JiaCeMonitorSystem.Hubs
{
    /// <summary>
    /// 租户数据库切换状态实时推送 Hub
    /// </summary>
    public class TenantDatabaseSwitchHub : Hub
    {
        /// <summary>
        /// 前端订阅指定租户的数据库切换状态
        /// </summary>
        public async Task SubscribeTenant(Guid tenantId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, tenantId.ToString("N"));
        }

        /// <summary>
        /// 前端取消订阅
        /// </summary>
        public async Task UnsubscribeTenant(Guid tenantId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, tenantId.ToString("N"));
        }
    }
}
