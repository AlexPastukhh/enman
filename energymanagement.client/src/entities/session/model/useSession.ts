import { useContext } from "react";
import { sessionContext } from "./SessionProvider";

export const useSession = () => useContext(sessionContext);

