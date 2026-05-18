import { useMutation, useQueryClient } from "@tanstack/react-query";
import { employeeRequestQueryKeys } from "../../../../entities/employee-request/model/employeeRequestQueryKeys";
import {
  runApplicantPartyVerification,
} from "../api/runApplicantPartyVerification";
import type { RunApplicantPartyVerificationResponse } from "../api/applicantVerificationApiTypes";

export const useRunApplicantPartyVerificationMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<RunApplicantPartyVerificationResponse, unknown, number>({
    mutationFn: runApplicantPartyVerification,
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
