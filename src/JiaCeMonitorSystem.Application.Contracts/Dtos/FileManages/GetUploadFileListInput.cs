using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.FileManages
{
    /// <summary>
    /// 获取文件管理列表输入参数
    /// </summary>
    public class GetUploadFileListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询（文件名称）
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 关键字（文件选择列表用）
        /// </summary>
        public string? Keyword { get; set; }

        /// <summary>
        /// 文件名称筛选
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// 文件类型筛选
        /// </summary>
        public int? FileType { get; set; }

        /// <summary>
        /// 文件归属筛选
        /// </summary>
        public string? FileBy { get; set; }

        /// <summary>
        /// 所属组织ID筛选
        /// </summary>
        public Guid? OrganizeId { get; set; }

        /// <summary>
        /// 是否启用筛选
        /// </summary>
        public bool? EnabledMark { get; set; }
    }
}
