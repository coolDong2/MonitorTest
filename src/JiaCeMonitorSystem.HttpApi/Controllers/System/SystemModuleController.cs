using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.SystemModules;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.System
{
    /// <summary>
    /// 系统菜单模块控制器
    /// </summary>
    [Route("api/app/module")]
    public class SystemModuleController : JiaCeMonitorSystemController
    {
        private readonly ISystemModuleAppService _systemModuleAppService;

        /// <summary>
        /// 初始化系统菜单模块控制器
        /// </summary>
        public SystemModuleController(ISystemModuleAppService systemModuleAppService)
        {
            _systemModuleAppService = systemModuleAppService;
        }

        /// <summary>
        /// 获取树形列表
        /// </summary>
        [HttpGet("tree-list")]
        public virtual Task<List<SystemModuleTreeDto>> GetTreeListAsync()
        {
            return _systemModuleAppService.GetTreeListAsync();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<SystemModuleDto>> GetListAsync([FromQuery] GetSystemModuleListInput input)
        {
            return _systemModuleAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<SystemModuleDto> GetModelAsync(Guid id)
        {
            return _systemModuleAppService.GetModelAsync(id);
        }


        /// <summary>
        /// 创建
        /// </summary>
        [HttpPost]
        public virtual Task<SystemModuleDto> CreateAsync([FromBody] SystemModuleCreateDto input)
        {
            return _systemModuleAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<SystemModuleDto> UpdateAsync(Guid id, [FromBody] SystemModuleUpdateDto input)
        {
            return _systemModuleAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _systemModuleAppService.DeleteAsync(id);
        }
    }
}
