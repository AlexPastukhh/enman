import { fetchJson } from "../../../shared/api/fetchJson";
import type { AgreementExchangeListResponseDto } from "./agreementExchangeApiTypes";

const agreementExchangesPath = "/api/agreement-exchanges";

export const listAgreementExchanges = (): Promise<AgreementExchangeListResponseDto> =>
  fetchJson<AgreementExchangeListResponseDto>(agreementExchangesPath, {
    method: "GET",
  });
