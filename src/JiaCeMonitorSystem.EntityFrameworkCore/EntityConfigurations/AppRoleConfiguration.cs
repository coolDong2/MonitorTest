using JiaCeMonitorSystem.AppRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 业务角色实体配置
    /// </summary>
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("JC_AppRoles", b => b.HasComment("业务角色"));

            builder.Property(r => r.EnCode)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("角色编号");

            builder.Property(r => r.FullName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("角色名称");

            builder.Property(r => r.CompanyName)
                .HasMaxLength(100)
                .HasComment("所属公司名称");

            builder.Property(r => r.Type)
                .HasMaxLength(50)
                .HasComment("角色类型名称");

            builder.Property(r => r.Description)
                .HasMaxLength(500)
                .HasComment("描述");

            builder.Property(r => r.PermissionButtonIds)
                .HasMaxLength(4000)
                .HasComment("权限按钮ID");

            builder.Property(r => r.PermissionFieldsIds)
                .HasMaxLength(4000)
                .HasComment("权限字段ID");

            builder.Property(r => r.Category)
                .HasComment("角色类型");

            builder.Property(r => r.SortCode)
                .HasComment("排序码");

            builder.Property(r => r.EnabledMark)
                .HasComment("是否启用");

            builder.Property(r => r.AllowEdit)
                .HasComment("允许编辑");

            builder.Property(r => r.AllowDelete)
                .HasComment("允许删除");

            builder.HasIndex(r => r.EnCode)
                .HasDatabaseName("IX_AppRoles_EnCode");

            builder.HasIndex(r => r.CompanyId)
                .HasDatabaseName("IX_AppRoles_CompanyId");
        }
    }
}
