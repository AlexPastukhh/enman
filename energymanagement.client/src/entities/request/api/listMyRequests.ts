import { fetchJson } from "../../../shared/api/fetchJson";
import type { MyRequestsFilters } from "../model/myRequestsFilters";
import type { ListMyRequestsResponse } from "./requestApiTypes";

const requestsPath = "/api/requests";

const withQuery = (path: string, searchParams: URLSearchParams) => {
  const query = searchParams.toString();
  return query ? `${path}?${query}` : path;
};

export const listMyRequests = (
  filters: MyRequestsFilters = {},
): Promise<ListMyRequestsResponse> => {
  const searchParams = new URLSearchParams();

  if (filters.status) {
    searchParams.set("status", filters.status);
  }

  return fetchJson<ListMyRequestsResponse>(
    withQuery(requestsPath, searchParams),
    {
      method: "GET",
    },
  );
};
