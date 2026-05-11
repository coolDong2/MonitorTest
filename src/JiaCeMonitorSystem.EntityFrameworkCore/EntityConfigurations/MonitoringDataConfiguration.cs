using System.Text.Json;
using MonitoringDataEntity = JiaCeMonitorSystem.MonitoringData.MonitoringData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 监测数据实体配置
    /// </summary>
    public class MonitoringDataConfiguration : IEntityTypeConfiguration<MonitoringDataEntity>
    {
        public void Configure(EntityTypeBuilder<MonitoringDataEntity> builder)
        {
            builder.ToTable("JC_MonitoringData", "public");

            builder.Property(e => e.PointId)
                .IsRequired()
                .HasComment("测点ID");

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

            builder.Property(e => e.MonitoringTime)
                .IsRequired()
                .HasComment("监测时间");

            builder.Property(e => e.MonitoringValue)
                .IsRequired()
                .HasColumnType("decimal(18,4)")
                .HasComment("监测数值");

            builder.Property(e => e.DataQuality)
                .HasConversion<int>()
                .HasComment("数据质量：0=正常 1=异常 2=缺失 3=可疑");

            builder.Property(e => e.DataState)
                .HasConversion<int>()
                .HasComment("数据状态：0=原始 1=已审核 2=已归档");

            builder.Property(e => e.DeviceId)
                .HasComment("采集设备ID");

            builder.Property(e => e.DeviceName)
                .HasMaxLength(256)
                .HasComment("采集设备名称（冗余）");

            builder.Property(e => e.CollectorId)
                .HasComment("采集人ID");

            builder.Property(e => e.CollectorName)
                .HasMaxLength(128)
                .HasComment("采集人姓名（冗余）");

            builder.Property(e => e.CollectionMethod)
                .HasConversion<int>()
                .HasComment("采集方式：0=手动 1=自动 2=导入");

            // JsonDocument → jsonb
            builder.Property(e => e.ExtendedData)
                .HasConversion(
                    v => v == null ? (string?)null : v.RootElement.GetRawText(),
                    v => string.IsNullOrEmpty(v) ? null : JsonDocument.Parse(v, new JsonDocumentOptions()))
                .HasColumnType("jsonb")
                .HasComment("扩展监测数据（JSON格式）");

            builder.Property(e => e.DataRemark)
                .HasMaxLength(2000)
                .HasComment("数据备注");

            // 索引
            builder.HasIndex(e => new { e.PointId, e.MonitoringTime })
                .HasDatabaseName("IX_MonitoringData_PointId_MonitoringTime");

            builder.HasIndex(e => e.ProjectId)
                .HasDatabaseName("IX_MonitoringData_ProjectId");

            builder.HasIndex(e => e.MonitoringTime)
                .HasDatabaseName("IX_MonitoringData_MonitoringTime");

            builder.HasIndex(e => e.DataState)
                .HasDatabaseName("IX_MonitoringData_DataState");

            builder.HasIndex(e => e.PropertyId)
                .HasDatabaseName("IX_MonitoringData_PropertyId");
        }
    }
}
