import { createContext, type ReactNode } from "react";
import { useSessionQuery } from "./useSessionQuery";
import type { SessionState } from "./sessionTypes";

export const sessionContext = createContext<SessionState | null>(null);

export const SessionProvider = ({ children }: { children: ReactNode }) => {
  const { data } = useSessionQuery();

  return (
    <sessionContext.Provider value={data ?? null}>
      {children}
    </sessionContext.Provider>
  );
};

