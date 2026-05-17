# SL-EMP-REQ-003 — Start Request Review

Status: full backend/API command slice draft / implementation-ready after Employee auth + Request-owned Review prerequisites are confirmed  
Package: `[Employee] [Requests]`  
Slice type: backend/API command slice  
Primary purpose: employee starts request-owned Review  
Parent slices:
- `SL-EMP-REQ-001 — Employee Request List Read`
- `SL-EMP-REQ-002 — Employee Request Details Read`

Implementation direction: L2 target domain model — Request-owned Review mutation through `Request.StartReview(Employee)`

## 1. Slice Overview

This slice adds an employee-side command endpoint that starts review for one request.

Endpoint direction:

```http
POST /api/employee/requests/{requestId}/review/start
```

This slice is only:

```text
start request-owned Review for current authenticated Employee
```

Target behavior:

```text
Employee starts review for a request that is not already started/completed.
Request-owned Review records started state, started Employee and started timestamp.
Employee request list/details can then show reviewState = StartedByCurrentEmployee.
```

This slice does not approve or reject the request.

This slice does not create AgreementProposalExchange.

This slice does not send the first agreement proposal.

This slice does not add employee UI.

## 2. Scope

```text
- add employee start-review command endpoint;
- resolve current authenticated Employee;
- load request by requestId;
- verify request is visible/reviewable by current Employee;
- verify request-owned Review can be started;
- call Request.StartReview(currentEmployee);
- persist Review started state;
- return compact command result;
- add API integration tests with DB state assertions.
```

Target review transition:

```text
Review.NotStarted
        ↓ StartReview(Employee)
Review.Started
```

Employee-facing read state after successful command:

```text
StartedByCurrentEmployee
```

This state is consumed by:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
```

## 3. Out of Scope

| Out of scope                                                   | Owner                                            |
| -------------------------------------------------------------- | ------------------------------------------------ |
| Employee request list / filters                                | `SL-EMP-REQ-001 — Employee Request List Read`    |
| Employee request details read                                  | `SL-EMP-REQ-002 — Employee Request Details Read` |
| Approve review command                                         | `SL-EMP-REQ-004 — Approve Request Review`        |
| Reject review command                                          | `SL-EMP-REQ-005 — Reject Request Review`         |
| Rejection feedback                                             | `SL-EMP-REQ-005`                                 |
| AgreementProposalExchange creation                             | future agreement proposal slices                 |
| Employee sends first proposal                                  | future agreement proposal slices                 |
| Employee dashboard/client UI                                   | future `.client` sidecar                         |
| Employee profile/display-name read model                       | future Employee profile/read slice               |
| Full assignment/queue model                                    | future employee assignment/review queue slice    |
| Notifications                                                  | future notification slice                        |
| Review history/audit log beyond required Review started fields | future audit/review-history slice                |
| Legacy runtime cleanup                                         | backend cleanup / compatibility task             |
| Broad validation matrix                                        | future validation matrix testing slice           |

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

L2 Domain Draft
  Owns target domain concepts:
  Employee,
  Request-owned Review,
  Request.StartReview(Employee),
  Start/Started terminology,
  no EmployeeRef,
  no ReviewDecisionRecord.

CC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.

CC-VALIDATION-001
  Owns FluentValidation boundary for request/route/body shape validation.

CC-API-001
  Owns OpenAPI/generated artifact workflow if API contract changes.
```

## 5. Sources / Source Behavior Items

Scenario Flow and Behavior Coverage must come from scenario source files, not from this slice locally.

Primary scenario sources after L2 scenario sync:

```text
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md
```

Related read scenario context:

```text
planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
```

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Important distinction:

```text
The domain draft informs domain terminology, aggregate boundaries and state-machine direction.
Scenario text/DATA/UI/behavior files remain source of truth for Scenario Flow and Behavior Coverage.
```

Target L2 domain direction for this slice:

```text
- Use Employee terminology.
- Request owns Review.
- Review is not an aggregate.
- Review starts through Request.StartReview(...).
- Domain methods receive Employee when employee behavior matters.
- Do not use EmployeeRef in the L2 target model.
- Do not use ReviewDecisionRecord in the L2 target model.
- Decision/result data lives inside Review.
```

Stable behavior item IDs for this command slice may still need final mapping in the behavior source file.

