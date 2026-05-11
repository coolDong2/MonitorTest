using System;
using JiaCeMonitorSystem.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.WarningRecords
{
    /// <summary>
    /// 预警记录实体，记录单次预警的完整生命周期
    /// </summary>
    public class WarningRecord : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 监测点ID
        /// </summary>
        public Guid PointId { get; private set; }

        /// <summary>
        /// 测点名称（冗余，便于列表展示）
        /// </summary>
        public string PointName { get; private set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; private set; }

        /// <summary>
        /// 项目名称（冗余，便于列表展示）
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// 监测项目类型ID
        /// </summary>
        public Guid? ItemTypeId { get; private set; }

        /// <summary>
        /// 监测项目类型名称（冗余，便于列表展示）
        /// </summary>
        public string? ItemTypeName { get; private set; }

        /// <summary>
        /// 监测属性ID（外键，关联MonitoringItemProperty）
        /// 【重构新增】解决同一测点下多属性数据无法区分的问题（如水平位移 vs 垂直位移）
        /// </summary>
        public Guid PropertyId { get; private set; }

        /// <summary>
        /// 属性编码（冗余，如 DISPLACEMENT_X）
        /// </summary>
        public string PropertyCode { get; private set; }

        /// <summary>
        /// 属性名称（冗余，如"水平位移"）
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// 单位（冗余，如 mm，直接从属性继承，避免历史数据展示时联表）
        /// </summary>
        public string? Unit { get; private set; }

        /// <summary>
        /// 触发该预警的监测数据ID
        /// </summary>
        public Guid? MonitoringDataId { get; private set; }

        /// <summary>
        /// 数据状态（冗余，来自监测数据）
        /// </summary>
        public DataState DataState { get; private set; }

        /// <summary>
        /// 采集人姓名（冗余，来自监测数据）
        /// </summary>
        public string? CollectorName { get; private set; }

        /// <summary>
        /// 数据备注（冗余，来自监测数据）
        /// </summary>
        public string? DataRemark { get; private set; }

        /// <summary>
        /// 预警时间（通常等于监测时间）
        /// </summary>
        public DateTime WarningTime { get; private set; }

        /// <summary>
        /// 触发监测时间
        /// </summary>
        public DateTime MonitoringTime { get; private set; }

        /// <summary>
        /// 触发监测值
        /// </summary>
        public decimal MonitoringValue { get; private set; }

        /// <summary>
        /// 预警类型
        /// </summary>
        public WarningType WarningType { get; private set; }

        /// <summary>
        /// 预警级别
        /// </summary>
        public WarningLevel WarningLevel { get; private set; }

        /// <summary>
        /// 触发值
        /// </summary>
        public decimal TriggerValue { get; private set; }

        /// <summary>
        /// 阈值设定值
        /// </summary>
        public decimal ThresholdValue { get; private set; }

        /// <summary>
        /// 前次监测值
        /// </summary>
        public decimal? PreviousValue { get; private set; }

        /// <summary>
        /// 变化率（%）
        /// </summary>
        public decimal? ChangeRate { get; private set; }

        /// <summary>
        /// 累计变化量
        /// </summary>
        public decimal? CumulativeChange { get; private set; }

        /// <summary>
        /// 预警内容描述
        /// </summary>
        public string WarningContent { get; private set; }

        /// <summary>
        /// 建议措施
        /// </summary>
        public string? SuggestedAction { get; private set; }

        /// <summary>
        /// 处理负责人ID
        /// </summary>
        public Guid? HandlerId { get; private set; }

        /// <summary>
        /// 处理负责人姓名
        /// </summary>
        public string? HandlerName { get; private set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public HandleStatus HandleStatus { get; private set; }

        /// <summary>
        /// 处理完成时间
        /// </summary>
        public DateTime? HandleTime { get; private set; }

        /// <summary>
        /// 处理方案
        /// </summary>
        public string? HandleSolution { get; private set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string? HandleResult { get; private set; }

        /// <summary>
        /// 确认人ID
        /// </summary>
        public Guid? ConfirmerId { get; private set; }

        /// <summary>
        /// 确认人姓名
        /// </summary>
        public string? ConfirmerName { get; private set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmTime { get; private set; }

        /// <summary>
        /// 确认备注
        /// </summary>
        public string? ConfirmRemark { get; private set; }

        private WarningRecord()
        {
            WarningContent = string.Empty;
            PropertyCode = string.Empty;
            PropertyName = string.Empty;
            PointName = string.Empty;
            ProjectName = string.Empty;
        }

        /// <summary>
        /// 创建预警记录
        /// </summary>
        public WarningRecord(
            Guid id,
            Guid pointId,
            string pointName,
            Guid projectId,
            string projectName,
            DateTime monitoringTime,
            decimal monitoringValue,
            WarningType warningType,
            WarningLevel warningLevel,
            decimal triggerValue,
            decimal thresholdValue,
            string warningContent,
            Guid propertyId,
            string propertyCode,
            string propertyName,
            string? unit = null,
            Guid? monitoringDataId = null,
            Guid? itemTypeId = null,
            string? itemTypeName = null,
            DataState dataState = DataState.Raw,
            string? collectorName = null,
            string? dataRemark = null,
            decimal? previousValue = null,
            decimal? changeRate = null,
            decimal? cumulativeChange = null,
            string? suggestedAction = null)
            : base(id)
        {
            PointId = pointId;
            PointName = pointName;
            ProjectId = projectId;
            ProjectName = projectName;
            PropertyId = propertyId;
            PropertyCode = propertyCode;
            PropertyName = propertyName;
            Unit = unit;
            MonitoringDataId = monitoringDataId;
            ItemTypeId = itemTypeId;
            ItemTypeName = itemTypeName;
            DataState = dataState;
            CollectorName = collectorName;
            DataRemark = dataRemark;
            WarningTime = DateTime.UtcNow;
            MonitoringTime = monitoringTime;
            MonitoringValue = monitoringValue;
            WarningType = warningType;
            WarningLevel = warningLevel;
            TriggerValue = triggerValue;
            ThresholdValue = thresholdValue;
            PreviousValue = previousValue;
            ChangeRate = changeRate;
            CumulativeChange = cumulativeChange;
            WarningContent = warningContent;
            SuggestedAction = suggestedAction;
            HandleStatus = HandleStatus.Unhandled;
        }

        /// <summary>
        /// 分配处理人（未处理→处理中）
        /// </summary>
        public void AssignHandler(Guid handlerId, string handlerName)
        {
            if (HandleStatus != HandleStatus.Unhandled)
            {
                throw new BusinessException(ErrorCodes.Warning_InvalidStatusTransition)
                    .WithData("CurrentStatus", HandleStatus);
            }

            HandlerId = handlerId;
            HandlerName = handlerName;
            HandleStatus = HandleStatus.InProgress;
        }

        /// <summary>
        /// 提交处理方案（处理中→已处理）
        /// </summary>
        public void SubmitSolution(string solution, string? result = null)
        {
            if (HandleStatus != HandleStatus.InProgress)
            {
                throw new BusinessException(ErrorCodes.Warning_InvalidStatusTransition)
                    .WithData("CurrentStatus", HandleStatus);
            }

            if (HandlerId == null)
            {
                throw new BusinessException(ErrorCodes.Warning_HandlerNotAssigned);
            }

            HandleSolution = solution;
            HandleResult = result;
            HandleTime = DateTime.UtcNow;
            HandleStatus = HandleStatus.Handled;
        }

        /// <summary>
        /// 管理员确认（已处理→已确认）
        /// </summary>
        public void Confirm(Guid confirmerId, string confirmerName, string? remark = null)
        {
            if (HandleStatus != HandleStatus.Handled)
            {
                throw new BusinessException(ErrorCodes.Warning_InvalidStatusTransition)
                    .WithData("CurrentStatus", HandleStatus);
            }

            ConfirmerId = confirmerId;
            ConfirmerName = confirmerName;
            ConfirmTime = DateTime.UtcNow;
            ConfirmRemark = remark;
        }

        /// <summary>
        /// 关闭预警（未处理/处理中→已关闭）
        /// </summary>
        public void Close(string reason)
        {
            if (HandleStatus == HandleStatus.Closed)
            {
                throw new BusinessException(ErrorCodes.Warning_AlreadyClosed);
            }

            HandleStatus = HandleStatus.Closed;
            HandleResult = $"已关闭：{reason}";
            HandleTime = DateTime.UtcNow;
        }

        /// <summary>
        /// 驳回处理结果（已处理→处理中）
        /// </summary>
        public void Reject(string reason)
        {
            if (HandleStatus != HandleStatus.Handled)
            {
                throw new BusinessException(ErrorCodes.Warning_InvalidStatusTransition)
                    .WithData("CurrentStatus", HandleStatus);
            }

            HandleStatus = HandleStatus.InProgress;
            HandleSolution = null;
            HandleResult = $"驳回：{reason}";
            HandleTime = null;
        }
    }
}
