using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.Notices
{
    /// <summary>
    /// 更新系统通知输入参数
    /// </summary>
    public class NoticeUpdateDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; }
    }
}
