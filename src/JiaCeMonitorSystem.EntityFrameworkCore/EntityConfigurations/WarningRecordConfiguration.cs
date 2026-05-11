using JiaCeMonitorSystem.WarningRecords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 预警记录实体配置
    /// </summary>
    public class WarningRecordConfiguration : IEntityTypeConfiguration<WarningRecord>
    {
        public void Configure(EntityTypeBuilder<WarningRecord> builder)
        {
            builder.ToTable("JC_WarningRecords", "public");

            builder.Property(e => e.PointId)
                .IsRequired()
                .HasComment("监测点ID");

            builder.Property(e => e.PointName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("测点名称（冗余）");

            builder.Property(e => e.ProjectId)
                .IsRequired()
                .HasComment("项目ID");

            builder.Property(e => e.ProjectName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("项目名称（冗余）");

            builder.Property(e => e.ItemTypeId)
                .HasComment("监测项目类型ID");

            builder.Property(e => e.ItemTypeName)
                .HasMaxLength(256)
                .HasComment("监测项目类型名称（冗余）");

            builder.Property(e => e.PropertyId)
                .IsRequired()
                .HasComment("监测属性ID（外键，关联MonitoringItemProperty）【重构新增】");

            builder.Property(e => e.PropertyCode)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("属性编码（冗余）");

            builder.Property(e => e.PropertyName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("属性名称（冗余）");

            builder.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasComment("单位（冗余）");

            builder.Property(e => e.MonitoringDataId)
                .HasComment("触发该预警的监测数据ID");

            builder.Property(e => e.DataState)
                .HasConversion<int>()
                .HasComment("数据状态（冗余）");

            builder.Property(e => e.CollectorName)
                .HasMaxLength(128)
                .HasComment("采集人姓名（冗余）");

            builder.Property(e => e.DataRemark)
                .HasMaxLength(2000)
                .HasComment("数据备注（冗余）");

            builder.Property(e => e.WarningTime)
                .IsRequired()
                .HasComment("预警时间");

            builder.Property(e => e.MonitoringTime)
                .IsRequired()
                .HasComment("触发监测时间");

            builder.Property(e => e.MonitoringValue)
                .IsRequired()
                .HasColumnType("decimal(18,4)")
                .HasComment("触发监测值");

            builder.Property(e => e.WarningType)
                .HasConversion<int>()
                .HasComment("预警类型：0=阈值超限 1=变化率超限 2=累计变化超限");

            builder.Property(e => e.WarningLevel)
                .HasConversion<int>()
                .HasComment("预警级别：0=提示 1=一级预警 2=二级预警 3=三级预警");

            builder.Property(e => e.TriggerValue)
                .IsRequired()
                .HasColumnType("decimal(18,4)")
                .HasComment("触发值");

            builder.Property(e => e.ThresholdValue)
                .IsRequired()
                .HasColumnType("decimal(18,4)")
                .HasComment("阈值设定值");

            builder.Property(e => e.PreviousValue)
                .HasColumnType("decimal(18,4)")
                .HasComment("前次监测值");

            builder.Property(e => e.ChangeRate)
                .HasColumnType("decimal(18,4)")
                .HasComment("变化率（%）");

            builder.Property(e => e.CumulativeChange)
                .HasColumnType("decimal(18,4)")
                .HasComment("累计变化量");

            builder.Property(e => e.WarningContent)
                .IsRequired()
                .HasMaxLength(2000)
                .HasComment("预警内容描述");

            builder.Property(e => e.SuggestedAction)
                .HasMaxLength(2000)
                .HasComment("建议措施");

            builder.Property(e => e.HandlerId)
                .HasComment("处理负责人ID");

            builder.Property(e => e.HandlerName)
                .HasMaxLength(128)
                .HasComment("处理负责人姓名");

            builder.Property(e => e.HandleStatus)
                .HasConversion<int>()
                .HasComment("处理状态：0=未处理 1=处理中 2=已处理 3=已确认 4=已关闭");

            builder.Property(e => e.HandleTime)
                .HasComment("处理完成时间");

            builder.Property(e => e.HandleSolution)
                .HasMaxLength(4000)
                .HasComment("处理方案");

            builder.Property(e => e.HandleResult)
                .HasMaxLength(4000)
                .HasComment("处理结果");

            builder.Property(e => e.ConfirmerId)
                .HasComment("确认人ID");

            builder.Property(e => e.ConfirmerName)
                .HasMaxLength(128)
                .HasComment("确认人姓名");

            builder.Property(e => e.ConfirmTime)
                .HasComment("确认时间");

            builder.Property(e => e.ConfirmRemark)
                .HasMaxLength(2000)
                .HasComment("确认备注");

            // 索引
            builder.HasIndex(e => new { e.PointId, e.HandleStatus })
                .HasDatabaseName("IX_WarningRecords_PointId_HandleStatus");

            builder.HasIndex(e => e.ProjectId)
                .HasDatabaseName("IX_WarningRecords_ProjectId");

            builder.HasIndex(e => e.HandleStatus)
                .HasDatabaseName("IX_WarningRecords_HandleStatus");

            builder.HasIndex(e => e.WarningLevel)
                .HasDatabaseName("IX_WarningRecords_WarningLevel");

            builder.HasIndex(e => e.MonitoringTime)
                .HasDatabaseName("IX_WarningRecords_MonitoringTime");

            builder.HasIndex(e => e.PropertyId)
                .HasDatabaseName("IX_WarningRecords_PropertyId");
        }
    }
}
