# SL-EMP-REQ-003 — Start Request Review

Status: refactored server/backend slice draft / implementation sync pending  
Logical slice: Employee Request Review command family  
Package: `[Employee] [Requests]`  
Slice type: server/API command slice  
Primary purpose: Employee starts request-owned Review for one request  
Draft refactor mode: docs-only; runtime implementation was not rechecked in this pass

Parent slices:
- `SL-EMP-REQ-001 — Employee Request List Read`
- `SL-EMP-REQ-002 — Employee Request Details Read`

Related client sidecar:
- `L2-REVIEW-START-001.client — Start Request Review`

## 0. Scenario Sources

Business scenario:
- `planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md`

Related read scenario context:
- `planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md`
- `planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md`

Behavior items:
- `planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md`

Domain-design input:
- `planning/tables/domain-drafts/domain-draft-02.md`

Cross-cutting behavior:
- `CC-CSRF-001` for unsafe browser command protection
- `CC-VALIDATION-001` for API validation / ProblemDetails conventions
- `CC-API-001` for OpenAPI/generated artifact workflow

Source note:

```text
Scenario text/DATA/UI/behavior files remain the source of truth for behavior.
Domain drafts inform terminology, aggregate boundaries and invariants.
This slice draft must not invent final behavior item IDs when source IDs are missing.
```

## 0.1 Source / Domain / Slice Coverage Snapshot

Source registry status:

```text
Scenario source registry was not available in the uploaded archive used for this pass.
Versions are therefore marked as pending source-sync registry.
```

| Behavior label | Source/version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| `EMP-REQ-START-B01` | pending registry | provided/owned by Request domain model | call domain start-review method from API/application | provisional label, not final source ID |
| `EMP-REQ-START-B02` | pending registry | partially provided | persist started Review state | domain mutates aggregate; application saves |
| `EMP-REQ-START-B03` | pending registry | not domain-only | resolve authenticated Employee and enforce Employee API boundary | actor comes from session/claims |
| `EMP-REQ-START-B04` | pending registry | application/read-model responsibility | list/details later show `StartedByCurrentEmployee` | read slices own read DTOs |
| `EMP-REQ-START-B05` | pending registry | domain/application | failed start does not mutate request/review | command tests must prove no-mutation |

## 0.2 Implementation Sync Status

Implementation status:

```text
draft-refactor-only / implementation-not-rechecked
```

Known draft drift corrected by this refactor:

```text
- old draft described preferred 200 OK with response DTO;
- current command family direction is 204 No Content and read-state refresh from read endpoints;
- old draft allowed an idempotent duplicate-start assumption;
- first-pass draft now treats already-started review as 422/no-mutation;
- old draft did not have Source / Domain / Slice Coverage Snapshot;
- old draft did not have Behavior-to-Test Trace.
```

Implementation inspection:

```text
Skipped intentionally in this pass per user direction.
If code/test drift must be checked later, run implemented slice sync workflow before changing runtime code.
```

## 1. Slice Overview

This slice adds the Employee command endpoint for starting review of one request.

Endpoint:

```http
POST /api/employee/requests/{requestId}/review/start
```

Meaning:

```text
Start review is the explicit moment when an Employee takes a request into active review.
It is not approval.
It is not rejection.
It is not agreement exchange creation.
It does not send an agreement proposal.
```

Target state after success:

```text
Request remains in high-level InReview lifecycle.
Request-owned Review becomes Started.
Review records started Employee and started timestamp.
Employee list/details can derive reviewState = StartedByCurrentEmployee.
```

## 2. Scope

```text
- Employee-only command endpoint;
- current Employee resolution from authenticated session;
- route id validation / route constraint;
- load request aggregate;
- use temporary first-pass visibility policy unless a stricter assignment policy exists;
- start request-owned Review through domain method;
- persist started review state;
- return 204 No Content;
- provide integration-testable behavior contract.
```

Target transition:

```text
Review.NotStarted
        ↓ StartReview(Employee)
Review.Started
```

## 3. Out of Scope

