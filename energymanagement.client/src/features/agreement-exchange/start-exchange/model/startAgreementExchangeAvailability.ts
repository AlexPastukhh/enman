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
      reason: "Договорной обмен по этой заявке уже не завершён.",
    };
  }

  return {
    canStartAgreementExchange: false,
    reason: "Договорной обмен можно начать только после одобрения заявки.",
  };
};
