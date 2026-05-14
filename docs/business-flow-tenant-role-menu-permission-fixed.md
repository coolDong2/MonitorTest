# JiaCeMonitorSystem 租户·角色·菜单·权限 综合业务流程图

> 本文档汇总 `feature/tenant-reform-phase7` 分支中四大核心模块的完整交互流程，涵盖 API 接口、DTO 字段、数据表及前端接入方式。

---

## 一、租户管理（Tenant Management）

### 1.1 创建租户（SaaS 配置版）

```mermaid
sequenceDiagram
    autonumber
    actor F as 前端/管理后台
    participant TC as TenantConfigurationController<br/>POST /api/app/tenant-configuration
    participant TS as TenantConfigurationAppService
    participant TM as TenantManager (ABP)
    participant TDM as TenantDatabaseManager
    participant DS as DataSeeder
    participant DB as PostgreSQL

    F->>TC: CreateAsync(CreateTenantWithConfigDto)<br/>{Name, UnitCode, AdminEmail, AdminPassword,<br/>UseIndependentDatabase, GrantedModuleIds, License}
    TC->>TS: CreateAsync(input)
    TS->>TS: 校验 UnitCode 唯一性
    TS->>TM: CreateAsync(input.Name)
    TM-->>TS: tenant
    alt UseIndependentDatabase == true
        TS->>TDM: CreateDatabaseAsync(tenant.Id, input.Name)
        TDM->>DB: CREATE DATABASE jcmonitor_tenant_{tenantId:N}
        TDM->>DB: Database.MigrateAsync()
        TDM->>DB: DataSeeder.SeedAsync(tenantId)
        TDM-->>TS: connectionString
        TS->>TS: 加密 → config.IndependentConnectionString
    end
    TS->>DB: INSERT INTO JC_TenantConfigurations<br/>(TenantId, UnitCode, Status, License, IsIndependentDatabase)
    loop 遍历 GrantedModuleIds
        TS->>DB: INSERT INTO JC_TenantModuleGrants<br/>(TenantId, ModuleId, IsGranted, GrantDate)
    end
    alt AdminEmail & AdminPassword 不为空
        TS->>TS: CurrentTenant.Change(tenant.Id)
        TS->>DB: INSERT INTO AbpUsers<br/>(UserName=AdminEmail, Name="管理员", TenantId)
        Note over TS,DB: 触发 EntityCreatedEventData(IdentityUser)<br/>自动创建 TenantUserExtension
    end
    TS->>TS: 发布 TenantInitializedEvent
    TS-->>TC: TenantConfigurationDto
    TC-->>F: 201 Created
```

### 1.2 独立数据库切换（含 SignalR 实时推送）

```mermaid
sequenceDiagram
    autonumber
    actor F as 前端
    participant TC as TenantConfigurationController<br/>POST /api/app/tenant-configuration/{tenantId}/switch-to-independent-db
    participant TS as TenantConfigurationAppService
    participant BJM as BackgroundJobManager (Hangfire)
    participant TDSJ as TenantDatabaseSwitchJob
    participant TDS as TenantDatabaseSwitchService
    participant EB as EventBus (RabbitMQ)
    participant EH as TenantDatabaseSwitchStatusEventHandler
    participant SH as TenantDatabaseSwitchHub<br/>/hubs/tenant-database-switch
    participant DB as PostgreSQL

    F->>TC: SwitchToIndependentDatabaseAsync(tenantId)
    TC->>TS: 校验配置存在且非独立数据库
    TS->>BJM: EnqueueAsync(TenantDatabaseSwitchArgs)
    BJM-->>TC: 202 Accepted
    TC-->>F: { message: "任务已提交，将在后台异步执行" }

    BJM->>TDSJ: ExecuteAsync(args)
    TDSJ->>EB: PublishAsync(TenantDatabaseSwitchStatusEto<br/>{Status: "started"})
    EB->>EH: HandleEventAsync
    EH->>SH: SendAsync("DatabaseSwitchStatusChanged", started)
    SH-->>F: 🔵 状态：切换开始

    TDSJ->>TDS: SwitchToIndependentDatabaseAsync(tenantId, tenantName)
    TDS->>DB: CREATE DATABASE + Migrate + Seed
    TDS->>DB: 数据导出（Identity/PermissionGrant/ModuleGrant）
    TDS->>DB: 数据验证（逐表 COUNT 比对）
    TDS->>DB: UPDATE JC_TenantConfigurations<br/>SET IsIndependentDatabase = true

    TDSJ->>EB: PublishAsync({Status: "completed", ConnectionString})
    EB->>EH: HandleEventAsync
    EH->>SH: SendAsync("DatabaseSwitchStatusChanged", completed)
    SH-->>F: 🟢 状态：切换成功

    alt 任何步骤异常
        TDSJ->>DB: DROP DATABASE（回滚）
        TDSJ->>EB: PublishAsync({Status: "failed", Message})
        EB->>EH: HandleEventAsync
        EH->>SH: SendAsync("DatabaseSwitchStatusChanged", failed)
        SH-->>F: 🔴 状态：切换失败
    end
```

