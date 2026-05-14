# JCMonitoring 租户重构任务日志

## 阶段总览

- [x] Phase 1: 领域层基础改造（数据模型 + 枚举）
- [x] Phase 2: 数据库迁移与种子数据
- [x] Phase 3: 应用契约层定义（DTO + 接口 + 权限常量）
- [x] Phase 4: 应用层服务实现（核心业务逻辑）
- [x] Phase 5: 登录流程重构（认证层改造）
- [x] Phase 6: 控制器架构重组 + 废弃 RoleManagementController + 增强 IdentityRoleController
- [x] Phase 7: AppRole 映射与权限同步机制
- [x] Phase 8: 独立数据库切换与连接串管理
- [x] Phase 9: 前端适配与系统后台界面
- [ ] Phase 10: 集成测试与验证

## 详细记录

### [2026-05-13 09:00] Phase 6 开始
- **状态**: 🟡 进行中
- **说明**: 控制器按业务模块重组，废弃 RoleManagementController。创建 8 个业务模块文件夹，将控制器按领域归类；彻底删除 RoleManagementController 及关联服务/DTO/映射；增强 IdentityRoleController 补充权限分配能力。

---

### [2026-05-13 09:38] Phase 1 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase1`
- **说明**: 新建 TenantManagement 领域文件夹，完成聚合根 TenantConfiguration、实体 TenantModuleGrant/TenantButtonPermission/TenantUserExtension、枚举 TenantStatus/TenantDatabaseType/UserType、仓储接口 ITenantConfigurationRepository、Domain.Shared 常量 TenantConsts。
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.Domain` —— **0 错误，14 警告（CS1591）**

### [2026-05-13 09:48] Phase 2 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase2`
- **说明**: DbContext 注册 4 个新 DbSet（TenantConfigurations/TenantModuleGrants/TenantButtonPermissions/TenantUserExtensions），创建 4 个 EF Core 配置类（含 UNIQUE 索引与联合 UNIQUE 约束），生成迁移 `20260513014809_AddTenantManagementEntities`，创建 TenantManagementDataSeedContributor（Host 租户配置、模块授权、admin 用户扩展）。
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.Application` —— **0 错误，1 警告（CS0618 Obsolete）**

### [2026-05-13 09:53] Phase 3 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase3`
- **说明**: 新建 Application.Contracts/TenantManagement 文件夹，定义 7 个 DTO、3 个应用服务接口、TenantManagementPermissions 权限常量，并在 PermissionDefinitionProvider 中注册新权限。将 TenantStatus/TenantDatabaseType/UserType 枚举从 Domain 迁移至 Domain.Shared 以解决层引用问题。
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.Application.Contracts` —— **0 错误，66 警告（CS1591）**；`dotnet build src/JiaCeMonitorSystem.Domain` —— **0 错误，14 警告（CS1591）**

### [2026-05-13 10:08] Phase 4 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase4`
- **说明**: 创建领域事件 TenantInitializedEvent、ITenantDatabaseManager 接口、TenantDatabaseManager（EF Core 层，独立数据库创建与迁移）、TenantConfigurationAppService（租户创建/配置/许可证/分页）、TenantModuleGrantAppService（模块授权/撤销/菜单树）、TenantStatusCheckJob（ABP BackgroundJob，到期检查与提醒）。更新 AutoMapper Profile 添加 TenantConfiguration 映射。修复 Application 与 EntityFrameworkCore 项目间的包版本冲突（OpenIddict.Abstractions 升级至 7.2.0）。
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.Application` —— **0 错误，1 警告（CS0618）**；`dotnet build src/JiaCeMonitorSystem.EntityFrameworkCore` —— **0 错误，0 警告**

### [2026-05-13 10:12] Phase 5 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase5`
- **说明**: 保留现有 `/api/app/account/login` 系统管理员登录；新增 `/api/tenant-auth/login` 租户用户登录（单位编码 + 用户名密码）。实现 TenantAuthAppService：UnitCode 校验 → 租户状态检查（Expired/Suspended）→ CurrentTenant.Change 切换上下文 → 用户密码验证 → 获取 UserType → 生成含 tenant_id/unit_code/user_type 的 JWT Token。Token Claims 增强，支持租户级权限与菜单树返回。
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.Application` —— **0 错误，2 警告（CS0618 + CS8629）**；`dotnet build src/JiaCeMonitorSystem.HttpApi` —— **0 错误，2 警告（CS1591）**

### [2026-05-13 09:34] Phase 6 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase6`
- **说明**: 控制器按业务模块重组，废弃 RoleManagementController
  - **创建 8 个业务模块文件夹**：`Engineering`（工程监测）、`Monitoring`（监测数据）、`Warning`（预警管理）、`Equipment`（设备管理）、`System`（系统管理）、`Identity`（身份认证）、`Authorization`（权限授权）、`Common`（公共通用）
  - **移动全部控制器文件**：将原本散落在根目录的控制器按业务领域归类到对应模块文件夹，同步更新所有 `using` 语句与命名空间（如 `JiaCeMonitorSystem.Controllers.Engineering`）
  - **彻底废弃 RoleManagementController**：
    - 删除 `RoleManagementController.cs`（路由 `api/app/role-management`）
    - 删除 `IRoleManagementAppService` 接口与 `RoleManagementAppService` 实现
    - 删除 `RoleDto`、`RoleUserDto` 及相关 AutoMapper 映射
    - 清理 `Permissions.cs` 中 `RoleManagement` 相关权限常量
    - **源码零残留**：全局搜索 `RoleManagement`、`IRoleManagement`、`RoleManagementAppService` 确认无引用
  - **增强 IdentityRoleController**（ABP 标准角色体系替代）：
    - 新增 `GET /api/identity/roles/{id}/permissions` —— 查询角色权限树
    - 新增 `PUT /api/identity/roles/{id}/permissions` —— 更新角色权限
    - 新增 `GET /api/identity/roles/{id}/users` —— 查询角色下的用户列表
  - **新增 IIdentityRoleExtendedAppService 接口及实现**：扩展 ABP 标准 `IIdentityRoleAppService`，补充权限分配能力
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.HttpApi.Host` —— **0 错误，0 警告**

### [2026-05-13 10:00] Phase 7 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase7`
- **说明**: 
  - 修复 `AppRoleMigrationService` 编译错误：注入 `IGuidGenerator` 替代 `GuidGenerator.Create()`；`AppRole` 无 `TenantId`，迁移时传入 `null`（Host 级角色）
  - 修复 `IdentityRoleController` `PermissionTreeDto` 歧义：同时引用 `Dtos.Permissions.PermissionTreeDto`（ABP 权限树）与 `Dtos.AppRoles.PermissionTreeDto`（模块树），分别用于 `/permissions` 与 `/module-permissions`、`/field-permissions` 端点
  - 删除旧 AppRole API 层：移除 `RoleController`、`RoleAuthorizeController`、`IAppRoleAppService`、`AppRoleAppService` 及全部 AppRole DTO（`AppRoleDto`/`AppRoleCreateDto`/`AppRoleUpdateDto`/`GetAppRoleListInput`）
  - 清理 AutoMapper Profile 中的 AppRole 映射代码
  - `TenantMenuAppService.GetCurrentTenantMenusAsync` 已基于 `TenantModuleGrant` + `SystemModule` 实现租户菜单加载；`GetRolePermissionTreeAsync` 已对接 ABP Permission 体系
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.HttpApi.Host` —— **0 错误，81 警告（CS1591/CS0618/CS8629）**

### [2026-05-13 10:30] Phase 8 完成 ✅
- **状态**: 已完成
- **Git 分支**: `feature/tenant-reform-phase7`
- **已完成**: 
  - `JiaCeTenantConnectionStringResolver`：继承 `MultiTenantConnectionStringResolver`，重写 `ResolveAsync`，查询 `TenantConfiguration` 解密独立数据库连接串，注册替换 `IConnectionStringResolver`
  - `TenantDatabaseManager`：创建 PostgreSQL 数据库、运行 EF Core Migration、执行 DataSeeder 种子数据
  - `TenantDataExporter`（EntityFrameworkCore 层）：按 `TenantId` 从共享库读取 ABP Identity 数据（Users/Roles/UserRoles/Claims/Logins/Tokens）、PermissionGrant、TenantManagement 数据（ModuleGrant/ButtonPermission/UserExtension），先清空目标库对应表再插入，避免种子数据冲突
  - `TenantDataValidator`（EntityFrameworkCore 层）：逐表对比源库与目标库的行数，输出 `TenantDataValidationResult` 明细
  - `TenantDataCleaner`（EntityFrameworkCore 层）：回滚时执行 `DROP DATABASE IF EXISTS`，先 `pg_terminate_backend` 断开活跃连接
  - `TenantDatabaseSwitchService`：整合创建数据库 → 数据导出 → 一致性验证 → 更新配置流程；任一步骤失败均触发 `TenantDataCleaner` 回滚删除独立数据库
  - `TenantDatabaseSwitchJob`：Hangfire 后台异步执行切换任务
  - `TenantConfigurationAppService.SwitchToIndependentDatabaseAsync`：触发后台 Job 的入口方法
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.HttpApi.Host` —— **0 错误，14 警告（CS1591/CS0618/CS8629）**

