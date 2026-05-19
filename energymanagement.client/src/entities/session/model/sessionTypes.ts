import type { CurrentUserResponseDto } from "../../../shared/api/authApi";

export type SessionState = {
  accountId: number;
  email: string;
  role: string;
  isActive: boolean;
  isAuthenticated: boolean;
};

const requireCurrentUserField = <T>(
  value: T | null | undefined,
  fieldName: string,
): T => {
  if (value === null || value === undefined) {
    throw new Error(`L1 current-user response is missing ${fieldName}.`);
  }

  return value;
};

export const mapCurrentUserToSession = (
  response: CurrentUserResponseDto,
): SessionState => ({
  accountId: requireCurrentUserField(response.accountId, "accountId"),
  email: requireCurrentUserField(response.email, "email"),
  role: requireCurrentUserField(response.role, "role"),
  isActive: requireCurrentUserField(response.isActive, "isActive"),
  isAuthenticated: requireCurrentUserField(
    response.isAuthenticated,
    "isAuthenticated",
  ),
});
