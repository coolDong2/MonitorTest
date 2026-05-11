using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Permissions;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 权限管理控制器
    /// </summary>
    [Route("api/app/permission")]
    public class PermissionController : JiaCeMonitorSystemController
    {
        private readonly IPermissionAppService _permissionAppService;

        /// <summary>
        /// 初始化权限控制器
        /// </summary>
        public PermissionController(IPermissionAppService permissionAppService)
        {
            _permissionAppService = permissionAppService;
        }

        /// <summary>
        /// 获取权限树
        /// </summary>
        [HttpGet("permission-tree")]
        public virtual Task<PermissionTreeDto> GetPermissionTreeAsync(
            [FromQuery] string providerName,
            [FromQuery] string providerKey)
        {
            return _permissionAppService.GetPermissionTreeAsync(providerName, providerKey);
        }

        /// <summary>
        /// 保存权限授权
        /// </summary>
        [HttpPost("grant")]
        public virtual Task GrantAsync([FromBody] PermissionGrantDto input)
        {
            return _permissionAppService.GrantAsync(input);
        }
    }
}
