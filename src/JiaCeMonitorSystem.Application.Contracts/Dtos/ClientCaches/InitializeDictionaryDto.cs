using System.Collections.Generic;

namespace JiaCeMonitorSystem.Dtos.ClientCaches
{
    /// <summary>
    /// 初始化字典数据传输对象
    /// </summary>
    public class InitializeDictionaryDto
    {
        /// <summary>
        /// 字段信息（系统字典键值对）
        /// </summary>
        public Dictionary<string, List<DictionaryItemDto>> KeyValuePairs { get; set; } = new();

        /// <summary>
        /// 菜单按钮
        /// </summary>
        public Dictionary<string, object> ModuleButtons { get; set; } = new();

        /// <summary>
        /// 模块字段
        /// </summary>
        public Dictionary<string, object> ModelFields { get; set; } = new();

        /// <summary>
        /// 菜单字段
        /// </summary>
        public Dictionary<string, object> MenuFields { get; set; } = new();
    }

    /// <summary>
    /// 字典项DTO
    /// </summary>
    public class DictionaryItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
