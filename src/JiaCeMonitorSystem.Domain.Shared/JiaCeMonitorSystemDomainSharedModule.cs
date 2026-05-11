using System;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.FeatureManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Identity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace JiaCeMonitorSystem
{
    [DependsOn(
        typeof(AbpMultiTenancyModule),
        typeof(AbpTenantManagementDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule)
    )]
    public class JiaCeMonitorSystemDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<JiaCeMonitorSystemDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<JiaCeMonitorSystemResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/JiaCeMonitorSystem");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("JiaCeMonitorSystem", typeof(JiaCeMonitorSystemResource));
            });
        }
    }

    [LocalizationResourceName("JiaCeMonitorSystem")]
    public class JiaCeMonitorSystemResource
    {
    }
}
