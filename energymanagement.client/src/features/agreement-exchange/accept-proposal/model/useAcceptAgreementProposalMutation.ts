import { useMutation, useQueryClient } from "@tanstack/react-query";
import { agreementExchangeQueryKeys } from "../../../../entities/agreement-exchange/model/agreementExchangeQueryKeys";
import { acceptAgreementProposal } from "../api/acceptAgreementProposal";

export const useAcceptAgreementProposalMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<void, unknown, number>({
    mutationFn: acceptAgreementProposal,
    onSuccess: async (_data, exchangeId) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.details(exchangeId),
        }),
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.all,
        }),
      ]);
    },
    onError: async (_error, exchangeId) => {
      await Promise.all([
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.details(exchangeId),
        }),
        queryClient.invalidateQueries({
          queryKey: agreementExchangeQueryKeys.all,
        }),
      ]);
    },
  });
};
