import type {
  EmployeeDashboardReviewState,
  EmployeeRequestListItemDto,
  EmployeeRequestListResponseDto,
  EmployeeRequestStatus,
} from "../api/employeeRequestApiTypes";

export type EmployeeRequestDashboardItem = EmployeeRequestListItemDto;
export type EmployeeRequestDashboardState = EmployeeRequestListResponseDto["requests"];
export type EmployeeRequestStatusValue = EmployeeRequestStatus;
export type EmployeeRequestReviewState = EmployeeDashboardReviewState;
