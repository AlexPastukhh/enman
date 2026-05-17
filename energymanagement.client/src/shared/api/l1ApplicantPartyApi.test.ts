import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "./antiforgeryTokenStore";
import {
  getAccountApplicantParties,
  makeApplicantPartyCurrentDefault,
} from "./l1ApplicantPartyApi";

afterEach(() => {
  clearAntiforgeryToken();
  vi.unstubAllGlobals();
});

describe("l1ApplicantPartyApi", () => {
  it("gets account applicant parties from the L1 account list endpoint", async () => {
    const response = {
      applicantParties: [],
    };
    const fetchMock = vi.fn().mockResolvedValue(
      new Response(JSON.stringify(response), {
        status: 200,
        headers: { "content-type": "application/json" },
      }),
    );
    vi.stubGlobal("fetch", fetchMock);

    await expect(getAccountApplicantParties()).resolves.toEqual(response);

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/l1/applicant-parties",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });

  it("posts make current/default command without request body", async () => {
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(new Response(null, { status: 200 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(makeApplicantPartyCurrentDefault(42)).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/l1/applicant-parties/42/make-current-default",
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
