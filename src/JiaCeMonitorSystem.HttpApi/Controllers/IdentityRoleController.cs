using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 身份角色管理控制器
    /// </summary>
    [Route("api/identity/roles")]
    public class IdentityRoleController : JiaCeMonitorSystemController
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;

        /// <summary>
        /// 初始化角色管理控制器
        /// </summary>
        public IdentityRoleController(IIdentityRoleAppService identityRoleAppService)
        {
            _identityRoleAppService = identityRoleAppService;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<IdentityRoleDto>> GetListAsync([FromQuery] GetIdentityRolesInput input)
        {
            return _identityRoleAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取所有角色（不分页）
        /// </summary>
        [HttpGet("all")]
        public virtual Task<ListResultDto<IdentityRoleDto>> GetAllListAsync()
        {
            return _identityRoleAppService.GetAllListAsync();
        }

        /// <summary>
        /// 获取单个角色
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<IdentityRoleDto> GetAsync(Guid id)
        {
            return _identityRoleAppService.GetAsync(id);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        [HttpPost]
        public virtual Task<IdentityRoleDto> CreateAsync([FromBody] IdentityRoleCreateDto input)
        {
            return _identityRoleAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<IdentityRoleDto> UpdateAsync(Guid id, [FromBody] IdentityRoleUpdateDto input)
        {
            return _identityRoleAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _identityRoleAppService.DeleteAsync(id);
        }
    }
}
