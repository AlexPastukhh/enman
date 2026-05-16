import type {
  L1AccountApplicantPartiesResponse,
  L1ApplicantPartySummary,
  L1CurrentIndividualApplicantPartyResponse,
  L1IndividualApplicantParty,
} from "../../../shared/api/l1ApplicantPartyApi";

export type CurrentIndividualApplicantPartyState =
  L1CurrentIndividualApplicantPartyResponse;

export type IndividualApplicantParty = L1IndividualApplicantParty;

export type AccountApplicantPartiesState = L1AccountApplicantPartiesResponse;

export type ApplicantPartySummary = L1ApplicantPartySummary;
