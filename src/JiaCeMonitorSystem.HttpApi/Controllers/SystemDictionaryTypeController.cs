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
    /// 系统字典类型控制器
    /// </summary>
    [Route("api/app/system-dictionary-type")]
    public class SystemDictionaryTypeController : JiaCeMonitorSystemController
    {
        private readonly ISystemDictionaryTypeAppService _systemDictionaryTypeAppService;

        /// <summary>
        /// 初始化系统字典类型控制器
        /// </summary>
        public SystemDictionaryTypeController(ISystemDictionaryTypeAppService systemDictionaryTypeAppService)
        {
            _systemDictionaryTypeAppService = systemDictionaryTypeAppService;
        }

        /// <summary>
        /// 获取字典树
        /// </summary>
        [HttpGet("tree")]
        public virtual Task<List<SystemDictionaryTypeTreeDto>> GetDictionaryTreeAsync()
        {
            return _systemDictionaryTypeAppService.GetDictionaryTreeAsync();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet]
        public virtual Task<PagedResultDto<SystemDictionaryTypeDto>> GetPageListAsync([FromQuery] GetSystemDictionaryTypeListInput input)
        {
            return _systemDictionaryTypeAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<SystemDictionaryTypeDto> GetModelAsync(Guid id)
        {
            return _systemDictionaryTypeAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        [HttpPost]
        public virtual Task<SystemDictionaryTypeDto> CreateAsync([FromBody] SystemDictionaryTypeCreateDto input)
        {
            return _systemDictionaryTypeAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<SystemDictionaryTypeDto> UpdateAsync(Guid id, [FromBody] SystemDictionaryTypeUpdateDto input)
        {
            return _systemDictionaryTypeAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _systemDictionaryTypeAppService.DeleteAsync(id);
        }
    }
}
