# 数据库表结构设计文档

## 1. 文档目标

本文档用于定义语雀类在线文档产品在首版阶段的数据库表结构设计方案，覆盖核心业务实体、字段建议、主外键关系、索引策略、约束规则与建模原则，为后续后端领域建模、ORM 映射、接口设计与迁移脚本编写提供依据。

## 2. 设计范围

本设计以 MVP 范围为主，优先覆盖以下能力：

- 用户注册登录
- 个人空间与团队空间
- 知识库创建与成员授权
- 文档目录与文档发布
- 评论与通知
- 分享访问控制
- 版本历史与回收站
- 审计日志

暂不在首版强绑定以下能力的最终模型，但预留扩展空间：

- 实时协同编辑
- 多组织架构
- 企业 SSO
- 插件系统
- 复杂审批流

## 3. 设计原则

- 以关系型建模为主，核心实体采用强约束关系。
- 文档结构化内容、审计附加信息等可借助 PostgreSQL 的 JSONB 承载扩展字段。
- 首版采用模块化单体，数据库优先保持边界清晰而不是过度拆分。
- 所有删除操作优先考虑软删除，避免误删后无法恢复。
- 权限设计采用多层授权模型，优先支持知识库级默认权限，文档级按需覆盖。
- 读多写多的查询路径必须提前设计必要索引，避免后期补救式优化。

## 4. 命名与通用字段约定

### 4.1 命名规范

- 表名使用复数蛇形命名，如 users、documents、document_versions。
- 主键统一使用 bigint 或 uuid 二选一。
- 若系统更强调分布式扩展与跨服务写入，建议使用 uuid。
- 若当前阶段更强调实现效率与查询简单性，建议使用 bigint 自增。

本文档示例统一使用 bigint 作为主键类型，后续若切换 uuid，可保持表结构不变仅替换主键生成策略。

### 4.2 通用审计字段

除中间关系表外，建议大部分业务表统一包含以下字段：

- id：主键
- created_at：创建时间
- created_by：创建人用户 ID，可空
- updated_at：更新时间
- updated_by：更新人用户 ID，可空
- deleted_at：软删除时间，可空
- deleted_by：删除人用户 ID，可空

### 4.3 状态字段约定

状态字段优先采用 varchar 或 smallint 枚举值，不建议在数据库层过早使用 PostgreSQL enum type，避免后续迁移复杂度升高。

推荐方式：

- 应用层维护枚举常量
- 数据库层通过 check constraint 约束合法范围

## 5. 核心实体总览

首版核心表建议如下：

- users
- user_identities
- teams
- team_members
- repositories
- repository_members
- repository_tags
- documents
- document_nodes
- document_permissions
- document_versions
- document_tags
- comments
- attachments
- notifications
- notification_receipts
- shares
- favorites
- operation_logs

其中：

- documents 用于承载文档主体。
- document_nodes 用于承载知识库目录树中的目录与文档节点关系。
- document_permissions 用于支持文档级权限覆盖。
- document_versions 用于保存发布版本与关键快照。
- shares 用于公开分享、密码访问、有效期控制。

## 6. 表结构设计

### 6.1 users

用户主表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 用户 ID |
| email | varchar(255) | unique, null | 邮箱 |
| phone | varchar(32) | unique, null | 手机号 |
| password_hash | varchar(255) | not null | 密码哈希 |
| nickname | varchar(100) | not null | 昵称 |
| avatar_url | varchar(500) | null | 头像 |
| bio | varchar(500) | null | 个人简介 |
| language | varchar(20) | not null default 'zh-CN' | 语言偏好 |
| status | varchar(20) | not null | active, disabled, pending |
| last_login_at | timestamptz | null | 最近登录时间 |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |
| deleted_at | timestamptz | null | 软删除时间 |

索引建议：

- unique index on email where email is not null
- unique index on phone where phone is not null
- index on status

