# 数据库设计 vs 后端基表现有表（对照与复用）

对照对象：

- **设计文档**：`docs/database_schema.md`（语雀业务域）
- **基座现状**：`Yuque_Backend/Host/Yuque.WebAPI/Migrations/20260307033208_InitialDatabase.cs` 及 `Domain/Yuque.Core/Entities/**`

基座库表名为 **PascalCase**（如 `User`、`OperationLog`），设计文档为 **snake_case** 逻辑名；以下用基座**实际表名**表述。

---

## 0. 无外键策略（与基座一致）

- 语雀新建表 **不创建 PostgreSQL `FOREIGN KEY`**，仅用 **`bigint` 业务 id** 表达引用；级联与存在性由 **应用层 / 领域服务** 保证。
- 与 `Yuque.EFCore` 中 **`UseRemoveForeignKeys`** 及基座迁移风格一致；详见 `database_schema.md` §1.2、§1.6。

---

## 1. 可直接复用（与设计意图一致或已覆盖）

| 设计文档逻辑名 | 基座表 / 实体 | 说明 |
|----------------|---------------|------|
| `users`（§4.1） | **`User`** | 账号、邮箱、密码盐、头像、启用状态等已具备；语雀业务里所有 `user_id` **语义对应 `User.Id`**，**不再新建用户表**。字段名与文档不完全一致时以基座为准，DTO 映射即可。 |
| 管理端 RBAC | **`Role`、`Permission`、`UserRole`、`Menu`、`MenuResource`、`ApiResource`** | 对应《后端基座》里「仅管理类 / 系统设置」权限；**不是**知识库 ACL，无需为语雀再建一套 RBAC 表。 |
| 刷新令牌 / 会话 | **`UserToken`** | 含 `Token`、`RefreshToken`、`TokenHash`、`ExpirationDate`、`PlatformType` 等；设计文档 §4.18「refresh_tokens」**可不单独建表**，除非你希望 refresh 与基座登录完全拆库（一般不需要）。 |
| 系统操作审计（HTTP/菜单维度） | **`OperationLog`** | 偏「谁点了哪个菜单、请求轨迹」；`LogType`、`OperationContent`、`MenuCode`、`IpAddress` 等与文档 §4.17「资源级审计（repository/document + metadata）」**字段模型不同**，见 §3.2。 |
| 通用上传文件 | **`File`** | 基座通用文件表；**语雀附件已定为独立 `attachments` 表**（见 `database_schema.md` §4.12），与 `File` 并存：管理端/其它模块可继续用 `File`，编辑器与知识库附件走 `attachments`。 |
| 异步任务 | **`AsyncTask`** | 适合批量导出、全文索引重建、导入队列等；可与文档中的「后台重任务」复用，**不等价**于 `document_versions`。 |
| 计划任务 / 种子 | **`ScheduleTask`、`ScheduleTaskRecord`、`SeedDataTask`** | 基座调度与种子数据；语雀定时清理回收站、统计等可挂在同一套机制上，**无需为语雀重复造调度表**。 |
| 全局键值配置 | **`GlobalSettings`** | 平台级开关；与单库 `repositories.settings` 不同层级，**并存**。 |
| 开放平台应用 | **`AppConfig` 及 `AppEventConfig`、`AppNotificationConfig`、`AppWebhookConfig`** | 第三方应用、Webhook、通知渠道配置；与「站内用户通知 inbox」不是同一张表，见 §2。 |
| 下载中心 | **`DownloadItem`** | 导出大文件异步下载场景可复用。 |
| 组织 / 部门绑定 | **`Region`、`UserDepartment`** | 行政组织树与用户归属；**不替代**设计文档中的 **`teams` / `team_members`（知识空间）**。若产品上「企业团队」要与 Region 同步，可在应用层做映射或冗余 `external_region_id`，**表仍分开建更清晰**。 |

---

## 2. 必须新建（设计文档有、基座无）

以下在设计中属于**语雀核心内容域**，当前迁移中**不存在**对应表，需 **EF 新实体 + 新迁移** 引入。**均不设 DB 外键**，见 §0。

