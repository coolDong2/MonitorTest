using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.DomainServices;
using JiaCeMonitorSystem.Dtos.MonitoringData;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.MonitoringItemTypes;
using JiaCeMonitorSystem.Points;
using JiaCeMonitorSystem.Projects;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MonitoringDataEntity = JiaCeMonitorSystem.MonitoringData.MonitoringData;

namespace JiaCeMonitorSystem.Services.MonitoringData
{
    /// <summary>
    /// 监测数据应用服务
    /// </summary>
    [Authorize]
    public class MonitoringDataAppService :
        CrudAppService<MonitoringDataEntity, MonitoringDataDto, Guid, GetMonitoringDataListInput, CreateMonitoringDataDto, UpdateMonitoringDataDto>,
        IMonitoringDataAppService
    {
        private readonly WarningDomainService _warningDomainService;
        private readonly IRepository<Point, Guid> _pointRepository;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<MonitoringItemProperty, Guid> _monitoringItemPropertyRepository;

        public MonitoringDataAppService(
            IRepository<MonitoringDataEntity, Guid> repository,
            WarningDomainService warningDomainService,
            IRepository<Point, Guid> pointRepository,
            IRepository<Project, Guid> projectRepository,
            IRepository<MonitoringItemProperty, Guid> monitoringItemPropertyRepository) : base(repository)
        {
            _warningDomainService = warningDomainService;
            _pointRepository = pointRepository;
            _projectRepository = projectRepository;
            _monitoringItemPropertyRepository = monitoringItemPropertyRepository;
        }

        /// <summary>
        /// 根据测点ID获取历史数据
        /// </summary>
        public async Task<PagedResultDto<MonitoringDataHistoryDto>> GetHistoryListByPointIdAsync(Guid pointId, int currentPage, int pageSize)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(d => d.PointId == pointId).OrderByDescending(d => d.MonitoringTime);

            var totalCount = await AsyncExecuter.CountAsync(query);
            var dataList = await AsyncExecuter.ToListAsync(query.PageBy(currentPage - 1, pageSize));

            var dtos = dataList.Select(d => new MonitoringDataHistoryDto
            {
                Id = d.Id,
                PointId = d.PointId,
                SnapshotTime = d.MonitoringTime,
                FormattedSnapshotTime = d.MonitoringTime.ToString("yyyy-MM-dd HH:mm"),
                MonitoringValue = d.MonitoringValue,
                DataState = (int)d.DataState,
                DataStateText = d.DataState.ToString(),
                Collector = d.CollectorName,
                Remark = d.DataRemark,
                CreatorTime = d.CreationTime,
                CreatorUserName = null,
                FormattedCreatorTime = d.CreationTime.ToString("yyyy-MM-dd HH:mm")
            }).ToList();

            return new PagedResultDto<MonitoringDataHistoryDto>(totalCount, dtos);
        }

        /// <summary>
        /// 批量导入监测数据
        /// </summary>
        [Authorize(Permissions.Permissions.MonitoringData_Import)]
        public async Task BatchImportAsync(CreateMonitoringDataDto[] inputs)
        {
            foreach (var input in inputs)
            {
                await CreateAsync(input);
            }
        }

        /// <summary>
        /// 审核数据
        /// </summary>
        [Authorize(Permissions.Permissions.MonitoringData_Approve)]
        public async Task ApproveAsync(Guid id)
        {
            var data = await Repository.GetAsync(id);
            data.ApproveData();
            await Repository.UpdateAsync(data);
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        [Authorize(Permissions.Permissions.MonitoringData_Export)]
        public async Task<byte[]> ExportAsync(GetMonitoringDataListInput input)
        {
            var dataList = await GetListAsync(input);
            var csv = "MonitoringTime,MonitoringValue,DataQuality,CollectionMethod\n";
            foreach (var item in dataList.Items)
            {
                csv += $"{item.MonitoringTime},{item.MonitoringValue},{item.DataQualityText},{item.CollectionMethodText}\n";
            }
            return System.Text.Encoding.UTF8.GetBytes(csv);
        }

        /// <summary>
        /// 创建监测数据并触发预警判定
        /// 【重构】手动创建实体，校验PropertyId有效性，自动填充冗余字段
        /// </summary>
        [Authorize(Permissions.Permissions.MonitoringData_Create)]
        public override async Task<MonitoringDataDto> CreateAsync(CreateMonitoringDataDto input)
        {
            // 校验PropertyId是否属于该Point的ItemType
            await ValidatePropertyAsync(input.PointId, input.PropertyId);

            // 获取关联实体信息以填充冗余字段
            var point = await _pointRepository.GetAsync(input.PointId);
            var project = await _projectRepository.GetAsync(input.ProjectId);
            var property = await _monitoringItemPropertyRepository.GetAsync(input.PropertyId);

            var data = new MonitoringDataEntity(
                GuidGenerator.Create(),
                input.PointId,
                point.PointName,
                input.ProjectId,
                project.ProjectName,
                input.MonitoringTime,
                input.MonitoringValue,
                input.PropertyId,
                property.PropertyCode,
                property.PropertyName,
                property.Unit,
                input.ItemTypeId ?? point.ItemTypeId,
                point.ItemTypeName,
                Enums.DataQuality.Normal,
                input.DeviceId,
                input.DeviceName,
                input.CollectorId,
                input.CollectorName,
                (Enums.CollectionMethod)input.CollectionMethod,
                input.ExtendedData,
                input.DataRemark);

            data = await Repository.InsertAsync(data);

            // 触发预警判定
            await _warningDomainService.EvaluateAsync(data);

            return ObjectMapper.Map<MonitoringDataEntity, MonitoringDataDto>(data);
        }

        /// <summary>
        /// 校验监测属性是否属于测点的监测类型
        /// </summary>
        private async Task ValidatePropertyAsync(Guid pointId, Guid propertyId)
        {
            var point = await _pointRepository.GetAsync(pointId);
            if (!point.ItemTypeId.HasValue)
            {
                throw new BusinessException(ErrorCodes.MonitoringData_PropertyNotFound)
                    .WithData("PointId", pointId)
                    .WithData("Reason", "该测点未关联监测项目类型");
            }

            var property = await _monitoringItemPropertyRepository.FirstOrDefaultAsync(
                p => p.Id == propertyId && p.ItemTypeId == point.ItemTypeId.Value);

            if (property == null)
            {
                throw new BusinessException(ErrorCodes.MonitoringData_PropertyNotFound)
                    .WithData("PropertyId", propertyId)
                    .WithData("PointId", pointId);
            }
        }
    }
}
