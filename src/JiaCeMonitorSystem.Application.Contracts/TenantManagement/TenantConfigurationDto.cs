using System;
using System.Collections.Generic;
using JiaCeMonitorSystem.TenantManagement;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户配置数据传输对象
    /// </summary>
    public class TenantConfigurationDto : EntityDto<Guid>
    {
        /// <summary>
        /// 关联租户Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenantName { get; set; } = string.Empty;

        /// <summary>
        /// 单位编码
        /// </summary>
        public string UnitCode { get; set; } = string.Empty;

        /// <summary>
        /// 是否使用独立数据库
        /// </summary>
        public bool IsIndependentDatabase { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 租户状态
        /// </summary>
        public TenantStatus Status { get; set; }

        /// <summary>
        /// 最大用户数量
        /// </summary>
        public int? MaxUserCount { get; set; }

        /// <summary>
        /// 最大工程数量
        /// </summary>
        public int? MaxProjectCount { get; set; }

        /// <summary>
        /// 最大测点数量
        /// </summary>
        public int? MaxPointCount { get; set; }

        /// <summary>
        /// 已授权的模块列表
        /// </summary>
        public List<ModuleGrantDto> GrantedModules { get; set; } = new();
    }
}
