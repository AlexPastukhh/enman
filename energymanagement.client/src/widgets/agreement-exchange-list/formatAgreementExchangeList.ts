import type { AgreementExchangeListItem } from "../../entities/agreement-exchange/model/agreementExchangeTypes";
import { agreementExchangeListConst } from "./agreementExchangeListConst";

export const formatAgreementExchangeStatus = (status: string): string => {
  switch (status) {
    case "AwaitingClientConfirmation":
      return "Awaiting client confirmation";
    case "AwaitingEmployeeResponse":
      return "Awaiting employee response";
    case "Accepted":
      return "Accepted";
    case "FinallyRefused":
      return "Finally refused";
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
      return "Client";
    case "Employee":
      return "Employee";
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

  return `Version ${exchange.activeProposalVersion} from ${formatProposalSender(
    exchange.activeProposalSender,
  )}`;
};

export const formatDateTime = (value: string): string => {
  const date = new Date(value);

  if (Number.isNaN(date.getTime())) {
    return value;
  }

  return date.toLocaleString();
};

export const getAgreementExchangeTitle = (
  exchange: AgreementExchangeListItem,
): string => exchange.requestDisplayName?.trim() || `Request #${exchange.requestId}`;
