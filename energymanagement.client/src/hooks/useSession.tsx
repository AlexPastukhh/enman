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

const sessionContext= createContext<SessionState | undefined>(undefined);

export const SessionProvider = ({ children }: { children: ReactNode }) => {
    const { data } = useQuery<SessionState | undefined>({
        queryKey:[Keys.SessionQueryKey],
        queryFn: getUser,
        staleTime: 5*60*1000,
        retry:true,
        retryDelay:2000
        })
    return (
        <sessionContext.Provider value={data}>
            {children}
        </sessionContext.Provider>
    )
    }

export const useSession = () => {
    const session = useContext(sessionContext);
    if(!session){
        throw new Error("useSession must be used within a SessionProvider");
    }
    return {...session};
}