using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.MonitoringData
{
    /// <summary>
    /// 监测数据历史记录DTO（用于测点历史数据查询）
    /// </summary>
    public class MonitoringDataHistoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 测点ID
        /// </summary>
        public Guid PointId { get; set; }

        /// <summary>
        /// 快照时间（即监测时间）
        /// </summary>
        public DateTime SnapshotTime { get; set; }

        /// <summary>
        /// 格式化快照时间（用于显示）
        /// </summary>
        public string FormattedSnapshotTime { get; set; } = string.Empty;

        /// <summary>
        /// 监测数值
        /// </summary>
        public decimal MonitoringValue { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public int DataState { get; set; }

        /// <summary>
        /// 数据状态显示文本
        /// </summary>
        public string DataStateText { get; set; } = string.Empty;

        /// <summary>
        /// 收集人姓名
        /// </summary>
        public string? Collector { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string? CreatorUserName { get; set; }

        /// <summary>
        /// 格式化创建时间（用于显示）
        /// </summary>
        public string FormattedCreatorTime { get; set; } = string.Empty;
    }
}
