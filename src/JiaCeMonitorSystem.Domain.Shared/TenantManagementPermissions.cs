namespace JiaCeMonitorSystem.Permissions
{
    /// <summary>
    /// 租户管理权限常量定义（SaaS 化重构新增）
    /// </summary>
    public static class TenantManagementPermissions
    {
        /// <summary>
        /// 权限分组名称
        /// </summary>
        public const string GroupName = "TenantManagement";

        /// <summary>
        /// 租户管理基础权限
        /// </summary>
        public const string Tenants = GroupName + ".Tenants";
        public const string Tenants_Create = Tenants + ".Create";
        public const string Tenants_Edit = Tenants + ".Edit";
        public const string Tenants_Delete = Tenants + ".Delete";
        public const string Tenants_Configure = Tenants + ".Configure";

        /// <summary>
        /// 套餐管理权限
        /// </summary>
        public const string Packages = GroupName + ".Packages";
        public const string Packages_Create = Packages + ".Create";
        public const string Packages_Edit = Packages + ".Edit";

        /// <summary>
        /// 数据库管理权限
        /// </summary>
        public const string Database = GroupName + ".Database";
        public const string Database_Manage = Database + ".Manage";
    }
}