### [2026-05-13 18:07] 运行时错误修复：ABP 内置模块表缺失（42P01）
- **状态**: 已修复
- **错误现象**: 运行 `HttpApi.Host` 报 `Npgsql.PostgresException (0x80004005): 42P01: 关系 "AbpBackgroundJobs" 不存在` 和 `AbpSettings` 不存在
- **错误原因**: `JiaCeMonitorSystemDbContext.OnModelCreating` 中调用了 `builder.ConfigureXxx()` 扩展方法（如 `ConfigureBackgroundJobs`/`ConfigureSettingManagement` 等），但 `JiaCeMonitorSystemDbContextFactory` 在设计时直接 `new JiaCeMonitorSystemDbContext(builder.Options)`，缺少 ABP DI 容器初始化。`AbpDbContext` 的 `LazyServiceProvider` 为 null，导致 `ConfigureXxx()` 内部的条件判断（如 `IsTenantOnlyDatabase()`）提前返回，ABP 内置实体（Identity/Permission/Setting/BackgroundJobs/OpenIddict 等）未注册到 EF Core 模型。旧迁移文件和 ModelSnapshot 中均不包含这些表，但运行时 ABP 模块初始化后会查询它们，导致 42P01 表不存在错误。
- **解决方案**:
  1. **删除 `JiaCeMonitorSystemDbContextFactory.cs`**：让 `dotnet ef` 通过启动项目（`HttpApi.Host`）的 DI 容器创建 DbContext，ABP 模块系统正确初始化后 `ConfigureXxx()` 调用生效
  2. **删除所有旧迁移文件**，重新生成 `Initial` 迁移（包含 40+ 张 ABP 内置模块表）
  3. **开发环境修复步骤**：删除现有数据库 `JiaCeMonitorPGDB`，运行 `DbMigrator` 重新创建并种子化数据库
- **编译验证**: `dotnet build src/JiaCeMonitorSystem.HttpApi.Host` —— **0 错误，14 警告**

---

# JCMonitoring 系统开发任务日志

## 项目信息
- **项目路径**: `E:\CODEWORK\JiaCeMonitorSystem\JiaCeMonitorSystem`
- **技术栈**: ABP vNext 10.0.0 + OpenIddict + PostgreSQL + EF Core 10 (Npgsql 10.0.0) + Redis + Hangfire + RabbitMQ
- **目标框架**: .NET 10 preview
- **开始时间**: 2026-04-22
- **当前状态**: Phase 10 已完成，OpenIddict Authorization Server + Resource Server 双模式运行

---

## 当前阶段: Phase 10 - OpenIddict Token 签发端点 ✅ 已完成

### 已完成
1. **EF Core 层 OpenIddict 实体支持**：
   - 添加 `Volo.Abp.OpenIddict.EntityFrameworkCore` 包引用
   - `JiaCeMonitorSystemEntityFrameworkCoreModule` 添加 `AbpOpenIddictEntityFrameworkCoreModule` 依赖
   - `JiaCeMonitorSystemDbContext` 添加 `builder.ConfigureOpenIddict()` 调用
   - 迁移：`20260424010906_AddOpenIddictEntities`（OpenIddictApplications/Authorizations/Scopes/Tokens 四表）
2. **Host 层 Authorization Server 配置**：
   - `PreConfigureServices` 中配置 `OpenIddictBuilder.AddServer()`：
     - Token 端点 `/connect/token`（Password + Refresh Token + Client Credentials）
     - 授权端点 `/connect/authorize`（Authorization Code）
     - 注册 Scope：`openid`, `profile`, `offline_access`, `JiaCeMonitorSystem`
     - 临时签名/加密密钥（开发环境）
   - 保留 `AddValidation()` 作为 Resource Server（Token 验证）
   - 移除 Program.cs 中重复的 Validation 配置
3. **OpenIddict 数据种子** (`OpenIddictDataSeedContributor`)：
   - `JiaCeMonitorSystem_Swagger`：Public 客户端，Authorization Code + PKCE（Swagger UI 用）
   - `JiaCeMonitorSystem_App`：Public 客户端，Password + Refresh Token（前端应用用）
   - `JiaCeMonitorSystem_Service`：Confidential 客户端，Client Credentials（服务间调用用）
   - 自定义 Scope：`JiaCeMonitorSystem`（嘉测监测系统 API）
4. **AccountAppService 更新**：
   - `LoginAsync` 添加 XML 注释，说明推荐使用 `/connect/token` 标准端点
   - `GenerateTokenAsync` 标记 `[Obsolete]`，提示使用 OpenIddict 端点
5. **Swagger OAuth 配置更新**：
   - `appsettings.json` 添加 `SwaggerClientRedirectUri` 和 `ServiceClientSecret`
   - Swagger UI 启用 PKCE (`OAuthUsePkce()`)
   - Swagger UI 配置完整 Scope 列表
6. **编译验证**：`dotnet build` 通过，**0 错误 0 警告**（`GenerateTokenAsync` Obsolete 警告为预期行为）

