using System;
using System.Linq.Expressions;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.ProjectPersonnels;
using Volo.Abp.Specifications;

namespace JiaCeMonitorSystem.Specifications
{
    /// <summary>
    /// 筛选在职且未过期的项目人员规约
    /// </summary>
    public class ActiveProjectPersonnelSpecification : Specification<ProjectPersonnel>
    {
        public override Expression<Func<ProjectPersonnel, bool>> ToExpression()
        {
            return p => p.WorkStatus == WorkStatus.Active
                        && (!p.EndDate.HasValue || p.EndDate.Value >= DateTime.UtcNow);
        }
    }
}
