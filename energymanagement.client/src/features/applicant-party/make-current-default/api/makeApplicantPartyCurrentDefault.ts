import { fetchJson } from "../../../../shared/api/fetchJson";

const makeApplicantPartyCurrentDefaultPath = (
  applicantPartyId: number | string,
) =>
  `/api/l1/applicant-parties/${encodeURIComponent(String(applicantPartyId))}/make-current-default`;

export const makeApplicantPartyCurrentDefault = (
  applicantPartyId: number,
): Promise<void> =>
  fetchJson<void>(makeApplicantPartyCurrentDefaultPath(applicantPartyId), {
    method: "POST",
  });
