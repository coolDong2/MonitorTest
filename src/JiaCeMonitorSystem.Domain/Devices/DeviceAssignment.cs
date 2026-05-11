using System;
using JiaCeMonitorSystem.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.Devices
{
    /// <summary>
    /// 设备分配实体，记录设备从单位借出到项目使用的分配关系
    /// </summary>
    public class DeviceAssignment : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid DeviceId { get; private set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid ProjectId { get; private set; }

        /// <summary>
        /// 分配日期
        /// </summary>
        public DateTime AssignmentDate { get; private set; }

        /// <summary>
        /// 预计归还日期
        /// </summary>
        public DateTime? ExpectedReturnDate { get; private set; }

        /// <summary>
        /// 实际归还日期
        /// </summary>
        public DateTime? ActualReturnDate { get; private set; }

        /// <summary>
        /// 分配人ID
        /// </summary>
        public Guid? AssignerId { get; private set; }

        /// <summary>
        /// 分配人姓名
        /// </summary>
        public string? AssignerName { get; private set; }

        /// <summary>
        /// 领用人ID
        /// </summary>
        public Guid? ReceiverId { get; private set; }

        /// <summary>
        /// 领用人姓名
        /// </summary>
        public string? ReceiverName { get; private set; }

        /// <summary>
        /// 用途说明
        /// </summary>
        public string? UsageDescription { get; private set; }

        /// <summary>
        /// 分配状态
        /// </summary>
        public AssignmentStatus AssignmentStatus { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; private set; }

        private DeviceAssignment()
        {
        }

        /// <summary>
        /// 创建设备调配记录
        /// </summary>
        public DeviceAssignment(
            Guid id,
            Guid deviceId,
            Guid projectId,
            DateTime assignmentDate,
            Guid? assignerId = null,
            string? assignerName = null,
            Guid? receiverId = null,
            string? receiverName = null,
            DateTime? expectedReturnDate = null,
            string? usageDescription = null,
            string? remark = null)
            : base(id)
        {
            DeviceId = deviceId;
            ProjectId = projectId;
            AssignmentDate = assignmentDate;
            AssignerId = assignerId;
            AssignerName = assignerName;
            ReceiverId = receiverId;
            ReceiverName = receiverName;
            ExpectedReturnDate = expectedReturnDate;
            UsageDescription = usageDescription;
            AssignmentStatus = AssignmentStatus.Assigned;
            Remark = remark;
        }

        /// <summary>
        /// 标记为使用中
        /// </summary>
        public void MarkInUse()
        {
            if (AssignmentStatus != AssignmentStatus.Assigned)
            {
                throw new BusinessException(ErrorCodes.General_OperationNotAllowed)
                    .WithData("Reason", "只有已分配的设备可以标记为使用中");
            }

            AssignmentStatus = AssignmentStatus.InUse;
        }

        /// <summary>
        /// 延期归还
        /// </summary>
        public void ExtendReturnDate(DateTime newDate, string reason)
        {
            if (AssignmentStatus != AssignmentStatus.InUse && AssignmentStatus != AssignmentStatus.Assigned)
            {
                throw new BusinessException(ErrorCodes.General_OperationNotAllowed)
                    .WithData("Reason", "当前状态不允许延期");
            }

            ExpectedReturnDate = newDate;
            Remark = $"延期原因：{reason}";
            AssignmentStatus = AssignmentStatus.Extended;
        }

        /// <summary>
        /// 归还设备
        /// </summary>
        public void ReturnDevice(DateTime returnDate, string? condition = null)
        {
            if (AssignmentStatus != AssignmentStatus.InUse && AssignmentStatus != AssignmentStatus.Extended)
            {
                throw new BusinessException(ErrorCodes.General_OperationNotAllowed)
                    .WithData("Reason", "当前状态不允许归还");
            }

            ActualReturnDate = returnDate;
            AssignmentStatus = AssignmentStatus.Returned;

            if (!string.IsNullOrWhiteSpace(condition))
            {
                Remark = $"归还状态：{condition}";
            }
        }

        /// <summary>
        /// 报告损坏
        /// </summary>
        public void ReportDamage(string damageDescription)
        {
            if (AssignmentStatus != AssignmentStatus.InUse && AssignmentStatus != AssignmentStatus.Extended)
            {
                throw new BusinessException(ErrorCodes.General_OperationNotAllowed)
                    .WithData("Reason", "当前状态不允许报告损坏");
            }

            AssignmentStatus = AssignmentStatus.Damaged;
            Remark = $"损坏描述：{damageDescription}";
        }
    }
}
