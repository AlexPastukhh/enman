import { useQuery } from "@tanstack/react-query";
import { listAccountApplicantParties } from "../api/listAccountApplicantParties";
import { applicantPartyQueryKeys } from "./applicantPartyQueryKeys";
import type { AccountApplicantPartiesState } from "./applicantPartyTypes";

type UseAccountApplicantPartiesQueryArgs = {
  enabled?: boolean;
};

export const useAccountApplicantPartiesQuery = ({
  enabled = true,
}: UseAccountApplicantPartiesQueryArgs = {}) =>
  useQuery<AccountApplicantPartiesState>({
    queryKey: applicantPartyQueryKeys.accountList,
    queryFn: listAccountApplicantParties,
    enabled,
    retry: false,
  });
