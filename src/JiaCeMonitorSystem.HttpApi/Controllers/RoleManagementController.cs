using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Roles;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 角色管理控制器
    /// </summary>
    [Route("api/app/role-management")]
    public class RoleManagementController : JiaCeMonitorSystemController
    {
        private readonly IRoleManagementAppService _roleManagementAppService;

        /// <summary>
        /// 初始化角色管理控制器
        /// </summary>
        public RoleManagementController(IRoleManagementAppService roleManagementAppService)
        {
            _roleManagementAppService = roleManagementAppService;
        }


        /// <summary>
        /// 获取角色列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<RoleDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            return _roleManagementAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个角色
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<RoleDto> GetAsync(Guid id)
        {
            return _roleManagementAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        [HttpPost]
        public virtual Task<RoleDto> CreateAsync([FromQuery] string name)
        {
            return _roleManagementAppService.CreateAsync(name);
        }

        /// <summary>
        /// 更新角色名称
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<RoleDto> UpdateAsync(Guid id, [FromQuery] string name)
        {
            return _roleManagementAppService.UpdateAsync(id, name);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _roleManagementAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取角色用户列表
        /// </summary>
        [HttpGet("{roleId}/users")]
        public virtual Task<List<RoleUserDto>> GetRoleUsersAsync(Guid roleId)
        {
            return _roleManagementAppService.GetRoleUsersAsync(roleId);
        }
    }
}
