import { describe, expect, it } from "vitest";
import type { EmployeeRequestDetails } from "./employeeRequestTypes";
import { getEmployeeReviewActionAvailability } from "./reviewActionAvailability";

const baseDetails: EmployeeRequestDetails = {
  requestId: 42,
  requestType: "Connection",
  status: "InReview",
  applicant: {
    applicantPartyId: 7,
    applicantPartyType: "Individual",
    displayName: "Ivan Petrov",
    email: "ivan@example.com",
    phoneNumber: "+79001234567",
  },
  objectAddress: "Altai Krai, Zarinsk, Lenina 10",
  details: "Request details",
  createdAt: "2026-01-02T10:30:00Z",
  reviewState: "NotStarted",
};

describe("getEmployeeReviewActionAvailability", () => {
  it("allows start review for InReview request with not started review", () => {
    expect(getEmployeeReviewActionAvailability(baseDetails)).toEqual({
      canStartReview: true,
      canApproveReview: false,
      canRejectReview: false,
      reason: null,
    });
  });

  it("allows approve and reject when started by current Employee", () => {
    expect(
      getEmployeeReviewActionAvailability({
        ...baseDetails,
        reviewState: "StartedByCurrentEmployee",
      }),
    ).toMatchObject({
      canStartReview: false,
      canApproveReview: true,
      canRejectReview: true,
    });
  });

  it("blocks actions when another Employee started review", () => {
    expect(
      getEmployeeReviewActionAvailability({
        ...baseDetails,
        reviewState: "StartedByAnotherEmployee",
      }),
    ).toEqual({
      canStartReview: false,
      canApproveReview: false,
      canRejectReview: false,
      reason: "Another Employee has already started this review.",
    });
  });

  it("blocks actions for completed review", () => {
    expect(
      getEmployeeReviewActionAvailability({
        ...baseDetails,
        status: "Approved",
        reviewState: "Approved",
      }),
    ).toMatchObject({
      canStartReview: false,
      canApproveReview: false,
      canRejectReview: false,
    });
  });
});
