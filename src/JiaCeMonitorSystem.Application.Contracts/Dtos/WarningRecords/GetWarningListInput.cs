using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.WarningRecords
{
    /// <summary>
    /// 获取预警记录列表输入参数
    /// </summary>
    public class GetWarningListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 项目ID筛选
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// 测点ID筛选
        /// </summary>
        public Guid? PointId { get; set; }

        /// <summary>
        /// 预警级别筛选
        /// </summary>
        public int? WarningLevel { get; set; }

        /// <summary>
        /// 处理状态筛选
        /// </summary>
        public int? HandleStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 监测属性ID筛选
        /// 【重构新增】支持按属性筛选
        /// </summary>
        public Guid? PropertyId { get; set; }

        /// <summary>
        /// 模糊查询
        /// </summary>
        public string? Filter { get; set; }
    }
}
