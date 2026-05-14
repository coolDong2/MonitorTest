namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        SystemAdmin = 0,

        /// <summary>
        /// 租户管理员
        /// </summary>
        TenantAdmin = 1,

        /// <summary>
        /// 租户普通用户
        /// </summary>
        TenantUser = 2
    }
}
