using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Application.Contracts.TenantManagement
{
    /// <summary>
    /// 租户配置应用服务接口
    /// </summary>
    public interface ITenantConfigurationAppService : IApplicationService
    {
        /// <summary>
        /// 创建租户并初始化配置
        /// </summary>
        Task<TenantConfigurationDto> CreateAsync(CreateTenantWithConfigDto input);

        /// <summary>
        /// 获取租户配置详情
        /// </summary>
        Task<TenantConfigurationDto> GetConfigurationAsync(Guid tenantId);

        /// <summary>
        /// 更新租户许可证配额
        /// </summary>
        Task<TenantConfigurationDto> UpdateLicenseAsync(Guid tenantId, TenantLicenseDto input);

        /// <summary>
        /// 获取租户配置分页列表
        /// </summary>
        Task<PagedResultDto<TenantConfigurationDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 将租户切换到独立数据库（异步后台执行）
        /// </summary>
        Task SwitchToIndependentDatabaseAsync(Guid tenantId);
    }
}
