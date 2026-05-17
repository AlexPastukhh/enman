/**
 * @vitest environment jsdom
 */
import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { signInEmployeeWithWindows } from "./signInEmployeeWithWindows";

afterEach(() => {
  vi.unstubAllGlobals();
  clearAntiforgeryToken();
});

describe("signInEmployeeWithWindows", () => {
  it("calls employee windows sign-in endpoint and refreshes antiforgery token", async () => {
    const fetchMock = vi.fn()
      .mockResolvedValueOnce(new Response(JSON.stringify({
        accountId: 700,
        email: "employee@example.com",
        role: "Employee",
        isActive: true,
        isAuthenticated: true,
      }), {
        status: 200,
        headers: { "content-type": "application/json" },
      }))
      .mockResolvedValueOnce(new Response(JSON.stringify({
        requestToken: "next-token",
      }), {
        status: 200,
        headers: { "content-type": "application/json" },
      }));

    vi.stubGlobal("fetch", fetchMock);

    await expect(signInEmployeeWithWindows()).resolves.toMatchObject({
      accountId: 700,
      role: "Employee",
      isAuthenticated: true,
    });

    expect(fetchMock).toHaveBeenNthCalledWith(1, "/api/employee/auth/windows-signin", expect.objectContaining({
      credentials: "include",
      method: "GET",
      headers: expect.any(Headers),
    }));
    expect(fetchMock).toHaveBeenNthCalledWith(2, "/api/antiforgery/token", {
      method: "GET",
      credentials: "include",
      headers: {
        Accept: "application/json",
      },
    });
  });
});
