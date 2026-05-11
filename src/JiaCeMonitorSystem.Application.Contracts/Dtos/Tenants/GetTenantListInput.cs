using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Tenants
{
    /// <summary>
    /// 获取租户列表输入参数
    /// </summary>
    public class GetTenantListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 租户名称模糊查询
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 是否已过期筛选
        /// </summary>
        public bool? IsExpired { get; set; }
    }
}