代码已实现于 `Yuque.Core.Entities.Knowledge`（C# 类型名与逻辑表对应如下；物理表名默认与类型名一致，**知识库主表**显式为 `KnowledgeBase` 以避免与英文 *repository* 模式混淆）。

| 设计文档（逻辑） | C# 实体 | 物理表名（当前约定） |
|------------------|---------|----------------------|
| `teams` | `Team` | `Team` |
| `team_members` | `TeamMember` | `TeamMember` |
| `repositories` | `KnowledgeBase` | `KnowledgeBase` |
| `repository_members` | `KnowledgeBaseMember` | `KnowledgeBaseMember` |
| `repository_entries` | `KnowledgeBaseEntry` | `KnowledgeBaseEntry` |
| `documents` | `Document` | `Document` |
| `document_versions` | `DocumentVersion` | `DocumentVersion` |
| `document_permissions` | `DocumentPermission` | `DocumentPermission` |
| `comments` | `Comment` | `Comment` |
| `comment_mentions` | `CommentMention` | `CommentMention` |
| `attachments` | `Attachment` | `Attachment` |
| `notifications` | `Notification` | `Notification` |
| `favorites` | `Favorite` | `Favorite` |
| `share_links` | `ShareLink` | `ShareLink` |
| `tags` / `document_tags` | （未建） | MVP 需要时再补 |

枚举见同目录 **`KnowledgeEnums.cs`**（如 `TeamType`、`KnowledgeBaseVisibility` 等）。

---

## 3. 部分复用 / 二选一（避免重复造轮子时的注意点）

### 3.1 `attachments` vs **`File`**（已定）

- **语雀侧**：**新建 `attachments` 表**（见 `database_schema.md` §4.12），字段紧贴 `repository_id` / `document_id` / `storage_key` 等。
- **基座 `File`**：仍可用于非语雀域的上传场景；两表 **并存**，不做强制统一。

### 3.2 `operation_logs`（设计） vs **`OperationLog`**（基座）

- 基座：面向 **API/菜单操作** 的审计。
- 设计：面向 **资源生命周期**（`resource_type` + `resource_id` + `metadata`）。

**建议**：**保留 `OperationLog`** 写管理端与网关类操作；语雀资源级事件可 **二选一**：（1）同一表加 `jsonb` 扩展列区分来源；（2）**新建 `ContentOperationLog` / `RepositoryAudit`** 避免菜单审计与文档审计混在一行语义里。与设计文档 §4.17 对齐时，推荐 **独立内容审计表**更清晰。

### 3.3 软删模型

- 基座实体普遍为 **`IsDeleted` bool** + `DateTimeOffset`（`AuditedEntity`）。
- 设计文档多处为 **`deleted_at timestamptz`**。

**建议**：新建语雀表时 **与基座保持一致**（`IsDeleted` + 可选 `DeletedAt`），仅在对外 API/查询封装成「回收站」语义；或在团队层统一改为 `deleted_at` 并批量调整基座（成本高，不推荐首版）。

---

## 4. 总览矩阵

| 类别 | 表或模块 |
|------|-----------|
| **复用基座即可** | `User`，`UserToken`，`Role`/`Permission`/`UserRole`/`Menu`/`MenuResource`/`ApiResource`，`OperationLog`（管理审计），`File`（非语雀附件场景），`AsyncTask`，`ScheduleTask`/`ScheduleTaskRecord`/`SeedDataTask`，`GlobalSettings`，`AppConfig*`，`DownloadItem`，`Region`/`UserDepartment`（行政，≠ teams） |
| **必须新建（语雀）** | `teams`…`share_links` 等见 §2，含 **`attachments`** |
| **按产品二选一或并存** | 资源审计：`OperationLog` 扩展 vs 独立内容审计表 |

---

## 5. 与设计文档的同步

- `database_schema.md` 文首已说明：§4.1 用户以基座为准；**§4.2 起为语雀新建表**；**§1.2 / §1.6 明确无 DB 外键**；**§4.12 `attachments` 为新建、不复用 `File`**。

以上对照基于当前仓库内 **InitialDatabase** 一次迁移；若你本地还有后续迁移，以 **`MainContextModelSnapshot`** 为准做一次 diff 即可。
