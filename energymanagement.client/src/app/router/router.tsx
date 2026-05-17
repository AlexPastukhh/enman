import { createBrowserRouter, useRouteError } from "react-router-dom";
import AccountPage from "../../pages/account/AccountPage";
import HomePage from "../../pages/home/HomePage";
import { LoginPage } from "../../pages/login/LoginPage";
import MyRequestDetailsPage from "../../pages/requests/details/MyRequestDetailsPage";
import EmployeeDashboardPage from "../../pages/employee/dashboard/EmployeeDashboardPage";
import CreateConnectionRequestPage from "../../pages/requests/create/CreateConnectionRequestPage";
import MyRequestsPage from "../../pages/requests/my/MyRequestsPage";
import RegisterPage from "../../pages/register/RegisterPage";
import { clientRoutes } from "../../shared/config/clientRoutes";
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

export const router = createBrowserRouter([
  {
    path: clientRoutes.home,
    element: <HomePage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.register,
    element: <RegisterPage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.login,
    element: <LoginPage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.account,
    element: <AccountPage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.requests,
    element: <MyRequestsPage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.employeeRequests,
    element: <EmployeeDashboardPage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.createRequest,
    element: <CreateConnectionRequestPage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.requestDetailsPath,
    element: <MyRequestDetailsPage />,
    errorElement: <RouteError />,
  },
  {
    path: clientRoutes.testUi,
    element: <TestSiteMockup />,
    errorElement: <RouteError />,
  },
]);
