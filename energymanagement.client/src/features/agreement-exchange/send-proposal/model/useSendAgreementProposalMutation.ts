import { useMutation, useQueryClient } from "@tanstack/react-query";
import { agreementExchangeQueryKeys } from "../../../../entities/agreement-exchange/model/agreementExchangeQueryKeys";
import {
  sendAgreementProposal,
  type SendAgreementProposalInput,
} from "../api/sendAgreementProposal";

export const useSendAgreementProposalMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<void, unknown, SendAgreementProposalInput>({
    mutationFn: sendAgreementProposal,
    onSuccess: async (_data, variables) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.details(variables.exchangeId),
        }),
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.all,
        }),
      ]);
    },
    onError: async (_error, variables) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.details(variables.exchangeId),
        }),
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.all,
        }),
      ]);
    },
  });
};
