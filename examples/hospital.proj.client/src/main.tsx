import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import './index.css'

// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";
// Bootstrap Bundle JS
import "bootstrap/dist/js/bootstrap.bundle.min";

import HomePage from './pages/HomePage.tsx'
import NotFoundPage from './pages/NotFoundPage.tsx'
import LoginPage from './pages/LoginPage.tsx'
import RegisterPage from './pages/RegisterPage.tsx'
import { routes } from './Utils/routes.ts'
import ActivateAccountpage from './pages/ActivateAccountPage.tsx'
import AccountActivatedPage from './pages/AccountActivatedPage.tsx'

const router = createBrowserRouter([{
    path:routes.home,
    element:<HomePage/>,
    errorElement: <NotFoundPage/>
  },
  {
    path:routes.register,
    element:<RegisterPage/>
  },
  {
    path:routes.login,
    element:<LoginPage/>
    },
    {
        path: routes.activate,
        element: <ActivateAccountpage />
    },
    {
        path: routes.activated,
        element: <AccountActivatedPage />
    },
]);

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router} />
  </StrictMode>,
)
