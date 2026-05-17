import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { finalRefuseAgreementExchange } from "./finalRefuseAgreementExchange";

describe("finalRefuseAgreementExchange", () => {
  afterEach(() => {
    clearAntiforgeryToken();
    vi.unstubAllGlobals();
  });

  it("posts Employee final-refuse command without a request body when reason is omitted", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      finalRefuseAgreementExchange({ exchangeId: 77 }),
    ).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/agreement-exchanges/77/final-refuse",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
      }),
    );
    const [, init] = fetchMock.mock.calls[1] ?? [];
    expect(init).not.toHaveProperty("body");
    const headers = init?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
  });

  it("posts Employee final-refuse command with optional reason payload", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-2"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      finalRefuseAgreementExchange({
        exchangeId: 77,
        payload: { reason: "Cannot agree on terms." },
      }),
    ).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/agreement-exchanges/77/final-refuse",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
        body: JSON.stringify({ reason: "Cannot agree on terms." }),
      }),
    );
    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-2");
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
