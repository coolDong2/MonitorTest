using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.SystemModules;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace JiaCeMonitorSystem.DomainServices
{
    /// <summary>
    /// 系统菜单模块领域服务
    /// 校验树形结构层级与删除约束
    /// </summary>
    public class SystemModuleManager : DomainService
    {
        private readonly IRepository<SystemModule, Guid> _systemModuleRepository;

        public SystemModuleManager(
            IRepository<SystemModule, Guid> systemModuleRepository)
        {
            _systemModuleRepository = systemModuleRepository;
        }

        /// <summary>
        /// 校验是否可以删除（无子节点）
        /// </summary>
        public async Task ValidateCanDeleteAsync(Guid moduleId)
        {
            var hasChildren = await _systemModuleRepository.AnyAsync(
                x => x.ParentId == moduleId);

            if (hasChildren)
            {
                throw new BusinessException(ErrorCodes.SystemModule_HasChildrenCannotDelete)
                    .WithData("ModuleId", moduleId);
            }
        }

        /// <summary>
        /// 计算层级
        /// </summary>
        public async Task<int> CalculateLayersAsync(Guid? parentId)
        {
            if (!parentId.HasValue)
                return 1;

            var parent = await _systemModuleRepository.FindAsync(parentId.Value);
            return parent == null ? 1 : parent.Layers + 1;
        }
    }
}
