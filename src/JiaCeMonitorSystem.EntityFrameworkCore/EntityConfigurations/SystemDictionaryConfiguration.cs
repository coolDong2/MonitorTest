using JiaCeMonitorSystem.SystemDictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 系统字典实体配置
    /// </summary>
    public class SystemDictionaryConfiguration : IEntityTypeConfiguration<SystemDictionary>
    {
        public void Configure(EntityTypeBuilder<SystemDictionary> builder)
        {
            builder.ToTable("JC_SystemDictionaries", "public");

            builder.Property(e => e.ItemId)
                .IsRequired()
                .HasComment("字典类型ID");

            builder.Property(e => e.ItemCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("字典编码");

            builder.Property(e => e.ItemName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("字典名称");

            builder.Property(e => e.SimpleSpelling)
                .HasMaxLength(128)
                .HasComment("简拼");

            builder.Property(e => e.IsDefault)
                .HasComment("是否默认");

            builder.Property(e => e.Layers)
                .HasComment("层级");

            builder.Property(e => e.SortCode)
                .HasComment("排序码");

            builder.Property(e => e.EnabledMark)
                .HasComment("是否启用");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("描述");

            // 索引
            builder.HasIndex(e => e.ItemId)
                .HasDatabaseName("IX_SystemDictionaries_ItemId");

            builder.HasIndex(e => new { e.ItemId, e.ItemCode })
                .IsUnique()
                .HasDatabaseName("IX_SystemDictionaries_ItemId_ItemCode");
        }
    }
}
