using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Tenants;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.System
{
    /// <summary>
    /// 租户管理控制器
    /// </summary>
    [Route("api/app/tenant")]
    public class TenantController : JiaCeMonitorSystemController
    {
        private readonly ITenantAppService _tenantAppService;

        /// <summary>
        /// 初始化租户控制器
        /// </summary>
        public TenantController(ITenantAppService tenantAppService)
        {
            _tenantAppService = tenantAppService;
        }

        /// <summary>
        /// 获取租户列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<TenantDto>> GetListAsync([FromQuery] GetTenantListInput input)
        {
            return _tenantAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个租户
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<TenantDto> GetAsync(Guid id)
        {
            return _tenantAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        [HttpPost]
        public virtual Task<TenantDto> CreateAsync([FromBody] TenantCreateDto input)
        {
            return _tenantAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新租户连接字符串
        /// </summary>
        [HttpPut("{id}/connection-string")]
        public virtual Task UpdateConnectionStringAsync(Guid id, [FromQuery] string? connectionString)
        {
            return _tenantAppService.UpdateConnectionStringAsync(id, connectionString);
        }

        /// <summary>
        /// 删除租户
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _tenantAppService.DeleteAsync(id);
        }
    }
}
