# SL-EMP-REQ-002 — Employee Request Details Read

Status: full backend/API read slice draft  
Package: `[Employee] [Requests]`  
Slice type: backend/API read slice  
Primary purpose: employee request details by id  
Parent slice: `SL-EMP-REQ-001 — Employee Request List Read`  
Implementation direction: Dapper/read projection, not aggregate repository / EF DTO shaping

## 1. Slice Overview

This slice adds an employee-side request details read endpoint.

Endpoint direction:

```http
GET /api/employee/requests/{requestId}
```

It returns one employee-visible request details payload by id.

This slice is only:

```text
GET by id + details projection + compact review state
```

It does not include filters, commands, action DTOs or client UI.

## 2. Scope

```text
- add employee request details read endpoint;
- get one request by requestId;
- use Dapper/read projection;
- return employee request details payload;
- include request status/type;
- include full request details text;
- include applicant summary/contact fields needed for employee review context;
- include object address;
- include compact review state;
- do not load/mutate Request aggregate;
- do not mutate request/review state.
```

## 3. Out of Scope

| Out of scope | Owner |
|---|---|
| Employee request list / filters | `SL-EMP-REQ-001 — Employee Request List Read` |
| Start review command | future `SL-EMP-REQ-003 — Start Request Review` |
| Approve review command | future `SL-EMP-REQ-004 — Approve Request Review` |
| Reject review command | future `SL-EMP-REQ-005 — Reject Request Review` |
| Action DTO / command affordance DTO | future command/client slices |
| Employee dashboard/client UI | future `.client` sidecar |
| Employee profile/display-name read model | future Employee profile/read slice |
| AgreementProposalExchange | future agreement proposal slices |
| Review domain mutation | domain/command slices |
| Query filters / paging / sorting | not this slice |

## 4. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns employee request list read endpoint and filters.

SL-EMP-REQ-002
  Owns employee request details by id.

SL-EMP-REQ-003 / 004 / 005
  Own StartReview / ApproveReview / RejectReview commands.

L2 Domain Draft
  Owns target domain concepts:
  Employee,
  Request-owned Review,
  Start/Started terminology,
  no EmployeeRef,
  no ReviewDecisionRecord.

CC-VALIDATION-001
  Owns request/query validation boundary.
  Minimal/no validation in this slice because there is no body/query.

CC-API-001
  Owns OpenAPI/generated artifact workflow if API contract changes.
```

## 5. Sources / Source Behavior Items

Scenario Flow and Behavior Coverage must come from scenario source files, not from this slice locally.

Primary scenario sources after L2 scenario sync:

```text
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md
```

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Important distinction:

```text
The domain draft informs domain terminology, review state and read-model semantics.
Scenario text/DATA/UI/behavior files remain source of truth for Scenario Flow and Behavior Coverage.
```

Stable behavior item IDs for this details slice may still need final mapping in the behavior source file.
Until then, use `Source BI TBD` and do not invent final IDs inside this slice.

## 6. Visual Scenario Flow

```text
[Signed-in Employee]
opens request from employee request list
        ↓
System loads request by id
        ↓
System shows request details
```

Details include:

```text
- request status/type;
- applicant summary/contact;
- object address;
- full request details text;
- compact review state.
```

## 7. Visual Implementation Flow

```text
[HTTP GET]
GET /api/employee/requests/{requestId}
        ↓
[Auth / Employee context]
resolve current Employee id
        ↓
[Route binding]
requestId is long
        ↓
[Query handler / Dapper read projection]
query request details by requestId for current Employee context
project directly into read DTO
        ↓
[Response]
return EmployeeRequestDetailsDto
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, response.

Validator:
  no body/query validator in first pass.

Query handler:
  Dapper query;
  employee read visibility;
  direct DTO projection.

Domain:
  source semantics only:
  Request.Status,
  Request.Review.Status,
  Request.Review.StartedByEmployeeId.

Aggregate repositories:
  not used for DTO shaping in this read slice.
