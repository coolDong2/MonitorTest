using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.TenantManagement
{
    /// <summary>
    /// 租户配置控制器，提供 SaaS 租户创建、许可证管理与独立数据库切换等功能
    /// </summary>
    [Route("api/app/tenant-configuration")]
    public class TenantConfigurationController : JiaCeMonitorSystemController
    {
        private readonly ITenantConfigurationAppService _tenantConfigurationAppService;

        /// <summary>
        /// 初始化租户配置控制器
        /// </summary>
        public TenantConfigurationController(ITenantConfigurationAppService tenantConfigurationAppService)
        {
            _tenantConfigurationAppService = tenantConfigurationAppService;
        }

        /// <summary>
        /// 创建租户并初始化配置（含模块授权、许可证配额、独立数据库选项）
        /// </summary>
        [HttpPost]
        public virtual Task<TenantConfigurationDto> CreateAsync([FromBody] CreateTenantWithConfigDto input)
        {
            return _tenantConfigurationAppService.CreateAsync(input);
        }

        /// <summary>
        /// 获取租户配置详情
        /// </summary>
        [HttpGet("{tenantId}")]
        public virtual Task<TenantConfigurationDto> GetConfigurationAsync(Guid tenantId)
        {
            return _tenantConfigurationAppService.GetConfigurationAsync(tenantId);
        }

        /// <summary>
        /// 获取租户配置分页列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<TenantConfigurationDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            return _tenantConfigurationAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新租户许可证配额
        /// </summary>
        [HttpPut("{tenantId}/license")]
        public virtual Task<TenantConfigurationDto> UpdateLicenseAsync(Guid tenantId, [FromBody] TenantLicenseDto input)
        {
            return _tenantConfigurationAppService.UpdateLicenseAsync(tenantId, input);
        }

        /// <summary>
        /// 将租户切换到独立数据库（异步后台执行，通过 Hangfire 调度）
        /// </summary>
        [HttpPost("{tenantId}/switch-to-independent-db")]
        public virtual async Task<IActionResult> SwitchToIndependentDatabaseAsync(Guid tenantId)
        {
            await _tenantConfigurationAppService.SwitchToIndependentDatabaseAsync(tenantId);
            return Accepted(new { message = "独立数据库切换任务已提交，将在后台异步执行。", tenantId });
        }
    }
}
