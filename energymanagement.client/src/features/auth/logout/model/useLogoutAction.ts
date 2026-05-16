import { useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { applicantPartyQueryKeys } from "../../../../entities/applicant-party/model/applicantPartyQueryKeys";
import { sessionQueryKey } from "../../../../entities/session/model/sessionKeys";
import { ApiError } from "../../../../shared/api/fetchJson";
import { clientRoutes } from "../../../../shared/config/clientRoutes";
import { fallbackErrorMessage } from "../../../../shared/errors/clientErrorMessages";
import { logoutClientAccount } from "../api/logoutClientAccount";

export const useLogoutAction = () => {
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [isPending, setIsPending] = useState(false);

  const clearAuthenticatedState = () => {
    queryClient.setQueryData(sessionQueryKey, null);
    queryClient.removeQueries({
      queryKey: applicantPartyQueryKeys.currentIndividual,
    });
  };

  const finishAsUnauthenticated = async () => {
    clearAuthenticatedState();
    await navigate(clientRoutes.home);
  };

  const logout = async () => {
    setErrorMessage(null);
    setIsPending(true);

    try {
      await logoutClientAccount();
      await finishAsUnauthenticated();
    } catch (error) {
      if (error instanceof ApiError && error.status === 401) {
        await finishAsUnauthenticated();
        return;
      }

      setErrorMessage(fallbackErrorMessage);
    } finally {
      setIsPending(false);
    }
  };

  return {
    logout,
    isPending,
    errorMessage,
  };
};
