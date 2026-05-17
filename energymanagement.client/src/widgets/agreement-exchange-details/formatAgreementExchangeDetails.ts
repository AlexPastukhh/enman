import type { AgreementDocumentRef } from "../../entities/agreement-exchange/model/agreementExchangeTypes";

const statusLabels: Record<string, string> = {
  AwaitingClientConfirmation: "Awaiting client confirmation",
  AwaitingEmployeeResponse: "Awaiting employee response",
  Accepted: "Accepted",
  FinallyRefused: "Finally refused",
};

export const formatAgreementExchangeStatus = (status: string): string =>
  statusLabels[status] ?? status;

export const formatProposalSender = (sender: string, senderId: number): string =>
  `${sender} #${senderId}`;

export const formatProposalVersion = (version: number, sender: string): string =>
  `Version ${version} from ${sender}`;

export const formatDocumentSize = (sizeBytes: number): string => {
  if (sizeBytes < 1024) {
    return `${sizeBytes} B`;
  }

  if (sizeBytes < 1024 * 1024) {
    return `${(sizeBytes / 1024).toFixed(1)} KB`;
  }

  return `${(sizeBytes / 1024 / 1024).toFixed(1)} MB`;
};

export const formatDocumentRef = (document: AgreementDocumentRef): string =>
  `${document.originalFileName} (${document.contentType}, ${formatDocumentSize(
    document.sizeBytes,
  )})`;
