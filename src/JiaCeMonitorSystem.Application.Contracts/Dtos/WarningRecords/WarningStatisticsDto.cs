using System;

namespace JiaCeMonitorSystem.Dtos.WarningRecords
{
    /// <summary>
    /// 预警统计数据传输对象
    /// </summary>
    public class WarningStatisticsDto
    {
        /// <summary>
        /// 总预警数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 未处理数
        /// </summary>
        public int UnhandledCount { get; set; }

        /// <summary>
        /// 处理中数
        /// </summary>
        public int HandlingCount { get; set; }

        /// <summary>
        /// 已处理数
        /// </summary>
        public int HandledCount { get; set; }

        /// <summary>
        /// 已确认数
        /// </summary>
        public int ConfirmedCount { get; set; }

        /// <summary>
        /// 已关闭数
        /// </summary>
        public int ClosedCount { get; set; }

        /// <summary>
        /// 一级预警数
        /// </summary>
        public int Level1Count { get; set; }

        /// <summary>
        /// 二级预警数
        /// </summary>
        public int Level2Count { get; set; }

        /// <summary>
        /// 三级预警数
        /// </summary>
        public int Level3Count { get; set; }
    }
}
