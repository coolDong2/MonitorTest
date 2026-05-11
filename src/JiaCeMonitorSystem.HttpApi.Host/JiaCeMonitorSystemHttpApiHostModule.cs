using Hangfire;
using Hangfire.PostgreSql;
using JiaCeMonitorSystem.EntityFrameworkCore;
using JiaCeMonitorSystem.Swagger;
using Medallion.Threading;
using Medallion.Threading.Postgres;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.Libs;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.WildcardDomains;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace JiaCeMonitorSystem
{
    [DependsOn(
        typeof(JiaCeMonitorSystemHttpApiModule),
        typeof(JiaCeMonitorSystemApplicationModule),
        typeof(JiaCeMonitorSystemEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpDistributedLockingModule),
        typeof(AbpBackgroundJobsHangfireModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(AbpAccountWebModule),
		  typeof(AbpEventBusRabbitMqModule)
    )]
    public class JiaCeMonitorSystemHttpApiHostModule : AbpModule
    {
		
		 private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
 {
     var connectionString = configuration.GetConnectionString("Default");

     context.Services.AddHangfire(config =>
     {
         // 使用新的重载，通过配置 PostgreSqlBootstrapperOptions 来指定连接字符串
         config.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString));
     });

     // 可选：添加 Hangfire 服务器（ABP 会自动启动，但显式注册也可以）
     context.Services.AddHangfireServer();
 }
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var wildcardDomains = configuration.GetSection("App:WildcardDomains").Get<List<string>>();
            
            if (wildcardDomains != null && wildcardDomains.Any())
            {
                PreConfigure<AbpOpenIddictWildcardDomainOptions>(options =>
                {
                    options.EnableWildcardDomainSupport = true;
                    foreach (var domain in wildcardDomains)
                    {
                        options.WildcardDomainsFormat.Add(domain);
                    }
                });
            }

            PreConfigure<OpenIddictBuilder>(builder =>
            {
                // Authorization Server 配置（签发 Token）
                builder.AddServer(options =>
                {
                    options.SetTokenEndpointUris("/connect/token");
                    options.SetAuthorizationEndpointUris("/connect/authorize");

                    options.AllowPasswordFlow()
                           .AllowRefreshTokenFlow()
                           .AllowClientCredentialsFlow();

                    options.RegisterScopes(
                        OpenIddictConstants.Scopes.OpenId,
                        OpenIddictConstants.Scopes.Profile,
                        OpenIddictConstants.Scopes.OfflineAccess,
                        "JiaCeMonitorSystem");

                    options.UseAspNetCore()
                           .EnableTokenEndpointPassthrough()
                           .EnableAuthorizationEndpointPassthrough()
                           .DisableTransportSecurityRequirement();

                    // 开发环境使用临时签名/加密密钥
                    // 生产环境应替换为持久化证书（AddSigningCertificate/AddEncryptionCertificate）
                    options.AddEphemeralSigningKey();
                    options.AddEphemeralEncryptionKey();
                });

                // Resource Server 配置（验证 Token）
                builder.AddValidation(options =>
                {
                    options.AddAudiences("JiaCeMonitorSystem");
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureMultiTenancy();
            ConfigureVirtualFileSystem(context);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context, configuration);
            ConfigureCache(configuration);
            ConfigureDistributedLocking(context, configuration);
            ConfigureAntiForgery();
            ConfigureDataProtection(context, configuration, hostingEnvironment);
            ConfigureLocalization();
            ConfigureHangfire(context, context.Services.GetConfiguration());
            ConfigureHealthChecks(context, configuration);
            ConfigureAbpMvcLibs();
        }
        
        private void ConfigureMultiTenancy()
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            Configure<AbpTenantResolveOptions>(options =>
            {
                options.AddDomainTenantResolver("{0}.localhost:5000");
            });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<JiaCeMonitorSystemDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath, 
                            $"..{Path.DirectorySeparatorChar}JiaCeMonitorSystem.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<JiaCeMonitorSystemHttpApiModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath, 
                            $"..{Path.DirectorySeparatorChar}JiaCeMonitorSystem.HttpApi"));
                });
            }
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]?
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.Trim().RemovePostFix("/"))
                                .ToArray() ?? Array.Empty<string>()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        private void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"]!,
                new Dictionary<string, string>
                {
                    {"JiaCeMonitorSystem", "JiaCeMonitorSystem API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "嘉测监测系统 API",
                        Version = "v1",
                        Description = "嘉测监测工程管理系统 RESTful API 文档"
                    });
                    options.DocInclusionPredicate((docName, description) =>
                    {
                        // 只显示本项目手动定义的控制器接口，过滤掉 ABP 内置模块自动生成的接口
                        if (description.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controllerAction)
                        {
                            var controllerType = controllerAction.ControllerTypeInfo;
                            return controllerType.Namespace?.StartsWith("JiaCeMonitorSystem") == true;
                        }
                        // 保留非控制器端点（如 OpenIddict /connect/token 等）
                        return true;
                    });
                    options.CustomSchemaIds(type => type.FullName);

                    // Bearer Token 认证（用于简化的登录接口）
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "请输入 `Bearer {token}` 进行认证",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                    // XML 注释文件：加载所有相关程序集的 XML 文档
                    var xmlFilePaths = new List<string>();
                    var assemblies = new[]
                    {
                        typeof(JiaCeMonitorSystemHttpApiHostModule).Assembly,
                        typeof(JiaCeMonitorSystemHttpApiModule).Assembly,
                        typeof(JiaCeMonitorSystemApplicationContractsModule).Assembly,
                        typeof(JiaCeMonitorSystemApplicationModule).Assembly,
                        typeof(JiaCeMonitorSystemDomainModule).Assembly,
                        typeof(JiaCeMonitorSystemDomainSharedModule).Assembly,
                        typeof(JiaCeMonitorSystemEntityFrameworkCoreModule).Assembly
                    }.Distinct();

                    foreach (var assembly in assemblies)
                    {
                        var xmlFile = Path.Combine(Path.GetDirectoryName(assembly.Location)!, $"{assembly.GetName().Name}.xml");
                        if (File.Exists(xmlFile))
                        {
                            xmlFilePaths.Add(xmlFile);
                        }
                    }

                    // 注册自定义 XML 注释过滤器（支持 ABP 传统控制器从接口读取注释）
                    options.OperationFilter<SwaggerXmlCommentsOperationFilter>(xmlFilePaths);
                    options.SchemaFilter<SwaggerXmlCommentsSchemaFilter>(xmlFilePaths);
                });
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "JiaCeMonitorSystem:";
            });
        }

        private void ConfigureDistributedLocking(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddSingleton<IDistributedLockProvider>(sp =>
            {
                var connectionString = configuration.GetConnectionString("Default");
                return new PostgresDistributedSynchronizationProvider(connectionString!);
            });
        }

        private void ConfigureAntiForgery()
        {
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.TokenCookie.Expiration = TimeSpan.FromDays(365);
                options.AutoValidate = false;
            });
        }

        private void ConfigureDataProtection(ServiceConfigurationContext context, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("JiaCeMonitorSystem");
            
            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
                dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "JiaCeMonitorSystem-Protection-Keys");
            }
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
            });
        }

        private void ConfigureHealthChecks(ServiceConfigurationContext context, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
            context.Services.AddHealthChecks()
                .AddNpgSql(connectionString!, name: "postgresql", tags: new[] { "db" })
                .AddCheck("redis", () => HealthCheckResult.Healthy(), tags: new[] { "cache" });
        }
        private void ConfigureAbpMvcLibs() 
        {
            // 禁用 ABP 客户端库检查（前后端分离项目不需要）
            Configure<AbpMvcLibsOptions>(options =>
            {
                options.CheckLibs = false;
            });
        }


        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseAbpRequestLocalization();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAbpOpenIddictValidation();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseAuthorization();

            // Swagger
            app.UseSwagger();
            app.UseAbpSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "嘉测监测系统 API");
                var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
                c.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                c.OAuthAppName("嘉测监测系统 Swagger");
                c.OAuthScopes("JiaCeMonitorSystem", "openid", "profile", "offline_access");
                c.OAuthUsePkce();
            });

            // 健康检查端点
            app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new
                        {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            exception = e.Value.Exception?.Message
                        })
                    }, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                    await context.Response.WriteAsync(result);
                }
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
