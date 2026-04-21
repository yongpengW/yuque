# 后端项目目录与模块初始化规范文档

## 1. 文档目标

本文档用于将后端架构方案进一步落地为可执行的项目目录规范和模块初始化规范，明确 .NET 10 解决方案如何拆分项目、如何组织目录、如何定义模块边界、如何注册依赖、如何接入 EF Core 与基础设施，以便后续可以直接开始搭建后端工程骨架并持续按统一规则扩展。

## 2. 适用范围

本规范适用于当前语雀类在线文档产品首版后端工程，目标是支撑以下工作：

- 初始化 .NET 10 Web API 解决方案
- 创建分层项目结构
- 建立 EF Core 持久化项目布局
- 为 Auth、Teams、Repositories、Documents 等模块提供统一模板
- 限制项目之间的依赖方向
- 约束新模块的落地方式，避免后续目录混乱

## 3. 项目拆分结论

建议后端解决方案采用以下项目结构：

```text
src/
  Yuque.Api/
  Yuque.Application/
  Yuque.Domain/
  Yuque.Infrastructure/
  Yuque.Shared/
tests/
  Yuque.UnitTests/
  Yuque.IntegrationTests/
```

当前阶段不强制单独拆出 Yuque.Contracts，原因如下：

- 当前仍处于首版内部开发阶段
- 前后端接口尚未稳定到需要独立契约项目的程度
- 过早拆 Contracts 会增加维护成本

如果后续要做开放 API、SDK 生成、契约复用，再考虑新增 Yuque.Contracts。

## 4. 项目职责定义

### 4.1 Yuque.Api

职责：

- 作为 Web API 启动项目
- 承载 Program 启动配置
- 注册中间件、Swagger、认证授权、全局异常处理
- 暴露 Controller 或 Endpoint
- 完成请求 DTO 到应用命令的组装
- 返回统一响应模型

禁止放入：

- 核心业务规则
- EF Core 配置
- 数据访问代码
- 权限核心算法

### 4.2 Yuque.Application

职责：

- 按业务用例组织应用服务
- 定义 Commands、Queries、Dtos、ViewModels
- 定义仓储接口和外部依赖抽象
- 定义事务、当前用户、时间、ID 生成等抽象接口
- 实现业务流程编排、权限协调、返回结果组装

禁止放入：

- Controller
- EF Core DbContext 具体实现
- 第三方 SDK 调用细节

### 4.3 Yuque.Domain

职责：

- 存放领域实体、值对象、枚举、领域规则、领域异常
- 建立核心聚合边界
- 承载不依赖框架的业务规则

禁止放入：

- HTTP 相关模型
- 数据库配置属性污染严重的实现
- 外部服务实现

### 4.4 Yuque.Infrastructure

职责：

- 实现 EF Core DbContext 与实体映射
- 实现仓储接口
- 实现 Token、缓存、文件存储、邮件、搜索等基础设施服务
- 管理 EF Core Migrations

禁止放入：

- Controller
- 前端契约 DTO
- 业务流程编排入口

### 4.5 Yuque.Shared

职责：

- 放置跨项目可复用的轻量公共内容
- 如结果对象、分页模型、错误码、常量、基础异常、通用扩展方法

约束：

- Shared 只能放稳定、通用、无明显业务归属的内容
- 不要把杂项都塞进 Shared，避免演变为大杂烩

## 5. 项目依赖方向

必须遵守以下依赖规则：

- Yuque.Api 可以依赖 Yuque.Application、Yuque.Infrastructure、Yuque.Shared
- Yuque.Application 可以依赖 Yuque.Domain、Yuque.Shared
- Yuque.Infrastructure 可以依赖 Yuque.Application、Yuque.Domain、Yuque.Shared
- Yuque.Domain 只能依赖 Yuque.Shared
- Yuque.Shared 不依赖其他业务项目

禁止出现：

- Api 直接依赖 Domain 操作实体完成业务
- Application 直接依赖 Api
- Domain 依赖 Infrastructure
- Domain 依赖 EF Core 或 ASP.NET Core Web 层

依赖关系可以表示为：

