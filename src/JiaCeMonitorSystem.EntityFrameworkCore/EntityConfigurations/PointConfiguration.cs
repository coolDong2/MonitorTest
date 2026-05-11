using System.Text.Json;
using JiaCeMonitorSystem.Points;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 测点实体配置
    /// </summary>
    public class PointConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.ToTable("JC_Points", "public");

            builder.Property(e => e.ProjectId)
                .IsRequired()
                .HasComment("所属项目ID");

            builder.Property(e => e.PointCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("监测点编号");

            builder.Property(e => e.PointName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("监测点名称");

            builder.Property(e => e.ItemTypeId)
                .HasComment("监测项目类型ID");

            builder.Property(e => e.ItemTypeName)
                .HasMaxLength(256)
                .HasComment("监测项目类型名称（冗余）");

            builder.Property(e => e.LocationX)
                .HasColumnType("decimal(18,4)")
                .HasComment("X坐标/经度");

            builder.Property(e => e.LocationY)
                .HasColumnType("decimal(18,4)")
                .HasComment("Y坐标/纬度");

            builder.Property(e => e.LocationZ)
                .HasColumnType("decimal(18,4)")
                .HasComment("Z坐标/高程");

            builder.Property(e => e.CurrentValue)
                .HasColumnType("decimal(18,4)")
                .HasComment("当前监测值");

            builder.Property(e => e.LastMonitoringTime)
                .HasComment("最后监测时间");

            builder.Property(e => e.MonitoringFrequency)
                .HasComment("监测频率（天）");

            builder.Property(e => e.MaxValue)
                .HasColumnType("decimal(18,4)")
                .HasComment("历史最大值");

            builder.Property(e => e.MinValue)
                .HasColumnType("decimal(18,4)")
                .HasComment("历史最小值");

            builder.Property(e => e.AverageValue)
                .HasColumnType("decimal(18,4)")
                .HasComment("历史平均值");

            builder.Property(e => e.DataCount)
                .HasComment("数据点数");

            builder.Property(e => e.WarningThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("预警阈值");

            builder.Property(e => e.AlarmThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("报警阈值");

            builder.Property(e => e.ChangeRateThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("变化率阈值（%）");

            builder.Property(e => e.CumulativeThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("累计变化阈值");

            builder.Property(e => e.CurrentWarningLevel)
                .HasConversion<int>()
                .HasComment("当前预警级别：0=无 1=提示 2=一级预警 3=二级预警");

            builder.Property(e => e.LastWarningTime)
                .HasComment("最后预警时间");

            builder.Property(e => e.TotalWarningCount)
                .HasComment("总预警次数");

            builder.Property(e => e.ActiveWarningCount)
                .HasComment("当前活跃预警数");

            // JsonDocument → jsonb
            builder.Property(e => e.ExtendedProperties)
                .HasConversion(
                    v => v == null ? (string?)null : v.RootElement.GetRawText(),
                    v => string.IsNullOrEmpty(v) ? null : JsonDocument.Parse(v, new JsonDocumentOptions()))
                .HasColumnType("jsonb")
                .HasComment("扩展属性（JSON格式）");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("点位描述");

            // 索引
            builder.HasIndex(e => new { e.ProjectId, e.PointCode })
                .IsUnique()
                .HasDatabaseName("IX_Points_ProjectId_PointCode");

            builder.HasIndex(e => e.PointCode)
                .HasDatabaseName("IX_Points_PointCode");

            builder.HasIndex(e => e.ProjectId)
                .HasDatabaseName("IX_Points_ProjectId");

            builder.HasIndex(e => e.CurrentWarningLevel)
                .HasDatabaseName("IX_Points_CurrentWarningLevel");
        }
    }
}
