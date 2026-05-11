using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.AppRoles;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers.AppRoles
{
    /// <summary>
    /// 角色权限控制器
    /// </summary>
    [Route("api/app/role-authorize")]
    [Authorize]
    public class RoleAuthorizeController : JiaCeMonitorSystemController
    {
        private readonly IAppRoleAppService _appRoleAppService;

        public RoleAuthorizeController(IAppRoleAppService appRoleAppService)
        {
            _appRoleAppService = appRoleAppService;
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        [HttpGet("permission-tree")]
        public Task<List<PermissionTreeDto>> GetPermissionTreeAsync(Guid roleId)
        {
            return _appRoleAppService.GetPermissionTreeAsync(roleId);
        }

        /// <summary>
        /// 获取权限字段树
        /// </summary>
        [HttpGet("permission-fields-tree")]
        public Task<List<PermissionTreeDto>> GetPermissionFieldsTreeAsync(Guid roleId, string? moduleIds = null)
        {
            return _appRoleAppService.GetPermissionFieldsTreeAsync(roleId, moduleIds);
        }
    }
}