```text
Api -> Application
Api -> Infrastructure
Application -> Domain
Infrastructure -> Application
Infrastructure -> Domain
Domain -> Shared
Application -> Shared
Infrastructure -> Shared
Api -> Shared
```

## 6. 推荐目录结构

建议整体目录如下：

```text
src/
  Yuque.Api/
    Controllers/
    Extensions/
    Middleware/
    Filters/
    Models/
      Requests/
      Responses/
    Common/
    Program.cs
  Yuque.Application/
    Abstractions/
    Behaviors/
    Auth/
    Users/
    Teams/
    Repositories/
    Documents/
    Comments/
    Notifications/
    Permissions/
    Files/
    Search/
    AuditLogs/
  Yuque.Domain/
    Common/
    Auth/
    Users/
    Teams/
    Repositories/
    Documents/
    Comments/
    Notifications/
    Permissions/
    Files/
    AuditLogs/
  Yuque.Infrastructure/
    Persistence/
    Authentication/
    Authorization/
    Caching/
    Files/
    Notifications/
    Search/
    BackgroundJobs/
    Extensions/
  Yuque.Shared/
    Results/
    Errors/
    Paging/
    Constants/
    Exceptions/
```

## 7. 模块目录模板

为了保证新增模块时结构统一，建议每个核心业务模块都遵循类似模板。

### 7.1 Application 层模块模板

以 Documents 模块为例：

```text
Yuque.Application/
  Documents/
    Commands/
      CreateDocument/
        CreateDocumentCommand.cs
        CreateDocumentValidator.cs
        CreateDocumentHandler.cs
      UpdateDocumentDraft/
      PublishDocument/
      DeleteDocument/
      RestoreDocument/
    Queries/
      GetDocumentDetail/
        GetDocumentDetailQuery.cs
        GetDocumentDetailHandler.cs
      GetRepositoryDocumentTree/
      GetDocumentVersions/
    Dtos/
      DocumentDto.cs
      DocumentVersionDto.cs
      DocumentTreeNodeDto.cs
    Services/
      IDocumentPermissionChecker.cs
      IDocumentContentRenderer.cs
```

说明：

- 每个用例单独目录，避免一个 Commands 目录下堆大量文件难以维护
- Validator 和 Handler 与对应 Command 紧邻放置
- 复杂模块允许增加 Mappers 或 Factories 子目录，但不是默认必须有

### 7.2 Domain 层模块模板

以 Documents 模块为例：

```text
Yuque.Domain/
  Documents/
    Entities/
      Document.cs
      DocumentVersion.cs
      DocumentNode.cs
    Enums/
      DocumentStatus.cs
      DocumentType.cs
      ContentFormat.cs
    ValueObjects/
      DocumentTitle.cs
    Services/
      IDocumentDomainService.cs
    Events/
      DocumentPublishedDomainEvent.cs
    Exceptions/
      InvalidDocumentStateException.cs
```

说明：

- 并不是每个模块都必须拥有完整的 Entities、ValueObjects、Events、Services 四件套
- 只有真的存在对应复杂度时再引入，不要为形式而建空目录

### 7.3 Infrastructure 层模块模板

以 Documents 模块为例：

```text
Yuque.Infrastructure/
  Persistence/
    Configurations/
      Documents/
        DocumentConfiguration.cs
        DocumentVersionConfiguration.cs
        DocumentNodeConfiguration.cs
    Repositories/
      Documents/
        DocumentRepository.cs
        DocumentQueryService.cs
```

说明：

- 写模型相关的仓储与配置放在 Persistence 下
- 非数据库能力的模块实现，如搜索、缓存、对象存储，放在 Infrastructure 的对应技术目录

### 7.4 Api 层模块模板

建议控制器按业务域组织：

```text
Yuque.Api/
  Controllers/
    AuthController.cs
    TeamsController.cs
    RepositoriesController.cs
    DocumentsController.cs
    CommentsController.cs
    NotificationsController.cs
```

若单个控制器过重，可引入分文件请求响应模型：

```text
Yuque.Api/
  Models/
    Requests/
      Documents/
        CreateDocumentRequest.cs
        UpdateDocumentDraftRequest.cs
        PublishDocumentRequest.cs
    Responses/
      Documents/
        DocumentDetailResponse.cs
        DocumentTreeResponse.cs
```

