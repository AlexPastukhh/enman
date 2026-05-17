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
      reason: "Agreement exchange is already completed.",
    };
  }

  if (details.currentActorSide !== viewerRole) {
    return {
      canSendProposal: false,
      reason: "This agreement exchange is not available for the current role.",
    };
  }

  if (!details.activeProposal) {
    return {
      canSendProposal: false,
      reason: "There is no active proposal to answer.",
    };
  }

  if (details.activeProposal.sender === viewerRole) {
    return {
      canSendProposal: false,
      reason: "Waiting for the other party to respond to the active proposal.",
    };
  }

  if (
    viewerRole === "Client" &&
    details.exchangeStatus !== "AwaitingClientConfirmation"
  ) {
    return {
      canSendProposal: false,
      reason: "Client proposal can be sent only while client confirmation is pending.",
    };
  }

  if (
    viewerRole === "Employee" &&
    details.exchangeStatus !== "AwaitingEmployeeResponse"
  ) {
    return {
      canSendProposal: false,
      reason: "Employee proposal can be sent only while employee response is pending.",
    };
  }

  return {
    canSendProposal: true,
    reason: null,
  };
};
