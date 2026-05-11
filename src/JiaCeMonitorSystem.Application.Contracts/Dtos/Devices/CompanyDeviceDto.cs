using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Devices
{
    /// <summary>
    /// 单位设备数据传输对象
    /// </summary>
    public class CompanyDeviceDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; } = string.Empty;

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// 设备类型文本
        /// </summary>
        public string DeviceTypeText { get; set; } = string.Empty;

        /// <summary>
        /// 设备型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNumber { get; set; }

        /// <summary>
        /// 购置日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime? UseDate { get; set; }

        /// <summary>
        /// 设备精度
        /// </summary>
        public string? Accuracy { get; set; }

        /// <summary>
        /// 量程范围
        /// </summary>
        public string? MeasurementRange { get; set; }

        /// <summary>
        /// 最近校准日期
        /// </summary>
        public DateTime? CalibrationDate { get; set; }

        /// <summary>
        /// 下次校准日期
        /// </summary>
        public DateTime? NextCalibrationDate { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public int DeviceStatus { get; set; }

        /// <summary>
        /// 设备状态文本
        /// </summary>
        public string DeviceStatusText { get; set; } = string.Empty;

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string? ResponsiblePersonName { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string? ContactInfo { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string? Description { get; set; }
    }
}
