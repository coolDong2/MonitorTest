using System;
using System.Linq.Expressions;
using JiaCeMonitorSystem.MonitoringItemTypes;
using Volo.Abp.Specifications;

namespace JiaCeMonitorSystem.Specifications
{
    /// <summary>
    /// 筛选启用的监测类型规约
    /// </summary>
    public class ValidMonitoringItemTypeSpecification : Specification<MonitoringItemType>
    {
        public override Expression<Func<MonitoringItemType, bool>> ToExpression()
        {
            return t => t.EnabledMark;
        }
    }
}
