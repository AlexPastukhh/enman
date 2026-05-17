import { employeeRequestDashboardConst } from "./employeeRequestDashboardConst";

export type EmployeeRequestDashboardEmptyStateVariant = "default" | "filtered";

type EmployeeRequestDashboardEmptyStateProps = {
  variant?: EmployeeRequestDashboardEmptyStateVariant;
  onResetFilters?: () => void;
};

export const EmployeeRequestDashboardEmptyState = ({
  variant = "default",
  onResetFilters,
}: EmployeeRequestDashboardEmptyStateProps) => {
  const isFiltered = variant === "filtered";

  return (
    <section
      className="employeeRequestDashboardEmpty"
      aria-labelledby="employee-request-dashboard-empty-title"
    >
      <h2 id="employee-request-dashboard-empty-title">
        {isFiltered
          ? employeeRequestDashboardConst.filteredEmptyTitle
          : employeeRequestDashboardConst.emptyTitle}
      </h2>
      <p>
        {isFiltered
          ? employeeRequestDashboardConst.filteredEmptyDescription
          : employeeRequestDashboardConst.emptyDescription}
      </p>
      {isFiltered && onResetFilters && (
        <button type="button" onClick={onResetFilters}>
          {employeeRequestDashboardConst.filteredEmptyResetText}
        </button>
      )}
    </section>
  );
};
