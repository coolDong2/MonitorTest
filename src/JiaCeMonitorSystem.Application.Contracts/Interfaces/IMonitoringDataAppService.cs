using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.MonitoringData;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 监测数据应用服务接口
    /// </summary>
    public interface IMonitoringDataAppService :
        ICrudAppService<MonitoringDataDto, Guid, GetMonitoringDataListInput, CreateMonitoringDataDto, UpdateMonitoringDataDto>
    {
        /// <summary>
        /// 根据测点ID获取历史数据
        /// </summary>
        Task<PagedResultDto<MonitoringDataHistoryDto>> GetHistoryListByPointIdAsync(Guid pointId, int currentPage, int pageSize);

        /// <summary>
        /// 批量导入监测数据
        /// </summary>
        Task BatchImportAsync(CreateMonitoringDataDto[] inputs);

        /// <summary>
        /// 审核数据
        /// </summary>
        Task ApproveAsync(Guid id);

        /// <summary>
        /// 数据导出
        /// </summary>
        Task<byte[]> ExportAsync(GetMonitoringDataListInput input);
    }
}
