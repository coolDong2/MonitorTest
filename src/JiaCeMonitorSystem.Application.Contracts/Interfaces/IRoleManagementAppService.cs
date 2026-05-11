using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Roles;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 角色管理应用服务接口
    /// </summary>
    public interface IRoleManagementAppService : IApplicationService
    {
        /// <summary>
        /// 获取角色列表
        /// </summary>
        Task<PagedResultDto<RoleDto>> GetListAsync(PagedAndSortedResultRequestDto input);

        /// <summary>
        /// 获取单个角色
        /// </summary>
        Task<RoleDto> GetAsync(Guid id);

        /// <summary>
        /// 创建角色
        /// </summary>
        Task<RoleDto> CreateAsync(string name);

        /// <summary>
        /// 更新角色名称
        /// </summary>
        Task<RoleDto> UpdateAsync(Guid id, string name);

        /// <summary>
        /// 删除角色
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取角色用户列表
        /// </summary>
        Task<List<RoleUserDto>> GetRoleUsersAsync(Guid roleId);
    }
}
