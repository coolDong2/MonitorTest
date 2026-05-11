using System;
using JiaCeMonitorSystem.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.ModuleButtons
{
    /// <summary>
    /// 系统菜单按钮聚合根
    /// 表名：JC_ModuleButtons
    /// </summary>
    public class ModuleButton : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属模块ID
        /// </summary>
        public Guid ModuleId { get; private set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon { get; private set; }

        /// <summary>
        /// 按钮位置
        /// </summary>
        public ModuleButtonLocation Location { get; private set; }

        /// <summary>
        /// JS事件
        /// </summary>
        public string? JsEvent { get; private set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string? UrlAddress { get; private set; }

        /// <summary>
        /// 是否有分割线
        /// </summary>
        public bool Split { get; private set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; private set; }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool AllowEdit { get; private set; }

        /// <summary>
        /// 允许删除
        /// </summary>
        public bool AllowDelete { get; private set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// 授权
        /// </summary>
        public string? Authorize { get; private set; }

        private ModuleButton()
        {
            EnCode = string.Empty;
            FullName = string.Empty;
        }

        /// <summary>
        /// 创建菜单按钮
        /// </summary>
        public ModuleButton(
            Guid id,
            Guid moduleId,
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark = true,
            ModuleButtonLocation location = ModuleButtonLocation.Toolbar,
            string? icon = null,
            string? jsEvent = null,
            string? urlAddress = null,
            bool split = false,
            bool isPublic = false,
            bool allowEdit = true,
            bool allowDelete = true,
            string? description = null,
            string? authorize = null)
            : base(id)
        {
            ModuleId = moduleId;
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            Location = location;
            Icon = icon;
            JsEvent = jsEvent;
            UrlAddress = urlAddress;
            Split = split;
            IsPublic = isPublic;
            AllowEdit = allowEdit;
            AllowDelete = allowDelete;
            Description = description;
            Authorize = authorize;
        }

        /// <summary>
        /// 更新按钮信息
        /// </summary>
        public void UpdateInfo(
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark,
            ModuleButtonLocation location = ModuleButtonLocation.Toolbar,
            string? icon = null,
            string? jsEvent = null,
            string? urlAddress = null,
            bool split = false,
            bool isPublic = false,
            string? description = null,
            string? authorize = null)
        {
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            Location = location;
            Icon = icon;
            JsEvent = jsEvent;
            UrlAddress = urlAddress;
            Split = split;
            IsPublic = isPublic;
            Description = description;
            Authorize = authorize;
        }
    }
}
