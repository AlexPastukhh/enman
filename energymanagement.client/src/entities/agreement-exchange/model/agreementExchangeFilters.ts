import type { AgreementExchangeStatus } from "../api/agreementExchangeApiTypes";

export type AgreementExchangeListFilters = {
  status?: AgreementExchangeStatus;
};

export const agreementExchangeStatuses: AgreementExchangeStatus[] = [
  "AwaitingClientConfirmation",
  "AwaitingEmployeeResponse",
  "Accepted",
  "FinallyRefused",
];

export const hasActiveAgreementExchangeFilters = (
  filters: AgreementExchangeListFilters = {},
) => Boolean(filters.status);
