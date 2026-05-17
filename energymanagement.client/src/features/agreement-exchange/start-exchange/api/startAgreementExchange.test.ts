import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { startAgreementExchange } from "./startAgreementExchange";

describe("startAgreementExchange", () => {
  afterEach(() => {
    clearAntiforgeryToken();
    vi.unstubAllGlobals();
  });

  it("posts start exchange command with request id and initial proposal payload", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(
        new Response(JSON.stringify({ exchangeId: 88 }), {
          status: 201,
          headers: { "content-type": "application/json" },
        }),
      );
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      startAgreementExchange({
        requestId: 42,
        initialProposal: {
          document: {
            storageKey: "agreements/42/initial.pdf",
            originalFileName: "initial-proposal.pdf",
            contentType: "application/pdf",
            sizeBytes: 4096,
          },
          comment: "Initial employee proposal.",
        },
      }),
    ).resolves.toEqual({ exchangeId: 88 });

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/agreement-exchanges",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify({
          requestId: 42,
          initialProposal: {
            document: {
              storageKey: "agreements/42/initial.pdf",
              originalFileName: "initial-proposal.pdf",
              contentType: "application/pdf",
              sizeBytes: 4096,
            },
            comment: "Initial employee proposal.",
          },
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