## 8. 新增模块的标准流程

当新增一个业务模块时，建议严格按以下顺序初始化：

1. 在 Domain 中补实体、枚举和值对象
2. 在 Application 中定义该模块的 Commands、Queries、Dtos、接口抽象
3. 在 Infrastructure 中实现 EF 配置、仓储和外部服务适配
4. 在 Api 中补 Controller、Request、Response
5. 在 Api 或 Infrastructure 的扩展方法中注册 DI
6. 补充单元测试与关键集成测试

不要从 Controller 直接反推业务和数据结构，这样很容易把接口层写成业务核心。

## 9. 模块初始化最低标准

每个核心模块至少要具备：

- 一个清晰的模块目录
- 至少一个读用例和一个写用例的 Application 实现
- 至少一个领域实体或核心规则承载点
- 至少一个基础持久化配置
- 一个模块注册入口或集中注册项

以 Documents 模块为例，最少应包括：

- Document 实体
- CreateDocumentCommand
- GetDocumentDetailQuery
- DocumentConfiguration
- DocumentsController

## 10. DI 与模块注册规范

建议不要把所有依赖注册都堆在 Program.cs。

推荐方式是每层提供扩展方法：

- Yuque.Application 提供 AddApplication
- Yuque.Infrastructure 提供 AddInfrastructure
- 各核心模块在对应层提供 AddDocumentsModule、AddRepositoriesModule 等内部注册方法

例如：

```text
Yuque.Api/
  Extensions/
    ServiceCollectionExtensions.cs
Yuque.Infrastructure/
  Extensions/
    ServiceCollectionExtensions.cs
Yuque.Application/
  Abstractions/
  Extensions/
```

注册层级建议：

- Program.cs 只保留高层调用
- Application 注册应用服务、Validator、行为管道
- Infrastructure 注册 DbContext、仓储、缓存、文件服务、认证服务

## 11. EF Core 目录与迁移规范

### 11.1 Persistence 结构

建议结构如下：

```text
Yuque.Infrastructure/
  Persistence/
    AppDbContext.cs
    DesignTime/
      AppDbContextFactory.cs
    Configurations/
      Users/
      Teams/
      Repositories/
      Documents/
      Comments/
      Notifications/
      Files/
      AuditLogs/
    Repositories/
    Migrations/
```

### 11.2 配置规则

每个实体必须使用 IEntityTypeConfiguration 显式配置：

- 表名
- 主键
- 字段长度
- 必填约束
- 唯一索引
- 外键关系
- 删除行为
- JSONB 映射
- 并发控制字段，如需要

不要把所有配置都写在 OnModelCreating 的一个大文件里。

### 11.3 Migrations 放置位置

统一将迁移放在 Yuque.Infrastructure 的 Persistence/Migrations 目录下。

原因：

- 迁移本质属于持久化实现
- 避免 Api 项目污染数据库迁移职责
- 更符合 EF Core 设计习惯

### 11.4 仓储与查询分工

建议分工如下：

- 复杂写操作和聚合持久化走 Repository
- 列表页、后台页、筛选页、搜索页可以走 QueryService 或直接查询 DbContext

不要为了形式把所有 Select 都包成 Repository 方法。

## 12. 命名规范

### 12.1 项目与命名空间

- 项目统一使用 Yuque.* 前缀
- 命名空间与目录结构保持一致

例如：

- Yuque.Application.Documents.Commands.CreateDocument
- Yuque.Infrastructure.Persistence.Configurations.Documents

### 12.2 类型命名

- Command：动词 + 业务对象 + Command
- Query：Get/List/Search + 业务对象 + Query
- Handler：与命令或查询同名后缀 Handler
- DTO：业务对象 + Dto
- Response：业务对象 + Response
- Repository：业务对象 + Repository

例如：

- CreateRepositoryCommand
- PublishDocumentCommand
- GetDocumentDetailQuery
- DocumentDto
- RepositoryMemberResponse

### 12.3 文件命名

