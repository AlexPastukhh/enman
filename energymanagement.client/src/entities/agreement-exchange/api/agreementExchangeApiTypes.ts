export type AgreementExchangeStatus =
  | "AwaitingClientConfirmation"
  | "AwaitingEmployeeResponse"
  | "Accepted"
  | "FinallyRefused";

export type AgreementProposalSender = "Employee" | "Client";

export type AgreementExchangeListQuery = {
  status?: AgreementExchangeStatus;
};

export type AgreementExchangeListItemDto = {
  exchangeId: number;
  requestId: number;
  exchangeStatus: string;
  activeProposalVersion: number;
  activeProposalSender: string;
  activeProposalSenderId: number;
  requestDisplayName: string;
  objectAddress: string;
  createdAt: string;
  lastActivityAt?: string | null;
};

export type AgreementExchangeListResponseDto = {
  exchanges?: AgreementExchangeListItemDto[] | null;
};
