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
      reason: "Договорной обмен уже завершён.",
    };
  }

  if (details.currentActorSide !== "Employee") {
    return {
      canFinalRefuse: false,
      reason: "Только сотрудник может финально отказаться от договорного обмена.",
    };
  }

  if (!finalRefusalAllowedStatuses.has(details.exchangeStatus)) {
    return {
      canFinalRefuse: false,
      reason: "Договорной обмен сейчас нельзя финально отклонить.",
    };
  }

  return {
    canFinalRefuse: true,
    reason: null,
  };
};
