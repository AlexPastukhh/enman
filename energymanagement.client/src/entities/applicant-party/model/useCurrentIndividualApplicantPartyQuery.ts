import { useQuery } from "@tanstack/react-query";
import { getCurrentIndividualApplicantParty } from "../api/getCurrentIndividualApplicantParty";
import { applicantPartyQueryKeys } from "./applicantPartyQueryKeys";
import type { CurrentIndividualApplicantPartyState } from "./applicantPartyTypes";

export const useCurrentIndividualApplicantPartyQuery = (enabled: boolean) =>
  useQuery<CurrentIndividualApplicantPartyState>({
    queryKey: applicantPartyQueryKeys.currentIndividual,
    queryFn: getCurrentIndividualApplicantParty,
    enabled,
    retry: false,
  });