### 6.2 user_identities

用户登录身份表，用于支持邮箱、手机号、第三方登录扩展。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| user_id | bigint | fk users(id) | 用户 ID |
| identity_type | varchar(30) | not null | email, phone, wechat, github 等 |
| identity_key | varchar(255) | not null | 外部身份唯一标识 |
| credential_hash | varchar(255) | null | 凭证哈希，第三方登录可空 |
| is_verified | boolean | not null default false | 是否已验证 |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |

索引建议：

- unique index on (identity_type, identity_key)
- index on user_id

### 6.3 teams

团队空间表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 团队 ID |
| owner_user_id | bigint | fk users(id) | 创建者 |
| name | varchar(100) | not null | 团队名称 |
| slug | varchar(100) | unique | 团队标识 |
| logo_url | varchar(500) | null | 团队 Logo |
| description | varchar(500) | null | 团队说明 |
| domain | varchar(255) | null | 自定义域名映射预留 |
| status | varchar(20) | not null | active, archived |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |
| deleted_at | timestamptz | null | 软删除时间 |

索引建议：

- unique index on slug
- index on owner_user_id
- index on status

### 6.4 team_members

团队成员关系表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| team_id | bigint | fk teams(id) | 团队 ID |
| user_id | bigint | fk users(id) | 用户 ID |
| role | varchar(20) | not null | owner, admin, member |
| join_status | varchar(20) | not null | invited, active, removed |
| joined_at | timestamptz | null | 加入时间 |
| invited_by | bigint | fk users(id), null | 邀请人 |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |

约束建议：

- unique(team_id, user_id)

索引建议：

- index on user_id
- index on team_id, role

### 6.5 repositories

知识库表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 知识库 ID |
| owner_type | varchar(20) | not null | user, team |
| owner_user_id | bigint | fk users(id), null | 个人知识库归属 |
| owner_team_id | bigint | fk teams(id), null | 团队知识库归属 |
| name | varchar(150) | not null | 知识库名称 |
| slug | varchar(150) | not null | 知识库标识 |
| icon | varchar(100) | null | 图标或封面标识 |
| cover_url | varchar(500) | null | 封面 |
| description | varchar(1000) | null | 简介 |
| visibility | varchar(20) | not null | private, team, public |
| default_role | varchar(20) | not null | none, viewer, commenter |
| status | varchar(20) | not null | active, archived |
| created_at | timestamptz | not null | 创建时间 |
| created_by | bigint | fk users(id) | 创建人 |
| updated_at | timestamptz | not null | 更新时间 |
| updated_by | bigint | fk users(id), null | 更新人 |
| deleted_at | timestamptz | null | 软删除时间 |
| deleted_by | bigint | fk users(id), null | 删除人 |

约束建议：

- check 保证 owner_type = 'user' 时 owner_user_id 非空且 owner_team_id 为空
- check 保证 owner_type = 'team' 时 owner_team_id 非空且 owner_user_id 为空
- unique(owner_user_id, slug) where owner_user_id is not null
- unique(owner_team_id, slug) where owner_team_id is not null

索引建议：

- index on owner_team_id
- index on owner_user_id
- index on visibility, status

### 6.6 repository_members

知识库成员授权表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| repository_id | bigint | fk repositories(id) | 知识库 ID |
| principal_type | varchar(20) | not null | user, team_member_group |
| principal_id | bigint | not null | 授权对象 ID |
| role | varchar(20) | not null | viewer, commenter, editor, manager |
| granted_by | bigint | fk users(id) | 授权人 |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |

约束建议：

- unique(repository_id, principal_type, principal_id)

索引建议：

- index on repository_id, role
- index on principal_type, principal_id

说明：

若首版不实现用户组授权，可先只支持 principal_type = 'user'，字段保留即可。

### 6.7 repository_tags

