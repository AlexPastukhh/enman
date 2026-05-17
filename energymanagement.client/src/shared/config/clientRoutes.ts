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
  agreementExchanges: "/agreements",
  agreementExchangeDetailsPath: "/agreements/:exchangeId",
  employeeAgreementExchanges: "/employee/agreements",
  employeeAgreementExchangeDetailsPath: "/employee/agreements/:exchangeId",
  requestDetails: (requestId: number | string) =>
    `/requests/${encodeURIComponent(String(requestId))}`,
  employeeRequestDetails: (requestId: number | string) =>
    `/employee/requests/${encodeURIComponent(String(requestId))}`,
  agreementExchangeDetails: (exchangeId: number | string) =>
    `/agreements/${encodeURIComponent(String(exchangeId))}`,
  employeeAgreementExchangeDetails: (exchangeId: number | string) =>
    `/employee/agreements/${encodeURIComponent(String(exchangeId))}`,
  testUi: "/test-ui",
} as const;
