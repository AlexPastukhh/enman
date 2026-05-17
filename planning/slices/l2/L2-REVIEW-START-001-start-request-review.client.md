# L2-REVIEW-START-001.client — Start Request Review

Status: full client command sidecar draft / blocked until `SL-EMP-REQ-003` server endpoint and generated OpenAPI contract exist  
Parent server slice: `SL-EMP-REQ-003 — Start Request Review`  
Scenario: `SC-07B — Employee Request Review`  
Slice type: L2 client command sidecar  
Architecture direction: command/user-action slice maps to `pages + features + entities`; details read UI stays in `entities`; StartReview button/mutation lives in `features`.

## 1. Sidecar Overview

This sidecar owns the client-side **Start Review** user action for Employee request details.

The action is rendered from the Employee request details page only when the details read model says that Start Review is available.

Target command endpoint from the parent server slice:

```http
POST /api/employee/requests/{requestId}/review/start
```

This sidecar is not a standalone page.

It is composed into:

```text
L2-EMP-DETAILS-001.client — Employee Request Details
```

through the details page review action slot.

Expected user outcome:

```text
Employee opens request details
        ↓
Employee sees Start Review available
        ↓
Employee clicks Start Review
        ↓
command is submitted with current requestId only
        ↓
on success, details/list read state refreshes
        ↓
details show reviewState = StartedByCurrentEmployee
```

Important separation:

```text
- StartReview response is a compact command result.
- StartReview response is not the Employee request details DTO.
- Details read DTO remains owned by SL-EMP-REQ-002.
```

## 2. Scope

This client sidecar owns:

```text
- Start Review user action on Employee request details page;
- rendering Start Review action only when details read model says it is available;
- disabled/blocked state when Start Review is not available;
- submit command:
  POST /api/employee/requests/{requestId}/review/start;
- pending state while StartReview command is in flight;
- success feedback / details refresh after command success;
- list/dashboard refresh after command success if dashboard cache exists;
- visible error feedback when command is rejected;
- no blind retry after CSRF/session/security failure;
- no approve/reject behavior.
```

This is the explicit moment when an Employee starts active review.

It is not:

```text
- final approval;
- rejection;
- agreement proposal creation;
- employee assignment/queue claiming beyond review start.
```

## 3. Out of Scope

| Out of scope | Owner |
|---|---|
| Backend endpoint implementation | `SL-EMP-REQ-003 — Start Request Review` |
| Employee request details read endpoint | `SL-EMP-REQ-002 — Employee Request Details Read` |
| Employee request dashboard/list read endpoint | `SL-EMP-REQ-001 — Employee Request List Read` |
| Employee request details page read UI | `L2-EMP-DETAILS-001.client` |
| Approve review command | future `SL-EMP-REQ-004.client` / `L2-REVIEW-APPROVE-001.client` |
| Reject review command / rejection feedback | future `SL-EMP-REQ-005.client` / `L2-REVIEW-REJECT-001.client` |
| AgreementProposalExchange creation | future agreement slices |
| Employee sends first proposal | future agreement slices |
| Employee assignment/queue model | future slice |
| Local CSRF mechanics | `CC-CSRF-001` shared infrastructure |
| Manual generated OpenAPI/type edits | OpenAPI/generated artifact workflow |

## 4. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns Employee request list/dashboard read endpoint and compact review state in list rows.

L2-EMP-DASH-001.client
  Owns Employee request dashboard client read UI.

SL-EMP-REQ-002
  Owns Employee request details read endpoint and compact review state/action availability in details payload.

L2-EMP-DETAILS-001.client
  Owns Employee request details read page and optional review action slot.

SL-EMP-REQ-003
  Owns StartReview backend command.

L2-REVIEW-START-001.client
  Owns Start Review button/action/mutation.

SL-EMP-REQ-004 / L2-REVIEW-APPROVE-001.client
  Own future Approve Review command/action.

SL-EMP-REQ-005 / L2-REVIEW-REJECT-001.client
  Own future Reject Review command/action and rejection feedback UI.

CC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.
```

`SC-07A` defines when actions are available or blocked on details.

`SC-07B` owns start/approve/reject review behavior.

## 5. Sources / Source Behavior Items

Scenario Flow and Behavior Coverage must come from scenario source files, not from this sidecar locally.

Primary scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md
```

Related read/display context:

```text
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
planning/diagrams/scenario-data/SC-07A-employee-request-details-data.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
```

Parent server slice:

