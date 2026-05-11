using System;
using AutoMapper;
using JiaCeMonitorSystem.Devices;
using JiaCeMonitorSystem.AppRoles;
using JiaCeMonitorSystem.Dtos.Accounts;
using JiaCeMonitorSystem.Dtos.AppRoles;
using JiaCeMonitorSystem.Dtos.Devices;
using JiaCeMonitorSystem.Dtos.FileManages;
using JiaCeMonitorSystem.Dtos.ModuleButtons;
using JiaCeMonitorSystem.Dtos.MonitoringData;
using JiaCeMonitorSystem.Dtos.MonitoringItemTypes;
using JiaCeMonitorSystem.Dtos.Notices;
using JiaCeMonitorSystem.Dtos.Organizes;
using JiaCeMonitorSystem.Dtos.Points;
using JiaCeMonitorSystem.Dtos.ProjectPersonnels;
using JiaCeMonitorSystem.Dtos.Projects;
using JiaCeMonitorSystem.Dtos.Roles;
using JiaCeMonitorSystem.Dtos.SystemDictionaries;
using JiaCeMonitorSystem.Dtos.SystemModules;
using JiaCeMonitorSystem.Dtos.Tenants;
using JiaCeMonitorSystem.Dtos.WarningRecords;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.FileManages;
using JiaCeMonitorSystem.ModuleButtons;
using JiaCeMonitorSystem.MonitoringItemTypes;
using JiaCeMonitorSystem.Notices;
using JiaCeMonitorSystem.Organizes;
using JiaCeMonitorSystem.Points;
using JiaCeMonitorSystem.ProjectPersonnels;
using JiaCeMonitorSystem.Projects;
using JiaCeMonitorSystem.SystemDictionaries;
using JiaCeMonitorSystem.SystemModules;
using JiaCeMonitorSystem.WarningRecords;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;
using MonitoringDataEntity = JiaCeMonitorSystem.MonitoringData.MonitoringData;
using TenantDto = JiaCeMonitorSystem.Dtos.Tenants.TenantDto;
using TenantCreateDto = JiaCeMonitorSystem.Dtos.Tenants.TenantCreateDto;

