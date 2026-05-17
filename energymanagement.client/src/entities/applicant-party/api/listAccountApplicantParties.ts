import { fetchJson } from "../../../shared/api/fetchJson";
import type { L1AccountApplicantPartiesResponse } from "./applicantPartyApiTypes";

export const listAccountApplicantParties =
  (): Promise<L1AccountApplicantPartiesResponse> =>
    fetchJson<L1AccountApplicantPartiesResponse>("/api/l1/applicant-parties", {
      method: "GET",
    });
