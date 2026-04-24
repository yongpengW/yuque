# Frontend Shell Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build the first independent `Yuque_Frontend` React application with the main Yuque-style shell, routes, mock data, and static product surfaces.

**Architecture:** The app is a separate Vite React project under `Yuque_Frontend`. Layouts, pages, feature components, services, and stores are split into focused folders. Mock data is accessed through async service functions and TanStack Query so later backend API integration can replace only the service layer.

**Tech Stack:** Vite, React, TypeScript, Ant Design, React Router, Zustand, TanStack Query, CSS Modules, lucide-react.

---

### Task 1: Scaffold Frontend Project

**Files:**
- Create: `Yuque_Frontend/package.json`
- Create: `Yuque_Frontend/index.html`
- Create: `Yuque_Frontend/tsconfig.json`
- Create: `Yuque_Frontend/tsconfig.node.json`
- Create: `Yuque_Frontend/vite.config.ts`
- Create: `Yuque_Frontend/src/main.tsx`
- Create: `Yuque_Frontend/src/app/App.tsx`
- Create: `Yuque_Frontend/src/app/providers.tsx`
- Create: `Yuque_Frontend/src/styles/global.css`

- [ ] **Step 1: Create Vite React TypeScript project files**

Create the project using Vite's React TypeScript template or equivalent files. `package.json` must include scripts for `dev`, `build`, `preview`, and `typecheck`.

- [ ] **Step 2: Install runtime dependencies**

Run from `Yuque_Frontend`:

```powershell
npm install
npm install @ant-design/icons @tanstack/react-query antd axios lucide-react react-router-dom zustand
```

Expected: dependencies are installed and `package-lock.json` is created.

- [ ] **Step 3: Add global providers**

`src/app/providers.tsx` should wrap children in `ConfigProvider` and `QueryClientProvider`.

- [ ] **Step 4: Verify empty app builds**

Run:

```powershell
npm run build
```

Expected: Vite build completes without TypeScript errors.

### Task 2: Add Mock Data And Service Boundary

**Files:**
- Create: `Yuque_Frontend/src/services/apiClient.ts`
- Create: `Yuque_Frontend/src/services/mockData.ts`
- Create: `Yuque_Frontend/src/services/knowledgeService.ts`
- Create: `Yuque_Frontend/src/types/domain.ts`
- Create: `Yuque_Frontend/src/stores/appStore.ts`

- [ ] **Step 1: Define domain types**

Add types for `User`, `KnowledgeBase`, `DocumentItem`, `CatalogGroup`, `NoteItem`, and `FavoriteItem`.

- [ ] **Step 2: Add mock data**

Create realistic static data matching the prototypes: current user `Leo`, one personal knowledge base, grouped catalog entries, recent documents, favorites, and notes.

- [ ] **Step 3: Add async service functions**

Expose promise-returning functions such as `getCurrentUser`, `getKnowledgeBases`, `getRepository`, `getRecentDocuments`, `getNotes`, and `getFavorites`.

- [ ] **Step 4: Add API client shell**

Create an Axios instance reading `import.meta.env.VITE_API_BASE_URL`, defaulting to `/api`.

- [ ] **Step 5: Add lightweight Zustand app store**

Store current workspace key and sidebar state only.

### Task 3: Build Router And Layouts

**Files:**
- Create: `Yuque_Frontend/src/app/router.tsx`
- Create: `Yuque_Frontend/src/layouts/PublicLayout/PublicLayout.tsx`
- Create: `Yuque_Frontend/src/layouts/PublicLayout/PublicLayout.module.css`
- Create: `Yuque_Frontend/src/layouts/AppShellLayout/AppShellLayout.tsx`
- Create: `Yuque_Frontend/src/layouts/AppShellLayout/AppShellLayout.module.css`
- Create: `Yuque_Frontend/src/layouts/FavoritesLayout/FavoritesLayout.tsx`
- Create: `Yuque_Frontend/src/layouts/FavoritesLayout/FavoritesLayout.module.css`
- Create: `Yuque_Frontend/src/layouts/NotesLayout/NotesLayout.tsx`
- Create: `Yuque_Frontend/src/layouts/NotesLayout/NotesLayout.module.css`
- Create: `Yuque_Frontend/src/layouts/RepoWorkspaceLayout/RepoWorkspaceLayout.tsx`
- Create: `Yuque_Frontend/src/layouts/RepoWorkspaceLayout/RepoWorkspaceLayout.module.css`

- [ ] **Step 1: Configure React Router**

