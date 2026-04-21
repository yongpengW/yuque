import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { routes } from '../../constants/routes'
import { AppLayout } from '../../layouts/AppLayout'
import { DashboardPage } from '../../pages/app/DashboardPage'
import { DocumentListPage } from '../../pages/app/documents/DocumentListPage'
import { RepositoryListPage } from '../../pages/app/repositories/RepositoryListPage'
import { HomePage } from '../../pages/public/HomePage'

const router = createBrowserRouter([
  {
    path: routes.home,
    element: <HomePage />,
  },
  {
    path: routes.dashboard,
    element: <AppLayout />,
    children: [
      {
        index: true,
        element: <DashboardPage />,
      },
      {
        path: routes.repositories.replace('/app/', ''),
        element: <RepositoryListPage />,
      },
      {
        path: routes.documents.replace('/app/', ''),
        element: <DocumentListPage />,
      },
    ],
  },
])

export function AppRouter() {
  return <RouterProvider router={router} />
}
