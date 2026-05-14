using JiaCeMonitorSystem.TenantManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 租户用户扩展实体配置
    /// </summary>
    public class TenantUserExtensionConfiguration : IEntityTypeConfiguration<TenantUserExtension>
    {
        public void Configure(EntityTypeBuilder<TenantUserExtension> builder)
        {
            builder.ToTable("JC_TenantUserExtensions", "public");

            builder.Property(e => e.UserId)
                .IsRequired()
                .HasComment("关联用户Id");

            builder.HasIndex(e => e.UserId)
                .IsUnique()
                .HasDatabaseName("IX_JC_TenantUserExtensions_UserId");

            builder.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .HasComment("单位编码");

            builder.Property(e => e.UserType)
                .IsRequired()
                .HasDefaultValue(UserType.TenantUser)
                .HasComment("用户类型");

            builder.Property(e => e.TenantId)
                .IsRequired()
                .HasComment("关联租户Id");
        }
    }
}
