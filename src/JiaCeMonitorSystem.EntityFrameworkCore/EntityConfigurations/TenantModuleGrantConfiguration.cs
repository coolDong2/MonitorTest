using JiaCeMonitorSystem.TenantManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 租户模块授权实体配置
    /// </summary>
    public class TenantModuleGrantConfiguration : IEntityTypeConfiguration<TenantModuleGrant>
    {
        public void Configure(EntityTypeBuilder<TenantModuleGrant> builder)
        {
            builder.ToTable("JC_TenantModuleGrants", "public");

            builder.Property(e => e.TenantId)
                .IsRequired()
                .HasComment("关联租户Id");

            builder.Property(e => e.ModuleId)
                .IsRequired()
                .HasComment("系统模块Id");

            builder.HasIndex(e => new { e.TenantId, e.ModuleId })
                .IsUnique()
                .HasDatabaseName("IX_JC_TenantModuleGrants_TenantId_ModuleId");

            builder.Property(e => e.IsGranted)
                .IsRequired()
                .HasDefaultValue(true)
                .HasComment("是否已授权");

            builder.Property(e => e.GrantDate)
                .HasComment("授权日期");

            builder.Property(e => e.ExpireDate)
                .HasComment("授权到期日期");
        }
    }
}