### 1.3 关键接口与 DTO

| HTTP 方法 | 路由 | DTO | 功能 |
|:---------:|:-----|:----|:-----|
| `POST` | `/api/app/tenant-configuration` | `CreateTenantWithConfigDto` | 创建租户（含模块授权、许可证、独立数据库） |
| `GET` | `/api/app/tenant-configuration/{tenantId}` | — | 获取租户配置详情 |
| `GET` | `/api/app/tenant-configuration` | `PagedAndSortedResultRequestDto` | 分页获取租户配置列表 |
| `PUT` | `/api/app/tenant-configuration/{tenantId}/license` | `TenantLicenseDto` | 更新许可证配额 |
| `POST` | `/api/app/tenant-configuration/{tenantId}/switch-to-independent-db` | — | 触发独立数据库切换（异步） |

**`CreateTenantWithConfigDto` 字段**：

| 字段 | 类型 | 必填 | 说明 |
|:-----|:-----|:----:|:-----|
| `Name` | `string` | ✅ | 租户名称（最大 64） |
| `UnitCode` | `string` | ✅ | 单位编码，租户登录凭证（最大 50） |
| `AdminEmail` | `string` | ❌ | 管理员邮箱 |
| `AdminPassword` | `string` | ❌ | 管理员初始密码 |
| `ExpireDate` | `DateTime?` | ❌ | 到期日期 |
| `UseIndependentDatabase` | `bool` | ❌ | 是否独立数据库（默认 false） |
| `GrantedModuleIds` | `List<Guid>` | ❌ | 授权模块列表 |
| `License` | `TenantLicenseDto?` | ❌ | 许可证配额（MaxUserCount/MaxProjectCount/MaxPointCount/MaxStorageBytes） |

### 1.4 数据表

| 表名 | 核心字段 | 说明 |
|:-----|:---------|:-----|
| `AbpTenants` | `Id`, `Name` | ABP 租户主表 |
| `JC_TenantConfigurations` | `TenantId`, `UnitCode`, `Status`, `IsIndependentDatabase`, `IndependentConnectionString`, `License` | 租户扩展配置 |
| `JC_TenantModuleGrants` | `TenantId`, `ModuleId`, `IsGranted`, `GrantDate` | 租户模块授权 |

---

## 二、角色管理（Role Management）

### 2.1 创建角色与用户分配角色

