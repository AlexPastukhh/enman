import type { components } from "./generated/openapi-types";
import { fetchJson } from "./fetchJson";
import { l1ApiPaths } from "./l1ApiPaths";

export type L1MyRequestSummary = components["schemas"]["L1MyRequestSummaryDto"];
export type L1ListMyRequestsResponse = L1MyRequestSummary[];
export type L1MyRequestDetails = components["schemas"]["L1MyRequestDetailsDto"];
export type L1CreateConnectionRequestRequest =
  components["schemas"]["L1CreateConnectionRequestDto"];

export type L1ListMyRequestsParams = {
  status?: string;
};

const withQuery = (path: string, searchParams: URLSearchParams) => {
  const query = searchParams.toString();
  return query ? `${path}?${query}` : path;
};

export const listMyRequests = (
  params: L1ListMyRequestsParams = {},
): Promise<L1ListMyRequestsResponse> => {
  const searchParams = new URLSearchParams();

  if (params.status) {
    searchParams.set("status", params.status);
  }

  return fetchJson<L1ListMyRequestsResponse>(
    withQuery(l1ApiPaths.requests, searchParams),
    {
      method: "GET",
    },
  );
};

export const getMyRequestDetails = (
  requestId: number,
): Promise<L1MyRequestDetails> =>
  fetchJson<L1MyRequestDetails>(
    `${l1ApiPaths.requests}/${encodeURIComponent(String(requestId))}`,
    {
      method: "GET",
    },
  );


export const createConnectionRequest = (
  request: L1CreateConnectionRequestRequest,
): Promise<void> =>
  fetchJson<void>(l1ApiPaths.requests, {
    method: "POST",
    body: JSON.stringify(request),
  });
