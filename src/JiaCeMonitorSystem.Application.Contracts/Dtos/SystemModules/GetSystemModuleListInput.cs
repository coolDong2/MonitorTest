using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.SystemModules
{
    /// <summary>
    /// 获取系统菜单模块列表输入参数
    /// </summary>
    public class GetSystemModuleListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询（编码、名称）
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public System.Guid? ParentId { get; set; }
    }
}
