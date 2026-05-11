using JiaCeMonitorSystem.MonitoringItemTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 监测项目类型实体配置
    /// </summary>
    public class MonitoringItemTypeConfiguration : IEntityTypeConfiguration<MonitoringItemType>
    {
        public void Configure(EntityTypeBuilder<MonitoringItemType> builder)
        {
            builder.ToTable("JC_MonitoringItemTypes", "public");

            builder.Property(e => e.TypeCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("类型编码");

            builder.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("类型名称");

            builder.Property(e => e.Category)
                .HasConversion<int>()
                .HasComment("监测分类：0=位移监测 1=沉降监测 2=应力监测 3=水文监测 4=环境监测");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("描述");

            builder.Property(e => e.SortCode)
                .HasComment("排序码");

            builder.Property(e => e.EnabledMark)
                .HasComment("是否启用");

            // 索引
            builder.HasIndex(e => e.TypeCode)
                .IsUnique()
                .HasDatabaseName("IX_MonitoringItemTypes_TypeCode");
        }
    }
}
