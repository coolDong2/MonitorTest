using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Encryption;

using TenantConfigurationEntity = JiaCeMonitorSystem.TenantManagement.TenantConfiguration;

namespace JiaCeMonitorSystem.EntityFrameworkCore.TenantManagement
{
    /// <summary>
    /// 租户连接串解析器，支持自定义 TenantConfiguration 中的独立数据库配置
    /// </summary>
    public class JiaCeTenantConnectionStringResolver : MultiTenantConnectionStringResolver
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOptionsMonitor<AbpDbConnectionOptions> _options;
        private readonly IServiceProvider _serviceProvider;

        public JiaCeTenantConnectionStringResolver(
            IOptionsMonitor<AbpDbConnectionOptions> options,
            ICurrentTenant currentTenant,
            IServiceProvider serviceProvider)
            : base(options, currentTenant, serviceProvider)
        {
            _currentTenant = currentTenant;
            _options = options;
            _serviceProvider = serviceProvider;
        }

        public override async Task<string> ResolveAsync(string? connectionStringName = null)
        {
            // 先调用基类解析（处理 ABP 内置的租户连接串配置）
            var connectionString = await base.ResolveAsync(connectionStringName);

            // 只有在有租户上下文且解析结果为默认连接串时，才检查自定义配置
            if (_currentTenant.IsAvailable)
            {
                var defaultConnectionString = _options.CurrentValue.ConnectionStrings.Default;
                if (connectionString == defaultConnectionString)
                {
                    // 使用 scope 获取 Repository，避免循环依赖
                    using var scope = _serviceProvider.CreateScope();
                    var configRepo = scope.ServiceProvider.GetRequiredService<IRepository<TenantConfigurationEntity, Guid>>();
                    var encryptionService = scope.ServiceProvider.GetRequiredService<IStringEncryptionService>();

                    var config = await configRepo.FirstOrDefaultAsync(x => x.TenantId == _currentTenant.Id);
                    if (config != null && config.IsIndependentDatabase && !string.IsNullOrEmpty(config.IndependentConnectionString))
                    {
                        return encryptionService.Decrypt(config.IndependentConnectionString)
                            ?? throw new InvalidOperationException("解密后的连接串为空");
                    }
                }
            }

            return connectionString;
        }
    }
}
