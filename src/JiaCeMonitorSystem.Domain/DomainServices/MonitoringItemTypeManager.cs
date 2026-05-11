using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.MonitoringItemTypes;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace JiaCeMonitorSystem.DomainServices
{
    /// <summary>
    /// 监测项目类型领域服务
    /// 负责级联保存Type与Properties，校验PropertyCode唯一性
    /// </summary>
    public class MonitoringItemTypeManager : DomainService
    {
        private readonly IRepository<MonitoringItemType, Guid> _monitoringItemTypeRepository;
        private readonly IRepository<MonitoringItemProperty, Guid> _monitoringItemPropertyRepository;

        public MonitoringItemTypeManager(
            IRepository<MonitoringItemType, Guid> monitoringItemTypeRepository,
            IRepository<MonitoringItemProperty, Guid> monitoringItemPropertyRepository)
        {
            _monitoringItemTypeRepository = monitoringItemTypeRepository;
            _monitoringItemPropertyRepository = monitoringItemPropertyRepository;
        }

        /// <summary>
        /// 校验并创建监测项目类型（含属性集合）
        /// </summary>
        public async Task<MonitoringItemType> CreateAsync(
            MonitoringItemType itemType,
            List<MonitoringItemProperty> properties)
        {
            // 校验TypeCode全局唯一
            var existingType = await _monitoringItemTypeRepository.FirstOrDefaultAsync(
                x => x.TypeCode == itemType.TypeCode);
            if (existingType != null)
            {
                throw new BusinessException(ErrorCodes.MonitoringItemType_DuplicateCode)
                    .WithData("TypeCode", itemType.TypeCode);
            }

            // 校验同一Type下PropertyCode不可重复
            ValidatePropertyCodes(properties);

            // 添加属性
            foreach (var property in properties)
            {
                itemType.Properties.Add(property);
            }

            return itemType;
        }

        /// <summary>
        /// 校验并更新监测项目类型（含属性集合增删改）
        /// </summary>
        public async Task UpdateAsync(
            MonitoringItemType itemType,
            List<MonitoringItemProperty> newProperties)
        {
            // 校验TypeCode全局唯一（排除自身）
            var existingType = await _monitoringItemTypeRepository.FirstOrDefaultAsync(
                x => x.TypeCode == itemType.TypeCode && x.Id != itemType.Id);
            if (existingType != null)
            {
                throw new BusinessException(ErrorCodes.MonitoringItemType_DuplicateCode)
                    .WithData("TypeCode", itemType.TypeCode);
            }

            // 校验属性编码唯一性
            ValidatePropertyCodes(newProperties);

            // 获取现有属性
            var existingProperties = await _monitoringItemPropertyRepository.GetListAsync(
                x => x.ItemTypeId == itemType.Id);

            // 删除已不存在的属性
            var newPropertyIds = newProperties.Select(p => p.Id).ToHashSet();
            foreach (var existingProp in existingProperties)
            {
                if (!newPropertyIds.Contains(existingProp.Id))
                {
                    await _monitoringItemPropertyRepository.DeleteAsync(existingProp);
                }
            }

            // 更新或新增属性
            var existingPropertyDict = existingProperties.ToDictionary(p => p.Id);
            foreach (var newProp in newProperties)
            {
                if (existingPropertyDict.TryGetValue(newProp.Id, out var existingProp))
                {
                    existingProp.UpdateInfo(
                        newProp.PropertyCode,
                        newProp.PropertyName,
                        newProp.DataType,
                        newProp.Unit,
                        newProp.IsRequired,
                        newProp.SortCode,
                        newProp.Description);
                    await _monitoringItemPropertyRepository.UpdateAsync(existingProp);
                }
                else
                {
                    await _monitoringItemPropertyRepository.InsertAsync(newProp);
                }
            }
        }

        /// <summary>
        /// 校验属性编码列表是否重复
        /// </summary>
        private void ValidatePropertyCodes(List<MonitoringItemProperty> properties)
        {
            var duplicateCodes = properties
                .GroupBy(p => p.PropertyCode)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateCodes.Any())
            {
                throw new BusinessException(ErrorCodes.MonitoringItemProperty_DuplicateCode)
                    .WithData("PropertyCodes", string.Join(", ", duplicateCodes));
            }
        }
    }
}
