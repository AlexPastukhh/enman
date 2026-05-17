# SL-EMP-REQ-001 — Employee Request List Read

Status: full backend/API read slice draft  
Package: `[Employee] [Requests]`  
Slice type: backend/API read slice  
Primary purpose: employee-visible request list with filters and compact review state  
Architecture direction: read slice before employee request commands

## 1. Slice Overview

This slice adds an employee-side request list read endpoint.

It returns compact request rows visible to the current authenticated Employee and supports first-pass filters.

Endpoint direction:

```http
GET /api/employee/requests
```

This slice is only:

```text
employee request list + filters + compact review state projection
```

It is not the full employee dashboard/details/review-command scenario.

## 2. Scope

```text
- add employee request list read endpoint;
- return list of employee-visible requests;
- support list filters;
- include compact request row data;
- include compact review state for each row;
- derive whether review is not started, started by current Employee, started by another Employee, approved, or rejected;
- do not mutate request/review state.
```

First-pass filters:

```text
status
reviewState
```

First-pass non-goal:

```text
pagination/sorting unless existing repo pattern requires it during implementation.
```

## 3. Out of Scope

| Out of scope | Owner |
|---|---|
| Request details endpoint | `SL-EMP-REQ-002 — Employee Request Details Read` |
| Start review command | future `SL-EMP-REQ-003 — Start Request Review` |
| Approve review command | future `SL-EMP-REQ-004 — Approve Request Review` |
| Reject review command | future `SL-EMP-REQ-005 — Reject Request Review` |
| Employee dashboard/client UI | future `.client` sidecar |
| Employee profile/display-name read model | future Employee profile/read slice |
| AgreementProposalExchange | future agreement proposal slices |
| Review domain mutation | domain/command slices |
| ApplicantParty lifecycle/default/edit/delete | ApplicantParty slices |
| Pagination/sorting | extension point unless existing repo pattern requires it |

## 4. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns employee request list read endpoint and filters.

SL-EMP-REQ-002
  Owns employee request details read endpoint.

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
  Owns FluentValidation boundary for query filters.

CC-API-001
  Owns OpenAPI/generated artifact workflow if API contract changes.
```

## 5. Sources / Source Behavior Items

Scenario Flow and Behavior Coverage must come from scenario source files, not from this slice locally.

Primary scenario sources after L2 scenario sync:

```text
planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md
```

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Important distinction:

```text
The domain draft informs domain terminology and boundaries.
Scenario text/DATA/UI/behavior files remain source of truth for Scenario Flow and Behavior Coverage.
```

Stable behavior item IDs for this list slice may still need final mapping in the behavior source file.
Until then, use `Source BI TBD` and do not invent final IDs inside this slice.

## 6. Visual Scenario Flow

```text
[Signed-in Employee]
opens employee request list
        ↓
System shows requests visible to Employee
        ↓
Employee can filter list by request status and/or review state
        ↓
Each row shows compact request summary
        ↓
Each row shows compact review state:
  not started
  started by me
  started by another Employee
  approved
  rejected
```

Scenario meaning:

```text
The list makes started-review state visible before command buttons exist.
Employee can see whether a request is free to start or already being reviewed by another Employee.
```

## 7. Visual Implementation Flow

```text
[HTTP GET]
GET /api/employee/requests?status=...&reviewState=...
        ↓
[Auth / Employee context]
resolve current Employee id
        ↓
[FluentValidation]
validate query filter shape and allowed values
        ↓
[Query handler]
load employee-visible request rows
        ↓
[Projection]
derive compact review state using:
  Request.Status
  Review.Status
  Review.StartedByEmployeeId
  currentEmployeeId
        ↓
[Response]
return list of EmployeeRequestListItemDto
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, validation call, response.

Validator:
  query shape only.

Query handler:
  employee visibility and projection.

Domain model:
  source semantics:
  Request.Status,
  Request.Review.Status,
  Request.Review.StartedByEmployeeId.

Client:
  future rendering.
