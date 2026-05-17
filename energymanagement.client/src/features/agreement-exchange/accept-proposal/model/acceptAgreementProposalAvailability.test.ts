import { describe, expect, it } from "vitest";
import type { AgreementExchangeDetails } from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";
import { getAcceptAgreementProposalAvailability } from "./acceptAgreementProposalAvailability";

const details: AgreementExchangeDetails = {
  exchangeId: 20,
  requestId: 10,
  exchangeStatus: "AwaitingClientConfirmation",
  activeProposalVersion: 1,
  request: {
    requestId: 10,
    requestStatus: "Approved",
    requestDisplayName: "Connection request #10",
    objectAddress: "Altai Krai, Barnaul, Lenina 10",
  },
  activeProposal: {
    proposalId: 30,
    version: 1,
    sender: "Employee",
    senderId: 9,
    state: "AwaitingClientConfirmation",
    document: null,
    comment: null,
    createdAt: "2026-01-01T10:00:00Z",
  },
  proposals: [],
  currentActorSide: "Client",
  createdAt: "2026-01-01T10:00:00Z",
  lastActivityAt: "2026-01-02T10:00:00Z",
};

describe("getAcceptAgreementProposalAvailability", () => {
  it("allows Client to accept active Employee proposal", () => {
    expect(getAcceptAgreementProposalAvailability(details)).toEqual({
      canAcceptProposal: true,
      reason: null,
    });
  });

  it("blocks Employee-side details", () => {
    expect(
      getAcceptAgreementProposalAvailability({
        ...details,
        currentActorSide: "Employee",
      }).canAcceptProposal,
    ).toBe(false);
  });

  it("blocks Client-authored active proposal", () => {
    expect(
      getAcceptAgreementProposalAvailability({
        ...details,
        activeProposal: {
          ...details.activeProposal!,
          sender: "Client",
        },
      }).canAcceptProposal,
    ).toBe(false);
  });

  it("blocks completed exchange", () => {
    expect(
      getAcceptAgreementProposalAvailability({
        ...details,
        exchangeStatus: "Accepted",
      }).canAcceptProposal,
    ).toBe(false);
  });
});
