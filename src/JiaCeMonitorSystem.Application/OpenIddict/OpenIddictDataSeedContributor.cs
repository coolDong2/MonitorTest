using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace JiaCeMonitorSystem.Seeds
{
    /// <summary>
    /// OpenIddict 数据种子，注册默认应用与范围
    /// </summary>
    public class OpenIddictDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<OpenIddictDataSeedContributor> _logger;
        private readonly IOpenIddictApplicationManager _applicationManager;
        private readonly IOpenIddictScopeManager _scopeManager;

        public OpenIddictDataSeedContributor(
            IConfiguration configuration,
            ILogger<OpenIddictDataSeedContributor> logger,
            IOpenIddictApplicationManager applicationManager,
            IOpenIddictScopeManager scopeManager)
        {
            _configuration = configuration;
            _logger = logger;
            _applicationManager = applicationManager;
            _scopeManager = scopeManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedScopesAsync();
            await SeedApplicationsAsync();
        }

        private async Task SeedScopesAsync()
        {
            var scopeName = "JiaCeMonitorSystem";
            if (await _scopeManager.FindByNameAsync(scopeName) == null)
            {
                await _scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = scopeName,
                    DisplayName = "嘉测监测系统 API",
                    Resources =
                    {
                        scopeName
                    }
                });
                _logger.LogInformation("Created OpenIddict scope: {ScopeName}", scopeName);
            }
        }

        private async Task SeedApplicationsAsync()
        {
            var commonScopes = new List<string>
            {
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.OfflineAccess,
                "JiaCeMonitorSystem"
            };

            // Swagger UI 客户端（Authorization Code + PKCE）
            await CreateApplicationAsync(
                name: "JiaCeMonitorSystem_Swagger", 
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "Swagger UI",
                secret: null,
                grantTypes: new List<string>
                {
                    OpenIddictConstants.GrantTypes.AuthorizationCode
                },
                scopes: commonScopes,
                redirectUri: _configuration["AuthServer:SwaggerClientRedirectUri"] ?? "/swagger/oauth2-redirect.html",
                clientUri: null,
                logoutRedirectUri: null
            );

            // 前端应用客户端（Password + Refresh Token）
            await CreateApplicationAsync(
                name: "JiaCeMonitorSystem_App",
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "嘉测监测系统前端应用",
                secret: null,
                grantTypes:
                [
                    OpenIddictConstants.GrantTypes.Password,
                    OpenIddictConstants.GrantTypes.RefreshToken
                ],
                scopes: commonScopes,
                redirectUri: null,
                clientUri: null,
                logoutRedirectUri: null
            );

            // 后端服务间调用客户端（Client Credentials）
            await CreateApplicationAsync(
                name: "JiaCeMonitorSystem_Service",
                type: OpenIddictConstants.ClientTypes.Confidential,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "嘉测监测系统服务客户端",
                secret: _configuration["AuthServer:ServiceClientSecret"] ?? "1q2w3e*",
                grantTypes: new List<string>
                {
                    OpenIddictConstants.GrantTypes.ClientCredentials
                },
                scopes: commonScopes,
                redirectUri: null,
                clientUri: null,
                logoutRedirectUri: null
            );
        }

        private async Task CreateApplicationAsync(
            string name,
            string type,
            string consentType,
            string displayName,
            string? secret,
            List<string> grantTypes,
            List<string> scopes,
            string? redirectUri,
            string? clientUri,
            string? logoutRedirectUri)
        {
            if (!string.IsNullOrEmpty(name) && await _applicationManager.FindByClientIdAsync(name) != null)
            {
                _logger.LogDebug("OpenIddict application already exists: {Name}", name);
                return;
            }

            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = name,
                ClientType = type,
                ConsentType = consentType,
                DisplayName = displayName
            };

            if (!string.IsNullOrEmpty(secret))
            {
                descriptor.ClientSecret = secret;
            }

            foreach (var grantType in grantTypes)
            {
                descriptor.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.GrantType + grantType);
            }

            foreach (var scope in scopes)
            {
                descriptor.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
            }

            if (!string.IsNullOrEmpty(redirectUri))
            {
                descriptor.RedirectUris.Add(new Uri(redirectUri, UriKind.RelativeOrAbsolute));
            }

            if (!string.IsNullOrEmpty(logoutRedirectUri))
            {
                descriptor.PostLogoutRedirectUris.Add(new Uri(logoutRedirectUri, UriKind.RelativeOrAbsolute));
            }

            await _applicationManager.CreateAsync(descriptor);
            _logger.LogInformation("Created OpenIddict application: {Name}", name);
        }
    }
}
