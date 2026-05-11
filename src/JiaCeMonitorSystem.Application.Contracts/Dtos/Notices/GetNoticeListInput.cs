using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Notices
{
    /// <summary>
    /// 获取系统通知列表输入参数
    /// </summary>
    public class GetNoticeListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询（标题、内容）
        /// </summary>
        public string? Filter { get; set; }
    }
}
