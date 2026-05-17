import {
  listEmployeeRequests as listEmployeeRequestsApi,
  type EmployeeRequestListResponseDto,
} from "../../../shared/api/employeeRequestApi";
import type { EmployeeRequestDashboardFilters } from "../model/employeeRequestFilters";

export const listEmployeeDashboardRequests = (
  filters: EmployeeRequestDashboardFilters = {},
): Promise<EmployeeRequestListResponseDto> =>
  listEmployeeRequestsApi({
    status: filters.status,
    reviewState: filters.reviewState,
  });
