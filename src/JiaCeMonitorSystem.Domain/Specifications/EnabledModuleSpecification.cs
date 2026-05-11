using System;
using System.Linq.Expressions;
using JiaCeMonitorSystem.SystemModules;
using Volo.Abp.Specifications;

namespace JiaCeMonitorSystem.Specifications
{
    /// <summary>
    /// 筛选启用的菜单规约
    /// </summary>
    public class EnabledModuleSpecification : Specification<SystemModule>
    {
        public override Expression<Func<SystemModule, bool>> ToExpression()
        {
            return m => m.EnabledMark;
        }
    }
}
