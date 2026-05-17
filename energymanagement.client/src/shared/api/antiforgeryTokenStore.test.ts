import { afterEach, describe, expect, it, vi } from "vitest";
import {
  clearAntiforgeryToken,
  ensureAntiforgeryToken,
  getCurrentAntiforgeryToken,
  refreshAntiforgeryToken,
} from "./antiforgeryTokenStore";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("antiforgeryTokenStore", () => {
  it("fetches and stores an antiforgery token", async () => {
    const fetchMock = vi.fn().mockResolvedValue(tokenResponse("token-1"));
    vi.stubGlobal("fetch", fetchMock);

    await expect(ensureAntiforgeryToken()).resolves.toBe("token-1");

    expect(getCurrentAntiforgeryToken()).toBe("token-1");
    expect(fetchMock).toHaveBeenCalledWith(
      "/api/antiforgery/token",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });

  it("reuses an existing token", async () => {
    const fetchMock = vi.fn().mockResolvedValue(tokenResponse("token-1"));
    vi.stubGlobal("fetch", fetchMock);

    await ensureAntiforgeryToken();
    await expect(ensureAntiforgeryToken()).resolves.toBe("token-1");

    expect(fetchMock).toHaveBeenCalledTimes(1);
  });

  it("refreshes and replaces the current token", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(tokenResponse("token-2"));
    vi.stubGlobal("fetch", fetchMock);

    await ensureAntiforgeryToken();
    await expect(refreshAntiforgeryToken()).resolves.toBe("token-2");

    expect(getCurrentAntiforgeryToken()).toBe("token-2");
    expect(fetchMock).toHaveBeenCalledTimes(2);
  });

  it("clears the current token", async () => {
    const fetchMock = vi.fn().mockResolvedValue(tokenResponse("token-1"));
    vi.stubGlobal("fetch", fetchMock);

    await ensureAntiforgeryToken();
    clearAntiforgeryToken();

    expect(getCurrentAntiforgeryToken()).toBeNull();
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
