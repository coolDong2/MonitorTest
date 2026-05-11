using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace JiaCeMonitorSystem.Data
{
    /// <summary>
    /// 空实现的数据库架构迁移器
    /// </summary>
    public class NullJiaCeMonitorSystemDbSchemaMigrator : IJiaCeMonitorSystemDbSchemaMigrator, ITransientDependency
    {
        /// <inheritdoc />
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