知识库标签表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| repository_id | bigint | fk repositories(id) | 知识库 ID |
| name | varchar(50) | not null | 标签名 |
| color | varchar(20) | null | 展示色 |
| created_at | timestamptz | not null | 创建时间 |
| created_by | bigint | fk users(id), null | 创建人 |

约束建议：

- unique(repository_id, name)

### 6.8 documents

文档主表，承载文档元信息与当前草稿内容。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 文档 ID |
| repository_id | bigint | fk repositories(id) | 所属知识库 |
| creator_id | bigint | fk users(id) | 创建人 |
| current_editor_id | bigint | fk users(id), null | 最近编辑人 |
| title | varchar(255) | not null | 文档标题 |
| slug | varchar(255) | null | 路由友好标识 |
| summary | varchar(1000) | null | 摘要 |
| cover_url | varchar(500) | null | 封面 |
| doc_type | varchar(20) | not null | rich_text, markdown, api |
| status | varchar(20) | not null | draft, published, archived, deleted |
| content_format | varchar(20) | not null | json, markdown, html |
| draft_content | jsonb | null | 当前草稿内容 |
| rendered_content | text | null | 阅读态渲染结果缓存 |
| latest_version_id | bigint | fk document_versions(id), null | 最新发布版本 |
| published_at | timestamptz | null | 最近发布时间 |
| archived_at | timestamptz | null | 归档时间 |
| deleted_at | timestamptz | null | 软删除时间 |
| created_at | timestamptz | not null | 创建时间 |
| created_by | bigint | fk users(id) | 创建人 |
| updated_at | timestamptz | not null | 更新时间 |
| updated_by | bigint | fk users(id), null | 更新人 |

索引建议：

- index on repository_id, status
- index on creator_id
- index on updated_at desc
- gin index on draft_content jsonb_path_ops，可按实际查询决定是否启用

说明：

- 首版可将草稿内容保存在 documents.draft_content。
- 已发布快照统一保存在 document_versions。
- 若后续需要独立草稿表，可平滑拆分为 document_drafts。

### 6.9 document_nodes

知识库目录树节点表，用于表示目录、子目录、文档挂载关系。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 节点 ID |
| repository_id | bigint | fk repositories(id) | 所属知识库 |
| parent_id | bigint | fk document_nodes(id), null | 父节点 |
| node_type | varchar(20) | not null | folder, document |
| document_id | bigint | fk documents(id), null | 文档节点对应的文档 |
| title | varchar(255) | not null | 节点标题 |
| sort_order | integer | not null default 0 | 同层排序 |
| depth | integer | not null default 0 | 节点层级 |
| path | varchar(2000) | null | 物化路径，可选 |
| is_pinned | boolean | not null default false | 是否置顶 |
| created_at | timestamptz | not null | 创建时间 |
| created_by | bigint | fk users(id), null | 创建人 |
| updated_at | timestamptz | not null | 更新时间 |
| updated_by | bigint | fk users(id), null | 更新人 |
| deleted_at | timestamptz | null | 软删除时间 |

约束建议：

- check 保证 node_type = 'folder' 时 document_id 为空
- check 保证 node_type = 'document' 时 document_id 非空
- unique(document_id) where document_id is not null

索引建议：

- index on repository_id, parent_id, sort_order
- index on repository_id, path
- index on document_id

说明：

- 采用邻接表 + 可选物化路径方式，兼顾实现简单和目录查询效率。
- 若后续目录树操作频繁且层级更深，可考虑补充 closure table。

### 6.10 document_permissions

文档级权限覆盖表。当文档需要脱离知识库默认授权时使用。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| document_id | bigint | fk documents(id) | 文档 ID |
| principal_type | varchar(20) | not null | user, team_member_group |
| principal_id | bigint | not null | 授权对象 ID |
| permission_level | varchar(20) | not null | viewer, commenter, editor, manager |
| granted_by | bigint | fk users(id) | 授权人 |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |

约束建议：

