export const clientRoutes = {
  home: "/",
  register: "/register",
  login: "/login",
  account: "/account",
  requests: "/requests",
  createRequest: "/requests/create",
  requestDetailsPath: "/requests/:requestId",
  requestDetails: (requestId: number | string) =>
    `/requests/${encodeURIComponent(String(requestId))}`,
  testUi: "/test-ui",
} as const;
