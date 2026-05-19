import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { makeApplicantPartyCurrentDefault } from "./makeApplicantPartyCurrentDefault";

describe("makeApplicantPartyCurrentDefault", () => {
  afterEach(() => {
    clearAntiforgeryToken();
    vi.unstubAllGlobals();
  });

  it("posts make current/default command without request body", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 200 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(makeApplicantPartyCurrentDefault(7)).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/applicant-parties/7/make-current-default",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
      }),
    );
    expect(fetchMock.mock.calls[1]?.[1]).not.toHaveProperty("body");
    const headers = fetchMock.mock.calls[1]?.[1]?.headers as Headers;
    expect(headers.get("X-CSRF-TOKEN")).toBe("token-1");
  });
});

const tokenResponse = (requestToken: string) =>
  new Response(JSON.stringify({ requestToken }), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
