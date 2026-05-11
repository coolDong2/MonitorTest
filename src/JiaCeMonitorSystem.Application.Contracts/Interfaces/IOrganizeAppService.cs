using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Organizes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace JiaCeMonitorSystem.Interfaces
{
    /// <summary>
    /// 系统组织应用服务接口
    /// </summary>
    public interface IOrganizeAppService : IApplicationService
    {
        /// <summary>
        /// 获取组织树
        /// </summary>
        Task<List<OrganizeTreeDto>> GetOrganizeTreeAsync();

        /// <summary>
        /// 获取单个模型
        /// </summary>
        Task<OrganizeDto> GetModelAsync(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        Task<OrganizeDto> CreateAsync(OrganizeCreateDto input);

        /// <summary>
        /// 更新
        /// </summary>
        Task<OrganizeDto> UpdateAsync(Guid id, OrganizeUpdateDto input);

        /// <summary>
        /// 删除
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
