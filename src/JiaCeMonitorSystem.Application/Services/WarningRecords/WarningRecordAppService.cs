using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.WarningRecords;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.WarningRecords;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.WarningRecords
{
    /// <summary>
    /// 预警记录应用服务
    /// </summary>
    [Authorize]
    public class WarningRecordAppService :
        CrudAppService<WarningRecord, WarningRecordDto, Guid, GetWarningListInput, HandleWarningInput, ConfirmWarningInput>,
        IWarningRecordAppService
    {
        public WarningRecordAppService(IRepository<WarningRecord, Guid> repository) : base(repository)
        {
        }

        /// <summary>
        /// 分配处理人
        /// </summary>
        [Authorize(Permissions.Permissions.Warnings_Handle)]
        public async Task AssignHandlerAsync(Guid id, Guid handlerId, string handlerName)
        {
            var warning = await Repository.GetAsync(id);
            warning.AssignHandler(handlerId, handlerName);
            await Repository.UpdateAsync(warning);
        }

        /// <summary>
        /// 处理预警记录
        /// </summary>
        [Authorize(Permissions.Permissions.Warnings_Handle)]
        public async Task HandleAsync(HandleWarningInput input)
        {
            var warning = await Repository.GetAsync(input.Id);
            if (warning.HandlerId == null)
            {
                warning.AssignHandler(input.HandlerId, input.HandlerName);
            }
            warning.SubmitSolution(input.HandleSolution, input.HandleResult);
            await Repository.UpdateAsync(warning);
        }

        /// <summary>
        /// 确认预警处理结果
        /// </summary>
        [Authorize(Permissions.Permissions.Warnings_Confirm)]
        public async Task ConfirmAsync(ConfirmWarningInput input)
        {
            var warning = await Repository.GetAsync(input.Id);
            warning.Confirm(CurrentUser.Id.GetValueOrDefault(), CurrentUser.UserName ?? string.Empty, input.ConfirmRemark);
            await Repository.UpdateAsync(warning);
        }

        /// <summary>
        /// 关闭预警
        /// </summary>
        [Authorize(Permissions.Permissions.Warnings_Close)]
        public async Task CloseAsync(Guid id, string reason)
        {
            var warning = await Repository.GetAsync(id);
            warning.Close(reason);
            await Repository.UpdateAsync(warning);
        }

        /// <summary>
        /// 获取预警统计
        /// </summary>
        [Authorize(Permissions.Permissions.Warnings_ViewStatistics)]
        public async Task<WarningStatisticsDto> GetStatisticsAsync(Guid? projectId, Guid? pointId)
        {
            var warnings = await Repository.GetListAsync(w =>
                (!projectId.HasValue || w.ProjectId == projectId.Value) &&
                (!pointId.HasValue || w.PointId == pointId.Value));

            return new WarningStatisticsDto
            {
                TotalCount = warnings.Count,
                UnhandledCount = warnings.Where(w => w.HandleStatus == HandleStatus.Unhandled).Count(),
                HandlingCount = warnings.Where(w => w.HandleStatus == HandleStatus.InProgress).Count(),
                HandledCount = warnings.Where(w => w.HandleStatus == HandleStatus.Handled).Count(),
                ConfirmedCount = warnings.Where(w => w.ConfirmerId.HasValue).Count(),
                ClosedCount = warnings.Where(w => w.HandleStatus == HandleStatus.Closed).Count(),
                Level1Count = warnings.Where(w => w.WarningLevel == WarningLevel.Level1Notice).Count(),
                Level2Count = warnings.Where(w => w.WarningLevel == WarningLevel.Level2Warning).Count(),
                Level3Count = warnings.Where(w => w.WarningLevel == WarningLevel.Level3Danger).Count()
            };
        }
    }
}
