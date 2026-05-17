export type AgreementExchangeStatus =
  | "AwaitingClientConfirmation"
  | "AwaitingEmployeeResponse"
  | "Accepted"
  | "FinallyRefused";

export type AgreementProposalSender = "Client" | "Employee";

export type AgreementExchangeListItemDto = {
  requestId: number;
  exchangeId: number;
  exchangeStatus: AgreementExchangeStatus | string;
  activeProposalVersion: number | null;
  activeProposalSender: AgreementProposalSender | string | null;
  activeProposalSenderId: number | null;
  requestDisplayName?: string | null;
  objectAddress?: string | null;
  createdAt: string;
  lastActivityAt: string;
};

export type AgreementExchangeListResponseDto = {
  exchanges?: AgreementExchangeListItemDto[] | null;
};
