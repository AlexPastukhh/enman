# L2-EMP-DETAILS-001.client — Employee Request Details

Status: full client read sidecar draft / details read server contract pending / API placement synchronized

## 1. Scope

Owns Employee request details route/page, read state, request/applicant/client data display, review state display, action availability display, optional future action slot and navigation back to dashboard.

## 2. Out of Scope

Start/approve/reject command execution, details server endpoint implementation, proposal exchange, domain review persistence, local CSRF mechanics, manual generated edits.

## 3. Scenario Flow

```text
Signed-in Employee opens request details
  -> sees request data and applicant/client data
  -> sees review state
  -> sees which review actions are available/blocked
  -> can return to dashboard
```

## 4. Client Implementation Flow

```text
pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
  -> entities/employee-request/model/useEmployeeRequestDetailsQuery.ts
  -> entities/employee-request/api/getEmployeeRequestDetails.ts
  -> shared/api/fetchJson.ts + shared/api/generated/openapi-types.ts
  -> entities/employee-request/ui/*
  -> optional feature action slot
```

## 5. Client API / Server Contract

`entities/employee-request/api/getEmployeeRequestDetails.ts` owns the read endpoint wrapper and generated DTO aliases.

StartReviewResponseDto is not the details DTO. Details read waits for `SL-EMP-REQ-002`.


## API Placement Correction

Current accepted rule:

```text
read endpoint wrappers -> entities/*/api
command endpoint wrappers -> features/*/api
shared/api -> fetchJson / ProblemDetails / CSRF helpers / generated OpenAPI types only
```

Business-specific shared API wrappers are transitional compatibility and should not be copied into new implementation.


## 6. Cross-Cutting

Safe GET; Employee session required; server owns visibility; 404/403 map to safe UI states; generated contract required.

## 7. Verification

Component tests for states/details/review/action availability; entity API/query tests; E2E details happy/access/not-found paths.
