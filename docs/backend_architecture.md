# 后端架构设计文档

## 1. 文档目标

本文档用于定义语雀类在线文档产品首版后端架构方案，明确系统分层、模块边界、项目结构、数据库访问方式、接口组织、权限控制、后台任务、基础设施接入与演进路径，作为后续 .NET 10 Web API 项目初始化、EF Core 实体建模与迁移开发的基线。

## 2. 架构结论

首版后端建议采用模块化单体架构，技术实现基线如下：

- 运行时：.NET 10 Web API
- ORM：EF Core
- 数据库：PostgreSQL 18
- 认证：JWT + Refresh Token
- 缓存与异步协同预留：Redis
- 文件存储预留：本地存储抽象 + 对象存储适配层
- 后台任务：先用应用内后台任务抽象，后续可切换 Hangfire 或独立 Worker

不建议首版直接采用微服务，也不建议过早引入过重的 DDD 或事件驱动基础设施。首版重点是先把用户、团队、知识库、文档、权限、评论、通知、分享、审计这些核心域稳定落地。

## 3. 核心设计原则

- 以业务模块划分系统，而不是只按技术层拆目录。
- 保持模块内聚，避免跨模块直接访问彼此的数据实现细节。
- API 层只负责协议转换、鉴权入口、参数校验和响应组装。
- 业务规则集中在 Application 和 Domain 层，不把逻辑散落到 Controller 或 EF 配置里。
- EF Core 负责持久化映射，不让数据库实体直接污染接口契约。
- 先保证事务一致性与开发效率，再逐步增强缓存、搜索、实时协作等能力。

## 4. 推荐架构风格

### 4.1 总体风格

建议采用“模块化单体 + 分层架构”的组合方式：

- 横向按业务域拆模块
- 纵向按 API、Application、Domain、Infrastructure 分层

这种方案比纯三层架构更适合中长期演进，也比完整 DDD + 微服务更适合当前阶段。

### 4.2 分层职责

#### API 层

负责：

- HTTP 路由
- DTO 入参和出参
- 模型校验
- 调用 Application 层
- 返回统一响应格式

不负责：

- 复杂业务规则
- 数据库访问细节
- 权限规则拼装

#### Application 层

负责：

- 用例编排
- 事务边界控制
- 权限校验协调
- 调用领域服务与仓储接口
- 输出查询模型或结果对象

可以理解为每个业务动作对应一个明确的应用服务，例如：

- CreateRepository
- PublishDocument
- ResolveComment
- GrantRepositoryPermission

#### Domain 层

负责：

- 领域实体
- 值对象
- 领域规则
- 聚合边界
- 领域服务

例如：

- Document 的发布规则
- Repository 的可见性与默认权限规则
- ShareLink 的失效判断

#### Infrastructure 层

负责：

- EF Core DbContext
- EntityTypeConfiguration
- 仓储实现
- 外部服务适配
- 缓存、对象存储、消息、邮件、搜索等基础设施实现

## 5. 模块划分建议

首版建议按以下业务模块划分：

- Auth：认证、登录、刷新令牌、当前用户上下文
- Users：用户信息、账号设置、用户资料
- Teams：团队、成员、团队角色
- Repositories：知识库、知识库设置、知识库成员、标签
- Documents：文档、目录树、发布、版本、回收站、分享入口
- Comments：评论、回复、段落批注、解决状态
- Permissions：统一权限判定与授权策略
- Notifications：站内通知、已读状态、提醒聚合
- Search：检索入口、搜索记录、搜索结果聚合
- AuditLogs：关键操作审计日志
- Files：附件上传、访问、引用

说明：

- Permissions 可以作为独立模块存在，但实现上会高度参与 Repository、Document、Team 等模块。
- 若首版规模有限，也可以先不拆出独立 Search 模块，而是在 Documents 和 Repositories 中实现基础检索能力，再在目录结构上预留位置。

## 6. 推荐解决方案结构

建议采用多项目解决方案，但不要拆得过细。一个平衡实现效率和边界清晰度的结构如下：

```text
src/
  Yuque.Api/
  Yuque.Application/
  Yuque.Domain/
  Yuque.Infrastructure/
  Yuque.Contracts/
  Yuque.Shared/
tests/
  Yuque.UnitTests/
  Yuque.IntegrationTests/
```

