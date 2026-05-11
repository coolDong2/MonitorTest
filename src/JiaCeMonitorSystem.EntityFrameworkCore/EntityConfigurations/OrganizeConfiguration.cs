using JiaCeMonitorSystem.Organizes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 系统组织实体配置
    /// </summary>
    public class OrganizeConfiguration : IEntityTypeConfiguration<Organize>
    {
        public void Configure(EntityTypeBuilder<Organize> builder)
        {
            builder.ToTable("JC_Organizes", "public");

            builder.Property(e => e.ParentId)
                .HasComment("父节点ID");

            builder.Property(e => e.Layers)
                .HasComment("层级");

            builder.Property(e => e.EnCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("编码");

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("全称");

            builder.Property(e => e.ShortName)
                .HasMaxLength(128)
                .HasComment("简称");

            builder.Property(e => e.CategoryId)
                .HasComment("分类ID");

            builder.Property(e => e.ManagerId)
                .HasComment("负责人ID（关联IdentityUser）");

            builder.Property(e => e.TelePhone)
                .HasMaxLength(32)
                .HasComment("电话");

            builder.Property(e => e.MobilePhone)
                .HasMaxLength(32)
                .HasComment("手机");

            builder.Property(e => e.WeChat)
                .HasMaxLength(64)
                .HasComment("微信");

            builder.Property(e => e.Fax)
                .HasMaxLength(32)
                .HasComment("传真");

            builder.Property(e => e.Email)
                .HasMaxLength(128)
                .HasComment("邮箱");

            builder.Property(e => e.AreaId)
                .HasComment("区域ID");

            builder.Property(e => e.Address)
                .HasMaxLength(512)
                .HasComment("地址");

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

            // 索引
            builder.HasIndex(e => e.ParentId)
                .HasDatabaseName("IX_Organizes_ParentId");

            builder.HasIndex(e => e.ManagerId)
                .HasDatabaseName("IX_Organizes_ManagerId");
        }
    }
}
