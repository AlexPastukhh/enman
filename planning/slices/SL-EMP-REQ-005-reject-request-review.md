# SL-EMP-REQ-005 — Reject Request Review

Status: implemented slice draft refactor / implementation not rechecked in this pass  
Package: `[Employee] [Requests]`  
Slice type: backend/API command slice  
Primary purpose: employee rejects a request-owned Review with feedback  
Parent slices:

* `SL-EMP-REQ-001 — Employee Request List Read`
* `SL-EMP-REQ-002 — Employee Request Details Read`
* `SL-EMP-REQ-003 — Start Request Review`
* `SL-EMP-REQ-004 — Approve Request Review`

Implementation direction: L2 target domain model — request-owned Review mutation through `ConnectionRequest.RejectReview(employee, feedback, decidedAt)` / `RequestReview.Reject(employee, feedback, decidedAt)`.

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
pending scenario-data source for employee review action outcomes and rejection feedback copy/length
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
pending / add row for SL-EMP-REQ-005 during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Domain disposition | This slice responsibility | Notes |
|---|---|---|---|---|
| Employee can reject Review they started | SC-07B pending | provided/partially provided by request-owned review domain lifecycle | expose API command, resolve current Employee, verify Review was started by current Employee, call domain method, persist result | exact domain/service shape may be implementation-specific |
| API requires non-empty rejection feedback | SC-07B pending | not domain-only | validate DTO/API boundary before domain call | domain may allow nullable feedback; this endpoint is stricter |
| Valid rejection feedback is stored | SC-07B pending | provided/partially provided by `RejectionFeedback` value object and Review lifecycle | create/validate feedback, call domain, persist state | max length comes from `RejectionFeedback` |
| Request becomes Rejected after rejection | SC-07B pending | partially provided by domain state transition | persist Request/Review state and expose through read slices after refetch | command returns no read DTO |
| Employee cannot reject not-started Review | SC-07B pending | provided by domain lifecycle | map lifecycle failure to API problem response | no write on failure |
| Employee cannot reject Review started by another Employee | SC-07B pending | provided by domain lifecycle if Review stores starter | resolve current Employee and pass actor to domain | no write on failure |
| Duplicate/already completed reject attempt is rejected | SC-07B pending | provided by domain lifecycle | return validation/domain problem and preserve state | prefer `422` / no-mutation |
| Command does not create AgreementProposalExchange | SC-07B pending | not applicable | keep agreement exchange/proposal flow out of this slice | agreement flow remains absent |
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
  - old draft mixed backend/API command scope with client sidecar implementation notes;
  - old draft lacked Source / Domain / Slice Coverage Snapshot;
  - old draft lacked Implementation Sync Status;
  - old draft used behavior coverage but not Behavior-to-Test Trace;
  - old draft suggested a per-command status enum, while newer guardrails prefer shared Result/Error mapping.

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
- employee reject-review command endpoint;
- CSRF-protected unsafe request boundary;
- current authenticated Employee actor resolution;
- request lookup by requestId;
- verify request is visible/reviewable by current Employee;
- verify request-owned Review was started;
- verify Review was started by current Employee;
- API validation for required non-empty rejection feedback;
- validation/creation of RejectionFeedback;
- request-owned Review rejection lifecycle call;
- persisted Review rejected state and Request rejected status;
- 204 No Content on success;
- API integration test plan with DB/persisted state assertions.
```

Endpoint:

```http
POST /api/employee/requests/{requestId}/review/reject
```

Request body:

```json
{
  "feedback": "Reason visible to the client/request owner where the read model exposes it."
}
```

Target transition:

```text
Review.Started
        ↓ RejectReview(Employee, RejectionFeedback)
Review.Rejected

Request.InReview
        ↓
