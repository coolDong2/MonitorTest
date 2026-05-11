using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace JiaCeMonitorSystem.EntityFrameworkCore
{
    public static class JiaCeMonitorSystemEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                ObjectExtensionManager.Instance
                    .MapEfCoreProperty<IdentityUser, string>(
                        "SocialSecurityNumber",
                        (entityBuilder, propertyBuilder) =>
                        {
                            propertyBuilder.HasMaxLength(32);
                        }
                    );
            });
        }
    }
}
