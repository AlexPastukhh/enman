import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { startAgreementExchange } from "./startAgreementExchange";

describe("startAgreementExchange", () => {
  afterEach(() => {
    clearAntiforgeryToken();
    vi.unstubAllGlobals();
  });

  it("posts start exchange command to employee request route", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      startAgreementExchange({
        requestId: 42,
        proposal: {
          document: {
            storageKey: "agreements/42/initial.pdf",
            originalFileName: "initial-proposal.pdf",
            contentType: "application/pdf",
            sizeBytes: 4096,
          },
          comment: "Initial employee proposal.",
        },
      }),
    ).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/employee/requests/42/agreement-exchange/start",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify({
          document: {
            storageKey: "agreements/42/initial.pdf",
            originalFileName: "initial-proposal.pdf",
            contentType: "application/pdf",
            sizeBytes: 4096,
          },
          comment: "Initial employee proposal.",
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
