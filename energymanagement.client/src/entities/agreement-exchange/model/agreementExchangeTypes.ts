import type {
  AgreementDocumentRefDto,
  AgreementExchangeDetailsResponseDto,
  AgreementExchangeListItemDto,
  AgreementExchangeRequestSummaryDto,
  AgreementProposalDetailsDto,
} from "../api/agreementExchangeApiTypes";

export type AgreementExchangeListItem = AgreementExchangeListItemDto;
export type AgreementExchangeListState = AgreementExchangeListItem[];

export type AgreementExchangeDetails = AgreementExchangeDetailsResponseDto;
export type AgreementExchangeRequestSummary = AgreementExchangeRequestSummaryDto;
export type AgreementProposalDetails = AgreementProposalDetailsDto;
export type AgreementDocumentRef = AgreementDocumentRefDto;

export type AgreementExchangeViewerRole = "Client" | "Employee";
