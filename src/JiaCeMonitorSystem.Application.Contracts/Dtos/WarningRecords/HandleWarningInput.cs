using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.WarningRecords
{
    /// <summary>
    /// 处理预警记录输入参数
    /// </summary>
    public class HandleWarningInput
    {
        /// <summary>
        /// 预警记录ID
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 处理负责人ID
        /// </summary>
        [Required]
        public Guid HandlerId { get; set; }

        /// <summary>
        /// 处理负责人姓名
        /// </summary>
        [Required]
        [StringLength(100)]
        public string HandlerName { get; set; } = string.Empty;

        /// <summary>
        /// 处理方案
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string HandleSolution { get; set; } = string.Empty;

        /// <summary>
        /// 处理结果
        /// </summary>
        [StringLength(1000)]
        public string? HandleResult { get; set; }
    }
}
