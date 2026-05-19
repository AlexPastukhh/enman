import { afterEach, describe, expect, it, vi } from "vitest";
import {
  clearAntiforgeryToken,
  ensureAntiforgeryToken,
  getCurrentAntiforgeryToken,
} from "./antiforgeryTokenStore";
import { loginClientAccount, logoutClientAccount } from "./authApi";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("authApi antiforgery session lifecycle", () => {
  it("refreshes antiforgery token after successful login", async () => {
    const loginResponse = {
      isAuthenticated: true,
      accountId: 1,
      email: "client@example.com",
      role: "Client",
      isActive: true,
    };
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("anonymous-token"))
      .mockResolvedValueOnce(jsonResponse(loginResponse))
      .mockResolvedValueOnce(tokenResponse("authenticated-token"));
    vi.stubGlobal("fetch", fetchMock);

    await expect(
      loginClientAccount({
        email: "client@example.com",
        password: "ValidPassword!123",
      }),
    ).resolves.toEqual(loginResponse);

    expect(getCurrentAntiforgeryToken()).toBe("authenticated-token");
    expect(fetchMock.mock.calls.map((call) => call[0])).toEqual([
      "/api/antiforgery/token",
      "/api/auth/login",
      "/api/antiforgery/token",
    ]);
  });

  it("clears antiforgery token after successful logout", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("authenticated-token"))
      .mockResolvedValueOnce(new Response(null, { status: 204 }));
    vi.stubGlobal("fetch", fetchMock);
    await ensureAntiforgeryToken();

    await expect(logoutClientAccount()).resolves.toBeUndefined();

    expect(getCurrentAntiforgeryToken()).toBeNull();
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });

const jsonResponse = (body: object) =>
  new Response(JSON.stringify(body), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
