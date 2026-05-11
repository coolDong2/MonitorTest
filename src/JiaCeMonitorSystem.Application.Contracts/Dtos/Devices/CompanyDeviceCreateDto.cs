using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Devices
{
    /// <summary>
    /// 创建设备输入参数
    /// </summary>
    public class CompanyDeviceCreateDto
    {
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [Required]
        [StringLength(100)]
        public string DeviceCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DeviceName { get; set; } = string.Empty;

        /// <summary>
        /// 设备类型
        /// </summary>
        [Required]
        public int DeviceType { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        [StringLength(200)]
        public string? Model { get; set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        [StringLength(200)]
        public string? Manufacturer { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [StringLength(200)]
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
        [StringLength(100)]
        public string? Accuracy { get; set; }

        /// <summary>
        /// 量程范围
        /// </summary>
        [StringLength(200)]
        public string? MeasurementRange { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        [StringLength(200)]
        public string? Location { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid? ResponsiblePersonId { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        [StringLength(100)]
        public string? ResponsiblePersonName { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [StringLength(200)]
        public string? ContactInfo { get; set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
