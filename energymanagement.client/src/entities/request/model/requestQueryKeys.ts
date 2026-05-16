import type { MyRequestsFilters } from "./myRequestsFilters";

export const requestQueryKeys = {
  all: ["requests"] as const,
  myRequests: (filters: MyRequestsFilters = {}) =>
    [
      ...requestQueryKeys.all,
      "my",
      {
        status: filters.status ?? null,
      },
    ] as const,
  myRequestDetails: (requestId: number) =>
    [...requestQueryKeys.all, "my", requestId] as const,
} as const;
