import { useQuery } from "@tanstack/react-query";
import { getEmployeeRequestDetails } from "../api/getEmployeeRequestDetails";
import { employeeRequestQueryKeys } from "./employeeRequestQueryKeys";
import type { EmployeeRequestDetails } from "./employeeRequestTypes";

type UseEmployeeRequestDetailsQueryArgs = {
  requestId: number;
  enabled?: boolean;
};

export const useEmployeeRequestDetailsQuery = ({
  requestId,
  enabled = true,
}: UseEmployeeRequestDetailsQueryArgs) =>
  useQuery<EmployeeRequestDetails>({
    queryKey: employeeRequestQueryKeys.details(requestId),
    queryFn: async () =>
      (await getEmployeeRequestDetails(requestId)) as EmployeeRequestDetails,
    enabled,
    retry: false,
  });
