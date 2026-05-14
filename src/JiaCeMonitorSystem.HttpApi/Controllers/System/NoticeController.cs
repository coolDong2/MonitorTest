using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Notices;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JiaCeMonitorSystem.Controllers.System
{
    /// <summary>
    /// 系统通知控制器
    /// </summary>
    [Route("api/app/notice")]
    public class NoticeController : JiaCeMonitorSystemController
    {
        private readonly INoticeAppService _noticeAppService;

        /// <summary>
        /// 初始化系统通知控制器
        /// </summary>
        public NoticeController(INoticeAppService noticeAppService)
        {
            _noticeAppService = noticeAppService;
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<NoticeDto> GetModelAsync(Guid id)
        {
            return _noticeAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 创建
        /// </summary>
        [HttpPost]
        public virtual Task<NoticeDto> CreateAsync([FromBody] NoticeCreateDto input)
        {
            return _noticeAppService.CreateAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut("{id}")]
        public virtual Task<NoticeDto> UpdateAsync(Guid id, [FromBody] NoticeUpdateDto input)
        {
            return _noticeAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _noticeAppService.DeleteAsync(id);
        }
    }
}
