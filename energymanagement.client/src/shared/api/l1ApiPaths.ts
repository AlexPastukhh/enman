import type { paths } from "./generated/openapi-types";

type OpenApiPath = keyof paths;

export const l1ApiPaths = {
  register: "/api/auth/register",
  login: "/api/auth/login",
  currentUser: "/api/auth/current-user",
  logout: "/api/auth/logout",
} as const satisfies Record<string, OpenApiPath>;
