import type { AgreementExchangeDetails } from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";

export type FinalRefuseAgreementExchangeAvailability = {
  canFinalRefuse: boolean;
  reason: string | null;
};

const completedStatuses = new Set(["Accepted", "FinallyRefused"]);
const finalRefusalAllowedStatuses = new Set([
  "AwaitingClientConfirmation",
  "AwaitingEmployeeResponse",
]);

export const getFinalRefuseAgreementExchangeAvailability = (
  details: AgreementExchangeDetails,
): FinalRefuseAgreementExchangeAvailability => {
  if (completedStatuses.has(details.exchangeStatus)) {
    return {
      canFinalRefuse: false,
      reason: "Agreement exchange is already completed.",
    };
  }

  if (details.currentActorSide !== "Employee") {
    return {
      canFinalRefuse: false,
      reason: "Only an Employee can finally refuse an agreement exchange.",
    };
  }

  if (!finalRefusalAllowedStatuses.has(details.exchangeStatus)) {
    return {
      canFinalRefuse: false,
      reason: "Agreement exchange is not in an active state that can be finally refused.",
    };
  }

  return {
    canFinalRefuse: true,
    reason: null,
  };
};
