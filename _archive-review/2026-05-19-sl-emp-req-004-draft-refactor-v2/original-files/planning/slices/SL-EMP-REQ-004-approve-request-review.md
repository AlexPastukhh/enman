# SL-EMP-REQ-004 — Approve Request Review

Status: backend/API command slice draft
Package: `[Employee] [Requests]`
Slice type: backend/API command slice
Primary purpose: employee approves a request-owned Review
Parent slices:

* `SL-EMP-REQ-001 — Employee Request List Read`
* `SL-EMP-REQ-002 — Employee Request Details Read`
* `SL-EMP-REQ-003 — Start Request Review`

Implementation direction: L2 target domain model — request-owned Review mutation through `Request.ApproveReview(Employee)`.

---

## 1. Scope

This slice owns:

```text
- add employee approve-review command endpoint;
- resolve current authenticated Employee;
- load request by requestId;
- verify request is visible/reviewable by current Employee;
- verify request-owned Review was started;
- verify Review was started by current Employee;
- call Request.ApproveReview(currentEmployee, now);
- persist Review approved state and Request approved status;
- return 204 No Content on success;
- add API integration tests with DB state assertions.
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

---

## 2. Out of Scope

| Out of scope                            | Owner                                        |
| --------------------------------------- | -------------------------------------------- |
| Employee request list / filters         | `SL-EMP-REQ-001`                             |
| Employee request details read           | `SL-EMP-REQ-002`                             |
| Start review command                    | `SL-EMP-REQ-003`                             |
| Reject review command                   | `SL-EMP-REQ-005`                             |
| Rejection feedback                      | `SL-EMP-REQ-005`                             |
| AgreementProposalExchange creation      | future agreement proposal slice              |
| Employee sends first proposal           | future agreement proposal slice              |
| Returning details/list row from command | read slices after refetch                    |
| Employee dashboard/client UI            | future `.client` sidecar                     |
| Full assignment/queue model             | future assignment/review queue slice         |
| Client “two entry” UX                   | future client sidecar, not this server slice |

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

Agreement proposal slices
  Own agreement exchange/proposal creation after request approval.

L2 Domain Draft
  Owns target domain concepts:
  Employee,
  Request-owned Review,
  Request.ApproveReview(Employee),
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

---

## 4. Visual Scenario Flow

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

| Step | Actor / System layer | User-visible / system responsibility          |
| ---- | -------------------- | --------------------------------------------- |
| S01  | Signed-in Employee   | Opens request details.                        |
| S02  | System               | Shows review was started by current Employee. |
| S03  | Employee             | Clicks “Approve”.                             |
| S04  | System               | Verifies approval is allowed.                 |
| S05  | System               | Marks Review approved and Request approved.   |
| S06  | System               | Returns command success without body.         |
| S07  | Client/System        | Refreshes list/details read state.            |
| S08  | System               | Shows request as Approved.                    |

Scenario meaning:

```text
Approve review is the explicit final positive decision for the request review.

It is not StartReview.
It is not RejectReview.
It is not AgreementProposalExchange creation.
```

---

## 5. Visual Implementation Flow

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

Implementation flow table:

| Step | Layer                      | Responsibility                                                          |
| ---- | -------------------------- | ----------------------------------------------------------------------- |
| I01  | Route / Controller         | Exposes `POST /api/employee/requests/{requestId}/review/approve`.       |
| I02  | CSRF boundary              | Applies unsafe-request protection according to current project pattern. |
| I03  | Auth / Employee context    | Resolves current authenticated Employee.                                |
| I04  | Route binding / validation | Ensures `requestId` is a positive long.                                 |
| I05  | Command handler            | Loads request aggregate and coordinates command.                        |
| I06  | Visibility / reviewability | Checks current Employee can review this request.                        |
| I07  | Domain                     | Calls `Request.ApproveReview(Employee, now)`.                           |
| I08  | Persistence                | Saves Request and RequestReview state atomically.                       |
| I09  | API response               | Returns `204 No Content`.                                               |

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
  Request.ApproveReview(Employee) owns the transition.
  Review stores completed/approved state internally.

Persistence:
  persists Request status and Review approved state.

Client:
  future rendering/action UI and refetch behavior.
```

