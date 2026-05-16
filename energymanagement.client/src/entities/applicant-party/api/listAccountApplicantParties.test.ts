import { afterEach, describe, expect, it, vi } from "vitest";
import { listAccountApplicantParties } from "./listAccountApplicantParties";

const { mockedGetAccountApplicantParties } = vi.hoisted(() => ({
  mockedGetAccountApplicantParties: vi.fn(),
}));

vi.mock("../../../shared/api/l1ApplicantPartyApi", () => ({
  getAccountApplicantParties: mockedGetAccountApplicantParties,
  __esModule: true,
}));

describe("listAccountApplicantParties", () => {
  afterEach(() => {
    mockedGetAccountApplicantParties.mockReset();
  });

  it("delegates to the shared L1 applicant party API wrapper", async () => {
    const response = {
      applicantParties: [],
    };
    mockedGetAccountApplicantParties.mockResolvedValue(response);

    await expect(listAccountApplicantParties()).resolves.toBe(response);

    expect(mockedGetAccountApplicantParties).toHaveBeenCalledTimes(1);
  });
});
