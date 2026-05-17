import type { EmployeeRequestDashboardFilters } from "./employeeRequestFilters";

export const employeeRequestQueryKeys = {
  all: ["employee-requests"] as const,
  dashboard: (filters: EmployeeRequestDashboardFilters = {}) =>
    [
      ...employeeRequestQueryKeys.all,
      "dashboard",
      {
        status: filters.status ?? null,
        reviewState: filters.reviewState ?? null,
      },
    ] as const,
  details: (requestId: number) =>
    [...employeeRequestQueryKeys.all, "details", requestId] as const,
} as const;
