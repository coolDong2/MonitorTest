using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Notices;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.Notices;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.Notices
{
    /// <summary>
    /// 系统通知应用服务
    /// </summary>
    [Authorize]
    public class NoticeAppService :
        CrudAppService<Notice, NoticeDto, Guid, GetNoticeListInput, NoticeCreateDto, NoticeUpdateDto>,
        INoticeAppService
    {
        public NoticeAppService(IRepository<Notice, Guid> repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<NoticeDto> GetModelAsync(Guid id)
        {
            var notice = await Repository.GetAsync(id);
            return ObjectMapper.Map<Notice, NoticeDto>(notice);
        }
    }
}