namespace JiaCeMonitorSystem
{
    /// <summary>
    /// Application层AutoMapper配置，映射实体与DTO
    /// </summary>
    public class JiaCeMonitorSystemApplicationAutoMapperProfile : Profile
    {
        public JiaCeMonitorSystemApplicationAutoMapperProfile()
        {
            // 监测工程模块
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<ProjectCreateDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<ProjectUpdateDto, Project>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectCode, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Points, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 测点模块
            CreateMap<Point, PointDto>()
                .ForMember(dest => dest.CurrentWarningLevelText, opt => opt.MapFrom(src => src.CurrentWarningLevel.ToString()));
            CreateMap<PointCreateDto, Point>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
                .ForMember(dest => dest.LastMonitoringTime, opt => opt.Ignore())
                .ForMember(dest => dest.MaxValue, opt => opt.Ignore())
                .ForMember(dest => dest.MinValue, opt => opt.Ignore())
                .ForMember(dest => dest.AverageValue, opt => opt.Ignore())
                .ForMember(dest => dest.DataCount, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentWarningLevel, opt => opt.Ignore())
                .ForMember(dest => dest.LastWarningTime, opt => opt.Ignore())
                .ForMember(dest => dest.TotalWarningCount, opt => opt.Ignore())
                .ForMember(dest => dest.ActiveWarningCount, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<PointUpdateDto, Point>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.PointCode, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentValue, opt => opt.Ignore())
                .ForMember(dest => dest.LastMonitoringTime, opt => opt.Ignore())
                .ForMember(dest => dest.MaxValue, opt => opt.Ignore())
                .ForMember(dest => dest.MinValue, opt => opt.Ignore())
                .ForMember(dest => dest.AverageValue, opt => opt.Ignore())
                .ForMember(dest => dest.DataCount, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentWarningLevel, opt => opt.Ignore())
                .ForMember(dest => dest.LastWarningTime, opt => opt.Ignore())
                .ForMember(dest => dest.TotalWarningCount, opt => opt.Ignore())
                .ForMember(dest => dest.ActiveWarningCount, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 监测数据模块
            CreateMap<MonitoringDataEntity, MonitoringDataDto>()
                .ForMember(dest => dest.DataQualityText, opt => opt.MapFrom(src => src.DataQuality.ToString()))
                .ForMember(dest => dest.DataStateText, opt => opt.MapFrom(src => src.DataState.ToString()))
                .ForMember(dest => dest.CollectionMethodText, opt => opt.MapFrom(src => src.CollectionMethod.ToString()));
            CreateMap<CreateMonitoringDataDto, MonitoringDataEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataQuality, opt => opt.MapFrom(_ => DataQuality.Normal))
                .ForMember(dest => dest.DataState, opt => opt.MapFrom(_ => DataState.Raw))
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<UpdateMonitoringDataDto, MonitoringDataEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PointId, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.ItemTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.MonitoringTime, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceId, opt => opt.Ignore())
                .ForMember(dest => dest.CollectorId, opt => opt.Ignore())
                .ForMember(dest => dest.CollectionMethod, opt => opt.Ignore())
                .ForMember(dest => dest.DataState, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 预警记录模块
            CreateMap<WarningRecord, WarningRecordDto>()
                .ForMember(dest => dest.WarningTypeText, opt => opt.MapFrom(src => src.WarningType.ToString()))
                .ForMember(dest => dest.WarningLevelText, opt => opt.MapFrom(src => src.WarningLevel.ToString()))
                .ForMember(dest => dest.HandleStatusText, opt => opt.MapFrom(src => src.HandleStatus.ToString()));
            CreateMap<HandleWarningInput, WarningRecord>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HandlerId, opt => opt.MapFrom(src => src.HandlerId))
                .ForMember(dest => dest.HandlerName, opt => opt.MapFrom(src => src.HandlerName))
                .ForMember(dest => dest.HandleSolution, opt => opt.MapFrom(src => src.HandleSolution))
                .ForMember(dest => dest.HandleResult, opt => opt.MapFrom(src => src.HandleResult));
            CreateMap<ConfirmWarningInput, WarningRecord>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // 设备模块
            CreateMap<CompanyDevice, CompanyDeviceDto>()
                .ForMember(dest => dest.DeviceTypeText, opt => opt.MapFrom(src => src.DeviceType.ToString()))
                .ForMember(dest => dest.DeviceStatusText, opt => opt.MapFrom(src => src.DeviceStatus.ToString()));
            CreateMap<CompanyDeviceCreateDto, CompanyDevice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceStatus, opt => opt.MapFrom(_ => DeviceStatus.Normal))
                .ForMember(dest => dest.CalibrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.NextCalibrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<CompanyDeviceUpdateDto, CompanyDevice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceCode, opt => opt.Ignore())
                .ForMember(dest => dest.DeviceStatus, opt => opt.Ignore())
                .ForMember(dest => dest.PurchaseDate, opt => opt.Ignore())
                .ForMember(dest => dest.UseDate, opt => opt.Ignore())
                .ForMember(dest => dest.CalibrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.NextCalibrationDate, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<DeviceAssignment, DeviceAssignmentDto>()
                .ForMember(dest => dest.AssignmentStatusText, opt => opt.MapFrom(src => src.AssignmentStatus.ToString()));

            // 租户模块（复用ABP Tenant）
            CreateMap<Tenant, TenantDto>();
            CreateMap<TenantCreateDto, Tenant>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // 业务角色模块
            CreateMap<AppRole, AppRoleDto>();
            CreateMap<AppRoleCreateDto, AppRole>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<AppRoleUpdateDto, AppRole>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // ABP IdentityRole（保留原有映射）
            CreateMap<IdentityRole, RoleDto>();

            // 系统菜单模块
            CreateMap<SystemModule, SystemModuleDto>();
            CreateMap<SystemModuleCreateDto, SystemModule>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<SystemModuleUpdateDto, SystemModule>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<SystemModule, SystemModuleTreeDto>();

            // 系统菜单按钮模块
            CreateMap<ModuleButton, ModuleButtonDto>()
                .ForMember(dest => dest.LocationText, opt => opt.MapFrom(src => src.Location.ToString()));
            CreateMap<ModuleButtonCreateDto, ModuleButton>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<ModuleButtonUpdateDto, ModuleButton>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ModuleId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 监测项目类型模块
            CreateMap<MonitoringItemType, MonitoringItemTypeDto>()
                .ForMember(dest => dest.CategoryText, opt => opt.MapFrom(src => src.Category.ToString()));
            CreateMap<MonitoringItemProperty, MonitoringItemPropertyDto>()
                .ForMember(dest => dest.DataTypeText, opt => opt.MapFrom(src => src.DataType.ToString()));
            CreateMap<MonitoringItemTypeCreateDto, MonitoringItemType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<MonitoringItemTypeUpdateDto, MonitoringItemType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Properties, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<MonitoringItemPropertyCreateDto, MonitoringItemProperty>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ItemTypeId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 系统组织模块
            CreateMap<Organize, OrganizeDto>();
            CreateMap<OrganizeCreateDto, Organize>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<OrganizeUpdateDto, Organize>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<Organize, OrganizeTreeDto>();

            // 系统通知模块
            CreateMap<Notice, NoticeDto>();
            CreateMap<NoticeCreateDto, Notice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<NoticeUpdateDto, Notice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 项目人员安排模块
            CreateMap<ProjectPersonnel, ProjectPersonnelDto>()
                .ForMember(dest => dest.RoleTypeText, opt => opt.MapFrom(src => src.RoleType.ToString()))
                .ForMember(dest => dest.WorkStatusText, opt => opt.MapFrom(src => src.WorkStatus.ToString()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.WorkStatus == WorkStatus.Active && (!src.EndDate.HasValue || src.EndDate.Value >= DateTime.UtcNow)))
                .ForMember(dest => dest.IsEnded, opt => opt.MapFrom(src => src.WorkStatus == WorkStatus.Ended || (src.EndDate.HasValue && src.EndDate.Value < DateTime.UtcNow)))
                .ForMember(dest => dest.ServiceDays, opt => opt.MapFrom(src => (src.EndDate ?? DateTime.UtcNow).Subtract(src.StartDate).Days));
            CreateMap<ProjectPersonnelCreateDto, ProjectPersonnel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<ProjectPersonnelUpdateDto, ProjectPersonnel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 系统字典类型模块
            CreateMap<SystemDictionaryType, SystemDictionaryTypeDto>();
            CreateMap<SystemDictionaryTypeCreateDto, SystemDictionaryType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<SystemDictionaryTypeUpdateDto, SystemDictionaryType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<SystemDictionaryType, SystemDictionaryTypeTreeDto>();

            // 系统字典模块
            CreateMap<SystemDictionary, SystemDictionaryDto>();
            CreateMap<SystemDictionaryCreateDto, SystemDictionary>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<SystemDictionaryUpdateDto, SystemDictionary>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ItemId, opt => opt.Ignore())
                .ForMember(dest => dest.Layers, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());

