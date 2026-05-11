using JiaCeMonitorSystem.SystemModules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 系统菜单模块实体配置
    /// </summary>
    public class SystemModuleConfiguration : IEntityTypeConfiguration<SystemModule>
    {
        public void Configure(EntityTypeBuilder<SystemModule> builder)
        {
            builder.ToTable("JC_SystemModules", "public");

            builder.Property(e => e.EnCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("编码");

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("名称");

            builder.Property(e => e.Icon)
                .HasMaxLength(128)
                .HasComment("图标");

            builder.Property(e => e.UrlAddress)
                .HasMaxLength(512)
                .HasComment("链接地址");

            builder.Property(e => e.Target)
                .HasMaxLength(64)
                .HasComment("打开目标");

            builder.Property(e => e.IsMenu)
                .HasComment("是否菜单");

            builder.Property(e => e.IsExpand)
                .HasComment("是否展开");

            builder.Property(e => e.IsPublic)
                .HasComment("是否公共");

            builder.Property(e => e.IsFields)
                .HasComment("是否字段");

            builder.Property(e => e.AllowEdit)
                .HasComment("允许编辑");

            builder.Property(e => e.AllowDelete)
                .HasComment("允许删除");

            builder.Property(e => e.SortCode)
                .HasComment("排序码");

            builder.Property(e => e.EnabledMark)
                .HasComment("是否启用");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("描述");

            builder.Property(e => e.Authorize)
                .HasMaxLength(512)
                .HasComment("授权");

            builder.Property(e => e.ParentId)
                .HasComment("父节点ID");

            builder.Property(e => e.Layers)
                .HasComment("层级");

            // 索引
            builder.HasIndex(e => e.ParentId)
                .HasDatabaseName("IX_SystemModules_ParentId");

            builder.HasIndex(e => e.EnCode)
                .HasDatabaseName("IX_SystemModules_EnCode");
        }
    }
}
