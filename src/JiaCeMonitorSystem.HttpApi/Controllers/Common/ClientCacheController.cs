using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ClientCaches;
using JiaCeMonitorSystem.Dtos.Notices;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers.Common.ClientCaches
{
    /// <summary>
    /// 初始化缓存控制器
    /// </summary>
    [Route("api/app/client-cache")]
    [Authorize]
    public class ClientCacheController : JiaCeMonitorSystemController
    {
        private readonly IClientCacheAppService _clientCacheAppService;

        public ClientCacheController(IClientCacheAppService clientCacheAppService)
        {
            _clientCacheAppService = clientCacheAppService;
        }

        /// <summary>
        /// 初始数据加载（系统字典、模块按钮、模型字段、菜单字段）
        /// </summary>
        [HttpGet("initialize-dictionary")]
        public Task<InitializeDictionaryDto> InitializeDictionaryAsync()
        {
            return _clientCacheAppService.InitializeDictionaryAsync();
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        [HttpPost("clear-cache")]
        public Task ClearCacheAsync()
        {
            return _clientCacheAppService.ClearCacheAsync();
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        [HttpGet("notice-list")]
        public Task<List<NoticeDto>> GetNoticeListAsync(int? count = null)
        {
            return _clientCacheAppService.GetNoticeListAsync(count);
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        [HttpGet("current-user-info")]
        public Task<CurrentUserInfoDto> GetCurrentUserInfoAsync()
        {
            return _clientCacheAppService.GetCurrentUserInfoAsync();
        }

        /// <summary>
        /// 获取首页快捷菜单
        /// </summary>
        [HttpGet("quick-module")]
        public Task<List<QuickModuleDto>> GetQuickModuleAsync()
        {
            return _clientCacheAppService.GetQuickModuleAsync();
        }

        /// <summary>
        /// 获取系统信息统计
        /// </summary>
        [HttpGet("sys-satisfy")]
        public Task<SysSatisfyDto> GetSysSatisfyAsync()
        {
            return _clientCacheAppService.GetSysSatisfyAsync();
        }
    }
}
