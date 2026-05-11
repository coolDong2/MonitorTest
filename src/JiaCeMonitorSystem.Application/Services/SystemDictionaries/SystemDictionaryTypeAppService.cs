using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.SystemDictionaries;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.SystemDictionaries;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.SystemDictionaries
{
    /// <summary>
    /// 系统字典类型应用服务
    /// </summary>
    [Authorize]
    public class SystemDictionaryTypeAppService :
        CrudAppService<SystemDictionaryType, SystemDictionaryTypeDto, Guid, GetSystemDictionaryTypeListInput, SystemDictionaryTypeCreateDto, SystemDictionaryTypeUpdateDto>,
        ISystemDictionaryTypeAppService
    {
        public SystemDictionaryTypeAppService(IRepository<SystemDictionaryType, Guid> repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取字典树
        /// </summary>
        public async Task<List<SystemDictionaryTypeTreeDto>> GetDictionaryTreeAsync()
        {
            var types = await Repository.GetListAsync();
            var typeDtos = ObjectMapper.Map<List<SystemDictionaryType>, List<SystemDictionaryTypeTreeDto>>(types);
            return BuildTree(typeDtos);
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        public async Task<PagedResultDto<SystemDictionaryTypeDto>> GetPageListAsync(GetSystemDictionaryTypeListInput input)
        {
            return await GetListAsync(input);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<SystemDictionaryTypeDto> GetModelAsync(Guid id)
        {
            var type = await Repository.GetAsync(id);
            return ObjectMapper.Map<SystemDictionaryType, SystemDictionaryTypeDto>(type);
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        [Authorize(Permissions.Permissions.SystemDictionaryTypes_Create)]
        public override async Task<SystemDictionaryTypeDto> CreateAsync(SystemDictionaryTypeCreateDto input)
        {
            var type = ObjectMapper.Map<SystemDictionaryTypeCreateDto, SystemDictionaryType>(input);
            var layers = input.ParentId.HasValue ? 2 : 1;
            type.SetLayers(layers);
            await Repository.InsertAsync(type);
            return ObjectMapper.Map<SystemDictionaryType, SystemDictionaryTypeDto>(type);
        }

        /// <summary>
        /// 构建树形结构
        /// </summary>
        private List<SystemDictionaryTypeTreeDto> BuildTree(List<SystemDictionaryTypeTreeDto> types)
        {
            var lookup = types.ToLookup(t => t.ParentId);
            var rootTypes = types.Where(t => t.ParentId == null || t.ParentId == Guid.Empty).ToList();

            foreach (var type in rootTypes)
            {
                BuildChildren(type, lookup);
            }

            return rootTypes.OrderBy(t => t.SortCode).ToList();
        }

        private void BuildChildren(SystemDictionaryTypeTreeDto parent, ILookup<Guid?, SystemDictionaryTypeTreeDto> lookup)
        {
            parent.Children = lookup[parent.Id].OrderBy(t => t.SortCode).ToList();
            foreach (var child in parent.Children)
            {
                BuildChildren(child, lookup);
            }
        }
    }
}
