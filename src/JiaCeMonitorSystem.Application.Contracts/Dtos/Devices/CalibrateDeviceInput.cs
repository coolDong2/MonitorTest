using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Devices
{
    /// <summary>
    /// 设备校准输入参数
    /// </summary>
    public class CalibrateDeviceInput
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [Required]
        public Guid DeviceId { get; set; }

        /// <summary>
        /// 校准日期
        /// </summary>
        [Required]
        public DateTime CalibrationDate { get; set; }

        /// <summary>
        /// 下次校准日期
        /// </summary>
        [Required]
        public DateTime NextCalibrationDate { get; set; }

        /// <summary>
        /// 校准后精度
        /// </summary>
        [StringLength(100)]
        public string? Accuracy { get; set; }
    }
}