```

Read/query convenience should not change write aggregate shape.

## 8. API Contract Draft

### 8.1 Endpoint

```http
GET /api/employee/requests/{requestId}
```

Route:

```text
requestId: long
```

### 8.2 Response

```ts
type EmployeeRequestDetailsDto = {
  requestId: number;
  requestType: "Connection";

  status:
    | "InReview"
    | "Approved"
    | "Rejected"
    | "AgreementExchangeFailed";

  applicant: EmployeeRequestApplicantSummaryDto;

  objectAddress: string;
  details: string;
  createdAt: string;

  reviewState:
    | "NotStarted"
    | "StartedByCurrentEmployee"
    | "StartedByAnotherEmployee"
    | "Approved"
    | "Rejected";
};
```

### 8.3 Applicant summary

```ts
type EmployeeRequestApplicantSummaryDto = {
  applicantPartyId: number;
  applicantPartyType: "Individual";
  displayName: string;
  email?: string;
  phoneNumber?: string;
};
```

No extra DTOs in this slice:

```text
- no list response DTO;
- no filters;
- no action DTO;
- no command affordance DTO;
- no Employee profile/display-name DTO;
- no AgreementProposalExchange DTO.
```

## 9. Validation / ProblemDetails

No request body.

No query.

No FluentValidation validator required in first pass.

```text
- route constraint/model binding handles requestId shape;
- missing request / not-visible request is query-handler responsibility;
- malformed route can use ASP.NET route/model-binding behavior.
```

Do not put these into validators:

```text
- Employee exists;
- current user is Employee;
- request visibility;
- request belongs to employee-visible pool;
- review state;
- DB reads;
- transactions;
- mutations.
```

## 10. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Resolve current authenticated Employee. Employee auth/account may be dependency/blocker. |
| Authorization/visibility | yes | Query handler/Dapper projection must return only employee-visible request or documented not-found/visibility response. |
| Antiforgery / unsafe requests | no | Read-only GET. |
| Request validation / ProblemDetails | minimal | No body/query. Route id only. |
| Dapper/read projection | yes | Use Dapper/read model for details. Do not load aggregate just to shape DTO. |
| OpenAPI / generated artifacts | yes | New endpoint/DTO changes API contract; generated artifacts must come from repo commands. |
| Transaction / atomicity | no | Read-only. |
| No-mutation safety | yes | GET must not create review or change request state. |
| Concurrency / stale state | yes | Review state may change after details load; future commands must re-check. |
| Privacy / cross-employee exposure | yes | Do not expose Employee auth/private data. |
| Testing responsibility split | yes | API/read integration tests; no command/UI tests; no unit tests by default. |

## 11. Questions / Decisions

### Accepted

| ID | Status | Question | Decision / direction | Impact |
|---|---|---|---|---|
| `SL-EMP-REQ-002-Q-001` | accepted | Is this a list/filter slice? | No. Details by id only. | Keeps scope separate from list slice. |
| `SL-EMP-REQ-002-Q-002` | accepted | Should this use Dapper? | Yes. Dapper/read projection. | Do not add aggregate repository DTO shaping. |
| `SL-EMP-REQ-002-Q-003` | accepted | Is there query/filter behavior? | No. | No query DTO, no query validator, no filter tests. |
| `SL-EMP-REQ-002-Q-004` | accepted | Include action DTO? | No. | Actions belong to command/client slices. |
| `SL-EMP-REQ-002-Q-005` | accepted | Include full request details text? | Yes. | This is details read. |
| `SL-EMP-REQ-002-Q-006` | accepted | Add unit tests by default? | No. Integration/API tests unless reusable helper logic is introduced. | Prevents test bloat. |

### Assumptions / current direction

| ID | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `SL-EMP-REQ-002-Q-007` | assumption | Does Employee auth/account exist? | Treat as dependency. Implementation may block if missing. | Could require Employee auth slice first. |
| `SL-EMP-REQ-002-Q-008` | assumption | Does details need Employee display name for started-by-other? | No in first pass. Only state. | Avoids Employee profile scope. |
| `SL-EMP-REQ-002-Q-009` | assumption | Should route use `{requestId:long}`? | Yes. | No FluentValidation needed for route. |

## 12. Extension / Change Points

| ID | Area | Current direction | Future owner |
|---|---|---|---|
| `CP-EMP-REQ-DETAILS-001` | Action flags | Not first pass. | Command/client sidecars |
| `CP-EMP-REQ-DETAILS-002` | Employee display name | Not first pass. | Employee profile/read slice |
| `CP-EMP-REQ-DETAILS-003` | Review timestamps | Add only if UI needs StartedAt/CompletedAt. | Details extension |
| `CP-EMP-REQ-DETAILS-004` | Review history | Not first pass. | Future review history slice |
| `CP-EMP-REQ-DETAILS-005` | Agreement proposal state | Not first pass. | Agreement proposal read slice |
| `CP-EMP-REQ-DETAILS-006` | Shared Dapper projection helper | Only if list/details duplication becomes non-trivial. | Helper/read-model cleanup |

## 13. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Employee opens request by id | covered | `GET /api/employee/requests/{requestId}`. |
| Details show full request text | covered | `details` field. |
| Details show applicant summary/contact | covered | compact applicant summary. |
| Details show object address | covered | `objectAddress`. |
| Details show review not started | covered | `reviewState = NotStarted`. |
| Details show review started by current Employee | covered | `reviewState = StartedByCurrentEmployee`. |
| Details show review started by another Employee | covered | `reviewState = StartedByAnotherEmployee`. |
| Details show completed review outcome | covered | `reviewState = Approved` / `Rejected`. |
| List/filter behavior | out of scope | `SL-EMP-REQ-001`. |
| Start/approve/reject behavior | out of scope | Future command slices. |

## 14. Test / Verification Plan

Primary verification: **API/read integration tests**.

Do not add unit tests by default.

Unit tests are allowed only if this slice introduces reusable helper logic with non-trivial branching.

Even then, keep unit tests focused on that helper only.

### API boundary / access tests

```text
- unauthenticated details request returns 401;
- non-Employee/client account returns documented rejection;
- authenticated Employee gets 200 for visible request by id;
- missing request id returns documented not-found response;
- not-visible request id returns documented not-found/visibility response.
```

### Details payload test

One focused integration test:

```text
- returns requestId;
- returns requestType;
- returns status;
- returns full details text;
- returns applicant summary/contact;
- returns objectAddress;
- returns reviewState.
```

### Review state coverage

Keep compact:

```text
- no Review -> NotStarted;
- Review started by current Employee -> StartedByCurrentEmployee;
- Review started by another Employee -> StartedByAnotherEmployee.
```

Approved/rejected states may be one additional focused test if cheap:

```text
- approved reviewed request -> Approved;
- rejected reviewed request -> Rejected.
```

### No-mutation safety

Optional smoke only if cheap:

```text
- GET details does not create Review;
- GET details does not change Request.Status;
- GET details does not change Review.Status.
```

### What not to test

```text
- no unit tests by default;
- no validator unit tests;
- no query validator tests;
- no list/filter endpoint;
- no StartReview command;
- no ApproveReview command;
- no RejectReview command;
- no AgreementProposalExchange behavior;
- no client UI;
- no repository mock call-order as primary proof;
- no generated TypeScript as behavior proof.
```

## 15. Implementation Checklist

```text
[ ] Verify Employee auth/account dependency.
[ ] Verify Request-owned Review state dependency.
[ ] Add Employee request details endpoint.
[ ] Add details response DTO.
[ ] Add compact applicant summary DTO only if not already shared.
[ ] Implement Dapper query/projection by requestId.
[ ] Derive reviewState from request/review state and currentEmployeeId.
[ ] Keep response scoped to details; no action DTOs.
[ ] Add focused API/read integration tests.
[ ] Run OpenAPI/generated artifact workflow if endpoint/DTOs are added.
[ ] Do not implement list/filter endpoint.
[ ] Do not implement review commands.
[ ] Do not mutate request/review state.
[ ] Do not add unit tests unless reusable helper logic requires them.
```

## 16. Next Step

Before implementation, verify blockers:

```text
1. Is Employee auth/account/session already implemented?
2. Is Request-owned Review state already implemented?
3. Can current schema represent Review.Status and StartedByEmployeeId?
4. Is there existing Dapper infrastructure/pattern to use for employee read projections?
```

Recommended next drafts:

```text
SL-EMP-REQ-003 — Start Request Review
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
```
