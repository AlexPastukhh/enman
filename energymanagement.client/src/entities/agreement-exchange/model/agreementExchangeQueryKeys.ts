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
  details: (exchangeId: number) =>
    [...agreementExchangeQueryKeys.all, "details", exchangeId] as const,
} as const;
