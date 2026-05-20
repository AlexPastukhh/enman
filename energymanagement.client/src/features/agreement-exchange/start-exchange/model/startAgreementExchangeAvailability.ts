import type { EmployeeRequestDetails } from "../../../../entities/employee-request/model/employeeRequestTypes";

export type StartAgreementExchangeAvailability = {
  canStartAgreementExchange: boolean;
  reason: string | null;
};

export const getStartAgreementExchangeAvailability = (
  details: EmployeeRequestDetails,
): StartAgreementExchangeAvailability => {
  if (details.status === "Approved") {
    return {
      canStartAgreementExchange: true,
      reason: null,
    };
  }

  if (details.status === "AgreementExchangeFailed") {
    return {
      canStartAgreementExchange: false,
      reason: "Согласование договора по этой заявке уже не завершено.",
    };
  }

  return {
    canStartAgreementExchange: false,
    reason: "Согласование договора можно начать только после одобрения заявки.",
  };
};
