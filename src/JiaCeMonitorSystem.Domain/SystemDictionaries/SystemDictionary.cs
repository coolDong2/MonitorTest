using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.SystemDictionaries
{
    /// <summary>
    /// 系统字典聚合根
    /// 表名：JC_SystemDictionaries
    /// </summary>
    public class SystemDictionary : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 字典类型ID
        /// </summary>
        public Guid ItemId { get; private set; }

        /// <summary>
        /// 字典编码
        /// </summary>
        public string ItemCode { get; private set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string ItemName { get; private set; }

        /// <summary>
        /// 简拼
        /// </summary>
        public string? SimpleSpelling { get; private set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; private set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Layers { get; private set; }

        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool EnabledMark { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; private set; }

        private SystemDictionary()
        {
            ItemCode = string.Empty;
            ItemName = string.Empty;
        }

        /// <summary>
        /// 创建系统字典
        /// </summary>
        public SystemDictionary(
            Guid id,
            Guid itemId,
            string itemCode,
            string itemName,
            int sortCode,
            bool enabledMark = true,
            bool isDefault = false,
            int layers = 1,
            string? simpleSpelling = null,
            string? description = null)
            : base(id)
        {
            ItemId = itemId;
            ItemCode = itemCode;
            ItemName = itemName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            IsDefault = isDefault;
            Layers = layers;
            SimpleSpelling = simpleSpelling;
            Description = description;
        }

        /// <summary>
        /// 更新字典信息
        /// </summary>
        public void UpdateInfo(
            string itemCode,
            string itemName,
            int sortCode,
            bool enabledMark,
            bool isDefault = false,
            string? simpleSpelling = null,
            string? description = null)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            IsDefault = isDefault;
            SimpleSpelling = simpleSpelling;
            Description = description;
        }
    }
}
