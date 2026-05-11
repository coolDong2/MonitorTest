using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Roles
{
    /// <summary>
    /// 角色数据传输对象
    /// </summary>
    public class RoleDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 是否默认角色
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否公开角色
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string? Description { get; set; }
    }
}
