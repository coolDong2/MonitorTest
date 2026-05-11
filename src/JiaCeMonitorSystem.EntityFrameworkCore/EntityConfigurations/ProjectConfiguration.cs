using System.Text.Json;
using JiaCeMonitorSystem.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 监测工程实体配置
    /// </summary>
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("JC_Projects", "public");

            builder.Property(e => e.ProjectCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("项目编号");

            builder.Property(e => e.ProjectName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("项目名称");

            builder.Property(e => e.ProjectLocation)
                .HasMaxLength(512)
                .HasComment("项目地点");

            builder.Property(e => e.StartDate)
                .HasComment("项目开始日期");

            builder.Property(e => e.EndDate)
                .HasComment("项目结束日期");

            builder.Property(e => e.ResponsiblePerson)
                .HasMaxLength(128)
                .HasComment("项目负责人");

            builder.Property(e => e.ContactInfo)
                .HasMaxLength(256)
                .HasComment("负责人联系方式");

            builder.Property(e => e.Status)
                .HasConversion<int>()
                .HasComment("项目状态：0=筹备中 1=进行中 2=已完成 3=已暂停 4=已归档");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("项目描述");

            // 业务唯一索引
            builder.HasIndex(e => e.ProjectCode)
                .IsUnique()
                .HasDatabaseName("IX_Projects_ProjectCode");

            // 状态索引（常用于筛选）
            builder.HasIndex(e => e.Status)
                .HasDatabaseName("IX_Projects_Status");

            // 一对多关系：Project → Points
            builder.HasMany(e => e.Points)
                .WithOne()
                .HasForeignKey("ProjectId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
