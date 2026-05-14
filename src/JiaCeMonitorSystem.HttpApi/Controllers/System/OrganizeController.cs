using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Organizes;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers.System
{
    /// <summary>
    /// 系统组织控制器
    /// </summary>
    [Route("api/app/organize")]
    public class OrganizeController : JiaCeMonitorSystemController
    {
        private readonly IOrganizeAppService _organizeAppService;

        /// <summary>
        /// 初始化系统组织控制器
        /// </summary>
        public OrganizeController(IOrganizeAppService organizeAppService)
        {
            _organizeAppService = organizeAppService;
        }

        /// <summary>
        /// 获取组织树
        /// </summary>
        [HttpGet("tree")]
        public virtual Task<List<OrganizeTreeDto>> GetOrganizeTreeAsync()
        {
            return _organizeAppService.GetOrganizeTreeAsync();
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<OrganizeDto> GetModelAsync(Guid id)
        {
            return _organizeAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        [HttpPost]
        public virtual Task<OrganizeDto> CreateAsync([FromBody] OrganizeCreateDto input)
        {
            return _organizeAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<OrganizeDto> UpdateAsync(Guid id, [FromBody] OrganizeUpdateDto input)
        {
            return _organizeAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _organizeAppService.DeleteAsync(id);
        }
    }
}
