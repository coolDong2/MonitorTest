using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Notices;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 系统通知应用服务接口
    /// </summary>
    public interface INoticeAppService : IApplicationService
    {
        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<NoticeDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        Task<NoticeDto> CreateAsync(NoticeCreateDto input);

        /// <summary>
        /// 更新
        /// </summary>
        Task<NoticeDto> UpdateAsync(Guid id, NoticeUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
