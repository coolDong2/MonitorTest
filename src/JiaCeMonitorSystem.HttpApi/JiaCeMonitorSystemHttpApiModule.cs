using System;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.UI.Navigation;
using Volo.Abp.PermissionManagement.HttpApi;
namespace JiaCeMonitorSystem
{
    /// <summary>
    /// HTTP API模块，配置传统控制器与本地化资源
    /// </summary>
    [DependsOn(
        typeof(JiaCeMonitorSystemApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(AbpSettingManagementHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpAccountHttpApiModule)
    )]
    public class JiaCeMonitorSystemHttpApiModule : AbpModule
    {
        /// <inheritdoc />
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                // 禁用自动扫描生成控制器，改为在 Controllers 文件夹中手动定义每个控制器
                // 这样每个应用服务都有独立的控制器文件，便于自定义路由、权限和过滤器
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<JiaCeMonitorSystemResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
