using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.WarningRecords
{
    /// <summary>
    /// 确认预警记录输入参数
    /// </summary>
    public class ConfirmWarningInput
    {
        /// <summary>
        /// 预警记录ID
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 确认备注
        /// </summary>
        [StringLength(500)]
        public string? ConfirmRemark { get; set; }
    }
}
