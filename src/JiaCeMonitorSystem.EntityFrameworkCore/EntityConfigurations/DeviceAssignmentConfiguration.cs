using JiaCeMonitorSystem.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 设备分配实体配置
    /// </summary>
    public class DeviceAssignmentConfiguration : IEntityTypeConfiguration<DeviceAssignment>
    {
        public void Configure(EntityTypeBuilder<DeviceAssignment> builder)
        {
            builder.ToTable("JC_DeviceAssignments", "public");

            builder.Property(e => e.DeviceId)
                .IsRequired()
                .HasComment("设备ID");

            builder.Property(e => e.ProjectId)
                .IsRequired()
                .HasComment("项目ID");

            builder.Property(e => e.AssignmentDate)
                .IsRequired()
                .HasComment("分配日期");

            builder.Property(e => e.ExpectedReturnDate)
                .HasComment("预计归还日期");

            builder.Property(e => e.ActualReturnDate)
                .HasComment("实际归还日期");

            builder.Property(e => e.AssignerId)
                .HasComment("分配人ID");

            builder.Property(e => e.AssignerName)
                .HasMaxLength(128)
                .HasComment("分配人姓名");

            builder.Property(e => e.ReceiverId)
                .HasComment("领用人ID");

            builder.Property(e => e.ReceiverName)
                .HasMaxLength(128)
                .HasComment("领用人姓名");

            builder.Property(e => e.UsageDescription)
                .HasMaxLength(2000)
                .HasComment("用途说明");

            builder.Property(e => e.AssignmentStatus)
                .HasConversion<int>()
                .HasComment("分配状态：0=已分配 1=使用中 2=已延期 3=已归还 4=已损坏");

            builder.Property(e => e.Remark)
                .HasMaxLength(2000)
                .HasComment("备注");

            // 索引
            builder.HasIndex(e => new { e.DeviceId, e.AssignmentStatus })
                .HasDatabaseName("IX_DeviceAssignments_DeviceId_Status");

            builder.HasIndex(e => e.ProjectId)
                .HasDatabaseName("IX_DeviceAssignments_ProjectId");

            builder.HasIndex(e => e.AssignmentStatus)
                .HasDatabaseName("IX_DeviceAssignments_AssignmentStatus");
        }
    }
}
