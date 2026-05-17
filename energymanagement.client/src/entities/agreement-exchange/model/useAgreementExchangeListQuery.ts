import { useQuery } from "@tanstack/react-query";
import { listAgreementExchanges } from "../api/listAgreementExchanges";
import { agreementExchangeQueryKeys } from "./agreementExchangeQueryKeys";
import type { AgreementExchangeListState } from "./agreementExchangeTypes";

type UseAgreementExchangeListQueryArgs = {
  enabled?: boolean;
};

export const useAgreementExchangeListQuery = (
  args: UseAgreementExchangeListQueryArgs = {},
) =>
  useQuery<AgreementExchangeListState>({
    queryKey: agreementExchangeQueryKeys.list(),
    queryFn: async () => {
      const response = await listAgreementExchanges();
      return response.exchanges ?? [];
    },
    enabled: args.enabled ?? true,
    retry: false,
  });
