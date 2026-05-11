using System;
using System.ComponentModel.DataAnnotations;

namespace JiaCeMonitorSystem.Dtos.FileManages
{
    /// <summary>
    /// 创建文件记录输入参数
    /// </summary>
    public class UploadFileCreateDto
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件类型
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 文件归属
        /// </summary>
        public string? FileBy { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// 所属组织ID
        /// </summary>
        public Guid? OrganizeId { get; set; }
    }
}
