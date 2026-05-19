import type {
  AccountApplicantPartiesResponseDto,
  L1ApplicantPartySummary,
  CurrentIndividualApplicantPartyResponseDto,
  L1IndividualApplicantParty,
} from "../api/applicantPartyApiTypes";

export type CurrentIndividualApplicantPartyState =
  CurrentIndividualApplicantPartyResponseDto;

export type IndividualApplicantParty = L1IndividualApplicantParty;

export type AccountApplicantPartiesState = AccountApplicantPartiesResponseDto;

export type ApplicantPartySummary = L1ApplicantPartySummary;
