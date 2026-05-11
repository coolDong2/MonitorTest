using System;
using System.Text.Json;
using JiaCeMonitorSystem.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.Points
{
    /// <summary>
    /// 测点聚合根，管理监测点配置、当前状态与统计信息
    /// </summary>
    public class Point : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属项目ID
        /// </summary>
        public Guid ProjectId { get; private set; }

        /// <summary>
        /// 监测点编号
        /// </summary>
        public string PointCode { get; private set; }

        /// <summary>
        /// 监测点名称
        /// </summary>
        public string PointName { get; private set; }

        /// <summary>
        /// 监测项目类型ID
        /// </summary>
        public Guid? ItemTypeId { get; private set; }

        /// <summary>
        /// 监测项目类型名称（冗余，便于列表展示）
        /// </summary>
        public string? ItemTypeName { get; private set; }

        /// <summary>
        /// X坐标/经度
        /// </summary>
        public decimal? LocationX { get; private set; }

        /// <summary>
        /// Y坐标/纬度
        /// </summary>
        public decimal? LocationY { get; private set; }

        /// <summary>
        /// Z坐标/高程
        /// </summary>
        public decimal? LocationZ { get; private set; }

        /// <summary>
        /// 当前监测值
        /// </summary>
        public decimal? CurrentValue { get; private set; }

        /// <summary>
        /// 最后监测时间
        /// </summary>
        public DateTime? LastMonitoringTime { get; private set; }

        /// <summary>
        /// 监测频率（天）
        /// </summary>
        public int? MonitoringFrequency { get; private set; }

        /// <summary>
        /// 历史最大值
        /// </summary>
        public decimal? MaxValue { get; private set; }

        /// <summary>
        /// 历史最小值
        /// </summary>
        public decimal? MinValue { get; private set; }

        /// <summary>
        /// 历史平均值
        /// </summary>
        public decimal? AverageValue { get; private set; }

        /// <summary>
        /// 数据点数
        /// </summary>
        public int DataCount { get; private set; }

        /// <summary>
        /// 预警阈值 - 当监测值达到此值时触发一级预警
        /// </summary>
        public decimal? WarningThreshold { get; private set; }

        /// <summary>
        /// 报警阈值 - 当监测值达到此值时触发二级报警
        /// </summary>
        public decimal? AlarmThreshold { get; private set; }

        /// <summary>
        /// 变化率阈值 - 监测值单位时间内的变化率超过此值时触发预警
        /// </summary>
        public decimal? ChangeRateThreshold { get; private set; }

        /// <summary>
        /// 累计变化阈值 - 从初始值开始的累计变化量超过此值时触发预警
        /// </summary>
        public decimal? CumulativeThreshold { get; private set; }

        /// <summary>
        /// 当前预警级别 - 0:无预警 1:一级预警 2:二级预警 3:三级预警
        /// </summary>
        public WarningLevel CurrentWarningLevel { get; private set; }

        /// <summary>
        /// 最后预警时间
        /// </summary>
        public DateTime? LastWarningTime { get; private set; }

        /// <summary>
        /// 总预警次数
        /// </summary>
        public int TotalWarningCount { get; private set; }

        /// <summary>
        /// 当前活跃预警数
        /// </summary>
        public int ActiveWarningCount { get; private set; }

        /// <summary>
        /// 扩展属性 - JSON格式存储的监测点扩展属性
        /// </summary>
        public JsonDocument? ExtendedProperties { get; private set; }

        /// <summary>
        /// 点位描述
        /// </summary>
        public string? Description { get; private set; }

        private Point()
        {
            PointCode = string.Empty;
            PointName = string.Empty;
        }

        /// <summary>
        /// 创建测点实体
        /// </summary>
        public Point(
            Guid id,
            Guid projectId,
            string pointCode,
            string pointName,
            Guid? itemTypeId = null,
            string? itemTypeName = null,
            decimal? locationX = null,
            decimal? locationY = null,
            decimal? locationZ = null,
            int? monitoringFrequency = null,
            decimal? warningThreshold = null,
            decimal? alarmThreshold = null,
            decimal? changeRateThreshold = null,
            decimal? cumulativeThreshold = null,
            JsonDocument? extendedProperties = null,
            string? description = null)
            : base(id)
        {
            ProjectId = projectId;
            PointCode = pointCode;
            PointName = pointName;
            ItemTypeId = itemTypeId;
            ItemTypeName = itemTypeName;
            LocationX = locationX;
            LocationY = locationY;
            LocationZ = locationZ;
            MonitoringFrequency = monitoringFrequency;
            WarningThreshold = warningThreshold;
            AlarmThreshold = alarmThreshold;
            ChangeRateThreshold = changeRateThreshold;
            CumulativeThreshold = cumulativeThreshold;
            CurrentWarningLevel = WarningLevel.Hint;
            TotalWarningCount = 0;
            ActiveWarningCount = 0;
            DataCount = 0;
            ExtendedProperties = extendedProperties;
            Description = description;
        }

        /// <summary>
        /// 更新测点基础信息
        /// </summary>
        public void UpdateInfo(
            string pointName,
            Guid? itemTypeId = null,
            string? itemTypeName = null,
            decimal? locationX = null,
            decimal? locationY = null,
            decimal? locationZ = null,
            int? monitoringFrequency = null,
            string? description = null)
        {
            PointName = pointName;
            ItemTypeId = itemTypeId;
            ItemTypeName = itemTypeName;
            LocationX = locationX;
            LocationY = locationY;
            LocationZ = locationZ;
            MonitoringFrequency = monitoringFrequency;
            Description = description;
        }

        /// <summary>
        /// 配置预警阈值
        /// </summary>
        public void ConfigureThresholds(
            decimal? warningThreshold,
            decimal? alarmThreshold,
            decimal? changeRateThreshold,
            decimal? cumulativeThreshold)
        {
            if (warningThreshold.HasValue && alarmThreshold.HasValue && warningThreshold >= alarmThreshold)
            {
                throw new BusinessException(ErrorCodes.Point_InvalidThreshold)
                    .WithData("Reason", "预警阈值必须小于报警阈值");
            }

            WarningThreshold = warningThreshold;
            AlarmThreshold = alarmThreshold;
            ChangeRateThreshold = changeRateThreshold;
            CumulativeThreshold = cumulativeThreshold;
        }

        /// <summary>
        /// 更新监测值与最后监测时间
        /// </summary>
        public void UpdateMonitoringValue(decimal value, DateTime time)
        {
            CurrentValue = value;
            LastMonitoringTime = time;
        }

        /// <summary>
        /// 更新测点统计值（最大/最小/平均/计数）
        /// </summary>
        public void UpdateStatistics(decimal newValue)
        {
            DataCount++;

            if (!MaxValue.HasValue || newValue > MaxValue.Value)
                MaxValue = newValue;

            if (!MinValue.HasValue || newValue < MinValue.Value)
                MinValue = newValue;

            if (!AverageValue.HasValue)
            {
                AverageValue = newValue;
            }
            else
            {
                AverageValue = (AverageValue.Value * (DataCount - 1) + newValue) / DataCount;
            }
        }

        /// <summary>
        /// 设置当前预警级别
        /// </summary>
        public void SetWarningLevel(WarningLevel level)
        {
            CurrentWarningLevel = level;
            LastWarningTime = DateTime.UtcNow;
            TotalWarningCount++;
            ActiveWarningCount++;
        }

        /// <summary>
        /// 预警解除后重置当前预警级别
        /// </summary>
        public void ResetWarningStatus()
        {
            CurrentWarningLevel = WarningLevel.Hint;
            if (ActiveWarningCount > 0)
                ActiveWarningCount--;
        }

        /// <summary>
        /// 校验是否允许删除（存在活跃预警时禁止删除）
        /// </summary>
        public void CheckCanDelete()
        {
            if (ActiveWarningCount > 0)
            {
                throw new BusinessException(ErrorCodes.Point_HasActiveWarnings);
            }
        }
    }
}
