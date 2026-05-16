export const clientRoutes = {
  home: "/",
  register: "/register",
  login: "/login",
  account: "/account",
  requests: "/requests",
  requestDetailsPath: "/requests/:requestId",
  requestDetails: (requestId: number | string) =>
    `/requests/${encodeURIComponent(String(requestId))}`,
  testUi: "/test-ui",
} as const;
