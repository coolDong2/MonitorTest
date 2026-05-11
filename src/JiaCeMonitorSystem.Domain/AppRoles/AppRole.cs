using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.AppRoles
{
    /// <summary>
    /// 业务角色聚合根
    /// 表名：JC_AppRoles
    /// </summary>
    public class AppRole : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid? CompanyId { get; private set; }

        /// <summary>
        /// 所属公司名称
        /// </summary>
        public string? CompanyName { get; private set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string EnCode { get; private set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public int Category { get; private set; }

        /// <summary>
        /// 角色类型名称
        /// </summary>
        public string? Type { get; private set; }

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
        /// 权限按钮ID
        /// </summary>
        public string? PermissionButtonIds { get; private set; }

        /// <summary>
        /// 权限字段ID
        /// </summary>
        public string? PermissionFieldsIds { get; private set; }

        private AppRole()
        {
            EnCode = string.Empty;
            FullName = string.Empty;
        }

        /// <summary>
        /// 创建业务角色
        /// </summary>
        public AppRole(
            Guid id,
            string enCode,
            string fullName,
            int category,
            int sortCode,
            bool enabledMark = true,
            Guid? companyId = null,
            string? companyName = null,
            string? type = null,
            bool allowEdit = true,
            bool allowDelete = true,
            string? description = null,
            string? permissionButtonIds = null,
            string? permissionFieldsIds = null)
            : base(id)
        {
            EnCode = enCode;
            FullName = fullName;
            Category = category;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            CompanyId = companyId;
            CompanyName = companyName;
            Type = type;
            AllowEdit = allowEdit;
            AllowDelete = allowDelete;
            Description = description;
            PermissionButtonIds = permissionButtonIds;
            PermissionFieldsIds = permissionFieldsIds;
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        public void UpdateInfo(
            string enCode,
            string fullName,
            int category,
            int sortCode,
            bool enabledMark,
            Guid? companyId = null,
            string? companyName = null,
            string? type = null,
            bool allowEdit = true,
            bool allowDelete = true,
            string? description = null,
            string? permissionButtonIds = null,
            string? permissionFieldsIds = null)
        {
            EnCode = enCode;
            FullName = fullName;
            Category = category;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            CompanyId = companyId;
            CompanyName = companyName;
            Type = type;
            AllowEdit = allowEdit;
            AllowDelete = allowDelete;
            Description = description;
            PermissionButtonIds = permissionButtonIds;
            PermissionFieldsIds = permissionFieldsIds;
        }

        /// <summary>
        /// 设置权限按钮ID
        /// </summary>
        public void SetPermissionButtonIds(string? permissionButtonIds)
        {
            PermissionButtonIds = permissionButtonIds;
        }

        /// <summary>
        /// 设置权限字段ID
        /// </summary>
        public void SetPermissionFieldsIds(string? permissionFieldsIds)
        {
            PermissionFieldsIds = permissionFieldsIds;
        }
    }
}
