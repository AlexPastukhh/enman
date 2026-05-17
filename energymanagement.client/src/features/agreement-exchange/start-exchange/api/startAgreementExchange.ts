import { fetchJson } from "../../../../shared/api/fetchJson";
import type {
  StartAgreementExchangeRequest,
  StartAgreementExchangeResponse,
} from "./startAgreementExchangeApiTypes";

export const startAgreementExchange = (
  payload: StartAgreementExchangeRequest,
): Promise<StartAgreementExchangeResponse> =>
  fetchJson<StartAgreementExchangeResponse>("/api/agreement-exchanges", {
    method: "POST",
    body: JSON.stringify(payload),
  });
