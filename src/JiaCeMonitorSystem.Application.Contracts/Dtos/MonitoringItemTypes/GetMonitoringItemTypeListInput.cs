using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.MonitoringItemTypes
{
    /// <summary>
    /// 获取监测项目类型列表输入参数
    /// </summary>
    public class GetMonitoringItemTypeListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询（编码、名称）
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 监测分类筛选
        /// </summary>
        public int? Category { get; set; }
    }
}
