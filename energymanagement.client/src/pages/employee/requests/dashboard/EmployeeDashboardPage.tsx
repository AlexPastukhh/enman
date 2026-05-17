import { Link, useSearchParams } from "react-router-dom";
import {
  hasActiveEmployeeRequestDashboardFilters,
  type EmployeeRequestDashboardFilters as EmployeeRequestDashboardFiltersState,
} from "../../../../entities/employee-request/model/employeeRequestFilters";
import { useEmployeeRequestDashboardQuery } from "../../../../entities/employee-request/model/useEmployeeRequestDashboardQuery";
import { EmployeeRequestDashboardList } from "../../../../entities/employee-request/ui/EmployeeRequestDashboardList";
import { employeeRequestDashboardConst } from "../../../../entities/employee-request/ui/employeeRequestDashboardConst";
import { useSession } from "../../../../entities/session/model/useSession";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { Footer } from "../../../../shared/ui/layout/Footer";
import { Header } from "../../../../shared/ui/layout/Header";
import { EmployeeRequestDashboardFilters } from "./EmployeeRequestDashboardFilters";
import {
  parseEmployeeDashboardUrlFilters,
  serializeEmployeeDashboardUrlFilters,
} from "./model/employeeDashboardUrlFilters";
import "./employeeDashboardPage.css";

const isEmployeeSession = (role?: string | null) => role === "Employee";

const EmployeeDashboardPage = () => {
  const session = useSession();
  const [searchParams, setSearchParams] = useSearchParams();
  const parsedFilters = parseEmployeeDashboardUrlFilters(searchParams);
  const filters = parsedFilters.filters;
  const hasInvalidFilters = Boolean(parsedFilters.invalidFilterReason);
  const hasActiveFilters = hasActiveEmployeeRequestDashboardFilters(filters);
  const isEmployee = isEmployeeSession(session?.role);

  const dashboardQuery = useEmployeeRequestDashboardQuery({
    filters,
    enabled: Boolean(session) && isEmployee && !hasInvalidFilters,
  });

  const handleFiltersChange = (nextFilters: EmployeeRequestDashboardFiltersState) => {
    setSearchParams(serializeEmployeeDashboardUrlFilters(nextFilters));
  };

  const handleResetFilters = () => {
    setSearchParams(new URLSearchParams());
  };

  return (
    <>
      <Header />
      <main className="content">
        <section
          className="employeeDashboardPage"
          aria-labelledby="employee-dashboard-heading"
        >
          <h1 id="employee-dashboard-heading">
            {employeeRequestDashboardConst.pageTitle}
          </h1>
          <p>{employeeRequestDashboardConst.pageDescription}</p>

          {!session && (
            <div className="employeeDashboardPage__state">
              <h2>{employeeRequestDashboardConst.signInRequiredTitle}</h2>
              <p>{employeeRequestDashboardConst.signInRequiredDescription}</p>
              <Link to={clientRoutes.login}>
                {employeeRequestDashboardConst.signInLinkText}
              </Link>
            </div>
          )}

          {session && !isEmployee && (
            <div className="employeeDashboardPage__state" role="alert">
              <h2>{employeeRequestDashboardConst.accessDeniedTitle}</h2>
              <p>{employeeRequestDashboardConst.accessDeniedDescription}</p>
            </div>
          )}

          {session && isEmployee && (
            <EmployeeRequestDashboardFilters
              filters={filters}
              onChange={handleFiltersChange}
              onReset={handleResetFilters}
            />
          )}

          {session && isEmployee && hasInvalidFilters && (
            <div className="employeeDashboardPage__state" role="alert">
              <p>{parsedFilters.invalidFilterReason}</p>
              <button type="button" onClick={handleResetFilters}>
                {employeeRequestDashboardConst.invalidFiltersResetText}
              </button>
            </div>
          )}

          {session && isEmployee && !hasInvalidFilters && dashboardQuery.isPending && (
            <p className="employeeDashboardPage__state">
              {employeeRequestDashboardConst.loadingText}
            </p>
          )}

          {session && isEmployee && !hasInvalidFilters && dashboardQuery.isError && (
            <p className="employeeDashboardPage__state" role="alert">
              {employeeRequestDashboardConst.errorText}
            </p>
          )}

          {session && isEmployee && !hasInvalidFilters && dashboardQuery.data && (
            <EmployeeRequestDashboardList
              requests={dashboardQuery.data}
              emptyStateVariant={hasActiveFilters ? "filtered" : "default"}
              onResetFilters={handleResetFilters}
            />
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default EmployeeDashboardPage;
