import { fetchJson } from "../../../shared/api/fetchJson";
import type { L1CurrentIndividualApplicantPartyResponse } from "./applicantPartyApiTypes";

export const getCurrentIndividualApplicantParty =
  (): Promise<L1CurrentIndividualApplicantPartyResponse> =>
    fetchJson<L1CurrentIndividualApplicantPartyResponse>(
      "/api/applicant-parties/current-individual",
      {
        method: "GET",
      },
    );
