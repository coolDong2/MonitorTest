using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Devices
{
    /// <summary>
    /// 获取设备列表输入参数
    /// </summary>
    public class GetDeviceListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 模糊查询关键字（匹配设备编号/名称/型号）
        /// </summary>
        public string? Filter { get; set; }

        /// <summary>
        /// 所属单位ID筛选
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 设备类型筛选
        /// </summary>
        public int? DeviceType { get; set; }

        /// <summary>
        /// 设备状态筛选
        /// </summary>
        public int? DeviceStatus { get; set; }

        /// <summary>
        /// 是否需要检查校准有效期
        /// </summary>
        public bool? CheckCalibration { get; set; }
    }
}
