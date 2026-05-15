import { useQuery } from "@tanstack/react-query";
import { getCurrentSession } from "../api/getCurrentSession";
import { sessionQueryKey } from "./sessionKeys";
import type { SessionState } from "./sessionTypes";

export const useSessionQuery = () =>
  useQuery<SessionState | null>({
    queryKey: sessionQueryKey,
    queryFn: getCurrentSession,
    staleTime: 5 * 60 * 1000,
    retry: false,
  });

