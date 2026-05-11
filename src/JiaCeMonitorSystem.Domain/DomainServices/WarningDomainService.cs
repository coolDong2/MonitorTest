using System;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.Events;
using JiaCeMonitorSystem.MonitoringItemTypes;
using JiaCeMonitorSystem.Points;
using JiaCeMonitorSystem.WarningRecords;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace JiaCeMonitorSystem.DomainServices
{
    /// <summary>
    /// 预警领域服务，负责阈值判定与预警生成逻辑
    /// 【重构】支持按监测属性（Property）级别阈值判定，解决同一测点不同属性阈值差异问题
    /// </summary>
    public class WarningDomainService : DomainService
    {
        private readonly IRepository<Point, Guid> _pointRepository;
        private readonly IRepository<MonitoringItemProperty, Guid> _propertyRepository;
        private readonly IRepository<MonitoringData.MonitoringData, Guid> _dataRepository;
        private readonly IRepository<WarningRecord, Guid> _warningRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        /// <summary>
        /// 初始化预警领域服务
        /// </summary>
        public WarningDomainService(
            IRepository<Point, Guid> pointRepository,
            IRepository<MonitoringItemProperty, Guid> propertyRepository,
            IRepository<MonitoringData.MonitoringData, Guid> dataRepository,
            IRepository<WarningRecord, Guid> warningRepository,
            IDistributedEventBus distributedEventBus)
        {
            _pointRepository = pointRepository;
            _propertyRepository = propertyRepository;
            _dataRepository = dataRepository;
            _warningRepository = warningRepository;
            _distributedEventBus = distributedEventBus;
        }

        /// <summary>
        /// 判定监测数据是否触发预警
        /// 【重构】优先使用Property级别阈值，无则回退到Point级别阈值
        /// 【重构】按PropertyId分别查询历史数据，确保不同属性独立判定
        /// </summary>
        public async Task<WarningRecord?> EvaluateAsync(MonitoringData.MonitoringData data)
        {
            var point = await _pointRepository.GetAsync(data.PointId);
            var property = await _propertyRepository.GetAsync(data.PropertyId);

            // 获取前次同属性的监测值
            var previousDataList = await _dataRepository.GetListAsync(
                d => d.PointId == data.PointId
                     && d.PropertyId == data.PropertyId
                     && d.MonitoringTime < data.MonitoringTime);

            var previousData = previousDataList.OrderByDescending(d => d.MonitoringTime).FirstOrDefault();
            decimal? previousValue = previousData?.MonitoringValue;
            decimal currentValue = data.MonitoringValue;

            // 优先使用Property级别阈值，无则回退到Point级别
            var alarmThreshold = property.AlarmThreshold ?? point.AlarmThreshold;
            var warningThreshold = property.WarningThreshold ?? point.WarningThreshold;
            var changeRateThreshold = property.ChangeRateThreshold ?? point.ChangeRateThreshold;
            var cumulativeThreshold = property.CumulativeThreshold ?? point.CumulativeThreshold;

            // 1. 阈值判定（报警阈值优先）
            if (alarmThreshold.HasValue && currentValue >= alarmThreshold.Value)
            {
                return await CreateWarningRecordAsync(data, point, property, WarningType.Threshold, WarningLevel.Level3Danger,
                    currentValue, alarmThreshold.Value, previousValue);
            }

            if (warningThreshold.HasValue && currentValue >= warningThreshold.Value)
            {
                return await CreateWarningRecordAsync(data, point, property, WarningType.Threshold, WarningLevel.Level2Warning,
                    currentValue, warningThreshold.Value, previousValue);
            }

            // 2. 变化率判定
            if (previousValue.HasValue && previousValue.Value != 0 && changeRateThreshold.HasValue)
            {
                var changeRate = Math.Abs((currentValue - previousValue.Value) / previousValue.Value * 100);
                if (changeRate >= changeRateThreshold.Value)
                {
                    return await CreateWarningRecordAsync(data, point, property, WarningType.ChangeRate, WarningLevel.Level1Notice,
                        currentValue, changeRateThreshold.Value, previousValue, changeRate);
                }
            }

            // 3. 累计值判定
            if (cumulativeThreshold.HasValue)
            {
                var initialValue = await GetInitialValueAsync(data.PointId, data.PropertyId);
                if (initialValue.HasValue)
                {
                    var cumulativeChange = Math.Abs(currentValue - initialValue.Value);
                    if (cumulativeChange >= cumulativeThreshold.Value)
                    {
                        return await CreateWarningRecordAsync(data, point, property, WarningType.Cumulative, WarningLevel.Level2Warning,
                            currentValue, cumulativeThreshold.Value, previousValue, null, cumulativeChange);
                    }
                }
            }

            return null;
        }

        private async Task<WarningRecord> CreateWarningRecordAsync(
            MonitoringData.MonitoringData data,
            Point point,
            MonitoringItemProperty property,
            WarningType warningType,
            WarningLevel warningLevel,
            decimal triggerValue,
            decimal thresholdValue,
            decimal? previousValue,
            decimal? changeRate = null,
            decimal? cumulativeChange = null)
        {
            var warningContent = GenerateWarningContent(property.PropertyName, warningType, warningLevel, triggerValue, thresholdValue);
            var suggestedAction = GenerateSuggestedAction(warningType, warningLevel);

            var warning = new WarningRecord(
                GuidGenerator.Create(),
                point.Id,
                point.PointName,
                point.ProjectId,
                data.ProjectName,
                data.MonitoringTime,
                data.MonitoringValue,
                warningType,
                warningLevel,
                triggerValue,
                thresholdValue,
                warningContent,
                data.PropertyId,
                data.PropertyCode,
                data.PropertyName,
                data.Unit,
                data.Id,
                point.ItemTypeId,
                data.ItemTypeName,
                data.DataState,
                data.CollectorName,
                data.DataRemark,
                previousValue,
                changeRate,
                cumulativeChange,
                suggestedAction);

            await _warningRepository.InsertAsync(warning);

            // 更新测点预警状态
            point.SetWarningLevel(warningLevel);
            await _pointRepository.UpdateAsync(point);

            // 发布预警触发领域事件
            await PublishWarningEventAsync(warning, point, property);

            return warning;
        }

        private async Task PublishWarningEventAsync(WarningRecord warning, Point point, MonitoringItemProperty property)
        {
            var eventData = new WarningTriggeredDomainEvent
            {
                WarningRecordId = warning.Id,
                PointId = warning.PointId,
                ProjectId = warning.ProjectId,
                PropertyId = warning.PropertyId,
                PropertyName = property.PropertyName,
                WarningLevel = warning.WarningLevel,
                WarningType = warning.WarningType,
                MonitoringValue = warning.MonitoringValue,
                TriggerTime = warning.MonitoringTime,
                ThresholdValue = warning.ThresholdValue
            };

            await _distributedEventBus.PublishAsync(eventData);
        }

        private async Task<decimal?> GetInitialValueAsync(Guid pointId, Guid propertyId)
        {
            var firstDataList = await _dataRepository.GetListAsync(
                d => d.PointId == pointId && d.PropertyId == propertyId);
            return firstDataList.OrderBy(d => d.MonitoringTime).FirstOrDefault()?.MonitoringValue;
        }

        private static string GenerateWarningContent(string propertyName, WarningType type, WarningLevel level, decimal value, decimal threshold)
        {
            var levelText = level switch
            {
                WarningLevel.Level1Notice => "注意",
                WarningLevel.Level2Warning => "警告",
                WarningLevel.Level3Danger => "危险",
                _ => "提示"
            };

            var typeText = type switch
            {
                WarningType.Threshold => "阈值",
                WarningType.ChangeRate => "变化率",
                WarningType.Cumulative => "累计值",
                _ => "其他"
            };

            return $"属性 [{propertyName}] 触发{typeText}{levelText}：监测值 {value}，阈值 {threshold}";
        }

        private static string GenerateSuggestedAction(WarningType type, WarningLevel level)
        {
            if (level == WarningLevel.Level3Danger)
            {
                return "请立即安排人员现场核查，必要时暂停相关作业。";
            }

            return type switch
            {
                WarningType.Threshold => "建议加强监测频率，密切关注数据变化趋势。",
                WarningType.ChangeRate => "建议检查监测环境与设备，确认是否存在异常扰动。",
                WarningType.Cumulative => "建议开展结构安全评估，分析累计变形影响。",
                _ => "建议进行数据复核与现场巡查。"
            };
        }
    }
}
