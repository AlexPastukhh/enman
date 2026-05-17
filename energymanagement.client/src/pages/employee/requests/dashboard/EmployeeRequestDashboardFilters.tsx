import {
  employeeRequestStatusOptions,
  employeeReviewStateOptions,
  type EmployeeRequestDashboardFilters as EmployeeRequestDashboardFiltersState,
} from "../../../../entities/employee-request/model/employeeRequestFilters";
import { employeeRequestDashboardConst } from "../../../../entities/employee-request/ui/employeeRequestDashboardConst";

type EmployeeRequestDashboardFiltersProps = {
  filters: EmployeeRequestDashboardFiltersState;
  onChange(filters: EmployeeRequestDashboardFiltersState): void;
  onReset(): void;
};

export const EmployeeRequestDashboardFilters = ({
  filters,
  onChange,
  onReset,
}: EmployeeRequestDashboardFiltersProps) => {
  const handleStatusChange = (value: string) => {
    onChange({
      ...filters,
      status: value
        ? (value as EmployeeRequestDashboardFiltersState["status"])
        : undefined,
    });
  };

  const handleReviewStateChange = (value: string) => {
    onChange({
      ...filters,
      reviewState: value
        ? (value as EmployeeRequestDashboardFiltersState["reviewState"])
        : undefined,
    });
  };

  return (
    <form className="employeeDashboardFilters" aria-label="Employee dashboard filters">
      <h2>{employeeRequestDashboardConst.filtersTitle}</h2>
      <label>
        {employeeRequestDashboardConst.statusFilterLabel}
        <select
          value={filters.status ?? ""}
          onChange={(event) => handleStatusChange(event.target.value)}
        >
          <option value="">
            {employeeRequestDashboardConst.allStatusesOption}
          </option>
          {employeeRequestStatusOptions.map((status) => (
            <option key={status} value={status}>
              {status}
            </option>
          ))}
        </select>
      </label>
      <label>
        {employeeRequestDashboardConst.reviewStateFilterLabel}
        <select
          value={filters.reviewState ?? ""}
          onChange={(event) => handleReviewStateChange(event.target.value)}
        >
          <option value="">
            {employeeRequestDashboardConst.allReviewStatesOption}
          </option>
          {employeeReviewStateOptions.map((reviewState) => (
            <option key={reviewState} value={reviewState}>
              {employeeRequestDashboardConst.reviewStateLabels[reviewState]}
            </option>
          ))}
        </select>
      </label>
      <button type="button" onClick={onReset}>
        {employeeRequestDashboardConst.resetFiltersText}
      </button>
    </form>
  );
};
