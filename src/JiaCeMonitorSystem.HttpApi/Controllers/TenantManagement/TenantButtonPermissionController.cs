using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Application.Contracts.TenantManagement;
using JiaCeMonitorSystem.Controllers;
using JiaCeMonitorSystem.Dtos.ModuleButtons;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers.TenantManagement
{
    /// <summary>
    /// 租户按钮权限控制器
    /// </summary>
    [Route("api/app/tenant-button-permission")]
    public class TenantButtonPermissionController : JiaCeMonitorSystemController
    {
        private readonly ITenantButtonPermissionAppService _tenantButtonPermissionAppService;

        /// <summary>
        /// 初始化按钮权限控制器
        /// </summary>
        public TenantButtonPermissionController(ITenantButtonPermissionAppService tenantButtonPermissionAppService)
        {
            _tenantButtonPermissionAppService = tenantButtonPermissionAppService;
        }

        /// <summary>
        /// 批量授予角色按钮权限
        /// </summary>
        [HttpPost("grant")]
        public virtual Task GrantButtonsAsync(Guid roleId, [FromBody] List<Guid> buttonIds)
        {
            return _tenantButtonPermissionAppService.GrantButtonsAsync(roleId, buttonIds);
        }

        /// <summary>
        /// 批量撤销角色按钮权限
        /// </summary>
        [HttpPost("revoke")]
        public virtual Task RevokeButtonsAsync(Guid roleId, [FromBody] List<Guid> buttonIds)
        {
            return _tenantButtonPermissionAppService.RevokeButtonsAsync(roleId, buttonIds);
        }

        /// <summary>
        /// 获取角色的按钮权限列表
        /// </summary>
        [HttpGet("role-permissions/{roleId}")]
        public virtual Task<List<ButtonPermissionDto>> GetRoleButtonPermissionsAsync(Guid roleId, [FromQuery] Guid? moduleId = null)
        {
            return _tenantButtonPermissionAppService.GetRoleButtonPermissionsAsync(roleId, moduleId);
        }

        /// <summary>
        /// 获取当前用户在指定模块下的可用按钮
        /// </summary>
        [HttpGet("my-available-buttons/{moduleId}")]
        public virtual Task<List<ModuleButtonDto>> GetMyAvailableButtonsAsync(Guid moduleId)
        {
            return _tenantButtonPermissionAppService.GetMyAvailableButtonsAsync(moduleId);
        }
    }
}
