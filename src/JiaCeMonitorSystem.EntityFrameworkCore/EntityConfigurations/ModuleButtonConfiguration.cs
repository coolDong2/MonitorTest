using JiaCeMonitorSystem.ModuleButtons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 系统菜单按钮实体配置
    /// </summary>
    public class ModuleButtonConfiguration : IEntityTypeConfiguration<ModuleButton>
    {
        public void Configure(EntityTypeBuilder<ModuleButton> builder)
        {
            builder.ToTable("JC_ModuleButtons", "public");

            builder.Property(e => e.ModuleId)
                .IsRequired()
                .HasComment("所属模块ID");

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

            builder.Property(e => e.Location)
                .HasConversion<int>()
                .HasComment("按钮位置：0=工具栏 1=行内 2=右键菜单");

            builder.Property(e => e.JsEvent)
                .HasMaxLength(256)
                .HasComment("JS事件");

            builder.Property(e => e.UrlAddress)
                .HasMaxLength(512)
                .HasComment("链接地址");

            builder.Property(e => e.Split)
                .HasComment("是否有分割线");

            builder.Property(e => e.IsPublic)
                .HasComment("是否公共");

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

            // 索引
            builder.HasIndex(e => e.ModuleId)
                .HasDatabaseName("IX_ModuleButtons_ModuleId");

            builder.HasIndex(e => new { e.ModuleId, e.EnCode })
                .IsUnique()
                .HasDatabaseName("IX_ModuleButtons_ModuleId_EnCode");
        }
    }
}