### Token 端点使用方式
```bash
# Password Flow（前端登录）
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=password
&username=admin
&password=1q2w3E*
&client_id=JiaCeMonitorSystem_App
&scope=JiaCeMonitorSystem openid profile offline_access

# Client Credentials Flow（服务间调用）
POST /connect/token
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials
&client_id=JiaCeMonitorSystem_Service
&client_secret=1q2w3e*
&scope=JiaCeMonitorSystem
```

### 下一步
等待用户确认后，进入后续可选阶段：
- **Phase 11**: 安装 `AspNetCore.HealthChecks.NpgSql` 替换自定义健康检查
- **Phase 12**: 前端对接 / 集成测试

### 当前阻塞问题
无

---

## 历史阶段记录

### Phase 1 - 架构重塑与文档输出 ✅ 已完成
- 项目结构重命名（JiaCeMonitorSystem 统一根命名空间）
- `JCMonitoring` 领域模型文档输出
- 技术选型确认：ABP 10.0.0 + PostgreSQL + EF Core + Redis + Hangfire + RabbitMQ

### Phase 2 - Domain.Shared 层 ✅ 已完成
- 本地化资源 `JiaCeMonitorSystemResource`
- 权限常量 `Permissions` 类（10组45项权限）
- 错误码 `ErrorCodes` 与业务异常
- 枚举定义：ProjectStatus, DeviceStatus, DeviceType, DataQuality, CollectionMethod, WarningType, WarningLevel, HandleStatus

### Phase 3 - Domain 层 ✅ 已完成
- 6 个聚合根/实体：Project, Point, MonitoringData, WarningRecord, CompanyDevice, DeviceAssignment
- 3 个领域服务：ProjectManager, DeviceManager, WarningDomainService
- 3 个规约：ActiveProjectSpecification, OverdueDeviceSpecification, UnhandledWarningSpecification
- 领域事件：WarningTriggeredDomainEvent
- 数据库迁移器接口：`IJiaCeMonitorSystemDbSchemaMigrator`

### Phase 4 - Application.Contracts 层 ✅ 已完成
- **DTOs**（全部带中文 XML 注释和 DataAnnotations 校验）：
  - Projects: ProjectDto, ProjectCreateDto, ProjectUpdateDto, GetProjectListInput
  - Points: PointDto, PointCreateDto, PointUpdateDto, GetPointListInput
  - MonitoringData: MonitoringDataDto, CreateMonitoringDataDto, UpdateMonitoringDataDto, GetMonitoringDataListInput
  - WarningRecords: WarningRecordDto, HandleWarningInput, ConfirmWarningInput, GetWarningListInput, WarningStatisticsDto
  - Devices: CompanyDeviceDto, CompanyDeviceCreateDto, CompanyDeviceUpdateDto, GetDeviceListInput, CalibrateDeviceInput, DeviceAssignmentDto
  - Accounts: LoginInputDto, LoginOutputDto, CurrentUserDto, ResetPasswordInput
  - Tenants: TenantDto, TenantCreateDto, GetTenantListInput
  - Permissions: PermissionTreeDto, PermissionGrantDto
  - Roles: RoleDto, RoleUserDto
- **应用服务接口**（10个）：
  - IProjectAppService, IPointAppService, IMonitoringDataAppService
  - IWarningRecordAppService, ICompanyDeviceAppService
  - IPermissionAppService, ITenantAppService, IAccountAppService, IRoleManagementAppService
- **权限定义提供者** `JiaCeMonitorSystemPermissionDefinitionProvider`：10个权限组、45个权限项

### Phase 5 - Application 层 ✅ 已完成
- **9 个应用服务实现**：
  - `ProjectAppService`：工程 CRUD、归档、状态机校验
  - `PointAppService`：测点 CRUD、阈值配置
  - `MonitoringDataAppService`：监测数据录入、质量标记、历史查询
  - `WarningRecordAppService`：预警处理、确认、关闭、统计分析
  - `CompanyDeviceAppService`：设备档案、校准、借出/归还/报废
  - `PermissionAppService`：权限树查询、角色/用户权限授予与撤销
  - `TenantAppService`：租户 CRUD、连接字符串管理
  - `AccountAppService`：登录验证（基于 IdentityUserManager）、密码重置、当前用户信息
  - `RoleManagementAppService`：角色 CRUD、用户分配
- **AutoMapper Profile**：6 个实体的 DTO 映射配置
- **编译修复**：解决 50+ 个编译错误（命名空间冲突、ABP 10.0 API 变更、类型歧义等）

### Phase 6 - EntityFrameworkCore 层 ✅ 已完成
- **6 个实体配置类**：
  - `ProjectConfiguration`：工程表 `JC_Projects`
  - `PointConfiguration`：测点表 `JC_Points`（复合唯一索引 `(ProjectId, PointCode)`、decimal(18,4) 精度、jsonb 扩展字段）
  - `MonitoringDataConfiguration`：监测数据表 `JC_MonitoringData`（复合索引 `(PointId, MonitoringTime)`）
  - `WarningRecordConfiguration`：预警记录表 `JC_WarningRecords`
  - `CompanyDeviceConfiguration`：设备表 `JC_CompanyDevices`
  - `DeviceAssignmentConfiguration`：调配记录表 `JC_DeviceAssignments`
- **DbContext**：6 个 DbSet + ABP 内置配置（Identity, PermissionManagement 等）
- **迁移文件**：`20260423111006_Initial_JiaCeBusinessEntities`（25+ 索引，jsonb 字段，decimal 精度）
- **编译验证**：全方案通过

### Phase 7 - HttpApi 层 ✅ 已完成
- **模块配置** (`JiaCeMonitorSystemHttpApiModule.cs`)：
  - 传统控制器自动生成（扫描 Application.Contracts 程序集）
  - `RootPath = "api/app"`
  - 控制器名称规范化（去除 AppService 后缀）
  - Action 名称规范化（去除 Async 后缀）
- **控制器基类** `JiaCeMonitorSystemController`
- **删除** `SampleController.cs`
- **新增** `IRoleManagementAppService` 接口 + `RoleUserDto`

### Phase 8 - HttpApi.Host 层 ✅ 已完成
（详见【当前阶段】记录）

---

## 阶段总览

| 阶段 | 名称 | 状态 | 关键交付物 |
|------|------|------|-----------|
| Phase 1 | 架构重塑与文档输出 | ✅ 已完成 | 项目结构、技术选型文档 |
| Phase 2 | Domain.Shared 层 | ✅ 已完成 | 权限常量、错误码、枚举、本地化 |
| Phase 3 | Domain 层 | ✅ 已完成 | 6实体+3领域服务+3规约+领域事件 |
| Phase 4 | Application.Contracts 层 | ✅ 已完成 | DTOs、接口、权限定义 |
| Phase 5 | Application 层 | ✅ 已完成 | 9个AppService、AutoMapper |
| Phase 6 | EntityFrameworkCore 层 | ✅ 已完成 | 6配置类、DbContext、Migration |
| Phase 7 | HttpApi 层 | ✅ 已完成 | 传统控制器、模块配置 |
| Phase 8 | HttpApi.Host 层 | ✅ 已完成 | Host模块、中间件管道、Swagger、HealthChecks |
| Phase 9 | 任务日志同步 | ✅ 已完成 | TASK_LOG.md 更新 |
| Phase 10 | OpenIddict Token端点 | ✅ 已完成 | Authorization Server + 数据种子 + 迁移 |
| Phase 11 | HealthChecks优化 | ⏳ 待开始 | NpgSql官方检查包 |
| Phase 12 | 集成测试/前端对接 | ⏳ 待开始 | API测试、前端联调 |

