using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace JiaCeMonitorSystem.Dtos.ProjectPersonnels
{
    /// <summary>
    /// 获取项目人员安排列表输入参数
    /// </summary>
    public class GetProjectPersonnelListInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 项目ID（必填）
        /// </summary>
        [Required]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 角色类型筛选
        /// </summary>
        public int? RoleType { get; set; }

        /// <summary>
        /// 模糊查询（角色名称、用户名称）
        /// </summary>
        public string? Filter { get; set; }
    }
}