Until then, use `Source BI TBD` and do not invent final IDs inside this slice.

## 6. Visual Scenario Flow

```text
[Signed-in Employee]
opens employee request details
        ↓
System shows review state as not started
        ↓
Employee chooses “Start review”
        ↓
System verifies that the Employee can review this request
        ↓
System starts the request-owned Review for this Employee
        ↓
System shows review state as started by current Employee
        ↓
Employee can continue with later approve/reject actions
```

Scenario meaning:

```text
Start review is the explicit moment when an Employee takes the request into active review.

It is not final approval.
It is not rejection.
It is not agreement proposal creation.
```

## 7. Visual Implementation Flow

```text
[HTTP POST]
POST /api/employee/requests/{requestId}/review/start
        ↓
[CSRF boundary]
validate X-CSRF-TOKEN via CC-CSRF-001
        ↓
[Auth / Employee context]
resolve current Employee
        ↓
[Route binding / validation]
requestId is positive long
        ↓
[Command handler]
load Request aggregate by requestId
        ↓
[Visibility / reviewability]
verify request is visible/reviewable by current Employee
        ↓
[Domain]
request.StartReview(currentEmployee)
        ↓
[Persistence]
save request-owned Review started state
        ↓
[Response]
return compact StartReview result
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, CSRF attribute, response.

Validator:
  route/body shape only if needed.
  No business lifecycle validation.

Command handler:
  current Employee resolution;
  request aggregate load;
  employee visibility/reviewability check;
  transaction boundary if needed;
  SaveChanges.

Domain:
  Request owns review lifecycle.
  Request.StartReview(Employee) owns the transition.
  Review stores started state internally.

Persistence:
  persists Review started state and actor/timestamp fields.

Client:
  future rendering/action UI.
```

Current implementation compatibility note:

```text
Current runtime may still contain EmployeeRef and ReviewDecisionRecord.

For L2 target implementation, these are compatibility/background details, not target design.

Implementation should move toward:
- Employee domain object as actor;
- request-owned Review;
- Review started/decision/result state stored inside Review;
- public Request.StartReview(Employee) API.

If current runtime structures are temporarily bridged, the implementation handoff must explicitly call that compatibility out.
```

## 8. API Contract Draft

### 8.1 Endpoint

```http
POST /api/employee/requests/{requestId}/review/start
```

Route:

```text
requestId: long
```

### 8.2 Request body

No request body in first pass.

```json
{}
```

Do not accept Employee id in body.

Employee actor comes from authenticated server-side context.

### 8.3 Response

Preferred success status:

```http
200 OK
```

Response:

```ts
type StartRequestReviewResponseDto = {
  requestId: number;
  status:
    | "InReview"
    | "Approved"
    | "Rejected"
    | "AgreementExchangeFailed";

  reviewState: "StartedByCurrentEmployee";
  reviewStartedAt: string;
};
```

Notes:

```text
status is high-level request status.
reviewState is employee-facing compact review state.

If implementation separates Request.Status and Review.Status, keep both meanings explicit.
```

No extra DTOs in this slice:

```text
- no request details DTO;
- no list row DTO;
- no action affordance DTO;
- no approval DTO;
- no rejection DTO;
- no Employee profile/display-name DTO;
- no AgreementProposalExchange DTO.
```

## 9. Validation / ProblemDetails

No request body validation in first pass.

Route validation:

```text
requestId must be positive.
```

Accepted options:

```text
- route constraint `{requestId:long:min(1)}`;
- or small route DTO validator if project style requires it.
```

Validator does not own:

```text
- Employee exists;
- current user is Employee;
- request exists;
- request visibility;
- Review not started;
- Review started by another Employee;
- request already approved/rejected;
- concurrency;
- DB reads;
- transactions;
- mutations.
```

ProblemDetails direction:

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Employee / cannot access employee API

404 NotFound
  request does not exist or is not visible to Employee

422 UnprocessableEntity
  route/request shape validation or domain lifecycle rejection

400 Bad Request
  antiforgery failure only, owned by CC-CSRF-001
