using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.FileManages;
using JiaCeMonitorSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Controllers.System
{
    /// <summary>
    /// 文件管理控制器
    /// </summary>
    [Route("api/app/file-manage")]
    public class FileManageController : JiaCeMonitorSystemController
    {
        private readonly IFileManageAppService _fileManageAppService;

        /// <summary>
        /// 初始化文件管理控制器
        /// </summary>
        public FileManageController(IFileManageAppService fileManageAppService)
        {
            _fileManageAppService = fileManageAppService;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        [HttpGet("page-list")]
        public virtual Task<PagedResultDto<UploadFileDto>> GetPageListAsync([FromQuery] GetUploadFileListInput input)
        {
            return _fileManageAppService.GetPageListAsync(input);
        }

        /// <summary>
        /// 获取列表（不分页）
        /// </summary>
        [HttpGet("list")]
        public virtual Task<List<UploadFileDto>> GetListAsync([FromQuery] GetUploadFileListInput input)
        {
            return _fileManageAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        [HttpGet("{id}")]
        public virtual Task<UploadFileDto> GetModelAsync(Guid id)
        {
            return _fileManageAppService.GetModelAsync(id);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        [HttpPost("upload")]
        [Authorize(Permissions.Permissions.FileManages_Create)]
        public virtual async Task<UploadFileDto> UploadAsync(IFormFile file, [FromQuery] string? fileBy, [FromQuery] string? description, [FromQuery] Guid? organizeId)
        {
            return await _fileManageAppService.UploadAsync(file.FileName, file.OpenReadStream(), description, organizeId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _fileManageAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        [HttpGet("download/{id}")]
        public virtual async Task<IActionResult> DownloadFileAsync(Guid id)
        {
            var (content, fileName, contentType) = await _fileManageAppService.DownloadFileAsync(id);
            return File(content, contentType, fileName);
        }
    }
}
