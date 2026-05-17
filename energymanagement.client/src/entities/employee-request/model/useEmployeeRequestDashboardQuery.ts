import { useQuery } from "@tanstack/react-query";
import { listEmployeeDashboardRequests } from "../api/listEmployeeDashboardRequests";
import type { EmployeeRequestDashboardFilters } from "./employeeRequestFilters";
import { employeeRequestQueryKeys } from "./employeeRequestQueryKeys";
import type { EmployeeRequestDashboardState } from "./employeeRequestTypes";

type UseEmployeeRequestDashboardQueryArgs = {
  filters?: EmployeeRequestDashboardFilters;
  enabled?: boolean;
};

export const useEmployeeRequestDashboardQuery = (
  args: UseEmployeeRequestDashboardQueryArgs = {},
) => {
  const filters = args.filters ?? {};

  return useQuery<EmployeeRequestDashboardState>({
    queryKey: employeeRequestQueryKeys.dashboard(filters),
    queryFn: async () => {
      const response = await listEmployeeDashboardRequests(filters);
      return (response.requests ?? []) as EmployeeRequestDashboardState;
    },
    enabled: args.enabled ?? true,
    retry: false,
  });
};
