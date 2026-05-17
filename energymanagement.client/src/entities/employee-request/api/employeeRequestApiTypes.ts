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

export type EmployeeRequestListItemDto = {
  requestId: number;
  requestType: "Connection" | string;
  status: EmployeeRequestStatus;
  applicantDisplayName: string;
  objectAddress: string;
  createdAt: string;
  reviewState: EmployeeDashboardReviewState;
};

export type EmployeeRequestListResponseDto = {
  requests: EmployeeRequestListItemDto[];
};
