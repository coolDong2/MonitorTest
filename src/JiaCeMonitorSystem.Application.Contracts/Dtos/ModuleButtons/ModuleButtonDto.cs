using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.ModuleButtons
{
    /// <summary>
    /// 系统菜单按钮数据传输对象
    /// </summary>
    public class ModuleButtonDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 所属模块ID
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// 模块名称（冗余）
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

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
        /// 按钮位置
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// 按钮位置文本
        /// </summary>
        public string LocationText { get; set; } = string.Empty;

        /// <summary>
        /// JS事件
        /// </summary>
        public string? JsEvent { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string? UrlAddress { get; set; }

        /// <summary>
        /// 是否有分割线
        /// </summary>
        public bool Split { get; set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; set; }

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
    }
}
