using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.ModuleButtons;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.ModuleButtons;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.ModuleButtons
{
    /// <summary>
    /// 系统菜单按钮应用服务
    /// </summary>
    [Authorize]
    public class ModuleButtonAppService :
        CrudAppService<ModuleButton, ModuleButtonDto, Guid, GetModuleButtonListInput, ModuleButtonCreateDto, ModuleButtonUpdateDto>,
        IModuleButtonAppService
    {
        public ModuleButtonAppService(IRepository<ModuleButton, Guid> repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取分页列表（按模块筛选）
        /// </summary>
        public async Task<PagedResultDto<ModuleButtonDto>> GetPageListAsync(GetModuleButtonListInput input)
        {
            var query = await Repository.GetQueryableAsync();
            if (input.ModuleId.HasValue)
            {
                query = query.Where(b => b.ModuleId == input.ModuleId.Value);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);
            var buttons = await AsyncExecuter.ToListAsync(query.OrderBy(b => b.SortCode).PageBy(input));
            var buttonDtos = ObjectMapper.Map<List<ModuleButton>, List<ModuleButtonDto>>(buttons);

            return new PagedResultDto<ModuleButtonDto>(totalCount, buttonDtos);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<ModuleButtonDto> GetModelAsync(Guid id)
        {
            var button = await Repository.GetAsync(id);
            return ObjectMapper.Map<ModuleButton, ModuleButtonDto>(button);
        }
    }
}
