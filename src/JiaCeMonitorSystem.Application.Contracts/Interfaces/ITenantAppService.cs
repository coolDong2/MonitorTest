using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Tenants;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 租户管理应用服务接口（Host端专用）
    /// </summary>
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
