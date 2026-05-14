namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户状态
    /// </summary>
    public enum TenantStatus
    {
        /// <summary>
        /// 试用期
        /// </summary>
        Trial = 0,

        /// <summary>
        /// 正式运行
        /// </summary>
        Active = 1,

        /// <summary>
        /// 已到期
        /// </summary>
        Expired = 2,

        /// <summary>
        /// 已暂停
        /// </summary>
        Suspended = 3
    }
}
