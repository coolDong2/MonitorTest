using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.Notices
{
    /// <summary>
    /// 系统通知聚合根
    /// 表名：JC_Notices
    /// </summary>
    public class Notice : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; private set; }

        private Notice()
        {
            Title = string.Empty;
            Content = string.Empty;
        }

        /// <summary>
        /// 创建系统通知
        /// </summary>
        public Notice(
            Guid id,
            string title,
            string content,
            bool enabledMark = true,
            string? description = null)
            : base(id)
        {
            Title = title;
            Content = content;
            EnabledMark = enabledMark;
            Description = description;
        }

        /// <summary>
        /// 更新通知信息
        /// </summary>
        public void UpdateInfo(
            string title,
            string content,
            bool enabledMark,
            string? description = null)
        {
            Title = title;
            Content = content;
            EnabledMark = enabledMark;
            Description = description;
        }
    }
}