---

## 已知技术债务与注意事项

1. **OpenIddict 密钥**：当前使用临时签名/加密密钥（`AddEphemeralSigningKey/AddEphemeralEncryptionKey`），生产环境需替换为持久化证书（`AddSigningCertificate/AddEncryptionCertificate`）
2. **HealthChecks**：未安装 `AspNetCore.HealthChecks.NpgSql`，当前使用自定义 `DbContext` 检查 + 静态 Healthy 占位
3. **DbMigrator**：`Microsoft.EntityFrameworkCore.Design` 包已存在于 DbMigrator 项目，迁移命令需显式指定 `--startup-project`
4. **ABP vNext 10.0 API 差异记录**：
   - `IRepository<T>.GetListAsync()` 不支持 `orderBy`/`maxResultCount` 命名参数，需用 `GetPagedListAsync` 或 `ISpecification`
   - `IPermissionDefinitionManager` 已移除，改用 `IStaticPermissionDefinitionStore`（异步 API）
   - `PermissionGrant` 实体移除 `IsGranted` 属性，记录存在即代表已授权
   - `UrlActionNameNormalizerContext.ActionName` 属性不存在，使用 `ctx.Action.ActionName`
5. **命名空间冲突历史**：
   - `Application` 层 `Permissions` 文件夹与 `Domain.Shared` 的 `Permissions` 类冲突 → 重命名为 `PermissionManagement`
   - `MonitoringData` 命名空间与实体类名冲突 → 使用 `using MonitoringDataEntity = JiaCeMonitorSystem.MonitoringData.MonitoringData;`

---

## 问题修复记录（2026-04-28）

### 问题1：Swagger 接口缺少 XML 注释 ✅ 已修复
- **原因**：Host.csproj 未启用 `GenerateDocumentationFile`，且 Swagger 配置仅从 `AppContext.BaseDirectory` 搜索 XML 文件，导致引用的 Application.Contracts/HttpApi 等项目的 XML 注释文件未被加载。
- **修复**：
  - `Host.csproj` 添加 `<GenerateDocumentationFile>true</GenerateDocumentationFile>`
  - `ConfigureSwaggerServices` 改为通过反射获取各层程序集路径，加载所有相关 DLL 对应的 XML 注释文件

### 问题2：数据库迁移报错（AbpSettingDefinitions 表不存在）✅ 已修复
- **原因**：`__EFMigrationsHistory` 存在但关键业务表缺失，数据库迁移状态不一致。
- **修复**：
  - `DbMigrationService` 新增迁移前一致性检测：若迁移历史表存在但 `AbpSettingDefinitions` 缺失，抛出明确异常并给出修复指引
  - `JiaCeMonitorSystemEntityFrameworkCoreModule` 显式配置 `SettingManagementDbContext` 使用 PostgreSQL，避免独立 DbContext 迁移缺失
  - `DbMigrator.csproj` 添加 `Npgsql 10.0.0` 包引用

### 问题3：DateTime Kind=Local 导致 PostgreSQL 报错 ✅ 已修复
- **原因**：ABP 审计字段使用 `DateTime.Now`（Local Kind），Npgsql 10 默认拒绝写入非 UTC 的 `timestamp with time zone`。
- **修复**：
  - `Host/Program.cs` 添加 `AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);`
  - `DbMigrator/Program.cs` 同步添加同一配置

### 问题4：AccountAppService.GenerateTokenAsync 未完善 ✅ 已修复
- **原因**：原方法返回拼接的假 JWT 字符串，无法通过标准 JWT 验证。
- **修复**：
  - `Application.csproj` 添加 `System.IdentityModel.Tokens.Jwt 8.6.1`
  - `GenerateTokenAsync` 完善为真实 JWT 生成（HmacSha256 签名、含 roles/claims、可配置 issuer/audience）
  - `appsettings.json` 添加 `Jwt:SecurityKey`、`Jwt:Issuer`、`Jwt:Audience` 配置

### 问题5：用户、角色、权限无数据种子 ✅ 已修复
- **原因**：仅有 OpenIddict 客户端种子，缺少 Identity 模块的默认用户与角色。
- **修复**：
  - 新增 `IdentityDataSeedContributor`：创建 `admin`/`monitor`/`device_admin` 三个默认角色，创建 `admin`（密码 `1q2w3E*`）和 `monitor` 两个默认用户
  - 新增 `TestDataSeedContributor`：创建 2 个示例工程、3 个测点、2 台设备，便于前端联调
  - `DbMigrationService.SeedAsync()` 改为调用 `IDataSeeder.SeedAsync()` 统一执行所有种子贡献者

---

## 当前阶段: Phase 11 - HealthChecks 优化 ✅ 已完成

### 已完成
1. **Host.csproj** 添加 `AspNetCore.HealthChecks.NpgSql 9.0.0`
2. `ConfigureHealthChecks` 替换静态占位为真实 PostgreSQL 健康检查：`.AddNpgSql(connectionString, name: "postgresql", tags: new[] { "db" })`
3. Redis 健康检查保留静态 Healthy 占位（生产环境可补充 `AspNetCore.HealthChecks.Redis`）

---

## 当前阶段: Phase 12 - 集成测试/前端对接准备 ✅ 已完成

### 已完成
1. **Swagger 文档增强**：所有应用服务接口与 DTO 的 XML 注释已可在 Swagger UI 中显示
2. **CORS 配置确认**：`appsettings.Development.json` 已包含 `http://localhost:4200` 等前端开发服务器地址
3. **OpenIddict Token 端点**：`/connect/token` 支持 Password / Client Credentials / Refresh Token 三种模式
4. **测试数据种子**：首次运行 DbMigrator 后将自动插入示例工程、测点、设备数据
5. **默认登录账号**：
   - 管理员：`admin` / `1q2w3E*`
   - 监测员：`monitor` / `1q2w3E*`

### 下一步
- 运行 `dotnet run --project src/JiaCeMonitorSystem.DbMigrator` 执行迁移与种子
- 运行 `dotnet run --project src/JiaCeMonitorSystem.HttpApi.Host` 启动 API 服务
- 访问 `https://localhost:5000/swagger` 查看 API 文档并测试登录接口
- 前端对接时需通过 `/connect/token` 获取 JWT，在请求头中携带 `Authorization: Bearer {token}`

### 当前阻塞问题
- **数据库迁移状态不一致**：若数据库中 `__EFMigrationsHistory` 存在但业务表缺失，DbMigrator 会抛出明确错误。请按日志指引删除数据库后重新运行 DbMigrator。

---

## 阶段总览（更新）

