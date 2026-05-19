import type { AgreementExchangeDetails } from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";

export type AcceptAgreementProposalAvailability = {
  canAcceptProposal: boolean;
  reason: string | null;
};

const completedStatuses = new Set(["Accepted", "FinallyRefused"]);

const acceptedProposalStates = new Set(["Accepted", "Superseded", "FinallyRefused"]);

export const getAcceptAgreementProposalAvailability = (
  details: AgreementExchangeDetails,
): AcceptAgreementProposalAvailability => {
  if (completedStatuses.has(details.exchangeStatus)) {
    return {
      canAcceptProposal: false,
      reason: "Договорной обмен уже завершён.",
    };
  }

  if (details.currentActorSide !== "Client") {
    return {
      canAcceptProposal: false,
      reason: "Только клиент может принять активное предложение сотрудника.",
    };
  }

  if (details.exchangeStatus !== "AwaitingClientConfirmation") {
    return {
      canAcceptProposal: false,
      reason: "Договорной обмен не ожидает подтверждения клиента.",
    };
  }

  if (!details.activeProposal) {
    return {
      canAcceptProposal: false,
      reason: "Нет активного предложения для принятия.",
    };
  }

  if (details.activeProposal.sender !== "Employee") {
    return {
      canAcceptProposal: false,
      reason: "Клиент может принять только активное предложение сотрудника.",
    };
  }

  if (acceptedProposalStates.has(details.activeProposal.state)) {
    return {
      canAcceptProposal: false,
      reason: "Активное предложение не ожидает принятия клиентом.",
    };
  }

  return {
    canAcceptProposal: true,
    reason: null,
  };
};
