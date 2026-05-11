using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Dtos.Organizes;
using JiaCeMonitorSystem.Interfaces;
using JiaCeMonitorSystem.Organizes;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace JiaCeMonitorSystem.Services.Organizes
{
    /// <summary>
    /// 系统组织应用服务
    /// </summary>
    [Authorize]
    public class OrganizeAppService :
        CrudAppService<Organize, OrganizeDto, Guid, GetOrganizeListInput, OrganizeCreateDto, OrganizeUpdateDto>,
        IOrganizeAppService
    {
        public OrganizeAppService(IRepository<Organize, Guid> repository) : base(repository)
        {
        }

        /// <summary>
        /// 获取组织树
        /// </summary>
        public async Task<List<OrganizeTreeDto>> GetOrganizeTreeAsync()
        {
            var organizes = await Repository.GetListAsync();
            var organizeDtos = ObjectMapper.Map<List<Organize>, List<OrganizeTreeDto>>(organizes);
            return BuildTree(organizeDtos);
        }

        /// <summary>
        /// 获取单个模型
        /// </summary>
        public async Task<OrganizeDto> GetModelAsync(Guid id)
        {
            var organize = await Repository.GetAsync(id);
            return ObjectMapper.Map<Organize, OrganizeDto>(organize);
        }

        /// <summary>
        /// 创建组织
        /// </summary>
        [Authorize(Permissions.Permissions.Organizes_Create)]
        public override async Task<OrganizeDto> CreateAsync(OrganizeCreateDto input)
        {
            var organize = ObjectMapper.Map<OrganizeCreateDto, Organize>(input);
            var layers = input.ParentId.HasValue ? 2 : 1;
            organize.SetLayers(layers);
            await Repository.InsertAsync(organize);
            return ObjectMapper.Map<Organize, OrganizeDto>(organize);
        }

        /// <summary>
        /// 构建树形结构
        /// </summary>
        private List<OrganizeTreeDto> BuildTree(List<OrganizeTreeDto> organizes)
        {
            var lookup = organizes.ToLookup(o => o.ParentId);
            var rootOrganizes = organizes.Where(o => o.ParentId == null || o.ParentId == Guid.Empty).ToList();

            foreach (var organize in rootOrganizes)
            {
                BuildChildren(organize, lookup);
            }

            return rootOrganizes.OrderBy(o => o.SortCode).ToList();
        }

        private void BuildChildren(OrganizeTreeDto parent, ILookup<Guid?, OrganizeTreeDto> lookup)
        {
            parent.Children = lookup[parent.Id].OrderBy(o => o.SortCode).ToList();
            foreach (var child in parent.Children)
            {
                BuildChildren(child, lookup);
            }
        }
    }
}
