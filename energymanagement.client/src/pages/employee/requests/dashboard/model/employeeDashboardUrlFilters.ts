import {
  isEmployeeDashboardReviewState,
  isEmployeeRequestStatus,
  type EmployeeRequestDashboardFilters,
} from "../../../../../entities/employee-request/model/employeeRequestFilters";

export type ParseEmployeeDashboardUrlFiltersResult = {
  filters: EmployeeRequestDashboardFilters;
  invalidFilterReason: string | null;
};

export const parseEmployeeDashboardUrlFilters = (
  searchParams: URLSearchParams,
): ParseEmployeeDashboardUrlFiltersResult => {
  const filters: EmployeeRequestDashboardFilters = {};
  const status = searchParams.get("status");
  const reviewState = searchParams.get("reviewState");

  if (status) {
    if (!isEmployeeRequestStatus(status)) {
      return {
        filters: {},
        invalidFilterReason: "Unknown employee request status filter.",
      };
    }

    filters.status = status;
  }

  if (reviewState) {
    if (!isEmployeeDashboardReviewState(reviewState)) {
      return {
        filters: {},
        invalidFilterReason: "Unknown employee review state filter.",
      };
    }

    filters.reviewState = reviewState;
  }

  return { filters, invalidFilterReason: null };
};

export const serializeEmployeeDashboardUrlFilters = (
  filters: EmployeeRequestDashboardFilters,
) => {
  const searchParams = new URLSearchParams();

  if (filters.status) {
    searchParams.set("status", filters.status);
  }

  if (filters.reviewState) {
    searchParams.set("reviewState", filters.reviewState);
  }

  return searchParams;
};
