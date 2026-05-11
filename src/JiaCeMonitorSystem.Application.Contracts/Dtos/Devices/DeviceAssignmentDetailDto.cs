using System;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.Devices
{
    /// <summary>
    /// 设备分配详情DTO（包含借出统计计算字段）
    /// </summary>
    public class DeviceAssignmentDetailDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string DeviceTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 分配日期
        /// </summary>
        public DateTime AssignmentDate { get; set; }

        /// <summary>
        /// 预计归还日期
        /// </summary>
        public DateTime? ExpectedReturnDate { get; set; }

        /// <summary>
        /// 实际归还日期
        /// </summary>
        public DateTime? ActualReturnDate { get; set; }

        /// <summary>
        /// 分配人姓名
        /// </summary>
        public string? AssignerName { get; set; }

        /// <summary>
        /// 领用人姓名
        /// </summary>
        public string? ReceiverName { get; set; }

        /// <summary>
        /// 用途说明
        /// </summary>
        public string? UsageDescription { get; set; }

        /// <summary>
        /// 分配状态
        /// </summary>
        public int AssignmentStatus { get; set; }

        /// <summary>
        /// 分配状态文本
        /// </summary>
        public string AssignmentStatusText { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 借用天数
        /// </summary>
        public int BorrowedDays { get; set; }

        /// <summary>
        /// 剩余天数（如果未归还）
        /// </summary>
        public int RemainingDays { get; set; }

        /// <summary>
        /// 是否已过期
        /// </summary>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// 过期天数
        /// </summary>
        public int OverdueDays { get; set; }

        /// <summary>
        /// 是否当前借用中
        /// </summary>
        public bool IsCurrentlyBorrowed { get; set; }
    }
}
