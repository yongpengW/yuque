# 前端技术架构文档

## 1. 文档目标

本文档用于明确语雀类在线文档产品前端的技术架构、目录结构、状态管理、路由分层、接口分层与 UI 规范，作为前端项目初始化与后续团队协作的基线。

## 2. 技术栈

- React
- Vite
- TypeScript
- Ant Design
- React Router
- Zustand
- TanStack Query
- CSS Modules
- 编辑器单独选型，后续在 Tiptap 或 Slate 中确定

## 3. 架构原则

- 业务模块清晰分层，避免页面组件直接堆叠业务逻辑。
- 服务端状态与客户端状态分离管理。
- 以路由驱动页面组织，以业务域驱动模块划分。
- 组件复用优先通过业务封装实现，不直接到处耦合 Ant Design。
- 文档阅读与编辑区域保持可定制性，不被中后台视觉风格绑死。
- 样式以局部隔离为主，避免全局样式污染。

## 4. 目录结构

建议前端项目采用如下目录结构：

```text
src/
  app/
    router/
    providers/
    store/
    styles/
  assets/
  components/
    ui/
    business/
  features/
    auth/
    workspace/
    repository/
    document/
    comment/
    search/
    notification/
    setting/
  layouts/
  pages/
    public/
    app/
    admin/
  services/
    http/
    api/
    adapters/
  hooks/
  utils/
  types/
  constants/
  editor/
```

### 4.1 目录职责说明

- app：应用级初始化逻辑，包括路由注册、全局 Provider、全局状态装配。
- assets：图片、图标、字体等静态资源。
- components/ui：通用 UI 组件，对 Ant Design 进行二次封装。
- components/business：跨业务模块复用的业务组件，如知识库导航、权限标签、评论面板。
- features：按业务域组织的核心模块。
- layouts：布局组件，如主工作台布局、文档阅读布局、登录布局。
- pages：页面级组件，仅负责路由入口与页面拼装。
- services：http 客户端、接口定义、数据转换层。
- hooks：全局复用 Hook。
- utils：纯工具函数。
- types：公共类型定义。
- constants：常量定义，如路由名、权限码、查询键。
- editor：编辑器适配层，封装编辑器实例、插件、工具栏、内容序列化方案。

## 5. 模块划分

### 5.1 auth 模块

负责登录、注册、鉴权信息、当前用户信息、Token 生命周期管理。

### 5.2 workspace 模块

负责个人空间、团队空间、工作台首页、最近访问、收藏等工作区能力。

### 5.3 repository 模块

负责知识库列表、知识库设置、目录树、权限配置、归档状态等。

### 5.4 document 模块

负责文档阅读、编辑、保存、发布、历史版本、回收站、分享入口。

### 5.5 comment 模块

负责评论列表、段落评论、回复、已解决状态管理。

### 5.6 search 模块

负责全局搜索、筛选、搜索历史、结果展示。

### 5.7 notification 模块

负责站内通知、未读计数、消息分类与已读处理。

### 5.8 setting 模块

负责账号设置、团队设置、系统设置等。

## 6. 路由分层设计

### 6.1 路由层级

建议按访问范围与业务场景划分为三层：

- public routes：公开页面，如首页、公开知识库、分享页、登录注册页。
- app routes：登录后业务页面，如工作台、知识库、文档、搜索、通知。
- admin routes：团队管理后台或系统管理后台。

### 6.2 推荐路由结构

```text
/
/login
/register
/explore
/share/:shareId
/app
/app/dashboard
/app/repositories
/app/repositories/:repositoryId
/app/repositories/:repositoryId/docs/:docId
/app/search
/app/notifications
/app/settings
/admin
/admin/team
/admin/members
/admin/permissions
```

### 6.3 路由职责

- pages 负责路由入口，不承载复杂业务逻辑。
- layouts 负责统一壳层、导航区、面包屑、页面骨架。
- features 内部承接页面真正的业务实现。

### 6.4 路由守卫

- 未登录用户访问 app routes 时重定向到登录页。
- 无权限用户访问受限路由时跳转到 403 页面。
- 已登录用户访问登录页时跳转到工作台首页。
- 分享页与公开页单独处理鉴权逻辑。

## 7. 状态管理设计

### 7.1 状态分层原则

前端状态按三类处理：

- 服务端状态：来自后端接口的数据，如文档详情、知识库列表、评论列表，使用 TanStack Query 管理。
- 客户端全局状态：跨页面共享的会话状态，如当前用户、主题、当前团队、侧边栏状态，使用 Zustand 管理。
- 局部组件状态：仅当前组件使用的状态，使用 React 自身 state 管理。

### 7.2 Zustand 适用范围

建议使用 Zustand 管理以下全局状态：

- authStore：用户会话、Token、当前用户摘要。
- appStore：当前团队、当前知识库、全局 UI 抽屉状态。
- preferenceStore：主题、阅读宽度、编辑偏好。
- editorStore：当前文档的前端编辑上下文，如工具栏状态、选区状态、草稿变更标记。

### 7.3 TanStack Query 适用范围

建议所有接口数据查询与变更统一通过 TanStack Query 实现：

- useRepositoryListQuery
- useDocumentDetailQuery
- useCommentListQuery
- useNotificationListQuery
- useUpdateDocumentMutation
- usePublishDocumentMutation

### 7.4 状态管理约束

