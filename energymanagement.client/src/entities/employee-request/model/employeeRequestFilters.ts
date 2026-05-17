import type {
  EmployeeDashboardReviewState,
  EmployeeRequestStatus,
} from "../../../shared/api/employeeRequestApi";

export type EmployeeRequestDashboardFilters = {
  status?: EmployeeRequestStatus;
  reviewState?: EmployeeDashboardReviewState;
};

export const employeeRequestStatusOptions: EmployeeRequestStatus[] = [
  "InReview",
  "Approved",
  "Rejected",
  "AgreementExchangeFailed",
];

export const employeeReviewStateOptions: EmployeeDashboardReviewState[] = [
  "NotStarted",
  "StartedByCurrentEmployee",
  "StartedByAnotherEmployee",
  "Approved",
  "Rejected",
];

export const isEmployeeRequestStatus = (
  value: string,
): value is EmployeeRequestStatus =>
  employeeRequestStatusOptions.includes(value as EmployeeRequestStatus);

export const isEmployeeDashboardReviewState = (
  value: string,
): value is EmployeeDashboardReviewState =>
  employeeReviewStateOptions.includes(value as EmployeeDashboardReviewState);

export const hasActiveEmployeeRequestDashboardFilters = (
  filters: EmployeeRequestDashboardFilters,
) => Boolean(filters.status || filters.reviewState);