Routes must include `/login`, `/`, `/home`, `/knowledge-bases`, `/r/:repoKey`, `/r/:repoKey/d/:docKey`, `/notes`, `/notes/:noteId`, `/favorites`, `/settings/profile`, and `*`.

- [ ] **Step 2: Implement `AppShellLayout`**

Match the prototype's left navigation with logo, search, create button, main nav entries, knowledge-base list, membership hint, and bottom utility.

- [ ] **Step 3: Implement nested layouts**

`FavoritesLayout`, `NotesLayout`, and `RepoWorkspaceLayout` add their own secondary columns inside the shell. Each layout must render an `<Outlet />` for its page content.

- [ ] **Step 4: Verify route rendering**

Run the dev server and manually visit the planned routes. Each route should render a layout and no blank screen.

### Task 4: Build Core Pages

**Files:**
- Create: `Yuque_Frontend/src/pages/LoginPage/LoginPage.tsx`
- Create: `Yuque_Frontend/src/pages/LoginPage/LoginPage.module.css`
- Create: `Yuque_Frontend/src/pages/HomePage/HomePage.tsx`
- Create: `Yuque_Frontend/src/pages/HomePage/HomePage.module.css`
- Create: `Yuque_Frontend/src/pages/KnowledgeBasesPage/KnowledgeBasesPage.tsx`
- Create: `Yuque_Frontend/src/pages/KnowledgeBasesPage/KnowledgeBasesPage.module.css`
- Create: `Yuque_Frontend/src/pages/RepositoryPage/RepositoryPage.tsx`
- Create: `Yuque_Frontend/src/pages/RepositoryPage/RepositoryPage.module.css`
- Create: `Yuque_Frontend/src/pages/DocumentPage/DocumentPage.tsx`
- Create: `Yuque_Frontend/src/pages/DocumentPage/DocumentPage.module.css`
- Create: `Yuque_Frontend/src/pages/NotesPage/NotesPage.tsx`
- Create: `Yuque_Frontend/src/pages/NotesPage/NotesPage.module.css`
- Create: `Yuque_Frontend/src/pages/FavoritesPage/FavoritesPage.tsx`
- Create: `Yuque_Frontend/src/pages/FavoritesPage/FavoritesPage.module.css`
- Create: `Yuque_Frontend/src/pages/SettingsProfilePage/SettingsProfilePage.tsx`
- Create: `Yuque_Frontend/src/pages/SettingsProfilePage/SettingsProfilePage.module.css`
- Create: `Yuque_Frontend/src/pages/PlaceholderPage/PlaceholderPage.tsx`

- [ ] **Step 1: Build login page**

Match the centered card prototype with logo, phone/password-like inputs, verification controls, agreement text, and alternative login row.

- [ ] **Step 2: Build home page**

Add quick action cards and recent document table/list using mock data.

- [ ] **Step 3: Build knowledge-base overview**

Add common section, personal/invited segmented control, knowledge-base card, create dropdown, and view toggle controls.

- [ ] **Step 4: Build repository page**

Show repository title, document/word stats, favorite/share/more actions, welcome text, and grouped document list.

- [ ] **Step 5: Build document placeholder page**

Render selected document title and body preview placeholder within repository layout.

- [ ] **Step 6: Build notes page**

Use the notes layout list plus editor-like content area and toolbar.

- [ ] **Step 7: Build favorites page**

Use table-like columns for name, owner, favorite time, and star state.

- [ ] **Step 8: Build settings profile page**

Implement left settings navigation and profile form surface matching the prototype.

### Task 5: Polish And Verify

**Files:**
- Modify: `Yuque_Frontend/src/styles/global.css`
- Modify: relevant CSS module files under `Yuque_Frontend/src`
- Modify: `Yuque_Frontend/README.md`

- [ ] **Step 1: Add responsive guards**

Ensure the main layouts do not overlap at desktop widths and degrade acceptably on narrower screens.

- [ ] **Step 2: Add README**

Document install, dev, build, and API env variable:

```powershell
npm install
npm run dev
npm run build
```

- [ ] **Step 3: Run build verification**

Run from `Yuque_Frontend`:

```powershell
npm run build
```

Expected: TypeScript and Vite build pass.

- [ ] **Step 4: Start dev server**

Run:

```powershell
npm run dev -- --host 127.0.0.1
```

Expected: local Vite URL is available.

- [ ] **Step 5: Browser smoke test**

Open and inspect:

- `/login`
- `/home`
- `/knowledge-bases`
- `/r/leo-knowledge`
- `/r/leo-knowledge/d/bgk-micro40`
- `/notes`
- `/favorites`
- `/settings/profile`

Expected: routes render without runtime errors, visible UI follows the provided prototypes, and major controls do not overlap.