```

Lifecycle/domain errors should use existing API ProblemDetails/error-code pattern.

Do not model CSRF as FluentValidation.

Do not model lifecycle failures as CSRF.

## 10. Cross-Cutting Concerns / Considerations

| Concern                               | Applies? | Consideration / owner                                                                                   |
| ------------------------------------- | -------: | ------------------------------------------------------------------------------------------------------- |
| Auth/session/account context          |      yes | Must resolve current authenticated Employee. Employee auth/account may be dependency/blocker.           |
| Authorization/visibility              |      yes | Only Employee actors can start review. Request must be visible/reviewable by Employee.                  |
| Antiforgery / unsafe requests         |      yes | Unsafe POST. Must use `CC-CSRF-001` and `X-CSRF-TOKEN`.                                                 |
| Request validation / ProblemDetails   |      yes | Route id shape only. Lifecycle checks are domain/application.                                           |
| OpenAPI / generated artifacts         |      yes | New endpoint/DTO changes API contract; generated artifacts must come from repo commands.                |
| Generated constants/error codes       |    maybe | New review lifecycle errors may require constants if exposed to client.                                 |
| Transaction / atomicity               |      yes | Single request-owned Review state update. Use transaction if more than one row/table is touched.        |
| No-mutation safety                    |      yes | Failed start must not mutate request/review. Successful start must not alter details/applicant/address. |
| Idempotency / retry / double-submit   |      yes | Duplicate start behavior must be defined.                                                               |
| Concurrency / stale state             |      yes | Two Employees may attempt to start review concurrently.                                                 |
| File/document boundary                |       no | No documents in this slice.                                                                             |
| Clock/audit actor fields              |      yes | Use server UTC time and authenticated Employee id.                                                      |
| Privacy / cross-account data exposure |      yes | Do not expose client private fields beyond command response.                                            |
| Client feedback / accessibility       |   future | Future client sidecar owns button/error UX.                                                             |
| Testing responsibility split          |      yes | API integration + DB state assertions; no repository mocks as primary proof.                            |

### CSRF / antiforgery

This slice adds an unsafe browser API call.

Required:

```text
- endpoint uses CSRF protection from CC-CSRF-001;
- client mutation must go through shared CSRF-aware API boundary in future client sidecar;
- missing/invalid token returns 400 ProblemDetails with:
  code = security.antiforgery.validation.failed;