### 6.1 各项目职责

#### Yuque.Api

- Program 启动入口
- Controller 或 Minimal API Endpoint
- 鉴权配置
- Swagger
- 全局异常处理中间件
- 请求上下文与响应规范

#### Yuque.Application

- 应用服务
- Command / Query 对象
- DTO 与 ViewModel
- 接口定义，例如仓储接口、当前用户接口、时间服务接口
- 事务协调与权限编排

#### Yuque.Domain

- 领域实体
- 枚举与值对象
- 领域服务
- 领域异常
- 领域事件定义，可先定义不强依赖总线

#### Yuque.Infrastructure

- AppDbContext
- EF Core 配置
- Repository 实现
- Token 服务
- Redis 缓存实现
- 文件存储实现
- 邮件通知实现
- 搜索实现

#### Yuque.Contracts

- 对外接口契约
- API 请求与响应模型
- 公共枚举 DTO

说明：

- 如果你想简化项目数量，也可以先不单独拆 Yuque.Contracts，而是把 DTO 放在 Api 或 Application。
- 但如果未来前后端联调、OpenAPI 生成、开放 API 会增多，Contracts 单独存在会更稳。

#### Yuque.Shared

- 结果对象
- 错误码
- 分页模型
- 时间与 ID 抽象
- 通用扩展方法

## 7. 模块内目录结构建议

以 Yuque.Application 为例，建议按业务模块组织，而不是所有 Commands、Queries 混在一起：

```text
Yuque.Application/
  Auth/
    Commands/
    Queries/
    Dtos/
    Services/
  Teams/
    Commands/
    Queries/
    Dtos/
    Services/
  Repositories/
    Commands/
    Queries/
    Dtos/
    Services/
  Documents/
    Commands/
    Queries/
    Dtos/
    Services/
  Comments/
  Notifications/
  Permissions/
  Abstractions/
  Behaviors/
```

以 Yuque.Infrastructure 为例：

```text
Yuque.Infrastructure/
  Persistence/
    AppDbContext.cs
    Configurations/
      Users/
      Teams/
      Repositories/
      Documents/
      Comments/
      Notifications/
    Repositories/
  Authentication/
  Authorization/
  Caching/
  Files/
  Search/
  Notifications/
  BackgroundJobs/
```

## 8. EF Core 与持久化设计建议

### 8.1 DbContext 设计

首版建议使用单一 AppDbContext，对应单一 PostgreSQL 数据库。

原因：

- 当前是模块化单体，事务一致性优先于物理隔离
- 迁移管理更简单
- 跨模块查询和联表更直接
- 对 EF Core 上手成本更低

但在代码组织上，仍然按模块分文件夹维护实体映射配置，例如：

- Configurations/Users/UserConfiguration.cs
- Configurations/Documents/DocumentConfiguration.cs

### 8.2 实体与领域模型关系

建议采用以下务实策略：

- 核心业务实体可直接作为 EF Core 实体映射对象
- 但不要把 API DTO 直接当作 EF 实体使用
- 复杂查询结果用 Query Model 或 Read Model 单独承载

首版不建议为了“纯粹 DDD”额外引入一套完全独立的 Persistence Model，否则会显著增加开发成本。

### 8.3 Repository 模式建议

不建议为每张表机械创建一个 Repository。

建议：

- 聚合根或核心业务对象才定义仓储接口
- 简单查询可以在 Query Service 中直接使用 DbContext

例如：

- IUserRepository
- IRepositoryRepository
- IDocumentRepository
- ICommentRepository

对于列表页、搜索页、统计页之类的读操作，可直接通过 Application 层查询服务访问 DbContext 或专门的 ReadDbContext。

### 8.4 迁移策略

既然你计划使用 EF 迁移，建议遵循以下规则：

- 所有表结构变更统一通过 migration 维护
- EntityTypeConfiguration 中显式配置表名、字段长度、唯一约束、索引和 delete behavior
- 不依赖 EF 默认推断生成关键约束
- 每个版本迁移命名必须表达清楚业务意图

例如：

- InitCoreSchema
- AddRepositoryPermissions
- AddDocumentShareLinks
- AddNotificationReceipts

### 8.5 删除行为建议

