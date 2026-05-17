import { useMutation, useQueryClient } from "@tanstack/react-query";
import { employeeRequestQueryKeys } from "../../../../entities/employee-request/model/employeeRequestQueryKeys";
import { startRequestReview } from "../api/startRequestReview";

export const useStartRequestReviewMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<void, unknown, number>({
    mutationFn: startRequestReview,
    onSuccess: async (_data, requestId) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.details(requestId),
        }),
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.all,
        }),
      ]);
    },
    onError: async (_error, requestId) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.details(requestId),
        }),
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.all,
        }),
      ]);
    },
  });
};
