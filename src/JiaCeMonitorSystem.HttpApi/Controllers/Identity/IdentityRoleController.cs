using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Permissions;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;
using AppRolePermissionTreeDto = JiaCeMonitorSystem.Dtos.AppRoles.PermissionTreeDto;
using PermissionTreeDto = JiaCeMonitorSystem.Dtos.Permissions.PermissionTreeDto;

namespace JiaCeMonitorSystem.Controllers.Identity
{
    /// <summary>
    /// 身份角色管理控制器
    /// </summary>
    [Route("api/app/identity/roles")]
    public class IdentityRoleController : JiaCeMonitorSystemController
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;
        private readonly IPermissionAppService _permissionAppService;
        private readonly IIdentityRoleExtendedAppService _identityRoleExtendedAppService;
        private readonly ITenantMenuAppService _tenantMenuAppService;

        /// <summary>
        /// 初始化角色管理控制器
        /// </summary>
        public IdentityRoleController(
            IIdentityRoleAppService identityRoleAppService,
            IPermissionAppService permissionAppService,
            IIdentityRoleExtendedAppService identityRoleExtendedAppService,
            ITenantMenuAppService tenantMenuAppService)
        {
            _identityRoleAppService = identityRoleAppService;
            _permissionAppService = permissionAppService;
            _identityRoleExtendedAppService = identityRoleExtendedAppService;
            _tenantMenuAppService = tenantMenuAppService;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<IdentityRoleDto>> GetRolePageListAsync([FromQuery] GetIdentityRolesInput input)
        {
            return _identityRoleAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取所有角色（不分页）
        /// </summary>
        [HttpGet("all")]
        public virtual Task<ListResultDto<IdentityRoleDto>> GetAllRolesAsync()
        {
            return _identityRoleAppService.GetAllListAsync();
        }

        /// <summary>
        /// 获取单个角色
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<IdentityRoleDto> GetRoleByIdAsync(Guid id)
        {
            return _identityRoleAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        [HttpPost]
        public virtual Task<IdentityRoleDto> CreateRoleAsync([FromBody] IdentityRoleCreateDto input)
        {
            return _identityRoleAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<IdentityRoleDto> UpdateRoleAsync(Guid id, [FromBody] IdentityRoleUpdateDto input)
        {
            return _identityRoleAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteRoleAsync(Guid id)
        {
            return _identityRoleAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取角色权限树
        /// </summary>
        [HttpGet("{id}/permissions")]
        public virtual Task<PermissionTreeDto> GetRolePermissionsAsync(Guid id)
        {
            return _permissionAppService.GetPermissionTreeAsync("Role", id.ToString());
        }

        /// <summary>
        /// 更新角色权限
        /// </summary>
        [HttpPut("{id}/permissions")]
        public virtual Task UpdateRolePermissionsAsync(Guid id, [FromBody] List<string> permissions)
        {
            return _permissionAppService.GrantAsync(new PermissionGrantDto
            {
                ProviderName = "Role",
                ProviderKey = id.ToString(),
                Permissions = permissions
            });
        }

        /// <summary>
        /// 获取角色下的用户列表
        /// </summary>
        [HttpGet("{id}/users")]
        public virtual Task<List<IdentityUserDto>> GetRoleUsersAsync(Guid id)
        {
            return _identityRoleExtendedAppService.GetRoleUsersAsync(id);
        }

        /// <summary>
        /// 获取角色的模块权限树（基于 SystemModule）
        /// </summary>
        [HttpGet("{id}/module-permissions")]
        public virtual Task<List<AppRolePermissionTreeDto>> GetRoleModulePermissionsAsync(Guid id)
        {
            return _tenantMenuAppService.GetRolePermissionTreeAsync(id);
        }

        /// <summary>
        /// 获取角色的字段权限树（基于 SystemModule）
        /// </summary>
        [HttpGet("{id}/field-permissions")]
        public virtual Task<List<AppRolePermissionTreeDto>> GetRoleFieldPermissionsAsync(Guid id, string? moduleIds = null)
        {
            return _tenantMenuAppService.GetRolePermissionFieldsTreeAsync(id, moduleIds);
        }
    }
}
