import { ServerRoutes } from "../globConstants";
import type { SessionState } from "../Context/sessionContext";
import { fetchWrapper } from "../Utils/fetchWrapper";

export const getUser = async (): Promise<SessionState | null> => {
  const response = await fetchWrapper.get(
    ServerRoutes.GetUser.Path, {
    credentials: "include",
  });
  if (response.ok) {
    return await response.json();
  } else {
    console.log(
      `${response.url} Failed to fetch user data: ${response.status} ${response.statusText}`
    );
    return null;
  }
};
