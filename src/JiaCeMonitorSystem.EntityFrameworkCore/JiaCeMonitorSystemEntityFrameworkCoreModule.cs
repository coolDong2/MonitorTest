using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace JiaCeMonitorSystem.EntityFrameworkCore
{
    [DependsOn(
        typeof(JiaCeMonitorSystemDomainModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpOpenIddictEntityFrameworkCoreModule)
    )]
    public class JiaCeMonitorSystemEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<JiaCeMonitorSystemDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseNpgsql();
            });

            // 配置 SettingManagementDbContext 使用 PostgreSQL，避免独立 DbContext 迁移缺失
            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure<SettingManagementDbContext>(ctx =>
                {
                    ctx.UseNpgsql();
                });
            });
        }
    }
}
