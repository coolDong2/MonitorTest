using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Organizes
{
    /// <summary>
    /// 创建系统组织输入参数
    /// </summary>
    public class OrganizeCreateDto
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 全称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(100)]
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
        /// 电话
        /// </summary>
        [StringLength(50)]
        public string? TelePhone { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [StringLength(50)]
        public string? MobilePhone { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        [StringLength(100)]
        public string? WeChat { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [StringLength(50)]
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid? AreaId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(500)]
        public string? Address { get; set; }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool AllowEdit { get; set; } = true;

        /// <summary>
        /// 允许删除
        /// </summary>
        public bool AllowDelete { get; set; } = true;

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; } = true;

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
