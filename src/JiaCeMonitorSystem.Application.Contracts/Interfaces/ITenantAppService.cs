using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Tenants;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 租户管理应用服务接口（Host端专用）
    /// <para>【已弃用】请使用 <see cref="ITenantConfigurationAppService"/> 替代。</para>
    /// </summary>
    [Obsolete("请使用 ITenantConfigurationAppService 替代 ITenantAppService")]
    public interface ITenantAppService : IApplicationService
    {
        /// <summary>
        /// 创建租户
        /// </summary>
        Task<TenantDto> CreateAsync(TenantCreateDto input);

        /// <summary>
        /// 获取租户列表
        /// </summary>
        Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantListInput input);

        /// <summary>
        /// 获取单个租户
        /// </summary>
        Task<TenantDto> GetAsync(Guid id);

        /// <summary>
        /// 更新租户连接字符串
        /// </summary>
        Task UpdateConnectionStringAsync(Guid id, string? connectionString);

        /// <summary>
        /// 删除租户
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
