using JiaCeMonitorSystem.AppRoles;
using JiaCeMonitorSystem.Devices;
using JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations;
using JiaCeMonitorSystem.FileManages;
using JiaCeMonitorSystem.ModuleButtons;
using JiaCeMonitorSystem.MonitoringItemTypes;
using MonitoringDataEntity = JiaCeMonitorSystem.MonitoringData.MonitoringData;
using JiaCeMonitorSystem.Notices;
using JiaCeMonitorSystem.Organizes;
using JiaCeMonitorSystem.Points;
using JiaCeMonitorSystem.ProjectPersonnels;
using JiaCeMonitorSystem.Projects;
using JiaCeMonitorSystem.SystemDictionaries;
using JiaCeMonitorSystem.SystemModules;
using JiaCeMonitorSystem.WarningRecords;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace JiaCeMonitorSystem.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class JiaCeMonitorSystemDbContext : AbpDbContext<JiaCeMonitorSystemDbContext>
    {
        #region DbSets

        /// <summary>
        /// 监测工程
        /// </summary>
        public DbSet<Project> Projects => Set<Project>();

        /// <summary>
        /// 监测点
        /// </summary>
        public DbSet<Point> Points => Set<Point>();

        /// <summary>
        /// 监测数据
        /// </summary>
        public DbSet<MonitoringDataEntity> MonitoringData => Set<MonitoringDataEntity>();

        /// <summary>
        /// 预警记录
        /// </summary>
        public DbSet<WarningRecord> WarningRecords => Set<WarningRecord>();

        /// <summary>
        /// 单位设备
        /// </summary>
        public DbSet<CompanyDevice> CompanyDevices => Set<CompanyDevice>();

        /// <summary>
        /// 设备分配记录
        /// </summary>
        public DbSet<DeviceAssignment> DeviceAssignments => Set<DeviceAssignment>();

        /// <summary>
        /// 系统菜单模块
        /// </summary>
        public DbSet<SystemModule> SystemModules => Set<SystemModule>();

        /// <summary>
        /// 系统菜单按钮
        /// </summary>
        public DbSet<ModuleButton> ModuleButtons => Set<ModuleButton>();

        /// <summary>
        /// 监测项目类型
        /// </summary>
        public DbSet<MonitoringItemType> MonitoringItemTypes => Set<MonitoringItemType>();

        /// <summary>
        /// 监测项目属性
        /// </summary>
        public DbSet<MonitoringItemProperty> MonitoringItemProperties => Set<MonitoringItemProperty>();

        /// <summary>
        /// 系统组织
        /// </summary>
        public DbSet<Organize> Organizes => Set<Organize>();

        /// <summary>
        /// 系统通知
        /// </summary>
        public DbSet<Notice> Notices => Set<Notice>();

        /// <summary>
        /// 项目人员安排
        /// </summary>
        public DbSet<ProjectPersonnel> ProjectPersonnels => Set<ProjectPersonnel>();

        /// <summary>
        /// 系统字典类型
        /// </summary>
        public DbSet<SystemDictionaryType> SystemDictionaryTypes => Set<SystemDictionaryType>();

        /// <summary>
        /// 系统字典
        /// </summary>
        public DbSet<SystemDictionary> SystemDictionaries => Set<SystemDictionary>();

        /// <summary>
        /// 文件管理
        /// </summary>
        public DbSet<UploadFile> UploadFiles => Set<UploadFile>();

        /// <summary>
        /// 业务角色
        /// </summary>
        public DbSet<AppRole> AppRoles => Set<AppRole>();

        #endregion

        public JiaCeMonitorSystemDbContext(DbContextOptions<JiaCeMonitorSystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ABP 内置模块配置
            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureOpenIddict();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            // 业务实体配置
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new PointConfiguration());
            builder.ApplyConfiguration(new MonitoringDataConfiguration());
            builder.ApplyConfiguration(new WarningRecordConfiguration());
            builder.ApplyConfiguration(new CompanyDeviceConfiguration());
            builder.ApplyConfiguration(new DeviceAssignmentConfiguration());

            // Phase 15 新增实体配置
            builder.ApplyConfiguration(new SystemModuleConfiguration());
            builder.ApplyConfiguration(new ModuleButtonConfiguration());
            builder.ApplyConfiguration(new MonitoringItemTypeConfiguration());
            builder.ApplyConfiguration(new MonitoringItemPropertyConfiguration());
            builder.ApplyConfiguration(new OrganizeConfiguration());
            builder.ApplyConfiguration(new NoticeConfiguration());
            builder.ApplyConfiguration(new ProjectPersonnelConfiguration());
            builder.ApplyConfiguration(new SystemDictionaryTypeConfiguration());
            builder.ApplyConfiguration(new SystemDictionaryConfiguration());
            builder.ApplyConfiguration(new UploadFileConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
        }
    }
}
