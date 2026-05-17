import { useMutation, useQueryClient } from "@tanstack/react-query";
import { employeeRequestQueryKeys } from "../../../../entities/employee-request/model/employeeRequestQueryKeys";
import {
  rejectRequestReview,
  type RejectRequestReviewInput,
} from "../api/rejectRequestReview";

export const useRejectRequestReviewMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<void, unknown, RejectRequestReviewInput>({
    mutationFn: rejectRequestReview,
    onSuccess: async (_data, variables) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.details(variables.requestId),
        }),
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.all,
        }),
      ]);
    },
    onError: async (_error, variables) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.details(variables.requestId),
        }),
        queryClient.invalidateQueries({
          queryKey: employeeRequestQueryKeys.all,
        }),
      ]);
    },
  });
};
