using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiaCeMonitorSystem.Migrations
{
    /// <inheritdoc />
    public partial class Phase15_AddMissingModules_And_MonitoringDataRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemTypeName",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                comment: "监测项目类型名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "PointName",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                comment: "测点名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                comment: "项目名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "PropertyCode",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "属性编码（冗余）");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                schema: "public",
                table: "JC_WarningRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "监测属性ID（外键，关联MonitoringItemProperty）【重构新增】");

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "属性名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "public",
                table: "JC_WarningRecords",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "单位（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "ItemTypeName",
                schema: "public",
                table: "JC_Points",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                comment: "监测项目类型名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "CollectorName",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                comment: "采集人姓名（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                comment: "采集设备名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "ItemTypeName",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                comment: "监测项目类型名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "PointName",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                comment: "测点名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                comment: "项目名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "PropertyCode",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "属性编码（冗余）");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                schema: "public",
                table: "JC_MonitoringData",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "监测属性ID（外键，关联MonitoringItemProperty）【重构新增】");

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "属性名称（冗余）");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "public",
                table: "JC_MonitoringData",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "单位（冗余）");

            migrationBuilder.CreateTable(
                name: "JC_ModuleButtons",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false, comment: "所属模块ID"),
                    EnCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "编码"),
                    FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "名称"),
                    Icon = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "图标"),
                    Location = table.Column<int>(type: "integer", nullable: false, comment: "按钮位置：0=工具栏 1=行内 2=右键菜单"),
                    JsEvent = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "JS事件"),
                    UrlAddress = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: "链接地址"),
                    Split = table.Column<bool>(type: "boolean", nullable: false, comment: "是否有分割线"),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false, comment: "是否公共"),
                    AllowEdit = table.Column<bool>(type: "boolean", nullable: false, comment: "允许编辑"),
                    AllowDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "允许删除"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    Authorize = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: "授权"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_ModuleButtons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_MonitoringItemTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "类型编码"),
                    TypeName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "类型名称"),
                    Category = table.Column<int>(type: "integer", nullable: false, comment: "监测分类：0=位移监测 1=沉降监测 2=应力监测 3=水文监测 4=环境监测"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_MonitoringItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_Notices",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "标题"),
                    Content = table.Column<string>(type: "text", nullable: false, comment: "内容"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_Notices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_Organizes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true, comment: "父节点ID"),
                    Layers = table.Column<int>(type: "integer", nullable: false, comment: "层级"),
                    EnCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "编码"),
                    FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "全称"),
                    ShortName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "简称"),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true, comment: "分类ID"),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: true, comment: "负责人ID（关联IdentityUser）"),
                    TelePhone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true, comment: "电话"),
                    MobilePhone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true, comment: "手机"),
                    WeChat = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true, comment: "微信"),
                    Fax = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true, comment: "传真"),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "邮箱"),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: true, comment: "区域ID"),
                    Address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: "地址"),
                    AllowEdit = table.Column<bool>(type: "boolean", nullable: false, comment: "允许编辑"),
                    AllowDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "允许删除"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_Organizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_ProjectPersonnels",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false, comment: "项目ID"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, comment: "用户ID（关联IdentityUser）"),
                    RoleType = table.Column<int>(type: "integer", nullable: false, comment: "角色类型：0=项目经理 1=技术负责人 2=监测员 3=数据分析员 4=安全员 5=设备管理员"),
                    RoleName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, comment: "角色名称"),
                    Responsibility = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "职责描述"),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "开始日期"),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "结束日期"),
                    ContactInfo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "联系方式"),
                    WorkStatus = table.Column<int>(type: "integer", nullable: false, comment: "工作状态：0=在职 1=休假 2=调离 3=结束"),
                    Remark = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "备注"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_ProjectPersonnels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_SystemDictionaries",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false, comment: "字典类型ID"),
                    ItemCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "字典编码"),
                    ItemName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "字典名称"),
                    SimpleSpelling = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "简拼"),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, comment: "是否默认"),
                    Layers = table.Column<int>(type: "integer", nullable: false, comment: "层级"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_SystemDictionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_SystemDictionaryTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true, comment: "父节点ID"),
                    EnCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "编码"),
                    FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "名称"),
                    IsTree = table.Column<bool>(type: "boolean", nullable: false, comment: "是否树形"),
                    Layers = table.Column<int>(type: "integer", nullable: false, comment: "层级"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_SystemDictionaryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_SystemModules",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EnCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "编码"),
                    FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "名称"),
                    Icon = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "图标"),
                    UrlAddress = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: "链接地址"),
                    Target = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true, comment: "打开目标"),
                    IsMenu = table.Column<bool>(type: "boolean", nullable: false, comment: "是否菜单"),
                    IsExpand = table.Column<bool>(type: "boolean", nullable: false, comment: "是否展开"),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false, comment: "是否公共"),
                    IsFields = table.Column<bool>(type: "boolean", nullable: false, comment: "是否字段"),
                    AllowEdit = table.Column<bool>(type: "boolean", nullable: false, comment: "允许编辑"),
                    AllowDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "允许删除"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    Authorize = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: "授权"),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true, comment: "父节点ID"),
                    Layers = table.Column<int>(type: "integer", nullable: false, comment: "层级"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_SystemModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_UploadFiles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Hash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "文件Hash（MD5）"),
                    FilePath = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false, comment: "文件路径"),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "文件名称"),
                    FileType = table.Column<int>(type: "integer", nullable: false, comment: "文件类型：0=文件 1=图片"),
                    FileSize = table.Column<long>(type: "bigint", nullable: false, comment: "文件大小（字节）"),
                    FileExtension = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false, comment: "文件扩展名"),
                    FileBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "上传人"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    OrganizeId = table.Column<Guid>(type: "uuid", nullable: true, comment: "所属组织ID"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_UploadFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_MonitoringItemProperties",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemTypeId = table.Column<Guid>(type: "uuid", nullable: false, comment: "所属监测项目类型ID"),
                    PropertyCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "属性编码"),
                    PropertyName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "属性名称"),
                    DataType = table.Column<int>(type: "integer", nullable: false, comment: "数据类型：0=字符串 1=数字 2=日期 3=布尔"),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "单位"),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false, comment: "是否必填"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "描述"),
                    WarningThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "预警阈值【属性级】"),
                    AlarmThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "报警阈值【属性级】"),
                    ChangeRateThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "变化率阈值（%）【属性级】"),
                    CumulativeThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "累计变化阈值【属性级】"),
                    MonitoringItemTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_MonitoringItemProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JC_MonitoringItemProperties_JC_MonitoringItemTypes_Monitori~",
                        column: x => x.MonitoringItemTypeId,
                        principalSchema: "public",
                        principalTable: "JC_MonitoringItemTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarningRecords_PropertyId",
                schema: "public",
                table: "JC_WarningRecords",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringData_PropertyId",
                schema: "public",
                table: "JC_MonitoringData",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleButtons_ModuleId",
                schema: "public",
                table: "JC_ModuleButtons",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleButtons_ModuleId_EnCode",
                schema: "public",
                table: "JC_ModuleButtons",
                columns: new[] { "ModuleId", "EnCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JC_MonitoringItemProperties_MonitoringItemTypeId",
                schema: "public",
                table: "JC_MonitoringItemProperties",
                column: "MonitoringItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringItemProperties_ItemTypeId",
                schema: "public",
                table: "JC_MonitoringItemProperties",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringItemProperties_ItemTypeId_PropertyCode",
                schema: "public",
                table: "JC_MonitoringItemProperties",
                columns: new[] { "ItemTypeId", "PropertyCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringItemTypes_TypeCode",
                schema: "public",
                table: "JC_MonitoringItemTypes",
                column: "TypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizes_ManagerId",
                schema: "public",
                table: "JC_Organizes",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizes_ParentId",
                schema: "public",
                table: "JC_Organizes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPersonnels_ProjectId",
                schema: "public",
                table: "JC_ProjectPersonnels",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPersonnels_ProjectId_UserId",
                schema: "public",
                table: "JC_ProjectPersonnels",
                columns: new[] { "ProjectId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_SystemDictionaries_ItemId",
                schema: "public",
                table: "JC_SystemDictionaries",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemDictionaries_ItemId_ItemCode",
                schema: "public",
                table: "JC_SystemDictionaries",
                columns: new[] { "ItemId", "ItemCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemDictionaryTypes_EnCode",
                schema: "public",
                table: "JC_SystemDictionaryTypes",
                column: "EnCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemDictionaryTypes_ParentId",
                schema: "public",
                table: "JC_SystemDictionaryTypes",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemModules_EnCode",
                schema: "public",
                table: "JC_SystemModules",
                column: "EnCode");

            migrationBuilder.CreateIndex(
                name: "IX_SystemModules_ParentId",
                schema: "public",
                table: "JC_SystemModules",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadFiles_Hash",
                schema: "public",
                table: "JC_UploadFiles",
                column: "Hash");

            migrationBuilder.CreateIndex(
                name: "IX_UploadFiles_OrganizeId",
                schema: "public",
                table: "JC_UploadFiles",
                column: "OrganizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JC_ModuleButtons",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_MonitoringItemProperties",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_Notices",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_Organizes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_ProjectPersonnels",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_SystemDictionaries",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_SystemDictionaryTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_SystemModules",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_UploadFiles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_MonitoringItemTypes",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_WarningRecords_PropertyId",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropIndex(
                name: "IX_MonitoringData_PropertyId",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "ItemTypeName",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "PointName",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "PropertyCode",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "public",
                table: "JC_WarningRecords");

            migrationBuilder.DropColumn(
                name: "ItemTypeName",
                schema: "public",
                table: "JC_Points");

            migrationBuilder.DropColumn(
                name: "CollectorName",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "ItemTypeName",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "PointName",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "PropertyCode",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                schema: "public",
                table: "JC_MonitoringData");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "public",
                table: "JC_MonitoringData");
        }
    }
}
