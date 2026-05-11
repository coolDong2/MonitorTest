namespace JiaCeMonitorSystem.Dtos.ClientCaches
{
    /// <summary>
    /// 系统信息统计数据传输对象
    /// </summary>
    public class SysSatisfyDto
    {
        /// <summary>
        /// 已注册用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 已登录数量
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 系统模块数
        /// </summary>
        public int ModuleCount { get; set; }

        /// <summary>
        /// 日志总数
        /// </summary>
        public int LogCount { get; set; }
    }
}
