using System;
using System.Collections.Generic;
using JiaCeMonitorSystem.TenantManagement;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户登录结果数据传输对象
    /// </summary>
    public class TenantLoginResultDto
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// 租户Id
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
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<string> Permissions { get; set; } = new();

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<TenantMenuDto> Menus { get; set; } = new();

        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }
    }
}
