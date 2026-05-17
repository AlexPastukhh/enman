import type { components } from "../../../shared/api/generated/openapi-types";

export type EmployeeRequestStatus =
  | "InReview"
  | "Approved"
  | "Rejected"
  | "AgreementExchangeFailed";

export type EmployeeDashboardReviewState =
  | "NotStarted"
  | "StartedByCurrentEmployee"
  | "StartedByAnotherEmployee"
  | "Approved"
  | "Rejected";

export type EmployeeRequestListQuery = {
  status?: EmployeeRequestStatus;
  reviewState?: EmployeeDashboardReviewState;
};

export type EmployeeRequestListItemDto =
  components["schemas"]["EmployeeRequestListItemDto"];

export type EmployeeRequestListResponseDto =
  components["schemas"]["EmployeeRequestListResponseDto"];

export type EmployeeRequestDetailsDto =
  components["schemas"]["EmployeeRequestDetailsDto"];

export type EmployeeRequestApplicantSummaryDto =
  components["schemas"]["EmployeeRequestApplicantSummaryDto"];
