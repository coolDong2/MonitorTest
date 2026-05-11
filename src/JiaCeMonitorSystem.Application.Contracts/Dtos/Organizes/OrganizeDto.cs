using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Organizes
{
    /// <summary>
    /// 系统组织数据传输对象
    /// </summary>
    public class OrganizeDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Layers { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 简称
        /// </summary>
        public string? ShortName { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid? ManagerId { get; set; }

        /// <summary>
        /// 负责人姓名（冗余）
        /// </summary>
        public string? ManagerName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? TelePhone { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string? MobilePhone { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string? WeChat { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid? AreaId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }

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
        /// 子节点列表
        /// </summary>
        public List<OrganizeTreeDto> Children { get; set; } = new List<OrganizeTreeDto>();
    }

    /// <summary>
    /// 系统组织树形数据传输对象
    /// </summary>
    public class OrganizeTreeDto
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
        /// 全称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

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
        public List<OrganizeTreeDto> Children { get; set; } = new List<OrganizeTreeDto>();
    }
}