- 不在 Zustand 中缓存整份服务端数据副本。
- Query Key 统一管理，避免重复拼接与冲突。
- mutation 成功后优先通过 query invalidation 或 cache update 同步视图。
- 编辑器草稿态与已发布态必须分离，避免查询缓存被本地编辑污染。

## 8. 接口分层设计

### 8.1 分层目标

避免页面组件直接调用请求库，使接口调用、错误处理、鉴权处理、数据转换具备统一入口。

### 8.2 推荐分层

```text
services/
  http/
    client.ts
    interceptors.ts
  api/
    auth.api.ts
    repository.api.ts
    document.api.ts
    comment.api.ts
    search.api.ts
  adapters/
    document.adapter.ts
    repository.adapter.ts
```

### 8.3 分层职责

- http/client.ts：创建统一请求实例，处理 baseURL、超时、请求头。
- http/interceptors.ts：处理 Token 注入、刷新、统一错误响应。
- api/*.api.ts：定义按业务模块组织的接口函数。
- adapters：完成接口数据与前端视图模型之间的转换。

### 8.4 接口调用规范

- 页面层不直接写请求。
- feature 内部优先通过 query hooks 暴露数据能力。
- 接口返回结构统一处理，不将后端原始字段散落在各页面中。
- 错误提示统一由请求层和业务层协同处理。

## 9. UI 组件分层

### 9.1 分层模型

- 基础层：对 Ant Design 进行轻量封装，如 Button、Modal、Table、Form、Input。
- 业务层：组合基础组件形成业务组件，如 RepositoryTree、PermissionTag、CommentPanel。
- 页面层：布局与业务组件编排。

### 9.2 Ant Design 使用策略

- 优先用于表单、弹窗、抽屉、消息提示、后台列表页。
- 避免直接以默认视觉构建文档阅读页和编辑器主区域。
- 所有高频业务组件需要二次封装，减少未来替换成本。

### 9.3 自定义组件优先场景

以下区域建议以自定义样式与业务组件为主：

- 知识库左侧目录树
- 文档阅读页
- 编辑器工具栏与内容区
- 评论浮层与批注区
- 公开分享页

## 10. UI 规范

### 10.1 视觉原则

- 后台管理区强调清晰、稳定、信息密度可控。
- 文档阅读区强调内容优先、排版舒适、弱化操作噪音。
- 编辑区强调专注与效率，减少无关视觉干扰。

### 10.2 设计规范建议

- 颜色：基于 Ant Design Token 做主题定制，不直接使用默认整套风格。
- 间距：统一采用 4px 或 8px 递进体系。
- 圆角：中后台区域保持适中圆角，内容区不过度卡片化。
- 字体：后台与内容区可区分排版体系，内容区要优先保证长文阅读体验。
- 阴影：仅在弹层、抽屉、浮动工具条等需要层级感的区域使用。

### 10.3 交互规范

- 保存、发布、删除、权限变更等关键操作必须给出明确反馈。
- 列表、树、搜索结果、评论区统一处理空状态、错误状态和加载状态。
- 危险操作必须二次确认。
- 编辑器中的自动保存状态需要可见。

### 10.4 样式规范

- 页面级样式与组件级样式统一使用 CSS Modules。
- 全局样式仅保留 Reset、主题变量、排版基础规则。
- 禁止在业务页面大量写全局覆盖样式。
- 优先使用语义化 class 命名，避免样式与具体 DOM 结构强绑定。

## 11. 权限与前端控制

- 前端权限控制仅用于体验优化，不作为安全边界。
- 页面路由、按钮可见性、操作入口需基于权限码控制。
- 权限码统一维护在 constants 与 types 中。
- 接口返回 403 时前端需要给出一致的权限提示与回退策略。

## 12. 编辑器接入原则

- 编辑器作为独立子系统，不与普通表单状态直接混用。
- 内容模型、工具栏、插件体系、序列化策略要单独封装在 editor 目录。
- 首版预留 Tiptap 或 Slate 接口适配层，避免页面直接依赖编辑器实现细节。
- 编辑器输出需支持阅读态渲染、历史版本对比和后续导出能力。

## 13. 工程规范建议

- TypeScript 开启严格模式。
- 使用 ESLint + Prettier 保证代码风格一致。
- 目录、组件、Hook、Store、Query Key 命名保持统一。
- 组件文件尽量单一职责，超出复杂度后拆为 hooks、view、service。
- 核心业务组件补充单元测试与交互测试。

## 14. 初始化优先级建议

### 14.1 第一阶段

- 完成项目基础脚手架搭建。
- 接入 React Router、Ant Design、Zustand、TanStack Query。
- 建立基础布局、路由壳层、请求层与全局状态。

### 14.2 第二阶段

- 完成登录注册、工作台、知识库列表、文档详情页。
- 建立知识库目录树、文档阅读布局、基础权限显示逻辑。

### 14.3 第三阶段

- 接入编辑器。
- 实现评论、版本、搜索、通知等复杂业务能力。

## 15. 结论

本项目的前端架构应采用“业务模块驱动 + 状态分层管理 + 组件分层封装 + 内容区域定制化”的方案。Ant Design 负责业务基础组件，React Router 负责页面组织，Zustand 负责轻量全局状态，TanStack Query 负责服务端状态，CSS Modules 负责样式隔离，编辑器则保持独立可替换。该方案适合首版快速落地，也能支持后续向复杂在线文档产品演进。
