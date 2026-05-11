using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.SystemDictionaries
{
    /// <summary>
    /// 获取系统字典类型列表输入参数
    /// </summary>
    public class GetSystemDictionaryTypeListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询（编码、名称）
        /// </summary>
        public string? Filter { get; set; }
    }
}
