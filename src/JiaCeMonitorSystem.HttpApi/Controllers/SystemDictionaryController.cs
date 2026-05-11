using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.SystemDictionaries;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers
{
    /// <summary>
    /// 系统字典控制器
    /// </summary>
    [Route("api/app/system-dictionary")]
    public class SystemDictionaryController : JiaCeMonitorSystemController
    {
        private readonly ISystemDictionaryAppService _systemDictionaryAppService;

        /// <summary>
        /// 初始化系统字典控制器
        /// </summary>
        public SystemDictionaryController(ISystemDictionaryAppService systemDictionaryAppService)
        {
            _systemDictionaryAppService = systemDictionaryAppService;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet("page-list")]
        public virtual Task<PagedResultDto<SystemDictionaryDto>> GetPageListAsync([FromQuery] GetSystemDictionaryListInput input)
        {
            return _systemDictionaryAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        [HttpGet]
        public virtual Task<List<SystemDictionaryDto>> GetListAsync([FromQuery] GetSystemDictionaryListInput input)
        {
            return _systemDictionaryAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<SystemDictionaryDto> GetModelAsync(Guid id)
        {
            return _systemDictionaryAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        [HttpPost]
        public virtual Task<SystemDictionaryDto> CreateAsync([FromBody] SystemDictionaryCreateDto input)
        {
            return _systemDictionaryAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<SystemDictionaryDto> UpdateAsync(Guid id, [FromBody] SystemDictionaryUpdateDto input)
        {
            return _systemDictionaryAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _systemDictionaryAppService.DeleteAsync(id);
        }
    }
}
