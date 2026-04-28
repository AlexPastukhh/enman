import { StrictMode, useEffect } from 'react'
import { createRoot } from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import './styles/index.css'
import './styles/layout.css'
import './styles/general.css'
import './styles/forms.css'
import HomeView from './views/HomeView/HomeView.tsx'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import RegisterView from './views/RegisterView/RegisterView.tsx'
import { LoginView } from './views/LoginView/LoginView.tsx'
import SessionProvider from './hooks/useSession.tsx'
import { ClientRoutes } from './globConstants.ts'
import AccountView from './views/AccountView/AccountView.tsx'
import { PageErrorProvider, usePageError } from './hooks/usePageError.tsx'
import { LayoutWrapper } from './Components/Layout/LayoutWrapper.tsx'

const router = createBrowserRouter(
[
  {
    path:ClientRoutes.Home.Path,
    element:<HomeView/>
  },
  {
    path:ClientRoutes.Register.Path,
    element:<RegisterView/>
 },
{
    path:ClientRoutes.Login.Path,
    element:<LoginView/>
 },
{
  path:ClientRoutes.Account.Path,
  element:<AccountView/>
}
]
);

const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
<StrictMode>
<QueryClientProvider client={queryClient}>
<PageErrorProvider>
<SessionProvider>
  <LayoutWrapper>
    <RouterProvider router={router}/>
  </LayoutWrapper>
</SessionProvider>
</PageErrorProvider>
</QueryClientProvider>
</StrictMode>
)
