export const clientRoutes = {
  home: "/",
  register: "/register",
  login: "/login",
  account: "/account",
  requests: "/requests",
  createRequest: "/requests/create",
  requestDetailsPath: "/requests/:requestId",
  employeeRequests: "/employee/requests",
  employeeRequestDetailsPath: "/employee/requests/:requestId",
  agreementExchanges: "/agreement-exchanges",
  agreementExchangeDetailsPath: "/agreement-exchanges/:requestId",
  requestDetails: (requestId: number | string) =>
    `/requests/${encodeURIComponent(String(requestId))}`,
  employeeRequestDetails: (requestId: number | string) =>
    `/employee/requests/${encodeURIComponent(String(requestId))}`,
  agreementExchangeDetails: (requestId: number | string) =>
    `/agreement-exchanges/${encodeURIComponent(String(requestId))}`,
  testUi: "/test-ui",
} as const;
