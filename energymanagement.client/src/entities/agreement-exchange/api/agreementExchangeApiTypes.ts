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

export type AgreementExchangeRequestSummaryDto = {
  requestId: number;
  requestStatus: string;
  requestDisplayName?: string | null;
  objectAddress?: string | null;
};

export type AgreementDocumentRefDto = {
  storageKey: string;
  originalFileName: string;
  contentType: string;
  sizeBytes: number;
};

export type AgreementProposalDetailsDto = {
  proposalId: number;
  version: number;
  sender: string;
  senderId: number;
  state: string;
  document?: AgreementDocumentRefDto | null;
  comment?: string | null;
  createdAt: string;
};

export type AgreementExchangeDetailsResponseDto = {
  exchangeId: number;
  requestId: number;
  exchangeStatus: string;
  activeProposalVersion?: number | null;
  request: AgreementExchangeRequestSummaryDto;
  activeProposal?: AgreementProposalDetailsDto | null;
  proposals?: AgreementProposalDetailsDto[] | null;
  currentActorSide: string;
  createdAt: string;
  lastActivityAt?: string | null;
};
