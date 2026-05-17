import { fetchJson } from "../../../shared/api/fetchJson";
import type { AgreementExchangeDetailsResponseDto } from "./agreementExchangeApiTypes";

export const getAgreementExchangeDetails = (
  exchangeId: number,
): Promise<AgreementExchangeDetailsResponseDto> =>
  fetchJson<AgreementExchangeDetailsResponseDto>(
    `/api/agreement-exchanges/${encodeURIComponent(String(exchangeId))}`,
    {
      method: "GET",
    },
  );