| Out of scope | Owner |
|---|---|
| Employee request list / filters | `SL-EMP-REQ-001` |
| Employee request details read | `SL-EMP-REQ-002` |
| Approve review command | `SL-EMP-REQ-004` |
| Reject review command | `SL-EMP-REQ-005` |
| Rejection feedback | `SL-EMP-REQ-005` |
| AgreementProposalExchange creation | agreement exchange slices |
| First agreement proposal | agreement exchange slices |
| Employee UI button/action rendering | client sidecar |
| Full assignment/queue model | future assignment/queue slice |
| Notifications | future notification slice |
| Review history beyond started fields | future audit/history slice |
| UI refactoring / redirects / page flow | separate UI/page-flow audit |

## 4. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns employee request list read endpoint and compact review state in list rows.

SL-EMP-REQ-002
  Owns employee request details read endpoint and compact review state in details payload.

SL-EMP-REQ-003
  Owns StartReview command.

SL-EMP-REQ-004
  Owns ApproveReview command.

SL-EMP-REQ-005
  Owns RejectReview command and rejection feedback.

L2-REVIEW-START-001.client
  Owns UI action placement, click handling, pending/error state and read refetch.

CC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.

CC-VALIDATION-001
  Owns validation/ProblemDetails conventions.

CC-API-001
  Owns OpenAPI/generated artifact workflow.
```

## 5. Scenario Flow

```text
[Signed-in Employee]
opens employee request list or request details
        ↓
system shows review state as not started
        ↓
Employee chooses Start review
        ↓
system verifies Employee command boundary and request lifecycle
        ↓
system starts request-owned Review for the authenticated Employee
        ↓
system returns 204 No Content
        ↓
client refetches list/details
        ↓
read model shows reviewState = StartedByCurrentEmployee
```

## 6. Implementation Flow

```text
[HTTP POST]
POST /api/employee/requests/{requestId}/review/start
        ↓
[CSRF boundary]
validate unsafe browser request through CC-CSRF-001
        ↓
[Auth / Employee context]
resolve current Employee from authenticated L1 account claims
        ↓
[Route binding]
requestId is long and >= 1
        ↓
[Application command]
load Employee and request aggregate
        ↓
[Visibility / reviewability]
apply first-pass Employee review visibility policy
        ↓
[Domain]
request.StartReview(employee, now)
        ↓
[Persistence]
SaveChanges
        ↓
[Response]
204 No Content
```

Ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, CSRF attribute, response mapping.

Application command/handler:
  current Employee resolution;
  request aggregate load;
  first-pass visibility/reviewability policy;
  SaveChanges.

Domain:
  request-owned Review lifecycle;
  Employee active/review capability;
  NotStarted -> Started transition;
  already-started and non-reviewable lifecycle rejection.

Persistence:
  Review started state, actor and timestamp.

Client:
  not in this server slice.
```

## 7. API Contract

### 7.1 Endpoint

```http
POST /api/employee/requests/{requestId}/review/start
```

Route:

```text
requestId: long, min 1
```

### 7.2 Auth

```text
Employee only.
```

The client must not send Employee id in the body. The Employee actor comes from authenticated server-side context.

### 7.3 CSRF

Required for browser request:

```text
X-CSRF-TOKEN
```

CSRF behavior is owned by `CC-CSRF-001`.

### 7.4 Request body

```text
none
```

Do not define a command body for first pass.

### 7.5 Success response

```http
204 No Content
```

Response body:

```text
none
```

Read state is refreshed through:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
```

Do not create `StartRequestReviewResponseDto` for this first pass.

### 7.6 Failure responses

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Employee / cannot access employee command boundary

404 NotFound
  request does not exist or is not visible to Employee

422 UnprocessableEntity
  lifecycle/domain rejection:
    inactive Employee;
    request is not InReview;
    review already started;
    request cannot be reviewed

400 Bad Request
  antiforgery failure only, owned by CC-CSRF-001
```

## 8. Validation / ProblemDetails

Shape validation:

```text
requestId must be long and >= 1.
```

No body validation in first pass.

Validator does not own:

```text
- Employee exists;
- current user is Employee;
- request exists;
- request visibility;
- Review lifecycle;
- duplicate start;
- request approved/rejected lifecycle;
- DB reads;
- mutation.
```

Domain/application lifecycle errors are mapped to the existing validation/ProblemDetails pattern.

## 9. Domain Rules

Target domain direction:

