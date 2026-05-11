using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.DomainServices;
using JiaCeMonitorSystem.Dtos.SystemModules;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.SystemModules;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.SystemModules
{
    /// <summary>
    /// 系统菜单模块应用服务
    /// </summary>
    [Authorize]
    public class SystemModuleAppService :
        CrudAppService<SystemModule, SystemModuleDto, Guid, GetSystemModuleListInput, SystemModuleCreateDto, SystemModuleUpdateDto>,
        ISystemModuleAppService
    {
        private readonly SystemModuleManager _systemModuleManager;

        public SystemModuleAppService(
            IRepository<SystemModule, Guid> repository,
            SystemModuleManager systemModuleManager) : base(repository)
        {
            _systemModuleManager = systemModuleManager;
        }

        /// <summary>
        /// 获取树形列表
        /// </summary>
        public async Task<List<SystemModuleTreeDto>> GetTreeListAsync()
        {
            var modules = await Repository.GetListAsync();
            var moduleDtos = ObjectMapper.Map<List<SystemModule>, List<SystemModuleTreeDto>>(modules);
            return BuildTree(moduleDtos);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<SystemModuleDto> GetModelAsync(Guid id)
        {
            var module = await Repository.GetAsync(id);
            return ObjectMapper.Map<SystemModule, SystemModuleDto>(module);
        }

        /// <summary>
        /// 创建菜单模块
        /// </summary>
        [Authorize(Permissions.Permissions.SystemModules_Create)]
        public override async Task<SystemModuleDto> CreateAsync(SystemModuleCreateDto input)
        {
            var layers = await _systemModuleManager.CalculateLayersAsync(input.ParentId);
            var module = ObjectMapper.Map<SystemModuleCreateDto, SystemModule>(input);
            module.SetLayers(layers);
            await Repository.InsertAsync(module);
            return ObjectMapper.Map<SystemModule, SystemModuleDto>(module);
        }

        /// <summary>
        /// 更新菜单模块
        /// </summary>
        [Authorize(Permissions.Permissions.SystemModules_Edit)]
        public override async Task<SystemModuleDto> UpdateAsync(Guid id, SystemModuleUpdateDto input)
        {
            var module = await Repository.GetAsync(id);
            var layers = await _systemModuleManager.CalculateLayersAsync(module.ParentId);
            ObjectMapper.Map(input, module);
            module.SetLayers(layers);
            await Repository.UpdateAsync(module);
            return ObjectMapper.Map<SystemModule, SystemModuleDto>(module);
        }

        /// <summary>
        /// 删除菜单模块
        /// </summary>
        [Authorize(Permissions.Permissions.SystemModules_Delete)]
        public override async Task DeleteAsync(Guid id)
        {
            await _systemModuleManager.ValidateCanDeleteAsync(id);
            await base.DeleteAsync(id);
        }

        /// <summary>
        /// 构建树形结构
        /// </summary>
        private List<SystemModuleTreeDto> BuildTree(List<SystemModuleTreeDto> modules)
        {
            var lookup = modules.ToLookup(m => m.ParentId);
            var rootModules = modules.Where(m => m.ParentId == null || m.ParentId == Guid.Empty).ToList();

            foreach (var module in rootModules)
            {
                BuildChildren(module, lookup);
            }

            return rootModules.OrderBy(m => m.SortCode).ToList();
        }

        private void BuildChildren(SystemModuleTreeDto parent, ILookup<Guid?, SystemModuleTreeDto> lookup)
        {
            parent.Children = lookup[parent.Id].OrderBy(m => m.SortCode).ToList();
            foreach (var child in parent.Children)
            {
                BuildChildren(child, lookup);
            }
        }
    }
}
