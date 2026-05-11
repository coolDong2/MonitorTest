using System;

namespace JiaCeMonitorSystem.Dtos.ClientCaches
{
    /// <summary>
    /// 首页快捷菜单数据传输对象
    /// </summary>
    public class QuickModuleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string LinkAddress { get; set; } = string.Empty;
        public string? Icon { get; set; }
    }
}
