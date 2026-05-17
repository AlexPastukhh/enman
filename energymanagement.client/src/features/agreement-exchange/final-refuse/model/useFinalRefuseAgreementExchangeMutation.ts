import { useMutation, useQueryClient } from "@tanstack/react-query";
import { agreementExchangeQueryKeys } from "../../../../entities/agreement-exchange/model/agreementExchangeQueryKeys";
import { employeeRequestQueryKeys } from "../../../../entities/employee-request/model/employeeRequestQueryKeys";
import {
  finalRefuseAgreementExchange,
  type FinalRefuseAgreementExchangeInput,
} from "../api/finalRefuseAgreementExchange";

const invalidateRelatedReads = async (
  queryClient: ReturnType<typeof useQueryClient>,
  variables: FinalRefuseAgreementExchangeInput & { requestId?: number },
) => {
  const invalidations = [
    queryClient.invalidateQueries({
      queryKey: agreementExchangeQueryKeys.details(variables.exchangeId),
    }),
    queryClient.invalidateQueries({
      queryKey: agreementExchangeQueryKeys.all,
    }),
  ];

  if (variables.requestId !== undefined) {
    invalidations.push(
      queryClient.invalidateQueries({
        queryKey: employeeRequestQueryKeys.details(variables.requestId),
      }),
    );
  }

  await Promise.all(invalidations);
};

export const useFinalRefuseAgreementExchangeMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<
    void,
    unknown,
    FinalRefuseAgreementExchangeInput & { requestId?: number }
  >({
    mutationFn: finalRefuseAgreementExchange,
    onSuccess: async (_data, variables) => {
      await invalidateRelatedReads(queryClient, variables);
    },
    onError: async (_error, variables) => {
      await invalidateRelatedReads(queryClient, variables);
    },
  });
};
