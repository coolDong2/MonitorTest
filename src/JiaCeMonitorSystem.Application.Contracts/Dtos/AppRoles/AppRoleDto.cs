using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.AppRoles
{
    /// <summary>
    /// 业务角色数据传输对象
    /// </summary>
    public class AppRoleDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 所属公司名称
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 角色名称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 角色类型
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 角色类型名称
        /// </summary>
        public string? Type { get; set; }

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
        /// 权限按钮ID
        /// </summary>
        public string? PermissionButtonIds { get; set; }

        /// <summary>
        /// 权限字段ID
        /// </summary>
        public string? PermissionFieldsIds { get; set; }
    }
}
