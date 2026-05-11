using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JiaCeMonitorSystem.Devices;
using JiaCeMonitorSystem.Enums;
using JiaCeMonitorSystem.MonitoringItemTypes;
using JiaCeMonitorSystem.Notices;
using JiaCeMonitorSystem.Points;
using JiaCeMonitorSystem.ProjectPersonnels;
using JiaCeMonitorSystem.Projects;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace JiaCeMonitorSystem.Seeds
{
    /// <summary>
    /// 测试业务数据种子，创建示例工程、测点与设备数据，便于前端联调
    /// </summary>
    public class TestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Point, Guid> _pointRepository;
        private readonly IRepository<CompanyDevice, Guid> _deviceRepository;
        private readonly IRepository<MonitoringItemType, Guid> _monitoringItemTypeRepository;
        private readonly IRepository<MonitoringItemProperty, Guid> _monitoringItemPropertyRepository;
        private readonly IRepository<ProjectPersonnel, Guid> _projectPersonnelRepository;
        private readonly IRepository<Notice, Guid> _noticeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IGuidGenerator _guidGenerator;

        // 共享种子数据引用
        private MonitoringItemType? _displacementType;
        private List<MonitoringItemProperty> _displacementProperties = new();
        private Project? _project1;

        public TestDataSeedContributor(
            IRepository<Project, Guid> projectRepository,
            IRepository<Point, Guid> pointRepository,
            IRepository<CompanyDevice, Guid> deviceRepository,
            IRepository<MonitoringItemType, Guid> monitoringItemTypeRepository,
            IRepository<MonitoringItemProperty, Guid> monitoringItemPropertyRepository,
            IRepository<ProjectPersonnel, Guid> projectPersonnelRepository,
            IRepository<Notice, Guid> noticeRepository,
            UserManager<IdentityUser> userManager,
            IGuidGenerator guidGenerator)
        {
            _projectRepository = projectRepository;
            _pointRepository = pointRepository;
            _deviceRepository = deviceRepository;
            _monitoringItemTypeRepository = monitoringItemTypeRepository;
            _monitoringItemPropertyRepository = monitoringItemPropertyRepository;
            _projectPersonnelRepository = projectPersonnelRepository;
            _noticeRepository = noticeRepository;
            _userManager = userManager;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // 仅在数据库为空时插入测试数据
            if (await _projectRepository.GetCountAsync() > 0)
            {
                return;
            }

            await SeedProjectsAsync();
            await SeedMonitoringItemTypesAsync();
            await SeedPointsAsync();
            await SeedDevicesAsync();
            await SeedProjectPersonnelsAsync();
            await SeedNoticesAsync();
        }

        private async Task SeedProjectsAsync()
        {
            _project1 = new Project(
                _guidGenerator.Create(),
                "JC-2026-001",
                "地铁三号线沉降监测工程",
                "市中心人民大道",
                DateTime.Now.AddMonths(-6),
                DateTime.Now.AddMonths(6),
                "张工",
                "13800138000",
                "地铁三号线施工期间地表沉降实时监测项目"
            );
            _project1.ChangeStatus(ProjectStatus.InProgress);

            var project2 = new Project(
                _guidGenerator.Create(),
                "JC-2026-002",
                "高架桥梁变形监测",
                "环城高架K15+200段",
                DateTime.Now.AddMonths(-3),
                DateTime.Now.AddMonths(9),
                "李工",
                "13900139000",
                "高架桥梁结构变形与健康监测"
            );
            project2.ChangeStatus(ProjectStatus.InProgress);

            await _projectRepository.InsertAsync(_project1);
            await _projectRepository.InsertAsync(project2);
        }

        private async Task SeedMonitoringItemTypesAsync()
        {
            // 创建"位移监测"项目类型
            _displacementType = new MonitoringItemType(
                _guidGenerator.Create(),
                "DISPLACEMENT",
                "位移监测",
                MonitoringCategory.Displacement,
                1,
                true,
                "用于监测结构物或地表的水平、垂直位移变化"
            );

            // 添加3个属性
            var prop1 = _displacementType.AddProperty(
                _guidGenerator.Create(),
                "HORIZONTAL_DISP",
                "水平位移",
                PropertyDataType.Number,
                "mm",
                true,
                1,
                "监测点相对于基准点的水平方向位移量"
            );

            var prop2 = _displacementType.AddProperty(
                _guidGenerator.Create(),
                "VERTICAL_DISP",
                "垂直位移",
                PropertyDataType.Number,
                "mm",
                true,
                2,
                "监测点相对于基准点的垂直方向位移量（沉降/隆起）"
            );

            var prop3 = _displacementType.AddProperty(
                _guidGenerator.Create(),
                "CUMULATIVE_DISP",
                "累计位移",
                PropertyDataType.Number,
                "mm",
                true,
                3,
                "从监测开始至今的累计位移量"
            );

            _displacementProperties = new List<MonitoringItemProperty> { prop1, prop2, prop3 };

            await _monitoringItemTypeRepository.InsertAsync(_displacementType);
        }

        private async Task SeedPointsAsync()
        {
            var projects = await _projectRepository.GetListAsync();
            if (projects.Count == 0) return;

            var project1 = projects.FirstOrDefault(p => p.ProjectCode == "JC-2026-001") ?? projects[0];

            var itemTypeId = _displacementType?.Id;
            var itemTypeName = _displacementType?.TypeName;

            var point1 = new Point(
                _guidGenerator.Create(),
                project1.Id,
                "P-001",
                "人民大道北侧观测点",
                itemTypeId,
                itemTypeName,
                120.1234m,
                30.5678m,
                5.20m,
                7,
                -10.0m,
                -20.0m,
                2.0m,
                -15.0m,
                null,
                "位于人民大道北侧人行道，靠近施工围挡"
            );

            var point2 = new Point(
                _guidGenerator.Create(),
                project1.Id,
                "P-002",
                "人民大道南侧观测点",
                itemTypeId,
                itemTypeName,
                120.1240m,
                30.5680m,
                5.15m,
                7,
                -10.0m,
                -20.0m,
                2.0m,
                -15.0m,
                null,
                "位于人民大道南侧绿化带"
            );

            var point3 = new Point(
                _guidGenerator.Create(),
                project1.Id,
                "P-003",
                "交叉路口中心点",
                itemTypeId,
                itemTypeName,
                120.1237m,
                30.5679m,
                5.18m,
                3,
                -8.0m,
                -15.0m,
                1.5m,
                -10.0m,
                null,
                "交叉路口中心交通岛位置"
            );

            await _pointRepository.InsertAsync(point1);
            await _pointRepository.InsertAsync(point2);
            await _pointRepository.InsertAsync(point3);
        }

        private async Task SeedDevicesAsync()
        {
            var device1 = new CompanyDevice(
                _guidGenerator.Create(),
                "DEV-2026-001",
                "全站仪 TC-1201",
                DeviceType.TotalStation,
                null,
                "TC-1201",
                "徕卡测量系统",
                "SN123456789",
                DateTime.Now.AddYears(-2),
                DateTime.Now.AddYears(-2),
                "1\"",
                "1.5m - 3500m",
                "仪器室A-01柜",
                null,
                "王管理员",
                "13700137000",
                null
            );
            device1.Calibrate(DateTime.Now.AddMonths(-3), DateTime.Now.AddMonths(9), "1\"");

            var device2 = new CompanyDevice(
                _guidGenerator.Create(),
                "DEV-2026-002",
                "GNSS接收机 R10",
                DeviceType.DataCollector,
                null,
                "R10",
                "Trimble",
                "SN987654321",
                DateTime.Now.AddYears(-1),
                DateTime.Now.AddYears(-1),
                "水平±8mm+1ppm",
                "全球覆盖",
                "仪器室A-02柜",
                null,
                "王管理员",
                "13700137000",
                null
            );
            device2.Calibrate(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(11));

            await _deviceRepository.InsertAsync(device1);
            await _deviceRepository.InsertAsync(device2);
        }

        private async Task SeedProjectPersonnelsAsync()
        {
            var adminUser = await _userManager.FindByNameAsync("admin");
            var projects = await _projectRepository.GetListAsync();
            if (projects.Count == 0 || adminUser == null) return;

            var project = projects.FirstOrDefault(p => p.ProjectCode == "JC-2026-001") ?? projects[0];

            var personnel = new ProjectPersonnel(
                _guidGenerator.Create(),
                project.Id,
                adminUser.Id,
                RoleType.TechnicalLead,
                "技术负责人",
                DateTime.Now.AddMonths(-6),
                DateTime.Now.AddMonths(6),
                "负责项目整体技术方案制定、监测数据分析与报告审核",
                "13800138000",
                WorkStatus.Active,
                "拥有10年岩土工程监测经验"
            );

            await _projectPersonnelRepository.InsertAsync(personnel);
        }

        private async Task SeedNoticesAsync()
        {
            var notice = new Notice(
                _guidGenerator.Create(),
                "系统上线通知",
                @"尊敬的用户：

监测系统V1.0已正式上线运行，目前接入地铁三号线沉降监测工程和高架桥梁变形监测两个项目。

系统主要功能包括：
1. 实时监测数据查看与分析
2. 多级预警阈值配置
3. 设备管理与校准记录
4. 监测项目类型与属性管理

如有问题请联系技术支持。",
                true,
                "系统功能介绍与使用说明"
            );

            await _noticeRepository.InsertAsync(notice);
        }
    }
}
