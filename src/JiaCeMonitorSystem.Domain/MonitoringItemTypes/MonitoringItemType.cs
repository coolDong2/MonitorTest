using System;
using System.Collections.Generic;
using JiaCeMonitorSystem.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.MonitoringItemTypes
{
    /// <summary>
    /// 监测项目类型聚合根
    /// 表名：JC_MonitoringItemTypes
    /// </summary>
    public class MonitoringItemType : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 类型编码
        /// </summary>
        public string TypeCode { get; private set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// 监测分类
        /// </summary>
        public MonitoringCategory Category { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; private set; }

        /// <summary>
        /// 属性集合
        /// </summary>
        public ICollection<MonitoringItemProperty> Properties { get; private set; }

        private MonitoringItemType()
        {
            TypeCode = string.Empty;
            TypeName = string.Empty;
            Properties = new List<MonitoringItemProperty>();
        }

        /// <summary>
        /// 创建监测项目类型
        /// </summary>
        public MonitoringItemType(
            Guid id,
            string typeCode,
            string typeName,
            MonitoringCategory category = MonitoringCategory.Displacement,
            int sortCode = 0,
            bool enabledMark = true,
            string? description = null)
            : base(id)
        {
            TypeCode = typeCode;
            TypeName = typeName;
            Category = category;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            Description = description;
            Properties = new List<MonitoringItemProperty>();
        }

        /// <summary>
        /// 更新类型信息
        /// </summary>
        public void UpdateInfo(
            string typeCode,
            string typeName,
            MonitoringCategory category,
            int sortCode,
            bool enabledMark,
            string? description = null)
        {
            TypeCode = typeCode;
            TypeName = typeName;
            Category = category;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            Description = description;
        }

        /// <summary>
        /// 添加属性
        /// </summary>
        public MonitoringItemProperty AddProperty(
            Guid id,
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
        {
            var property = new MonitoringItemProperty(
                id,
                Id,
                propertyCode,
                propertyName,
                dataType,
                unit,
                isRequired,
                sortCode,
                description,
                warningThreshold,
                alarmThreshold,
                changeRateThreshold,
                cumulativeThreshold);

            Properties.Add(property);
            return property;
        }

        /// <summary>
        /// 清除所有属性
        /// </summary>
        public void ClearProperties()
        {
            Properties.Clear();
        }
    }
}