Request.Rejected
```

This slice does **not** create `AgreementProposalExchange`.

This slice does **not** send an agreement proposal.

This slice does **not** implement agreement final refusal.

This slice does **not** return request details/list data.

This slice does **not** own UI redirect/page flow.

---

## 2. Out of Scope

| Out of scope | Owner |
|---|---|
| Employee request list / filters | `SL-EMP-REQ-001` |
| Employee request details read | `SL-EMP-REQ-002` |
| Start review command | `SL-EMP-REQ-003` |
| Approve review command | `SL-EMP-REQ-004` |
| Agreement proposal exchange | agreement exchange slices |
| Agreement final refusal / agreement lifecycle refusal | agreement lifecycle decision slices |
| Department/assignment visibility | future employee visibility/permissions slice |
| Changing domain optional rejection feedback policy | separate domain/API decision |
| Employee auth / Windows auth changes | cross-cutting auth slice |
| Rewriting employee dashboard UI | client page/UI refactor workflow |
| Client command sidecar implementation | future/legacy `.client` sidecar |
| Page redirects/navigation after reject | client/page-flow audit, not this server draft |
| Changing StartReview response contract | not this slice |

---

## 3. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns employee request list endpoint and compact review/rejected state in list rows.

SL-EMP-REQ-002
  Owns employee request details endpoint and compact review/rejected state in details payload.

SL-EMP-REQ-003
  Owns StartReview command.

SL-EMP-REQ-004
  Owns ApproveReview command.

SL-EMP-REQ-005
  Owns RejectReview command and API-required rejection feedback.

Agreement exchange / agreement proposal slices
  Own agreement proposal exchange, agreement lifecycle and final refusal.

Domain
  Owns target domain concepts:
  Employee,
  Request-owned Review,
  ConnectionRequest.RejectReview(employee, feedback, decidedAt),
  RequestReview.Reject(employee, feedback, decidedAt),
  RejectionFeedback,
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

Client reject sidecar
  Future/client owner for feature API wrapper, mutation, invalidation and visible form behavior.
```

---

## 4. Scenario Flow

```text
[Signed-in Employee]
opens employee request details
        ↓
System shows review state as started by current Employee
        ↓
Employee chooses “Reject”
        ↓
System/client asks for rejection feedback
        ↓
Employee submits feedback
        ↓
System verifies this Employee owns the active Review
        ↓
System validates feedback
        ↓
System rejects the Review and marks Request as Rejected
        ↓
Command succeeds without response body
        ↓
Client refreshes request details/list read state
        ↓
System shows request as Rejected
        ↓
No agreement proposal flow starts
```

Scenario flow table:

| Step | Actor / System layer | User-visible / system responsibility |
|---|---|---|
| S01 | Signed-in Employee | Opens request details. |
| S02 | System | Shows review was started by current Employee. |
| S03 | Employee | Chooses “Reject”. |
| S04 | System/Client | Requires rejection feedback. |
| S05 | Employee | Submits feedback. |
| S06 | System | Verifies rejection is allowed. |
| S07 | System | Validates/stores feedback. |
| S08 | System | Marks Review rejected and Request rejected. |
| S09 | System | Returns command success without body. |
| S10 | Client/System | Refreshes list/details read state. |

Scenario meaning:

```text
Reject review is the explicit final negative decision for request review.

It is not StartReview.
It is not ApproveReview.
It is not AgreementProposalExchange creation.
It is not agreement final refusal.
```

---

## 5. Implementation Flow

```text
[HTTP POST]
POST /api/employee/requests/{requestId}/review/reject
        ↓
[CSRF boundary]
validate unsafe request protection
        ↓
[Auth / Employee context]
resolve current Employee
        ↓
[Route binding / DTO validation]
requestId is positive long
feedback is required / not whitespace / max length
        ↓
[Command handler]
load Employee
load Request aggregate by requestId
        ↓
[Feedback]
create/validate RejectionFeedback
        ↓
[Visibility / reviewability]
verify request is visible/reviewable by current Employee
verify Review was started by current Employee
        ↓
[Domain]
connectionRequest.RejectReview(currentEmployee, feedback, now)
        ↓
[Persistence]
save Request.Status = Rejected
save Review.Status = Rejected
save completed actor/timestamp/feedback
        ↓
[Response]
204 No Content
```