```

## 8. API / Query Contract Draft

### 8.1 Query

```ts
type EmployeeRequestListQuery = {
  status?: "InReview" | "Approved" | "Rejected" | "AgreementExchangeFailed";
  reviewState?: "NotStarted" | "StartedByCurrentEmployee" | "StartedByAnotherEmployee" | "Approved" | "Rejected";
};
```

First pass:

```text
No pagination in this slice unless existing project read-list pattern requires it.
Pagination can be added as an extension point.
```

### 8.2 Response

```ts
type EmployeeRequestListResponseDto = {
  requests: EmployeeRequestListItemDto[];
};
```

### 8.3 List item

```ts
type EmployeeRequestListItemDto = {
  requestId: number;
  requestType: "Connection";

  status:
    | "InReview"
    | "Approved"
    | "Rejected"
    | "AgreementExchangeFailed";

  applicantDisplayName: string;
  objectAddress: string;
  createdAt: string;

  reviewState:
    | "NotStarted"
    | "StartedByCurrentEmployee"
    | "StartedByAnotherEmployee"
    | "Approved"
    | "Rejected";
};
```

No extra DTOs in this slice:

```text
- no request details DTO;
- no full applicant/contact details;
- no employee profile/display name;
- no action DTO;
- no command affordance DTO.
```

## 9. Query Validation / FluentValidation

If filters are present, add query DTO validator:

```csharp
public sealed record EmployeeRequestListQueryDto(
    string? Status,
    string? ReviewState);
```

Validator owns only:

```text
- status is empty or known request status;
- reviewState is empty or known review-state filter;
- field names in 422 ProblemDetails.
```

Validator does not own:

```text
- Employee exists;
- current user is Employee;
- request visibility;
- review started by another Employee;
- can/cannot start review;
- ownership;
- DB reads;
- transactions;
- mutations.
```

Suggested field names:

```csharp
public static class EmployeeRequestListFieldNames
{
    public const string Status = "status";
    public const string ReviewState = "reviewState";
}
```

## 10. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Must resolve current authenticated Employee. Employee auth/account may be dependency/blocker. |
| Authorization/visibility | yes | Query handler owns employee-visible request filtering. |
| Antiforgery / unsafe requests | no | Read-only GET. |
| Request validation / ProblemDetails | yes | Query filters validated with FluentValidation. Unknown filter values return 422. |
| OpenAPI / generated artifacts | yes | New endpoint/DTOs change API contract; generated artifacts must come from repo commands. |
| Generated constants/error codes | maybe | Review-state/status constants may need generated constants later. |
| Transaction / atomicity | no | Read-only. |
| No-mutation safety | yes | GET must not create review or change request state. |
| Idempotency / retry | no | GET is repeatable. |
| Concurrency / stale state | yes | Review state may change after list is loaded; commands must re-check later. |
| File/document boundary | no | No documents in list slice. |
| Clock/audit actor fields | read only | Reads existing timestamps only. |
| Privacy / cross-employee exposure | yes | Show started-by-other state without leaking employee private/auth data. |
| Testing responsibility split | yes | API/read integration tests; no command/UI tests; no unit tests by default. |

## 11. Questions / Decisions

### Accepted

| ID | Status | Question | Decision / direction | Impact |
|---|---|---|---|---|
| `SL-EMP-REQ-001-Q-001` | accepted | Is this a details/dashboard scenario slice? | No. List endpoint + filters only. | Keeps scope small. |
| `SL-EMP-REQ-001-Q-002` | accepted | Use Employee or Worker? | Employee. | Aligns L2 terminology. |
| `SL-EMP-REQ-001-Q-003` | accepted | Include action DTO? | No. Not in list slice. | Actions belong to details/client/command slices. |
| `SL-EMP-REQ-001-Q-004` | accepted | Include full applicant/contact details? | No. Compact row only. | Details slice owns full data. |
| `SL-EMP-REQ-001-Q-005` | accepted | Validate filters with FluentValidation? | Yes, if filters are present. | Unknown filter values return 422. |
| `SL-EMP-REQ-001-Q-006` | accepted | Add unit tests by default? | No. Use integration/API tests unless reusable helper logic is introduced. | Prevents test bloat. |

### Assumptions / current direction

| ID | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `SL-EMP-REQ-001-Q-007` | assumption | Does Employee auth/account exist? | Treat as dependency. Implementation may block if missing. | Could require Employee auth slice first. |
| `SL-EMP-REQ-001-Q-008` | assumption | Include pagination? | No in first pass unless existing repo pattern requires it. | Pagination can be extension. |
| `SL-EMP-REQ-001-Q-009` | assumption | What review states are filterable? | Same compact states returned by list item. | Keeps filter/result vocabulary aligned. |

## 12. Extension / Change Points

| ID | Area | Current direction | Future owner |
|---|---|---|---|
| `CP-EMP-REQ-LIST-001` | Pagination | Not first pass. Add later if needed. | This slice extension |
| `CP-EMP-REQ-LIST-002` | Sorting | Not first pass. | Future list UX/read slice |
| `CP-EMP-REQ-LIST-003` | Employee display name | Not first pass. | Employee profile/read slice |
| `CP-EMP-REQ-LIST-004` | Action flags | Not first pass. | Details/client/command sidecars |
| `CP-EMP-REQ-LIST-005` | Assignment/queue semantics | Not first pass. | Future employee assignment slice |
| `CP-EMP-REQ-LIST-006` | Reusable review-state derivation helper | Only if projection logic becomes duplicated/non-trivial. | Helper slice or local helper |

## 13. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Employee can see request list | covered | `GET /api/employee/requests`. |
| Employee can filter request list | covered | `status`, `reviewState` query filters. |
| List row shows compact request summary | covered | list item DTO. |
| List row shows review not started | covered | `reviewState = NotStarted`. |
| List row shows review started by current Employee | covered | `reviewState = StartedByCurrentEmployee`. |
| List row shows review started by another Employee | covered | `reviewState = StartedByAnotherEmployee`. |
| List row shows completed review outcome | covered | `reviewState = Approved` / `Rejected`. |
| Request details behavior | out of scope | Future details slice. |
| Start/approve/reject behavior | out of scope | Future command slices. |

Source behavior ID note:

```text
Stable scenario behavior IDs for Employee request list are not yet referenced here.
If scenario/register IDs are added or found, map this table to those IDs.
```

## 14. Test / Verification Plan

Primary verification: **API/read integration tests**.

Do not add unit tests by default.

Unit tests are allowed only if this slice introduces reusable helper logic with non-trivial branching, for example:

```text
- reviewState derivation helper;
- filter parsing helper;
- field-name mapping helper.
```

Even then, keep unit tests focused on that helper only.

Endpoint behavior, auth, validation, visibility, filtering and response shape are verified through integration tests.

### API boundary / access tests

```text
- unauthenticated request list returns 401;
- non-Employee/client account returns documented rejection;
- authenticated Employee gets 200.
```

### Query validation tests

Keep the set small:

```text
- unknown status -> 422 ProblemDetails;
- unknown reviewState -> 422 ProblemDetails;
- valid status + reviewState filters -> 200.
```

Do not create one tiny test per DTO property unless a real boundary risk exists.

### List read correctness tests

Prefer one mixed dataset test:

```text
Arrange:
- request with no Review;
- request with Review started by current Employee;
- request with Review started by another Employee;
- approved reviewed request;
- rejected reviewed request.

