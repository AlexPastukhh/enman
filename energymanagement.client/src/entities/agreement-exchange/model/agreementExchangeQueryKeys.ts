import type { AgreementExchangeListFilters } from "./agreementExchangeFilters";

export const agreementExchangeQueryKeys = {
  all: ["agreement-exchanges"] as const,
  list: (filters: AgreementExchangeListFilters = {}) =>
    [
      ...agreementExchangeQueryKeys.all,
      "list",
      {
        status: filters.status ?? null,
      },
    ] as const,
} as const;