Implementation ownership:

```text
Controller:
  HTTP boundary, auth guard, route binding, CSRF attribute, response mapping.

Validator:
  route/body shape and API-required feedback policy.
  No business lifecycle validation.

Command handler:
  current Employee resolution;
  request aggregate load;
  feedback value object creation;
  employee visibility/reviewability check;
  transaction boundary if needed;
  SaveChanges.

Domain:
  Request owns review lifecycle.
  RejectReview owns the state transition.
  Review stores completion metadata and feedback.

Persistence:
  persists Request status and Review rejected state.

Client:
  future rendering/action UI, feedback form, mutation, refetch behavior and redirects.
```

---

## 6. API Contract

### Endpoint

```http
POST /api/employee/requests/{requestId}/review/reject
```

Route:

```text
requestId: long, positive
```

### Request body

```json
{
  "feedback": "string"
}
```

Request DTO:

```csharp
public sealed record EmployeeRejectRequestReviewDto(string Feedback);
```

DTO validation:

```text
feedback required
feedback not whitespace
feedback max length = RejectionFeedback.MaxLength
```

Important decision:

```text
The stricter “feedback required” rule belongs to this API slice.

The existing domain direction allows nullable/optional RejectionFeedback.
This slice should not change domain optionality unless a separate domain decision is made.
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
- reject is a command;
- client already has requestId;
- rejection result is visible through employee list/details after refetch;
- command does not create a separate external resource;
- no reject DTO is needed.
```

### Error responses

```text
400 Bad Request
  malformed JSON / antiforgery failure if current project CSRF boundary uses 400

401 Unauthorized
  no authenticated session

403 Forbidden
  authenticated but not Employee / cannot access employee API

404 NotFound
  request does not exist or is not visible to Employee

422 UnprocessableEntity
  DTO/API validation or domain lifecycle rejection:
  - feedback is missing/blank/too long;
  - review was not started;
  - review was started by another Employee;
  - request is not InReview;
  - request already approved/rejected;
  - request cannot be rejected now.

500 InternalServerError
  unexpected server failure
```

---

## 7. Validation / ProblemDetails

Route validation:

```text
requestId must be positive.
```

Body validation:

```text
feedback required
feedback not whitespace
feedback max length = RejectionFeedback.MaxLength
```

Domain validation:

```text
RejectionFeedback.Create(feedback)
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

## 8. Domain Behavior

Domain behavior after accepted API input:

```text
- Review must exist.
- Request status must be InReview.
- Review status must be Started.
- Current Employee must be same as StartedByEmployeeId.
- Provided feedback must be valid RejectionFeedback.
- Review status becomes Rejected.
- Review stores CompletedByEmployeeId.
- Review stores CompletedAt.
- Review stores RejectionFeedback.
- Request status becomes Rejected.
```

Existing direction:

```text
ConnectionRequest.RejectReview(employee, feedback, decidedAt)
  - requires Review exists;
  - requires request Status = InReview;
  - delegates completion to RequestReview;
  - sets request Status = Rejected.

RequestReview.Reject(employee, feedback, decidedAt)
  - requires review Status = Started;
  - requires StartedByEmployeeId == employee.Id;
  - stores completed employee/time/feedback;
  - sets review Status = Rejected.
```

Implementation note:

```text
RejectReview should protect against stale inactive Employee consistently with StartReview/ApproveReview direction.

