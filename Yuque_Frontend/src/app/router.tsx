import { createBrowserRouter, Navigate } from 'react-router-dom';
import { AppShellLayout } from '../layouts/AppShellLayout/AppShellLayout';
import { FavoritesLayout } from '../layouts/FavoritesLayout/FavoritesLayout';
import { NotesLayout } from '../layouts/NotesLayout/NotesLayout';
import { PublicLayout } from '../layouts/PublicLayout/PublicLayout';
import { RepoWorkspaceLayout } from '../layouts/RepoWorkspaceLayout/RepoWorkspaceLayout';
import { DocumentPage } from '../pages/DocumentPage/DocumentPage';
import { FavoritesPage } from '../pages/FavoritesPage/FavoritesPage';
import { HomePage } from '../pages/HomePage/HomePage';
import { KnowledgeBasesPage } from '../pages/KnowledgeBasesPage/KnowledgeBasesPage';
import { LoginPage } from '../pages/LoginPage/LoginPage';
import { NotesPage } from '../pages/NotesPage/NotesPage';
import { PlaceholderPage } from '../pages/PlaceholderPage/PlaceholderPage';
import { RepositoryPage } from '../pages/RepositoryPage/RepositoryPage';
import { SettingsProfilePage } from '../pages/SettingsProfilePage/SettingsProfilePage';

export const router = createBrowserRouter([
  {
    element: <PublicLayout />,
    children: [{ path: '/login', element: <LoginPage /> }],
  },
  { path: '/settings/profile', element: <SettingsProfilePage /> },
  {
    element: <AppShellLayout />,
    children: [
      { path: '/', element: <Navigate to="/home" replace /> },
      { path: '/home', element: <HomePage /> },
      { path: '/knowledge-bases', element: <KnowledgeBasesPage /> },
      { path: '/ai-writing', element: <PlaceholderPage title="AI 写作" /> },
      { path: '/traffic', element: <PlaceholderPage title="流量" /> },
      { path: '/recycle-bin', element: <PlaceholderPage title="回收站" /> },
      { path: '/search', element: <PlaceholderPage title="搜索" /> },
      {
        element: <FavoritesLayout />,
        children: [{ path: '/favorites', element: <FavoritesPage /> }],
      },
      {
        element: <NotesLayout />,
        children: [
          { path: '/notes', element: <NotesPage /> },
          { path: '/notes/:noteId', element: <NotesPage /> },
        ],
      },
      {
        element: <RepoWorkspaceLayout />,
        children: [
          { path: '/r/:repoKey', element: <RepositoryPage /> },
          { path: '/r/:repoKey/d/:docKey', element: <DocumentPage /> },
          { path: '/r/:repoKey/settings', element: <PlaceholderPage title="知识库设置" /> },
        ],
      },
    ],
  },
  { path: '*', element: <PlaceholderPage title="页面不存在" /> },
]);
