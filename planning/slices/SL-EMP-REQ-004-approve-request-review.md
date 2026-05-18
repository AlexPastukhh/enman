# SL-EMP-REQ-004 — Approve Request Review

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[Employee] [Requests]`  
Slice type: backend/API command slice  
Primary purpose: employee approves a request-owned Review  
Parent slices:

* `SL-EMP-REQ-001 — Employee Request List Read`
* `SL-EMP-REQ-002 — Employee Request Details Read`
* `SL-EMP-REQ-003 — Start Request Review`

Implementation direction: L2 target domain model — request-owned Review mutation through `Request.ApproveReview(Employee)`.

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was not rechecked in this pass.
UI/client page flow and redirects are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-07B — Employee Request Review Actions
```

UI scenario:

```text
missing / pending dedicated UI source for employee request review actions
```

Cross-cutting behavior:

```text
CC-SEC-CSRF-001 — Unsafe Command Protection
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility, for future client sidecar only
```

Data source:

```text
pending scenario-data source for employee review action outcomes
```

Behavior items:

```text
stable source behavior item IDs are pending scenario/source registry;
this draft uses provisional behavior names until source-sync files are completed.
```

Concern umbrella:

```text
none for this server slice;
CSRF is cross-cutting security concern.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-07B: pending / v000 if source registry is applied
CC-SEC-CSRF-001: v001 if source registry is applied
```

Domain baseline:

```text
DOM-v001 if source-sync/domain registry is applied;
otherwise pending domain baseline.
```

Slice derivation map:

```text
pending / add row for SL-EMP-REQ-004 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Employee can approve a Review they started | SC-07B pending | provided/partially provided by request-owned review domain lifecycle | expose API command, resolve current Employee, verify Review was started by current Employee, call domain method, persist result | exact domain method name may be implementation-specific |
| Request becomes Approved after approval | SC-07B pending | partially provided by domain state transition | persist Request/Review state and expose through read slices after refetch | command returns no read DTO |
| Employee cannot approve not-started Review | SC-07B pending | provided by domain lifecycle | map lifecycle failure to API problem response | no write on failure |
| Employee cannot approve Review started by another Employee | SC-07B pending | provided by domain lifecycle if Review stores starter | resolve current Employee and pass actor to domain | no write on failure |
| Duplicate/already approved approve attempt is rejected | SC-07B pending | provided by domain lifecycle | return validation/domain problem and preserve state | prefer `422` / no-mutation |
| Command does not create AgreementProposalExchange | SC-07B pending | not applicable | keep agreement exchange orchestration out of this slice | future agreement exchange slice |
| Unsafe command is CSRF-protected | CC-SEC-CSRF-001-v001 | not domain behavior | apply current CSRF/antiforgery boundary | full CSRF matrix belongs to cross-cutting tests |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync
```

Implemented files:

```text
server:
  not rechecked in this pass

client:
  out of scope; future/legacy client sidecar

tests:
  not rechecked in this pass
```

Checked against:

```text
source versions:
  pending source-sync registry

domain baseline:
  pending / DOM-v001 if source-sync files are applied

slice derivation map version:
  pending
```

Known drift:

```text
docs:
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old draft used behavior coverage but not Behavior-to-Test Trace;
  - old draft was written as implementation checklist even though slice may already be implemented.

source:
  - stable behavior item IDs are not yet assigned in source registry.

implementation:
  - not checked in this pass.

UI:
  - not touched in this pass.
```

Last sync note:

```text
Docs-only refactor. No runtime implementation inspection and no UI/redirect flow inspection.
```

---

## 1. Scope

This slice owns:

```text
- employee approve-review command endpoint;
- current authenticated Employee actor resolution;
- request lookup by requestId;
- verify request is visible/reviewable by current Employee;
- verify request-owned Review was started;
- verify Review was started by current Employee;
- request-owned Review approval lifecycle call;
- persisted Review approved state and Request approved status;
- 204 No Content on success;
- API integration test plan with DB/persisted state assertions.
```