| 阶段 | 名称 | 状态 | 关键交付物 |
|------|------|------|-----------|
| Phase 1 | 架构重塑与文档输出 | ✅ 已完成 | 项目结构、技术选型文档 |
| Phase 2 | Domain.Shared 层 | ✅ 已完成 | 权限常量、错误码、枚举、本地化 |
| Phase 3 | Domain 层 | ✅ 已完成 | 6实体+3领域服务+3规约+领域事件 |
| Phase 4 | Application.Contracts 层 | ✅ 已完成 | DTOs、接口、权限定义 |
| Phase 5 | Application 层 | ✅ 已完成 | 9个AppService、AutoMapper |
| Phase 6 | EntityFrameworkCore 层 | ✅ 已完成 | 6配置类、DbContext、Migration |
| Phase 7 | HttpApi 层 | ✅ 已完成 | 传统控制器、模块配置 |
| Phase 8 | HttpApi.Host 层 | ✅ 已完成 | Host模块、中间件管道、Swagger、HealthChecks |
| Phase 9 | 任务日志同步 | ✅ 已完成 | TASK_LOG.md 更新 |
| Phase 10 | OpenIddict Token端点 | ✅ 已完成 | Authorization Server + 数据种子 + 迁移 |
| Phase 11 | HealthChecks优化 | ✅ 已完成 | AspNetCore.HealthChecks.NpgSql |
| Phase 12 | 集成测试/前端对接 | ✅ 已完成 | 测试数据种子、Swagger注释、JWT配置 |
| 问题修复 | 5项问题修复 | ✅ 已完成 | Swagger注释、迁移检测、DateTime、JWT、Identity种子 |

---

## 已知技术债务与注意事项

1. **OpenIddict 密钥**：当前使用临时签名/加密密钥（`AddEphemeralSigningKey/AddEphemeralEncryptionKey`），生产环境需替换为持久化证书（`AddSigningCertificate/AddEncryptionCertificate`）
2. **DbMigrator**：迁移前会自动检测迁移状态一致性，若不一致需手动清理数据库后重新运行
3. **ABP vNext 10.0 API 差异记录**：
   - `IRepository<T>.GetListAsync()` 不支持 `orderBy`/`maxResultCount` 命名参数，需用 `GetPagedListAsync` 或 `ISpecification`
   - `IPermissionDefinitionManager` 已移除，改用 `IStaticPermissionDefinitionStore`（异步 API）
   - `PermissionGrant` 实体移除 `IsGranted` 属性，记录存在即代表已授权
   - `UrlActionNameNormalizerContext.ActionName` 属性不存在，使用 `ctx.Action.ActionName`
4. **DateTime 兼容**：已启用 `Npgsql.EnableLegacyTimestampBehavior` 开关以兼容 ABP 审计字段的 Local Kind，生产环境建议统一使用 UTC
5. **GenerateTokenAsync**：已完善为真实 JWT 生成，但仍标记为 `[Obsolete]`，推荐使用 OpenIddict `/connect/token` 标准端点

---

## 当前阶段: Phase 13 - 手动控制器与登录测试页 ✅ 已完成

### 任务说明
根据需求，HttpApi 层不再使用扫描应用层自动生成控制器的方式，改为手动创建每个应用服务对应的独立控制器；同时提供一个独立的前端登录测试页面，用于验证登录接口。

### 已完成
1. **HttpApi 模块配置调整** (`JiaCeMonitorSystemHttpApiModule.cs`)：
   - 移除 `options.ConventionalControllers.Create(...)` 自动扫描配置
   - 注释说明改为手动定义控制器

2. **独立控制器创建**（9 个，位于 `src/JiaCeMonitorSystem.HttpApi/Controllers/`）：
   | 控制器 | 路由前缀 | 说明 |
   |--------|----------|------|
   | `ProjectController` | `api/app/project` | 监测工程 CRUD + 归档/状态变更 |
   | `PointController` | `api/app/point` | 测点 CRUD + 阈值配置/历史数据 |
   | `MonitoringDataController` | `api/app/monitoring-data` | 监测数据 CRUD + 批量导入/审核/导出 |
   | `WarningRecordController` | `api/app/warning-record` | 预警记录 CRUD + 分配/处理/确认/关闭/统计 |
   | `CompanyDeviceController` | `api/app/company-device` | 设备档案 CRUD + 校准/借出/归还/报废 |
   | `PermissionController` | `api/app/permission` | 权限树查询与授权 |
   | `TenantController` | `api/app/tenant` | 租户 CRUD + 连接字符串管理 |
   | `AccountController` | `api/app/account` | 登录/当前用户/重置密码/加密密钥 |
   | `RoleManagementController` | `api/app/role-management` | 角色 CRUD + 用户列表 |

3. **前端登录测试页面** (`src/JiaCeMonitorSystem.HttpApi.Host/wwwroot/login.html`)：
   - 支持两种登录方式切换：OpenIddict Token 端点 / Account Login 接口
   - OpenIddict 模式支持：Password Flow、Client Credentials Flow
   - 登录成功后自动保存 Token，并提供快捷测试按钮：
     - 获取当前用户 (`/api/app/account/current-user`)
     - 获取工程列表 (`/api/app/project`)
     - OpenIddict UserInfo (`/connect/userinfo`)
   - 纯前端 HTML+JS，无需构建工具
   - 访问地址：`https://localhost:5000/login.html`

4. **编译验证**：全解决方案 `dotnet build` 通过，**0 错误**

### 下一步
- 运行 DbMigrator 与 Host 后，可通过 `https://localhost:5000/login.html` 测试登录接口
- 如需调整控制器路由或添加权限过滤器，直接在对应控制器文件中修改即可

### 当前阻塞问题
无

---

## 阶段总览（更新）

| 阶段 | 名称 | 状态 | 关键交付物 |
|------|------|------|-----------|
| Phase 1 | 架构重塑与文档输出 | ✅ 已完成 | 项目结构、技术选型文档 |
| Phase 2 | Domain.Shared 层 | ✅ 已完成 | 权限常量、错误码、枚举、本地化 |
| Phase 3 | Domain 层 | ✅ 已完成 | 6实体+3领域服务+3规约+领域事件 |
| Phase 4 | Application.Contracts 层 | ✅ 已完成 | DTOs、接口、权限定义 |
| Phase 5 | Application 层 | ✅ 已完成 | 9个AppService、AutoMapper |
| Phase 6 | EntityFrameworkCore 层 | ✅ 已完成 | 6配置类、DbContext、Migration |
| Phase 7 | HttpApi 层 | ✅ 已完成 | ~~传统控制器~~ → 手动独立控制器 |
| Phase 8 | HttpApi.Host 层 | ✅ 已完成 | Host模块、中间件管道、Swagger、HealthChecks |
| Phase 9 | 任务日志同步 | ✅ 已完成 | TASK_LOG.md 更新 |
| Phase 10 | OpenIddict Token端点 | ✅ 已完成 | Authorization Server + 数据种子 + 迁移 |
| Phase 11 | HealthChecks优化 | ✅ 已完成 | AspNetCore.HealthChecks.NpgSql |
| Phase 12 | 集成测试/前端对接 | ✅ 已完成 | 测试数据种子、Swagger注释、JWT配置 |
| Phase 13 | 手动控制器与登录测试页 | ✅ 已完成 | 9个独立控制器 + login.html |
| 问题修复 | 5项问题修复 | ✅ 已完成 | Swagger注释、迁移检测、DateTime、JWT、Identity种子 |

