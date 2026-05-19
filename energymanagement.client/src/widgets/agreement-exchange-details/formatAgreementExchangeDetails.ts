import type { AgreementDocumentRef } from "../../entities/agreement-exchange/model/agreementExchangeTypes";

const statusLabels: Record<string, string> = {
  AwaitingClientConfirmation: "Ожидает подтверждения клиента",
  AwaitingEmployeeResponse: "Ожидает ответа сотрудника",
  Accepted: "Принято",
  FinallyRefused: "Финально отклонено",
};

const senderLabels: Record<string, string> = {
  Client: "Клиент",
  Employee: "Сотрудник",
};

const proposalStateLabels: Record<string, string> = {
  Active: "Активное",
  Superseded: "Заменено встречным предложением",
  Accepted: "Принято",
  FinallyRefused: "Финально отклонено",
};

const requestStatusLabels: Record<string, string> = {
  InReview: "На рассмотрении",
  Approved: "Одобрена",
  Rejected: "Отклонена",
  AgreementExchangeFailed: "Договорной обмен не завершён",
};

export const formatAgreementExchangeStatus = (status: string): string =>
  statusLabels[status] ?? status;

export const formatProposalSender = (sender: string, senderId: number): string =>
  `${senderLabels[sender] ?? sender} #${senderId}`;

export const formatProposalVersion = (version: number, sender: string): string =>
  `Версия ${version}, автор: ${senderLabels[sender] ?? sender}`;

export const formatProposalState = (state: string): string =>
  proposalStateLabels[state] ?? state;

export const formatRequestStatus = (status: string): string =>
  requestStatusLabels[status] ?? status;

export const formatDocumentSize = (sizeBytes: number): string => {
  if (sizeBytes < 1024) {
    return `${sizeBytes} Б`;
  }

  if (sizeBytes < 1024 * 1024) {
    return `${(sizeBytes / 1024).toFixed(1)} КБ`;
  }

  return `${(sizeBytes / 1024 / 1024).toFixed(1)} МБ`;
};

export const formatDocumentRef = (document: AgreementDocumentRef): string =>
  `${document.originalFileName} (${document.contentType}, ${formatDocumentSize(
    document.sizeBytes,
  )})`;