- no domain/application CSRF logic.
```

This slice should not duplicate the full CSRF matrix. Generic CSRF behavior is owned by `CC-CSRF-001`.

## 11. Questions / Decisions

### Accepted

| ID                     | Status   | Question                                   | Decision / direction                                            | Impact                                |
| ---------------------- | -------- | ------------------------------------------ | --------------------------------------------------------------- | ------------------------------------- |
| `SL-EMP-REQ-003-Q-001` | accepted | Is this approve/reject?                    | No. Start review only.                                          | Keeps final decision slices separate. |
| `SL-EMP-REQ-003-Q-002` | accepted | Should client submit Employee id?          | No. Employee actor comes from authenticated context.            | Prevents spoofing.                    |
| `SL-EMP-REQ-003-Q-003` | accepted | Is this unsafe browser request?            | Yes. POST uses `CC-CSRF-001`.                                   | Requires CSRF-aware boundary.         |
| `SL-EMP-REQ-003-Q-004` | accepted | Should L2 target use EmployeeRef?          | No. Use Employee domain object where employee behavior matters. | Aligns L2 domain direction.           |
| `SL-EMP-REQ-003-Q-005` | accepted | Should L2 target use ReviewDecisionRecord? | No. Decision/result data lives inside Review.                   | Aligns L2 domain direction.           |
| `SL-EMP-REQ-003-Q-006` | accepted | Add unit tests by default?                 | No. Use API integration + DB state assertions.                  | Matches server command test rules.    |

### Assumptions / current direction

| ID                     | Status     | Question                                          | Assumption / current direction                                                                                                        | Impact                                   |
| ---------------------- | ---------- | ------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------- |
| `SL-EMP-REQ-003-Q-007` | assumption | Does Employee auth/account exist?                 | Treat as dependency. Implementation may block if missing.                                                                             | Could require Employee auth slice first. |
| `SL-EMP-REQ-003-Q-008` | assumption | What does StartReview mutate?                     | Request-owned Review state.                                                                                                           | Aligns L2 target terminology.            |
| `SL-EMP-REQ-003-Q-009` | assumption | Should Request.Status change?                     | Prefer Review state as source for NotStarted/Started; Request.Status remains high-level lifecycle unless domain draft says otherwise. | Avoids overloading Request.Status.       |
| `SL-EMP-REQ-003-Q-010` | assumption | Same Employee starts twice?                       | Prefer idempotent no-op success if already started by same Employee.                                                                  | Reduces double-click issues.             |
| `SL-EMP-REQ-003-Q-011` | assumption | Different Employee starts already-started Review? | Reject as lifecycle/conflict error.                                                                                                   | Prevents silent reassignment.            |

### Open / implementation blockers

| ID                     | Status | Question                                                                                  | Current direction                                  | Impact                                      |
| ---------------------- | ------ | ----------------------------------------------------------------------------------------- | -------------------------------------------------- | ------------------------------------------- |
| `SL-EMP-REQ-003-Q-012` | open   | Does current schema already represent request-owned Review state and StartedByEmployeeId? | Verify before implementation.                      | May require domain/schema foundation.       |
| `SL-EMP-REQ-003-Q-013` | open   | Is row-version/concurrency token needed in first pass?                                    | Not first pass unless implementation risk is high. | Future hardening point.                     |
| `SL-EMP-REQ-003-Q-014` | open   | Exact error code for already-started-by-another-Employee?                                 | Add domain error if missing.                       | Needed for stable ProblemDetails.           |
| `SL-EMP-REQ-003-Q-015` | open   | Should current EmployeeRef/ReviewDecisionRecord code be refactored before this slice?     | Preferred yes for L2 alignment.                    | Affects implementation size and sequencing. |

## 12. Extension / Change Points

| ID                      | Area                          | Current direction                                          | Future owner                          |
| ----------------------- | ----------------------------- | ---------------------------------------------------------- | ------------------------------------- |
| `CP-EMP-REQ-REVIEW-001` | Request-owned Review state    | StartReview introduces Started state.                      | L2 domain / review command slices     |
| `CP-EMP-REQ-REVIEW-002` | Assignment semantics          | First pass records started-by Employee only.               | Future assignment/queue slice         |
| `CP-EMP-REQ-REVIEW-003` | Concurrency hardening         | State guard first; row version later if needed.            | Future concurrency hardening          |
| `CP-EMP-REQ-REVIEW-004` | Employee display name         | Not in command response.                                   | Employee profile/read slice           |
| `CP-EMP-REQ-REVIEW-005` | Audit history                 | Not first pass beyond Review fields.                       | Future audit/review history slice     |
| `CP-EMP-REQ-REVIEW-006` | Client action button          | Future `.client` sidecar.                                  | Employee request details client slice |
| `CP-EMP-REQ-REVIEW-007` | Runtime compatibility cleanup | Remove/replace EmployeeRef and ReviewDecisionRecord usage. | L2 domain alignment cleanup           |

## 13. Behavior Coverage

| Source / draft behavior                                          | Status       | Covered by this slice                                     |
| ---------------------------------------------------------------- | ------------ | --------------------------------------------------------- |
| Employee can start review for not-started request                | covered      | `POST /api/employee/requests/{requestId}/review/start`.   |
| Started Review is associated with current Employee               | covered      | Employee comes from authenticated context.                |
| Started Review becomes visible to read models                    | covered      | persisted Review state supports list/details reviewState. |
| Employee cannot start missing/not-visible request                | covered      | documented rejection.                                     |
| Client actor cannot start employee review                        | covered      | authorization/access test.                                |
| Already approved/rejected request cannot be started              | covered      | lifecycle guard.                                          |
| Already-started-by-another request cannot be silently taken over | covered      | lifecycle/conflict guard.                                 |
| Approve request                                                  | out of scope | `SL-EMP-REQ-004`.                                         |
| Reject request                                                   | out of scope | `SL-EMP-REQ-005`.                                         |
| Rejection feedback                                               | out of scope | `SL-EMP-REQ-005`.                                         |
| AgreementProposalExchange                                        | out of scope | future agreement slices.                                  |
| Employee UI button rendering                                     | out of scope | future `.client` sidecar.                                 |

Source behavior ID note:

```text
Stable scenario behavior IDs for StartReview are not yet referenced here.
If scenario/register IDs are added or found, map this table to those IDs.
```

## 14. Test / Verification Plan

Primary verification: **API integration tests with direct DB state assertions**.

Do not add unit tests by default.

Unit tests are allowed only if this slice introduces reusable helper logic with non-trivial branching.

Endpoint behavior, auth, validation, lifecycle, persistence and no-mutation safety are verified through integration tests.

### API boundary / access tests

```text
- unauthenticated start-review request returns 401;
- authenticated non-Employee/client account returns documented rejection;
- authenticated Employee can start visible request;
- missing request id returns documented not-found response;
- not-visible request id returns documented not-found/visibility response;
- invalid route id returns documented route/model-binding response if testable.
```

### Main DB state transition test

One focused state transition test:

```text
Arrange:
- request exists;
- request-owned Review is NotStarted;
- no started-by Employee;
- no started-at timestamp;
- no final decision/result.

