import { describe, expect, it } from "vitest";
import type { EmployeeRequestDetails } from "../../../../entities/employee-request/model/employeeRequestTypes";
import { getStartAgreementExchangeAvailability } from "./startAgreementExchangeAvailability";

const details = (status: EmployeeRequestDetails["status"]): EmployeeRequestDetails =>
  ({
    requestId: 42,
    requestType: "Connection",
    status,
    applicant: {
      applicantPartyId: 7,
      applicantPartyType: "Individual",
      displayName: "Ivan Petrov",
      email: "ivan@example.com",
      phoneNumber: "+79001234567",
    },
    objectAddress: "Altai Krai, Zarinsk, Lenina 10",
    details: "Please connect the object to the grid.",
    createdAt: "2026-01-02T10:30:00Z",
    reviewState: "Approved",
  }) as EmployeeRequestDetails;

describe("getStartAgreementExchangeAvailability", () => {
  it("allows start exchange for approved request", () => {
    expect(getStartAgreementExchangeAvailability(details("Approved"))).toEqual({
      canStartAgreementExchange: true,
      reason: null,
    });
  });

  it("blocks start exchange before request is approved", () => {
    expect(getStartAgreementExchangeAvailability(details("InReview"))).toEqual({
      canStartAgreementExchange: false,
      reason: "Agreement exchange can be started only after the request is approved.",
    });
  });

  it("shows specific reason for failed agreement exchange request", () => {
    expect(
      getStartAgreementExchangeAvailability(details("AgreementExchangeFailed")),
    ).toEqual({
      canStartAgreementExchange: false,
      reason: "Agreement exchange already failed for this request.",
    });
  });
});
