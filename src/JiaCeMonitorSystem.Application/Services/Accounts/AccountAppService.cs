using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Accounts;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace JiaCeMonitorSystem.Services.Accounts
{
    /// <summary>
    /// 账号应用服务
    /// </summary>
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        private readonly IdentityUserManager _userManager;
        private readonly ICurrentTenant _currentTenant;
        private readonly IConfiguration _configuration;

        public AccountAppService(
            IdentityUserManager userManager,
            ICurrentTenant currentTenant,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _currentTenant = currentTenant;
            _configuration = configuration;
        }

        /// <summary>
        /// 登录验证（兼容端点，推荐使用 /connect/token 标准 OAuth2 端点）
        /// </summary>
        /// <remarks>
        /// 标准 OAuth2 Token 端点：POST /connect/token
        /// grant_type=password&amp;username={账号}&amp;password={密码}&amp;client_id=JiaCeMonitorSystem_App&amp;scope=JiaCeMonitorSystem openid profile offline_access
        /// </remarks>
        public async Task<LoginOutputDto> LoginAsync(LoginInputDto input)
        {
            // 多租户识别
            if (!string.IsNullOrWhiteSpace(input.TenantName))
            {
                if (Guid.TryParse(input.TenantName, out var tenantId))
                {
                    _currentTenant.Change(tenantId);
                }
            }

            var user = await _userManager.FindByNameAsync(input.Account);
            if (user == null)
            {
                throw new UserFriendlyException("用户名或密码错误");
            }

            // 解密密码（兼容明文传输）
            var encryptKey = _configuration["Password:EncryptKey"] ?? "JiaCeMonitorSystem_DefaultEncryptKey_2026";
            var decryptedPassword = PasswordEncryptor.Decrypt(input.Password, encryptKey);

            var passwordValid = await _userManager.CheckPasswordAsync(user, decryptedPassword); 
            if (!passwordValid)
            {
                throw new UserFriendlyException("用户名或密码错误");
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                throw new UserFriendlyException("用户已被锁定，请稍后重试");
            }

            // 获取用户角色
            var roles = await _userManager.GetRolesAsync(user);
            var isAdmin = roles.Contains("admin");

            // 签发Token（简化实现，生产环境建议通过OpenIddict/IdentityServer端点签发JWT）
            var token = await GenerateTokenAsync(user);

            return new LoginOutputDto
            {
                UserId = user.Id,
                LoginToken = token,
                RefreshToken = Guid.NewGuid().ToString("N"),
                ExpiresIn = 7200,
                TenantId = user.TenantId,
                IsAdmin = isAdmin,
                DisplayName = user.Name ?? user.UserName ?? input.Account,
                Permissions = new List<string>()
            };
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        [Authorize]
        public async Task<CurrentUserDto> GetCurrentUserAsync()
        {
            if (!CurrentUser.IsAuthenticated || CurrentUser.Id == null)
            {
                throw new UserFriendlyException("用户未登录");
            }

            var user = await _userManager.FindByIdAsync(CurrentUser.Id.Value.ToString());
            if (user == null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new CurrentUserDto
            {
                Id = user.Id,
                Account = user.UserName ?? string.Empty,
                RealName = user.Name,
                NickName = user.Surname,
                DisplayName = user.Name ?? user.UserName ?? string.Empty,
                HeadIcon = null,
                MobilePhone = user.PhoneNumber,
                Email = user.Email,
                TenantId = user.TenantId,
                IsAdmin = roles.Contains("admin"),
                Roles = roles.ToList(),
                Permissions = new List<string>()
            };
        }

        /// <summary>
        /// 当前用户重置密码
        /// </summary>
        [Authorize]
        public async Task ResetPasswordAsync(ResetPasswordInput input)
        {
            if (!CurrentUser.IsAuthenticated || CurrentUser.Id == null)
            {
                throw new UserFriendlyException("用户未登录");
            }

            var user = await _userManager.FindByIdAsync(CurrentUser.Id.Value.ToString());
            if (user == null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, input.OldPassword);
            if (!passwordValid)
            {
                throw new UserFriendlyException("旧密码不正确");
            }

            var result = await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException(result.Errors.First().Description);
            }
        }

        /// <summary>
        /// 获取密码加密密钥（供前端加密传输使用）
        /// </summary>
        [AllowAnonymous]
        public Task<string> GetEncryptKeyAsync()
        {
            var key = _configuration["Password:EncryptKey"] ?? "JiaCeMonitorSystem_DefaultEncryptKey_2026";
            return Task.FromResult(key);
        }

        /// <summary>
        /// 生成标准 JWT Token（开发环境使用，生产环境推荐使用 /connect/token 端点）
        /// </summary>
        [Obsolete("请优先使用 OpenIddict /connect/token 端点获取标准 JWT Token")]
        private async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(AbpClaimTypes.UserId, user.Id.ToString()),
                new Claim(AbpClaimTypes.UserName, user.UserName ?? string.Empty),
                new Claim(AbpClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(AbpClaimTypes.TenantId, user.TenantId?.ToString() ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(AbpClaimTypes.Role, role)));

            // 从配置读取密钥，默认使用开发密钥
            var keyString = _configuration["Jwt:SecurityKey"] ?? "JiaCeMonitorSystem_DevSecretKey_2026_!@#";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "JiaCeMonitorSystem",
                audience: _configuration["Jwt:Audience"] ?? "JiaCeMonitorSystem",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
