import type {
  EmployeeDashboardReviewState,
  EmployeeRequestListItemDto,
  EmployeeRequestStatus,
} from "../api/employeeRequestApiTypes";

export type EmployeeRequestDashboardItem = EmployeeRequestListItemDto & {
  requestId: number;
  reviewState: EmployeeDashboardReviewState;
};
export type EmployeeRequestDashboardState = EmployeeRequestDashboardItem[];
export type EmployeeRequestStatusValue = EmployeeRequestStatus;
export type EmployeeRequestReviewState = EmployeeDashboardReviewState;
