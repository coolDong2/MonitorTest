using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.SystemDictionaries
{
    /// <summary>
    /// 系统字典数据传输对象
    /// </summary>
    public class SystemDictionaryDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 字典类型ID
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// 字典类型名称（冗余）
        /// </summary>
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 字典编码
        /// </summary>
        public string ItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 字典名称
        /// </summary>
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        /// 简拼
        /// </summary>
        public string? SimpleSpelling { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Layers { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