```mermaid
sequenceDiagram
    autonumber
    actor F as 前端
    participant RC as IdentityRoleController<br/>api/identity/roles
    participant RS as IdentityRoleAppService (ABP)
    participant US as UserAppService (ABP)
    participant UM as UserManager (ABP)
    participant DB as PostgreSQL

    F->>RC: POST /api/identity/roles<br/>{Name, IsDefault, IsPublic}
    RC->>RS: CreateAsync(input)
    RS->>DB: INSERT INTO AbpRoles (Id, Name, NormalizedName, IsDefault, IsStatic, IsPublic)
    RS-->>RC: IdentityRoleDto
    RC-->>F: 201 Created

    F->>RC: GET /api/identity/roles/all
    RC->>RS: GetAllListAsync()
    RS->>DB: SELECT * FROM AbpRoles
    RS-->>RC: List(IdentityRoleDto)
    RC-->>F: 200 OK

    F->>RC: PUT /api/identity/users/{userId}/roles<br/>{RoleNames: ["admin", "monitor"]}
    RC->>US: UpdateRolesAsync(userId, roleNames)
    US->>UM: SetRolesAsync(user, ["admin", "monitor"])
    UM->>UM: RemoveFromRolesAsync(旧角色不在目标列表中的)
    UM->>DB: DELETE FROM AbpUserRoles WHERE UserId=x AND RoleId=y
    UM->>UM: AddToRolesAsync(新角色不在当前列表中的)
    UM->>DB: INSERT INTO AbpUserRoles (UserId, RoleId, TenantId)
    UM-->>US: IdentityResult.Success
    US-->>RC: 200 OK
    RC-->>F: 200 OK
```

### 2.2 关键接口与 DTO

| HTTP 方法 | 路由 | DTO | 功能 |
|:---------:|:-----|:----|:-----|
| `POST` | `/api/identity/roles` | `IdentityRoleCreateDto` | 创建角色 |
| `GET` | `/api/identity/roles/all` | — | 获取所有角色 |
| `PUT` | `/api/identity/roles/{id}` | `IdentityRoleUpdateDto` | 更新角色 |
| `DELETE` | `/api/identity/roles/{id}` | — | 删除角色 |
| `PUT` | `/api/identity/users/{id}/roles` | `List<string>`（角色名称列表） | 更新用户角色 |
| `GET` | `/api/identity/roles/{id}/users` | — | 获取角色下的用户列表 |

**`IdentityRoleDto` 字段**：

| 字段 | 类型 | 说明 |
|:-----|:-----|:-----|
| `Id` | `Guid` | 角色 ID |
| `Name` | `string` | 角色名称 |
| `IsDefault` | `bool` | 是否默认角色 |
| `IsStatic` | `bool` | 是否静态角色（不可删除） |
| `IsPublic` | `bool` | 是否公开 |

### 2.3 数据表

| 表名 | 核心字段 | 说明 |
|:-----|:---------|:-----|
| `AbpRoles` | `Id`, `Name`, `NormalizedName`, `IsDefault`, `IsStatic`, `IsPublic` | 角色主表 |
| `AbpUserRoles` | `UserId`, `RoleId`, `TenantId` | 用户-角色关联表（联合主键） |

---

## 三、菜单管理（Menu Management）

### 3.1 菜单树查询与租户菜单授权

```mermaid
sequenceDiagram
    autonumber
    actor F as 前端
    participant SMC as SystemModuleController<br/>/api/app/module
    participant TMC as TenantMenuAppService<br/>(动态 API /api/app/tenant-menu)
    participant TMG as TenantModuleGrantAppService<br/>(动态 API /api/app/tenant-module-grant)
    participant DB as PostgreSQL

    rect rgb(40, 80, 120)
        Note over F,DB: 【菜单定义管理】Host 端维护菜单树
        F->>SMC: GET /api/app/module/tree-list
        SMC->>DB: SELECT * FROM JC_SystemModules WHERE EnabledMark = true
        SMC-->>F: 完整模块树（SystemModuleDto）
    end

    rect rgb(120, 80, 40)
        Note over F,DB: 【租户菜单授权】为租户分配可见菜单
        F->>TMG: POST /api/app/tenant-module-grant/grant-modules<br/>{tenantId, moduleIds: [guid1, guid2]}
        TMG->>DB: INSERT INTO JC_TenantModuleGrants<br/>(TenantId, ModuleId, IsGranted, GrantDate)
        TMG-->>F: 200 OK

        F->>TMG: POST /api/app/tenant-module-grant/revoke-module<br/>{tenantId, moduleId}
        TMG->>DB: DELETE FROM JC_TenantModuleGrants<br/>WHERE TenantId=x AND ModuleId=y
        TMG-->>F: 200 OK
    end

    rect rgb(40, 100, 70)
        Note over F,DB: 【租户用户登录获取菜单】
        F->>TMC: GET /api/app/tenant-menu/current-tenant-menus
        TMC->>DB: SELECT ModuleId FROM JC_TenantModuleGrants<br/>WHERE TenantId = current AND IsGranted = true
        TMC->>DB: SELECT * FROM JC_SystemModules<br/>WHERE Id IN (grantedModuleIds) AND IsMenu = true
        TMC-->>F: List(TenantMenuDto)（当前租户可用菜单树）
    end
```

