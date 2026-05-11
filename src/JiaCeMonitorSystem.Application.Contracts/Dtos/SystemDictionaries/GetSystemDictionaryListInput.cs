using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.SystemDictionaries
{
    /// <summary>
    /// 获取系统字典列表输入参数
    /// </summary>
    public class GetSystemDictionaryListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 字典类型ID
        /// </summary>
        public Guid? ItemId { get; set; }

        /// <summary>
        /// 模糊查询（编码、名称）
        /// </summary>
        public string? Filter { get; set; }
    }
}
