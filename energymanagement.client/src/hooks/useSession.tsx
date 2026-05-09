import { useQuery } from "@tanstack/react-query";
import { getUser } from "../QueryFns/getUser";
import { useContext, type ReactNode } from "react";
import { Keys } from "../globConstants";
import { createContext } from "react";

export type SessionState = {
    userId: number;
    email: string;
    roles: string[];
};

const sessionContext = createContext<SessionState | null>(null);

export const SessionProvider = ({ children }: { children: ReactNode }) => {
    const { data } = useQuery<SessionState | null>({
        queryKey:[Keys.SessionQueryKey],
        queryFn: getUser,
        staleTime: 5*60*1000,
        retry:true,
        retryDelay:2000
        })
    return (
        <sessionContext.Provider value={data ?? null}>
            {children}
        </sessionContext.Provider>
    )
    }

export const useSession = () => {
    return useContext(sessionContext);
}
