namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 设备分配状态枚举，定义设备从借出到归还的流转状态
    /// </summary>
    public enum AssignmentStatus
    {
        /// <summary>
        /// 已分配
        /// </summary>
        Assigned = 0,

        /// <summary>
        /// 使用中
        /// </summary>
        InUse = 1,

        /// <summary>
        /// 已归还
        /// </summary>
        Returned = 2,

        /// <summary>
        /// 延期归还
        /// </summary>
        Extended = 3,

        /// <summary>
        /// 损坏
        /// </summary>
        Damaged = 4
    }
}
