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
      reason: "Agreement exchange already failed for this request.",
    };
  }

  return {
    canStartAgreementExchange: false,
    reason: "Agreement exchange can be started only after the request is approved.",
  };
};
