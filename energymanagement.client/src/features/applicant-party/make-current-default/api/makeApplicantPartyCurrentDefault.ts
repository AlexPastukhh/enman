import { makeApplicantPartyCurrentDefault as postMakeApplicantPartyCurrentDefault } from "../../../../shared/api/l1ApplicantPartyApi";

export const makeApplicantPartyCurrentDefault = (
  applicantPartyId: number,
): Promise<void> => postMakeApplicantPartyCurrentDefault(applicantPartyId);
