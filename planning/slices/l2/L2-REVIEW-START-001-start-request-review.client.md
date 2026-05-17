# L2-REVIEW-START-001.client — Start Request Review

Status: full client command sidecar draft / blocked until `SL-EMP-REQ-003` server endpoint and generated OpenAPI contract exist / API placement synchronized

## 1. Scope

Owns Start Review button/action/mutation on Employee request details page, available/blocked state, pending state, command submit, success refresh, error feedback and no blind retry.

## 2. Out of Scope

Backend endpoint, details/list read endpoints, approve/reject commands, agreement proposal exchange, employee assignment/queue, local CSRF mechanics, manual generated edits.

## 3. Scenario Flow

```text
Employee opens request details
  -> Start Review is available
  -> Employee clicks Start Review
  -> pending state visible
  -> success shows StartedByCurrentEmployee after refresh
  -> rejection shows error and keeps previous state safe
```

## 4. Client Implementation Flow

```text
pages/employee/requests/details/EmployeeRequestDetailsPage.tsx
  -> entities/employee-request/ui/EmployeeRequestDetailsView.tsx
  -> features/employee-request/start-review/ui/StartReviewButton.tsx
  -> features/employee-request/start-review/model/useStartRequestReviewMutation.ts
  -> features/employee-request/start-review/api/startRequestReview.ts
  -> shared/api/fetchJson.ts + shared/api/antiforgery helpers + shared/api/generated/openapi-types.ts
```

## 5. Client API / Server Contract

`features/employee-request/start-review/api/startRequestReview.ts` owns:

```text
POST /api/employee/requests/{requestId}/review/start
```

and generated command response alias.


## API Placement Correction

Current accepted rule:

```text
read endpoint wrappers -> entities/*/api
command endpoint wrappers -> features/*/api
shared/api -> fetchJson / ProblemDetails / CSRF helpers / generated OpenAPI types only
```

Business-specific shared API wrappers are transitional compatibility and should not be copied into new implementation.


## 6. Cross-Cutting

Unsafe POST; Employee session required; server owns visibility/reviewability; shared CSRF-aware transport; no auto-replay; stale state rejection refreshes/shows error.

## 7. Verification

Component tests for enabled/disabled/pending/error; feature API/model tests for POST/no-body/generated type/success refresh; E2E happy and blocked/stale paths.
