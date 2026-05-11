using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.FileManages
{
    /// <summary>
    /// 文件管理数据传输对象
    /// </summary>
    public class UploadFileDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 文件Hash（MD5）
        /// </summary>
        public string Hash { get; set; } = string.Empty;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件类型
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 文件类型文本
        /// </summary>
        public string FileTypeText { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 文件大小显示文本（如"1.5 MB"）
        /// </summary>
        public string FileSizeDisplay { get; set; } = string.Empty;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; set; } = string.Empty;

        /// <summary>
        /// 上传人
        /// </summary>
        public string? FileBy { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 所属组织ID
        /// </summary>
        public Guid? OrganizeId { get; set; }

        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string? OrganizeName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; }
    }
}