### 3.2 按钮权限管理

```mermaid
sequenceDiagram
    autonumber
    actor F as 前端
    participant BPC as TenantButtonPermissionController<br/>/api/app/tenant-button-permission
    participant BPS as TenantButtonPermissionAppService
    participant MB as ModuleButtonAppService<br/>/api/app/module-button
    participant DB as PostgreSQL

    rect rgb(40, 80, 120)
        Note over F,DB: 【按钮定义管理】
        F->>MB: GET /api/app/module-button?ModuleId={moduleId}
        MB->>DB: SELECT * FROM JC_ModuleButtons<br/>WHERE ModuleId = x AND EnabledMark = true
        MB-->>F: 分页按钮列表（ModuleButtonDto）
    end

    rect rgb(120, 80, 40)
        Note over F,DB: 【角色按钮权限配置】
        F->>BPC: GET /api/app/tenant-button-permission/role-permissions/{roleId}?moduleId=x
        BPC->>BPS: GetRolePermissionsAsync(roleId, moduleId)
        BPS->>DB: SELECT * FROM JC_ModuleButtons WHERE ModuleId=x
        BPS->>DB: SELECT * FROM JC_TenantButtonPermissions<br/>WHERE RoleId = roleId AND IsGranted = true
        BPC-->>F: List(ButtonPermissionDto)（按钮 + IsGranted 标记）

        F->>BPC: POST /api/app/tenant-button-permission/grant<br/>{roleId, buttonIds: [id1, id2]}
        BPC->>BPS: GrantAsync(roleId, buttonIds)
        BPS->>DB: INSERT INTO JC_TenantButtonPermissions<br/>(TenantId, ButtonId, RoleId, IsGranted)
        BPC-->>F: 200 OK

        F->>BPC: POST /api/app/tenant-button-permission/revoke<br/>{roleId, buttonIds: [id1, id2]}
        BPC->>BPS: RevokeAsync(roleId, buttonIds)
        BPS->>DB: DELETE FROM JC_TenantButtonPermissions<br/>WHERE RoleId=x AND ButtonId IN (...)
        BPC-->>F: 200 OK
    end

    rect rgb(40, 100, 70)
        Note over F,DB: 【页面加载获取可用按钮】
        F->>BPC: GET /api/app/tenant-button-permission/my-available-buttons/{moduleId}
        BPC->>BPS: GetMyAvailableButtonsAsync(moduleId)
        BPS->>DB: 获取当前用户角色 → 查询已授权按钮 + 公共按钮
        BPC-->>F: List(ModuleButtonDto)（当前用户可见按钮）
    end
```

### 3.3 关键实体字段

**SystemModule**（`JC_SystemModules`）

| 字段 | 类型 | 说明 |
|:-----|:-----|:-----|
| `Id` | `Guid` | 主键 |
| `EnCode` | `string(64)` | 编码（权限映射关键字段） |
| `FullName` | `string(256)` | 名称 |
| `UrlAddress` | `string(512)` | 链接地址 |
| `IsMenu` | `bool` | 是否菜单 |
| `IsFields` | `bool` | 是否字段 |
| `ParentId` | `Guid?` | 父节点（树结构） |
| `SortCode` | `int` | 排序码 |
| `EnabledMark` | `bool` | 是否启用 |

