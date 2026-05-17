import { afterEach, describe, expect, it, vi } from "vitest";
import { getAgreementExchangeDetails } from "./getAgreementExchangeDetails";

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("getAgreementExchangeDetails", () => {
  it("gets agreement exchange details by shared endpoint", async () => {
    const response = {
      exchangeId: 20,
      requestId: 10,
      exchangeStatus: "AwaitingClientConfirmation",
      activeProposalVersion: 1,
      request: {
        requestId: 10,
        requestStatus: "Approved",
        requestDisplayName: "Connection request #10",
        objectAddress: "Lenina 10",
      },
      activeProposal: null,
      proposals: [],
      currentActorSide: "Client",
      createdAt: "2026-01-01T10:00:00Z",
      lastActivityAt: "2026-01-02T10:00:00Z",
    };
    const fetchMock = vi.fn().mockResolvedValueOnce(jsonResponse(response));
    vi.stubGlobal("fetch", fetchMock);

    await expect(getAgreementExchangeDetails(20)).resolves.toEqual(response);

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/agreement-exchanges/20",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });
});

const jsonResponse = (body: unknown) =>
  new Response(JSON.stringify(body), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