If Employee.EnsureCanReview is not currently called during reject/approve completion,
add it or centralize it in RequestReview.CanComplete during implementation sync/code refactor.
```

---

## 9. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Resolve current authenticated Employee server-side. |
| Authorization/visibility | yes | Handler owns request visibility/reviewability check. |
| Antiforgery / unsafe requests | yes | POST command must follow current CSRF/unsafe-request policy. |
| Request validation / ProblemDetails | yes | Route + feedback DTO validation. |
| OpenAPI / generated artifacts | yes | API contract changes require generated artifact workflow. |
| Transaction / atomicity | yes | Review rejection and Request rejection must persist atomically. |
| No partial write | yes | Failed validation/lifecycle checks must not change Request or Review. |
| Idempotency / double-submit | yes | Second reject attempt should not create/change another decision. Prefer lifecycle `422`. |
| Concurrency / stale state | yes | Command re-checks domain state even if details page looked rejectable. |
| File/document boundary | no | No documents in this slice. |
| Clock/audit actor fields | yes | Use server UTC time and authenticated Employee id. |
| Privacy / cross-account data exposure | yes | Do not expose client private fields in command response. |
| Client feedback / accessibility | future | Future client sidecar owns feedback form/button/error UX. |
| Redirect/page flow | future | Page-flow audit, not this server slice. |
| Testing responsibility split | yes | API integration + DB assertions; no repository mocks as primary proof. |

---

## 10. Questions / Decisions

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| `SL-EMP-REQ-005-Q001` | accepted | Should reject return DTO? | No. Return `204 No Content`. | Client refetches list/details. |
| `SL-EMP-REQ-005-Q002` | accepted | Is feedback required? | Yes at API boundary for this slice. Domain currently allows nullable `RejectionFeedback`, but this endpoint requires non-empty feedback. | DTO/API validation. |
| `SL-EMP-REQ-005-Q003` | accepted | Who can reject? | Only Employee who started the review. | Domain lifecycle rule. |
| `SL-EMP-REQ-005-Q004` | accepted | Can another Employee reject started review? | No. Return lifecycle `422`. | Prevents cross-employee completion. |
| `SL-EMP-REQ-005-Q005` | accepted | Can not-started request be rejected? | No. Review must be started. | Lifecycle `422`. |
| `SL-EMP-REQ-005-Q006` | accepted | Can already approved/rejected request be rejected? | No. | Lifecycle `422`. |
| `SL-EMP-REQ-005-Q007` | accepted | Should reject start agreement flow? | No. | Agreement flow remains absent. |
| `SL-EMP-REQ-005-Q008` | accepted | Should command be CSRF-protected? | Yes. Unsafe browser command. | Server/client tests. |
| `SL-EMP-REQ-005-Q009` | accepted | Employee identity model? | `NameIdentifier = Account.Id = Employee.Id` under Employee TPH. | Same identity rule as StartReview. |

---

## 11. Extension / Change Points

| ID | Area | Current direction | Future owner |
|---|---|---|---|
| `CP-EMP-REQ-REJECT-001` | AgreementProposalExchange | Not created here. | Agreement exchange/proposal slices |
| `CP-EMP-REQ-REJECT-002` | Agreement final refusal | Not this slice. | Agreement lifecycle decision slice |
| `CP-EMP-REQ-REJECT-003` | Client reject action UI | Not this server slice. | Future `.client` sidecar |
| `CP-EMP-REQ-REJECT-004` | Redirect/page flow | Not this server slice. | Page-flow/redirect audit |
| `CP-EMP-REQ-REJECT-005` | Optional feedback policy | API requires feedback now; domain optionality remains separate. | Future domain/API decision |
| `CP-EMP-REQ-REJECT-006` | Assignment semantics | First pass uses existing visibility/reviewability policy. | Future assignment/queue slice |
| `CP-EMP-REQ-REJECT-007` | Inactive Employee completion guard | Keep consistent with approve/start direction. | Implementation sync/code refactor if missing |

---

## 12. Behavior Coverage

| Source / draft behavior | Status | Covered by this slice |
|---|---|---|
| Employee can reject own started review | covered | `POST /api/employee/requests/{requestId}/review/reject`. |
| Employee must provide feedback | covered | API validator + `RejectionFeedback` validation. |
| Request becomes Rejected | covered | Domain sets request status and persistence saves it. |
| Review stores completion metadata | covered | Review stores completed employee/time/feedback. |
| Another Employee cannot reject | covered | lifecycle/domain rejection. |
| Not-started Review cannot be rejected | covered | lifecycle/domain rejection. |
| Already completed Review cannot be rejected again | covered | lifecycle/domain rejection. |
| Reject does not start agreement flow | covered | command only changes request/review state. |
| Command does not return details payload | covered | `204 No Content`. |
| Client reads updated state after refetch | supported | read slices remain source of truth after command. |
| Approve review | out of scope | `SL-EMP-REQ-004`. |
| Agreement final refusal | out of scope | agreement lifecycle decision slice. |
| Client reject form/UX | out of scope | future client sidecar. |
| Redirect/page flow after reject | out of scope | future page-flow/redirect audit. |

---

## 13. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and server/system outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Server/system outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| Employee rejects own started review | POST returns `204`; Request and Review become rejected | API integration + DB assertion | auth fixture, HTTP POST, DB/read assertion | Low if persisted Request/Review state is asserted | Low/Medium: helper/schema refactor may affect DB assertion code | `RejectRequestReview_RejectsStartedReviewAndReturnsNoContent` |
| Feedback is required at API boundary | missing/blank feedback returns `422`; no state change | API integration + no-mutation assertion | HTTP POST body variants, DB snapshot | Low if no-mutation state is asserted | Low | `RejectRequestReview_WhenFeedbackMissingOrBlank_ReturnsValidationProblemAndDoesNotChangeState` |
| Feedback max length is enforced | too-long feedback returns `422`; no state change | API integration | HTTP POST with too-long feedback | Medium if no DB snapshot | Low | `RejectRequestReview_WhenFeedbackTooLong_ReturnsValidationProblem` |
| Employee actor comes from auth context | completed employee id is current Employee | API integration + DB assertion | auth fixture, HTTP POST, DB read | Low if actor id is asserted | Low | same success test or focused actor test |
| Feedback is persisted | Review stores RejectionFeedback | API integration + DB assertion | HTTP POST, DB read | Low | Low/Medium | same success test |
| No response DTO | command response has no body | API integration | HTTP response assertion | Low | Low | same success test |
| Not-started review cannot be rejected | returns `422`; Request/Review unchanged | API integration + no-mutation assertion | DB precondition, HTTP POST, DB snapshot | Low if no-mutation state is asserted | Low/Medium | `RejectRequestReview_WhenReviewNotStarted_ReturnsValidationProblemAndDoesNotChangeState` |
| Review started by another Employee cannot be rejected | returns `422`; original started actor/state unchanged | API integration + no-mutation assertion | DB precondition, HTTP POST, DB snapshot | Low if actor/state unchanged is asserted | Low/Medium | `RejectRequestReview_WhenStartedByAnotherEmployee_ReturnsValidationProblemAndDoesNotChangeState` |
| Already approved/rejected request cannot be rejected again | returns `422`; completed state unchanged | API integration + no-mutation assertion | DB precondition, HTTP POST, DB snapshot | Low if completed fields unchanged are asserted | Low/Medium | `RejectRequestReview_WhenAlreadyCompleted_ReturnsValidationProblemAndDoesNotChangeState` |
| Missing/not-visible request cannot be rejected | returns `404` or visibility-safe failure; no write | API integration | auth fixture, HTTP POST, DB/read assertion | Medium if only status asserted; Low with no-write check | Low | `RejectRequestReview_WhenMissingOrNotVisible_ReturnsNotFound` |
| Unsafe command is protected | missing/invalid CSRF rejected by command family smoke | API integration smoke | POST without token | Medium if no no-mutation assertion | Low | one CSRF smoke; full matrix belongs to `CC-SEC-CSRF-001` |
| Reject does not create agreement flow | no AgreementProposalExchange is created | API integration + DB assertion | HTTP POST, DB read for exchanges | Low if DB assertion exists | Low/Medium | success test or focused no-exchange test |

### API boundary / access

```text
- unauthenticated reject-review returns 401;
- non-Employee/client account returns 403;
- authenticated Employee can reject review they started.
```

### Command success

```text
- POST reject-review returns 204 No Content;
- response body is empty;
- DB Request.Status = Rejected;
- DB RequestReview.Status = Rejected;
- DB RequestReview.CompletedByEmployeeId = current Employee id;
- DB RequestReview.CompletedAt is set;
- DB RequestReview.RejectionFeedback is stored;
- DB RequestReview.StartedByEmployeeId remains unchanged;
- DB RequestReview.StartedAt remains unchanged;
- no AgreementProposalExchange is created.
```

### Lifecycle / no-write tests

```text
- rejecting missing/not-visible request returns 404;
- rejecting request without started Review returns 422;
- rejecting Review started by another Employee returns 422;
- rejecting already Approved request returns 422;
- rejecting already Rejected request returns 422;
- failed command does not change Request.Status;
- failed command does not change existing Review.Status;
- failed command does not set/overwrite CompletedByEmployeeId / CompletedAt / RejectionFeedback.
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
- reject response DTO;
- StartReview command behavior except precondition setup;
- ApproveReview command;
- AgreementProposalExchange behavior beyond no-create guard;
- client UI;
- client feedback form;
- redirect/page flow;
- repository mock call-order as primary proof;
- unit tests unless reusable helper logic is introduced.
```

---

## 14. Backend Implementation Direction / Current Refactor Checklist

Historical implementation checklist is replaced by implemented-draft sync checklist.

```text
[ ] Confirm endpoint is present or mark implementation drift.
[ ] Confirm success is 204 No Content.
[ ] Confirm no reject response DTO exists.
[ ] Confirm Employee id is not accepted in request body.
[ ] Confirm Employee comes from authenticated context.
[ ] Confirm feedback DTO is required/not whitespace/max length.
[ ] Confirm domain optional feedback policy is not changed by this API slice.
[ ] Confirm Request aggregate is loaded by requestId.
[ ] Confirm Review was started by current Employee or mark implementation drift.
[ ] Confirm RejectionFeedback is created/validated.
[ ] Confirm RejectReview(employee, feedback, now) or equivalent domain method is used.
[ ] Confirm Request + RequestReview state is persisted.
[ ] Confirm lifecycle failures map to ProblemDetails.
[ ] Confirm no new per-command status enum is introduced in target direction; if implementation already has one, mark as future architecture cleanup.
[ ] Confirm focused API integration tests with DB state assertions exist.
[ ] Confirm OpenAPI/type generation is current if contract changed.
[ ] Confirm ApproveReview is not implemented here.
[ ] Confirm AgreementProposalExchange is not created here.
[ ] Confirm command does not return details/list row.
```

This pass did not perform implementation verification.

---

## 15. Historical Client Notes / Future Client Sidecar

The original draft included client command-sidecar implementation notes.

Current docs direction separates server and client slice drafts. Therefore this server draft keeps only the server/API command target and records the future client owner.

Future client sidecar should own:

```text
features/employee-request/reject-review/api/rejectRequestReview.ts
features/employee-request/reject-review/model/useRejectRequestReviewMutation.ts
reject action UI/feedback form
client validation
query invalidation/refetch for list/details
visible error/success feedback
page-flow/redirect behavior if any
```

Client API placement rule remains:

```text
command endpoint wrappers go to features/*/api;
do not add business wrappers to shared/api.
```

---

## 16. Next Step

Next draft-only refactor candidate:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
```

Separate later work:

```text
- client sidecar draft refactor;
- page flow / redirects audit;
- UI refactoring workflow.
```
