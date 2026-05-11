using System;
using System.Linq.Expressions;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.Projects;
using Volo.Abp.Specifications;

namespace JiaCeMonitorSystem.Specifications
{
    /// <summary>
    /// 活跃项目规约，筛选进行中的项目
    /// </summary>
    public class ActiveProjectSpecification : Specification<Project>
    {
        /// <inheritdoc />
        public override Expression<Func<Project, bool>> ToExpression()
        {
            return p => p.Status == ProjectStatus.InProgress && !p.IsDeleted;
        }
    }
}
