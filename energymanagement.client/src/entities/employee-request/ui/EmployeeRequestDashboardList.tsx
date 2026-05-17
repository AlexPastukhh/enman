import type { ReactNode } from "react";
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
  renderRowActions?: (request: EmployeeRequestDashboardItem) => ReactNode;
};

export const EmployeeRequestDashboardList = ({
  requests,
  emptyStateVariant = "default",
  onResetFilters,
  renderRowActions,
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
          renderActions={renderRowActions}
        />
      ))}
    </div>
  );
};
