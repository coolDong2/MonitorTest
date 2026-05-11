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
    /// 系统字典应用服务
    /// </summary>
    [Authorize]
    public class SystemDictionaryAppService :
        CrudAppService<SystemDictionary, SystemDictionaryDto, Guid, GetSystemDictionaryListInput, SystemDictionaryCreateDto, SystemDictionaryUpdateDto>,
        ISystemDictionaryAppService
    {
        public SystemDictionaryAppService(IRepository<SystemDictionary, Guid> repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        public async Task<PagedResultDto<SystemDictionaryDto>> GetPageListAsync(GetSystemDictionaryListInput input)
        {
            var query = await Repository.GetQueryableAsync();
            if (input.ItemId.HasValue)
            {
                query = query.Where(d => d.ItemId == input.ItemId.Value);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            var dictionaries = await AsyncExecuter.ToListAsync(query.OrderBy(d => d.SortCode).PageBy(input));
            var dictionaryDtos = ObjectMapper.Map<List<SystemDictionary>, List<SystemDictionaryDto>>(dictionaries);

            return new PagedResultDto<SystemDictionaryDto>(totalCount, dictionaryDtos);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<SystemDictionaryDto> GetModelAsync(Guid id)
        {
            var dictionary = await Repository.GetAsync(id);
            return ObjectMapper.Map<SystemDictionary, SystemDictionaryDto>(dictionary);
        }

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        public new async Task<List<SystemDictionaryDto>> GetListAsync(GetSystemDictionaryListInput input)
        {
            var query = await Repository.GetQueryableAsync();
            if (input.ItemId.HasValue)
            {
                query = query.Where(d => d.ItemId == input.ItemId.Value);
            }

            var dictionaries = await AsyncExecuter.ToListAsync(query.OrderBy(d => d.SortCode));
            return ObjectMapper.Map<List<SystemDictionary>, List<SystemDictionaryDto>>(dictionaries);
        }
    }
}
