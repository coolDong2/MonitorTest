using System;
using System.Threading.Tasks;
using JiaCeMonitorSystem.ProjectPersonnels;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace JiaCeMonitorSystem.DomainServices
{
    /// <summary>
    /// 项目人员安排领域服务
    /// 校验人员项目时段冲突
    /// </summary>
    public class ProjectPersonnelManager : DomainService
    {
        private readonly IRepository<ProjectPersonnel, Guid> _projectPersonnelRepository;

        public ProjectPersonnelManager(
            IRepository<ProjectPersonnel, Guid> projectPersonnelRepository)
        {
            _projectPersonnelRepository = projectPersonnelRepository;
        }

        /// <summary>
        /// 校验同一项目同一时段内，同一人员不能重复安排
        /// </summary>
        public async Task ValidateTimeConflictAsync(
            Guid projectId,
            Guid userId,
            DateTime startDate,
            DateTime? endDate,
            Guid? excludeId = null)
        {
            var existingPersonnels = await _projectPersonnelRepository.GetListAsync(
                x => x.ProjectId == projectId
                     && x.UserId == userId
                     && x.WorkStatus != Enums.WorkStatus.Ended);

            foreach (var existing in existingPersonnels)
            {
                if (excludeId.HasValue && existing.Id == excludeId.Value)
                    continue;

                // 检查时间段是否重叠
                if (IsTimeOverlap(existing.StartDate, existing.EndDate, startDate, endDate))
                {
                    throw new BusinessException(ErrorCodes.ProjectPersonnel_TimeConflict)
                        .WithData("UserId", userId)
                        .WithData("ProjectId", projectId)
                        .WithData("ExistingStart", existing.StartDate.ToString("yyyy-MM-dd"))
                        .WithData("ExistingEnd", existing.EndDate?.ToString("yyyy-MM-dd") ?? "未结束");
                }
            }
        }

        /// <summary>
        /// 判断两个时间段是否重叠
        /// </summary>
        private bool IsTimeOverlap(
            DateTime start1, DateTime? end1,
            DateTime start2, DateTime? end2)
        {
            // 如果两个都没有结束日期，则只要开始日期相同就重叠
            if (!end1.HasValue && !end2.HasValue)
                return true;

            // 将无结束日期视为无限远
            var actualEnd1 = end1 ?? DateTime.MaxValue;
            var actualEnd2 = end2 ?? DateTime.MaxValue;

            return start1 <= actualEnd2 && actualEnd1 >= start2;
        }
    }
}
