namespace JiaCeMonitorSystem.Permissions
{
    /// <summary>
    /// 系统权限常量定义类，按业务模块分组管理所有权限标识
    /// 权限命名规范：{Module}.{Action}
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// 权限分组前缀：监测工程
        /// </summary>
        public const string Projects_Default = "Projects";
        public const string Projects_Create = "Projects.Create";
        public const string Projects_Edit = "Projects.Edit";
        public const string Projects_Delete = "Projects.Delete";
        public const string Projects_Archive = "Projects.Archive";
        public const string Projects_Export = "Projects.Export";
        public const string Projects_ManagePersonnel = "Projects.ManagePersonnel";

        /// <summary>
        /// 权限分组前缀：测点管理
        /// </summary>
        public const string Points_Default = "Points";
        public const string Points_Create = "Points.Create";
        public const string Points_Edit = "Points.Edit";
        public const string Points_Delete = "Points.Delete";
        public const string Points_Import = "Points.Import";
        public const string Points_Export = "Points.Export";
        public const string Points_ConfigureThreshold = "Points.ConfigureThreshold";

        /// <summary>
        /// 权限分组前缀：监测数据
        /// </summary>
        public const string MonitoringData_Default = "MonitoringData";
        public const string MonitoringData_Create = "MonitoringData.Create";
        public const string MonitoringData_Edit = "MonitoringData.Edit";
        public const string MonitoringData_Delete = "MonitoringData.Delete";
        public const string MonitoringData_Import = "MonitoringData.Import";
        public const string MonitoringData_Export = "MonitoringData.Export";
        public const string MonitoringData_Approve = "MonitoringData.Approve";
        public const string MonitoringData_QueryHistory = "MonitoringData.QueryHistory";

        /// <summary>
        /// 权限分组前缀：仪器设备
        /// </summary>
        public const string Devices_Default = "Devices";
        public const string Devices_Create = "Devices.Create";
        public const string Devices_Edit = "Devices.Edit";
        public const string Devices_Delete = "Devices.Delete";
        public const string Devices_Calibrate = "Devices.Calibrate";
        public const string Devices_Lend = "Devices.Lend";
        public const string Devices_Return = "Devices.Return";
        public const string Devices_Scrap = "Devices.Scrap";

        /// <summary>
        /// 权限分组前缀：预警记录
        /// </summary>
        public const string Warnings_Default = "Warnings";
        public const string Warnings_Handle = "Warnings.Handle";
        public const string Warnings_Confirm = "Warnings.Confirm";
        public const string Warnings_Close = "Warnings.Close";
        public const string Warnings_ViewStatistics = "Warnings.ViewStatistics";
        public const string Warnings_Export = "Warnings.Export";

        /// <summary>
        /// 权限分组前缀：租户管理（Host端专用）
        /// </summary>
        public const string Tenants_Default = "Tenants";
        public const string Tenants_Create = "Tenants.Create";
        public const string Tenants_Edit = "Tenants.Edit";
        public const string Tenants_Delete = "Tenants.Delete";
        public const string Tenants_ManageConnectionString = "Tenants.ManageConnectionString";

        /// <summary>
        /// 权限分组前缀：角色管理
        /// </summary>
        public const string Roles_Default = "Roles";
        public const string Roles_Create = "Roles.Create";
        public const string Roles_Edit = "Roles.Edit";
        public const string Roles_Delete = "Roles.Delete";
        public const string Roles_AssignPermissions = "Roles.AssignPermissions";
        public const string Roles_AssignUsers = "Roles.AssignUsers";

        /// <summary>
        /// 权限分组前缀：权限管理
        /// </summary>
        public const string Permissions_Default = "Permissions";
        public const string Permissions_ViewTree = "Permissions.ViewTree";
        public const string Permissions_Grant = "Permissions.Grant";
        public const string Permissions_ConfigureDataRules = "Permissions.ConfigureDataRules";

        /// <summary>
        /// 权限分组前缀：系统用户
        /// </summary>
        public const string Users_Default = "Users";
        public const string Users_Create = "Users.Create";
        public const string Users_Edit = "Users.Edit";
        public const string Users_Delete = "Users.Delete";
        public const string Users_ResetPassword = "Users.ResetPassword";
        public const string Users_Enable = "Users.Enable";
        public const string Users_Disable = "Users.Disable";

