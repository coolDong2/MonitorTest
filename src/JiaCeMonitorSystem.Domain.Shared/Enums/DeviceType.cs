namespace JiaCeMonitorSystem.Enums
{
    /// <summary>
    /// 仪器设备类型枚举，定义监测领域常用的设备分类
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// 全站仪
        /// </summary>
        TotalStation = 0,

        /// <summary>
        /// 水准仪
        /// </summary>
        LevelInstrument = 1,

        /// <summary>
        /// 测斜仪
        /// </summary>
        Inclinometer = 2,

        /// <summary>
        /// 沉降仪
        /// </summary>
        SettlementGauge = 3,

        /// <summary>
        /// 应变计
        /// </summary>
        StrainGauge = 4,

        /// <summary>
        /// 压力计
        /// </summary>
        PressureGauge = 5,

        /// <summary>
        /// 水位计
        /// </summary>
        WaterLevelGauge = 6,

        /// <summary>
        /// 采集器
        /// </summary>
        DataCollector = 7,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 8
    }
}