```text
- Use Employee terminology.
- Employee is an Account-derived actor in the L2 target.
- Request owns Review.
- Review is not an aggregate.
- Review starts through Request.StartReview(Employee, startedAt).
- StartReview receives Employee when employee behavior matters.
- Review records started state, started Employee and started timestamp.
```

First-pass duplicate rule:

```text
Already-started Review cannot be started again.
Duplicate start returns lifecycle/domain validation problem.
Existing review row remains unchanged.
```

Request status rule:

```text
StartReview does not approve/reject the request.
Request high-level status remains InReview.
Review state carries Started/StartedByCurrentEmployee semantics.
```

## 10. Security / Protection

| Concern | Direction |
|---|---|
| Auth/session | Employee account is resolved from server-side session/claims |
| Role authorization | Employee-only endpoint |
| Actor spoofing | Employee id is never accepted from request body |
| CSRF | unsafe POST protected by CSRF boundary |
| Visibility | first-pass temporary policy may allow active Employees to review relevant requests; stricter assignment can replace later |
| Data exposure | response body is empty; read DTOs are owned by read slices |
| No-mutation safety | failed command must not create or alter review/request state |

## 11. Questions / Decisions

### Accepted

| ID | Decision | Impact |
|---|---|---|
| `SL-EMP-REQ-003-D-001` | Start review only; no approve/reject. | Keeps final decision slices separate. |
| `SL-EMP-REQ-003-D-002` | Employee actor comes from authenticated context. | Prevents spoofing. |
| `SL-EMP-REQ-003-D-003` | Success is `204 No Content`. | Read state refresh is done through list/details. |
| `SL-EMP-REQ-003-D-004` | No response DTO in first pass. | Keeps command compact. |
| `SL-EMP-REQ-003-D-005` | Already-started review returns lifecycle/domain validation problem. | Avoids ambiguous idempotency. |
| `SL-EMP-REQ-003-D-006` | No client UI in this server slice. | UI sidecar owns button/feedback/refetch. |
| `SL-EMP-REQ-003-D-007` | No AgreementProposalExchange behavior. | Agreement exchange slices own that flow. |

### Assumptions

| ID | Assumption | Impact |
|---|---|---|
| `SL-EMP-REQ-003-A-001` | Temporary Employee visibility policy remains acceptable first pass. | Future assignment/queue can replace it. |
| `SL-EMP-REQ-003-A-002` | Review started timestamp uses server UTC time. | Tests should not require exact timestamp equality. |
| `SL-EMP-REQ-003-A-003` | Stable source behavior IDs are pending scenario registry. | Use provisional labels only in this draft. |

### Open

| ID | Question | Current direction |
|---|---|---|
| `SL-EMP-REQ-003-O-001` | Exact stable error code for already-started review. | Use existing domain validation error if present; otherwise add explicit code during implementation sync. |
| `SL-EMP-REQ-003-O-002` | Whether stricter assignment/queue visibility is needed. | Future slice; not first pass. |
| `SL-EMP-REQ-003-O-003` | Whether source registry versions exist. | Add when source-sync registry is applied. |

## 12. Behavior Coverage

| Behavior label | Scenario/source meaning | Covered by this slice? | Verification direction |
|---|---|---|---|
| `EMP-REQ-START-B01` | Employee can start review for a not-started InReview request. | yes | API integration + DB review state |
| `EMP-REQ-START-B02` | Started Review is associated with authenticated Employee. | yes | DB `StartedByEmployeeId` |
| `EMP-REQ-START-B03` | Command does not return state body; read state comes from read endpoints. | yes | 204 + read refetch/read endpoint proof |
| `EMP-REQ-START-B04` | Unauthenticated user cannot start review. | yes | 401 |
| `EMP-REQ-START-B05` | Client/non-Employee cannot start employee review. | yes | 403 |
| `EMP-REQ-START-B06` | Missing/not-visible request cannot be started. | yes | 404 |
| `EMP-REQ-START-B07` | Inactive Employee cannot start review. | yes | 422/no mutation |
| `EMP-REQ-START-B08` | Already-started review cannot be started again. | yes | 422/no mutation |
| `EMP-REQ-START-B09` | Approved/rejected/non-InReview request cannot be started. | yes | 422/no mutation |
| `EMP-REQ-START-B10` | Failed start does not alter request/review. | yes | DB snapshot/no mutation |
| `EMP-REQ-START-B11` | Approve/reject remain unavailable in this slice. | out of scope | owned by `SL-EMP-REQ-004/005` |
| `EMP-REQ-START-B12` | UI button rendering and redirects. | out of scope | client/page-flow audit later |

