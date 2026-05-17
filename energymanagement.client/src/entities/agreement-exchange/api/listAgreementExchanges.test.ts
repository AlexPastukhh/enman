import { afterEach, describe, expect, it, vi } from "vitest";
import { listAgreementExchanges } from "./listAgreementExchanges";

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("listAgreementExchanges", () => {
  it("gets agreement exchanges without filters", async () => {
    const response = { exchanges: [] };
    const fetchMock = vi.fn().mockResolvedValueOnce(jsonResponse(response));
    vi.stubGlobal("fetch", fetchMock);

    await expect(listAgreementExchanges()).resolves.toEqual(response);

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/agreement-exchanges",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });

  it("maps status filter to query string", async () => {
    const response = { exchanges: [] };
    const fetchMock = vi.fn().mockResolvedValueOnce(jsonResponse(response));
    vi.stubGlobal("fetch", fetchMock);

    await listAgreementExchanges({ status: "AwaitingClientConfirmation" });

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/agreement-exchanges?status=AwaitingClientConfirmation",
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
