using System;
using Microsoft.Extensions.DependencyInjection;
using JiaCeMonitorSystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace JiaCeMonitorSystem.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(JiaCeMonitorSystemEntityFrameworkCoreModule),
        typeof(JiaCeMonitorSystemApplicationContractsModule)
    )]
    public class JiaCeMonitorSystemDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<DbMigrationService>();
        }
    }
}
