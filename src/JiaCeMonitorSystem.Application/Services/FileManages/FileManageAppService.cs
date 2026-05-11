using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.FileManages;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.FileManages;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.FileManages
{
    /// <summary>
    /// 文件管理应用服务
    /// </summary>
    [Authorize]
    public class FileManageAppService :
        CrudAppService<UploadFile, UploadFileDto, Guid, GetUploadFileListInput, UploadFileCreateDto, UploadFileUpdateDto>,
        IFileManageAppService
    {
        public FileManageAppService(IRepository<UploadFile, Guid> repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        public async Task<PagedResultDto<UploadFileDto>> GetPageListAsync(GetUploadFileListInput input)
        {
            var query = await BuildFileQueryAsync(input);
            var totalCount = await AsyncExecuter.CountAsync(query);
            var files = await AsyncExecuter.ToListAsync(query.OrderByDescending(f => f.CreationTime).PageBy(input));
            var fileDtos = ObjectMapper.Map<List<UploadFile>, List<UploadFileDto>>(files);
            return new PagedResultDto<UploadFileDto>(totalCount, fileDtos);
        }

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        public new async Task<List<UploadFileDto>> GetListAsync(GetUploadFileListInput input)
        {
            var query = await BuildFileQueryAsync(input);
            var files = await AsyncExecuter.ToListAsync(query.OrderByDescending(f => f.CreationTime));
            return ObjectMapper.Map<List<UploadFile>, List<UploadFileDto>>(files);
        }

        private async Task<IQueryable<UploadFile>> BuildFileQueryAsync(GetUploadFileListInput input)
        {
            var query = await Repository.GetQueryableAsync();

            if (input.FileType.HasValue)
                query = query.Where(f => (int)f.FileType == input.FileType.Value);

            if (!string.IsNullOrWhiteSpace(input.FileName))
                query = query.Where(f => f.FileName.Contains(input.FileName));

            if (!string.IsNullOrWhiteSpace(input.Keyword))
                query = query.Where(f => f.FileName.Contains(input.Keyword));

            if (!string.IsNullOrWhiteSpace(input.FileBy))
                query = query.Where(f => f.FileBy == input.FileBy);

            if (input.OrganizeId.HasValue)
                query = query.Where(f => f.OrganizeId == input.OrganizeId.Value);

            if (input.EnabledMark.HasValue)
                query = query.Where(f => f.EnabledMark == input.EnabledMark.Value);

            return query;
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<UploadFileDto> GetModelAsync(Guid id)
        {
            var file = await Repository.GetAsync(id);
            return ObjectMapper.Map<UploadFile, UploadFileDto>(file);
        }

        /// <summary>
        /// 文件上传
        /// 保存到 wwwroot/uploads/{tenantId}/{date}/{hash}{ext}
        /// </summary>
        [Authorize(Permissions.Permissions.FileManages_Create)]
        public async Task<UploadFileDto> UploadAsync(string fileName, Stream fileStream, string? description = null, Guid? organizeId = null)
        {
            // 计算MD5 Hash
            string hash;
            using (var md5 = MD5.Create())
            {
                var hashBytes = await md5.ComputeHashAsync(fileStream);
                hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }

            // 获取文件扩展名
            var extension = Path.GetExtension(fileName);
            var fileType = IsImage(extension) ? FileType.Image : FileType.File;
            var fileSize = fileStream.Length;

            // 构建存储路径
            var tenantId = CurrentTenant.Id?.ToString() ?? "host";
            var dateFolder = DateTime.Now.ToString("yyyyMM");
            var relativePath = $"uploads/{tenantId}/{dateFolder}/{hash}{extension}";
            var absolutePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", relativePath);

            // 确保目录存在
            var directory = Path.GetDirectoryName(absolutePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // 保存文件
            fileStream.Position = 0;
            await using (var fileOutput = File.Create(absolutePath))
            {
                await fileStream.CopyToAsync(fileOutput);
            }

            // 创建文件记录
            var uploadFile = new UploadFile(
                GuidGenerator.Create(),
                hash,
                relativePath,
                fileName,
                fileType,
                fileSize,
                extension,
                CurrentUser.UserName,
                description,
                organizeId);

            await Repository.InsertAsync(uploadFile);
            return ObjectMapper.Map<UploadFile, UploadFileDto>(uploadFile);
        }

        /// <summary>
        /// 文件下载（返回元数据）
        /// </summary>
        public async Task<UploadFileDto> DownloadAsync(Guid id)
        {
            var file = await Repository.GetAsync(id);
            return ObjectMapper.Map<UploadFile, UploadFileDto>(file);
        }

        /// <summary>
        /// 文件下载（返回文件内容字节流）
        /// </summary>
        public async Task<(byte[] Content, string FileName, string ContentType)> DownloadFileAsync(Guid id)
        {
            var file = await Repository.GetAsync(id);
            var absolutePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", file.FilePath);

            if (!File.Exists(absolutePath))
            {
                throw new BusinessException(ErrorCodes.File_NotFound)
                    .WithData("Reason", "文件不存在或已被删除");
            }

            var content = await File.ReadAllBytesAsync(absolutePath);
            var contentType = GetContentType(file.FileExtension);
            return (content, file.FileName, contentType);
        }

        private static string GetContentType(string? extension)
        {
            return extension?.ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                ".svg" => "image/svg+xml",
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".txt" => "text/plain",
                ".csv" => "text/csv",
                ".json" => "application/json",
                ".xml" => "application/xml",
                ".zip" => "application/zip",
                ".rar" => "application/x-rar-compressed",
                ".7z" => "application/x-7z-compressed",
                _ => "application/octet-stream"
            };
        }

        /// <summary>
        /// 判断是否为图片
        /// </summary>
        private bool IsImage(string extension)
        {
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg" };
            return imageExtensions.Contains(extension?.ToLowerInvariant());
        }
    }
}
