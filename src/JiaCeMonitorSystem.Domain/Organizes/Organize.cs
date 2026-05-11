using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.Organizes
{
    /// <summary>
    /// 系统组织聚合根
    /// 表名：JC_Organizes
    /// </summary>
    public class Organize : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; private set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Layers { get; private set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; private set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string? ShortName { get; private set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid? CategoryId { get; private set; }

        /// <summary>
        /// 负责人ID（关联IdentityUser）
        /// </summary>
        public Guid? ManagerId { get; private set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? TelePhone { get; private set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string? MobilePhone { get; private set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string? WeChat { get; private set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; private set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid? AreaId { get; private set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; private set; }

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

        private Organize()
        {
            EnCode = string.Empty;
            FullName = string.Empty;
        }

        /// <summary>
        /// 创建系统组织
        /// </summary>
        public Organize(
            Guid id,
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark = true,
            Guid? parentId = null,
            int layers = 1,
            string? shortName = null,
            Guid? categoryId = null,
            Guid? managerId = null,
            string? telePhone = null,
            string? mobilePhone = null,
            string? weChat = null,
            string? fax = null,
            string? email = null,
            Guid? areaId = null,
            string? address = null,
            bool allowEdit = true,
            bool allowDelete = true,
            string? description = null)
            : base(id)
        {
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            ParentId = parentId;
            Layers = layers;
            ShortName = shortName;
            CategoryId = categoryId;
            ManagerId = managerId;
            TelePhone = telePhone;
            MobilePhone = mobilePhone;
            WeChat = weChat;
            Fax = fax;
            Email = email;
            AreaId = areaId;
            Address = address;
            AllowEdit = allowEdit;
            AllowDelete = allowDelete;
            Description = description;
        }

        /// <summary>
        /// 更新组织信息
        /// </summary>
        public void UpdateInfo(
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark,
            string? shortName = null,
            Guid? categoryId = null,
            Guid? managerId = null,
            string? telePhone = null,
            string? mobilePhone = null,
            string? weChat = null,
            string? fax = null,
            string? email = null,
            Guid? areaId = null,
            string? address = null,
            string? description = null)
        {
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            ShortName = shortName;
            CategoryId = categoryId;
            ManagerId = managerId;
            TelePhone = telePhone;
            MobilePhone = mobilePhone;
            WeChat = weChat;
            Fax = fax;
            Email = email;
            AreaId = areaId;
            Address = address;
            Description = description;
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
