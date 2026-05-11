using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Accounts;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    [Route("api/app/account")]
    public class AccountController : JiaCeMonitorSystemController
    {
        private readonly IAccountAppService _accountAppService;

        /// <summary>
        /// 初始化账号控制器
        /// </summary>
        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        [HttpPost("login")]
        public virtual Task<LoginOutputDto> LoginAsync([FromBody] LoginInputDto input)
        {
            return _accountAppService.LoginAsync(input);
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        [HttpGet("current-user")]
        public virtual Task<CurrentUserDto> GetCurrentUserAsync()
        {
            return _accountAppService.GetCurrentUserAsync();
        }

        /// <summary>
        /// 当前用户重置密码
        /// </summary>
        [HttpPost("reset-password")]
        public virtual Task ResetPasswordAsync([FromBody] ResetPasswordInput input)
        {
            return _accountAppService.ResetPasswordAsync(input);
        }

        /// <summary>
        /// 获取密码加密密钥
        /// </summary>
        [HttpGet("encrypt-key")]
        public virtual Task<string> GetEncryptKeyAsync()
        {
            return _accountAppService.GetEncryptKeyAsync();
        }
    }
}
