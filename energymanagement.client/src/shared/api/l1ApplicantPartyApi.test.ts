import { afterEach, describe, expect, it, vi } from "vitest";
import {
  getAccountApplicantParties,
  makeApplicantPartyCurrentDefault,
} from "./l1ApplicantPartyApi";

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

  it("posts make current/default command without request body", async () => {
    const fetchMock = vi.fn().mockResolvedValue(new Response(null, { status: 200 }));
    vi.stubGlobal("fetch", fetchMock);

    await expect(makeApplicantPartyCurrentDefault(42)).resolves.toBeUndefined();

    expect(fetchMock).toHaveBeenCalledWith(
      "/api/l1/applicant-parties/42/make-current-default",
      expect.objectContaining({
        method: "POST",
        credentials: "include",
      }),
    );
    expect(fetchMock.mock.calls[0]?.[1]).not.toHaveProperty("body");
  });
});