```text
planning/slices/SL-EMP-REQ-003-start-request-review.md
```

Domain-design input only:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Important rule:

```text
The domain draft informs domain terminology, aggregate boundaries and state-machine direction.
Scenario text/DATA/UI/behavior files remain source of truth for Scenario Flow and Behavior Coverage.
```

Behavior ID status:

```text
Stable source behavior IDs for StartReview are pending final mapping.
Use Source BI TBD until the scenario behavior source/register provides final IDs.
Do not invent final behavior item IDs inside this client draft.
```

## 6. Visual UI / Scenario Flow

```text
[Signed-in Employee]
opens Employee request details
        ↓
[Details Page]
shows request is InReview
and review is not started
        ↓
[Review Action Area]
Start Review action is available
        ↓
Employee clicks “Start review”
        ↓
[Page]
shows pending action state
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ command accepted             │ command rejected             │
 ▼                              ▼
Review state becomes            Error feedback is visible
StartedByCurrentEmployee        Existing visible state remains unchanged
        ↓
Details/dashboard can show
review started by current Employee
        ↓
Approve/Reject may become
available in future sidecars
```

In ordinary words:

An Employee opens request details. If the request is reviewable and no review has started, the details read model says Start Review is available. The user clicks Start Review. The feature sends one unsafe command to the server. On success, the details read state refreshes and shows review started by the current Employee. On failure, the page shows feedback and does not pretend the review has started.

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Employee | Opens request details. |
| S02 | Details read state | Shows request and review state. |
| S03 | Action availability | Shows Start Review action only when available. |
| S04 | User action | Employee clicks Start Review. |
| S05 | Pending state | Button/action shows command in progress. |
| S06 | Success outcome | Details show review started by current Employee. |
| S07 | Rejected outcome | Error feedback visible; previous state remains unchanged. |
| S08 | Future handoff | Approve/Reject may become available through future sidecars. |

## 7. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/employee/requests/details/EmployeeRequestDetailsPage.tsx

Lives here:
  EmployeeRequestDetailsPage

Uses:
  useEmployeeRequestDetailsQuery(requestId)
  StartReviewButton through review action slot

Owns:
  page composition
  details loading/error/success branches
  passing details/action availability to entity details view
  placing feature-owned StartReview action in action slot

Does not own:
  StartReview mutation implementation
  low-level HTTP
  CSRF token mechanics
  approve/reject behavior
```

```text
        ↓

[Entity Details UI Layer]
entities/employee-request/ui/EmployeeRequestDetailsView.tsx
entities/employee-request/ui/EmployeeReviewActionAvailabilityPanel.tsx

Lives here:
  EmployeeRequestDetailsView
  EmployeeReviewActionAvailabilityPanel

Props:
  details: EmployeeRequestDetails
  renderReviewActions?(details): ReactNode

Owns:
  read-only details layout
  review state display
  action availability display
  optional action slot placement

Does not own:
  StartReview button logic
  StartReview mutation
  pending/error command state
```

```text
        ↓

[Command Feature UI Layer]
features/employee-request/start-review/ui/StartReviewButton.tsx
features/employee-request/start-review/ui/startReviewButtonConst.ts
features/employee-request/start-review/ui/startReviewButton.css

Lives here:
  StartReviewButton

Props:
  requestId: number
  disabled?: boolean
  unavailableReason?: string | null

Uses:
  useStartRequestReviewMutation()

Owns:
  visible Start Review button
  click handler
  disabled/unavailable state
  pending state
  command error feedback
  accessible label/copy

Does not own:
  details layout
  request details fetching
  approve/reject actions
```

```text
        ↓

[Command Feature Model/API Layer]
features/employee-request/start-review/model/useStartRequestReviewMutation.ts
features/employee-request/start-review/api/startRequestReview.ts

Lives here:
  useStartRequestReviewMutation()
  startRequestReview(requestId)

Uses:
  useMutation()
  shared API wrapper
  employeeRequestQueryKeys.details(requestId)
  employeeRequestQueryKeys.dashboard(...)
  queryClient.invalidateQueries(...)

Owns:
  command mutation
  command success handling
  details/list refresh convention
  ProblemDetails-to-visible-error mapping if needed

Does not own:
  low-level fetchJson
  CSRF implementation internals
  entity details display
```

```text
        ↓

[Shared API Layer]
shared/api/employeeRequestApi.ts
shared/api/apiPaths.ts or package-specific path file

Lives here after server/OpenAPI exists:
  startRequestReview(requestId): Promise<StartRequestReviewResponse>

