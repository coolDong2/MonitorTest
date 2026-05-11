using JiaCeMonitorSystem.Devices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JiaCeMonitorSystem.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// 单位设备实体配置
    /// </summary>
    public class CompanyDeviceConfiguration : IEntityTypeConfiguration<CompanyDevice>
    {
        public void Configure(EntityTypeBuilder<CompanyDevice> builder)
        {
            builder.ToTable("JC_CompanyDevices", "public");

            builder.Property(e => e.CompanyId)
                .HasComment("所属单位ID");

            builder.Property(e => e.DeviceCode)
                .IsRequired()
                .HasMaxLength(64)
                .HasComment("设备编号");

            builder.Property(e => e.DeviceName)
                .IsRequired()
                .HasMaxLength(256)
                .HasComment("设备名称");

            builder.Property(e => e.DeviceType)
                .HasConversion<int>()
                .HasComment("设备类型：0=全站仪 1=水准仪 2=GNSS接收机 3=测斜仪 4=应变计 5=土压力计 6=水位计 7=渗压计 8=裂缝计 9=其他");

            builder.Property(e => e.Model)
                .HasMaxLength(128)
                .HasComment("设备型号");

            builder.Property(e => e.Manufacturer)
                .HasMaxLength(256)
                .HasComment("生产厂家");

            builder.Property(e => e.SerialNumber)
                .HasMaxLength(128)
                .HasComment("序列号");

            builder.Property(e => e.PurchaseDate)
                .HasComment("购置日期");

            builder.Property(e => e.UseDate)
                .HasComment("启用日期");

            builder.Property(e => e.Accuracy)
                .HasMaxLength(64)
                .HasComment("设备精度");

            builder.Property(e => e.MeasurementRange)
                .HasMaxLength(128)
                .HasComment("量程范围");

            builder.Property(e => e.CalibrationDate)
                .HasComment("最近校准日期");

            builder.Property(e => e.NextCalibrationDate)
                .HasComment("下次校准日期");

            builder.Property(e => e.DeviceStatus)
                .HasConversion<int>()
                .HasComment("设备状态：0=正常 1=维修中 2=已停用 3=已报废 4=已借出");

            builder.Property(e => e.Location)
                .HasMaxLength(512)
                .HasComment("存放位置");

            builder.Property(e => e.ResponsiblePersonId)
                .HasComment("负责人ID");

            builder.Property(e => e.ResponsiblePersonName)
                .HasMaxLength(128)
                .HasComment("负责人姓名");

            builder.Property(e => e.ContactInfo)
                .HasMaxLength(256)
                .HasComment("联系方式");

            builder.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasComment("设备描述");

            // 索引
            builder.HasIndex(e => e.DeviceCode)
                .IsUnique()
                .HasDatabaseName("IX_CompanyDevices_DeviceCode");

            builder.HasIndex(e => e.DeviceStatus)
                .HasDatabaseName("IX_CompanyDevices_DeviceStatus");

            builder.HasIndex(e => e.DeviceType)
                .HasDatabaseName("IX_CompanyDevices_DeviceType");

            builder.HasIndex(e => e.NextCalibrationDate)
                .HasDatabaseName("IX_CompanyDevices_NextCalibrationDate");
        }
    }
}
