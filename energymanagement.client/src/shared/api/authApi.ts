import type { components } from "./generated/openapi-types";
import {
  clearAntiforgeryToken,
  refreshAntiforgeryToken,
} from "./antiforgeryTokenStore";
import { fetchJson } from "./fetchJson";
import { apiPaths } from "./apiPaths";

export type RegisterClientAccountRequest =
  components["schemas"]["RegisterClientAccountDto"];
export type RegisterClientAccountResponse =
  components["schemas"]["RegisterClientAccountResponseDto"];
export type LoginRequestDto = components["schemas"]["LoginRequestDto"];
export type CurrentUserResponseDto =
  components["schemas"]["CurrentUserResponseDto"];

export const registerClientAccount = (
  request: RegisterClientAccountRequest,
): Promise<RegisterClientAccountResponse> =>
  fetchJson<RegisterClientAccountResponse>(apiPaths.register, {
    method: "POST",
    body: JSON.stringify(request),
  });

export const loginClientAccount = (
  request: LoginRequestDto,
): Promise<CurrentUserResponseDto> =>
  fetchJson<CurrentUserResponseDto>(apiPaths.login, {
    method: "POST",
    body: JSON.stringify(request),
  }).then(async (response) => {
    await refreshAntiforgeryToken();
    return response;
  });

export const getCurrentUser = (): Promise<CurrentUserResponseDto> =>
  fetchJson<CurrentUserResponseDto>(apiPaths.currentUser, {
    method: "GET",
  });

export const logoutClientAccount = (): Promise<void> =>
  fetchJson<void>(apiPaths.logout, {
    method: "POST",
  }).then((response) => {
    clearAntiforgeryToken();
    return response;
  });
