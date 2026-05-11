using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Accounts;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 账号应用服务接口
    /// </summary>
    public interface IAccountAppService : IApplicationService
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        Task<LoginOutputDto> LoginAsync(LoginInputDto input);

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        Task<CurrentUserDto> GetCurrentUserAsync();

        /// <summary>
        /// 当前用户重置密码
        /// </summary>
        Task ResetPasswordAsync(ResetPasswordInput input);

        /// <summary>
        /// 获取密码加密密钥（供前端 AES 加密使用）
        /// </summary>
        Task<string> GetEncryptKeyAsync();
    }
}
