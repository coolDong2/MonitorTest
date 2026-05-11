using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.AppRoles;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 业务角色应用服务接口
    /// </summary>
    public interface IAppRoleAppService : IApplicationService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<AppRoleDto>> GetPageListAsync(GetAppRoleListInput input);

        /// <summary>
        /// 获取列表
        /// </summary>
        Task<List<AppRoleDto>> GetListAsync(string? keyword);

        /// <summary>
        /// 获取单个角色
        /// </summary>
        Task<AppRoleDto> GetModelAsync(Guid id);

        /// <summary>
        /// 新增或编辑角色
        /// </summary>
        Task<AppRoleDto> AddAndEditRoleAsync(AppRoleCreateDto input);

        /// <summary>
        /// 更新角色
        /// </summary>
        Task<AppRoleDto> UpdateAsync(Guid id, AppRoleUpdateDto input);

        /// <summary>
        /// 删除角色
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取权限树
        /// </summary>
        Task<List<PermissionTreeDto>> GetPermissionTreeAsync(Guid roleId);

        /// <summary>
        /// 获取权限字段树
        /// </summary>
        Task<List<PermissionTreeDto>> GetPermissionFieldsTreeAsync(Guid roleId, string? moduleIds);
    }
}
