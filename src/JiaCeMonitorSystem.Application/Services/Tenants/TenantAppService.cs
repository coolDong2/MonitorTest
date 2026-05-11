using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    /// </summary>
    [Authorize(Permissions.Permissions.Tenants_Default)]
    public class TenantAppService : ApplicationService, JiaCeMonitorSystem.Interfaces.ITenantAppService
    {
        private readonly ITenantManager _tenantManager;
        private readonly ITenantRepository _tenantRepository;
        private readonly IdentityUserManager _userManager;
        private readonly IDataSeeder _dataSeeder;
        private readonly ICurrentTenant _currentTenant;

        public TenantAppService(
            ITenantManager tenantManager,
            ITenantRepository tenantRepository,
            IdentityUserManager userManager,
            IDataSeeder dataSeeder,
            ICurrentTenant currentTenant)
        {
            _tenantManager = tenantManager;
            _tenantRepository = tenantRepository;
            _userManager = userManager;
            _dataSeeder = dataSeeder;
            _currentTenant = currentTenant;
        }

        /// <summary>
        /// 创建租户并初始化管理员账号
        /// </summary>
        [Authorize(Permissions.Permissions.Tenants_Create)]
        public async Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            // 使用DisableMultiTenancy在Host端创建租户
            using (_currentTenant.Change(null))
            {
                var tenant = await _tenantManager.CreateAsync(input.TenantName);
                if (!string.IsNullOrWhiteSpace(input.ConnectionString))
                {
                    tenant.SetConnectionString("Default", input.ConnectionString);
                }
                await _tenantRepository.InsertAsync(tenant);

                // 切换到新租户上下文，创建管理员账号
                using (_currentTenant.Change(tenant.Id))
                {
                    await _dataSeeder.SeedAsync(new DataSeedContext(tenant.Id));

                    var adminUser = new IdentityUser(
                        GuidGenerator.Create(),
                        input.AdminAccount,
                        input.AdminEmail ?? $"{input.AdminAccount}@jcmonitor.com");

                    adminUser.Name = "管理员";
                    var result = await _userManager.CreateAsync(adminUser, input.AdminPassword);
                    if (!result.Succeeded)
                    {
                        throw new UserFriendlyException($"创建管理员账号失败：{string.Join(", ", result.Errors)}");
                    }

                    // 分配管理员角色
                    await _userManager.AddToRoleAsync(adminUser, "admin");
                }

                return ObjectMapper.Map<Tenant, TenantDto>(tenant);
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
