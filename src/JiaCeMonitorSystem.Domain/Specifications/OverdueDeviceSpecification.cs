using System;
using System.Linq.Expressions;
using JiaCeMonitorSystem.Devices;
using Volo.Abp.Specifications;

namespace JiaCeMonitorSystem.Specifications
{
    /// <summary>
    /// 超期未校准设备规约，筛选下次校准日期已过期或即将过期（7天内）的设备
    /// </summary>
    public class OverdueDeviceSpecification : Specification<CompanyDevice>
    {
        private readonly DateTime _referenceDate;

        /// <summary>
        /// 初始化超期设备规约
        /// </summary>
        public OverdueDeviceSpecification(DateTime? referenceDate = null)
        {
            _referenceDate = referenceDate ?? DateTime.UtcNow;
        }

        /// <inheritdoc />
        public override Expression<Func<CompanyDevice, bool>> ToExpression()
        {
            return d => d.NextCalibrationDate.HasValue
                     && d.NextCalibrationDate.Value <= _referenceDate.AddDays(7)
                     && !d.IsDeleted;
        }
    }
}
