using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Notices
{
    /// <summary>
    /// 系统通知数据传输对象
    /// </summary>
    public class NoticeDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; set; }
    }
}