**ModuleButton**（`JC_ModuleButtons`）

| 字段 | 类型 | 说明 |
|:-----|:-----|:-----|
| `Id` | `Guid` | 主键 |
| `ModuleId` | `Guid` | 所属模块 |
| `EnCode` | `string(64)` | 按钮编码 |
| `FullName` | `string(256)` | 按钮名称 |
| `Location` | `int` | 0=工具栏, 1=行内, 2=右键菜单 |
| `IsPublic` | `bool` | 是否公共（无需授权） |
| `SortCode` | `int` | 排序码 |
| `EnabledMark` | `bool` | 是否启用 |

---

## 四、权限管理（Permission Management）

### 4.1 双重权限体系架构

```mermaid
flowchart TB
    subgraph 前端["前端权限配置"]
        F1[角色权限配置页]
        F2[租户模块授权页]
    end

    subgraph ABP权限["ABP 标准权限体系"]
        P1[PermissionDefinitionProvider]
        P2[Permissions.cs 常量]
        P3[Authorize 特性保护 API]
        P4[AbpPermissionGrants 表]
    end

    subgraph 自定义权限["自定义模块/字段/按钮权限"]
        C1[JC_TenantModuleGrants 表]
        C2[JC_TenantButtonPermissions 表]
        C3[SystemModule.IsFields 标记]
    end

    subgraph 运行时映射["运行时映射逻辑"]
        M1["TenantMenuAppService.GetRolePermissionTreeAsync<br/>ABP权限名匹配SystemModule.EnCode"]
        M2["TenantMenuAppService.GetRolePermissionFieldsTreeAsync<br/>筛选IsFields=true"]
        M3["TenantButtonPermissionAppService.GetMyAvailableButtonsAsync<br/>RoleId查询ButtonId"]
    end

    F1 -->|"PUT /api/identity/roles/[id]/permissions"| P4
    F1 -->|"GET /api/identity/roles/[id]/module-permissions"| M1
    F1 -->|"GET /api/identity/roles/[id]/field-permissions"| M2
    F2 -->|"POST /api/app/tenant-module-grant/grant-modules"| C1
    F2 -->|"POST /api/app/tenant-button-permission/grant"| C2

    P1 -->|"注册权限分组"| P2
    P2 -->|"定义常量如 Projects.Create"| P3
    P4 -->|"ProviderName=Role, ProviderKey=roleId"| M1
    M1 -->|"Projects.Create包含Projects<br/>SystemModule.EnCode=Projects被授权"| C1
    C1 -->|"TenantAuthAppService.TenantLoginAsync<br/>返回Menus"| 前端
```

### 4.2 ABP 标准权限分配流程

```mermaid
sequenceDiagram
    autonumber
    actor F as 前端
    participant RC as IdentityRoleController<br/>PUT /api/identity/roles/{id}/permissions
    participant PS as PermissionAppService
    participant PM as PermissionManager (ABP Domain)
    participant DB as PostgreSQL

    F->>RC: 更新角色权限<br/>List(string)<br/>["Projects.Create", "Projects.Edit", "Points.Delete"]
    RC->>PS: GrantAsync(providerName:"Role", providerKey:roleId, permissions)
    PS->>PM: SetAsync(...)
    PM->>DB: DELETE FROM AbpPermissionGrants<br/>WHERE ProviderName='Role' AND ProviderKey='{roleId}'
    loop 遍历每个权限名称
        PM->>DB: INSERT INTO AbpPermissionGrants<br/>(Id, TenantId, Name, ProviderName, ProviderKey)
    end
    PM-->>PS: 完成
    PS-->>RC: 完成
    RC-->>F: 200 OK
```

### 4.3 模块权限与字段权限的实时映射

