using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.ModuleButtons
{
    /// <summary>
    /// 获取系统菜单按钮列表输入参数
    /// </summary>
    public class GetModuleButtonListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 所属模块ID
        /// </summary>
        public Guid? ModuleId { get; set; }

        /// <summary>
        /// 模糊查询（编码、名称）
        /// </summary>
        public string? Filter { get; set; }
    }
}
