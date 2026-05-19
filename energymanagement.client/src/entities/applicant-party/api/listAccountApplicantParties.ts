import { fetchJson } from "../../../shared/api/fetchJson";
import type { AccountApplicantPartiesResponseDto } from "./applicantPartyApiTypes";

export const listAccountApplicantParties =
  (): Promise<AccountApplicantPartiesResponseDto> =>
    fetchJson<AccountApplicantPartiesResponseDto>("/api/applicant-parties", {
      method: "GET",
    });
