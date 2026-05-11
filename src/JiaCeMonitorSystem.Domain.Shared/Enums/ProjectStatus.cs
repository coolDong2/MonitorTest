namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 监测工程状态枚举，定义项目全生命周期状态
    /// </summary>
    public enum ProjectStatus
    {
        /// <summary>
        /// 筹备中
        /// </summary>
        Preparing = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 已完工
        /// </summary>
        Completed = 2,

        /// <summary>
        /// 已暂停
        /// </summary>
        Paused = 3,

        /// <summary>
        /// 已归档
        /// </summary>
        Archived = 4
    }
}
