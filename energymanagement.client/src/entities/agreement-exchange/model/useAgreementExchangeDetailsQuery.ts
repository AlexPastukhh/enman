import { useQuery } from "@tanstack/react-query";
import { getAgreementExchangeDetails } from "../api/getAgreementExchangeDetails";
import { agreementExchangeQueryKeys } from "./agreementExchangeQueryKeys";
import type { AgreementExchangeDetails } from "./agreementExchangeTypes";

type UseAgreementExchangeDetailsQueryArgs = {
  exchangeId: number;
  enabled?: boolean;
};

export const useAgreementExchangeDetailsQuery = ({
  exchangeId,
  enabled = true,
}: UseAgreementExchangeDetailsQueryArgs) =>
  useQuery<AgreementExchangeDetails>({
    queryKey: agreementExchangeQueryKeys.details(exchangeId),
    queryFn: async () => await getAgreementExchangeDetails(exchangeId),
    enabled: enabled && Number.isFinite(exchangeId) && exchangeId > 0,
    retry: false,
  });
