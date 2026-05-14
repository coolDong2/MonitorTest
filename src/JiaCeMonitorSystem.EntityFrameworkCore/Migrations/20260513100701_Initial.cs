using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiaCeMonitorSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "AbpAuditLogExcelFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogExcelFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpAuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationName = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImpersonatorUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ImpersonatorTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImpersonatorTenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "integer", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    HttpMethod = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Url = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Exceptions = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    HttpStatusCode = table.Column<int>(type: "integer", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpBackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationName = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    JobName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    JobArgs = table.Column<string>(type: "character varying(1048576)", maxLength: 1048576, nullable: false),
                    TryCount = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NextTryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastTryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsAbandoned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Priority = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)15),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpBackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    IsStatic = table.Column<bool>(type: "boolean", nullable: false),
                    Regex = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    RegexDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatureGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DefaultValue = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "boolean", nullable: false),
                    IsAvailableToHost = table.Column<bool>(type: "boolean", nullable: false),
                    AllowedProviders = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatureValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpLinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetTenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpLinkUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "character varying(95)", maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    MultiTenancySide = table.Column<byte>(type: "smallint", nullable: false),
                    Providers = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    StateCheckers = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsStatic = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSecurityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApplicationName = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    Identity = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    Action = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSecurityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Device = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DeviceInfo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    IpAddresses = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    SignedIn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastAccessed = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettingDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    DefaultValue = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "boolean", nullable: false),
                    Providers = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsInherited = table.Column<bool>(type: "boolean", nullable: false),
                    IsEncrypted = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettingDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserDelegations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserDelegations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsExternal = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ShouldChangePasswordOnNextLogin = table.Column<bool>(type: "boolean", nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    LastPasswordChangeTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "所属公司名称"),
                    EnCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "角色编号"),
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "角色名称"),
                    Category = table.Column<int>(type: "integer", nullable: false, comment: "角色类型"),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "角色类型名称"),
                    AllowEdit = table.Column<bool>(type: "boolean", nullable: false, comment: "允许编辑"),
                    AllowDelete = table.Column<bool>(type: "boolean", nullable: false, comment: "允许删除"),
                    SortCode = table.Column<int>(type: "integer", nullable: false, comment: "排序码"),
                    EnabledMark = table.Column<bool>(type: "boolean", nullable: false, comment: "是否启用"),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "描述"),
                    PermissionButtonIds = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "权限按钮ID"),
                    PermissionFieldsIds = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "权限字段ID"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_AppRoles", x => x.Id);
                },
                comment: "业务角色");

            migrationBuilder.CreateTable(
                name: "JC_CompanyDevices",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true, comment: "所属单位ID"),
                    DeviceCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "设备编号"),
                    DeviceName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "设备名称"),
                    DeviceType = table.Column<int>(type: "integer", nullable: false, comment: "设备类型：0=全站仪 1=水准仪 2=GNSS接收机 3=测斜仪 4=应变计 5=土压力计 6=水位计 7=渗压计 8=裂缝计 9=其他"),
                    Model = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "设备型号"),
                    Manufacturer = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "生产厂家"),
                    SerialNumber = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "序列号"),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "购置日期"),
                    UseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "启用日期"),
                    Accuracy = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true, comment: "设备精度"),
                    MeasurementRange = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "量程范围"),
                    CalibrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "最近校准日期"),
                    NextCalibrationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "下次校准日期"),
                    DeviceStatus = table.Column<int>(type: "integer", nullable: false, comment: "设备状态：0=正常 1=维修中 2=已停用 3=已报废 4=已借出"),
                    Location = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: "存放位置"),
                    ResponsiblePersonId = table.Column<Guid>(type: "uuid", nullable: true, comment: "负责人ID"),
                    ResponsiblePersonName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "负责人姓名"),
                    ContactInfo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "联系方式"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "设备描述"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_CompanyDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_DeviceAssignments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false, comment: "设备ID"),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false, comment: "项目ID"),
                    AssignmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "分配日期"),
                    ExpectedReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "预计归还日期"),
                    ActualReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "实际归还日期"),
                    AssignerId = table.Column<Guid>(type: "uuid", nullable: true, comment: "分配人ID"),
                    AssignerName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "分配人姓名"),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: true, comment: "领用人ID"),
                    ReceiverName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "领用人姓名"),
                    UsageDescription = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "用途说明"),
                    AssignmentStatus = table.Column<int>(type: "integer", nullable: false, comment: "分配状态：0=已分配 1=使用中 2=已延期 3=已归还 4=已损坏"),
                    Remark = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "备注"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_DeviceAssignments", x => x.Id);
                });

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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_ModuleButtons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_MonitoringData",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PointId = table.Column<Guid>(type: "uuid", nullable: false, comment: "测点ID"),
                    PointName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "测点名称（冗余）"),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false, comment: "项目ID"),
                    ProjectName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "项目名称（冗余）"),
                    ItemTypeId = table.Column<Guid>(type: "uuid", nullable: true, comment: "监测项目类型ID"),
                    ItemTypeName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "监测项目类型名称（冗余）"),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false, comment: "监测属性ID（外键，关联MonitoringItemProperty）【重构新增】"),
                    PropertyCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "属性编码（冗余）"),
                    PropertyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "属性名称（冗余）"),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "单位（冗余）"),
                    MonitoringTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "监测时间"),
                    MonitoringValue = table.Column<decimal>(type: "numeric(18,4)", nullable: false, comment: "监测数值"),
                    DataQuality = table.Column<int>(type: "integer", nullable: false, comment: "数据质量：0=正常 1=异常 2=缺失 3=可疑"),
                    DataState = table.Column<int>(type: "integer", nullable: false, comment: "数据状态：0=原始 1=已审核 2=已归档"),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: true, comment: "采集设备ID"),
                    DeviceName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "采集设备名称（冗余）"),
                    CollectorId = table.Column<Guid>(type: "uuid", nullable: true, comment: "采集人ID"),
                    CollectorName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "采集人姓名（冗余）"),
                    CollectionMethod = table.Column<int>(type: "integer", nullable: false, comment: "采集方式：0=手动 1=自动 2=导入"),
                    ExtendedData = table.Column<string>(type: "jsonb", nullable: true, comment: "扩展监测数据（JSON格式）"),
                    DataRemark = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "数据备注"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_MonitoringData", x => x.Id);
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "开始日期"),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "结束日期"),
                    ContactInfo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "联系方式"),
                    WorkStatus = table.Column<int>(type: "integer", nullable: false, comment: "工作状态：0=在职 1=休假 2=调离 3=结束"),
                    Remark = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "备注"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_ProjectPersonnels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_Projects",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "项目编号"),
                    ProjectName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "项目名称"),
                    ProjectLocation = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: "项目地点"),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "项目开始日期"),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "项目结束日期"),
                    ResponsiblePerson = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "项目负责人"),
                    ContactInfo = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "负责人联系方式"),
                    Status = table.Column<int>(type: "integer", nullable: false, comment: "项目状态：0=筹备中 1=进行中 2=已完成 3=已暂停 4=已归档"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "项目描述"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_Projects", x => x.Id);
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_SystemModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_TenantButtonPermissions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false, comment: "关联租户Id"),
                    ButtonId = table.Column<Guid>(type: "uuid", nullable: false, comment: "按钮Id"),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true, comment: "角色Id"),
                    IsGranted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "是否已授权"),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_TenantButtonPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_TenantConfigurations",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false, comment: "关联租户Id"),
                    IsIndependentDatabase = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "是否使用独立数据库"),
                    IndependentConnectionString = table.Column<string>(type: "text", nullable: true, comment: "独立数据库连接字符串"),
                    MaxUserCount = table.Column<int>(type: "integer", nullable: true, comment: "最大用户数量"),
                    MaxStorageBytes = table.Column<long>(type: "bigint", nullable: true, comment: "最大存储容量（字节）"),
                    MaxProjectCount = table.Column<int>(type: "integer", nullable: true, comment: "最大工程数量"),
                    MaxPointCount = table.Column<int>(type: "integer", nullable: true, comment: "最大测点数量"),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "到期日期"),
                    RemindDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "提醒日期"),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1, comment: "租户状态"),
                    LicenseKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: "许可证密钥"),
                    CertificateInfo = table.Column<string>(type: "text", nullable: true, comment: "证书信息"),
                    UnitCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "单位编码"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_TenantConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_TenantModuleGrants",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false, comment: "关联租户Id"),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false, comment: "系统模块Id"),
                    IsGranted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true, comment: "是否已授权"),
                    GrantDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "授权日期"),
                    ExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "授权到期日期"),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_TenantModuleGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_TenantUserExtensions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false, comment: "关联用户Id"),
                    UnitCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "单位编码"),
                    UserType = table.Column<int>(type: "integer", nullable: false, defaultValue: 2, comment: "用户类型"),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false, comment: "关联租户Id"),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_TenantUserExtensions", x => x.Id);
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_UploadFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JC_WarningRecords",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PointId = table.Column<Guid>(type: "uuid", nullable: false, comment: "监测点ID"),
                    PointName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "测点名称（冗余）"),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false, comment: "项目ID"),
                    ProjectName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "项目名称（冗余）"),
                    ItemTypeId = table.Column<Guid>(type: "uuid", nullable: true, comment: "监测项目类型ID"),
                    ItemTypeName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "监测项目类型名称（冗余）"),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false, comment: "监测属性ID（外键，关联MonitoringItemProperty）【重构新增】"),
                    PropertyCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "属性编码（冗余）"),
                    PropertyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "属性名称（冗余）"),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "单位（冗余）"),
                    MonitoringDataId = table.Column<Guid>(type: "uuid", nullable: true, comment: "触发该预警的监测数据ID"),
                    DataState = table.Column<int>(type: "integer", nullable: false, comment: "数据状态（冗余）"),
                    CollectorName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "采集人姓名（冗余）"),
                    DataRemark = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "数据备注（冗余）"),
                    WarningTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "预警时间"),
                    MonitoringTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "触发监测时间"),
                    MonitoringValue = table.Column<decimal>(type: "numeric(18,4)", nullable: false, comment: "触发监测值"),
                    WarningType = table.Column<int>(type: "integer", nullable: false, comment: "预警类型：0=阈值超限 1=变化率超限 2=累计变化超限"),
                    WarningLevel = table.Column<int>(type: "integer", nullable: false, comment: "预警级别：0=提示 1=一级预警 2=二级预警 3=三级预警"),
                    TriggerValue = table.Column<decimal>(type: "numeric(18,4)", nullable: false, comment: "触发值"),
                    ThresholdValue = table.Column<decimal>(type: "numeric(18,4)", nullable: false, comment: "阈值设定值"),
                    PreviousValue = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "前次监测值"),
                    ChangeRate = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "变化率（%）"),
                    CumulativeChange = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "累计变化量"),
                    WarningContent = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false, comment: "预警内容描述"),
                    SuggestedAction = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "建议措施"),
                    HandlerId = table.Column<Guid>(type: "uuid", nullable: true, comment: "处理负责人ID"),
                    HandlerName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "处理负责人姓名"),
                    HandleStatus = table.Column<int>(type: "integer", nullable: false, comment: "处理状态：0=未处理 1=处理中 2=已处理 3=已确认 4=已关闭"),
                    HandleTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "处理完成时间"),
                    HandleSolution = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "处理方案"),
                    HandleResult = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true, comment: "处理结果"),
                    ConfirmerId = table.Column<Guid>(type: "uuid", nullable: true, comment: "确认人ID"),
                    ConfirmerName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: "确认人姓名"),
                    ConfirmTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "确认时间"),
                    ConfirmRemark = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "确认备注"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_WarningRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<string>(type: "text", nullable: true),
                    ClientType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ConsentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    JsonWebKeySet = table.Column<string>(type: "text", nullable: true),
                    Permissions = table.Column<string>(type: "text", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedirectUris = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    Settings = table.Column<string>(type: "text", nullable: true),
                    FrontChannelLogoutUri = table.Column<string>(type: "text", nullable: true),
                    ClientUri = table.Column<string>(type: "text", nullable: true),
                    LogoUri = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Descriptions = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Resources = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpAuditLogActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Parameters = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AbpAuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ChangeType = table.Column<byte>(type: "smallint", nullable: false),
                    EntityTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    EntityTypeFullName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityChanges_AbpAuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AbpAuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClaimType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpRoleClaims_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenantConnectionStrings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenantConnectionStrings", x => new { x.TenantId, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpTenantConnectionStrings_AbpTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AbpTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClaimType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserClaims_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProviderKey = table.Column<string>(type: "character varying(196)", maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_AbpUserLogins_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserOrganizationUnits_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AbpUserTokens_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "JC_Points",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false, comment: "所属项目ID"),
                    PointCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: "监测点编号"),
                    PointName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: "监测点名称"),
                    ItemTypeId = table.Column<Guid>(type: "uuid", nullable: true, comment: "监测项目类型ID"),
                    ItemTypeName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: "监测项目类型名称（冗余）"),
                    LocationX = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "X坐标/经度"),
                    LocationY = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "Y坐标/纬度"),
                    LocationZ = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "Z坐标/高程"),
                    CurrentValue = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "当前监测值"),
                    LastMonitoringTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "最后监测时间"),
                    MonitoringFrequency = table.Column<int>(type: "integer", nullable: true, comment: "监测频率（天）"),
                    MaxValue = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "历史最大值"),
                    MinValue = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "历史最小值"),
                    AverageValue = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "历史平均值"),
                    DataCount = table.Column<int>(type: "integer", nullable: false, comment: "数据点数"),
                    WarningThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "预警阈值"),
                    AlarmThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "报警阈值"),
                    ChangeRateThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "变化率阈值（%）"),
                    CumulativeThreshold = table.Column<decimal>(type: "numeric(18,4)", nullable: true, comment: "累计变化阈值"),
                    CurrentWarningLevel = table.Column<int>(type: "integer", nullable: false, comment: "当前预警级别：0=无 1=提示 2=一级预警 3=二级预警"),
                    LastWarningTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "最后预警时间"),
                    TotalWarningCount = table.Column<int>(type: "integer", nullable: false, comment: "总预警次数"),
                    ActiveWarningCount = table.Column<int>(type: "integer", nullable: false, comment: "当前活跃预警数"),
                    ExtendedProperties = table.Column<string>(type: "jsonb", nullable: true, comment: "扩展属性（JSON格式）"),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true, comment: "点位描述"),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JC_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JC_Points_JC_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "JC_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Scopes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityPropertyChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityChangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PropertyTypeFullName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityPropertyChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId",
                        column: x => x.EntityChangeId,
                        principalTable: "AbpEntityChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReferenceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogActions_AuditLogId",
                table: "AbpAuditLogActions",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~",
                table: "AbpAuditLogActions",
                columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_ExecutionTime",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "UserId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_AuditLogId",
                table: "AbpEntityChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId",
                table: "AbpEntityChanges",
                columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityPropertyChanges_EntityChangeId",
                table: "AbpEntityPropertyChanges",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureGroups_Name",
                table: "AbpFeatureGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_GroupName",
                table: "AbpFeatures",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_Name",
                table: "AbpFeatures",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatureValues_Name_ProviderName_ProviderKey",
                table: "AbpFeatureValues",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~",
                table: "AbpLinkUsers",
                columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "AbpOrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_Code",
                table: "AbpOrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey",
                table: "AbpPermissionGrants",
                columns: new[] { "TenantId", "Name", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissionGroups_Name",
                table: "AbpPermissionGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_GroupName",
                table: "AbpPermissions",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_Name",
                table: "AbpPermissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoleClaims_RoleId",
                table: "AbpRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_NormalizedName",
                table: "AbpRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Action",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Action" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_ApplicationName",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "ApplicationName" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_Identity",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "Identity" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSecurityLogs_TenantId_UserId",
                table: "AbpSecurityLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_Device",
                table: "AbpSessions",
                column: "Device");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_SessionId",
                table: "AbpSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSessions_TenantId_UserId",
                table: "AbpSessions",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettingDefinitions_Name",
                table: "AbpSettingDefinitions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettings_Name_ProviderName_ProviderKey",
                table: "AbpSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_Name",
                table: "AbpTenants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserClaims_UserId",
                table: "AbpUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLogins_LoginProvider_ProviderKey",
                table: "AbpUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId",
                table: "AbpUserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserRoles_RoleId_UserId",
                table: "AbpUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_Email",
                table: "AbpUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedEmail",
                table: "AbpUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_NormalizedUserName",
                table: "AbpUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_UserName",
                table: "AbpUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_CompanyId",
                table: "JC_AppRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_EnCode",
                table: "JC_AppRoles",
                column: "EnCode");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDevices_DeviceCode",
                schema: "public",
                table: "JC_CompanyDevices",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDevices_DeviceStatus",
                schema: "public",
                table: "JC_CompanyDevices",
                column: "DeviceStatus");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDevices_DeviceType",
                schema: "public",
                table: "JC_CompanyDevices",
                column: "DeviceType");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDevices_NextCalibrationDate",
                schema: "public",
                table: "JC_CompanyDevices",
                column: "NextCalibrationDate");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAssignments_AssignmentStatus",
                schema: "public",
                table: "JC_DeviceAssignments",
                column: "AssignmentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAssignments_DeviceId_Status",
                schema: "public",
                table: "JC_DeviceAssignments",
                columns: new[] { "DeviceId", "AssignmentStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAssignments_ProjectId",
                schema: "public",
                table: "JC_DeviceAssignments",
                column: "ProjectId");

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
                name: "IX_MonitoringData_DataState",
                schema: "public",
                table: "JC_MonitoringData",
                column: "DataState");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringData_MonitoringTime",
                schema: "public",
                table: "JC_MonitoringData",
                column: "MonitoringTime");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringData_PointId_MonitoringTime",
                schema: "public",
                table: "JC_MonitoringData",
                columns: new[] { "PointId", "MonitoringTime" });

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringData_ProjectId",
                schema: "public",
                table: "JC_MonitoringData",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringData_PropertyId",
                schema: "public",
                table: "JC_MonitoringData",
                column: "PropertyId");

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
                name: "IX_Points_CurrentWarningLevel",
                schema: "public",
                table: "JC_Points",
                column: "CurrentWarningLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Points_PointCode",
                schema: "public",
                table: "JC_Points",
                column: "PointCode");

            migrationBuilder.CreateIndex(
                name: "IX_Points_ProjectId",
                schema: "public",
                table: "JC_Points",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_ProjectId_PointCode",
                schema: "public",
                table: "JC_Points",
                columns: new[] { "ProjectId", "PointCode" },
                unique: true);

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
                name: "IX_Projects_ProjectCode",
                schema: "public",
                table: "JC_Projects",
                column: "ProjectCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Status",
                schema: "public",
                table: "JC_Projects",
                column: "Status");

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
                name: "IX_JC_TenantButtonPermissions_TenantId_ButtonId_RoleId",
                schema: "public",
                table: "JC_TenantButtonPermissions",
                columns: new[] { "TenantId", "ButtonId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JC_TenantConfigurations_TenantId",
                schema: "public",
                table: "JC_TenantConfigurations",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JC_TenantConfigurations_UnitCode",
                schema: "public",
                table: "JC_TenantConfigurations",
                column: "UnitCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JC_TenantModuleGrants_TenantId_ModuleId",
                schema: "public",
                table: "JC_TenantModuleGrants",
                columns: new[] { "TenantId", "ModuleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JC_TenantUserExtensions_UserId",
                schema: "public",
                table: "JC_TenantUserExtensions",
                column: "UserId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_WarningRecords_HandleStatus",
                schema: "public",
                table: "JC_WarningRecords",
                column: "HandleStatus");

            migrationBuilder.CreateIndex(
                name: "IX_WarningRecords_MonitoringTime",
                schema: "public",
                table: "JC_WarningRecords",
                column: "MonitoringTime");

            migrationBuilder.CreateIndex(
                name: "IX_WarningRecords_PointId_HandleStatus",
                schema: "public",
                table: "JC_WarningRecords",
                columns: new[] { "PointId", "HandleStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_WarningRecords_ProjectId",
                schema: "public",
                table: "JC_WarningRecords",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WarningRecords_PropertyId",
                schema: "public",
                table: "JC_WarningRecords",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_WarningRecords_WarningLevel",
                schema: "public",
                table: "JC_WarningRecords",
                column: "WarningLevel");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                table: "OpenIddictTokens",
                column: "ReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpAuditLogActions");

            migrationBuilder.DropTable(
                name: "AbpAuditLogExcelFiles");

            migrationBuilder.DropTable(
                name: "AbpBackgroundJobs");

            migrationBuilder.DropTable(
                name: "AbpClaimTypes");

            migrationBuilder.DropTable(
                name: "AbpEntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "AbpFeatureGroups");

            migrationBuilder.DropTable(
                name: "AbpFeatures");

            migrationBuilder.DropTable(
                name: "AbpFeatureValues");

            migrationBuilder.DropTable(
                name: "AbpLinkUsers");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "AbpPermissionGrants");

            migrationBuilder.DropTable(
                name: "AbpPermissionGroups");

            migrationBuilder.DropTable(
                name: "AbpPermissions");

            migrationBuilder.DropTable(
                name: "AbpRoleClaims");

            migrationBuilder.DropTable(
                name: "AbpSecurityLogs");

            migrationBuilder.DropTable(
                name: "AbpSessions");

            migrationBuilder.DropTable(
                name: "AbpSettingDefinitions");

            migrationBuilder.DropTable(
                name: "AbpSettings");

            migrationBuilder.DropTable(
                name: "AbpTenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "AbpUserClaims");

            migrationBuilder.DropTable(
                name: "AbpUserDelegations");

            migrationBuilder.DropTable(
                name: "AbpUserLogins");

            migrationBuilder.DropTable(
                name: "AbpUserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpUserRoles");

            migrationBuilder.DropTable(
                name: "AbpUserTokens");

            migrationBuilder.DropTable(
                name: "JC_AppRoles");

            migrationBuilder.DropTable(
                name: "JC_CompanyDevices",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_DeviceAssignments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_ModuleButtons",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_MonitoringData",
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
                name: "JC_Points",
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
                name: "JC_TenantButtonPermissions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_TenantConfigurations",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_TenantModuleGrants",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_TenantUserExtensions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_UploadFiles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_WarningRecords",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OpenIddictScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens");

            migrationBuilder.DropTable(
                name: "AbpEntityChanges");

            migrationBuilder.DropTable(
                name: "AbpTenants");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpRoles");

            migrationBuilder.DropTable(
                name: "AbpUsers");

            migrationBuilder.DropTable(
                name: "JC_MonitoringItemTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "JC_Projects",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations");

            migrationBuilder.DropTable(
                name: "AbpAuditLogs");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications");
        }
    }
}
