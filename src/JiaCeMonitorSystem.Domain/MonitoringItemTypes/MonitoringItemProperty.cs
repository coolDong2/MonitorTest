using System;
using JiaCeMonitorSystem.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.MonitoringItemTypes
{
    /// <summary>
    /// 监测项目属性实体
    /// 表名：JC_MonitoringItemProperties
    /// </summary>
    public class MonitoringItemProperty : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属监测项目类型ID
        /// </summary>
        public Guid ItemTypeId { get; private set; }

        /// <summary>
        /// 属性编码
        /// </summary>
        public string PropertyCode { get; private set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public PropertyDataType DataType { get; private set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string? Unit { get; private set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// 预警阈值 - 当监测值达到此值时触发一级预警
        /// 【属性级阈值】解决同一测点不同属性阈值差异问题
        /// </summary>
        public decimal? WarningThreshold { get; private set; }

        /// <summary>
        /// 报警阈值 - 当监测值达到此值时触发二级报警
        /// 【属性级阈值】解决同一测点不同属性阈值差异问题
        /// </summary>
        public decimal? AlarmThreshold { get; private set; }

        /// <summary>
        /// 变化率阈值 - 监测值单位时间内的变化率超过此值时触发预警
        /// 【属性级阈值】解决同一测点不同属性阈值差异问题
        /// </summary>
        public decimal? ChangeRateThreshold { get; private set; }

        /// <summary>
        /// 累计变化阈值 - 从初始值开始的累计变化量超过此值时触发预警
        /// 【属性级阈值】解决同一测点不同属性阈值差异问题
        /// </summary>
        public decimal? CumulativeThreshold { get; private set; }

        private MonitoringItemProperty()
        {
            PropertyCode = string.Empty;
            PropertyName = string.Empty;
        }

        /// <summary>
        /// 创建监测项目属性
        /// </summary>
        public MonitoringItemProperty(
            Guid id,
            Guid itemTypeId,
            string propertyCode,
            string propertyName,
            PropertyDataType dataType = PropertyDataType.Number,
            string? unit = null,
            bool isRequired = true,
            int sortCode = 0,
            string? description = null,
            decimal? warningThreshold = null,
            decimal? alarmThreshold = null,
            decimal? changeRateThreshold = null,
            decimal? cumulativeThreshold = null)
            : base(id)
        {
            ItemTypeId = itemTypeId;
            PropertyCode = propertyCode;
            PropertyName = propertyName;
            DataType = dataType;
            Unit = unit;
            IsRequired = isRequired;
            SortCode = sortCode;
            Description = description;
            WarningThreshold = warningThreshold;
            AlarmThreshold = alarmThreshold;
            ChangeRateThreshold = changeRateThreshold;
            CumulativeThreshold = cumulativeThreshold;
        }

        /// <summary>
        /// 更新属性信息
        /// </summary>
        public void UpdateInfo(
            string propertyCode,
            string propertyName,
            PropertyDataType dataType,
            string? unit,
            bool isRequired,
            int sortCode,
            string? description = null,
            decimal? warningThreshold = null,
            decimal? alarmThreshold = null,
            decimal? changeRateThreshold = null,
            decimal? cumulativeThreshold = null)
        {
            PropertyCode = propertyCode;
            PropertyName = propertyName;
            DataType = dataType;
            Unit = unit;
            IsRequired = isRequired;
            SortCode = sortCode;
            Description = description;
            WarningThreshold = warningThreshold;
            AlarmThreshold = alarmThreshold;
            ChangeRateThreshold = changeRateThreshold;
            CumulativeThreshold = cumulativeThreshold;
        }
    }
}
