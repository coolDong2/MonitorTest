namespace JiaCeMonitorSystem
{
    /// <summary>
    /// 全局异常错误码枚举，统一管理系统内所有业务异常的错误标识
    /// 错误码格式：{模块缩写}:{三位序号}
    /// </summary>
    public static class ErrorCodes
    {
        // ==================== 通用错误 (000-099) ====================

        /// <summary>
        /// 未知错误
        /// </summary>
        public const string General_Unknown = "JC:000";

        /// <summary>
        /// 参数无效
        /// </summary>
        public const string General_InvalidParameter = "JC:001";

        /// <summary>
        /// 记录不存在
        /// </summary>
        public const string General_EntityNotFound = "JC:002";

        /// <summary>
        /// 无权访问
        /// </summary>
        public const string General_AccessDenied = "JC:003";

        /// <summary>
        /// 操作不允许
        /// </summary>
        public const string General_OperationNotAllowed = "JC:004";

        // ==================== 认证授权错误 (100-199) ====================

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        public const string Auth_InvalidCredentials = "JC:100";

        /// <summary>
        /// 用户已被锁定
        /// </summary>
        public const string Auth_UserLockedOut = "JC:101";

        /// <summary>
        /// 用户已被禁用
        /// </summary>
        public const string Auth_UserDisabled = "JC:102";

        /// <summary>
        /// Token 已过期
        /// </summary>
        public const string Auth_TokenExpired = "JC:103";

        /// <summary>
        /// 无权执行该操作
        /// </summary>
        public const string Auth_InsufficientPermissions = "JC:104";

        /// <summary>
        /// 租户不存在或已过期
        /// </summary>
        public const string Auth_TenantNotFoundOrExpired = "JC:105";

        // ==================== 监测工程错误 (200-299) ====================

        /// <summary>
        /// 已归档项目禁止变更状态
        /// </summary>
        public const string Project_ArchivedCannotModify = "JC:200";

        /// <summary>
        /// 项目编号已存在
        /// </summary>
        public const string Project_DuplicateCode = "JC:201";

        /// <summary>
        /// 项目下存在测点，禁止删除
        /// </summary>
        public const string Project_HasPointsCannotDelete = "JC:202";

        /// <summary>
        /// 项目状态不允许该操作
        /// </summary>
        public const string Project_InvalidStatusTransition = "JC:203";

        // ==================== 测点错误 (300-399) ====================

        /// <summary>
        /// 测点编号已存在
        /// </summary>
        public const string Point_DuplicateCode = "JC:300";

        /// <summary>
        /// 测点阈值配置无效
        /// </summary>
        public const string Point_InvalidThreshold = "JC:301";

        /// <summary>
        /// 测点存在活跃预警，禁止删除
        /// </summary>
        public const string Point_HasActiveWarnings = "JC:302";

        // ==================== 监测数据错误 (400-499) ====================

        /// <summary>
        /// 监测数据格式无效
        /// </summary>
        public const string MonitoringData_InvalidFormat = "JC:400";

        /// <summary>
        /// 监测时间不能晚于当前时间
        /// </summary>
        public const string MonitoringData_FutureTime = "JC:401";

        /// <summary>
        /// 监测值超出物理量程
        /// </summary>
        public const string MonitoringData_OutOfRange = "JC:402";

        /// <summary>
        /// 数据已审核，禁止修改
        /// </summary>
        public const string MonitoringData_AlreadyApproved = "JC:403";

        // ==================== 预警记录错误 (500-599) ====================

        /// <summary>
        /// 预警状态不允许该操作
        /// </summary>
        public const string Warning_InvalidStatusTransition = "JC:500";

        /// <summary>
        /// 预警已被分配处理人
        /// </summary>
        public const string Warning_AlreadyAssigned = "JC:501";

        /// <summary>
        /// 预警已关闭，禁止操作
        /// </summary>
        public const string Warning_AlreadyClosed = "JC:502";

        /// <summary>
        /// 未分配处理人，禁止提交方案
        /// </summary>
        public const string Warning_HandlerNotAssigned = "JC:503";

        // ==================== 设备错误 (600-699) ====================

        /// <summary>
        /// 设备编号已存在
        /// </summary>
        public const string Device_DuplicateCode = "JC:600";

        /// <summary>
        /// 已借出设备禁止校准
        /// </summary>
        public const string Device_LentOutCannotCalibrate = "JC:601";

        /// <summary>
        /// 维修中设备禁止校准
        /// </summary>
        public const string Device_UnderRepairCannotCalibrate = "JC:602";

        /// <summary>
        /// 报废设备禁止任何操作
        /// </summary>
        public const string Device_ScrappedCannotModify = "JC:603";

        /// <summary>
        /// 设备当前状态不允许借出
        /// </summary>
        public const string Device_CannotLendCurrentStatus = "JC:604";

        /// <summary>
        /// 下次校准日期必须晚于校准日期
        /// </summary>
        public const string Device_InvalidCalibrationDate = "JC:605";

        // ==================== 租户错误 (700-799) ====================

        /// <summary>
        /// 租户名称已存在
        /// </summary>
        public const string Tenant_DuplicateName = "JC:700";

        /// <summary>
        /// 租户已过期
        /// </summary>
        public const string Tenant_Expired = "JC:701";

        /// <summary>
        /// 仅 Host 端可执行该操作
        /// </summary>
        public const string Tenant_HostOnlyOperation = "JC:702";

        // ==================== 权限错误 (800-899) ====================

        /// <summary>
        /// 权限规则配置无效
        /// </summary>
        public const string Permission_InvalidRule = "JC:800";

        /// <summary>
        /// 无法删除系统内置角色
        /// </summary>
        public const string Role_SystemRoleCannotDelete = "JC:801";

        // ==================== 业务模块错误 (2001-2099) ====================

        /// <summary>
        /// 监测项目类型编码已存在
        /// </summary>
        public const string MonitoringItemType_DuplicateCode = "JC:2001";

        /// <summary>
        /// 监测项目属性编码在类型内重复
        /// </summary>
        public const string MonitoringItemProperty_DuplicateCode = "JC:2002";

        /// <summary>
        /// 该测点下不存在指定的监测属性
        /// </summary>
        public const string MonitoringData_PropertyNotFound = "JC:2003";

        /// <summary>
        /// 项目人员时段冲突
        /// </summary>
        public const string ProjectPersonnel_TimeConflict = "JC:2004";

        /// <summary>
        /// 系统菜单存在子节点，禁止删除
        /// </summary>
        public const string SystemModule_HasChildrenCannotDelete = "JC:2005";

        /// <summary>
        /// 字典类型编码已存在
        /// </summary>
        public const string SystemDictionaryType_DuplicateCode = "JC:2006";

        /// <summary>
        /// 文件不存在或已被删除
        /// </summary>
        public const string File_NotFound = "JC:2007";
    }
}
