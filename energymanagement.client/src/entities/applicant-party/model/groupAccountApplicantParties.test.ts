import { describe, expect, it } from "vitest";
import { groupAccountApplicantParties } from "./groupAccountApplicantParties";
import type { ApplicantPartySummary } from "./applicantPartyTypes";

const applicantParty = (
  applicantPartyId: number,
  isCurrentDefault: boolean,
): ApplicantPartySummary => ({
  applicantPartyId,
  applicantPartyType: "Individual",
  displayName: `Applicant ${applicantPartyId}`,
  email: `applicant${applicantPartyId}@example.com`,
  phoneNumber: "+79001234567",
  verificationStatus: "Unverified",
  isCurrentDefault,
  createdAt: "2026-01-02T10:30:00Z",
});

describe("groupAccountApplicantParties", () => {
  it("separates current defaults from other saved applicant parties", () => {
    const first = applicantParty(1, true);
    const second = applicantParty(2, false);

    expect(groupAccountApplicantParties([first, second])).toEqual({
      currentDefaults: [first],
      otherSaved: [second],
    });
  });

  it("returns empty groups for empty input", () => {
    expect(groupAccountApplicantParties()).toEqual({
      currentDefaults: [],
      otherSaved: [],
    });
  });
});
