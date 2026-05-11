using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Organizes
{
    /// <summary>
    /// 获取系统组织列表输入参数
    /// </summary>
    public class GetOrganizeListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询（编码、名称）
        /// </summary>
        public string? Filter { get; set; }
    }
}
