import { useQuery } from "@tanstack/react-query";
import { listAgreementExchanges } from "../api/listAgreementExchanges";
import type { AgreementExchangeListFilters } from "./agreementExchangeFilters";
import { agreementExchangeQueryKeys } from "./agreementExchangeQueryKeys";
import type { AgreementExchangeListState } from "./agreementExchangeTypes";

type UseAgreementExchangeListQueryArgs = {
  filters?: AgreementExchangeListFilters;
  enabled?: boolean;
};

export const useAgreementExchangeListQuery = (
  args: UseAgreementExchangeListQueryArgs = {},
) => {
  const filters = args.filters ?? {};

  return useQuery<AgreementExchangeListState>({
    queryKey: agreementExchangeQueryKeys.list(filters),
    queryFn: async () => {
      const response = await listAgreementExchanges(filters);
      return response.exchanges ?? [];
    },
    enabled: args.enabled ?? true,
    retry: false,
  });
};
