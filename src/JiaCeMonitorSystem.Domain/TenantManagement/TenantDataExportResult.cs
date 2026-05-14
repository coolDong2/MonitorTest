namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户数据导出结果
    /// </summary>
    public class TenantDataExportResult
    {
        /// <summary>IdentityUser 迁移数量</summary>
        public int IdentityUserCount { get; set; }

        /// <summary>IdentityRole 迁移数量</summary>
        public int IdentityRoleCount { get; set; }

        /// <summary>IdentityUserRole 迁移数量</summary>
        public int IdentityUserRoleCount { get; set; }

        /// <summary>IdentityUserClaim 迁移数量</summary>
        public int IdentityUserClaimCount { get; set; }

        /// <summary>IdentityRoleClaim 迁移数量</summary>
        public int IdentityRoleClaimCount { get; set; }

        /// <summary>IdentityUserLogin 迁移数量</summary>
        public int IdentityUserLoginCount { get; set; }

        /// <summary>IdentityUserToken 迁移数量</summary>
        public int IdentityUserTokenCount { get; set; }

        /// <summary>PermissionGrant 迁移数量</summary>
        public int PermissionGrantCount { get; set; }

        /// <summary>TenantModuleGrant 迁移数量</summary>
        public int TenantModuleGrantCount { get; set; }

        /// <summary>TenantButtonPermission 迁移数量</summary>
        public int TenantButtonPermissionCount { get; set; }

        /// <summary>TenantUserExtension 迁移数量</summary>
        public int TenantUserExtensionCount { get; set; }

        /// <summary>是否成功</summary>
        public bool Succeeded { get; set; }

        /// <summary>错误信息</summary>
        public string? ErrorMessage { get; set; }
    }
}
