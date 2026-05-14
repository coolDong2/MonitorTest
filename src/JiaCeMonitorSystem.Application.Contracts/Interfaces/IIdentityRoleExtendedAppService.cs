using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 身份角色扩展应用服务接口
    /// </summary>
    public interface IIdentityRoleExtendedAppService : IApplicationService
    {
        /// <summary>
        /// 获取角色下的用户列表
        /// </summary>
        Task<List<IdentityUserDto>> GetRoleUsersAsync(Guid roleId);
    }
}
