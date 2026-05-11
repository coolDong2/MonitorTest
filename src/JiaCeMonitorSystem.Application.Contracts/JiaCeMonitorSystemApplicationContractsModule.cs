using System;
using Volo.Abp.Application;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;

namespace JiaCeMonitorSystem
{
    /// <summary>
    /// 应用契约层模块，定义DTO、接口与权限常量
    /// </summary>
    [DependsOn(
        typeof(JiaCeMonitorSystemDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule)
    )]
    public class JiaCeMonitorSystemApplicationContractsModule : AbpModule
    {
        /// <inheritdoc />
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<JiaCeMonitorSystemApplicationContractsModule>();
            });
        }
    }
}