Endpoint:

```http
POST /api/employee/requests/{requestId}/review/approve
```

Target transition:

```text
Review.Started
        ↓ ApproveReview(Employee)
Review.Approved

Request.InReview
        ↓
Request.Approved
```

This slice does **not** create `AgreementProposalExchange`.

This slice does **not** send an agreement proposal.

This slice does **not** return request details/list data.

This slice does **not** own UI redirect/page flow.

---

## 2. Out of Scope

| Out of scope | Owner |
|---|---|
| Employee request list / filters | `SL-EMP-REQ-001` |
| Employee request details read | `SL-EMP-REQ-002` |
| Start review command | `SL-EMP-REQ-003` |
| Reject review command | `SL-EMP-REQ-005` |
| Rejection feedback | `SL-EMP-REQ-005` |
| AgreementProposalExchange creation | agreement exchange start slice |
| Employee sends first agreement proposal | agreement exchange start slice |
| Returning details/list row from command | read slices after refetch |
| Employee dashboard/client UI | client sidecar |
| Page redirects/navigation after approve | client/page-flow audit, not this server draft |
| Full assignment/queue model | future assignment/review queue slice |
| Client “two entry” UX | future client sidecar, not this server slice |
| UI accessibility/visual feedback | client sidecar / UI refactor workflow |

---

## 3. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns employee request list endpoint and compact review state in list rows.

SL-EMP-REQ-002
  Owns employee request details endpoint and compact review state in details payload.

SL-EMP-REQ-003
  Owns StartReview command.

SL-EMP-REQ-004
  Owns ApproveReview command.

SL-EMP-REQ-005
  Owns RejectReview command and rejection feedback.

Agreement proposal / agreement exchange slices
  Own agreement exchange/proposal creation after request approval.

L2 Domain Draft
  Owns target domain concepts:
  Employee,
  Request-owned Review,
  Request.ApproveReview(Employee),
  Start/Started terminology,
  no EmployeeRef,
  no ReviewDecisionRecord,
  no Review repository.

CC-SEC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.

Validation / ProblemDetails cross-cutting rules
  Own route/body shape validation and error mapping conventions.

OpenAPI/generated artifact workflow
  Owns regeneration/checks if API contract changes.
```

---

## 4. Scenario Flow

```text
[Signed-in Employee]
opens employee request details
        ↓
System shows review state as started by current Employee
        ↓
Employee chooses “Approve”
        ↓
System verifies this Employee owns the active Review
        ↓
System approves the Review and marks Request as Approved
        ↓
Command succeeds without response body
        ↓
Client refreshes request details/list read state
        ↓
System shows request as Approved
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Signed-in Employee | Opens request details. |
| S02 | System | Shows review was started by current Employee. |
| S03 | Employee | Chooses “Approve”. |
| S04 | System | Verifies approval is allowed. |
| S05 | System | Marks Review approved and Request approved. |
| S06 | System | Returns command success without body. |
| S07 | Client/System | Refreshes list/details read state. |
| S08 | System | Shows request as Approved. |

Scenario meaning:

```text
Approve review is the explicit final positive decision for request review.

It is not StartReview.
It is not RejectReview.
It is not AgreementProposalExchange creation.
```

---

## 5. Implementation Flow

```text
[HTTP POST]
POST /api/employee/requests/{requestId}/review/approve
        ↓
[CSRF boundary]
validate unsafe request protection
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
verify Review was started by current Employee
        ↓
[Domain]
request.ApproveReview(currentEmployee, now)
        ↓
[Persistence]
save Request.Status = Approved
save Review.Status = Approved
save completed actor/timestamp
        ↓
[Response]
204 No Content
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, CSRF attribute, response mapping.

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
  Request.ApproveReview(Employee) owns the transition.
  Review stores completed/approved state internally.

Persistence:
  persists Request status and Review approved state.

Client:
  future rendering/action UI, refetch behavior and redirects.
```