数据库外键不要默认级联删除核心数据，建议显式控制：

- 用户、团队、知识库、文档等核心实体使用 Restrict 或 NoAction
- 删除动作由应用层完成软删除与关联清理
- 少量中间表可以使用 Cascade

## 9. API 组织建议

### 9.1 路由风格

建议采用 REST 风格为主，少量复杂动作用 action endpoint 补充：

- GET /api/repositories
- POST /api/repositories
- GET /api/repositories/{repositoryId}
- GET /api/repositories/{repositoryId}/documents/{documentId}
- POST /api/documents/{documentId}/publish
- POST /api/comments/{commentId}/resolve

这种方式比完全 RPC 化更清晰，也比强行纯 REST 更适合文档产品中“发布、恢复、授权、归档”这类动作接口。

### 9.2 控制器组织

建议按业务模块拆 Controller：

- AuthController
- TeamsController
- RepositoriesController
- DocumentsController
- CommentsController
- NotificationsController
- SearchController

不要按 HTTP 方法或技术层拆 Controller。

### 9.3 响应模型建议

建议统一响应规范：

- 成功返回业务数据
- 失败返回错误码、错误消息、追踪 ID
- 分页接口统一返回 items + total + page + pageSize

不要把 EF 实体直接返回给前端。

## 10. 权限架构建议

权限是该产品后端的核心横切能力，建议单独设计统一的权限判定服务。

### 10.1 权限判定入口

建议提供统一接口，例如：

- IPermissionService
- ICurrentUser
- IAuthorizationEvaluator

核心职责：

- 判定当前用户能否访问团队、知识库、文档
- 判定当前用户是否具备 viewer、commenter、editor、manager 权限
- 统一处理知识库权限继承与文档覆盖权限

### 10.2 权限检查位置

建议在 Application 层做主权限检查，不要把权限判断全部堆到 Controller。

例如：

- GetDocumentDetailQueryHandler 负责检查查看权限
- PublishDocumentCommandHandler 负责检查编辑或管理权限

Controller 只做认证入口判断，例如要求登录。

### 10.3 权限缓存

首版可以不做复杂权限缓存。

若后续访问量上来，可对以下结果做短期缓存：

- 用户在某团队中的角色
- 用户在某知识库中的权限角色
- 文档最终权限计算结果

缓存失效应在授权变更、成员变更、文档权限更新时主动清除。

## 11. 文档与版本模块设计建议

### 11.1 文档模块的核心职责

文档模块不仅是 CRUD，还要承担：

- 草稿保存
- 发布版本生成
- 文档状态流转
- 回收站恢复
- 分享入口对接
- 阅读态与编辑态分离

### 11.2 建议的聚合边界

建议以 Document 为主聚合，DocumentVersion 作为其历史快照集合的一部分处理。

目录树 document_nodes 虽然和 document 高相关，但职责更偏知识库导航结构，可由 Repository 或 Document 模块协同管理，不建议让一个大聚合把目录树、文档、评论、权限全包进去。

### 11.3 发布流程建议

PublishDocumentCommand 的流程建议如下：

1. 检查文档编辑权限
2. 加载文档当前草稿
3. 校验内容格式与状态
4. 生成版本快照
5. 更新 documents.latest_version_id、published_at、status
6. 写入审计日志
7. 触发搜索索引刷新或异步通知

## 12. 评论与通知架构建议

### 12.1 评论模块

评论模块要支持：

- 文档评论
- 段落批注
- 回复链
- 已解决状态
- @ 提及

评论创建成功后，不建议在 Controller 里直接顺手写通知。应由 Application 层统一编排，或者发出领域事件后交由通知处理器消费。

### 12.2 通知模块

通知模块建议拆成两层：

- NotificationEvent：业务触发的通知事实
- NotificationDelivery：面向站内信、邮件等渠道的投递实现

首版可以先只做站内通知，邮件和外部机器人留适配接口。

## 13. 搜索架构建议

首版建议不要独立上搜索服务，先走 PostgreSQL 基础检索。

后端可在 Search 模块中统一封装搜索能力，对上暴露统一查询接口，例如：

- SearchDocumentsAsync
- SearchRepositoriesAsync
- SearchAllAsync

