import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { sendAgreementProposal } from "./sendAgreementProposal";

describe("sendAgreementProposal", () => {
  afterEach(() => {
    clearAntiforgeryToken();
    vi.unstubAllGlobals();
  });

  it("posts proposal version command with document reference payload", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      sendAgreementProposal({
        exchangeId: 77,
        proposal: {
          document: {
            storageKey: "agreements/77/v2.pdf",
            originalFileName: "proposal-v2.pdf",
            contentType: "application/pdf",
            sizeBytes: 2048,
          },
          comment: "Updated connection terms.",
        },
      }),
    ).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/agreement-exchanges/77/proposals",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify({
          document: {
            storageKey: "agreements/77/v2.pdf",
            originalFileName: "proposal-v2.pdf",
            contentType: "application/pdf",
            sizeBytes: 2048,
          },
          comment: "Updated connection terms.",
        }),
      }),
    );
    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
