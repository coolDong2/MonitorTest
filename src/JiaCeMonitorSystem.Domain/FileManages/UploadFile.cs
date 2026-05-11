using System;
using JiaCeMonitorSystem.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.FileManages
{
    /// <summary>
    /// 文件管理聚合根（仅保存元数据）
    /// 表名：JC_UploadFiles
    /// 文件流存储到 wwwroot/uploads/{tenantId}/{date}/{hash}{ext}
    /// </summary>
    public class UploadFile : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 文件Hash（MD5）
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; private set; }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension { get; private set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string? FileBy { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// 所属组织ID
        /// </summary>
        public Guid? OrganizeId { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; private set; }

        private UploadFile()
        {
            Hash = string.Empty;
            FilePath = string.Empty;
            FileName = string.Empty;
            FileExtension = string.Empty;
        }

        /// <summary>
        /// 创建文件记录
        /// </summary>
        public UploadFile(
            Guid id,
            string hash,
            string filePath,
            string fileName,
            FileType fileType,
            long fileSize,
            string fileExtension,
            string? fileBy = null,
            string? description = null,
            Guid? organizeId = null,
            bool enabledMark = true)
            : base(id)
        {
            Hash = hash;
            FilePath = filePath;
            FileName = fileName;
            FileType = fileType;
            FileSize = fileSize;
            FileExtension = fileExtension;
            FileBy = fileBy;
            Description = description;
            OrganizeId = organizeId;
            EnabledMark = enabledMark;
        }

        /// <summary>
        /// 更新文件信息
        /// </summary>
        public void UpdateInfo(
            string fileName,
            string? description = null,
            bool enabledMark = true)
        {
            FileName = fileName;
            Description = description;
            EnabledMark = enabledMark;
        }
    }
}
