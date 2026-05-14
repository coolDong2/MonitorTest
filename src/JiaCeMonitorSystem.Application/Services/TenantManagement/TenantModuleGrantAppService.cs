using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.SystemModules;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Application.Services.TenantManagement
{
    /// <summary>
    /// 租户模块授权应用服务
    /// </summary>
    [Authorize]
    public class TenantModuleGrantAppService : ApplicationService, ITenantModuleGrantAppService
    {
        private readonly IRepository<TenantModuleGrant, Guid> _moduleGrantRepo;
        private readonly IRepository<SystemModule, Guid> _systemModuleRepo;

        public TenantModuleGrantAppService(
            IRepository<TenantModuleGrant, Guid> moduleGrantRepo,
            IRepository<SystemModule, Guid> systemModuleRepo)
        {
            _moduleGrantRepo = moduleGrantRepo;
            _systemModuleRepo = systemModuleRepo;
        }

        public async Task GrantModulesAsync(Guid tenantId, List<Guid> moduleIds)
        {
            var existingGrants = await _moduleGrantRepo.GetListAsync(x => x.TenantId == tenantId);
            var existingModuleIds = existingGrants.Select(g => g.ModuleId).ToHashSet();

            foreach (var moduleId in moduleIds)
            {
                if (existingModuleIds.Contains(moduleId))
                {
                    continue;
                }

                await _moduleGrantRepo.InsertAsync(new TenantModuleGrant(
                    GuidGenerator.Create(),
                    tenantId,
                    moduleId,
                    true)
                {
                    GrantDate = Clock.Now
                });
            }
        }

        public async Task RevokeModuleAsync(Guid tenantId, Guid moduleId)
        {
            var grant = await _moduleGrantRepo.FirstOrDefaultAsync(
                x => x.TenantId == tenantId && x.ModuleId == moduleId);

            if (grant != null)
            {
                await _moduleGrantRepo.DeleteAsync(grant);
            }
        }

        public async Task<List<TenantMenuDto>> GetGrantedMenusAsync(Guid tenantId)
        {
            var grants = await _moduleGrantRepo.GetListAsync(x => x.TenantId == tenantId && x.IsGranted);
            var moduleIds = grants.Select(g => g.ModuleId).ToList();

            var modules = await _systemModuleRepo.GetListAsync(m => moduleIds.Contains(m.Id) && m.EnabledMark);
            var moduleList = modules.OrderBy(m => m.SortCode).ToList();

            var menuDict = moduleList.ToDictionary(
                m => m.Id,
                m => new TenantMenuDto
                {
                    Code = m.Id.ToString("N"),
                    Name = m.FullName,
                    ParentCode = m.ParentId?.ToString("N"),
                    Url = m.UrlAddress,
                    Icon = m.Icon,
                    Sort = m.SortCode,
                    Children = new List<TenantMenuDto>()
                });

            var rootMenus = new List<TenantMenuDto>();

            foreach (var menu in menuDict.Values)
            {
                if (string.IsNullOrEmpty(menu.ParentCode) || !menuDict.ContainsKey(Guid.Parse(menu.ParentCode)))
                {
                    rootMenus.Add(menu);
                }
                else
                {
                    var parent = menuDict[Guid.Parse(menu.ParentCode)];
                    parent.Children.Add(menu);
                }
            }

            return rootMenus;
        }
    }
}
