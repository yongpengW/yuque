# 编辑器开发设计文档

## 1. 文档目标

本文档用于明确语雀类在线文档编辑器的前端开发方案、依赖选型、功能边界、数据结构与分期计划。当前结论是：正式编辑器采用 **Tiptap + ProseMirror JSON** 作为核心编辑器方案，前端保持 React + TypeScript + CSS Modules 的现有技术栈，后端继续以 `document_versions.body` 的 `jsonb` 字段作为正文权威存储。

---

## 2. 设计原则

- **先稳定编辑内核，再补复杂协作**：首期优先完成单人编辑、保存、阅读渲染，不在一开始实现实时协同。
- **标题与正文分离**：文档标题作为文档元信息与版本快照字段，正文由 Tiptap 文档树表达；界面上保持“第一行就是标题”的体验。
- **JSON 为主存**：数据库保存 Tiptap/ProseMirror JSON；Markdown、HTML、纯文本仅作为导入导出、搜索和只读渲染的派生结果。
- **UI 自研，内核复用**：Tiptap 负责编辑器状态、选区、命令、schema、撤销重做等底层能力；语雀风格的工具栏、块菜单、`/` 菜单、自动保存状态由项目自行封装。
- **渐进扩展**：先覆盖常用文档块，再逐步加入表格、图片、附件、评论、历史版本、协同编辑。

---

## 3. 依赖选型

### 3.1 首期必需依赖

前端正式接入编辑器时建议新增以下依赖：

```json
{
  "@tiptap/react": "latest",
  "@tiptap/pm": "latest",
  "@tiptap/starter-kit": "latest",
  "@tiptap/extension-placeholder": "latest",
  "@tiptap/extension-underline": "latest",
  "@tiptap/extension-link": "latest",
  "@tiptap/extension-task-list": "latest",
  "@tiptap/extension-task-item": "latest",
  "@tiptap/extension-table": "latest",
  "@tiptap/extension-table-row": "latest",
  "@tiptap/extension-table-header": "latest",
  "@tiptap/extension-table-cell": "latest",
  "@tiptap/extension-image": "latest"
}
```

说明：

- `@tiptap/react`：React 集成入口，提供 `useEditor`、`EditorContent` 等能力。
- `@tiptap/pm`：Tiptap 对 ProseMirror 底层包的统一依赖入口。
- `@tiptap/starter-kit`：基础节点和 marks，包括段落、标题、列表、引用、代码块、粗体、斜体、删除线、撤销重做等。
- `@tiptap/extension-placeholder`：实现“请输入标题”“输入 / 唤起更多”等占位提示。
- `@tiptap/extension-underline`：补充下划线。
- `@tiptap/extension-link`：链接编辑与跳转。
- `@tiptap/extension-task-list`、`@tiptap/extension-task-item`：待办列表。
- 表格扩展：支撑语雀常见表格块。
- `@tiptap/extension-image`：首期先支持图片节点，上传能力另接附件服务。

### 3.2 二期可选依赖

```json
{
  "@tiptap/extension-color": "latest",
  "@tiptap/extension-text-style": "latest",
  "@tiptap/extension-highlight": "latest",
  "@tiptap/extension-character-count": "latest",
  "@dnd-kit/core": "latest",
  "@dnd-kit/sortable": "latest",
  "yjs": "latest"
}
```

说明：

- 颜色、高亮、文本样式：用于顶部格式工具栏。
- 字数统计：用于文档统计与状态栏。
- `@dnd-kit/*`：用于后续块拖拽排序；首期可先做块菜单，不做真实拖拽。
- `yjs`：用于未来实时协同编辑；首期不接入。

### 3.3 暂不引入

- 不在首期引入完整协同依赖和协同服务。
- 不在首期引入 Markdown 编辑器作为主编辑器。
- 不在首期引入大型 UI 编辑器套件，避免与 Tiptap schema 和项目 UI 风格冲突。

---

## 4. 功能范围

### 4.1 首期必须实现

