using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.SystemDictionaries
{
    /// <summary>
    /// 系统字典类型聚合根
    /// 表名：JC_SystemDictionaryTypes
    /// </summary>
    public class SystemDictionaryType : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid? ParentId { get; private set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// 是否树形
        /// </summary>
        public bool IsTree { get; private set; }

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

        private SystemDictionaryType()
        {
            EnCode = string.Empty;
            FullName = string.Empty;
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        public SystemDictionaryType(
            Guid id,
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark = true,
            bool isTree = false,
            Guid? parentId = null,
            int layers = 1,
            string? description = null)
            : base(id)
        {
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            IsTree = isTree;
            ParentId = parentId;
            Layers = layers;
            Description = description;
        }

        /// <summary>
        /// 更新字典类型信息
        /// </summary>
        public void UpdateInfo(
            string enCode,
            string fullName,
            int sortCode,
            bool enabledMark,
            bool isTree = false,
            string? description = null)
        {
            EnCode = enCode;
            FullName = fullName;
            SortCode = sortCode;
            EnabledMark = enabledMark;
            IsTree = isTree;
            Description = description;
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        public void SetLayers(int layers)
        {
            Layers = layers;
        }
    }
}
