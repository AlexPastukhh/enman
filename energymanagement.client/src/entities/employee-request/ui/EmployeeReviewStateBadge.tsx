import type { EmployeeRequestReviewState } from "../model/employeeRequestTypes";
import { employeeRequestDashboardConst } from "./employeeRequestDashboardConst";

type EmployeeReviewStateBadgeProps = {
  reviewState: EmployeeRequestReviewState;
};

export const EmployeeReviewStateBadge = ({
  reviewState,
}: EmployeeReviewStateBadgeProps) => (
  <span className="employeeReviewStateBadge">
    {employeeRequestDashboardConst.reviewStateLabels[reviewState] ?? reviewState}
  </span>
);
