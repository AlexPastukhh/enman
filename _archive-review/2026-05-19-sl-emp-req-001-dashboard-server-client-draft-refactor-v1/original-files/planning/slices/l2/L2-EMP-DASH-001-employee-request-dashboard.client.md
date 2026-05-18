# L2-EMP-DASH-001.client — Employee Request Dashboard

Status: full client read sidecar draft / server read contract pending / API placement synchronized

## 1. Scope

Owns Employee dashboard route/page, read state, list rows, review markers, optional status filter, loading/empty/error states and navigation to details.

## 2. Out of Scope

Start/approve/reject commands, details page, server endpoint implementation, employee assignment/queue policy, agreement proposal exchange, local CSRF mechanics, manual generated edits.

## 3. Scenario Flow

```text
Signed-in Employee opens dashboard
  -> sees review-relevant requests
  -> sees row data and review state marker
  -> opens request details
```

## 4. Client Implementation Flow

```text
pages/employee/requests/dashboard/EmployeeDashboardPage.tsx
  -> entities/employee-request/model/useEmployeeRequestDashboardQuery.ts
  -> entities/employee-request/api/listEmployeeDashboardRequests.ts
  -> shared/api/fetchJson.ts + shared/api/generated/openapi-types.ts
  -> entities/employee-request/ui/*
```

## 5. Client API / Server Contract

`entities/employee-request/api/listEmployeeDashboardRequests.ts` owns the read endpoint wrapper and generated DTO aliases.


## API Placement Correction

Current accepted rule:

```text
read endpoint wrappers -> entities/*/api
command endpoint wrappers -> features/*/api
shared/api -> fetchJson / ProblemDetails / CSRF helpers / generated OpenAPI types only
```

Business-specific shared API wrappers are transitional compatibility and should not be copied into new implementation.


## 6. Cross-Cutting

Employee account identity:
  Target L2 model uses Employee : Account.
  ClaimTypes.NameIdentifier stores Account.Id, which is Employee.Id for Employee sessions.
  Client must never submit employeeId; server derives Employee actor from session.

Page placement:
  Employee request-area pages are grouped under pages/employee/requests/*.
  Dashboard page lives in pages/employee/requests/dashboard.
  Details page lives in pages/employee/requests/details.
  Do not place Employee request dashboard under pages/employee/dashboard.


Safe GET; Employee session required; Employee : Account so auth claim Account.Id is Employee.Id; server owns visibility/filtering; generated contract required; UI renders safe read errors.

## 7. Verification

Component tests for rows/markers/states; entity API/query tests for endpoint wrapper/query key; E2E dashboard happy/empty/access paths.
