using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ClientCaches;
using JiaCeMonitorSystem.Dtos.Notices;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 初始化缓存应用服务接口
    /// </summary>
    public interface IClientCacheAppService : IApplicationService
    {
        /// <summary>
        /// 初始数据加载（系统字典、模块按钮、模型字段、菜单字段）
        /// </summary>
        Task<InitializeDictionaryDto> InitializeDictionaryAsync();

        /// <summary>
        /// 清空缓存
        /// </summary>
        Task ClearCacheAsync();

        /// <summary>
        /// 获取公告列表
        /// </summary>
        Task<List<NoticeDto>> GetNoticeListAsync(int? count);

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        Task<CurrentUserInfoDto> GetCurrentUserInfoAsync();

        /// <summary>
        /// 获取首页快捷菜单
        /// </summary>
        Task<List<QuickModuleDto>> GetQuickModuleAsync();

        /// <summary>
        /// 获取系统信息统计
        /// </summary>
        Task<SysSatisfyDto> GetSysSatisfyAsync();
    }
}
