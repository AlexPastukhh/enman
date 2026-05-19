import type { components } from "../../../../shared/api/generated/openapi-types";
import { refreshAntiforgeryToken } from "../../../../shared/api/antiforgeryTokenStore";
import { fetchJson } from "../../../../shared/api/fetchJson";

export type EmployeeWindowsSignInResponse =
  components["schemas"]["CurrentUserResponseDto"];

export const signInEmployeeWithWindows = (): Promise<EmployeeWindowsSignInResponse> =>
  fetchJson<EmployeeWindowsSignInResponse>("/api/employee/auth/windows-signin", {
    method: "GET",
  }).then(async (response) => {
    await refreshAntiforgeryToken();
    return response;
  });
