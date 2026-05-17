import { fetchJson } from "../../../shared/api/fetchJson";
import type { EmployeeRequestDashboardFilters } from "../model/employeeRequestFilters";
import type {
  EmployeeRequestListQuery,
  EmployeeRequestListResponseDto,
} from "./employeeRequestApiTypes";

const employeeRequestsPath = "/api/employee/requests";

const withQuery = (path: string, searchParams: URLSearchParams) => {
  const query = searchParams.toString();
  return query ? `${path}?${query}` : path;
};

export const listEmployeeDashboardRequests = (
  filters: EmployeeRequestDashboardFilters = {},
): Promise<EmployeeRequestListResponseDto> => {
  const query: EmployeeRequestListQuery = {
    status: filters.status,
    reviewState: filters.reviewState,
  };
  const searchParams = new URLSearchParams();

  if (query.status) {
    searchParams.set("status", query.status);
  }

  if (query.reviewState) {
    searchParams.set("reviewState", query.reviewState);
  }

  return fetchJson<EmployeeRequestListResponseDto>(
    withQuery(employeeRequestsPath, searchParams),
    {
      method: "GET",
    },
  );
};
