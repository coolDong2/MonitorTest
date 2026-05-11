namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 预警类型枚举，定义触发预警的判定依据
    /// </summary>
    public enum WarningType
    {
        /// <summary>
        /// 阈值预警
        /// </summary>
        Threshold = 0,

        /// <summary>
        /// 变化率预警
        /// </summary>
        ChangeRate = 1,

        /// <summary>
        /// 累计值预警
        /// </summary>
        Cumulative = 2,

        /// <summary>
        /// 其他预警
        /// </summary>
        Other = 3
    }
}