Act:
- POST /api/employee/requests/{requestId}/review/start as Employee.

Assert:
- Review state is Started;
- startedByEmployeeId = current Employee id;
- startedAt is not null;
- final decision/result fields remain empty;
- request details/address/applicantPartyId remain unchanged;
- response returns requestId and reviewState = StartedByCurrentEmployee.
```

### Idempotency / conflict tests

Depending accepted decision:

```text
- same Employee starts same request twice:
  expected no-op success and same started-by Employee.

- different Employee starts already-started request:
  expected lifecycle/conflict rejection and original started-by Employee unchanged.
```

### No-mutation / unrelated-record safety tests

```text
- failed start does not change Review started fields;
- starting one request does not mutate another request;
- starting review does not mutate ApplicantParty;
- starting review does not mutate request details/address;
- starting review does not write approve/reject decision/result fields.
```

### Regression guards

```text
- approved request cannot be started again;
- rejected request cannot be started again;
- employee list/details read can derive StartedByCurrentEmployee after start, if cheap to assert through existing read endpoint;
- client-created request enters the expected not-started Review state, if this is not already covered by domain/read tests.
```

### Cross-cutting tests

```text
- endpoint uses CSRF-aware boundary;
- generic CSRF failure behavior is owned by CC-CSRF-001;
- this slice may include only a smoke/access test if needed.
```

### What not to test

```text
- no repository mock call-order tests;
- no exact SaveChanges count tests;
- no MediatR pipeline tests;
- no generated TypeScript as behavior proof;
- no OpenAPI generation as behavior proof;
- no client button rendering;
- no approve/reject behavior;
- no AgreementProposalExchange behavior;
- no React Query/cache behavior;
- no broad CSRF matrix duplication.
```

## 15. Implementation Checklist

```text
[ ] Verify Employee auth/account/session dependency.
[ ] Verify request-owned Review target domain model.
[ ] Align current runtime away from EmployeeRef if implementing L2 target now.
[ ] Align current runtime away from ReviewDecisionRecord if implementing L2 target now.
[ ] Add/confirm Request.StartReview(Employee).
[ ] Ensure Review stores:
    - started state;
    - startedByEmployeeId;
    - startedAt;
    - decision/result state for later approve/reject slices.
[ ] Add API endpoint:
    POST /api/employee/requests/{requestId}/review/start.
[ ] Apply employee authorization.
[ ] Apply CSRF protection.
[ ] Add command:
    StartRequestReviewCommand(requestId, currentEmployeeId).
[ ] Add command handler.
[ ] Add response DTO.
[ ] Add missing error codes if lifecycle errors do not exist.
[ ] Add API integration tests with DB assertions.
[ ] Run OpenAPI/generated artifact workflow if endpoint/DTOs are added.
[ ] Do not implement approve command.
[ ] Do not implement reject command.
[ ] Do not implement client UI.
[ ] Do not accept Employee id from body.
```

## 16. Next Step

Before implementation, verify blockers:

```text
1. Is Employee auth/account/session already implemented?
2. Is request-owned Review state already implemented?
3. Can current schema represent:
   - NotStarted;
   - Started;
   - StartedByEmployeeId;
   - StartedAt?
4. Should same-Employee duplicate start be idempotent?
5. What exact error code should be used for already-started-by-another-Employee?
6. Will implementation include L2 domain alignment away from EmployeeRef/ReviewDecisionRecord now,
   or will that be a prerequisite slice?
```

Recommended next drafts:

```text
SL-EMP-REQ-004 — Approve Request Review
SL-EMP-REQ-005 — Reject Request Review
```