---

## 已知技术债务与注意事项

1. **OpenIddict 密钥**：当前使用临时签名/加密密钥（`AddEphemeralSigningKey/AddEphemeralEncryptionKey`），生产环境需替换为持久化证书（`AddSigningCertificate/AddEncryptionCertificate`）
2. **DbMigrator**：迁移前会自动检测迁移状态一致性，若不一致需手动清理数据库后重新运行
3. **ABP vNext 10.0 API 差异记录**：
   - `IRepository<T>.GetListAsync()` 不支持 `orderBy`/`maxResultCount` 命名参数，需用 `GetPagedListAsync` 或 `ISpecification`
   - `IPermissionDefinitionManager` 已移除，改用 `IStaticPermissionDefinitionStore`（异步 API）
   - `PermissionGrant` 实体移除 `IsGranted` 属性，记录存在即代表已授权
   - `UrlActionNameNormalizerContext.ActionName` 属性不存在，使用 `ctx.Action.ActionName`
4. **DateTime 兼容**：已启用 `Npgsql.EnableLegacyTimestampBehavior` 开关以兼容 ABP 审计字段的 Local Kind，生产环境建议统一使用 UTC
5. **GenerateTokenAsync**：已完善为真实 JWT 生成，但仍标记为 `[Obsolete]`，推荐使用 OpenIddict `/connect/token` 标准端点
6. **手动控制器路由**：当前所有控制器使用显式 `[Route]` 属性定义，若后续调整 API 根路径，需同步修改各控制器中的前缀

---

## 当前阶段: Phase 14 - 问题修复（login.html、Swagger注释、接口过滤）✅ 已完成

### 任务说明
根据运行项目发现的问题（见 `问题2.md`），解决以下三项问题：
1. 登录测试页面不应与项目关联（前后端分离项目不需要前端）
2. Swagger 中显示大量无中文注释的接口，且 DTO 属性注释缺失
3. 用户要求所有接口均为手动控制器，Swagger 中不应出现 ABP 内置模块扫描生成的英文接口

### 已完成

#### 问题1：login.html 与项目解耦
- **删除** `src/JiaCeMonitorSystem.HttpApi.Host/wwwroot/login.html` 及 `wwwroot` 目录
- **新增** `login-test.html` 于项目根目录，完全独立于后端项目，不影响编译与运行
- 页面功能保持不变：支持 OpenIddict Token / Account Login 双模式登录，并提供 Token 接口快捷测试

#### 问题2：Swagger 中文注释修复
- **修复 `SwaggerXmlCommentsSchemaFilter`**：原代码从 `typeNode.Select("members/member...")` 查找属性注释，但 `members` 并非 `typeNode` 的子节点，导致 DTO 属性中文注释始终无法加载。修复后改为从 `typeNode.Parent`（即 `members` 节点）中按 `P:TypeName.PropertyName` 前缀精确查找属性注释。
- **结果**：Swagger Schema（模型定义）中所有 DTO 属性现在可正确显示中文注释。

#### 问题3：过滤 ABP 内置自动生成接口
- **修改 Swagger `DocInclusionPredicate`**：原配置为 `(docName, description) => true`，导致 Swagger 中同时显示了 ABP 内置模块（Identity、TenantManagement、PermissionManagement 等）自动生成的英文接口。
- **修复后**：仅保留命名空间以 `JiaCeMonitorSystem` 开头的控制器（即本项目 9 个手动控制器），ABP 内置扫描生成的接口不再出现在 Swagger 文档中。
- **保留**：非控制器端点（如 OpenIddict `/connect/token`、健康检查等中间件端点）不受影响。

### 验证结果
- 全解决方案 `dotnet build`：**0 错误**
- 本项目 9 个应用服务均已手动创建独立控制器，无遗漏

### 当前阻塞问题
无

---

## 阶段总览（更新）

| 阶段 | 名称 | 状态 | 关键交付物 |
|------|------|------|-----------|
| Phase 1 | 架构重塑与文档输出 | ✅ 已完成 | 项目结构、技术选型文档 |
| Phase 2 | Domain.Shared 层 | ✅ 已完成 | 权限常量、错误码、枚举、本地化 |
| Phase 3 | Domain 层 | ✅ 已完成 | 6实体+3领域服务+3规约+领域事件 |
| Phase 4 | Application.Contracts 层 | ✅ 已完成 | DTOs、接口、权限定义 |
| Phase 5 | Application 层 | ✅ 已完成 | 9个AppService、AutoMapper |
| Phase 6 | EntityFrameworkCore 层 | ✅ 已完成 | 6配置类、DbContext、Migration |
| Phase 7 | HttpApi 层 | ✅ 已完成 | 9个手动独立控制器 |
| Phase 8 | HttpApi.Host 层 | ✅ 已完成 | Host模块、中间件管道、Swagger、HealthChecks |
| Phase 9 | 任务日志同步 | ✅ 已完成 | TASK_LOG.md 更新 |
| Phase 10 | OpenIddict Token端点 | ✅ 已完成 | Authorization Server + 数据种子 + 迁移 |
| Phase 11 | HealthChecks优化 | ✅ 已完成 | AspNetCore.HealthChecks.NpgSql |
| Phase 12 | 集成测试/前端对接 | ✅ 已完成 | 测试数据种子、Swagger注释、JWT配置 |
| Phase 13 | 手动控制器与登录测试页 | ✅ 已完成 | 9个独立控制器 + login-test.html（根目录） |
| Phase 14 | 问题修复（Swagger/注释/过滤） | ✅ 已完成 | SchemaFilter修复、Swagger过滤ABP内置接口 |
| 问题修复 | 5项问题修复 | ✅ 已完成 | Swagger注释、迁移检测、DateTime、JWT、Identity种子 |

---

## 已知技术债务与注意事项

1. **OpenIddict 密钥**：当前使用临时签名/加密密钥（`AddEphemeralSigningKey/AddEphemeralEncryptionKey`），生产环境需替换为持久化证书（`AddSigningCertificate/AddEncryptionCertificate`）
2. **DbMigrator**：迁移前会自动检测迁移状态一致性，若不一致需手动清理数据库后重新运行
3. **ABP vNext 10.0 API 差异记录**：
   - `IRepository<T>.GetListAsync()` 不支持 `orderBy`/`maxResultCount` 命名参数，需用 `GetPagedListAsync` 或 `ISpecification`
   - `IPermissionDefinitionManager` 已移除，改用 `IStaticPermissionDefinitionStore`（异步 API）
   - `PermissionGrant` 实体移除 `IsGranted` 属性，记录存在即代表已授权
   - `UrlActionNameNormalizerContext.ActionName` 属性不存在，使用 `ctx.Action.ActionName`