---

## 6. API Contract Draft

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

| Concern                               | Applies? | Consideration / owner                                                                     |
| ------------------------------------- | -------: | ----------------------------------------------------------------------------------------- |
| Auth/session/account context          |      yes | Resolve current authenticated Employee server-side.                                       |
| Authorization/visibility              |      yes | Handler owns request visibility/reviewability check.                                      |
| Antiforgery / unsafe requests         |      yes | POST command must follow current CSRF/unsafe-request policy.                              |
| Request validation / ProblemDetails   |  minimal | No body. Route id shape only.                                                             |
| OpenAPI / generated artifacts         |      yes | API contract changes; run generated artifact workflow.                                    |
| Transaction / atomicity               |      yes | Review approval and Request approval must persist atomically.                             |
| No partial write                      |      yes | Failed lifecycle checks must not change Request or Review.                                |
| Idempotency / double-submit           |      yes | Second approve attempt should not create/change another decision. Prefer lifecycle `422`. |
| Concurrency / stale state             |      yes | Command re-checks domain state even if details page looked approvable.                    |
| File/document boundary                |       no | No documents in this slice.                                                               |
| Clock/audit actor fields              |      yes | Use server UTC time and authenticated Employee id.                                        |
| Privacy / cross-account data exposure |      yes | Do not expose client private fields in command response.                                  |
| Client feedback / accessibility       |   future | Future client sidecar owns button/error UX.                                               |
| Testing responsibility split          |      yes | API integration + DB assertions; no repository mocks as primary proof.                    |

---

## 9. Questions / Decisions

### Accepted

| ID                     | Status   | Question                                      | Decision / direction                             | Impact                             |
| ---------------------- | -------- | --------------------------------------------- | ------------------------------------------------ | ---------------------------------- |
| `SL-EMP-REQ-004-Q-001` | accepted | Is this StartReview?                          | No. Review must already be started.              | Keeps command sequence explicit.   |
| `SL-EMP-REQ-004-Q-002` | accepted | Is this RejectReview?                         | No. Rejection belongs to `SL-EMP-REQ-005`.       | Keeps rejection feedback separate. |
| `SL-EMP-REQ-004-Q-003` | accepted | Should client submit Employee id?             | No. Employee actor comes from auth context.      | Prevents spoofing.                 |
| `SL-EMP-REQ-004-Q-004` | accepted | Should success return DTO?                    | No. Return `204 No Content`.                     | Read state comes from refetch.     |
| `SL-EMP-REQ-004-Q-005` | accepted | Should this create AgreementProposalExchange? | No. Approval only marks request/review approved. | Agreement flow stays separate.     |
| `SL-EMP-REQ-004-Q-006` | accepted | Add unit tests by default?                    | No. Use API integration + DB assertions.         | Matches server command test rules. |
| `SL-EMP-REQ-004-Q-007` | accepted | Does client two-entry UX belong here?         | No. Client sidecar owns UI entries.              | Server stays command-only.         |

### Assumptions / current direction

| ID                     | Status     | Question                                                       | Assumption / current direction                            | Impact                                                     |
| ---------------------- | ---------- | -------------------------------------------------------------- | --------------------------------------------------------- | ---------------------------------------------------------- |
| `SL-EMP-REQ-004-Q-008` | assumption | Does Employee auth/account exist?                              | Yes / required by previous employee slices.               | Implementation can reuse current Employee context pattern. |
| `SL-EMP-REQ-004-Q-009` | assumption | What does ApproveReview mutate?                                | Request-owned Review state and high-level Request.Status. | Aligns L2 target terminology.                              |
| `SL-EMP-REQ-004-Q-010` | assumption | Should approve be idempotent for same Employee?                | Prefer lifecycle `422` on duplicate approve.              | Simpler state machine and clearer no-write behavior.       |
| `SL-EMP-REQ-004-Q-011` | assumption | Should approval begin AgreementProposalExchange automatically? | No. Separate agreement proposal slice.                    | Prevents cross-aggregate orchestration here.               |

