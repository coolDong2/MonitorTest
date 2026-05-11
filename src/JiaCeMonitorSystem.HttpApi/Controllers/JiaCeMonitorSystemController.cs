using System;
using Volo.Abp.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 监测云平台控制器基类
    /// </summary>
    public abstract class JiaCeMonitorSystemController : AbpControllerBase
    {
        /// <summary>
        /// 初始化控制器基类
        /// </summary>
        protected JiaCeMonitorSystemController()
        {
            LocalizationResource = typeof(JiaCeMonitorSystemResource);
        }
    }
}