- 一个公开类型一个文件
- 文件名与类型名一致
- 不使用 Utils、Helper 这种模糊命名作为核心业务承载点

## 13. 通用横切目录规范

### 13.1 当前用户上下文

建议在 Application.Abstractions 中定义：

- ICurrentUser

由 Infrastructure 或 Api 提供实现，从 JWT Claims 或请求上下文中读取当前用户信息。

### 13.2 时间与 ID 生成

建议定义：

- IClock
- IIdGenerator

目的：

- 方便测试
- 避免业务代码直接依赖 DateTime.UtcNow 或随机 ID 实现

### 13.3 结果对象

建议在 Shared 中统一提供：

- Result
- Result<T>
- PagedResult<T>

这样 Application 层返回结果时更统一。

### 13.4 错误码与业务异常

建议在 Shared 或 Application 中统一定义：

- ErrorCodes
- BusinessException
- NotFoundException
- ForbiddenException
- ConflictException

## 14. 控制器与请求模型规范

### 14.1 Controller 只做四件事

- 接收请求
- 校验模型
- 调用应用服务
- 返回响应

不要在 Controller 中写：

- 事务逻辑
- 权限策略细节
- 数据拼装查询
- 审计日志落库细节

### 14.2 Request 与 Response 规范

建议：

- Api.Requests 只用于入站 HTTP 模型
- Application.Dtos 只用于应用层数据交换
- Api.Responses 只用于出站响应模型

三者不要混用成一个万能模型。

## 15. 模块边界约束

为避免后续耦合失控，建议遵守以下规则：

- Documents 模块可以依赖 Permissions 抽象服务做权限校验，但不要直接读写 Permissions 内部实现细节
- Comments 模块可以依赖 Documents 查询文档存在性和访问权限，但不要直接修改 Document 聚合内部状态
- Notifications 模块负责通知记录和投递，不负责评论或文档主流程
- AuditLogs 作为横切模块，由 Application 用例在关键动作中调用

简单说：

- 可以依赖抽象
- 不要跨模块直接侵入内部实现
- 涉及多个模块的流程放在 Application 层编排

## 16. 首批建议初始化的模块

建议第一批先初始化以下模块，不要一口气把所有模块铺满：

1. Auth
2. Users
3. Teams
4. Repositories
5. Documents

第二批再补：

1. Permissions
2. Comments
3. Notifications
4. Files
5. AuditLogs

Search 可以先在 Documents 与 Repositories 查询中预留接口，后面再独立强化。

## 17. 首版初始化建议顺序

建议实际落地顺序如下：

1. 创建解决方案和 5 个核心项目
2. 建立项目引用关系
3. 初始化 Shared、Domain.Common、Application.Abstractions
4. 初始化 Infrastructure.Persistence 与 AppDbContext
5. 先完成 Auth、Users、Teams、Repositories、Documents 五个模块骨架
6. 为 Documents 建第一条完整链路作为模板

第一条完整链路建议选：

- 创建知识库
或
- 创建文档并查询详情

因为这两条链路能同时验证：

- Controller 到 Application 的调用
- Domain 实体建模
- EF Core 配置
- DbContext 持久化
- 返回 DTO/Response

## 18. 不建议的目录和实现方式

以下做法建议明确避免：

- 所有模块共享一个 Services 目录塞满所有业务服务
- 所有 DTO、Command、Query 放在一个平铺目录
- 所有 EF 配置集中在一个超大 DbContext 文件
- 一个模块只有 Controller + DbContext，没有 Application 和 Domain 承载点
- Controller 直接注入 DbContext 操作数据库并完成业务逻辑
- 为了“整洁”创建大量空目录和空接口

## 19. 结论

这份规范的目标不是把后端工程设计得很重，而是给你一个足够稳定、可以直接动手搭建的骨架标准。核心原则是：项目拆分保持克制，业务模块边界清晰，目录结构按模块而不是按技术名词堆叠，API、Application、Domain、Infrastructure 各司其职，EF Core 和迁移集中在 Infrastructure，后续新增模块严格按统一模板扩展。这样首版开发速度不会被架构拖垮，但后面也不至于演变成难以维护的大泥球。