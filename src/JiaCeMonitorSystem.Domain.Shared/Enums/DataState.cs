namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 监测数据状态枚举，定义数据在业务流转中的生命周期状态
    /// </summary>
    public enum DataState
    {
        /// <summary>
        /// 原始数据
        /// </summary>
        Raw = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        Approved = 1,

        /// <summary>
        /// 已归档
        /// </summary>
        Archived = 2
    }
}