- 文档编辑页：复用 `/r/:repoKey/d/:docKey` 路由，在 `DocumentPage` 内承载正式编辑器。
- 标题输入：页面第一行是标题，支持占位、编辑、保存。
- 正文编辑：支持段落、一级/二级/三级标题、无序列表、有序列表、引用、代码块、分割线。
- 基础格式：粗体、斜体、删除线、下划线、链接。
- 待办列表：支持勾选和取消勾选。
- 表格：支持插入表格、增加/删除行列。
- 图片：支持插入图片节点；首期可先使用 URL 插入，文件上传接附件模块后补齐。
- `/` 命令菜单：输入 `/` 后展示块类型菜单，选择后转换或插入对应节点。
- 浮动工具栏：选中文本后展示加粗、斜体、链接等常用操作。
- 自动保存提示：展示“已保存 / 保存中 / 保存失败”。
- 只读渲染：无编辑权限或阅读态时使用同一份 JSON 渲染为只读内容。

### 4.2 二期增强

- 块左侧操作菜单：转化为、删除、复制、剪切、缩进、在下方添加。
- 块拖拽排序：通过块 handle 进行排序。
- 图片上传：接 `attachments` 表与对象存储。
- 附件卡片：支持文件附件块。
- 目录锚点：从标题节点生成文档大纲。
- 评论锚点：评论绑定到文本范围或块 id。
- 历史版本：保存版本、查看版本、恢复版本。
- 导入导出：Markdown / HTML / PDF。

### 4.3 三期协同能力

- 实时多人编辑：基于 `Yjs` 和 WebSocket/SignalR。
- 在线用户光标：显示协作者光标与选区。
- 冲突处理：由 CRDT 层处理编辑冲突。
- 离线恢复：保存本地草稿，网络恢复后同步。

---

## 5. 前端组件设计

建议在前端新增编辑器模块：

```text
Yuque_Frontend/src/features/editor/
  components/
    DocumentEditor.tsx
    EditorToolbar.tsx
    SlashCommandMenu.tsx
    BubbleFormatMenu.tsx
    BlockActionMenu.tsx
    EditorSaveStatus.tsx
  extensions/
    yuqueDocument.ts
    yuquePlaceholder.ts
  hooks/
    useDocumentEditor.ts
    useAutoSaveDocument.ts
  types/
    editorTypes.ts
  editor.module.css
```

职责划分：

- `DocumentEditor`：编辑器入口组件，负责创建 Tiptap editor、渲染标题输入和正文。
- `EditorToolbar`：顶部工具栏，调用 Tiptap commands。
- `SlashCommandMenu`：`/` 命令菜单，负责节点插入与转换。
- `BubbleFormatMenu`：文本选区浮动菜单。
- `BlockActionMenu`：块左侧操作菜单，二期实现。
- `EditorSaveStatus`：展示保存状态。
- `useDocumentEditor`：封装 editor 初始化、extensions 配置、只读切换。
- `useAutoSaveDocument`：封装 debounce 自动保存、错误状态、TanStack Query mutation。

---

## 6. 数据结构设计

### 6.1 前端编辑态

标题与正文分开管理：

```ts
type DocumentEditorValue = {
  title: string;
  body: TiptapDocumentJson;
};

type TiptapDocumentJson = {
  type: 'doc';
  content?: unknown[];
};
```

`title` 用于页面第一行输入，并写入版本快照的 `title` 字段。`body` 是 Tiptap 的 ProseMirror JSON，写入 `document_versions.body`。

### 6.2 正文 JSON 示例

```json
{
  "type": "doc",
  "content": [
    {
      "type": "paragraph",
      "content": [{ "type": "text", "text": "共计 4 日的「广州」之旅。" }]
    },
    {
      "type": "heading",
      "attrs": { "level": 2 },
      "content": [{ "type": "text", "text": "行程时间" }]
    },
    {
      "type": "taskList",
      "content": [
        {
          "type": "taskItem",
          "attrs": { "checked": false },
          "content": [{ "type": "paragraph", "content": [{ "type": "text", "text": "预订酒店" }] }]
        }
      ]
    }
  ]
}
```

### 6.3 后端存储映射

与 `docs/database_schema.md` 保持一致：

- `documents.content_format`：建议使用 `tiptap_v1`。
- `document_versions.title`：保存标题快照。
- `document_versions.body`：保存 Tiptap JSON。
- `document_versions.body_search`：由服务端从 JSON 抽取纯文本后生成，用于全文检索。

---

## 7. 接口边界

首期建议接口形态如下：

```http
GET /api/repositories/{repoKey}/documents/{docKey}
```

返回：

```json
{
  "id": "123",
  "repoKey": "leo",
  "docKey": "travel-plan",
  "title": "旅行计划",
  "contentFormat": "tiptap_v1",
  "body": {
    "type": "doc",
    "content": []
  },
  "updatedAt": "2026-04-25T14:00:00Z",
  "canEdit": true
}
```

