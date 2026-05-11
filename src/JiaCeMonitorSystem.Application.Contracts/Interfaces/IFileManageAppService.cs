using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.FileManages;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 文件管理应用服务接口
    /// </summary>
    public interface IFileManageAppService : IApplicationService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        Task<PagedResultDto<UploadFileDto>> GetPageListAsync(GetUploadFileListInput input);

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        Task<List<UploadFileDto>> GetListAsync(GetUploadFileListInput input);

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<UploadFileDto> GetModelAsync(Guid id);

        /// <summary>
        /// 文件上传
        /// </summary>
        Task<UploadFileDto> UploadAsync(string fileName, Stream fileStream, string? description = null, Guid? organizeId = null);

        /// <summary>
        /// 文件下载（返回元数据）
        /// </summary>
        Task<UploadFileDto> DownloadAsync(Guid id);

        /// <summary>
        /// 文件下载（返回文件内容字节流）
        /// </summary>
        Task<(byte[] Content, string FileName, string ContentType)> DownloadFileAsync(Guid id);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
