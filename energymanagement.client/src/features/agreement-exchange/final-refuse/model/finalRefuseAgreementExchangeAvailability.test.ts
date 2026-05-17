import { describe, expect, it } from "vitest";
import type { AgreementExchangeDetails } from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";
import { getFinalRefuseAgreementExchangeAvailability } from "./finalRefuseAgreementExchangeAvailability";

const details: AgreementExchangeDetails = {
  exchangeId: 20,
  requestId: 10,
  exchangeStatus: "AwaitingEmployeeResponse",
  activeProposalVersion: 2,
  request: {
    requestId: 10,
    requestStatus: "Approved",
    requestDisplayName: "Connection request #10",
    objectAddress: "Altai Krai, Barnaul, Lenina 10",
  },
  activeProposal: {
    proposalId: 30,
    version: 2,
    sender: "Client",
    senderId: 7,
    state: "Active",
    document: null,
    comment: null,
    createdAt: "2026-01-01T10:00:00Z",
  },
  proposals: [],
  currentActorSide: "Employee",
  createdAt: "2026-01-01T10:00:00Z",
  lastActivityAt: "2026-01-02T10:00:00Z",
};

describe("getFinalRefuseAgreementExchangeAvailability", () => {
  it("allows Employee to finally refuse an active exchange", () => {
    expect(getFinalRefuseAgreementExchangeAvailability(details)).toEqual({
      canFinalRefuse: true,
      reason: null,
    });
  });

  it("allows Employee to finally refuse while Client confirmation is pending", () => {
    expect(
      getFinalRefuseAgreementExchangeAvailability({
        ...details,
        exchangeStatus: "AwaitingClientConfirmation",
      }).canFinalRefuse,
    ).toBe(true);
  });

  it("blocks Client-side details", () => {
    expect(
      getFinalRefuseAgreementExchangeAvailability({
        ...details,
        currentActorSide: "Client",
      }).canFinalRefuse,
    ).toBe(false);
  });

  it("blocks completed exchange", () => {
    expect(
      getFinalRefuseAgreementExchangeAvailability({
        ...details,
        exchangeStatus: "FinallyRefused",
      }).canFinalRefuse,
    ).toBe(false);
  });
});