Assert list rows contain:
- NotStarted;
- StartedByCurrentEmployee;
- StartedByAnotherEmployee;
- Approved;
- Rejected.
```

This proves the review-state projection without a large matrix of separate tests.

### Filtering tests

```text
- one status filter test narrows rows;
- one reviewState filter test narrows rows.
```

### No-mutation safety tests

Optional single smoke test only if cheap with existing helpers:

```text
- GET list does not create Review;
- GET list does not change Request.Status;
- GET list does not change Review.Status.
```

### What not to test

```text
- no unit tests by default;
- no validator unit tests by default;
- no query-handler unit tests with mocks as primary proof;
- no helper unit tests unless reusable helper has meaningful branching;
- no request details endpoint;
- no StartReview command;
- no ApproveReview command;
- no RejectReview command;
- no AgreementProposalExchange behavior;
- no client UI;
- no React Query/cache behavior;
- no repository mock call-order as primary proof;
- no generated TypeScript as behavior proof;
- do not create many tiny tests for every DTO property if integration tests already cover the boundary.
```

## 15. Implementation Checklist

```text
[ ] Verify Employee auth/account dependency.
[ ] Verify Request-owned Review state dependency.
[ ] Add Employee request list endpoint.
[ ] Add list query DTO with status/reviewState filters.
[ ] Add FluentValidation query validator.
[ ] Add field-name constants for query filters.
[ ] Add response DTO with requests array.
[ ] Add compact list item DTO.
[ ] Implement employee-visible request projection.
[ ] Derive reviewState from request/review state and currentEmployeeId.
[ ] Keep response compact; no details/contact/action DTOs.
[ ] Add focused API/read integration tests.
[ ] Run OpenAPI/generated artifact workflow if endpoint/DTOs are added.
[ ] Do not implement details endpoint.
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
```

Recommended next drafts:

```text
SL-EMP-REQ-002 — Employee Request Details Read
SL-EMP-REQ-003 — Start Request Review
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
```
