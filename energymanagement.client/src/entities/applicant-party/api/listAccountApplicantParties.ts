import {
  getAccountApplicantParties,
  type L1AccountApplicantPartiesResponse,
} from "../../../shared/api/l1ApplicantPartyApi";

export const listAccountApplicantParties =
  (): Promise<L1AccountApplicantPartiesResponse> =>
    getAccountApplicantParties();
