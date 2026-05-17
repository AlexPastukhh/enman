import { fetchJson } from "./fetchJson";

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

export const employeeRequestApiPaths = {
  requests: "/api/employee/requests",
  requestDetails: (requestId: number | string) =>
    `/api/employee/requests/${encodeURIComponent(String(requestId))}`,
} as const;

const withQuery = (path: string, searchParams: URLSearchParams) => {
  const query = searchParams.toString();
  return query ? `${path}?${query}` : path;
};

export const listEmployeeRequests = (
  query: EmployeeRequestListQuery = {},
): Promise<EmployeeRequestListResponseDto> => {
  const searchParams = new URLSearchParams();

  if (query.status) {
    searchParams.set("status", query.status);
  }

  if (query.reviewState) {
    searchParams.set("reviewState", query.reviewState);
  }

  return fetchJson<EmployeeRequestListResponseDto>(
    withQuery(employeeRequestApiPaths.requests, searchParams),
    {
      method: "GET",
    },
  );
};