- unique(document_id, principal_type, principal_id)

索引建议：

- index on document_id, permission_level
- index on principal_type, principal_id

### 6.11 document_versions

文档历史版本表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 版本 ID |
| document_id | bigint | fk documents(id) | 文档 ID |
| version_no | integer | not null | 版本号，从 1 递增 |
| title | varchar(255) | not null | 版本标题快照 |
| content_format | varchar(20) | not null | json, markdown, html |
| snapshot_content | jsonb | not null | 内容快照 |
| rendered_content | text | null | 渲染结果快照 |
| summary | varchar(1000) | null | 版本摘要 |
| change_note | varchar(500) | null | 版本说明 |
| created_at | timestamptz | not null | 版本创建时间 |
| created_by | bigint | fk users(id) | 发布人 |

约束建议：

- unique(document_id, version_no)

索引建议：

- index on document_id, version_no desc
- index on created_at desc

说明：

- 首版无需保存每次输入变更，仅保存发布版本和关键保存快照。
- 若未来支持细粒度历史回放，可新增操作日志或编辑事件表。

### 6.12 document_tags

文档标签关联表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| document_id | bigint | fk documents(id) | 文档 ID |
| tag_id | bigint | fk repository_tags(id) | 标签 ID |
| created_at | timestamptz | not null | 创建时间 |
| created_by | bigint | fk users(id), null | 创建人 |

约束建议：

- unique(document_id, tag_id)

### 6.13 comments

评论表，支持文档评论、段落评论、回复。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 评论 ID |
| document_id | bigint | fk documents(id) | 文档 ID |
| version_id | bigint | fk document_versions(id), null | 关联版本 |
| parent_id | bigint | fk comments(id), null | 父评论 |
| root_id | bigint | fk comments(id), null | 根评论 |
| author_id | bigint | fk users(id) | 评论人 |
| reply_to_user_id | bigint | fk users(id), null | 回复目标用户 |
| content | text | not null | 评论内容 |
| content_format | varchar(20) | not null default 'plain_text' | 内容格式 |
| anchor_type | varchar(20) | not null | document, block, text_range |
| anchor_data | jsonb | null | 批注锚点信息 |
| status | varchar(20) | not null | open, resolved |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |
| deleted_at | timestamptz | null | 软删除时间 |

索引建议：

- index on document_id, root_id, created_at
- index on author_id
- index on status

说明：

- anchor_data 可保存块 ID、偏移范围、选中文本摘要等信息。
- 回复链查询优先依赖 root_id + parent_id 组合。

### 6.14 attachments

附件表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 附件 ID |
| uploader_id | bigint | fk users(id) | 上传人 |
| repository_id | bigint | fk repositories(id), null | 所属知识库 |
| document_id | bigint | fk documents(id), null | 所属文档 |
| storage_provider | varchar(30) | not null | local, s3, oss, cos |
| bucket_name | varchar(100) | null | 存储桶 |
| object_key | varchar(500) | not null | 对象键 |
| file_name | varchar(255) | not null | 原文件名 |
| mime_type | varchar(100) | not null | MIME 类型 |
| file_size | bigint | not null | 文件大小 |
| file_ext | varchar(20) | null | 扩展名 |
| checksum | varchar(128) | null | 校验值 |
| is_public | boolean | not null default false | 是否公开访问 |
| created_at | timestamptz | not null | 创建时间 |
| deleted_at | timestamptz | null | 软删除时间 |

索引建议：

- index on uploader_id
- index on repository_id
- index on document_id
- unique index on (storage_provider, bucket_name, object_key)

### 6.15 notifications

