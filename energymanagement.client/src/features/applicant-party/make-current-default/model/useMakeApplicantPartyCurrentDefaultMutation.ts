import { useMutation, useQueryClient } from "@tanstack/react-query";
import { applicantPartyQueryKeys } from "../../../../entities/applicant-party/model/applicantPartyQueryKeys";
import { makeApplicantPartyCurrentDefault } from "../api/makeApplicantPartyCurrentDefault";

export const useMakeApplicantPartyCurrentDefaultMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<void, unknown, number>({
    mutationFn: makeApplicantPartyCurrentDefault,
    onSuccess: async () => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: applicantPartyQueryKeys.accountList,
        }),
        queryClient.invalidateQueries({
          queryKey: applicantPartyQueryKeys.currentIndividual,
        }),
      ]);
    },
  });
};
