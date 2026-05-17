import { useMutation, useQueryClient } from "@tanstack/react-query";
import { agreementExchangeQueryKeys } from "../../../../entities/agreement-exchange/model/agreementExchangeQueryKeys";
import { employeeRequestQueryKeys } from "../../../../entities/employee-request/model/employeeRequestQueryKeys";
import { startAgreementExchange } from "../api/startAgreementExchange";
import type {
  StartAgreementExchangeRequest,
  StartAgreementExchangeResponse,
} from "../api/startAgreementExchangeApiTypes";

const invalidateRelatedReads = async (
  queryClient: ReturnType<typeof useQueryClient>,
  requestId: number,
) => {
  await Promise.all([
    queryClient.invalidateQueries({
      queryKey: employeeRequestQueryKeys.details(requestId),
    }),
    queryClient.invalidateQueries({
      queryKey: employeeRequestQueryKeys.all,
    }),
    queryClient.invalidateQueries({
      queryKey: agreementExchangeQueryKeys.all,
    }),
  ]);
};

export const useStartAgreementExchangeMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<
    StartAgreementExchangeResponse,
    unknown,
    StartAgreementExchangeRequest
  >({
    mutationFn: startAgreementExchange,
    onSuccess: async (_data, variables) => {
      await invalidateRelatedReads(queryClient, variables.requestId);
    },
    onError: async (_error, variables) => {
      await invalidateRelatedReads(queryClient, variables.requestId);
    },
  });
};
