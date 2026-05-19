import { ApiError } from "../../../shared/api/fetchJson";
import { getCurrentUser } from "../../../shared/apiAuthApi";
import { mapCurrentUserToSession, type SessionState } from "../model/sessionTypes";

export const getCurrentSession = async (): Promise<SessionState | null> => {
  try {
    const currentUser = await getCurrentUser();
    return mapCurrentUserToSession(currentUser);
  } catch (error) {
    if (error instanceof ApiError && error.status === 401) {
      return null;
    }

    throw error;
  }
};
