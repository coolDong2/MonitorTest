namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 单位设备状态枚举，定义设备全生命周期状态
    /// </summary>
    public enum DeviceStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 维修中
        /// </summary>
        UnderRepair = 1,

        /// <summary>
        /// 停用
        /// </summary>
        Deactivated = 2,

        /// <summary>
        /// 报废
        /// </summary>
        Scrapped = 3,

        /// <summary>
        /// 校准中
        /// </summary>
        Calibrating = 4,

        /// <summary>
        /// 已借出
        /// </summary>
        LentOut = 5
    }
}
