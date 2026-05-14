using System;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 模块授权信息数据传输对象
    /// </summary>
    public class ModuleGrantDto
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; } = string.Empty;

        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsGranted { get; set; }

        /// <summary>
        /// 授权日期
        /// </summary>
        public DateTime? GrantDate { get; set; }
    }
}
