using JiaCeMonitorSystem.TenantManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 租户配置实体配置
    /// </summary>
    public class TenantConfigurationConfiguration : IEntityTypeConfiguration<TenantConfiguration>
    {
        public void Configure(EntityTypeBuilder<TenantConfiguration> builder)
        {
            builder.ToTable("JC_TenantConfigurations", "public");

            builder.Property(e => e.TenantId)
                .IsRequired()
                .HasComment("关联租户Id");

            builder.HasIndex(e => e.TenantId)
                .IsUnique()
                .HasDatabaseName("IX_JC_TenantConfigurations_TenantId");

            builder.Property(e => e.UnitCode)
                .HasMaxLength(50)
                .HasComment("单位编码");

            builder.HasIndex(e => e.UnitCode)
                .IsUnique()
                .HasDatabaseName("IX_JC_TenantConfigurations_UnitCode");

            builder.Property(e => e.IsIndependentDatabase)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("是否使用独立数据库");

            builder.Property(e => e.IndependentConnectionString)
                .HasComment("独立数据库连接字符串");

            builder.Property(e => e.MaxUserCount)
                .HasComment("最大用户数量");

            builder.Property(e => e.MaxStorageBytes)
                .HasComment("最大存储容量（字节）");

            builder.Property(e => e.MaxProjectCount)
                .HasComment("最大工程数量");

            builder.Property(e => e.MaxPointCount)
                .HasComment("最大测点数量");

            builder.Property(e => e.ExpireDate)
                .HasComment("到期日期");

            builder.Property(e => e.RemindDate)
                .HasComment("提醒日期");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(TenantStatus.Active)
                .HasComment("租户状态");

            builder.Property(e => e.LicenseKey)
                .HasMaxLength(500)
                .HasComment("许可证密钥");

            builder.Property(e => e.CertificateInfo)
                .HasComment("证书信息");
        }
    }
}
