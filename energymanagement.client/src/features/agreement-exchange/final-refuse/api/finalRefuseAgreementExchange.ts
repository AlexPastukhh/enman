import { fetchJson } from "../../../../shared/api/fetchJson";
import type { FinalRefuseAgreementExchangeRequest } from "./finalRefuseAgreementExchangeApiTypes";

export type FinalRefuseAgreementExchangeInput = {
  exchangeId: number;
  payload?: FinalRefuseAgreementExchangeRequest;
};

const finalRefuseAgreementExchangePath = (exchangeId: number | string) =>
  `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}/final-refuse`;

export const finalRefuseAgreementExchange = ({
  exchangeId,
  payload,
}: FinalRefuseAgreementExchangeInput): Promise<void> =>
  fetchJson<void>(
    finalRefuseAgreementExchangePath(exchangeId),
    payload === undefined
      ? { method: "POST" }
      : {
          method: "POST",
          body: JSON.stringify(payload),
        },
  );
