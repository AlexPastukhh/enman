import { createBrowserRouter, Outlet, useRouteError } from "react-router-dom";
import AccountPage from "../../pages/account/AccountPage";
import ClientAgreementExchangeDetailsPage from "../../pages/agreements/details/ClientAgreementExchangeDetailsPage";
import ClientAgreementExchangesPage from "../../pages/agreements/my/ClientAgreementExchangesPage";
import EmployeeAgreementExchangeDetailsPage from "../../pages/employee/agreements/details/EmployeeAgreementExchangeDetailsPage";
import EmployeeAgreementExchangesDashboardPage from "../../pages/employee/agreements/dashboard/EmployeeAgreementExchangesDashboardPage";
import EmployeeDashboardPage from "../../pages/employee/requests/dashboard/EmployeeDashboardPage";
import EmployeeRequestDetailsPage from "../../pages/employee/requests/details/EmployeeRequestDetailsPage";
import HomePage from "../../pages/home/HomePage";
import { LoginPage } from "../../pages/login/LoginPage";
import MyRequestDetailsPage from "../../pages/requests/details/MyRequestDetailsPage";
import CreateConnectionRequestPage from "../../pages/requests/create/CreateConnectionRequestPage";
import MyRequestsPage from "../../pages/requests/my/MyRequestsPage";
import RegisterPage from "../../pages/register/RegisterPage";
import { clientRoutes } from "../../shared/config/clientRoutes";
import { LayoutWrapper } from "../../shared/ui/layout/LayoutWrapper";
import TestSiteMockup from "../../test-site-mockup/TestSiteMockup";

type RouterError = {
  message?: string;
};

const RouteError = () => {
  const error = useRouteError() as RouterError;
  return (
    <div role="alert">
      {String(error.message ?? "Unexpected route error")}
    </div>
  );
};

const AppShellRoute = () => (
  <LayoutWrapper>
    <Outlet />
  </LayoutWrapper>
);

export const router = createBrowserRouter([
  {
    path: clientRoutes.home,
    element: <AppShellRoute />,
    errorElement: <RouteError />,
    children: [
      {
        index: true,
        element: <HomePage />,
      },
      {
        path: clientRoutes.register,
        element: <RegisterPage />,
      },
      {
        path: clientRoutes.login,
        element: <LoginPage />,
      },
      {
        path: clientRoutes.account,
        element: <AccountPage />,
      },
      {
        path: clientRoutes.requests,
        element: <MyRequestsPage />,
      },
      {
        path: clientRoutes.employeeRequests,
        element: <EmployeeDashboardPage />,
      },
      {
        path: clientRoutes.agreementExchanges,
        element: <ClientAgreementExchangesPage />,
      },
      {
        path: clientRoutes.agreementExchangeDetailsPath,
        element: <ClientAgreementExchangeDetailsPage />,
      },
      {
        path: clientRoutes.employeeAgreementExchanges,
        element: <EmployeeAgreementExchangesDashboardPage />,
      },
      {
        path: clientRoutes.employeeAgreementExchangeDetailsPath,
        element: <EmployeeAgreementExchangeDetailsPage />,
      },
      {
        path: clientRoutes.employeeRequestDetailsPath,
        element: <EmployeeRequestDetailsPage />,
      },
      {
        path: clientRoutes.createRequest,
        element: <CreateConnectionRequestPage />,
      },
      {
        path: clientRoutes.requestDetailsPath,
        element: <MyRequestDetailsPage />,
      },
      {
        path: clientRoutes.testUi,
        element: <TestSiteMockup />,
      },
    ],
  },
]);
