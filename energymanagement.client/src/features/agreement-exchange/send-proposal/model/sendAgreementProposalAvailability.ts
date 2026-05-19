import type {
  AgreementExchangeDetails,
  AgreementExchangeViewerRole,
} from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";

export type SendAgreementProposalAvailability = {
  canSendProposal: boolean;
  reason: string | null;
};

const completedStatuses = new Set(["Accepted", "FinallyRefused"]);

export const getSendAgreementProposalAvailability = (
  details: AgreementExchangeDetails,
  viewerRole: AgreementExchangeViewerRole,
): SendAgreementProposalAvailability => {
  if (completedStatuses.has(details.exchangeStatus)) {
    return {
      canSendProposal: false,
      reason: "Договорной обмен уже завершён.",
    };
  }

  if (details.currentActorSide !== viewerRole) {
    return {
      canSendProposal: false,
      reason: "Этот договорной обмен недоступен для текущей роли.",
    };
  }

  if (!details.activeProposal) {
    return {
      canSendProposal: false,
      reason: "Нет активного предложения для ответа.",
    };
  }

  if (details.activeProposal.sender === viewerRole) {
    return {
      canSendProposal: false,
      reason: "Ожидается ответ другой стороны на активное предложение.",
    };
  }

  if (
    viewerRole === "Client" &&
    details.exchangeStatus !== "AwaitingClientConfirmation"
  ) {
    return {
      canSendProposal: false,
      reason: "Клиент может отправить предложение только пока ожидается подтверждение клиента.",
    };
  }

  if (
    viewerRole === "Employee" &&
    details.exchangeStatus !== "AwaitingEmployeeResponse"
  ) {
    return {
      canSendProposal: false,
      reason: "Сотрудник может отправить предложение только пока ожидается ответ сотрудника.",
    };
  }

  return {
    canSendProposal: true,
    reason: null,
  };
};
