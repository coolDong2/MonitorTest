using System;
using System.Linq.Expressions;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.WarningRecords;
using Volo.Abp.Specifications;

namespace JiaCeMonitorSystem.Specifications
{
    /// <summary>
    /// 未处理预警规约，筛选未处理或处理中的预警记录
    /// </summary>
    public class UnhandledWarningSpecification : Specification<WarningRecord>
    {
        /// <inheritdoc />
        public override Expression<Func<WarningRecord, bool>> ToExpression()
        {
            return w => (w.HandleStatus == HandleStatus.Unhandled || w.HandleStatus == HandleStatus.InProgress)
                     && !w.IsDeleted;
        }
    }
}
