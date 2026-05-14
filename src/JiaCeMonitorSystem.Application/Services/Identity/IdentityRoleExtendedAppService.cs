using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Interfaces;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Services.Identity
{
    /// <summary>
    /// 身份角色扩展应用服务
    /// </summary>
    public class IdentityRoleExtendedAppService : ApplicationService, IIdentityRoleExtendedAppService
    {
        private readonly IdentityUserManager _userManager;
        private readonly IIdentityRoleRepository _roleRepository;

        public IdentityRoleExtendedAppService(
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取角色下的用户列表
        /// </summary>
        public async Task<List<IdentityUserDto>> GetRoleUsersAsync(Guid roleId)
        {
            var role = await _roleRepository.GetAsync(roleId);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            return ObjectMapper.Map<List<IdentityUser>, List<IdentityUserDto>>(users.ToList());
        }
    }
}
