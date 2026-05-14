using System.Collections.Generic;

namespace JiaCeMonitorSystem.TenantManagement
{
    /// <summary>
    /// 租户数据验证结果
    /// </summary>
    public class TenantDataValidationResult
    {
        /// <summary>是否通过验证</summary>
        public bool IsValid { get; set; }

        /// <summary>验证详情</summary>
        public List<TenantDataValidationItem> Items { get; set; } = new();

        /// <summary>错误信息</summary>
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// 单表数据验证项
    /// </summary>
    public class TenantDataValidationItem
    {
        /// <summary>表名</summary>
        public string TableName { get; set; } = string.Empty;

        /// <summary>源库行数</summary>
        public int SourceCount { get; set; }

        /// <summary>目标库行数</summary>
        public int TargetCount { get; set; }

        /// <summary>是否匹配</summary>
        public bool IsMatched { get; set; }
    }
}