---

## 6. API Contract

### Endpoint

```http
POST /api/employee/requests/{requestId}/review/approve
```

Route:

```text
requestId: long, positive
```

### Request body

No request body.

Do not accept Employee id in body.

```text
Employee actor comes from authenticated server-side context.
```

### Success response

```http
204 No Content
```

Response body:

```text
none
```

Reason:

```text
- client already has requestId;
- approval result is visible through employee list/details after refetch;
- command does not create a separate external resource;
- no approve DTO is needed.
```

### Error responses

```text
401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Employee / cannot access employee API

404 NotFound
  request does not exist or is not visible to Employee

422 UnprocessableEntity
  domain lifecycle rejection:
  - review was not started;
  - review was started by another Employee;
  - request is not InReview;
  - request already approved/rejected;
  - request cannot be approved now.

400 Bad Request
  antiforgery failure if current project CSRF boundary uses 400
```

---

## 7. Validation / ProblemDetails

No body validation.

Route validation:

```text
requestId must be positive.
```

Accepted implementation options:

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
- Review started state;
- Review ownership by Employee;
- request lifecycle;
- DB reads;
- transactions;
- mutations.
```

Lifecycle/domain errors should use existing API ProblemDetails/error-code pattern.

Do not model CSRF as FluentValidation.

Do not model lifecycle failures as CSRF.

---

## 8. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Resolve current authenticated Employee server-side. |
| Authorization/visibility | yes | Handler owns request visibility/reviewability check. |
| Antiforgery / unsafe requests | yes | POST command must follow current CSRF/unsafe-request policy. |
| Request validation / ProblemDetails | minimal | No body. Route id shape only. |
| OpenAPI / generated artifacts | yes | API contract changes require generated artifact workflow. |
| Transaction / atomicity | yes | Review approval and Request approval must persist atomically. |
| No partial write | yes | Failed lifecycle checks must not change Request or Review. |
| Idempotency / double-submit | yes | Second approve attempt should not create/change another decision. Prefer lifecycle `422`. |
| Concurrency / stale state | yes | Command re-checks domain state even if details page looked approvable. |
| File/document boundary | no | No documents in this slice. |
| Clock/audit actor fields | yes | Use server UTC time and authenticated Employee id. |
| Privacy / cross-account data exposure | yes | Do not expose client private fields in command response. |
| Client feedback / accessibility | future | Future client sidecar owns button/error UX. |
| Redirect/page flow | future | Page-flow audit, not this server slice. |
| Testing responsibility split | yes | API integration + DB assertions; no repository mocks as primary proof. |

---

## 9. Questions / Decisions

### Accepted

| ID | Status | Question | Decision / direction | Impact |
|---|---|---|---|---|
| `SL-EMP-REQ-004-Q-001` | accepted | Is this StartReview? | No. Review must already be started. | Keeps command sequence explicit. |
| `SL-EMP-REQ-004-Q-002` | accepted | Is this RejectReview? | No. Rejection belongs to `SL-EMP-REQ-005`. | Keeps rejection feedback separate. |
| `SL-EMP-REQ-004-Q-003` | accepted | Should client submit Employee id? | No. Employee actor comes from auth context. | Prevents spoofing. |
| `SL-EMP-REQ-004-Q-004` | accepted | Should success return DTO? | No. Return `204 No Content`. | Read state comes from refetch. |
| `SL-EMP-REQ-004-Q-005` | accepted | Should this create AgreementProposalExchange? | No. Approval only marks request/review approved. | Agreement flow stays separate. |
| `SL-EMP-REQ-004-Q-006` | accepted | Add unit tests by default? | No. Use API integration + DB assertions. | Matches server command test rules. |
| `SL-EMP-REQ-004-Q-007` | accepted | Does client two-entry UX belong here? | No. Client sidecar owns UI entries. | Server stays command-only. |

### Assumptions / current direction

| ID | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `SL-EMP-REQ-004-Q-008` | assumption | Does Employee auth/account exist? | Yes / required by previous employee slices. | Implementation can reuse current Employee context pattern. |
| `SL-EMP-REQ-004-Q-009` | assumption | What does ApproveReview mutate? | Request-owned Review state and high-level Request.Status. | Aligns L2 target terminology. |
| `SL-EMP-REQ-004-Q-010` | assumption | Should approve be idempotent for same Employee? | Prefer lifecycle `422` on duplicate approve. | Simpler state machine and clearer no-write behavior. |
| `SL-EMP-REQ-004-Q-011` | assumption | Should approval begin AgreementProposalExchange automatically? | No. Separate agreement proposal slice. | Prevents cross-aggregate orchestration here. |

---

## 10. Extension / Change Points

| ID | Area | Current direction | Future owner |
|---|---|---|---|
| `CP-EMP-REQ-APPROVE-001` | AgreementProposalExchange | Not created here. | Future agreement proposal slice |
| `CP-EMP-REQ-APPROVE-002` | Client approve action UI | Not this server slice. | Future `.client` sidecar |
| `CP-EMP-REQ-APPROVE-003` | Redirect/page flow | Not this server slice. | Page-flow/redirect audit |
| `CP-EMP-REQ-APPROVE-004` | Assignment semantics | First pass uses existing visibility/reviewability policy. | Future assignment/queue slice |
| `CP-EMP-REQ-APPROVE-005` | Concurrency hardening | State guard first; row version later if needed. | Future concurrency hardening |
| `CP-EMP-REQ-APPROVE-006` | Audit/history | Only Review completed fields now. | Future audit/review-history slice |

---

## 11. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Employee can approve a review they started | covered | `POST /api/employee/requests/{requestId}/review/approve`. |
| Approved Review records current Employee | covered | Employee comes from authenticated context. |
| Request becomes Approved | covered | Domain `Request.ApproveReview(Employee, now)` or equivalent. |
| Approval becomes visible to read models | covered | persisted Request/Review state supports list/details after refetch. |
| Employee cannot approve not-started review | covered | lifecycle/domain rejection. |
| Employee cannot approve review started by another Employee | covered | lifecycle/domain rejection. |
| Missing/not-visible request cannot be approved | covered | documented rejection. |
| Duplicate/already-approved approve does not mutate state | covered | lifecycle/domain rejection + no-mutation expectation. |
| Command does not return details payload | covered | `204 No Content`. |
| Command does not create AgreementProposalExchange | covered | out of scope. |
| Reject request | out of scope | `SL-EMP-REQ-005`. |
| Client two-entry UX | out of scope | future client sidecar. |
| Redirect/page flow after approve | out of scope | future page-flow/redirect audit. |

---

## 12. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Employee approves review they started | POST returns `204`; Request and Review become approved | API integration + DB assertion | auth fixture, HTTP POST, DB/read assertion | Low if persisted Request/Review state is asserted | Low/Medium: helper/schema refactor may affect DB assertion code | `ApproveRequestReview_ApprovesStartedReviewAndReturnsNoContent` |
| Employee actor comes from auth context | approved/completed employee id is current Employee | API integration + DB assertion | auth fixture, HTTP POST, DB read | Low if actor id is asserted | Low | same success test or focused actor test |
| No response DTO | command response has no body | API integration | HTTP response assertion | Low | Low | same success test |
| Not-started review cannot be approved | returns `422`; Request/Review unchanged | API integration + no-mutation assertion | DB precondition, HTTP POST, DB snapshot | Low if no-mutation state is asserted | Low/Medium | `ApproveRequestReview_WhenReviewNotStarted_ReturnsValidationProblemAndDoesNotChangeState` |
| Review started by another Employee cannot be approved | returns `422`; original started actor/state unchanged | API integration + no-mutation assertion | DB precondition, HTTP POST, DB snapshot | Low if actor/state unchanged is asserted | Low/Medium | `ApproveRequestReview_WhenStartedByAnotherEmployee_ReturnsValidationProblemAndDoesNotChangeState` |
| Already approved/rejected request cannot be approved again | returns `422`; completed state unchanged | API integration + no-mutation assertion | DB precondition, HTTP POST, DB snapshot | Low if completed fields unchanged are asserted | Low/Medium | `ApproveRequestReview_WhenAlreadyApprovedOrRejected_ReturnsValidationProblemAndDoesNotChangeState` |
| Missing/not-visible request cannot be approved | returns `404` or visibility-safe failure; no write | API integration | auth fixture, HTTP POST, DB/read assertion | Medium if only status asserted; Low with no-write check | Low | `ApproveRequestReview_WhenMissingOrNotVisible_ReturnsNotFound` |
| Unsafe command is protected | missing/invalid CSRF rejected by command family smoke | API integration smoke | POST without token | Medium if no no-mutation assertion | Low | one CSRF smoke; full matrix belongs to `CC-SEC-CSRF-001` |

### API boundary / access

```text
- unauthenticated approve-review returns 401;
- non-Employee/client account returns 403;
- authenticated Employee can approve review they started.
```

### Command success

```text
- POST approve-review returns 204 No Content;
- response body is empty;
- DB Request.Status = Approved;
- DB RequestReview.Status = Approved;
- DB RequestReview.CompletedByEmployeeId = current Employee id;
- DB RequestReview.CompletedAt is set;
- DB RequestReview.RejectionReason remains null;
- DB RequestReview.StartedByEmployeeId remains unchanged;
- DB RequestReview.StartedAt remains unchanged.
```

### Lifecycle / no-write tests

```text
- approving missing/not-visible request returns 404;
- approving request without started Review returns 422;
- approving Review started by another Employee returns 422;
- approving already Approved request returns 422;
- approving Rejected request returns 422;
- failed command does not change Request.Status;
- failed command does not change existing Review.Status;
- failed command does not set/overwrite CompletedByEmployeeId / CompletedAt.
```

### Generated artifacts

```text
- run OpenAPI generation if API contract changed;
- run API type generation if OpenAPI changed;
- stage generated artifacts;
- run check:api.
```

### What not to test

```text
- request details payload in command response;
- approval response DTO;
- StartReview command behavior except precondition setup;
- RejectReview command;
- AgreementProposalExchange;
- client UI;
- client two-entry UX;
- redirect/page flow;
- repository mock call-order as primary proof;
- unit tests unless reusable helper logic is introduced.
```

---

## 13. Implementation Checklist / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[ ] Confirm endpoint is present or mark implementation drift.
[ ] Confirm success is 204 No Content.
[ ] Confirm no approve response DTO exists.
[ ] Confirm Employee id is not accepted in request body.
[ ] Confirm Employee comes from authenticated context.
[ ] Confirm Request aggregate is loaded by requestId.
[ ] Confirm Review was started by current Employee or mark implementation drift.
[ ] Confirm Request.ApproveReview(Employee, now) or equivalent domain method is used.
[ ] Confirm Request + RequestReview state is persisted.
[ ] Confirm lifecycle failures map to ProblemDetails.
[ ] Confirm focused API integration tests with DB state assertions exist.
[ ] Confirm OpenAPI/type generation is current if contract changed.
[ ] Confirm RejectReview is not implemented here.
[ ] Confirm AgreementProposalExchange is not created here.
[ ] Confirm command does not return details/list row.
```

This pass did not perform implementation verification.

---

## 14. Next Step

Next draft-only refactor candidate:

```text
SL-EMP-REQ-005 — Reject Request Review
```

Separate later work:

```text
- client sidecar draft refactor;
- page flow / redirects audit;
- UI refactoring workflow.
```
