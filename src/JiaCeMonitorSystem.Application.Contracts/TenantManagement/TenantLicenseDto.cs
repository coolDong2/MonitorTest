namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户许可证配额数据传输对象
    /// </summary>
    public class TenantLicenseDto
    {
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
        /// 最大存储容量（字节）
        /// </summary>
        public long? MaxStorageBytes { get; set; }
    }
}
