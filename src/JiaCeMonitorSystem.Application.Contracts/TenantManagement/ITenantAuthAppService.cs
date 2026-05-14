using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户认证应用服务接口
    /// </summary>
    public interface ITenantAuthAppService : IApplicationService
    {
        /// <summary>
        /// 租户用户登录
        /// </summary>
        Task<TenantLoginResultDto> TenantLoginAsync(TenantUserLoginDto input);

        /// <summary>
        /// 刷新令牌
        /// </summary>
        Task<TenantLoginResultDto> RefreshTokenAsync(string refreshToken);
    }
}