        /// <summary>
        /// 权限分组前缀：组织机构
        /// </summary>
        public const string OrganizationUnits_Default = "OrganizationUnits";
        public const string OrganizationUnits_Create = "OrganizationUnits.Create";
        public const string OrganizationUnits_Edit = "OrganizationUnits.Edit";
        public const string OrganizationUnits_Delete = "OrganizationUnits.Delete";
        public const string OrganizationUnits_ManageMembers = "OrganizationUnits.ManageMembers";

        /// <summary>
        /// 权限分组前缀：系统设置
        /// </summary>
        public const string Settings_Default = "Settings";
        public const string Settings_Edit = "Settings.Edit";

        /// <summary>
        /// 权限分组前缀：文件管理
        /// </summary>
        public const string Files_Default = "Files";
        public const string Files_Upload = "Files.Upload";
        public const string Files_Delete = "Files.Delete";
        public const string Files_Download = "Files.Download";

        /// <summary>
        /// 权限分组前缀：系统菜单模块管理
        /// </summary>
        public const string SystemModules_Default = "SystemModules";
        public const string SystemModules_Create = "SystemModules.Create";
        public const string SystemModules_Edit = "SystemModules.Edit";
        public const string SystemModules_Delete = "SystemModules.Delete";

        /// <summary>
        /// 权限分组前缀：系统菜单按钮管理
        /// </summary>
        public const string ModuleButtons_Default = "ModuleButtons";
        public const string ModuleButtons_Create = "ModuleButtons.Create";
        public const string ModuleButtons_Edit = "ModuleButtons.Edit";
        public const string ModuleButtons_Delete = "ModuleButtons.Delete";

        /// <summary>
        /// 权限分组前缀：监测项目类型配置
        /// </summary>
        public const string MonitoringItemTypes_Default = "MonitoringItemTypes";
        public const string MonitoringItemTypes_Create = "MonitoringItemTypes.Create";
        public const string MonitoringItemTypes_Edit = "MonitoringItemTypes.Edit";
        public const string MonitoringItemTypes_Delete = "MonitoringItemTypes.Delete";

        /// <summary>
        /// 权限分组前缀：系统组织管理
        /// </summary>
        public const string Organizes_Default = "Organizes";
        public const string Organizes_Create = "Organizes.Create";
        public const string Organizes_Edit = "Organizes.Edit";
        public const string Organizes_Delete = "Organizes.Delete";

        /// <summary>
        /// 权限分组前缀：系统通知管理
        /// </summary>
        public const string Notices_Default = "Notices";
        public const string Notices_Create = "Notices.Create";
        public const string Notices_Edit = "Notices.Edit";
        public const string Notices_Delete = "Notices.Delete";

        /// <summary>
        /// 权限分组前缀：项目人员安排管理
        /// </summary>
        public const string ProjectPersonnels_Default = "ProjectPersonnels";
        public const string ProjectPersonnels_Create = "ProjectPersonnels.Create";
        public const string ProjectPersonnels_Edit = "ProjectPersonnels.Edit";
        public const string ProjectPersonnels_Delete = "ProjectPersonnels.Delete";

        /// <summary>
        /// 权限分组前缀：系统字典管理
        /// </summary>
        public const string SystemDictionaries_Default = "SystemDictionaries";
        public const string SystemDictionaries_Create = "SystemDictionaries.Create";
        public const string SystemDictionaries_Edit = "SystemDictionaries.Edit";
        public const string SystemDictionaries_Delete = "SystemDictionaries.Delete";

        /// <summary>
        /// 权限分组前缀：系统字典类型管理
        /// </summary>
        public const string SystemDictionaryTypes_Default = "SystemDictionaryTypes";
        public const string SystemDictionaryTypes_Create = "SystemDictionaryTypes.Create";
        public const string SystemDictionaryTypes_Edit = "SystemDictionaryTypes.Edit";
        public const string SystemDictionaryTypes_Delete = "SystemDictionaryTypes.Delete";

        /// <summary>
        /// 权限分组前缀：文件管理（扩展）
        /// </summary>
        public const string FileManages_Default = "FileManages";
        public const string FileManages_Create = "FileManages.Create";
        public const string FileManages_Edit = "FileManages.Edit";
        public const string FileManages_Delete = "FileManages.Delete";
    }
}
