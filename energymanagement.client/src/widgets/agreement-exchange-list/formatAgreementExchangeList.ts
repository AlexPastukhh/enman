import type { AgreementExchangeListItem } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeListConst } from "./agreementExchangeListConst";

export const formatAgreementExchangeStatus = (status: string): string => {
  switch (status) {
    case "AwaitingClientConfirmation":
      return "Ожидает подтверждения клиента";
    case "AwaitingEmployeeResponse":
      return "Ожидает ответа сотрудника";
    case "Accepted":
      return "Принято";
    case "FinallyRefused":
      return "Финально отклонено";
    default:
      return status;
  }
};

export const formatProposalSender = (
  sender: AgreementExchangeListItem["activeProposalSender"],
): string => {
  if (!sender) {
    return agreementExchangeListConst.unknownSenderText;
  }

  switch (sender) {
    case "Client":
      return "Клиент";
    case "Employee":
      return "Сотрудник";
    default:
      return String(sender);
  }
};

export const formatProposalSummary = (
  exchange: AgreementExchangeListItem,
): string => {
  if (exchange.activeProposalVersion === null) {
    return agreementExchangeListConst.noActiveProposalText;
  }

  return `Версия ${exchange.activeProposalVersion}, автор: ${formatProposalSender(
    exchange.activeProposalSender,
  )}`;
};

export const formatDateTime = (value?: string | null): string => {
  if (!value) {
    return "—";
  }
  const date = new Date(value);

  if (Number.isNaN(date.getTime())) {
    return value;
  }

  return new Intl.DateTimeFormat("ru-RU", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(date);
};

export const getAgreementExchangeTitle = (
  exchange: AgreementExchangeListItem,
): string =>
  exchange.requestDisplayName?.trim() || `Заявка #${exchange.requestId}`;
