import { fetchJson } from "../../../shared/api/fetchJson";
import type { MyRequestsFilters } from "../model/myRequestsFilters";
import type { L1ListMyRequestsResponse } from "./requestApiTypes";

const requestsPath = "/api/l1/requests";

const withQuery = (path: string, searchParams: URLSearchParams) => {
  const query = searchParams.toString();
  return query ? `${path}?${query}` : path;
};

export const listMyRequests = (
  filters: MyRequestsFilters = {},
): Promise<L1ListMyRequestsResponse> => {
  const searchParams = new URLSearchParams();

  if (filters.status) {
    searchParams.set("status", filters.status);
  }

  return fetchJson<L1ListMyRequestsResponse>(
    withQuery(requestsPath, searchParams),
    {
      method: "GET",
    },
  );
};
