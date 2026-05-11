using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace JiaCeMonitorSystem
{
    [DependsOn(
        typeof(JiaCeMonitorSystemDomainModule),
        typeof(JiaCeMonitorSystemApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpSettingManagementApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpAutoMapperModule)
    )]
    public class JiaCeMonitorSystemApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<JiaCeMonitorSystemApplicationModule>();
            });
        }
    }
}