```mermaid
sequenceDiagram
    autonumber
    actor F as 前端
    participant RC as IdentityRoleController<br/>GET /api/identity/roles/{id}/module-permissions
    participant TM as TenantMenuAppService
    participant PS as PermissionAppService
    participant DB as PostgreSQL

    F->>RC: GetRoleModulePermissionsAsync(roleId)
    RC->>TM: GetRolePermissionTreeAsync(roleId)
    TM->>DB: SELECT * FROM JC_SystemModules WHERE EnabledMark = true
    TM->>PS: GetPermissionTreeAsync("Role", roleId.ToString())
    PS->>DB: SELECT * FROM AbpPermissionGrants<br/>WHERE ProviderName='Role' AND ProviderKey='{roleId}'
    PS-->>TM: PermissionTreeDto（含 IsGranted）
    TM->>TM: grantedNames = ["Projects.Create", "Projects.Edit", ...]<br/>allowedModuleIds = modules.Where(m =><br/>grantedNames.Any(g => g.Contains(m.EnCode)))
    TM-->>RC: List(PermissionTreeDto)（模块树，Checked=true/false）
    RC-->>F: 200 OK

    F->>RC: GET /api/identity/roles/{id}/field-permissions?moduleIds=...
    RC->>TM: GetRolePermissionFieldsTreeAsync(roleId, moduleIds)
    TM->>DB: SELECT * FROM JC_SystemModules<br/>WHERE Id IN (moduleIds) AND EnabledMark = true
    TM->>PS: GetPermissionTreeAsync("Role", roleId.ToString())
    PS-->>TM: grantedNames
    TM->>TM: 相同的字符串包含匹配逻辑<br/>筛选出 IsFields=true 的模块
    TM-->>RC: List(PermissionTreeDto)（字段权限树）
    RC-->>F: 200 OK
```

### 4.4 关键接口汇总

| 权限类型 | HTTP 方法 | 路由 | 写入表 | 映射逻辑 |
|:---------|:---------:|:-----|:-------|:---------|
| ABP 标准权限 | `PUT` | `/api/identity/roles/{id}/permissions` | `AbpPermissionGrants` | 直接存储权限名称 |
| 模块权限 | `GET` | `/api/identity/roles/{id}/module-permissions` | —（实时计算） | `PermissionGrant.Name` 包含 `SystemModule.EnCode` |
| 字段权限 | `GET` | `/api/identity/roles/{id}/field-permissions` | —（实时计算） | 同上，筛选 `IsFields=true` |
| 租户模块授权 | `POST` | `/api/app/tenant-module-grant/grant-modules` | `JC_TenantModuleGrants` | 直接存储 TenantId-ModuleId 关系 |
| 角色按钮权限 | `POST` | `/api/app/tenant-button-permission/grant` | `JC_TenantButtonPermissions` | 直接存储 RoleId-ButtonId 关系 |

### 4.5 权限数据表汇总

| 表名 | 核心字段 | 作用 |
|:-----|:---------|:-----|
| `AbpPermissionGrants` | `Name`, `ProviderName`("Role"/"User"), `ProviderKey`(roleId/userId), `TenantId` | ABP 标准权限存储 |
| `JC_TenantModuleGrants` | `TenantId`, `ModuleId`, `IsGranted`, `GrantDate` | 租户可见菜单控制 |
| `JC_TenantButtonPermissions` | `TenantId`, `ButtonId`, `RoleId`, `IsGranted` | 角色按钮权限控制 |

---

## 五、四大模块交互总图

