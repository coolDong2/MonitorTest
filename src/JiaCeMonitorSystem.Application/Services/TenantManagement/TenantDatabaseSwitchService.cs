using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.TenantManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Security.Encryption;
using Volo.Abp.Uow;

using TenantConfigurationEntity = JiaCeMonitorSystem.TenantManagement.TenantConfiguration;

namespace JiaCeMonitorSystem.Services.TenantManagement
{
    /// <summary>
    /// 租户数据库切换服务，负责将租户从共享数据库迁移到独立数据库
    /// </summary>
    public class TenantDatabaseSwitchService : ApplicationService
    {
        private readonly IRepository<TenantConfigurationEntity, Guid> _configRepo;
        private readonly ITenantDatabaseManager _tenantDatabaseManager;
        private readonly IStringEncryptionService _encryptionService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ITenantDataExporter _dataExporter;
        private readonly ITenantDataValidator _dataValidator;
        private readonly ITenantDataCleaner _dataCleaner;
        private readonly IConfiguration _configuration;

        public TenantDatabaseSwitchService(
            IRepository<TenantConfigurationEntity, Guid> configRepo,
            ITenantDatabaseManager tenantDatabaseManager,
            IStringEncryptionService encryptionService,
            IUnitOfWorkManager unitOfWorkManager,
            ITenantDataExporter dataExporter,
            ITenantDataValidator dataValidator,
            ITenantDataCleaner dataCleaner,
            IConfiguration configuration)
        {
            _configRepo = configRepo;
            _tenantDatabaseManager = tenantDatabaseManager;
            _encryptionService = encryptionService;
            _unitOfWorkManager = unitOfWorkManager;
            _dataExporter = dataExporter;
            _dataValidator = dataValidator;
            _dataCleaner = dataCleaner;
            _configuration = configuration;
        }

        /// <summary>
        /// 切换租户到独立数据库
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="tenantName">租户名称</param>
        /// <returns>新数据库的连接字符串</returns>
        [UnitOfWork]
        public virtual async Task<string> SwitchToIndependentDatabaseAsync(Guid tenantId, string tenantName)
        {
            var config = await _configRepo.FirstOrDefaultAsync(x => x.TenantId == tenantId);
            if (config == null)
                throw new BusinessException("TENANT_CONFIG_NOT_FOUND", $"租户 {tenantId} 的配置不存在");

            if (config.IsIndependentDatabase)
                throw new BusinessException("ALREADY_INDEPENDENT_DB", $"租户 {tenantName} 已经是独立数据库模式");

            // 1. 创建独立数据库（含迁移和数据种子）
            var connectionString = await _tenantDatabaseManager.CreateDatabaseAsync(tenantId, tenantName);

            var sourceConnectionString = _configuration.GetConnectionString("Default")
                ?? throw new InvalidOperationException("Default connection string is not configured.");

            try
            {
                // 2. 数据迁移（共享库 → 独立库）
                var exportResult = await _dataExporter.ExportAsync(tenantId, sourceConnectionString, connectionString);
                if (!exportResult.Succeeded)
                {
                    throw new BusinessException("DATA_EXPORT_FAILED", $"数据迁移失败: {exportResult.ErrorMessage}");
                }

                // 3. 数据一致性验证
                var validationResult = await _dataValidator.ValidateAsync(tenantId, sourceConnectionString, connectionString);
                if (!validationResult.IsValid)
                {
                    throw new BusinessException("DATA_VALIDATION_FAILED", $"数据验证失败: {validationResult.ErrorMessage}");
                }

                // 4. 更新租户配置为独立数据库模式
                config.IsIndependentDatabase = true;
                config.IndependentConnectionString = _encryptionService.Encrypt(connectionString);
                await _configRepo.UpdateAsync(config);

                await _unitOfWorkManager.Current.SaveChangesAsync();

                Logger.LogInformation("租户 {TenantName} 已成功切换到独立数据库", tenantName);
                return connectionString;
            }
            catch (Exception)
            {
                // 回滚：删除已创建的独立数据库
                try
                {
                    await _dataCleaner.CleanAsync(connectionString);
                }
                catch (Exception cleanEx)
                {
                    Logger.LogError(cleanEx, "回滚时删除独立数据库失败");
                }
                throw;
            }
        }
    }
}
