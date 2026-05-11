using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.SystemModules
{
    /// <summary>
    /// 系统菜单模块聚合根
    /// 表名：JC_SystemModules
    /// </summary>
    public class SystemModule : FullAuditedAggregateRoot<Guid>
    {
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
        /// 链接地址
        /// </summary>
        public string? UrlAddress { get; private set; }

        /// <summary>
        /// 打开目标
        /// </summary>
        public string? Target { get; private set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool IsMenu { get; private set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand { get; private set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; private set; }

        /// <summary>
        /// 是否字段
        /// </summary>
        public bool IsFields { get; private set; }

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

        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; private set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Layers { get; private set; }

        private SystemModule()
        {
            EnCode = string.Empty;
            FullName = string.Empty;
        }

        /// <summary>
        /// 创建系统菜单模块
        /// </summary>
        public SystemModule(
            Guid id,
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark = true,
            Guid? parentId = null,
            int layers = 1,
            string? icon = null,
            string? urlAddress = null,
            string? target = null,
            bool isMenu = true,
            bool isExpand = false,
            bool isPublic = false,
            bool isFields = false,
            bool allowEdit = true,
            bool allowDelete = true,
            string? description = null,
            string? authorize = null)
            : base(id)
        {
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            ParentId = parentId;
            Layers = layers;
            Icon = icon;
            UrlAddress = urlAddress;
            Target = target;
            IsMenu = isMenu;
            IsExpand = isExpand;
            IsPublic = isPublic;
            IsFields = isFields;
            AllowEdit = allowEdit;
            AllowDelete = allowDelete;
            Description = description;
            Authorize = authorize;
        }

        /// <summary>
        /// 更新基础信息
        /// </summary>
        public void UpdateInfo(
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark,
            string? icon = null,
            string? urlAddress = null,
            string? target = null,
            bool isMenu = true,
            bool isExpand = false,
            bool isPublic = false,
            string? description = null,
            string? authorize = null)
        {
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            Icon = icon;
            UrlAddress = urlAddress;
            Target = target;
            IsMenu = isMenu;
            IsExpand = isExpand;
            IsPublic = isPublic;
            Description = description;
            Authorize = authorize;
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        public void SetLayers(int layers)
        {
            Layers = layers;
        }
    }
}
