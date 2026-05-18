import { afterEach, describe, expect, it, vi } from "vitest";
import { clearAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { runApplicantPartyVerification } from "./runApplicantPartyVerification";

describe("runApplicantPartyVerification", () => {
  afterEach(() => {
    clearAntiforgeryToken();
    vi.unstubAllGlobals();
  });

  it("posts verification command by requestId without request body", async () => {
    const responseBody = {
      requestId: 42,
      applicantPartyId: 7,
      verificationStatus: "Verified",
      mockResult: "Passed",
      message: "Mock verification passed.",
    };
    const fetchMock = vi
      .fn()
      .mockResolvedValueOnce(tokenResponse("token-1"))
      .mockResolvedValueOnce(jsonResponse(responseBody));
    vi.stubGlobal("fetch", fetchMock);

    await expect(runApplicantPartyVerification(42)).resolves.toEqual(
      responseBody,
    );

    expect(fetchMock).toHaveBeenLastCalledWith(
      "/api/employee/requests/42/applicant-party/verification/run",
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

const jsonResponse = (body: unknown) =>
  new Response(JSON.stringify(body), {
    status: 200,
    headers: { "content-type": "application/json" },
  });
