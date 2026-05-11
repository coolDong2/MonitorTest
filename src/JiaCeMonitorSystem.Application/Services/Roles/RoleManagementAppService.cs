using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Services.Roles
{
    /// <summary>
    /// 角色管理应用服务
    /// </summary>
    [Authorize]
    public class RoleManagementAppService : ApplicationService, Interfaces.IRoleManagementAppService
    {
        private readonly IdentityRoleManager _roleManager;
        private readonly IdentityUserManager _userManager;
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IIdentityUserRepository _userRepository;

        public RoleManagementAppService(
            IdentityRoleManager roleManager,
            IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository,
            IIdentityUserRepository userRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        public async Task<PagedResultDto<RoleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var roles = await _roleRepository.GetListAsync(
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);
            var totalCount = await _roleRepository.GetCountAsync();

            return new PagedResultDto<RoleDto>(totalCount,
                ObjectMapper.Map<List<IdentityRole>, List<RoleDto>>(roles));
        }

        /// <summary>
        /// 获取单个角色
        /// </summary>
        public async Task<RoleDto> GetAsync(Guid id)
        {
            var role = await _roleRepository.GetAsync(id);
            return ObjectMapper.Map<IdentityRole, RoleDto>(role);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        [Authorize(Permissions.Permissions.Roles_Create)]
        public async Task<RoleDto> CreateAsync(string name)
        {
            var role = new IdentityRole(GuidGenerator.Create(), name, CurrentTenant.Id);

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"创建角色失败：{string.Join(", ", result.Errors)}");
            }

            return ObjectMapper.Map<IdentityRole, RoleDto>(role);
        }

        /// <summary>
        /// 更新角色名称
        /// </summary>
        [Authorize(Permissions.Permissions.Roles_Edit)]
        public async Task<RoleDto> UpdateAsync(Guid id, string name)
        {
            var role = await _roleRepository.GetAsync(id);
            var result = await _roleManager.SetRoleNameAsync(role, name);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"更新角色失败：{string.Join(", ", result.Errors)}");
            }

            return ObjectMapper.Map<IdentityRole, RoleDto>(role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        [Authorize(Permissions.Permissions.Roles_Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var role = await _roleRepository.GetAsync(id);
            if (role.IsStatic)
            {
                throw new UserFriendlyException("无法删除系统内置角色");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException($"删除角色失败：{string.Join(", ", result.Errors)}");
            }
        }

        /// <summary>
        /// 获取角色用户列表
        /// </summary>
        public async Task<List<RoleUserDto>> GetRoleUsersAsync(Guid roleId)
        {
            var role = await _roleRepository.GetAsync(roleId);
            var users = await _userRepository.GetListAsync();
            var roleUsers = new List<RoleUserDto>();
            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    roleUsers.Add(new RoleUserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName ?? string.Empty,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    });
                }
            }
            return roleUsers;
        }
    }
}
