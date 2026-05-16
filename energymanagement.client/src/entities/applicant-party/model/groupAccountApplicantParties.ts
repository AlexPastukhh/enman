import type { ApplicantPartySummary } from "./applicantPartyTypes";

export type GroupedAccountApplicantParties = {
  currentDefaults: ApplicantPartySummary[];
  otherSaved: ApplicantPartySummary[];
};

export const groupAccountApplicantParties = (
  applicantParties: readonly ApplicantPartySummary[] = [],
): GroupedAccountApplicantParties => ({
  currentDefaults: applicantParties.filter(
    (applicantParty) => applicantParty.isCurrentDefault,
  ),
  otherSaved: applicantParties.filter(
    (applicantParty) => !applicantParty.isCurrentDefault,
  ),
});
