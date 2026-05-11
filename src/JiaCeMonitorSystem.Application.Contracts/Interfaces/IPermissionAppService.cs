using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Permissions;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 权限管理应用服务接口
    /// </summary>
    public interface IPermissionAppService : IApplicationService
    {
        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="providerName">提供者名称（Role/User）</param>
        /// <param name="providerKey">提供者Key（角色ID或用户ID）</param>
        Task<PermissionTreeDto> GetPermissionTreeAsync(string providerName, string providerKey);

        /// <summary>
        /// 保存权限授权
        /// </summary>
        Task GrantAsync(PermissionGrantDto input);
    }
}
