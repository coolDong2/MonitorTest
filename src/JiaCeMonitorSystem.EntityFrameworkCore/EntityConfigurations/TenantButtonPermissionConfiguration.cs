using JiaCeMonitorSystem.TenantManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 租户按钮权限实体配置
    /// </summary>
    public class TenantButtonPermissionConfiguration : IEntityTypeConfiguration<TenantButtonPermission>
    {
        public void Configure(EntityTypeBuilder<TenantButtonPermission> builder)
        {
            builder.ToTable("JC_TenantButtonPermissions", "public");

            builder.Property(e => e.TenantId)
                .IsRequired()
                .HasComment("关联租户Id");

            builder.Property(e => e.ButtonId)
                .IsRequired()
                .HasComment("按钮Id");

            builder.Property(e => e.RoleId)
                .HasComment("角色Id");

            builder.HasIndex(e => new { e.TenantId, e.ButtonId, e.RoleId })
                .IsUnique()
                .HasDatabaseName("IX_JC_TenantButtonPermissions_TenantId_ButtonId_RoleId");

            builder.Property(e => e.IsGranted)
                .IsRequired()
                .HasDefaultValue(true)
                .HasComment("是否已授权");
        }
    }
}
