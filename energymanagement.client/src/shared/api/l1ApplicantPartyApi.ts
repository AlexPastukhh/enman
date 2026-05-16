import type { components } from "./generated/openapi-types";
import { fetchJson } from "./fetchJson";
import { l1ApiPaths } from "./l1ApiPaths";

export type L1CreateIndividualApplicantPartyRequest =
  components["schemas"]["L1CreateIndividualApplicantPartyDto"];

export type L1CreateIndividualApplicantPartyResponse =
  components["schemas"]["L1CreateIndividualApplicantPartyResponse"];
export type L1CurrentIndividualApplicantPartyResponse =
  components["schemas"]["L1CurrentIndividualApplicantPartyResponse"];
export type L1IndividualApplicantParty =
  components["schemas"]["L1IndividualApplicantPartyDto"];

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

export const getCurrentIndividualApplicantParty =
  (): Promise<L1CurrentIndividualApplicantPartyResponse> =>
    fetchJson<L1CurrentIndividualApplicantPartyResponse>(
      l1ApiPaths.currentIndividualApplicantParty,
      {
        method: "GET",
      },
    );
