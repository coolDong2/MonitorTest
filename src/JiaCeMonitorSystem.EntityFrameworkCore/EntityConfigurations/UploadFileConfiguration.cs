using JiaCeMonitorSystem.FileManages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 文件管理实体配置
    /// </summary>
    public class UploadFileConfiguration : IEntityTypeConfiguration<UploadFile>
    {
        public void Configure(EntityTypeBuilder<UploadFile> builder)
        {
            builder.ToTable("JC_UploadFiles", "public");

            builder.Property(e => e.Hash)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("文件Hash（MD5）");

            builder.Property(e => e.FilePath)
                .IsRequired()
                .HasMaxLength(512)
                .HasComment("文件路径");

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("文件名称");

            builder.Property(e => e.FileType)
                .HasConversion<int>()
                .HasComment("文件类型：0=文件 1=图片");

            builder.Property(e => e.FileSize)
                .HasComment("文件大小（字节）");

            builder.Property(e => e.FileExtension)
                .IsRequired()
                .HasMaxLength(32)
                .HasComment("文件扩展名");

            builder.Property(e => e.FileBy)
                .HasMaxLength(128)
                .HasComment("上传人");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("描述");

            builder.Property(e => e.OrganizeId)
                .HasComment("所属组织ID");

            builder.Property(e => e.EnabledMark)
                .HasComment("是否启用");

            // 索引
            builder.HasIndex(e => e.OrganizeId)
                .HasDatabaseName("IX_UploadFiles_OrganizeId");

            builder.HasIndex(e => e.Hash)
                .HasDatabaseName("IX_UploadFiles_Hash");
        }
    }
}
