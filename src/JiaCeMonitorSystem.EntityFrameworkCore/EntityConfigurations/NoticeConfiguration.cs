using JiaCeMonitorSystem.Notices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 系统通知实体配置
    /// </summary>
    public class NoticeConfiguration : IEntityTypeConfiguration<Notice>
    {
        public void Configure(EntityTypeBuilder<Notice> builder)
        {
            builder.ToTable("JC_Notices", "public");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("标题");

            builder.Property(e => e.Content)
                .IsRequired()
                .HasComment("内容");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("描述");

            builder.Property(e => e.EnabledMark)
                .HasComment("是否启用");
        }
    }
}
