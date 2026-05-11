using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.MonitoringData
{
    /// <summary>
    /// 获取监测数据列表输入参数
    /// </summary>
    public class GetMonitoringDataListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 测点ID筛选
        /// </summary>
        public Guid? PointId { get; set; }

        /// <summary>
        /// 项目ID筛选
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 数据质量筛选
        /// </summary>
        public int? DataQuality { get; set; }

        /// <summary>
        /// 监测属性ID筛选
        /// 【重构新增】支持按属性筛选
        /// </summary>
        public Guid? PropertyId { get; set; }

        /// <summary>
        /// 模糊查询（测点名称、属性名称）
        /// </summary>
        public string? Filter { get; set; }
    }
}