## 13. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior label | Scenario outcome being proved | Test layer | Implementation used as mechanism | Escape risk | Refactor risk | Planned / expected test |
|---|---|---|---|---|---|---|
| `EMP-REQ-START-B01` | Employee starts review for not-started request. | API integration + DB assertion | HTTP POST, auth cookie, CSRF token, DB read | Low if DB review state and read state are asserted | Low/Medium: DB helper may change with schema | `StartRequestReview_StartsReviewAndReturnsNoContent` |
| `EMP-REQ-START-B02` | Started review belongs to current Employee. | API integration + DB assertion | authenticated Employee id + review row | Low if `StartedByEmployeeId` is asserted | Low | same success test |
| `EMP-REQ-START-B03` | Command success has no body; read state is refreshed elsewhere. | API integration + read endpoint assertion | response status/body + details read | Low | Low | success test + details read assertion |
| `EMP-REQ-START-B04` | Anonymous user cannot start review. | API integration | unauthenticated HTTP client | Low | Low | `StartRequestReview_WithoutAuth_ReturnsUnauthorized` |
| `EMP-REQ-START-B05` | Client account cannot start Employee review. | API integration | authenticated Client role | Low | Low | `StartRequestReview_WithClientAccount_ReturnsForbidden` |
| `EMP-REQ-START-B06` | Missing request is not started. | API integration | missing route id | Low | Low | `StartRequestReview_ForMissingRequest_ReturnsNotFound` |
| `EMP-REQ-START-B07` | Inactive Employee is rejected and no review is created. | API integration + DB assertion | inactive employee fixture + DB read | Low | Medium: fixture helper may change | inactive Employee test |
| `EMP-REQ-START-B08` | Already-started review is rejected and original review remains unchanged. | API integration + DB snapshot | preinserted review row + post-failure read | Low if started-by/timestamp unchanged are asserted | Medium: schema helper may change | already-started no-mutation test |
| `EMP-REQ-START-B09` | Non-InReview request cannot start review. | API integration + DB assertion | DB status fixture + HTTP POST | Low if request status and review absence are asserted | Medium | non-InReview no-mutation test |

### Required no-mutation checks

```text
- failed start does not create Review;
- failed start does not alter existing Review;
- failed start does not change Request status;
- failed start does not alter applicant party/details/address.
```

### What not to test here

```text
- repository mock call order;
- exact SaveChanges count;
- MediatR pipeline internals;
- generated TypeScript as behavior proof;
- OpenAPI generation as behavior proof;
- client button rendering;
- route redirect/page flow;
- approve/reject behavior;
- AgreementProposalExchange behavior;
- full CSRF matrix duplication.
```

## 14. OpenAPI / Generated Artifacts

If endpoint shape changes:

```powershell
npm run generate:api
npm run check:api
```

Current first-pass contract expected by this draft:

```text
POST /api/employee/requests/{requestId}/review/start
204 No Content
no request body
no response DTO
```

## 15. Implementation / Refactor Checklist

Docs-only refactor checklist:

```text
[ ] Add Source / Domain / Slice Coverage Snapshot.
[ ] Replace old 200 OK response section with 204 No Content.
[ ] Remove StartRequestReviewResponseDto from first-pass contract.
[ ] Replace duplicate-start idempotency assumption with 422/no-mutation behavior.
[ ] Add Behavior-to-Test Trace.
[ ] Mark behavior labels as provisional until scenario registry IDs exist.
[ ] Keep UI/page-flow/redirect work out of this draft.
```

Runtime implementation checklist for a later implementation-sync pass:

```text
[ ] Verify controller endpoint matches contract.
[ ] Verify handler calls domain lifecycle method and persists.
[ ] Verify domain duplicate-start behavior matches this draft.
[ ] Verify tests prove behavior/no-mutation without testing internals.
[ ] Verify command result style against current guardrails.
```

## 16. Next Step

Recommended next draft-only refactor:

```text
SL-EMP-REQ-004 — Approve Request Review
```

Separate later work:

```text
Client page flow / redirects / UI route audit.
```
