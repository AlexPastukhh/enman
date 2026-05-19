import type {
  AccountApplicantPartiesResponseDto,
  ApplicantPartySummaryDto,
  CurrentIndividualApplicantPartyResponseDto,
  IndividualApplicantPartyDto,
} from "../api/applicantPartyApiTypes";

export type CurrentIndividualApplicantPartyState =
  CurrentIndividualApplicantPartyResponseDto;

export type IndividualApplicantParty = IndividualApplicantPartyDto;

export type AccountApplicantPartiesState = AccountApplicantPartiesResponseDto;

export type ApplicantPartySummary = ApplicantPartySummaryDto;