Uses:
  fetchJson()
  generated OpenAPI types
  shared CSRF-aware unsafe request behavior

Owns:
  low-level HTTP call:
    POST /api/employee/requests/{requestId}/review/start
  generated response type alias

Does not own:
  React Query mutation
  visible button feedback
  page routing
```

```text
        ↓

[Generated Contract Layer]
shared/api/generated/openapi-types.ts

Lives here after server implementation/generation:
  StartReview operation
  StartRequestReviewResponseDto
  ProblemDetails branches

Owns:
  generated structural API contract

Does not own:
  handwritten client logic
  manual edits
  UI decisions
```

In ordinary words:

The details page composes the read details view and passes a feature-owned action into an optional review action slot. The entity details UI remains display-only. The StartReview feature owns the button, mutation, pending state, error feedback and cache refresh. The shared API wrapper owns the POST call after the server contract and generated OpenAPI types exist.

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Page | Employee details page renders details read state. |
| I02 | Entity details UI | Details view exposes review action slot. |
| I03 | Feature UI | StartReviewButton renders when action availability allows it. |
| I04 | Feature model | Mutation submits `requestId`. |
| I05 | Shared API | Wrapper posts to StartReview endpoint. |
| I06 | Feature model | Success refreshes employee details/list read state. |
| I07 | Entity details UI | Details state shows `StartedByCurrentEmployee`. |
| I08 | Feature UI | Rejected command shows visible feedback. |

## 8. Client API / Server Contract

Server endpoint from `SL-EMP-REQ-003`:

```http
POST /api/employee/requests/{requestId}/review/start
```

Route:

```text
requestId: long / number
```

Request body:

```text
none
```

Do not submit Employee id.

Employee actor comes from authenticated server-side context.

Command response from server draft:

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

Target shared API wrapper after generated contract exists:

```ts
export type StartRequestReviewResponse =
  components["schemas"]["StartRequestReviewResponseDto"];

export const startRequestReview = (
  requestId: number,
): Promise<StartRequestReviewResponse> =>
  fetchJson<StartRequestReviewResponse>(
    employeeRequestApiPaths.startReview(requestId),
    { method: "POST" },
  );
```

Important generated contract rule:

```text
Do not handwrite StartRequestReviewResponseDto once generated types exist.
Use generated OpenAPI type aliases.

The wrapper example is conceptual until exact generated operation/type names exist.
```

Client rules:

```text
- requestId comes from Employee request details route/data.
- Employee id is never sent by client.
- Command is unsafe POST and uses shared CSRF-aware request infrastructure.
- On success, refresh Employee request details.
- If dashboard/list cache exists, refresh it too.
- Do not use StartReview response as full details DTO.
```

## 9. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Requires authenticated Employee context; client account sessions must not execute Employee review commands. |
| Authorization/visibility | yes | Server owns visibility/reviewability checks; client only hides/disables action from details read model. |
| Antiforgery / unsafe requests | yes | Unsafe POST; feature must consume shared CSRF-aware request helper from `CC-CSRF-001`. |
| Request validation / ProblemDetails | yes | Route id comes from details route; lifecycle rejection is server/domain responsibility. |
| OpenAPI / generated artifacts | yes | Use generated OpenAPI types after `SL-EMP-REQ-003` server implementation/generation. |
| Generated constants/error codes | maybe | Lifecycle/security error codes may be exposed to client through generated constants. |
| Transaction / atomicity | server | Server command owns persisted transition; client only reacts to success/failure. |
| No-mutation / existing data safety | yes | Rejected command must not cause UI to pretend review started. |
| Idempotency / double-submit / retry | yes | Pending state disables button; backend may return idempotent success for same Employee; no blind replay after CSRF refresh. |
| Concurrency / stale state | yes | Another Employee may start review between read and click; server rejects and client shows feedback/refreshes details. |
| File/document boundary | no | No documents in this sidecar. |
| Clock/audit actor fields | server | Server sets reviewStartedAt and Employee actor; client only displays returned/refresh state. |
| Privacy / cross-account data exposure | yes | Do not expose Employee/private server details in client feedback. |
| Client feedback / accessibility | yes | Pending/error/disabled states must be visible; disabled/unavailable reason should be accessible if available. |
| Testing responsibility split | yes | Component/shared API/E2E visible outcome tests; no backend transaction assertions in client tests. |

### CSRF / antiforgery

This sidecar triggers an unsafe browser API call.

Required:

```text
- mutation uses shared CSRF-aware request helper;
- missing/invalid token is handled by CC-CSRF-001;
- client may refresh token but must not blindly replay unsafe command;
- StartReviewButton shows recoverable error/session feedback.
```

Do not duplicate the full CSRF test matrix in this sidecar.

Generic CSRF behavior is owned by `CC-CSRF-001`.

## 10. Questions / Decisions

### Blocked / unresolved

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-001` | blocked | Is backend endpoint and generated OpenAPI contract available? | Runtime implementation waits for `SL-EMP-REQ-003` backend + generated types. | API wrapper/tests. |
| `Q-L2-REVIEW-START-CLIENT-002` | blocked | What are exact generated operation/type names? | Use OpenAPI after server implementation/generation. | Shared API wrapper. |

