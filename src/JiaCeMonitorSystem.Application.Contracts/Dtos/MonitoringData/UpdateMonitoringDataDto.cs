using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace JiaCeMonitorSystem.Dtos.MonitoringData
{
    /// <summary>
    /// 更新监测数据输入参数
    /// </summary>
    public class UpdateMonitoringDataDto
    {
        /// <summary>
        /// 监测数值
        /// </summary>
        [Required]
        public decimal MonitoringValue { get; set; }

        /// <summary>
        /// 数据质量
        /// </summary>
        public int DataQuality { get; set; }

        /// <summary>
        /// 监测属性ID
        /// </summary>
        public Guid? PropertyId { get; set; }

        /// <summary>
        /// 属性编码
        /// </summary>
        [StringLength(100)]
        public string? PropertyCode { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [StringLength(100)]
        public string? PropertyName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(50)]
        public string? Unit { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public JsonDocument? ExtendedData { get; set; }

        /// <summary>
        /// 数据备注
        /// </summary>
        [StringLength(500)]
        public string? DataRemark { get; set; }
    }
}
