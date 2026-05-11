using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ModuleButtons;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 系统菜单按钮控制器
    /// </summary>
    [Route("api/app/module-button")]
    public class ModuleButtonController : JiaCeMonitorSystemController
    {
        private readonly IModuleButtonAppService _moduleButtonAppService;

        /// <summary>
        /// 初始化系统菜单按钮控制器
        /// </summary>
        public ModuleButtonController(IModuleButtonAppService moduleButtonAppService)
        {
            _moduleButtonAppService = moduleButtonAppService;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<ModuleButtonDto>> GetPageListAsync([FromQuery] GetModuleButtonListInput input)
        {
            return _moduleButtonAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<ModuleButtonDto> GetModelAsync(Guid id)
        {
            return _moduleButtonAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        [HttpPost]
        public virtual Task<ModuleButtonDto> CreateAsync([FromBody] ModuleButtonCreateDto input)
        {
            return _moduleButtonAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<ModuleButtonDto> UpdateAsync(Guid id, [FromBody] ModuleButtonUpdateDto input)
        {
            return _moduleButtonAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _moduleButtonAppService.DeleteAsync(id);
        }
    }
}
