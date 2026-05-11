using JiaCeMonitorSystem.ProjectPersonnels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 项目人员安排实体配置
    /// </summary>
    public class ProjectPersonnelConfiguration : IEntityTypeConfiguration<ProjectPersonnel>
    {
        public void Configure(EntityTypeBuilder<ProjectPersonnel> builder)
        {
            builder.ToTable("JC_ProjectPersonnels", "public");

            builder.Property(e => e.ProjectId)
                .IsRequired()
                .HasComment("项目ID");

            builder.Property(e => e.UserId)
                .IsRequired()
                .HasComment("用户ID（关联IdentityUser）");

            builder.Property(e => e.RoleType)
                .HasConversion<int>()
                .HasComment("角色类型：0=项目经理 1=技术负责人 2=监测员 3=数据分析员 4=安全员 5=设备管理员");

            builder.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(128)
                .HasComment("角色名称");

            builder.Property(e => e.Responsibility)
                .HasMaxLength(2000)
                .HasComment("职责描述");

            builder.Property(e => e.StartDate)
                .IsRequired()
                .HasComment("开始日期");

            builder.Property(e => e.EndDate)
                .HasComment("结束日期");

            builder.Property(e => e.ContactInfo)
                .HasMaxLength(256)
                .HasComment("联系方式");

            builder.Property(e => e.WorkStatus)
                .HasConversion<int>()
                .HasComment("工作状态：0=在职 1=休假 2=调离 3=结束");

            builder.Property(e => e.Remark)
                .HasMaxLength(2000)
                .HasComment("备注");

            // 索引
            builder.HasIndex(e => new { e.ProjectId, e.UserId })
                .HasDatabaseName("IX_ProjectPersonnels_ProjectId_UserId");

            builder.HasIndex(e => e.ProjectId)
                .HasDatabaseName("IX_ProjectPersonnels_ProjectId");
        }
    }
}