            // 文件管理模块
            CreateMap<UploadFile, UploadFileDto>()
                .ForMember(dest => dest.FileTypeText, opt => opt.MapFrom(src => src.FileType.ToString()))
                .ForMember(dest => dest.FileSizeDisplay, opt => opt.MapFrom(src => FormatFileSize(src.FileSize)));
            CreateMap<UploadFileCreateDto, UploadFile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Hash, opt => opt.Ignore())
                .ForMember(dest => dest.FilePath, opt => opt.Ignore())
                .ForMember(dest => dest.FileSize, opt => opt.Ignore())
                .ForMember(dest => dest.FileExtension, opt => opt.Ignore())
                .ForMember(dest => dest.FileBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
            CreateMap<UploadFileUpdateDto, UploadFile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Hash, opt => opt.Ignore())
                .ForMember(dest => dest.FilePath, opt => opt.Ignore())
                .ForMember(dest => dest.FileType, opt => opt.Ignore())
                .ForMember(dest => dest.FileSize, opt => opt.Ignore())
                .ForMember(dest => dest.FileExtension, opt => opt.Ignore())
                .ForMember(dest => dest.FileBy, opt => opt.Ignore())
                .ForMember(dest => dest.OrganizeId, opt => opt.Ignore())
                .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
                .ForMember(dest => dest.CreatorId, opt => opt.Ignore())
                .ForMember(dest => dest.LastModificationTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifierId, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeleterId, opt => opt.Ignore())
                .ForMember(dest => dest.DeletionTime, opt => opt.Ignore());
        }

        /// <summary>
        /// 格式化文件大小显示
        /// </summary>
        private static string FormatFileSize(long bytes)
        {
            const int scale = 1024;
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            if (bytes == 0) return "0 B";
            var magnitude = (int)Math.Log(bytes, scale);
            magnitude = Math.Min(magnitude, units.Length - 1);
            var size = bytes / Math.Pow(scale, magnitude);
            return $"{size:0.##} {units[magnitude]}";
        }
    }
}
