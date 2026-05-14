using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户配置聚合根，管理租户的生命周期、数据库策略与许可证配额
    /// </summary>
    public class TenantConfiguration : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 关联租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 是否使用独立数据库
        /// </summary>
        public bool IsIndependentDatabase { get; set; }

        /// <summary>
        /// 独立数据库连接字符串（加密存储）
        /// </summary>
        public string? IndependentConnectionString { get; set; }

        /// <summary>
        /// 最大用户数量
        /// </summary>
        public int? MaxUserCount { get; set; }

        /// <summary>
        /// 最大存储容量（字节）
        /// </summary>
        public long? MaxStorageBytes { get; set; }

        /// <summary>
        /// 最大工程数量
        /// </summary>
        public int? MaxProjectCount { get; set; }

        /// <summary>
        /// 最大测点数量
        /// </summary>
        public int? MaxPointCount { get; set; }

        /// <summary>
        /// 到期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 提醒日期
        /// </summary>
        public DateTime? RemindDate { get; set; }

        /// <summary>
        /// 租户状态
        /// </summary>
        public TenantStatus Status { get; set; }

        /// <summary>
        /// 许可证密钥
        /// </summary>
        public string? LicenseKey { get; set; }

        /// <summary>
        /// 证书信息
        /// </summary>
        public string? CertificateInfo { get; set; }

        /// <summary>
        /// 单位编码，用于租户用户登录
        /// </summary>
        public string? UnitCode { get; set; }

        protected TenantConfiguration()
        {
        }

        public TenantConfiguration(Guid id, Guid tenantId, string? unitCode = null)
        {
            Id = id;
            TenantId = tenantId;
            UnitCode = unitCode;
            Status = TenantStatus.Active;
            IsIndependentDatabase = false;
        }
    }
}
