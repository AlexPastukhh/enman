import type { paths } from "./generated/openapi-types";

type OpenApiPath = keyof paths;

export const l1ApiPaths = {
  register: "/api/l1/auth/register",
  login: "/api/l1/auth/login",
  currentUser: "/api/l1/auth/current-user",
  logout: "/api/l1/auth/logout",
  createIndividualApplicantParty: "/api/l1/applicant-parties/individual",
  accountApplicantParties: "/api/l1/applicant-parties",
  currentIndividualApplicantParty:
    "/api/l1/applicant-parties/current-individual",
  requests: "/api/l1/requests",
} as const satisfies Record<string, OpenApiPath>;
