import type { EmployeeRequestDashboardItem } from "../model/employeeRequestTypes";
import {
  EmployeeRequestDashboardEmptyState,
  type EmployeeRequestDashboardEmptyStateVariant,
} from "./EmployeeRequestDashboardEmptyState";
import { EmployeeRequestDashboardRow } from "./EmployeeRequestDashboardRow";
import { employeeRequestDashboardConst } from "./employeeRequestDashboardConst";
import "./employeeRequestDashboard.css";

type EmployeeRequestDashboardListProps = {
  requests: EmployeeRequestDashboardItem[];
  emptyStateVariant?: EmployeeRequestDashboardEmptyStateVariant;
  onResetFilters?: () => void;
};

export const EmployeeRequestDashboardList = ({
  requests,
  emptyStateVariant = "default",
  onResetFilters,
}: EmployeeRequestDashboardListProps) => {
  if (requests.length === 0) {
    return (
      <EmployeeRequestDashboardEmptyState
        variant={emptyStateVariant}
        onResetFilters={onResetFilters}
      />
    );
  }

  return (
    <div
      className="employeeRequestDashboardList"
      aria-label={employeeRequestDashboardConst.listLabel}
    >
      {requests.map((request) => (
        <EmployeeRequestDashboardRow
          key={request.requestId}
          request={request}
        />
      ))}
    </div>
  );
};
