using System.Threading.Tasks;

namespace JiaCeMonitorSystem.Data
{
    /// <summary>
    /// 数据库架构迁移器接口
    /// </summary>
    public interface IJiaCeMonitorSystemDbSchemaMigrator
    {
        /// <summary>
        /// 执行数据库架构迁移
        /// </summary>
        Task MigrateAsync();
    }
}
