using JiaCeMonitorSystem.MonitoringItemTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 监测项目属性实体配置
    /// </summary>
    public class MonitoringItemPropertyConfiguration : IEntityTypeConfiguration<MonitoringItemProperty>
    {
        public void Configure(EntityTypeBuilder<MonitoringItemProperty> builder)
        {
            builder.ToTable("JC_MonitoringItemProperties", "public");

            builder.Property(e => e.ItemTypeId)
                .IsRequired()
                .HasComment("所属监测项目类型ID");

            builder.Property(e => e.PropertyCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("属性编码");

            builder.Property(e => e.PropertyName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("属性名称");

            builder.Property(e => e.DataType)
                .HasConversion<int>()
                .HasComment("数据类型：0=字符串 1=数字 2=日期 3=布尔");

            builder.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasComment("单位");

            builder.Property(e => e.IsRequired)
                .HasComment("是否必填");

            builder.Property(e => e.SortCode)
                .HasComment("排序码");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("描述");

            builder.Property(e => e.WarningThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("预警阈值【属性级】");

            builder.Property(e => e.AlarmThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("报警阈值【属性级】");

            builder.Property(e => e.ChangeRateThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("变化率阈值（%）【属性级】");

            builder.Property(e => e.CumulativeThreshold)
                .HasColumnType("decimal(18,4)")
                .HasComment("累计变化阈值【属性级】");

            // 索引
            builder.HasIndex(e => e.ItemTypeId)
                .HasDatabaseName("IX_MonitoringItemProperties_ItemTypeId");

            builder.HasIndex(e => new { e.ItemTypeId, e.PropertyCode })
                .IsUnique()
                .HasDatabaseName("IX_MonitoringItemProperties_ItemTypeId_PropertyCode");
        }
    }
}
