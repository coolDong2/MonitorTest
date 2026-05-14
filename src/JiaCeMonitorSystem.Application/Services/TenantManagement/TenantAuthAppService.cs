using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.TenantManagement;
using TenantConfigurationEntity = JiaCeMonitorSystem.TenantManagement.TenantConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace JiaCeMonitorSystem.Application.Services.TenantManagement
{
    /// <summary>
    /// 租户认证应用服务，实现租户用户的双轨登录
    /// </summary>
    public class TenantAuthAppService : ApplicationService, ITenantAuthAppService
    {
        private readonly IRepository<TenantConfigurationEntity, Guid> _configRepo;
        private readonly IRepository<TenantUserExtension, Guid> _userExtRepo;
        private readonly IdentityUserManager _userManager;
        private readonly ITenantModuleGrantAppService _moduleGrantAppService;
        private readonly IConfiguration _configuration;
        private readonly ICurrentTenant _currentTenant;

        public TenantAuthAppService(
            IRepository<TenantConfigurationEntity, Guid> configRepo,
            IRepository<TenantUserExtension, Guid> userExtRepo,
            IdentityUserManager userManager,
            ITenantModuleGrantAppService moduleGrantAppService,
            IConfiguration configuration,
            ICurrentTenant currentTenant)
        {
            _configRepo = configRepo;
            _userExtRepo = userExtRepo;
            _userManager = userManager;
            _moduleGrantAppService = moduleGrantAppService;
            _configuration = configuration;
            _currentTenant = currentTenant;
        }

        public async Task<TenantLoginResultDto> TenantLoginAsync(TenantUserLoginDto input)
        {
            // 1. 根据 UnitCode 查找租户配置
            var config = await _configRepo.FirstOrDefaultAsync(x => x.UnitCode == input.UnitCode);
            if (config == null)
            {
                throw new BusinessException("TENANT_NOT_FOUND", $"单位编码 '{input.UnitCode}' 不存在");
            }

            // 2. 检查租户状态
            if (config.Status == TenantStatus.Expired)
            {
                throw new BusinessException("TENANT_EXPIRED", $"租户已于 {config.ExpireDate} 到期");
            }
            if (config.Status == TenantStatus.Suspended)
            {
                throw new BusinessException("TENANT_SUSPENDED", "租户已被暂停使用");
            }

            // 3. 切换租户上下文
            using (_currentTenant.Change(config.TenantId))
            {
                // 4. 查找并验证用户
                var user = await _userManager.FindByNameAsync(input.UserName);
                if (user == null)
                {
                    throw new BusinessException("INVALID_CREDENTIALS", "用户名或密码错误");
                }

                var valid = await _userManager.CheckPasswordAsync(user, input.Password);
                if (!valid)
                {
                    throw new BusinessException("INVALID_CREDENTIALS", "用户名或密码错误");
                }

                // 5. 获取用户扩展信息
                var userExt = await _userExtRepo.FirstOrDefaultAsync(x => x.UserId == user.Id);
                var userType = userExt?.UserType ?? UserType.TenantUser;

                // 6. 获取权限和菜单
                var roles = await _userManager.GetRolesAsync(user);
                var permissions = new List<string>(roles);
                var menus = await _moduleGrantAppService.GetGrantedMenusAsync(config.TenantId.Value);

                // 7. 生成 Token（含租户信息）
                var token = await CreateTenantTokenAsync(user, config, userType);
                var refreshToken = Guid.NewGuid().ToString("N");

                return new TenantLoginResultDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    TenantId = config.TenantId.Value,
                    TenantName = config.UnitCode ?? string.Empty,
                    UnitCode = config.UnitCode ?? string.Empty,
                    UserType = userType,
                    Permissions = permissions,
                    Menus = menus,
                    ExpireDate = config.ExpireDate
                };
            }
        }

        public async Task<TenantLoginResultDto> RefreshTokenAsync(string refreshToken)
        {
            // 简化实现：刷新令牌逻辑（生产环境应校验 refreshToken 有效性并关联用户）
            throw new BusinessException("NOT_IMPLEMENTED", "刷新令牌功能待实现");
        }

        /// <summary>
        /// 创建包含租户信息的 JWT Token
        /// </summary>
        private async Task<string> CreateTenantTokenAsync(IdentityUser user, TenantConfigurationEntity config, UserType userType)
        {
            var claims = new List<Claim>
            {
                new Claim(AbpClaimTypes.UserId, user.Id.ToString()),
                new Claim(AbpClaimTypes.UserName, user.UserName ?? string.Empty),
                new Claim(AbpClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(AbpClaimTypes.TenantId, config.TenantId?.ToString() ?? string.Empty),
                new Claim("tenant_id", config.TenantId?.ToString() ?? string.Empty),
                new Claim("unit_code", config.UnitCode ?? string.Empty),
                new Claim("user_type", ((int)userType).ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(AbpClaimTypes.Role, role)));

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