---

## 10. Extension / Change Points

| ID                       | Area                      | Current direction                                         | Future owner                      |
| ------------------------ | ------------------------- | --------------------------------------------------------- | --------------------------------- |
| `CP-EMP-REQ-APPROVE-001` | AgreementProposalExchange | Not created here.                                         | Future agreement proposal slice   |
| `CP-EMP-REQ-APPROVE-002` | Client approve action UI  | Not this server slice.                                    | Future `.client` sidecar          |
| `CP-EMP-REQ-APPROVE-003` | Assignment semantics      | First pass uses existing visibility/reviewability policy. | Future assignment/queue slice     |
| `CP-EMP-REQ-APPROVE-004` | Concurrency hardening     | State guard first; row version later if needed.           | Future concurrency hardening      |
| `CP-EMP-REQ-APPROVE-005` | Audit/history             | Only Review completed fields now.                         | Future audit/review-history slice |

---

## 11. Behavior Coverage

| Source / draft behavior                                    | Status       | Covered by this slice                                               |
| ---------------------------------------------------------- | ------------ | ------------------------------------------------------------------- |
| Employee can approve a review they started                 | covered      | `POST /api/employee/requests/{requestId}/review/approve`.           |
| Approved Review records current Employee                   | covered      | Employee comes from authenticated context.                          |
| Request becomes Approved                                   | covered      | Domain `Request.ApproveReview(Employee, now)`.                      |
| Approval becomes visible to read models                    | covered      | persisted Request/Review state supports list/details after refetch. |
| Employee cannot approve not-started review                 | covered      | lifecycle/domain rejection.                                         |
| Employee cannot approve review started by another Employee | covered      | lifecycle/domain rejection.                                         |
| Missing/not-visible request cannot be approved             | covered      | documented rejection.                                               |
| Command does not return details payload                    | covered      | `204 No Content`.                                                   |
| Command does not create AgreementProposalExchange          | covered      | out of scope.                                                       |
| Reject request                                             | out of scope | `SL-EMP-REQ-005`.                                                   |
| Client two-entry UX                                        | out of scope | future client sidecar.                                              |

---

## 12. Test / Verification Plan

Primary verification: API integration tests with DB state assertions.

Do not add unit tests by default for this server slice. Domain unit tests belong to domain model work.

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
- failed command does not set CompletedByEmployeeId / CompletedAt.
```

### Generated artifacts

```text
- run OpenAPI generation;
- run API type generation;
- stage generated artifacts;
- run check:api.
```

### What not to test

```text
- no request details payload in command response;
- no approval response DTO;
- no StartReview command behavior except precondition setup;
- no RejectReview command;
- no AgreementProposalExchange;
- no client UI;
- no client two-entry UX;
- no repository mock call-order as primary proof;
- no unit tests unless reusable helper logic is introduced.
```

---

## 13. Implementation Checklist

```text
[ ] Add `POST /api/employee/requests/{requestId}/review/approve`.
[ ] Use `204 No Content` on success.
[ ] Do not add approve response DTO.
[ ] Do not accept Employee id in request body.
[ ] Resolve Employee from authenticated context.
[ ] Load Request aggregate by requestId.
[ ] Check Employee visibility/reviewability.
[ ] Call Request.ApproveReview(Employee, clock.UtcNow).
[ ] Save Request + RequestReview state.
[ ] Map lifecycle failures to ProblemDetails.
[ ] Add focused API integration tests with DB state assertions.
[ ] Run OpenAPI/type generation workflow.
[ ] Do not implement reject.
[ ] Do not create AgreementProposalExchange.
[ ] Do not return details/list row from command.
```

---

## 14. Next Step

After this slice:

```text
SL-EMP-REQ-005 — Reject Request Review
```

Future client sidecar behavior after successful 204:

```text
- invalidate/refetch employee request details;
- invalidate/refetch employee request list if visible on dashboard;
- show success feedback if UI requires it.
```
