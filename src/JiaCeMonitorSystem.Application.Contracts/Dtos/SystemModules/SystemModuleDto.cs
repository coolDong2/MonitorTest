using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.SystemModules
{
    /// <summary>
    /// 系统菜单模块数据传输对象
    /// </summary>
    public class SystemModuleDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string? UrlAddress { get; set; }

        /// <summary>
        /// 打开目标
        /// </summary>
        public string? Target { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand { get; set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 是否字段
        /// </summary>
        public bool IsFields { get; set; }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool AllowEdit { get; set; }

        /// <summary>
        /// 允许删除
        /// </summary>
        public bool AllowDelete { get; set; }

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
        /// 授权
        /// </summary>
        public string? Authorize { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Layers { get; set; }

        /// <summary>
        /// 子节点列表
        /// </summary>
        public List<SystemModuleTreeDto> Children { get; set; } = new List<SystemModuleTreeDto>();
    }
}
