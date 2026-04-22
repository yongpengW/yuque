# 后端基座约定（Yuque_Backend）

本文档记录语雀类产品在 **`Yuque_Backend`** 上扩展时的架构决策，与《技术选型》《数据库表结构设计》一致。

---

## 1. 主键与正文

| 项 | 约定 |
|----|------|
| 主键 | **`bigint` + 雪花（Snowflake）**，与基座 `Yuque.EFCore.Entities.Entity` 一致 |
| 文档版本正文 | **`jsonb`**，块编辑器 JSON 为权威存储（见 `database_schema.md` §1.4） |
| 库表关联 | **不设数据库外键**；仅用 **`bigint` 业务 id** 逻辑关联，与 `Yuque.EFCore` 的 `UseRemoveForeignKeys` 一致（见 `database_schema.md` §1.2、§1.6） |
| 语雀附件 | **独立 `attachments` 表**，不复用基座 `File`（见 `database_schema.md` §4.12、`database_baseline_table_mapping.md`） |

---

## 2. 权限：RBAC 与内容 ACL

| 范围 | 机制 | 用途 |
|------|------|------|
| 基座 RBAC | `User` / `Role` / `Permission` / 菜单等 | **管理端、系统设置、运营/审计** |
| 语雀内容权限 | `repository_members`、`document_permissions`、`repositories.visibility` 等 | **知识库 / 文档读写与分享** |

两套逻辑 **不要混用**：内容 API 使用独立授权组件（如中间件 + 资源表查询），与现有 RBAC API 过滤器分流。

---

## 3. 配置与连接

- **已移除 AgileConfig 宿主集成**：配置以 `appsettings*.json` 与环境变量为准（见 `Yuque.Core` 中 `InitHostAndConfig`）。
- **数据库连接字符串**：优先读取 `ConnectionStrings:DefaultConnection`，若无则回退 `ConnectionStrings:PostgreSQL`（兼容旧模板）。

---

## 4. 解决方案组成（语雀 Fork 裁剪）

| 组件 | 状态 | 说明 |
|------|------|------|
| `Yuque.WebAPI` | **保留** | 主 API，语雀业务在此扩展 |
| `Yuque.MQService` | **保留** | 后续文档同步、协作消息等 |
| `Yuque.PlanTaskService` | **保留** | 定时任务、后台补偿 |
| `Host/Yuque.Gateway` | **不纳入日常交付** | 工程可仍留在仓库；**当前 `Yuque.sln` 未引用**，单机开发只跑 WebAPI 即可 |
| AgileConfig | **已移除** | 不再引用 `AgileConfig.Client`，无需部署配置中心即可本地启动（需本地 PostgreSQL 等仍按 appsettings 配置） |

---

## 5. 命名

产品期程序集与命名空间 **继续使用 `Yuque.*`**，与现有基座一致。

---

## 6. 许可证与第三方包

按团队要求可暂缓审计；上线前再统一核对商业组件授权。
