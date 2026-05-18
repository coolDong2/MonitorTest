using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Controllers.Identity
{
    /// <summary>
    /// 身份用户管理控制器
    /// </summary>
    [Route("api/app/identity/users")]
    public class IdentityUserController : JiaCeMonitorSystemController
    {
        private readonly IIdentityUserAppService _identityUserAppService;

        /// <summary>
        /// 初始化用户管理控制器
        /// </summary>
        public IdentityUserController(IIdentityUserAppService identityUserAppService)
        {
            _identityUserAppService = identityUserAppService;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<IdentityUserDto>> GetUserPageListAsync([FromQuery] GetIdentityUsersInput input)
        {
            return _identityUserAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个用户
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<IdentityUserDto> GetUserByIdAsync(Guid id)
        {
            return _identityUserAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        [HttpPost]
        public virtual Task<IdentityUserDto> CreateUserAsync([FromBody] IdentityUserCreateDto input)
        {
            return _identityUserAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<IdentityUserDto> UpdateUserAsync(Guid id, [FromBody] IdentityUserUpdateDto input)
        {
            return _identityUserAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteUserAsync(Guid id)
        {
            return _identityUserAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取指定用户的角色列表
        /// </summary>
        [HttpGet("{id}/roles")]
        public virtual Task<ListResultDto<IdentityRoleDto>> GetUserRolesAsync(Guid id)
        {
            return _identityUserAppService.GetRolesAsync(id);
        }

        /// <summary>
        /// 获取可分配的角色列表
        /// </summary>
        [HttpGet("assignable-roles")]
        public virtual Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync()
        {
            return _identityUserAppService.GetAssignableRolesAsync();
        }

        /// <summary>
        /// 更新用户角色
        /// </summary>
        [HttpPut("{id}/roles")]
        public virtual Task UpdateUserRolesAsync(Guid id, [FromBody] IdentityUserUpdateRolesDto input)
        {
            return _identityUserAppService.UpdateRolesAsync(id, input);
        }

        /// <summary>
        /// 根据用户名查找用户
        /// </summary>
        [HttpGet("find-by-username")]
        public virtual Task<IdentityUserDto> FindUserByNameAsync([FromQuery] string userName)
        {
            return _identityUserAppService.FindByUsernameAsync(userName);
        }

        /// <summary>
        /// 根据邮箱查找用户
        /// </summary>
        [HttpGet("find-by-email")]
        public virtual Task<IdentityUserDto> FindUserByEmailAsync([FromQuery] string email)
        {
            return _identityUserAppService.FindByEmailAsync(email);
        }
    }
}
