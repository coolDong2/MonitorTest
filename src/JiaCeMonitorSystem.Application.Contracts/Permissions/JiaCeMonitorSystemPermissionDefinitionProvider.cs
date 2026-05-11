using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace JiaCeMonitorSystem.Permissions
{
    /// <summary>
    /// 监测云平台权限定义提供者
    /// </summary>
    public class JiaCeMonitorSystemPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        /// <inheritdoc />
        public override void Define(IPermissionDefinitionContext context)
        {
            var monitoringGroup = context.AddGroup(
                "JiaCeMonitorSystem",
                L("Permission:JiaCeMonitorSystem")
            );

            // 监测工程权限
            var projects = monitoringGroup.AddPermission(Permissions.Projects_Default, L("Permission:Projects"));
            projects.AddChild(Permissions.Projects_Create, L("Permission:Create"));
            projects.AddChild(Permissions.Projects_Edit, L("Permission:Edit"));
            projects.AddChild(Permissions.Projects_Delete, L("Permission:Delete"));
            projects.AddChild(Permissions.Projects_Archive, L("Permission:Archive"));
            projects.AddChild(Permissions.Projects_Export, L("Permission:Export"));
            projects.AddChild(Permissions.Projects_ManagePersonnel, L("Permission:ManagePersonnel"));

            // 测点管理权限
            var points = monitoringGroup.AddPermission(Permissions.Points_Default, L("Permission:Points"));
            points.AddChild(Permissions.Points_Create, L("Permission:Create"));
            points.AddChild(Permissions.Points_Edit, L("Permission:Edit"));
            points.AddChild(Permissions.Points_Delete, L("Permission:Delete"));
            points.AddChild(Permissions.Points_Import, L("Permission:Import"));
            points.AddChild(Permissions.Points_Export, L("Permission:Export"));
            points.AddChild(Permissions.Points_ConfigureThreshold, L("Permission:ConfigureThreshold"));

            // 监测数据权限
            var monitoringData = monitoringGroup.AddPermission(Permissions.MonitoringData_Default, L("Permission:MonitoringData"));
            monitoringData.AddChild(Permissions.MonitoringData_Create, L("Permission:Create"));
            monitoringData.AddChild(Permissions.MonitoringData_Edit, L("Permission:Edit"));
            monitoringData.AddChild(Permissions.MonitoringData_Delete, L("Permission:Delete"));
            monitoringData.AddChild(Permissions.MonitoringData_Import, L("Permission:Import"));
            monitoringData.AddChild(Permissions.MonitoringData_Export, L("Permission:Export"));
            monitoringData.AddChild(Permissions.MonitoringData_Approve, L("Permission:Approve"));
            monitoringData.AddChild(Permissions.MonitoringData_QueryHistory, L("Permission:QueryHistory"));

            // 仪器设备权限
            var devices = monitoringGroup.AddPermission(Permissions.Devices_Default, L("Permission:Devices"));  
            devices.AddChild(Permissions.Devices_Create, L("Permission:Create"));
            devices.AddChild(Permissions.Devices_Edit, L("Permission:Edit"));
            devices.AddChild(Permissions.Devices_Delete, L("Permission:Delete"));
            devices.AddChild(Permissions.Devices_Calibrate, L("Permission:Calibrate"));
            devices.AddChild(Permissions.Devices_Lend, L("Permission:Lend"));
            devices.AddChild(Permissions.Devices_Return, L("Permission:Return"));
            devices.AddChild(Permissions.Devices_Scrap, L("Permission:Scrap"));

            // 预警记录权限
            var warnings = monitoringGroup.AddPermission(Permissions.Warnings_Default, L("Permission:Warnings"));
            warnings.AddChild(Permissions.Warnings_Handle, L("Permission:Handle"));
            warnings.AddChild(Permissions.Warnings_Confirm, L("Permission:Confirm"));
            warnings.AddChild(Permissions.Warnings_Close, L("Permission:Close"));
            warnings.AddChild(Permissions.Warnings_ViewStatistics, L("Permission:ViewStatistics"));
            warnings.AddChild(Permissions.Warnings_Export, L("Permission:Export"));

            // 租户管理权限（Host端）
            var tenants = monitoringGroup.AddPermission(Permissions.Tenants_Default, L("Permission:TenantManagement"));
            tenants.AddChild(Permissions.Tenants_Create, L("Permission:Create"));
            tenants.AddChild(Permissions.Tenants_Edit, L("Permission:Edit"));
            tenants.AddChild(Permissions.Tenants_Delete, L("Permission:Delete"));
            tenants.AddChild(Permissions.Tenants_ManageConnectionString, L("Permission:ManageConnectionString"));

            // 角色管理权限
            var roles = monitoringGroup.AddPermission(Permissions.Roles_Default, L("Permission:Roles"));
            roles.AddChild(Permissions.Roles_Create, L("Permission:Create"));
            roles.AddChild(Permissions.Roles_Edit, L("Permission:Edit"));
            roles.AddChild(Permissions.Roles_Delete, L("Permission:Delete"));
            roles.AddChild(Permissions.Roles_AssignPermissions, L("Permission:AssignPermissions"));
            roles.AddChild(Permissions.Roles_AssignUsers, L("Permission:AssignUsers"));

            // 权限管理权限
            var permissions = monitoringGroup.AddPermission(Permissions.Permissions_Default, L("Permission:PermissionManagement"));
            permissions.AddChild(Permissions.Permissions_ViewTree, L("Permission:ViewTree"));
            permissions.AddChild(Permissions.Permissions_Grant, L("Permission:Grant"));
            permissions.AddChild(Permissions.Permissions_ConfigureDataRules, L("Permission:ConfigureDataRules"));

            // 用户管理权限
            var users = monitoringGroup.AddPermission(Permissions.Users_Default, L("Permission:Users"));
            users.AddChild(Permissions.Users_Create, L("Permission:Create"));
            users.AddChild(Permissions.Users_Edit, L("Permission:Edit"));
            users.AddChild(Permissions.Users_Delete, L("Permission:Delete"));
            users.AddChild(Permissions.Users_ResetPassword, L("Permission:ResetPassword"));
            users.AddChild(Permissions.Users_Enable, L("Permission:Enable"));
            users.AddChild(Permissions.Users_Disable, L("Permission:Disable"));

            // 组织机构权限
            var orgUnits = monitoringGroup.AddPermission(Permissions.OrganizationUnits_Default, L("Permission:OrganizationUnits"));
            orgUnits.AddChild(Permissions.OrganizationUnits_Create, L("Permission:Create"));
            orgUnits.AddChild(Permissions.OrganizationUnits_Edit, L("Permission:Edit"));
            orgUnits.AddChild(Permissions.OrganizationUnits_Delete, L("Permission:Delete"));
            orgUnits.AddChild(Permissions.OrganizationUnits_ManageMembers, L("Permission:ManageMembers"));

            // 文件管理权限
            var files = monitoringGroup.AddPermission(Permissions.Files_Default, L("Permission:Files"));
            files.AddChild(Permissions.Files_Upload, L("Permission:Upload"));
            files.AddChild(Permissions.Files_Delete, L("Permission:Delete"));
            files.AddChild(Permissions.Files_Download, L("Permission:Download"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<JiaCeMonitorSystemResource>(name);
        }
    }
}