这样后续从 PostgreSQL 切到 Elasticsearch 或 OpenSearch 时，不需要改动上层业务。

## 14. 文件与对象存储架构建议

附件、图片、封面等文件能力建议抽象成统一文件服务：

- IFileStorageService
- IAttachmentService

首版可以先支持本地存储或单一对象存储实现，但业务层不能直接依赖具体 SDK。

建议把以下能力抽象出来：

- 上传文件
- 删除文件
- 获取访问 URL
- 校验类型和大小
- 生成安全对象键

## 15. 后台任务与异步处理建议

首版虽然是单体，也建议把异步动作抽象出来，不要全部内联在请求事务中。

适合异步化的任务包括：

- 发送邮件通知
- 刷新搜索索引
- 清理过期分享链接
- 定时清理回收站物理删除数据
- 生成文档导出文件

建议先抽象接口，例如：

- IBackgroundJobDispatcher
- IJobHandler<TJob>

实现上可以先用内存队列或 HostedService，后续再切 Hangfire、Quartz 或独立 Worker。

## 16. 横切能力建议

### 16.1 统一异常处理

建议通过中间件统一处理异常，分类为：

- 参数错误
- 认证失败
- 权限不足
- 业务冲突
- 资源不存在
- 系统异常

不要在每个 Controller 中手写 try-catch。

### 16.2 日志与可观测性

建议至少具备：

- 结构化日志
- 请求 traceId
- 关键业务操作审计日志
- 慢查询与异常告警预留

日志建议区分：

- 应用运行日志
- 安全与审计日志
- 后台任务日志

### 16.3 参数校验

建议在 API 层统一进行请求模型校验，可使用：

- ASP.NET Core 原生模型校验
- FluentValidation

首版如果团队规模不大，FluentValidation 会更利于维护复杂命令参数规则。

### 16.4 事务管理

建议以应用服务用例为事务边界。

原则：

- 单次业务动作默认一个事务
- 跨聚合但必须一致的操作放在同一事务内
- 非关键副作用通过异步任务或事件后处理

## 17. 配置与环境管理建议

建议按环境拆配置：

- appsettings.json
- appsettings.Development.json
- appsettings.Staging.json
- appsettings.Production.json

敏感配置不要写死在仓库中，例如：

- 数据库连接串
- JWT 密钥
- 对象存储密钥
- 邮件服务密钥

推荐通过环境变量或密钥管理服务注入。

## 18. 测试策略建议

首版建议至少覆盖两类测试：

- 单元测试：核心领域规则、权限判断、状态流转
- 集成测试：API + DbContext + PostgreSQL 或 Testcontainers

优先测试的高价值场景：

- 文档发布
- 文档权限继承与覆盖
- 知识库成员授权
- 评论回复与解决状态
- 分享链接访问控制

## 19. 首版不建议过早引入的复杂度

以下能力建议明确克制：

- 不要一开始就拆微服务
- 不要引入过重的 Mediator + Pipeline + EventBus 套娃式架构
- 不要为每张表都建仓储、服务、DTO、Mapper 四件套
- 不要为了理论纯度把读写完全物理分离
- 不要过早做多数据库、多租户、多搜索引擎适配

当前目标是做出一个边界清晰、可迁移、可扩展、但不过度设计的后端底座。

## 20. 建设顺序建议

建议按以下顺序推进：

1. 初始化 .NET 10 解决方案与基础分层项目
2. 搭建 AppDbContext、EF Core 配置、基础迁移体系
3. 完成 Auth、Users、Teams、Repositories 基础模块
4. 完成 Documents、DocumentVersions、DocumentNodes 核心模块
5. 完成 Permissions、Comments、Notifications
6. 完成 Shares、AuditLogs、Files
7. 最后再补 Search、Redis、后台任务和对象存储增强

## 21. 结论

这个项目的后端最合适的路线不是微服务，也不是极重的企业级分布式架构，而是一个边界清晰的模块化单体。核心思路是：按业务域拆模块，按 API、Application、Domain、Infrastructure 分层，用 EF Core 承接 PostgreSQL 持久化和迁移管理，用统一权限、审计、异常处理和后台任务把复杂业务兜住。这样既能快速支撑首版交付，也不会把后续实时协作、搜索增强、对象存储和开放平台的演进路线堵死。