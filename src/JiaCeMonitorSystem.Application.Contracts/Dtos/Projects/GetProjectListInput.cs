using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Projects
{
    /// <summary>
    /// 获取监测工程列表输入参数
    /// </summary>
    public class GetProjectListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询关键字（匹配项目编号/名称/地点）
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 项目状态筛选
        /// </summary>
        public int? Status { get; set; }
    }
}