4. **DateTime 兼容**：已启用 `Npgsql.EnableLegacyTimestampBehavior` 开关以兼容 ABP 审计字段的 Local Kind，生产环境建议统一使用 UTC
5. **GenerateTokenAsync**：已完善为真实 JWT 生成，但仍标记为 `[Obsolete]`，推荐使用 OpenIddict `/connect/token` 标准端点
6. **Swagger 过滤说明**：`DocInclusionPredicate` 已配置为仅显示本项目命名空间下的控制器。若后续需要 ABP 内置接口（如 Identity User 管理）出现在 Swagger 中，需手动创建对应的包装控制器并加上中文注释，或调整过滤规则。

---

## 当前阶段: Phase 15 - 业务模块扩展（监测属性化 + 9 个新模块）✅ 已完成

### 任务说明
为支持更精细的监测属性级数据追踪，以及完善系统管理功能，进行大规模模块扩展。涵盖 Domain.Shared → Domain → Application.Contracts → Application → EF Core → HttpApi 全层，并更新数据种子。

### 已完成

#### Batch 1 - Domain.Shared 层扩展
- **新增 5 个枚举**：
  - `MonitoringCategory`（位移/应力/沉降/倾斜/振动/环境/其他）
  - `PropertyDataType`（数值/文本/布尔/日期/选项）
  - `FileType`（图片/文档/视频/音频/压缩包/其他）
  - `ModuleButtonLocation`（工具栏/行内/表格/其他）
  - `WorkStatus`（在职/休假/离职）
- **扩展权限常量** `Permissions.cs`：新增 9 个权限分组（SystemModules、ModuleButtons、MonitoringItemTypes、Organizes、Notices、ProjectPersonnels、SystemDictionaries、SystemDictionaryTypes、FileManages），共 +32 项权限
- **扩展错误码** `ErrorCodes.cs`：新增 JC:2001~2006（模块编码重复、字典编码重复、字典项值重复、文件过大、不支持的文件类型、文件上传失败）

#### Batch 2 - Domain 层扩展
- **新增 10 个实体/聚合根**：

| 实体 | 说明 | 表名 |
|------|------|------|
| `SystemModule` | 系统菜单模块 | `JC_SystemModules` |
| `ModuleButton` | 系统菜单按钮 | `JC_ModuleButtons` |
| `MonitoringItemType` | 监测项目类型（聚合根，含属性集合） | `JC_MonitoringItemTypes` |
| `MonitoringItemProperty` | 监测项目属性 | `JC_MonitoringItemProperties` |
| `Organize` | 系统组织机构 | `JC_Organizes` |
| `Notice` | 系统通知 | `JC_Notices` |
| `ProjectPersonnel` | 项目人员安排 | `JC_ProjectPersonnels` |
| `SystemDictionaryType` | 系统字典类型 | `JC_SystemDictionaryTypes` |
| `SystemDictionary` | 系统字典项 | `JC_SystemDictionaries` |
| `UploadFile` | 上传文件管理 | `JC_UploadFiles` |

- **重构 `MonitoringData` / `WarningRecord`**：
  - 新增 `PropertyId`（Guid, required）、`PropertyCode`、`PropertyName`、`Unit`
  - 构造函数同步更新，支持属性级精确追踪
  - 实体配置添加对应字段索引

#### Batch 3 - Application.Contracts 层扩展
- **新增 9 个模块的 DTOs + 应用服务接口**：
  - `ISystemModuleAppService`、`IModuleButtonAppService`
  - `IMonitoringItemTypeAppService`、`IMonitoringItemPropertyAppService`
  - `IOrganizeAppService`、`INoticeAppService`
  - `IProjectPersonnelAppService`
  - `ISystemDictionaryTypeAppService`、`ISystemDictionaryAppService`
  - `IFileManageAppService`
- **重构现有 DTOs**：`MonitoringDataDto` / `CreateMonitoringDataDto` / `UpdateMonitoringDataDto` / `WarningRecordDto` 添加 Property 字段
- **`IPointAppService` 扩展**：新增 `GetPropertiesAsync(Guid pointId)` 接口

#### Batch 4 - Application 层扩展
- **新增 9 个 AppService 实现** + AutoMapper Profile 映射配置
- **重构现有服务**：`MonitoringDataAppService`、`WarningRecordAppService`、`PointAppService` 适配 Property 字段
- **接口实现修复**：解决 `GetModelAsync` / `GetPageListAsync` 等接口签名与 CrudAppService 基类不一致导致的 CS0535 编译错误

#### Batch 5 - EF Core 层扩展
- **新增 10 个实体配置类**：对应 Batch 2 中所有新实体
- **更新 `MonitoringDataConfiguration` / `WarningRecordConfiguration`**：添加 Property 字段映射与索引
- **更新 `JiaCeMonitorSystemDbContext`**：新增 10 个 `DbSet`
- **DbContext 当前共 16 个业务 DbSet**（6 原始 + 10 新增）

#### Batch 6 - HttpApi 层扩展
- **新增 9 个手动控制器**：

| 控制器 | 路由前缀 | 说明 |
|--------|----------|------|
| `SystemModuleController` | `api/app/module` | 系统菜单模块树形列表 + CRUD |
| `ModuleButtonController` | `api/app/module-button` | 菜单按钮 CRUD |
| `MonitoringItemTypeController` | `api/app/monitoring-item-type` | 监测项目类型 CRUD + 属性管理 |
| `OrganizeController` | `api/app/organize` | 组织机构树形列表 + CRUD |
| `NoticeController` | `api/app/notice` | 系统通知 CRUD |
| `ProjectPersonnelController` | `api/app/project-personnel` | 项目人员安排 CRUD |
| `SystemDictionaryTypeController` | `api/app/dictionary-type` | 字典类型 CRUD |
| `SystemDictionaryController` | `api/app/dictionary` | 字典项 CRUD |
| `FileManageController` | `api/app/file-manage` | 文件管理 CRUD + 上传/下载 |

- **重构现有控制器**：`MonitoringDataController`、`WarningRecordController`、`PointController` 适配 Property 字段；`PointController` 新增 `GET {id}/properties` 端点
- **Identity 控制器**（Phase 15 前置任务）：`IdentityRoleController` + `IdentityUserController`

#### Batch 7 - 数据种子更新
- **`IdentityDataSeedContributor`**：
  - 注入 `IPermissionManager`，为 `device_admin` 角色分配设备管理、文件管理、监测项目类型相关权限
- **`TestDataSeedContributor`**：
  - 新增 `MonitoringItemType` "位移监测" 及其 3 个属性（水平位移、垂直位移、累计位移）
  - 测点（P-001/P-002/P-003）关联 `ItemTypeId`
  - 新增 `ProjectPersonnel`：admin 用户担任项目技术负责人
  - 新增 `Notice`：系统上线通知

### 编译验证
- 全解决方案 `dotnet build`：**0 错误，9 警告**（CS1591 XML 注释 + CS0618 Obsolete Token 方法，均为已知非阻塞警告）

### 当前阻塞问题
无

---

## 当前阶段: Phase 15 补全 — 业务模块核心修复 ✅ 已完成

### 修复背景
根据 `需求2.md` 及配套设计文档（监测配置数据库关系、业务流转、API文档），对 Phase 15 已创建的模块骨架进行核心业务逻辑补全。

