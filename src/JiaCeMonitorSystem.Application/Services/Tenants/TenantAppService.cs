using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.Dtos.Tenants;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using TenantDto = JiaCeMonitorSystem.Dtos.Tenants.TenantDto;
using TenantCreateDto = JiaCeMonitorSystem.Dtos.Tenants.TenantCreateDto;

namespace JiaCeMonitorSystem.Services.Tenants
{
    /// <summary>
    /// 租户管理应用服务（仅Host端可用）
    /// <para>【已弃用】请使用 <see cref="ITenantConfigurationAppService"/> 替代，当前服务仅保留做兼容代理。</para>
    /// </summary>
    [Obsolete("请使用 TenantConfigurationAppService 替代 TenantAppService，接口路由 /api/app/tenant-configuration")]
    [Authorize(Permissions.Permissions.Tenants_Default)]
    public class TenantAppService : ApplicationService, JiaCeMonitorSystem.Interfaces.ITenantAppService
    {
        private readonly ITenantManager _tenantManager;
        private readonly ITenantRepository _tenantRepository;
        private readonly IdentityUserManager _userManager;
        private readonly IDataSeeder _dataSeeder;
        private readonly ICurrentTenant _currentTenant;
        private readonly ITenantConfigurationAppService _tenantConfigurationAppService;

        public TenantAppService(
            ITenantManager tenantManager,
            ITenantRepository tenantRepository,
            IdentityUserManager userManager,
            IDataSeeder dataSeeder,
            ICurrentTenant currentTenant,
            ITenantConfigurationAppService tenantConfigurationAppService)
        {
            _tenantManager = tenantManager;
            _tenantRepository = tenantRepository;
            _userManager = userManager;
            _dataSeeder = dataSeeder;
            _currentTenant = currentTenant;
            _tenantConfigurationAppService = tenantConfigurationAppService;
        }

        /// <summary>
        /// 创建租户并初始化管理员账号
        /// <para>【代理实现】内部已统一调用 <see cref="TenantConfigurationAppService.CreateAsync"/></para>
        /// </summary>
        [Authorize(Permissions.Permissions.Tenants_Create)]
        public async Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            // 统一代理到 TenantConfigurationAppService，确保所有租户创建都走 SaaS 配置流程
            using (_currentTenant.Change(null))
            {
                var configDto = await _tenantConfigurationAppService.CreateAsync(new CreateTenantWithConfigDto
                {
                    Name = input.TenantName,
                    UnitCode = input.AdminAccount, // 兼容旧接口：使用管理员账号作为单位编码
                    AdminEmail = input.AdminEmail,
                    AdminPassword = input.AdminPassword,
                    ExpireDate = input.ExpireDate,
                    UseIndependentDatabase = !string.IsNullOrWhiteSpace(input.ConnectionString),
                    GrantedModuleIds = new List<Guid>() // 旧接口默认不授予模块，由前端单独配置
                });

                // 如果提供了独立数据库连接字符串，额外写入 ABP 租户连接字符串表（兼容旧行为）
                if (!string.IsNullOrWhiteSpace(input.ConnectionString))
                {
                    var tenant = await _tenantRepository.GetAsync(configDto.TenantId);
                    tenant.SetConnectionString("Default", input.ConnectionString);
                    await _tenantRepository.UpdateAsync(tenant);
                }

                return ObjectMapper.Map<Tenant, TenantDto>(await _tenantRepository.GetAsync(configDto.TenantId));
            }
        }

        /// <summary>
        /// 获取租户列表
        /// </summary>
        public async Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantListInput input)
        {
            using (_currentTenant.Change(null))
            {
                var tenants = await _tenantRepository.GetListAsync(
                    input.Sorting,
                    input.MaxResultCount,
                    input.SkipCount);
                var totalCount = await _tenantRepository.GetCountAsync();

                return new PagedResultDto<TenantDto>(totalCount,
                    ObjectMapper.Map<List<Tenant>, List<TenantDto>>(tenants));
            }
        }

        /// <summary>
        /// 获取单个租户
        /// </summary>
        public async Task<TenantDto> GetAsync(Guid id)
        {
            using (_currentTenant.Change(null))
            {
                var tenant = await _tenantRepository.GetAsync(id);
                return ObjectMapper.Map<Tenant, TenantDto>(tenant);
            }
        }

        /// <summary>
        /// 更新租户连接字符串
        /// </summary>
        [Authorize(Permissions.Permissions.Tenants_ManageConnectionString)]
        public async Task UpdateConnectionStringAsync(Guid id, string? connectionString)
        {
            using (_currentTenant.Change(null))
            {
                var tenant = await _tenantRepository.GetAsync(id);
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    tenant.SetConnectionString("Default", connectionString);
                }
                else
                {
                    tenant.RemoveConnectionString("Default");
                }
                await _tenantRepository.UpdateAsync(tenant);
            }
        }

        /// <summary>
        /// 删除租户
        /// </summary>
        [Authorize(Permissions.Permissions.Tenants_Delete)]
        public async Task DeleteAsync(Guid id)
        {
            using (_currentTenant.Change(null))
            {
                await _tenantRepository.DeleteAsync(id);
            }
        }
    }
}
