using System;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain;
using Volo.Abp.EventBus;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Timing;

namespace JiaCeMonitorSystem
{
    /// <summary>
    /// 领域层模块，配置多租户、审计与系统时钟
    /// </summary>
    [DependsOn(
        typeof(JiaCeMonitorSystemDomainSharedModule),
        typeof(AbpDddDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpPermissionManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpAuditingModule),
        typeof(AbpDataModule),
        typeof(AbpEventBusModule),
        typeof(AbpTimingModule)
    )]
    public class JiaCeMonitorSystemDomainModule : AbpModule
    {
        /// <inheritdoc />
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabled = true;
                options.IsEnabledForGetRequests = false;
                options.ApplicationName = "JiaCeMonitorSystem";
            });

            Configure<AbpClockOptions>(options =>
            {
                options.Kind = DateTimeKind.Local;
            });
        }
    }

    /// <summary>
    /// 多租户配置常量
    /// </summary>
    public static class MultiTenancyConsts
    {
        /// <summary>
        /// 是否启用多租户
        /// </summary>
        public const bool IsEnabled = true;
    }
}
