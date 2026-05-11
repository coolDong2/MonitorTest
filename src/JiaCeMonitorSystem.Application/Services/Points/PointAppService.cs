using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.MonitoringItemTypes;
using JiaCeMonitorSystem.Dtos.Points;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.MonitoringItemTypes;
using JiaCeMonitorSystem.Points;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.Points
{
    /// <summary>
    /// 测点管理应用服务
    /// </summary>
    [Authorize]
    public class PointAppService :
        CrudAppService<Point, PointDto, Guid, GetPointListInput, PointCreateDto, PointUpdateDto>,
        IPointAppService
    {
        private readonly IRepository<MonitoringItemProperty, Guid> _monitoringItemPropertyRepository;
        private readonly IRepository<MonitoringItemType, Guid> _monitoringItemTypeRepository;

        public PointAppService(
            IRepository<Point, Guid> repository,
            IRepository<MonitoringItemProperty, Guid> monitoringItemPropertyRepository,
            IRepository<MonitoringItemType, Guid> monitoringItemTypeRepository) : base(repository)
        {
            _monitoringItemPropertyRepository = monitoringItemPropertyRepository;
            _monitoringItemTypeRepository = monitoringItemTypeRepository;
        }

        /// <summary>
        /// 创建测点，自动填充监测项目类型名称冗余字段
        /// </summary>
        public override async Task<PointDto> CreateAsync(PointCreateDto input)
        {
            MonitoringItemType? itemType = null;
            if (input.ItemTypeId.HasValue)
                itemType = await _monitoringItemTypeRepository.GetAsync(input.ItemTypeId.Value);

            var point = new Point(
                GuidGenerator.Create(),
                input.ProjectId,
                input.PointCode,
                input.PointName,
                input.ItemTypeId,
                itemType?.TypeName,
                input.LocationX,
                input.LocationY,
                input.LocationZ,
                input.MonitoringFrequency,
                input.WarningThreshold,
                input.AlarmThreshold,
                input.ChangeRateThreshold,
                input.CumulativeThreshold,
                input.ExtendedProperties,
                input.Description);

            await Repository.InsertAsync(point);
            var dto = ObjectMapper.Map<Point, PointDto>(point);
            await FillExtendedFieldsAsync(new List<PointDto> { dto }, itemType);
            return dto;
        }

        /// <summary>
        /// 更新测点，自动填充监测项目类型名称冗余字段
        /// </summary>
        public override async Task<PointDto> UpdateAsync(Guid id, PointUpdateDto input)
        {
            var point = await Repository.GetAsync(id);
            var itemType = input.ItemTypeId.HasValue
                ? await _monitoringItemTypeRepository.GetAsync(input.ItemTypeId.Value)
                : null;

            point.UpdateInfo(
                input.PointName,
                input.ItemTypeId,
                itemType?.TypeName,
                input.LocationX,
                input.LocationY,
                input.LocationZ,
                input.MonitoringFrequency,
                input.Description);

            if (input.WarningThreshold.HasValue || input.AlarmThreshold.HasValue ||
                input.ChangeRateThreshold.HasValue || input.CumulativeThreshold.HasValue)
            {
                point.ConfigureThresholds(input.WarningThreshold, input.AlarmThreshold, input.ChangeRateThreshold, input.CumulativeThreshold);
            }

            await Repository.UpdateAsync(point);
            var dto = ObjectMapper.Map<Point, PointDto>(point);
            await FillExtendedFieldsAsync(new List<PointDto> { dto }, itemType);
            return dto;
        }

        /// <summary>
        /// 配置测点阈值
        /// </summary>
        [Authorize(Permissions.Permissions.Points_ConfigureThreshold)]
        public async Task ConfigureThresholdAsync(Guid id, decimal? warningThreshold, decimal? alarmThreshold, decimal? changeRateThreshold, decimal? cumulativeThreshold)
        {
            var point = await Repository.GetAsync(id);
            point.ConfigureThresholds(warningThreshold, alarmThreshold, changeRateThreshold, cumulativeThreshold);
            await Repository.UpdateAsync(point);
        }

        /// <summary>
        /// 获取测点列表（非分页，按项目筛选）
        /// </summary>
        public async Task<List<PointDto>> GetListAsync(Guid? projectId)
        {
            var query = await Repository.GetQueryableAsync();
            if (projectId.HasValue)
                query = query.Where(p => p.ProjectId == projectId.Value);

            var points = await AsyncExecuter.ToListAsync(query.OrderBy(p => p.PointCode));
            var dtos = ObjectMapper.Map<List<Point>, List<PointDto>>(points);
            await FillExtendedFieldsAsync(dtos);
            return dtos;
        }

        /// <summary>
        /// 获取测点历史数据简要
        /// </summary>
        public async Task<List<PointDto>> GetHistoryAsync(Guid projectId)
        {
            var points = await Repository.GetListAsync(p => p.ProjectId == projectId);
            var dtos = ObjectMapper.Map<List<Point>, List<PointDto>>(points);
            await FillExtendedFieldsAsync(dtos);
            return dtos;
        }

        /// <summary>
        /// 获取测点可用属性列表
        /// 根据测点关联的监测项目类型，返回所有属性
        /// </summary>
        public async Task<List<MonitoringItemPropertyDto>> GetPropertiesAsync(Guid pointId)
        {
            var point = await Repository.GetAsync(pointId);
            if (!point.ItemTypeId.HasValue)
            {
                return new List<MonitoringItemPropertyDto>();
            }

            var properties = await _monitoringItemPropertyRepository.GetListAsync(p => p.ItemTypeId == point.ItemTypeId.Value);
            return ObjectMapper.Map<List<MonitoringItemProperty>, List<MonitoringItemPropertyDto>>(properties);
        }

        /// <summary>
        /// 填充测点扩展字段（分类、属性列表）
        /// </summary>
        private async Task FillExtendedFieldsAsync(List<PointDto> dtos, MonitoringItemType? singleItemType = null)
        {
            var itemTypeIds = dtos.Where(d => d.ItemTypeId.HasValue).Select(d => d.ItemTypeId!.Value).Distinct().ToList();
            if (!itemTypeIds.Any()) return;

            var itemTypes = singleItemType != null && itemTypeIds.Contains(singleItemType.Id)
                ? new List<MonitoringItemType> { singleItemType }
                : await _monitoringItemTypeRepository.GetListAsync(t => itemTypeIds.Contains(t.Id));

            var properties = await _monitoringItemPropertyRepository.GetListAsync(p => itemTypeIds.Contains(p.ItemTypeId));

            foreach (var dto in dtos)
            {
                if (!dto.ItemTypeId.HasValue) continue;
                var itemType = itemTypes.FirstOrDefault(t => t.Id == dto.ItemTypeId.Value);
                if (itemType == null) continue;

                dto.ItemCategory = (int)itemType.Category;
                dto.ItemCategoryName = itemType.Category.ToString();
                dto.Properties = ObjectMapper.Map<List<MonitoringItemProperty>, List<MonitoringItemPropertyDto>>(
                    properties.Where(p => p.ItemTypeId == itemType.Id).ToList());
            }
        }
    }
}
