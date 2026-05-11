using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.AppRoles;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.AppRoles
{
    /// <summary>
    /// 角色管理控制器
    /// </summary>
    [Route("api/app/role")]
    [Authorize]
    public class RoleController : JiaCeMonitorSystemController
    {
        private readonly IAppRoleAppService _appRoleAppService;

        public RoleController(IAppRoleAppService appRoleAppService)
        {
            _appRoleAppService = appRoleAppService;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet("page-list")]
        public Task<PagedResultDto<AppRoleDto>> GetPageListAsync([FromQuery] GetAppRoleListInput input)
        {
            return _appRoleAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        [HttpGet("list")]
        public Task<List<AppRoleDto>> GetListAsync(string? keyword = null)
        {
            return _appRoleAppService.GetListAsync(keyword);
        }

        /// <summary>
        /// 获取单个角色
        /// </summary>
        [HttpGet("model")]
        public Task<AppRoleDto> GetModelAsync(Guid id)
        {
            return _appRoleAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        [HttpPost("add-and-edit")]
        [Authorize(Permissions.Permissions.Roles_Create)]
        public Task<AppRoleDto> AddAndEditRoleAsync([FromBody] AppRoleCreateDto input)
        {
            return _appRoleAppService.AddAndEditRoleAsync(input);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        [HttpPut("update")]
        [Authorize(Permissions.Permissions.Roles_Edit)]
        public Task<AppRoleDto> UpdateAsync(Guid id, [FromBody] AppRoleUpdateDto input)
        {
            return _appRoleAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [HttpDelete("delete")]
        [Authorize(Permissions.Permissions.Roles_Delete)]
        public Task DeleteAsync(Guid id)
        {
            return _appRoleAppService.DeleteAsync(id);
        }
    }
}
