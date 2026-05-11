using System;
using JiaCeMonitorSystem.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace JiaCeMonitorSystem.Devices
{
    /// <summary>
    /// 单位设备聚合根，管理仪器设备全生命周期档案
    /// </summary>
    public class CompanyDevice : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 所属单位ID
        /// </summary>
        public Guid? CompanyId { get; private set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceCode { get; private set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; private set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType DeviceType { get; private set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string? Model { get; private set; }

        /// <summary>
        /// 生产厂家
        /// </summary>
        public string? Manufacturer { get; private set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNumber { get; private set; }

        /// <summary>
        /// 购置日期
        /// </summary>
        public DateTime? PurchaseDate { get; private set; }

        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime? UseDate { get; private set; }

        /// <summary>
        /// 设备精度
        /// </summary>
        public string? Accuracy { get; private set; }

        /// <summary>
        /// 量程范围
        /// </summary>
        public string? MeasurementRange { get; private set; }

        /// <summary>
        /// 最近校准日期
        /// </summary>
        public DateTime? CalibrationDate { get; private set; }

        /// <summary>
        /// 下次校准日期
        /// </summary>
        public DateTime? NextCalibrationDate { get; private set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public DeviceStatus DeviceStatus { get; private set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? Location { get; private set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        public Guid? ResponsiblePersonId { get; private set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string? ResponsiblePersonName { get; private set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string? ContactInfo { get; private set; }

        /// <summary>
        /// 设备描述
        /// </summary>
        public string? Description { get; private set; }

        private CompanyDevice()
        {
            DeviceCode = string.Empty;
            DeviceName = string.Empty;
        }

        /// <summary>
        /// 创建设备实体
        /// </summary>
        public CompanyDevice(
            Guid id,
            string deviceCode,
            string deviceName,
            DeviceType deviceType,
            Guid? companyId = null,
            string? model = null,
            string? manufacturer = null,
            string? serialNumber = null,
            DateTime? purchaseDate = null,
            DateTime? useDate = null,
            string? accuracy = null,
            string? measurementRange = null,
            string? location = null,
            Guid? responsiblePersonId = null,
            string? responsiblePersonName = null,
            string? contactInfo = null,
            string? description = null)
            : base(id)
        {
            CompanyId = companyId;
            DeviceCode = deviceCode;
            DeviceName = deviceName;
            DeviceType = deviceType;
            Model = model;
            Manufacturer = manufacturer;
            SerialNumber = serialNumber;
            PurchaseDate = purchaseDate;
            UseDate = useDate;
            Accuracy = accuracy;
            MeasurementRange = measurementRange;
            DeviceStatus = DeviceStatus.Normal;
            Location = location;
            ResponsiblePersonId = responsiblePersonId;
            ResponsiblePersonName = responsiblePersonName;
            ContactInfo = contactInfo;
            Description = description;
        }

        /// <summary>
        /// 更新设备基础信息
        /// </summary>
        public void UpdateInfo(
            string deviceName,
            DeviceType deviceType,
            string? model = null,
            string? manufacturer = null,
            string? serialNumber = null,
            string? location = null,
            string? description = null)
        {
            CheckNotScrapped();

            DeviceName = deviceName;
            DeviceType = deviceType;
            Model = model;
            Manufacturer = manufacturer;
            SerialNumber = serialNumber;
            Location = location;
            Description = description;
        }

        /// <summary>
        /// 借出设备
        /// </summary>
        public void LendOut()
        {
            CheckNotScrapped();

            if (DeviceStatus != DeviceStatus.Normal)
            {
                throw new BusinessException(ErrorCodes.Device_CannotLendCurrentStatus)
                    .WithData("CurrentStatus", DeviceStatus);
            }

            DeviceStatus = DeviceStatus.LentOut;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = DeviceStatus.Normal,
                NewStatus = DeviceStatus.LentOut
            });
        }

        /// <summary>
        /// 归还设备
        /// </summary>
        public void Return()
        {
            CheckNotScrapped();

            if (DeviceStatus != DeviceStatus.LentOut)
            {
                throw new BusinessException(ErrorCodes.Device_CannotLendCurrentStatus)
                    .WithData("CurrentStatus", DeviceStatus);
            }

            var oldStatus = DeviceStatus;
            DeviceStatus = DeviceStatus.Normal;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = oldStatus,
                NewStatus = DeviceStatus.Normal
            });
        }

        /// <summary>
        /// 执行校准
        /// </summary>
        public void Calibrate(DateTime calibrationDate, DateTime nextCalibrationDate, string? accuracy = null)
        {
            CheckNotScrapped();

            if (DeviceStatus == DeviceStatus.LentOut)
            {
                throw new BusinessException(ErrorCodes.Device_LentOutCannotCalibrate);
            }

            if (DeviceStatus == DeviceStatus.UnderRepair)
            {
                throw new BusinessException(ErrorCodes.Device_UnderRepairCannotCalibrate);
            }

            if (nextCalibrationDate <= calibrationDate)
            {
                throw new BusinessException(ErrorCodes.Device_InvalidCalibrationDate);
            }

            var oldStatus = DeviceStatus;
            CalibrationDate = calibrationDate;
            NextCalibrationDate = nextCalibrationDate;
            Accuracy = accuracy ?? Accuracy;
            DeviceStatus = DeviceStatus.Normal;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = oldStatus,
                NewStatus = DeviceStatus.Normal
            });
        }

        /// <summary>
        /// 报废设备
        /// </summary>
        public void Scrap(string reason)
        {
            var oldStatus = DeviceStatus;
            DeviceStatus = DeviceStatus.Scrapped;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = oldStatus,
                NewStatus = DeviceStatus.Scrapped,
                Reason = reason
            });
        }

        /// <summary>
        /// 送修
        /// </summary>
        public void Repair()
        {
            CheckNotScrapped();

            var oldStatus = DeviceStatus;
            DeviceStatus = DeviceStatus.UnderRepair;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = oldStatus,
                NewStatus = DeviceStatus.UnderRepair
            });
        }

        /// <summary>
        /// 维修完成
        /// </summary>
        public void FinishRepair()
        {
            CheckNotScrapped();

            if (DeviceStatus != DeviceStatus.UnderRepair)
            {
                throw new BusinessException(ErrorCodes.Device_CannotLendCurrentStatus)
                    .WithData("CurrentStatus", DeviceStatus);
            }

            var oldStatus = DeviceStatus;
            DeviceStatus = DeviceStatus.Normal;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = oldStatus,
                NewStatus = DeviceStatus.Normal
            });
        }

        /// <summary>
        /// 停用设备
        /// </summary>
        public void Deactivate()
        {
            CheckNotScrapped();

            var oldStatus = DeviceStatus;
            DeviceStatus = DeviceStatus.Deactivated;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = oldStatus,
                NewStatus = DeviceStatus.Deactivated
            });
        }

        /// <summary>
        /// 恢复使用
        /// </summary>
        public void Activate()
        {
            CheckNotScrapped();

            if (DeviceStatus != DeviceStatus.Deactivated)
            {
                throw new BusinessException(ErrorCodes.Device_CannotLendCurrentStatus)
                    .WithData("CurrentStatus", DeviceStatus);
            }

            var oldStatus = DeviceStatus;
            DeviceStatus = DeviceStatus.Normal;

            AddLocalEvent(new Events.DeviceStatusChangedDomainEvent
            {
                DeviceId = Id,
                OldStatus = oldStatus,
                NewStatus = DeviceStatus.Normal
            });
        }

        private void CheckNotScrapped()
        {
            if (DeviceStatus == DeviceStatus.Scrapped)
            {
                throw new BusinessException(ErrorCodes.Device_ScrappedCannotModify);
            }
        }
    }
}
