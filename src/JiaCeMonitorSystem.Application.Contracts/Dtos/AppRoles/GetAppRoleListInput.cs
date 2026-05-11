using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.AppRoles
{
    /// <summary>
    /// 获取业务角色列表输入
    /// </summary>
    public class GetAppRoleListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 关键字（角色名称/编号）
        /// </summary>
        public string? Keyword { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public System.Guid? CompanyId { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public int? Category { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? EnabledMark { get; set; }
    }
}
