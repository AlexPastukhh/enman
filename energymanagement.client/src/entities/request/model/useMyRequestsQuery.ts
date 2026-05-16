import { useQuery } from "@tanstack/react-query";
import { listMyRequests } from "../api/listMyRequests";
import type { MyRequestsFilters } from "./myRequestsFilters";
import { requestQueryKeys } from "./requestQueryKeys";
import type { MyRequestsState } from "./requestTypes";

type UseMyRequestsQueryArgs = {
  filters?: MyRequestsFilters;
  enabled?: boolean;
};

export const useMyRequestsQuery = (
  args: UseMyRequestsQueryArgs | boolean = {},
) => {
  const resolvedArgs = typeof args === "boolean" ? { enabled: args } : args;
  const filters = resolvedArgs.filters ?? {};

  return useQuery<MyRequestsState>({
    queryKey: requestQueryKeys.myRequests(filters),
    queryFn: () => listMyRequests(filters),
    enabled: resolvedArgs.enabled ?? true,
    retry: false,
  });
};
