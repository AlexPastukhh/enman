import {
  getCurrentIndividualApplicantParty as getCurrentIndividualApplicantPartyApi,
  type L1CurrentIndividualApplicantPartyResponse,
} from "../../../shared/api/l1ApplicantPartyApi";

export const getCurrentIndividualApplicantParty =
  (): Promise<L1CurrentIndividualApplicantPartyResponse> =>
    getCurrentIndividualApplicantPartyApi();
