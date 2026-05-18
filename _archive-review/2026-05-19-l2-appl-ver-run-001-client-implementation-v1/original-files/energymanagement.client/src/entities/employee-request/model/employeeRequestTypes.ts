import type {
  EmployeeDashboardReviewState,
  EmployeeRequestDetailsDto,
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

export type EmployeeRequestDetails = EmployeeRequestDetailsDto & {
  requestId: number;
  reviewState: EmployeeRequestReviewState;
};

export type EmployeeReviewActionAvailability = {
  canStartReview: boolean;
  canApproveReview: boolean;
  canRejectReview: boolean;
  reason: string | null;
};