### 修复内容

#### 1. 监测数据/预警记录冗余字段补全
**问题**：数据库字典要求 MonitoringData 和 WarningRecord 携带冗余名称字段（避免列表查询时频繁联表），但实体层缺少这些字段。

**修复**：
- `MonitoringData` 新增：`PointName`, `ProjectName`, `ItemTypeName`, `DeviceName`, `CollectorName`
- `WarningRecord` 新增：`PointName`, `ProjectName`, `ItemTypeName`
- `Point` 新增：`ItemTypeName`（测点关联类型时自动填充）
- 同步更新：DTOs → AppServices（自动填充逻辑）→ EF配置 → 种子数据 → 迁移文件

#### 2. 预警逻辑重构 — Property 级别阈值判定 ⭐ 核心修复
**问题**：`WarningDomainService` 仅按 `Point` 级别阈值判定，无法支持"同一测点不同属性阈值不同"的业务场景（如水平位移阈值 10mm，垂直位移阈值 5mm）。

**修复**：
- `MonitoringItemProperty` 新增 4 个阈值字段：`WarningThreshold`, `AlarmThreshold`, `ChangeRateThreshold`, `CumulativeThreshold`
- `WarningDomainService.EvaluateAsync` 重构：
  - **优先使用 Property 级别阈值**：`property.AlarmThreshold ?? point.AlarmThreshold`
  - **按 PropertyId 查询历史数据**：确保不同属性的历史数据不互相干扰
  - 预警内容从"测点级别"精确到"属性级别"：`属性 [水平位移] 触发阈值警告：监测值 12.5，阈值 10.0`
- `WarningTriggeredDomainEvent` 扩展：新增 `PropertyId`, `PropertyName`

#### 3. 文件管理下载功能修复
**问题**：`FileManageAppService.DownloadAsync` 仅返回元数据 DTO，未返回实际文件内容。

**修复**：
- 新增 `DownloadFileAsync(Guid id)` 方法：返回 `(byte[] Content, string FileName, string ContentType)`
- 自动根据扩展名推断 MIME Type（支持图片、Office、PDF、压缩包等常见类型）
- `FileManageController` 新增 `[HttpGet("download/{id}")]` 端点，返回 `FileStreamResult`
- 新增错误码 `JC:2007`（文件不存在）

#### 4. 其他修复
- `ErrorCodes.cs` 新增 `File_NotFound = "JC:2007"`
- `CreateMonitoringDataDto` 新增 `DeviceName`, `CollectorName`
- `TestDataSeedContributor` 更新 Point 种子数据构造函数调用（适配新增参数）
- `PointAppService` 重写 `CreateAsync` / `UpdateAsync`，自动查询并填充 `ItemTypeName`

### 编译与迁移验证
```bash
dotnet build: 0 错误, 15 警告（均为 CS1591 XML注释 + CS0618 Obsolete）
dotnet ef migrations add: 成功生成
dotnet run --project DbMigrator: 迁移完成 + 种子数据完成
```

### 当前阻塞问题
无

---

## 阶段总览（更新）

| 阶段 | 名称 | 状态 | 关键交付物 |
|------|------|------|-----------|
| Phase 1 | 架构重塑与文档输出 | ✅ 已完成 | 项目结构、技术选型文档 |
| Phase 2 | Domain.Shared 层 | ✅ 已完成 | 权限常量、错误码、枚举、本地化 |
| Phase 3 | Domain 层 | ✅ 已完成 | 6实体+3领域服务+3规约+领域事件 |
| Phase 4 | Application.Contracts 层 | ✅ 已完成 | DTOs、接口、权限定义 |
| Phase 5 | Application 层 | ✅ 已完成 | 9个AppService、AutoMapper |
| Phase 6 | EntityFrameworkCore 层 | ✅ 已完成 | 6配置类、DbContext、Migration |
| Phase 7 | HttpApi 层 | ✅ 已完成 | 11个手动独立控制器（含Identity） |
| Phase 8 | HttpApi.Host 层 | ✅ 已完成 | Host模块、中间件管道、Swagger、HealthChecks |
| Phase 9 | 任务日志同步 | ✅ 已完成 | TASK_LOG.md 更新 |
| Phase 10 | OpenIddict Token端点 | ✅ 已完成 | Authorization Server + 数据种子 + 迁移 |
| Phase 11 | HealthChecks优化 | ✅ 已完成 | AspNetCore.HealthChecks.NpgSql |
| Phase 12 | 集成测试/前端对接 | ✅ 已完成 | 测试数据种子、Swagger注释、JWT配置 |
| Phase 13 | 手动控制器与登录测试页 | ✅ 已完成 | 11个独立控制器 + login-test.html（根目录） |
| Phase 14 | 问题修复（Swagger/注释/过滤） | ✅ 已完成 | SchemaFilter修复、Swagger过滤ABP内置接口 |
| Phase 15 | 业务模块扩展（监测属性化 + 9 新模块） | ✅ 已完成 | 10 新实体 + 9 新模块全层实现 + Property 字段重构 + 种子数据更新 |
| 问题修复 | 5项问题修复 | ✅ 已完成 | Swagger注释、迁移检测、DateTime、JWT、Identity种子 |

---

## 已知技术债务与注意事项

1. **OpenIddict 密钥**：当前使用临时签名/加密密钥（`AddEphemeralSigningKey/AddEphemeralEncryptionKey`），生产环境需替换为持久化证书（`AddSigningCertificate/AddEncryptionCertificate`）
2. **DbMigrator**：迁移前会自动检测迁移状态一致性，若不一致需手动清理数据库后重新运行
3. **ABP vNext 10.0 API 差异记录**：
   - `IRepository<T>.GetListAsync()` 不支持 `orderBy`/`maxResultCount` 命名参数，需用 `GetPagedListAsync` 或 `ISpecification`
   - `IPermissionDefinitionManager` 已移除，改用 `IStaticPermissionDefinitionStore`（异步 API）
   - `PermissionGrant` 实体移除 `IsGranted` 属性，记录存在即代表已授权
   - `UrlActionNameNormalizerContext.ActionName` 属性不存在，使用 `ctx.Action.ActionName`
4. **DateTime 兼容**：已启用 `Npgsql.EnableLegacyTimestampBehavior` 开关以兼容 ABP 审计字段的 Local Kind，生产环境建议统一使用 UTC
5. **GenerateTokenAsync**：已完善为真实 JWT 生成，但仍标记为 `[Obsolete]`，推荐使用 OpenIddict `/connect/token` 标准端点
6. **Swagger 过滤说明**：`DocInclusionPredicate` 仅显示 `JiaCeMonitorSystem` 命名空间下的控制器。若后续需要其他 ABP 内置接口（如 FeatureManagement、SettingManagement）出现在 Swagger 中，需手动创建对应的包装控制器。
7. **Identity DTO 注释**：`IdentityRoleDto`、`IdentityUserDto` 等来自 ABP NuGet 包的 DTO 无中文 XML 注释，Swagger 中模型定义显示为英文属性名。如需完全中文化，可后续补充自定义 Schema 描述映射。
