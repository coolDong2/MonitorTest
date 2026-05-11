namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 预警级别枚举，定义监测数据超过阈值时的告警严重程度
    /// </summary>
    public enum WarningLevel
    {
        /// <summary>
        /// 提示
        /// </summary>
        Hint = 0,

        /// <summary>
        /// 一级预警（注意）
        /// </summary>
        Level1Notice = 1,

        /// <summary>
        /// 二级预警（警告）
        /// </summary>
        Level2Warning = 2,

        /// <summary>
        /// 三级预警（危险）
        /// </summary>
        Level3Danger = 3
    }
}
