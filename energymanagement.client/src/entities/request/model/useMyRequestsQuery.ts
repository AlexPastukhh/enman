import { useQuery } from "@tanstack/react-query";
import { listMyRequests } from "../api/listMyRequests";
import { requestQueryKeys } from "./requestQueryKeys";
import type { MyRequestsState } from "./requestTypes";

export const useMyRequestsQuery = (enabled: boolean) =>
  useQuery<MyRequestsState>({
    queryKey: requestQueryKeys.myRequests,
    queryFn: listMyRequests,
    enabled,
    retry: false,
  });