### Assumptions / current direction

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-003` | assumption | Same Employee double-clicks Start Review? | Button pending disables double-submit; backend may return idempotent success. | UX/error handling. |
| `Q-L2-REVIEW-START-CLIENT-004` | assumption | Another Employee started review between read and click? | Server rejects; client shows lifecycle/conflict feedback and refreshes details. | Stale state handling. |

### Accepted directions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-005` | accepted | Is this approve/reject? | No. Start review only. | Keeps decision sidecars separate. |
| `Q-L2-REVIEW-START-CLIENT-006` | accepted | Does client submit Employee id? | No. Server resolves Employee from session/auth. | Prevents spoofing. |
| `Q-L2-REVIEW-START-CLIENT-007` | accepted | Is this unsafe request? | Yes. POST uses shared CSRF-aware boundary. | Cross-cutting requirement. |
| `Q-L2-REVIEW-START-CLIENT-008` | accepted | Where does button live? | `features/employee-request/start-review/ui`. | Command feature placement. |
| `Q-L2-REVIEW-START-CLIENT-009` | accepted | Where does action appear? | In Employee details page action slot when details read model says available. | Page/entity/feature composition. |
| `Q-L2-REVIEW-START-CLIENT-010` | accepted | Should dashboard show StartReview directly? | Not first pass. Start action belongs to details action area. | Scope boundary. |

## 11. Extension / Change Points

```text
- Approve Review action -> SL-EMP-REQ-004.client / L2-REVIEW-APPROVE-001.client;
- Reject Review action / feedback form -> SL-EMP-REQ-005.client / L2-REVIEW-REJECT-001.client;
- Dashboard row action -> future dashboard action refinement, not first pass;
- review history/audit display -> future details refinement;
- assignment/queue/lock model -> future employee queue slice;
- richer lifecycle error codes -> generated constants if exposed to client;
- optimistic UI -> future UX hardening only after command semantics are stable.
```

## 12. Behavior Coverage

Stable source behavior IDs for StartReview are pending final mapping. Use `Source BI TBD` until behavior source file/register provides final IDs.

Do not invent final behavior IDs in this client draft.

`SC-07B` defines start review behavior: Employee starts review for an InReview request with no active started review, stores started Employee/timestamp, and blocks invalid flows.

| Source / draft behavior | How client sidecar covers it | Status |
|---|---|---|
| `Source BI TBD` — Employee can start review for not-started request | StartReviewButton sends StartReview command when details read model allows it. | covered |
| `Source BI TBD` — Started review is associated with current Employee | Client sends no Employee id; server uses auth context. | covered by contract boundary |
| `Source BI TBD` — Started review becomes visible to read models | Success refreshes details/list read state. | covered |
| `Source BI TBD` — Missing/not-visible request is rejected | Client shows error/access/not-found feedback from server response. | covered at UI boundary |
| `Source BI TBD` — Client actor cannot start employee review | Client route/session branch blocks; server owns enforcement. | covered at UI boundary |
| `Source BI TBD` — Already approved/rejected request cannot be started | Details read model disables action; server rejection shown if stale. | covered |
| `Source BI TBD` — Already-started-by-another cannot be silently taken over | Details read model disables action; server rejection shown if stale. | covered |
| Approve request | Not covered; future approve sidecar. | out of scope |
| Reject request | Not covered; future reject sidecar. | out of scope |
| AgreementProposalExchange | Not covered. | out of scope |

Not behavior coverage:

```text
- generated OpenAPI type exists;
- endpoint string exists;
- query invalidation call exists;
- repository/handler was called;
- CSRF helper internals were called.
```

## 13. Client / Component / E2E Verification Plan

E2E is implementation-time coverage only after Employee auth/session, details read, and StartReview backend/generated contract are available.

