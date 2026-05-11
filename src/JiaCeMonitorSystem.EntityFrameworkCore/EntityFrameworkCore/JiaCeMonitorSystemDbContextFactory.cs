using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JiaCeMonitorSystem.EntityFrameworkCore
{
    public class JiaCeMonitorSystemDbContextFactory : IDesignTimeDbContextFactory<JiaCeMonitorSystemDbContext>
    {
        public JiaCeMonitorSystemDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<JiaCeMonitorSystemDbContext>();
            var connectionString = configuration.GetConnectionString("Default");

            builder.UseNpgsql(connectionString, b =>
            {
                b.MigrationsAssembly("JiaCeMonitorSystem.EntityFrameworkCore");
            });

            return new JiaCeMonitorSystemDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var hostAppSettings = Path.Combine(
                Directory.GetCurrentDirectory(),
                "..", "..", "..", "src", "JiaCeMonitorSystem.HttpApi.Host", "appsettings.json"
            );

            if (File.Exists(hostAppSettings))
            {
                return new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(hostAppSettings)!)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
            }

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}
