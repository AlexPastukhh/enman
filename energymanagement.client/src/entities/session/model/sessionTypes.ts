import type { L1CurrentUserResponse } from "../../../shared/api/l1AuthApi";

export type SessionState = {
  accountId: number;
  email: string;
  role: string;
  isActive: boolean;
  isAuthenticated: boolean;
};

export const mapCurrentUserToSession = (
  response: L1CurrentUserResponse,
): SessionState => ({
  accountId: response.accountId ?? 0,
  email: response.email ?? "",
  role: response.role ?? "",
  isActive: response.isActive ?? false,
  isAuthenticated: response.isAuthenticated ?? false,
});