通知事件表，记录系统产生的通知主体。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 通知 ID |
| event_type | varchar(30) | not null | comment, mention, share, permission_changed, task_assigned |
| actor_user_id | bigint | fk users(id), null | 触发人 |
| target_user_id | bigint | fk users(id) | 接收人 |
| resource_type | varchar(30) | not null | document, comment, repository, share |
| resource_id | bigint | not null | 资源 ID |
| title | varchar(255) | not null | 通知标题 |
| content | varchar(1000) | null | 通知摘要 |
| payload | jsonb | null | 扩展信息 |
| created_at | timestamptz | not null | 创建时间 |

索引建议：

- index on target_user_id, created_at desc
- index on event_type

### 6.16 notification_receipts

通知阅读状态表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| notification_id | bigint | fk notifications(id) | 通知 ID |
| user_id | bigint | fk users(id) | 用户 ID |
| is_read | boolean | not null default false | 是否已读 |
| read_at | timestamptz | null | 阅读时间 |
| archived_at | timestamptz | null | 归档时间 |
| created_at | timestamptz | not null | 创建时间 |

约束建议：

- unique(notification_id, user_id)

索引建议：

- index on user_id, is_read, created_at desc

说明：

- 若确定一条通知只发给一人，可将 is_read 直接放在 notifications 表。
- 若后续支持群发、广播通知，拆 receipt 表更稳妥，因此建议首版即按当前方案设计。

### 6.17 shares

分享链接表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| resource_type | varchar(20) | not null | document, repository |
| resource_id | bigint | not null | 资源 ID |
| creator_id | bigint | fk users(id) | 创建人 |
| share_token | varchar(100) | not null unique | 分享令牌 |
| password_hash | varchar(255) | null | 访问密码哈希 |
| allow_comment | boolean | not null default false | 是否允许评论 |
| allow_copy | boolean | not null default true | 是否允许复制 |
| allow_export | boolean | not null default true | 是否允许导出 |
| expire_at | timestamptz | null | 失效时间 |
| max_visit_count | integer | null | 最大访问次数 |
| current_visit_count | integer | not null default 0 | 当前访问次数 |
| status | varchar(20) | not null | active, disabled, expired |
| created_at | timestamptz | not null | 创建时间 |
| updated_at | timestamptz | not null | 更新时间 |

索引建议：

- unique index on share_token
- index on resource_type, resource_id
- index on creator_id

### 6.18 favorites

收藏表，用于收藏知识库或文档。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 主键 |
| user_id | bigint | fk users(id) | 用户 ID |
| resource_type | varchar(20) | not null | repository, document |
| resource_id | bigint | not null | 资源 ID |
| created_at | timestamptz | not null | 创建时间 |

约束建议：

- unique(user_id, resource_type, resource_id)

索引建议：

- index on user_id, created_at desc

### 6.19 operation_logs

操作审计日志表。

| 字段名 | 类型 | 约束 | 说明 |
| --- | --- | --- | --- |
| id | bigint | pk | 日志 ID |
| operator_user_id | bigint | fk users(id), null | 操作者 |
| action | varchar(50) | not null | create_document, publish_document, grant_permission 等 |
| resource_type | varchar(30) | not null | document, repository, team, comment, share |
| resource_id | bigint | not null | 资源 ID |
| team_id | bigint | fk teams(id), null | 团队上下文 |
| repository_id | bigint | fk repositories(id), null | 知识库上下文 |
| document_id | bigint | fk documents(id), null | 文档上下文 |
| request_id | varchar(100) | null | 请求链路 ID |
| ip_address | varchar(64) | null | IP 地址 |
| user_agent | varchar(500) | null | User Agent |
| result | varchar(20) | not null | success, failed |
| detail | jsonb | null | 操作详情 |
| created_at | timestamptz | not null | 创建时间 |

索引建议：

- index on operator_user_id, created_at desc
- index on resource_type, resource_id, created_at desc
- index on repository_id, created_at desc
- index on action

## 7. 核心关系说明

主要关系如下：

