# Frontend Shell Design

## Context

The backend database migration and knowledge-domain entities are already in place under `Yuque_Backend`. The next step is to create a separate frontend application in `Yuque_Frontend`, keeping the project front-end/back-end separated.

The frontend should follow the existing project documents:

- `docs/technical_stack.md`
- `docs/frontend_information_architecture.md`
- `docs/yuque_core_features.md`
- `docs/Prototyping/*`

## Goals

Create a runnable first frontend shell that establishes the product structure and main UI surfaces before real API integration.

The first pass should include:

- Vite + React + TypeScript project in `Yuque_Frontend`.
- Ant Design, React Router, Zustand, TanStack Query, and CSS Modules.
- Global app providers and route configuration.
- Main authenticated shell matching the Yuque-style prototypes.
- Public login page.
- Core static pages backed by local mock data.
- A small API/client boundary that can later be switched from mock data to backend HTTP calls.

## Non-Goals

This first pass will not implement:

- Real backend API integration.
- Full document editor behavior.
- Authentication persistence against the backend.
- Realtime collaboration.
- Search indexing or real search API.
- Uploads, object storage, or attachment handling.
- Complete mobile responsive behavior beyond basic layout resilience.

## Architecture

The frontend project will live at:

```text
Yuque_Frontend/
```

Recommended high-level structure:

```text
src/
  app/
    App.tsx
    providers.tsx
    router.tsx
  layouts/
    AppShellLayout/
    PublicLayout/
    FavoritesLayout/
    NotesLayout/
    RepoWorkspaceLayout/
  pages/
    LoginPage/
    HomePage/
    KnowledgeBasesPage/
    RepositoryPage/
    NotesPage/
    FavoritesPage/
    SettingsProfilePage/
    PlaceholderPage/
  features/
    shell/
    knowledge/
    notes/
    favorites/
    settings/
  services/
    apiClient.ts
    mockData.ts
  stores/
    appStore.ts
  styles/
    global.css
```

This keeps page composition, reusable feature components, service boundaries, and global state separate from the start.

## Routes

The first shell should implement these routes:

| Route | Layout | Purpose |
| --- | --- | --- |
| `/login` | `PublicLayout` | Login/register visual surface based on the prototype. |
| `/` | redirect | Redirect to `/home`. |
| `/home` | `AppShellLayout` | Start page with quick actions and recent document list. |
| `/knowledge-bases` | `AppShellLayout` | Knowledge-base overview page. |
| `/r/:repoKey` | `RepoWorkspaceLayout` | Knowledge-base home and catalog surface. |
| `/r/:repoKey/d/:docKey` | `RepoWorkspaceLayout` | Document view placeholder. |
| `/notes` | `NotesLayout` | Notes list and editor-style reading surface. |
| `/notes/:noteId` | `NotesLayout` | Selected note view. |
| `/favorites` | `FavoritesLayout` | Favorites table/list page. |
| `/settings/profile` | `SettingsProfilePage` | User profile settings surface. |

Other planned routes from the documentation can use a shared placeholder page so navigation is complete without implying finished functionality.

## UI Direction

The main interface should match the prototypes rather than a generic Ant Design admin dashboard:

- Left global navigation with logo, search shortcut, create button, primary entries, knowledge-base section, and bottom utility area.
- Main content should be quiet, information-dense, and work-focused.
- Ant Design is used for low-level controls, forms, dropdowns, buttons, tooltips, segmented controls, and lists.
- Core document/product surfaces should use custom CSS Modules to better match the Yuque-like layout.
- Avoid decorative landing-page treatment. The first authenticated screen is the actual workbench.

## Mock Data

Use local mock data for:

- Current user.
- Knowledge bases.
- Repository entries and grouped catalog items.
- Recent documents.
- Favorites.
- Notes.

Mock data should live behind service functions, not directly inside page components. This makes later replacement with real HTTP calls localized.

## State And Data Flow

Use Zustand only for light client state:

- Current workspace/user context.
- Sidebar collapsed or selected state if needed.
- Visible theme/language controls may be shown where they exist in the prototypes, but they do not need to change runtime behavior in the first pass.

Use TanStack Query for data access even while using mock data:

- Query keys should model the future API surfaces, such as `knowledgeBases`, `repository(repoKey)`, `notes`, and `favorites`.
- Mock service calls can return promises to preserve async loading and error boundaries.

## API Boundary

Create a minimal API client file with a base URL read from Vite env:

```text
VITE_API_BASE_URL
```

The first implementation can keep pages on mock services. The HTTP client exists so future API integration does not require changing layout/page structure.

## Error And Empty States

The shell should include practical states where easy:

- Route-level fallback for unknown pages.
- Empty-state components for missing repository, note, or document.
- Basic loading states through TanStack Query.

## Testing And Verification

Initial verification should include:

- Dependency install succeeds.
- TypeScript build succeeds.
- Vite dev server starts.
- Key routes render without runtime errors.
- A browser pass checks desktop layout for `/login`, `/home`, `/knowledge-bases`, `/r/demo`, `/notes`, `/favorites`, and `/settings/profile`.

## Acceptance Criteria

The first frontend increment is complete when:

- `Yuque_Frontend` exists as an independent frontend project.
- The app runs locally with Vite.
- Core routes and layouts render using mock data.
- The UI follows the supplied Yuque-style prototypes at the shell/layout level.
- The code is structured so real backend API integration can be added without replacing the UI skeleton.