Until then, keep E2E as planned verification, not an immediately runnable requirement.

The server draft itself lists Employee auth/account/session as a dependency/blocker.

### Component/client tests

| Test / check | Verifies |
|---|---|
| StartReviewButton renders enabled when action is available | Available action visible |
| StartReviewButton renders disabled/hidden when action unavailable | Blocked state respected |
| Disabled button shows/unavailable reason when provided | User understands blocked state |
| Clicking button calls mutation with requestId | Correct command target |
| Pending state disables button | Double-submit UX guard |
| Success state triggers details refresh callback/invalidation | Read state can update |
| Error state renders visible feedback | Rejected command feedback |
| Button does not render approve/reject controls | Scope boundary |

### Shared API / feature model tests

| Test / check | Verifies |
|---|---|
| `startRequestReview(requestId)` posts to `/api/employee/requests/{requestId}/review/start` | Correct endpoint usage |
| Wrapper sends no request body | Contract compliance |
| Wrapper uses generated response type | No handwritten DTO drift |
| 401/403/404/422 ProblemDetails surface to mutation caller | Feedback can render |
| Mutation invalidates/refetches details query after success | Details refresh |
| Mutation invalidates/refetches dashboard/list query if present | Dashboard refresh |

### E2E happy path

```text
setup Employee session
        ↓
setup employee-accessible InReview request with no started review
        ↓
open Employee request details
        ↓
assert Start Review action visible
        ↓
click Start Review
        ↓
assert pending/success outcome visible
        ↓
assert details show StartedByCurrentEmployee
```

### E2E stale/blocked path

```text
setup Employee session
        ↓
setup request started by another Employee
        ↓
open Employee request details
        ↓
assert Start Review action unavailable/blocked
        ↓
assert started-by-another marker visible
```

### E2E access/error path

```text
open Employee details without Employee session
        ↓
assert auth/access state visible

or

trigger server rejection for stale lifecycle state
        ↓
assert command error feedback visible
        ↓
assert details state refreshes or remains safe
```

### Explicit non-goals for tests

```text
Do not test approve/reject behavior here.
Do not test backend transaction internals in client tests.
Do not assert React Query cache internals in E2E.
Do not duplicate full CSRF matrix here.
Do not test agreement proposal behavior here.
Do not test Employee assignment/queue behavior here.
```

## 14. Suggested File Placement

```text
src/features/employee-request/start-review/ui/
  StartReviewButton.tsx
  startReviewButtonConst.ts
  startReviewButton.css

src/features/employee-request/start-review/model/
  useStartRequestReviewMutation.ts

src/features/employee-request/start-review/api/
  startRequestReview.ts

src/shared/api/
  employeeRequestApi.ts
  employee request path file chosen by server/client convention

src/pages/employee/requests/details/
  EmployeeRequestDetailsPage.tsx

src/entities/employee-request/ui/
  EmployeeRequestDetailsView.tsx
  EmployeeReviewActionAvailabilityPanel.tsx

tests/e2e/employee/
  start-request-review.spec.ts
```

## 15. Implementation Checklist

```text
[ ] Confirm generated StartReview operation/type names.
[ ] Add shared API wrapper startRequestReview().
[ ] Add feature API adapter.
[ ] Add useStartRequestReviewMutation().
[ ] Add StartReviewButton.
[ ] Wire StartReviewButton into Employee details action slot.
[ ] Ensure Employee id is never sent by client.
[ ] Use shared CSRF-aware unsafe request helper.
[ ] Refresh details query after success.
[ ] Refresh dashboard/list query after success if present.
[ ] Show pending/error feedback.
[ ] Do not render approve/reject controls here.
[ ] Add component tests.
[ ] Add shared API/feature tests.
[ ] Add E2E happy path.
[ ] Add E2E blocked/stale path.
```

## 16. Next Step

Runtime implementation waits for server endpoint + generated OpenAPI contract.

Recommended order:

```text
1. Implement/apply SL-EMP-REQ-003 backend endpoint.
2. Run OpenAPI/type generation.
3. Update this client draft with exact generated operation/type names.
4. Implement shared API wrapper.
5. Implement start-review feature button/mutation.
6. Wire action through Employee details page action slot.
7. Add component/shared API tests.
8. Add E2E happy + blocked paths.
```

Recommended next sidecar drafts:

```text
L2-REVIEW-APPROVE-001.client — Approve Request Review
L2-REVIEW-REJECT-001.client — Reject Request Review
```
