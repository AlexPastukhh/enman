import { afterEach, describe, expect, it, vi } from "vitest";
import { getAccountApplicantParties } from "./l1ApplicantPartyApi";

afterEach(() => {
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
});
