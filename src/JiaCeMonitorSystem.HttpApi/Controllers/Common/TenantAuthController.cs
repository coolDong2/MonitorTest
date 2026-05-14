using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers.Common
{
    /// <summary>
    /// 租户认证控制器，提供租户用户登录接口
    /// </summary>
    [Route("api/tenant-auth")]
    [AllowAnonymous]
    public class TenantAuthController : JiaCeMonitorSystemController
    {
        private readonly ITenantAuthAppService _tenantAuthAppService;

        /// <summary>
        /// 初始化租户认证控制器
        /// </summary>
        public TenantAuthController(ITenantAuthAppService tenantAuthAppService)
        {
            _tenantAuthAppService = tenantAuthAppService;
        }

        /// <summary>
        /// 租户用户登录（单位编码 + 用户名密码）
        /// </summary>
        [HttpPost("login")]
        public virtual async Task<TenantLoginResultDto> TenantLoginAsync([FromBody] TenantUserLoginDto input)
        {
            return await _tenantAuthAppService.TenantLoginAsync(input);
        }

        /// <summary>
        /// 刷新租户用户 Token
        /// </summary>
        [HttpPost("refresh-token")]
        public virtual async Task<TenantLoginResultDto> RefreshTokenAsync([FromBody] string refreshToken)
        {
            return await _tenantAuthAppService.RefreshTokenAsync(refreshToken);
        }
    }
}
