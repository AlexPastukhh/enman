import { useQuery } from "@tanstack/react-query";
import { getMyRequestDetails } from "../api/getMyRequestDetails";
import { requestQueryKeys } from "./requestQueryKeys";
import type { MyRequestDetails } from "./requestTypes";

type UseMyRequestDetailsQueryArgs = {
  requestId: number;
  enabled?: boolean;
};

export const useMyRequestDetailsQuery = ({
  requestId,
  enabled = true,
}: UseMyRequestDetailsQueryArgs) =>
  useQuery<MyRequestDetails>({
    queryKey: requestQueryKeys.myRequestDetails(requestId),
    queryFn: () => getMyRequestDetails(requestId),
    enabled,
    retry: false,
  });
