using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.DomainServices;
using JiaCeMonitorSystem.Dtos.MonitoringItemTypes;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.MonitoringItemTypes;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.MonitoringItemTypes
{
    /// <summary>
    /// 监测项目类型应用服务
    /// </summary>
    [Authorize]
    public class MonitoringItemTypeAppService :
        CrudAppService<MonitoringItemType, MonitoringItemTypeDto, Guid, GetMonitoringItemTypeListInput, MonitoringItemTypeCreateDto, MonitoringItemTypeUpdateDto>,
        IMonitoringItemTypeAppService
    {
        private readonly MonitoringItemTypeManager _monitoringItemTypeManager;
        private readonly IRepository<MonitoringItemProperty, Guid> _monitoringItemPropertyRepository;

        public MonitoringItemTypeAppService(
            IRepository<MonitoringItemType, Guid> repository,
            MonitoringItemTypeManager monitoringItemTypeManager,
            IRepository<MonitoringItemProperty, Guid> monitoringItemPropertyRepository) : base(repository)
        {
            _monitoringItemTypeManager = monitoringItemTypeManager;
            _monitoringItemPropertyRepository = monitoringItemPropertyRepository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        public async Task<PagedResultDto<MonitoringItemTypeDto>> GetPageListAsync(GetMonitoringItemTypeListInput input)
        {
            var query = await Repository.GetQueryableAsync();
            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                query = query.Where(t => t.TypeCode.Contains(input.Filter) || t.TypeName.Contains(input.Filter));
            }
            if (input.Category.HasValue)
            {
                query = query.Where(t => (int)t.Category == input.Category.Value);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            var types = await AsyncExecuter.ToListAsync(query.OrderBy(t => t.SortCode).PageBy(input));
            var typeDtos = ObjectMapper.Map<List<MonitoringItemType>, List<MonitoringItemTypeDto>>(types);

            return new PagedResultDto<MonitoringItemTypeDto>(totalCount, typeDtos);
        }

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        public new async Task<List<MonitoringItemTypeDto>> GetListAsync(GetMonitoringItemTypeListInput input)
        {
            var types = await Repository.GetListAsync();
            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                types = types.Where(t => t.TypeCode.Contains(input.Filter) || t.TypeName.Contains(input.Filter)).ToList();
            }
            if (input.Category.HasValue)
            {
                types = types.Where(t => (int)t.Category == input.Category.Value).ToList();
            }
            return ObjectMapper.Map<List<MonitoringItemType>, List<MonitoringItemTypeDto>>(types);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<MonitoringItemTypeDto> GetModelAsync(Guid id)
        {
            var type = await Repository.GetAsync(id);
            var dto = ObjectMapper.Map<MonitoringItemType, MonitoringItemTypeDto>(type);
            var properties = await _monitoringItemPropertyRepository.GetListAsync(p => p.ItemTypeId == id);
            dto.Properties = ObjectMapper.Map<List<MonitoringItemProperty>, List<MonitoringItemPropertyDto>>(properties);
            return dto;
        }

        /// <summary>
        /// 创建监测项目类型（级联保存属性）
        /// </summary>
        [Authorize(Permissions.Permissions.MonitoringItemTypes_Create)]
        public override async Task<MonitoringItemTypeDto> CreateAsync(MonitoringItemTypeCreateDto input)
        {
            var itemType = ObjectMapper.Map<MonitoringItemTypeCreateDto, MonitoringItemType>(input);
            var properties = input.PropertyList.Select(p =>
                ObjectMapper.Map<MonitoringItemPropertyCreateDto, MonitoringItemProperty>(p)).ToList();

            itemType = await _monitoringItemTypeManager.CreateAsync(itemType, properties);
            await Repository.InsertAsync(itemType);

            return ObjectMapper.Map<MonitoringItemType, MonitoringItemTypeDto>(itemType);
        }

        /// <summary>
        /// 更新监测项目类型（级联更新属性）
        /// </summary>
        [Authorize(Permissions.Permissions.MonitoringItemTypes_Edit)]
        public override async Task<MonitoringItemTypeDto> UpdateAsync(Guid id, MonitoringItemTypeUpdateDto input)
        {
            var itemType = await Repository.GetAsync(id);
            ObjectMapper.Map(input, itemType);

            var properties = input.PropertyList.Select(p =>
            {
                var prop = ObjectMapper.Map<MonitoringItemPropertyCreateDto, MonitoringItemProperty>(p);
                prop.GetType().GetProperty("ItemTypeId")?.SetValue(prop, id);
                return prop;
            }).ToList();

            await _monitoringItemTypeManager.UpdateAsync(itemType, properties);
            await Repository.UpdateAsync(itemType);

            return ObjectMapper.Map<MonitoringItemType, MonitoringItemTypeDto>(itemType);
        }
    }
}
