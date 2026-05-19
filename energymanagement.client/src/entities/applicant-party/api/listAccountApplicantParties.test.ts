import { afterEach, describe, expect, it, vi } from "vitest";
import { listAccountApplicantParties } from "./listAccountApplicantParties";

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("listAccountApplicantParties", () => {
  it("gets account applicant parties from the account list endpoint", async () => {
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

    await expect(listAccountApplicantParties()).resolves.toEqual(response);

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/applicant-parties",
      expect.objectContaining({
        method: "GET",
        credentials: "include",
      }),
    );
  });
});
