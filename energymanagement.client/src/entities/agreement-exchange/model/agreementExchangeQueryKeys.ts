export const agreementExchangeQueryKeys = {
  all: ["agreement-exchanges"] as const,
  list: () => [...agreementExchangeQueryKeys.all, "list"] as const,
};