- 一个 user 可创建多个 team。
- 一个 team 可拥有多个 repository。
- 一个 repository 下包含多个 document。
- 一个 repository 通过 document_nodes 组织目录树。
- 一个 document 有多条 document_versions。
- 一个 document 有多条 comments。
- repository_members 定义知识库默认授权。
- document_permissions 定义文档级覆盖授权。
- shares 绑定 document 或 repository。
- notifications 面向用户投递事件，notification_receipts 记录阅读状态。

## 8. 权限模型落库建议

### 8.1 首版权限来源

建议按以下顺序判定用户对文档的最终权限：

1. 平台超级管理员
2. 团队管理员或知识库管理员
3. document_permissions 中的显式授权
4. repository_members 中的默认授权
5. repository.visibility 推导出的默认访问能力
6. 无权限

### 8.2 角色与权限映射建议

建议在应用层维护角色能力映射，例如：

- viewer：可查看
- commenter：可查看、评论
- editor：可查看、评论、编辑
- manager：可查看、评论、编辑、管理、授权、导出

数据库中优先存储角色，而不是细粒度布尔字段，便于扩展与统一判权。

## 9. 删除与恢复策略

首版建议以下资源使用软删除：

- users
- teams
- repositories
- documents
- document_nodes
- comments
- attachments

处理原则：

- 文档删除时，documents.status 置为 deleted，并写入 deleted_at。
- 文档从目录树默认视图隐藏，但保留恢复能力。
- 回收站页面优先基于 documents 查询，而不是单独建 trash 表。
- 真正物理删除可交由后台定时任务处理。

## 10. 索引与性能建议

### 10.1 首版必须索引的查询路径

- 用户登录查询：users.email、users.phone、user_identities(identity_type, identity_key)
- 团队成员查询：team_members(team_id, user_id)
- 知识库列表：repositories(owner_team_id)、repositories(owner_user_id)
- 目录树查询：document_nodes(repository_id, parent_id, sort_order)
- 文档详情：documents(repository_id, status)
- 历史版本：document_versions(document_id, version_no)
- 评论列表：comments(document_id, root_id, created_at)
- 通知列表：notifications(target_user_id, created_at)
- 审计日志：operation_logs(resource_type, resource_id, created_at)

### 10.2 搜索设计建议

首版若使用 PostgreSQL 做基础全文检索，可考虑：

- 在 documents 中增加 tsvector 字段，例如 search_vector
- 由标题、摘要、渲染文本共同构建检索向量
- 建立 gin index on search_vector

若当前阶段先不做数据库内全文检索，也至少应预留以下搜索输入来源：

- title
- summary
- rendered_content
- repository_tags

## 11. 迁移顺序建议

建议按依赖顺序创建表：

1. users
2. user_identities
3. teams
4. team_members
5. repositories
6. repository_members
7. repository_tags
8. documents
9. document_nodes
10. document_permissions
11. document_versions
12. document_tags
13. comments
14. attachments
15. notifications
16. notification_receipts
17. shares
18. favorites
19. operation_logs

说明：

- documents.latest_version_id 对 document_versions 存在反向依赖，可在初始迁移时先不加外键，后续通过增量迁移补上。
- 若 ORM 对循环外键支持有限，建议由应用层保证 latest_version_id 合法性，数据库层只保留普通索引。

## 12. 首版可延后建设的表

以下表在功能落地前可暂缓：

- tasks：文档任务协同
- webhooks：开放平台回调
- api_tokens：开放 API 鉴权
- document_templates：模板中心
- recent_visits：最近访问记录
- search_histories：搜索历史

这些能力不是 MVP 的阻塞项，但字段和模型设计时应保留扩展余地。

## 13. 结论

首版数据库应围绕用户、团队、知识库、目录树、文档、版本、评论、通知、分享、审计十个核心域建设。整体建模采用 PostgreSQL 关系型方案为主，辅以 JSONB 承载内容结构和扩展元数据，既能支撑当前 MVP，也能为后续实时协同、开放平台、企业权限与搜索增强留下足够扩展空间。