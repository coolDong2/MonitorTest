using JiaCeMonitorSystem.SystemDictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 系统字典类型实体配置
    /// </summary>
    public class SystemDictionaryTypeConfiguration : IEntityTypeConfiguration<SystemDictionaryType>
    {
        public void Configure(EntityTypeBuilder<SystemDictionaryType> builder)
        {
            builder.ToTable("JC_SystemDictionaryTypes", "public");

            builder.Property(e => e.ParentId)
                .HasComment("父节点ID");

            builder.Property(e => e.EnCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("编码");

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("名称");

            builder.Property(e => e.IsTree)
                .HasComment("是否树形");

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
            builder.HasIndex(e => e.ParentId)
                .HasDatabaseName("IX_SystemDictionaryTypes_ParentId");

            builder.HasIndex(e => e.EnCode)
                .IsUnique()
                .HasDatabaseName("IX_SystemDictionaryTypes_EnCode");
        }
    }
}
