import type { components } from "./generated/openapi-types";
import { fetchJson } from "./fetchJson";
import { l1ApiPaths } from "./l1ApiPaths";

export type L1RegisterRequest =
  components["schemas"]["L1RegisterClientAccountDto"];
export type L1RegisterResponse =
  components["schemas"]["L1RegisterClientAccountResponse"];
export type L1LoginRequest = components["schemas"]["L1LoginRequest"];
export type L1CurrentUserResponse =
  components["schemas"]["L1CurrentUserResponse"];

export const registerClientAccount = (
  request: L1RegisterRequest,
): Promise<L1RegisterResponse> =>
  fetchJson<L1RegisterResponse>(l1ApiPaths.register, {
    method: "POST",
    body: JSON.stringify(request),
  });

export const loginClientAccount = (
  request: L1LoginRequest,
): Promise<L1CurrentUserResponse> =>
  fetchJson<L1CurrentUserResponse>(l1ApiPaths.login, {
    method: "POST",
    body: JSON.stringify(request),
  });

export const getCurrentUser = (): Promise<L1CurrentUserResponse> =>
  fetchJson<L1CurrentUserResponse>(l1ApiPaths.currentUser, {
    method: "GET",
  });

export const logoutClientAccount = (): Promise<void> =>
  fetchJson<void>(l1ApiPaths.logout, {
    method: "POST",
  });

