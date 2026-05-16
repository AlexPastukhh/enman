import type { components } from "./generated/openapi-types";
import { fetchJson } from "./fetchJson";
import { l1ApiPaths } from "./l1ApiPaths";

export type L1MyRequestSummary = components["schemas"]["L1MyRequestSummaryDto"];
export type L1ListMyRequestsResponse = L1MyRequestSummary[];

export const listMyRequests = (): Promise<L1ListMyRequestsResponse> =>
  fetchJson<L1ListMyRequestsResponse>(l1ApiPaths.requests, {
    method: "GET",
  });
