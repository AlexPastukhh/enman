import { fetchJson } from "../../../shared/api/fetchJson";
import type { L1AccountApplicantPartiesResponse } from "./applicantPartyApiTypes";

export const listAccountApplicantParties =
  (): Promise<L1AccountApplicantPartiesResponse> =>
    fetchJson<L1AccountApplicantPartiesResponse>("/api/applicant-parties", {
      method: "GET",
    });
