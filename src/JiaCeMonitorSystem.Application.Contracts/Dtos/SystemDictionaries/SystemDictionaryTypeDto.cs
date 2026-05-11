using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.SystemDictionaries
{
    /// <summary>
    /// 系统字典类型数据传输对象
    /// </summary>
    public class SystemDictionaryTypeDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 是否树形
        /// </summary>
        public bool IsTree { get; set; }

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

        /// <summary>
        /// 子节点列表
        /// </summary>
        public List<SystemDictionaryTypeTreeDto> Children { get; set; } = new List<SystemDictionaryTypeTreeDto>();
    }

    /// <summary>
    /// 系统字典类型树形数据传输对象
    /// </summary>
    public class SystemDictionaryTypeTreeDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 是否树形
        /// </summary>
        public bool IsTree { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

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
        /// 子节点列表
        /// </summary>
        public List<SystemDictionaryTypeTreeDto> Children { get; set; } = new List<SystemDictionaryTypeTreeDto>();
    }
}