```mermaid
flowchart TB
    subgraph 租户管理["🏢 租户管理"]
        T1[TenantConfigurationController]
        T2[TenantConfigurationAppService]
        T3[AbpTenants / JC_TenantConfigurations]
    end

    subgraph 角色管理["👤 角色管理"]
        R1[IdentityRoleController]
        R2[IdentityRoleAppService]
        R3[AbpRoles / AbpUserRoles]
    end

    subgraph 菜单管理["📋 菜单管理"]
        M1[SystemModuleController]
        M2[TenantMenuAppService]
        M3[TenantModuleGrantAppService]
        M4[TenantButtonPermissionAppService]
        M5[JC_SystemModules / JC_ModuleButtons]
    end

    subgraph 权限管理["🔐 权限管理"]
        P1[PermissionController]
        P2[PermissionAppService]
        P3[AbpPermissionGrants]
    end

    subgraph 认证与登录["🔑 认证与登录"]
        A1[TenantAuthAppService]
        A2[TenantLoginAsync]
    end

    %% 租户创建到角色/用户/权限/菜单初始化
    T2 -->|DataSeeder| R3
    T2 -->|DataSeeder| P3
    T2 -->|GrantedModuleIds| M5
    T2 -->|"IdentityUser.CreateAsync"| R3

    %% 角色权限配置
    R1 -->|"PUT /[id]/permissions"| P1
    R1 -->|"GET /[id]/module-permissions"| M2
    R1 -->|"GET /[id]/field-permissions"| M2
    R1 -->|"PUT /[id]/roles"| R2

    %% 菜单与权限映射
    M2 -->|查询已授权权限名| P2
    M2 -->|字符串包含匹配| M5
    M3 -->|读写| M5
    M4 -->|读写| M5

    %% 登录时组装数据
    A2 -->|查询 TenantConfiguration| T3
    A2 -->|查询 TenantModuleGrant| M5
    A2 -->|查询 TenantUserExtension| T3
    A2 -->|生成 JWT Token| A1
```

---

## 六、前端接入速查表

### 6.1 租户管理

```javascript
// 创建租户
POST /api/app/tenant-configuration
{ Name: "某某监测公司", UnitCode: "JC001", AdminEmail: "admin@example.com", AdminPassword: "1q2w3E*", GrantedModuleIds: [guid1, guid2] }

// 切换独立数据库（异步）
POST /api/app/tenant-configuration/{tenantId}/switch-to-independent-db
// 返回 202，前端监听 SignalR
```

### 6.2 角色管理

```javascript
// 获取角色列表
GET /api/identity/roles/all

// 创建用户并分配角色
POST /api/identity/users
{ UserName: "zhangsan", Email: "zs@example.com", Password: "...", RoleNames: ["monitor"] }

// 更新用户角色
PUT /api/identity/users/{userId}/roles
["admin", "monitor"]
```

### 6.3 菜单管理

```javascript
// 获取全部模块树
GET /api/app/module/tree-list

// 获取当前租户菜单
GET /api/app/tenant-menu/current-tenant-menus

// 授予租户模块
POST /api/app/tenant-module-grant/grant-modules
{ tenantId: guid, moduleIds: [guid1, guid2] }

// 获取角色按钮权限
GET /api/app/tenant-button-permission/role-permissions/{roleId}?moduleId={moduleId}

// 批量授权按钮
POST /api/app/tenant-button-permission/grant
{ roleId: guid, buttonIds: [id1, id2] }
```

### 6.4 权限管理

```javascript
// 保存 ABP 标准权限
PUT /api/identity/roles/{roleId}/permissions
["Projects.Create", "Projects.Edit", "Points.Delete"]

// 获取模块权限树（用于前端勾选）
GET /api/identity/roles/{roleId}/module-permissions

// 获取字段权限树
GET /api/identity/roles/{roleId}/field-permissions?moduleIds=guid1,guid2
```

### 6.5 SignalR 实时推送（数据库切换）

```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/tenant-database-switch")
    .build();

connection.start().then(() => {
    connection.invoke("SubscribeTenant", tenantId);
});

connection.on("DatabaseSwitchStatusChanged", (data) => {
    console.log(data.status);   // "started" | "completed" | "failed"
    console.log(data.message);  // 状态描述
});
```

---

*文档生成时间：2026-05-14*  
*基于分支：`feature/tenant-reform-phase7`*
