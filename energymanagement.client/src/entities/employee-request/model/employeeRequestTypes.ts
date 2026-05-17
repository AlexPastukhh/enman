import type {
  EmployeeDashboardReviewState,
  EmployeeRequestListItemDto,
  EmployeeRequestListResponseDto,
  EmployeeRequestStatus,
} from "../../../shared/api/employeeRequestApi";

export type EmployeeRequestDashboardItem = EmployeeRequestListItemDto;
export type EmployeeRequestDashboardState = EmployeeRequestListResponseDto["requests"];
export type EmployeeRequestStatusValue = EmployeeRequestStatus;
export type EmployeeRequestReviewState = EmployeeDashboardReviewState;
