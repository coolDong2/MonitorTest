using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.FileManages
{
    /// <summary>
    /// 更新文件记录输入参数
    /// </summary>
    public class UploadFileUpdateDto
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FileName { get; set; } = string.Empty;

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
