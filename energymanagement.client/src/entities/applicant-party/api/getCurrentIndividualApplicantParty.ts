import { fetchJson } from "../../../shared/api/fetchJson";
import type { CurrentIndividualApplicantPartyResponseDto } from "./applicantPartyApiTypes";

export const getCurrentIndividualApplicantParty =
  (): Promise<CurrentIndividualApplicantPartyResponseDto> =>
    fetchJson<CurrentIndividualApplicantPartyResponseDto>(
      "/api/applicant-parties/current-individual",
      {
        method: "GET",
      },
    );
