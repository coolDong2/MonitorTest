using System;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 按钮权限数据传输对象
    /// </summary>
    public class ButtonPermissionDto
    {
        /// <summary>
        /// 按钮ID
        /// </summary>
        public Guid ButtonId { get; set; }

        /// <summary>
        /// 按钮编码
        /// </summary>
        public string EnCode { get; set; } = string.Empty;

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 所属模块ID
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsGranted { get; set; }
    }
}
