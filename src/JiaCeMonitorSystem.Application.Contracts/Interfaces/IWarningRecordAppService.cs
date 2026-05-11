using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.WarningRecords;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 预警记录应用服务接口
    /// </summary>
    public interface IWarningRecordAppService :
        ICrudAppService<WarningRecordDto, Guid, GetWarningListInput, HandleWarningInput, ConfirmWarningInput>
    {
        /// <summary>
        /// 分配处理人
        /// </summary>
        Task AssignHandlerAsync(Guid id, Guid handlerId, string handlerName);

        /// <summary>
        /// 处理预警记录
        /// </summary>
        Task HandleAsync(HandleWarningInput input);

        /// <summary>
        /// 确认预警处理结果
        /// </summary>
        Task ConfirmAsync(ConfirmWarningInput input);

        /// <summary>
        /// 关闭预警
        /// </summary>
        Task CloseAsync(Guid id, string reason);

        /// <summary>
        /// 获取预警统计
        /// </summary>
        Task<WarningStatisticsDto> GetStatisticsAsync(Guid? projectId, Guid? pointId);
    }
}
