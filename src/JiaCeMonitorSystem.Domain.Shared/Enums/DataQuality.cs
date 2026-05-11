namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 监测数据质量枚举，标记单次采集数据的可信度
    /// </summary>
    public enum DataQuality
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 可疑
        /// </summary>
        Suspicious = 1,

        /// <summary>
        /// 异常
        /// </summary>
        Abnormal = 2
    }
}
