namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 预警记录处理状态枚举，定义预警从触发到闭环的完整状态机
    /// </summary>
    public enum HandleStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        Unhandled = 0,

        /// <summary>
        /// 处理中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 已处理
        /// </summary>
        Handled = 2,

        /// <summary>
        /// 已关闭
        /// </summary>
        Closed = 3
    }
}
