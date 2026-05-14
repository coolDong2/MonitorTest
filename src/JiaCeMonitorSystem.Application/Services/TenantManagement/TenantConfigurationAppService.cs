using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.Dtos.TenantManagement;
using JiaCeMonitorSystem.TenantManagement;
using JiaCeMonitorSystem.TenantManagement.Events;
using TenantConfigurationEntity = JiaCeMonitorSystem.TenantManagement.TenantConfiguration;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Encryption;
using Volo.Abp.TenantManagement;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace JiaCeMonitorSystem.Application.Services.TenantManagement
{
    /// <summary>
    /// 租户配置应用服务
    /// </summary>
    public class TenantConfigurationAppService : ApplicationService, ITenantConfigurationAppService
    {
        private readonly ITenantManager _tenantManager;
        private readonly IRepository<TenantConfigurationEntity, Guid> _configRepo;
        private readonly IRepository<TenantModuleGrant, Guid> _moduleGrantRepo;
        private readonly ITenantDatabaseManager _tenantDatabaseManager;
        private readonly IStringEncryptionService _stringEncryptionService;
        private readonly IDistributedEventBus _eventBus;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly ITenantRepository _tenantRepository;

        public TenantConfigurationAppService(
            ITenantManager tenantManager,
            IRepository<TenantConfigurationEntity, Guid> configRepo,
            IRepository<TenantModuleGrant, Guid> moduleGrantRepo,
            ITenantDatabaseManager tenantDatabaseManager,
            IStringEncryptionService stringEncryptionService,
            IDistributedEventBus eventBus,
            IdentityUserManager identityUserManager,
            IBackgroundJobManager backgroundJobManager,
            ITenantRepository tenantRepository)
        {
            _tenantManager = tenantManager;
            _configRepo = configRepo;
            _moduleGrantRepo = moduleGrantRepo;
            _tenantDatabaseManager = tenantDatabaseManager;
            _stringEncryptionService = stringEncryptionService;
            _eventBus = eventBus;
            _identityUserManager = identityUserManager;
            _backgroundJobManager = backgroundJobManager;
            _tenantRepository = tenantRepository;
        }

        public async Task<TenantConfigurationDto> CreateAsync(CreateTenantWithConfigDto input)
        {
            // 1. 校验 UnitCode 唯一性
            var existing = await _configRepo.FirstOrDefaultAsync(x => x.UnitCode == input.UnitCode);
            if (existing != null)
            {
                throw new BusinessException("UNIT_CODE_EXISTS", $"单位编码 '{input.UnitCode}' 已存在");
            }

            // 2. 使用 ABP TenantManager 创建基础租户
            var tenant = await _tenantManager.CreateAsync(input.Name);

            // 3. 创建 TenantConfiguration
            var config = new TenantConfigurationEntity(
                GuidGenerator.Create(),
                tenant.Id,
                input.UnitCode)
            {
                IsIndependentDatabase = input.UseIndependentDatabase,
                ExpireDate = input.ExpireDate,
                Status = TenantStatus.Active,
                MaxUserCount = input.License?.MaxUserCount,
                MaxProjectCount = input.License?.MaxProjectCount,
                MaxPointCount = input.License?.MaxPointCount,
                MaxStorageBytes = input.License?.MaxStorageBytes
            };

            // 4. 如果独立数据库，创建数据库
            if (input.UseIndependentDatabase)
            {
                var connectionString = await _tenantDatabaseManager.CreateDatabaseAsync(tenant.Id, input.Name);
                config.IndependentConnectionString = _stringEncryptionService.Encrypt(connectionString);
            }

            await _configRepo.InsertAsync(config);

            // 5. 批量授权模块
            foreach (var moduleId in input.GrantedModuleIds)
            {
                await _moduleGrantRepo.InsertAsync(new TenantModuleGrant(
                    GuidGenerator.Create(),
                    tenant.Id,
                    moduleId,
                    true)
                {
                    GrantDate = Clock.Now
                });
            }

            // 6. 创建租户管理员账号
            if (!string.IsNullOrWhiteSpace(input.AdminEmail) && !string.IsNullOrWhiteSpace(input.AdminPassword))
            {
                using (CurrentTenant.Change(tenant.Id))
                {
                    var adminUser = new IdentityUser(
                        GuidGenerator.Create(),
                        input.AdminEmail,
                        input.AdminEmail,
                        tenant.Id)
                    {
                        Name = "管理员"
                    };

                    var result = await _identityUserManager.CreateAsync(adminUser, input.AdminPassword);
                    if (!result.Succeeded)
                    {
                        throw new BusinessException("TENANT_ADMIN_CREATE_FAILED",
                            $"创建租户管理员失败: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            // 7. 发布领域事件
            await _eventBus.PublishAsync(new TenantInitializedEvent
            {
                TenantId = tenant.Id,
                UnitCode = input.UnitCode,
                DatabaseType = input.UseIndependentDatabase ? TenantDatabaseType.Isolated : TenantDatabaseType.Shared
            });

            return ObjectMapper.Map<TenantConfigurationEntity, TenantConfigurationDto>(config);
        }

        public async Task<TenantConfigurationDto> GetConfigurationAsync(Guid tenantId)
        {
            var config = await _configRepo.FirstOrDefaultAsync(x => x.TenantId == tenantId)
                ?? throw new BusinessException("TENANT_CONFIG_NOT_FOUND", "租户配置不存在");

            return ObjectMapper.Map<TenantConfigurationEntity, TenantConfigurationDto>(config);
        }

        public async Task<TenantConfigurationDto> UpdateLicenseAsync(Guid tenantId, TenantLicenseDto input)
        {
            var config = await _configRepo.FirstOrDefaultAsync(x => x.TenantId == tenantId)
                ?? throw new BusinessException("TENANT_CONFIG_NOT_FOUND", "租户配置不存在");

            config.MaxUserCount = input.MaxUserCount;
            config.MaxProjectCount = input.MaxProjectCount;
            config.MaxPointCount = input.MaxPointCount;
            config.MaxStorageBytes = input.MaxStorageBytes;

            await _configRepo.UpdateAsync(config);
            return ObjectMapper.Map<TenantConfigurationEntity, TenantConfigurationDto>(config);
        }

        public async Task<PagedResultDto<TenantConfigurationDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _configRepo.GetQueryableAsync();
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            var list = await AsyncExecuter.ToListAsync(
                queryable.OrderBy(e => e.CreationTime)
                         .Skip(input.SkipCount)
                         .Take(input.MaxResultCount));

            return new PagedResultDto<TenantConfigurationDto>(
                totalCount,
                ObjectMapper.Map<List<TenantConfigurationEntity>, List<TenantConfigurationDto>>(list));
        }

        public async Task SwitchToIndependentDatabaseAsync(Guid tenantId)
        {
            var config = await _configRepo.FirstOrDefaultAsync(x => x.TenantId == tenantId);
            if (config == null)
                throw new BusinessException("TENANT_CONFIG_NOT_FOUND", "租户配置不存在");

            if (config.IsIndependentDatabase)
                throw new BusinessException("ALREADY_INDEPENDENT_DB", "该租户已经是独立数据库模式");

            var tenant = await _tenantRepository.GetAsync(tenantId);

            await _backgroundJobManager.EnqueueAsync(new TenantDatabaseSwitchArgs
            {
                TenantId = tenantId,
                TenantName = tenant.Name
            });
        }
    }
}
