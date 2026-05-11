using System;
using System.Text.Json;
using JiaCeMonitorSystem.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.MonitoringData
{
    /// <summary>
    /// 监测数据实体，存储单次监测采集的原始数据
    /// </summary>
    public class MonitoringData : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 测点ID
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
        /// 监测时间
        /// </summary>
        public DateTime MonitoringTime { get; private set; }

        /// <summary>
        /// 监测数值
        /// </summary>
        public decimal MonitoringValue { get; private set; }

        /// <summary>
        /// 数据质量
        /// </summary>
        public DataQuality DataQuality { get; private set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public DataState DataState { get; private set; }

        /// <summary>
        /// 采集设备ID
        /// </summary>
        public Guid? DeviceId { get; private set; }

        /// <summary>
        /// 采集设备名称（冗余，便于列表展示）
        /// </summary>
        public string? DeviceName { get; private set; }

        /// <summary>
        /// 采集人ID
        /// </summary>
        public Guid? CollectorId { get; private set; }

        /// <summary>
        /// 采集人姓名（冗余，便于列表展示）
        /// </summary>
        public string? CollectorName { get; private set; }

        /// <summary>
        /// 采集方式
        /// </summary>
        public CollectionMethod CollectionMethod { get; private set; }

        /// <summary>
        /// 扩展监测数据 - JSON格式存储的扩展监测参数
        /// </summary>
        public JsonDocument? ExtendedData { get; private set; }

        /// <summary>
        /// 数据备注
        /// </summary>
        public string? DataRemark { get; private set; }

        private MonitoringData()
        {
            PropertyCode = string.Empty;
            PropertyName = string.Empty;
            PointName = string.Empty;
            ProjectName = string.Empty;
        }

        /// <summary>
        /// 创建监测数据实体
        /// </summary>
        public MonitoringData(
            Guid id,
            Guid pointId,
            string pointName,
            Guid projectId,
            string projectName,
            DateTime monitoringTime,
            decimal monitoringValue,
            Guid propertyId,
            string propertyCode,
            string propertyName,
            string? unit = null,
            Guid? itemTypeId = null,
            string? itemTypeName = null,
            DataQuality dataQuality = DataQuality.Normal,
            Guid? deviceId = null,
            string? deviceName = null,
            Guid? collectorId = null,
            string? collectorName = null,
            CollectionMethod collectionMethod = CollectionMethod.Manual,
            JsonDocument? extendedData = null,
            string? dataRemark = null)
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
            ItemTypeId = itemTypeId;
            ItemTypeName = itemTypeName;
            MonitoringTime = monitoringTime;
            MonitoringValue = monitoringValue;
            DataQuality = dataQuality;
            DataState = DataState.Raw;
            DeviceId = deviceId;
            DeviceName = deviceName;
            CollectorId = collectorId;
            CollectorName = collectorName;
            CollectionMethod = collectionMethod;
            ExtendedData = extendedData;
            DataRemark = dataRemark;

            ValidateData();
        }

        /// <summary>
        /// 数据校验（范围检查、时间有效性）
        /// </summary>
        public void ValidateData()
        {
            if (MonitoringTime > DateTime.UtcNow.AddMinutes(5))
            {
                throw new BusinessException(ErrorCodes.MonitoringData_FutureTime);
            }
        }

        /// <summary>
        /// 标记数据质量
        /// </summary>
        public void MarkQuality(DataQuality quality, string? remark = null)
        {
            DataQuality = quality;
            if (!string.IsNullOrWhiteSpace(remark))
            {
                DataRemark = remark;
            }
        }

        /// <summary>
        /// 审核数据（原始→已审核）
        /// </summary>
        public void ApproveData()
        {
            if (DataState != DataState.Raw)
            {
                throw new BusinessException(ErrorCodes.MonitoringData_AlreadyApproved);
            }

            DataState = DataState.Approved;
        }

        /// <summary>
        /// 归档数据
        /// </summary>
        public void ArchiveData()
        {
            DataState = DataState.Archived;
        }
    }
}
