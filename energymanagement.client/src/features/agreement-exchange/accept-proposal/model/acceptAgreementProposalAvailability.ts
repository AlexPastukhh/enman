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
      reason: "Agreement exchange is already completed.",
    };
  }

  if (details.currentActorSide !== "Client") {
    return {
      canAcceptProposal: false,
      reason: "Only the Client can accept an active Employee proposal.",
    };
  }

  if (details.exchangeStatus !== "AwaitingClientConfirmation") {
    return {
      canAcceptProposal: false,
      reason: "Agreement exchange is not awaiting Client confirmation.",
    };
  }

  if (!details.activeProposal) {
    return {
      canAcceptProposal: false,
      reason: "There is no active proposal to accept.",
    };
  }

  if (details.activeProposal.sender !== "Employee") {
    return {
      canAcceptProposal: false,
      reason: "Only an active Employee proposal can be accepted by the Client.",
    };
  }

  if (acceptedProposalStates.has(details.activeProposal.state)) {
    return {
      canAcceptProposal: false,
      reason: "Active proposal is not awaiting Client acceptance.",
    };
  }

  return {
    canAcceptProposal: true,
    reason: null,
  };
};
