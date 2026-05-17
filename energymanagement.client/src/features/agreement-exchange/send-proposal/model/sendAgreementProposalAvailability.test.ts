import { describe, expect, it } from "vitest";
import type { AgreementExchangeDetails } from "../../../../entities/agreement-exchange/model/agreementExchangeTypes";
import { getSendAgreementProposalAvailability } from "./sendAgreementProposalAvailability";

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
    document: {
      storageKey: "agreements/20/v1.pdf",
      originalFileName: "proposal-v1.pdf",
      contentType: "application/pdf",
      sizeBytes: 1024,
    },
    comment: null,
    createdAt: "2026-01-01T10:00:00Z",
  },
  proposals: [],
  currentActorSide: "Client",
  createdAt: "2026-01-01T10:00:00Z",
  lastActivityAt: "2026-01-02T10:00:00Z",
};

describe("getSendAgreementProposalAvailability", () => {
  it("allows Client to answer active Employee proposal", () => {
    expect(getSendAgreementProposalAvailability(details, "Client")).toEqual({
      canSendProposal: true,
      reason: null,
    });
  });

  it("blocks when current actor already sent active proposal", () => {
    expect(
      getSendAgreementProposalAvailability(
        {
          ...details,
          activeProposal: {
            ...details.activeProposal!,
            sender: "Client",
          },
        },
        "Client",
      ).canSendProposal,
    ).toBe(false);
  });

  it("allows Employee to answer active Client proposal", () => {
    expect(
      getSendAgreementProposalAvailability(
        {
          ...details,
          exchangeStatus: "AwaitingEmployeeResponse",
          currentActorSide: "Employee",
          activeProposal: {
            ...details.activeProposal!,
            sender: "Client",
          },
        },
        "Employee",
      ),
    ).toEqual({
      canSendProposal: true,
      reason: null,
    });
  });

  it("blocks completed exchange", () => {
    expect(
      getSendAgreementProposalAvailability(
        {
          ...details,
          exchangeStatus: "Accepted",
        },
        "Client",
      ).canSendProposal,
    ).toBe(false);
  });
});
