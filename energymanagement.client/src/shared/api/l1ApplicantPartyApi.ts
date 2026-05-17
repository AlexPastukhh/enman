import type { components } from "./generated/openapi-types";
import { fetchJson } from "./fetchJson";
import { l1ApiPaths, makeApplicantPartyCurrentDefaultPath } from "./l1ApiPaths";

export type L1CreateIndividualApplicantPartyRequest =
  components["schemas"]["L1CreateIndividualApplicantPartyDto"];

export type L1CreateIndividualApplicantPartyResponse =
  components["schemas"]["L1CreateIndividualApplicantPartyResponse"];
export type L1CurrentIndividualApplicantPartyResponse =
  components["schemas"]["L1CurrentIndividualApplicantPartyResponse"];
export type L1IndividualApplicantParty =
  components["schemas"]["L1IndividualApplicantPartyDto"];
export type L1AccountApplicantPartiesResponse =
  components["schemas"]["L1AccountApplicantPartiesResponse"];
export type L1ApplicantPartySummary =
  components["schemas"]["L1ApplicantPartySummaryDto"];

export const createIndividualApplicantParty = (
  request: L1CreateIndividualApplicantPartyRequest,
): Promise<L1CreateIndividualApplicantPartyResponse> =>
  fetchJson<L1CreateIndividualApplicantPartyResponse>(
    l1ApiPaths.createIndividualApplicantParty,
    {
      method: "POST",
      body: JSON.stringify(request),
    },
  );

export const getAccountApplicantParties =
  (): Promise<L1AccountApplicantPartiesResponse> =>
    fetchJson<L1AccountApplicantPartiesResponse>(
      l1ApiPaths.accountApplicantParties,
      {
        method: "GET",
      },
    );

export const makeApplicantPartyCurrentDefault = (
  applicantPartyId: number,
): Promise<void> =>
  fetchJson<void>(makeApplicantPartyCurrentDefaultPath(applicantPartyId), {
    method: "POST",
  });

export const getCurrentIndividualApplicantParty =
  (): Promise<L1CurrentIndividualApplicantPartyResponse> =>
    fetchJson<L1CurrentIndividualApplicantPartyResponse>(
      l1ApiPaths.currentIndividualApplicantParty,
      {
        method: "GET",
      },
    );
