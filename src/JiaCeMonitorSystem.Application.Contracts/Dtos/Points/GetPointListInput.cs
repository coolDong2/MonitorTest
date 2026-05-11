using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Points
{
    /// <summary>
    /// 获取测点列表输入参数
    /// </summary>
    public class GetPointListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询关键字（匹配测点编号/名称）
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 所属项目ID筛选
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// 当前预警级别筛选
        /// </summary>
        public int? CurrentWarningLevel { get; set; }
    }
}
