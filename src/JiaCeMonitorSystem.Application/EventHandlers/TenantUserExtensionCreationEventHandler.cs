using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.TenantManagement;
using TenantConfigurationEntity = JiaCeMonitorSystem.TenantManagement.TenantConfiguration;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace JiaCeMonitorSystem.EventHandlers
{
    /// <summary>
    /// 当 IdentityUser 被创建时，自动创建对应的 TenantUserExtension 记录。
    /// 确保租户登录时能够正确获取 UnitCode 和 UserType 生成 JWT Claim。
    /// </summary>
    public class TenantUserExtensionCreationEventHandler :
        ILocalEventHandler<EntityCreatedEventData<IdentityUser>>,
        ITransientDependency
    {
        private readonly IRepository<TenantUserExtension, Guid> _userExtensionRepository;
        private readonly IRepository<TenantConfigurationEntity, Guid> _tenantConfigRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public TenantUserExtensionCreationEventHandler(
            IRepository<TenantUserExtension, Guid> userExtensionRepository,
            IRepository<TenantConfigurationEntity, Guid> tenantConfigRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _userExtensionRepository = userExtensionRepository;
            _tenantConfigRepository = tenantConfigRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<IdentityUser> eventData)
        {
            var user = eventData.Entity;

            // 幂等：若该用户已存在扩展记录，则跳过
            var existing = await _userExtensionRepository.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (existing != null)
            {
                return;
            }

            // 根据当前上下文判断是 Host 还是租户用户
            if (_currentTenant.Id.HasValue)
            {
                await CreateTenantUserExtensionAsync(user);
            }
            else
            {
                await CreateHostUserExtensionAsync(user);
            }
        }

        /// <summary>
        /// 创建租户用户的扩展记录
        /// </summary>
        private async Task CreateTenantUserExtensionAsync(IdentityUser user)
        {
            var tenantId = _currentTenant.Id!.Value;

            // 从租户配置中获取 UnitCode
            var config = await _tenantConfigRepository.FirstOrDefaultAsync(x => x.TenantId == tenantId);
            var unitCode = config?.UnitCode;

            var extension = new TenantUserExtension(
                _guidGenerator.Create(),
                user.Id,
                tenantId,
                UserType.TenantUser,
                unitCode
            );

            await _userExtensionRepository.InsertAsync(extension);
        }

        /// <summary>
        /// 创建 Host 环境用户的扩展记录
        /// </summary>
        private async Task CreateHostUserExtensionAsync(IdentityUser user)
        {
            // Host 环境默认使用 SystemAdmin 类型和 HOST 单位编码
            var userType = string.Equals(user.UserName, "admin", StringComparison.OrdinalIgnoreCase)
                ? UserType.SystemAdmin
                : UserType.TenantAdmin;

            var extension = new TenantUserExtension(
                _guidGenerator.Create(),
                user.Id,
                Guid.Empty,
                userType,
                "HOST"
            )
            {
                TenantId = null
            };

            await _userExtensionRepository.InsertAsync(extension);
        }
    }
}
