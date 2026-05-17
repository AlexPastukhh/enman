import { fetchJson } from "../../../shared/api/fetchJson";
import type {
  AgreementExchangeListQuery,
  AgreementExchangeListResponseDto,
} from "./agreementExchangeApiTypes";

const agreementExchangesPath = "/api/agreement-exchanges";

const withQuery = (path: string, searchParams: URLSearchParams) => {
  const query = searchParams.toString();
  return query ? `${path}?${query}` : path;
};

export const listAgreementExchanges = (
  query: AgreementExchangeListQuery = {},
): Promise<AgreementExchangeListResponseDto> => {
  const searchParams = new URLSearchParams();

  if (query.status) {
    searchParams.set("status", query.status);
  }

  return fetchJson<AgreementExchangeListResponseDto>(
    withQuery(agreementExchangesPath, searchParams),
    {
      method: "GET",
    },
  );
};