保存：

```http
PUT /api/repositories/{repoKey}/documents/{docKey}
```

请求体：

```json
{
  "title": "旅行计划",
  "contentFormat": "tiptap_v1",
  "body": {
    "type": "doc",
    "content": []
  }
}
```

首期保存策略：

- 前端编辑后 debounce 1-2 秒自动保存。
- 页面离开前如有未保存内容，提示用户。
- 后端每次保存更新当前版本或生成草稿版本，具体以版本策略定稿为准。
- 失败时保留前端内容并显示“保存失败，可重试”。

---

## 8. 编辑器交互设计

### 8.1 页面结构

```text
文档顶部栏：文档名 / 保存状态 / 更多操作
编辑区：
  标题输入
  正文编辑器
  / 命令菜单
  选区浮动工具栏
  块操作菜单（二期）
```

### 8.2 `/` 命令菜单

首期命令项：

- 正文
- 一级标题
- 二级标题
- 三级标题
- 无序列表
- 有序列表
- 待办事项
- 引用
- 代码块
- 分割线
- 表格
- 图片

实现方式：

- 监听 Tiptap editor state 中当前输入位置和 `/` 查询文本。
- 使用自定义 React 菜单渲染候选项。
- 选择命令后调用 `editor.chain().focus()...run()` 执行插入或转换。

### 8.3 工具栏

顶部工具栏用于全局格式入口，浮动工具栏用于选中文本后的快捷编辑。两者都只通过 Tiptap commands 修改文档，不直接操作 DOM。

### 8.4 阅读态

阅读态使用同一套 `EditorContent`，但设置 `editable: false`，隐藏工具栏、菜单和输入占位。后续如果性能需要，可再实现专门的只读渲染器。

---

## 9. 权限与状态

编辑器需要消费后端返回的权限字段：

- `canView`：是否可查看。
- `canEdit`：是否可编辑。
- `canComment`：是否可评论。
- `canManage`：是否可管理文档。

首期只要求：

- `canEdit = true` 时启用编辑器和自动保存。
- `canEdit = false` 时进入只读态。

后续评论、分享、版本恢复等能力再根据权限细分。

---

## 10. 风险与约束

- Tiptap 能提供编辑器内核，但语雀风格 UI 需要项目自行实现。
- 块拖拽、评论锚点、协同编辑属于复杂能力，不应与首期基础编辑器一次性交付。
- `document_versions.body` 一旦保存为 `tiptap_v1`，后续 schema 变更需要迁移策略。
- 图片上传依赖附件服务和对象存储，首期只做 URL 图片或占位入口更稳。
- 自动保存需要处理并发保存、失败重试和页面离开提示，否则容易丢内容。

---

## 11. 分期计划

### 第一阶段：正式编辑器内核

- 安装 Tiptap 基础依赖。
- 新增 `features/editor` 模块。
- 用 Tiptap 替换当前 `DocumentPage` 的手写 textarea Demo。
- 实现标题、正文、基础格式、列表、引用、代码块、待办、表格、图片 URL。
- 实现只读态。

### 第二阶段：语雀式编辑体验

- 实现 `/` 命令菜单。
- 实现选区浮动工具栏。
- 实现顶部工具栏。
- 实现保存状态展示。
- 接入 TanStack Query mutation 做自动保存。

### 第三阶段：内容块增强

- 实现块左侧菜单。
- 实现块复制、删除、转换、在下方添加。
- 实现块拖拽排序。
- 实现标题生成目录。

### 第四阶段：后端持久化与版本

- 接入文档详情 API。
- 接入文档保存 API。
- 保存 `content_format = tiptap_v1`。
- 保存版本快照。
- 从 JSON 抽取纯文本，写入搜索索引字段。

### 第五阶段：协作与高级能力

- 评论锚点。
- 历史版本查看与恢复。
- 图片和附件上传。
- 导入导出。
- 基于 Yjs + WebSocket/SignalR 的实时协作。

---

## 12. 当前结论

正式编辑器建议采用 **Tiptap**，以 **ProseMirror JSON** 作为正文结构，保存到 `document_versions.body`。首期目标不是一次性复刻完整语雀，而是先完成稳定的单人编辑、结构化存储、只读渲染和自动保存基础，再逐步补齐语雀式块操作、表格图片、评论、版本与协同能力。
