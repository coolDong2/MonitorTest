using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ClientCaches;
using JiaCeMonitorSystem.Dtos.Notices;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.Notices;
using JiaCeMonitorSystem.SystemDictionaries;
using JiaCeMonitorSystem.SystemModules;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.ClientCaches
{
    /// <summary>
    /// 初始化缓存应用服务
    /// </summary>
    [Authorize]
    public class ClientCacheAppService : ApplicationService, IClientCacheAppService
    {
        private readonly IRepository<SystemDictionary, Guid> _systemDictionaryRepository;
        private readonly IRepository<Notice, Guid> _noticeRepository;
        private readonly IRepository<SystemModule, Guid> _systemModuleRepository;
        private readonly IDistributedCache _distributedCache;

        public ClientCacheAppService(
            IRepository<SystemDictionary, Guid> systemDictionaryRepository,
            IRepository<Notice, Guid> noticeRepository,
            IRepository<SystemModule, Guid> systemModuleRepository,
            IDistributedCache distributedCache)
        {
            _systemDictionaryRepository = systemDictionaryRepository;
            _noticeRepository = noticeRepository;
            _systemModuleRepository = systemModuleRepository;
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// 初始数据加载（系统字典、模块按钮、模型字段、菜单字段）
        /// </summary>
        public async Task<InitializeDictionaryDto> InitializeDictionaryAsync()
        {
            var result = new InitializeDictionaryDto();

            // 加载系统字典，按 ItemId 分组
            var dictionaries = await _systemDictionaryRepository.GetListAsync(d => d.EnabledMark);
            var grouped = dictionaries.GroupBy(d => d.ItemId);
            foreach (var group in grouped)
            {
                var key = group.Key.ToString("N");
                var items = group.Select(d => new DictionaryItemDto
                {
                    Id = d.Id.ToString(),
                    Text = d.ItemName,
                    Value = d.ItemCode
                }).ToList();
                result.KeyValuePairs[key] = items;
            }

            // 模块按钮、模型字段、菜单字段暂时返回空字典（后续可扩展）
            result.ModuleButtons = new Dictionary<string, object>();
            result.ModelFields = new Dictionary<string, object>();
            result.MenuFields = new Dictionary<string, object>();

            return result;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public async Task ClearCacheAsync()
        {
            // 清除已知的缓存键
            var cacheKeys = new[]
            {
                "ClientCache::Dictionary",
                "ClientCache::Notice",
                "ClientCache::QuickModule",
                "ClientCache::SysSatisfy"
            };

            foreach (var key in cacheKeys)
            {
                await _distributedCache.RemoveAsync(key);
            }
        }

        /// <summary>
        /// 获取公告列表
        /// </summary>
        public async Task<List<NoticeDto>> GetNoticeListAsync(int? count)
        {
            var query = await _noticeRepository.GetQueryableAsync();
            query = query.Where(n => n.EnabledMark).OrderByDescending(n => n.CreationTime);

            if (count.HasValue && count.Value > 0)
            {
                query = query.Take(count.Value);
            }

            var notices = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Notice>, List<NoticeDto>>(notices);
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        public async Task<CurrentUserInfoDto> GetCurrentUserInfoAsync()
        {
            var dto = new CurrentUserInfoDto
            {
                Id = CurrentUser.Id.GetValueOrDefault(),
                Account = CurrentUser.UserName ?? string.Empty,
                RealName = CurrentUser.Name ?? string.Empty,
                DisplayName = CurrentUser.Name ?? CurrentUser.UserName ?? string.Empty,
                MobilePhone = CurrentUser.PhoneNumber,
                Email = CurrentUser.Email,
                IsAdmin = CurrentUser.Roles.Contains("admin", StringComparer.OrdinalIgnoreCase),
                EnabledMark = CurrentUser.IsAuthenticated,
                StatusText = CurrentUser.IsAuthenticated ? "正常" : "未登录"
            };

            return await Task.FromResult(dto);
        }

        /// <summary>
        /// 获取首页快捷菜单
        /// </summary>
        public async Task<List<QuickModuleDto>> GetQuickModuleAsync()
        {
            var modules = await _systemModuleRepository.GetListAsync(
                m => m.EnabledMark && !string.IsNullOrEmpty(m.UrlAddress));

            return modules
                .OrderBy(m => m.SortCode)
                .Take(10)
                .Select(m => new QuickModuleDto
                {
                    Id = m.Id,
                    Title = m.FullName,
                    LinkAddress = m.UrlAddress ?? string.Empty,
                    Icon = m.Icon
                })
                .ToList();
        }

        /// <summary>
        /// 获取系统信息统计
        /// </summary>
        public async Task<SysSatisfyDto> GetSysSatisfyAsync()
        {
            var moduleCount = await _systemModuleRepository.GetCountAsync();

            return new SysSatisfyDto
            {
                UserCount = 0,  // 需要 IdentityUser 统计，暂不提供
                LoginCount = 0, // 需要登录日志统计，暂不提供
                ModuleCount = (int)moduleCount,
                LogCount = 0    // 需要审计日志统计，暂不提供
            };
        }
    }
}
