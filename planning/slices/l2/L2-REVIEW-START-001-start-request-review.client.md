# L2-REVIEW-START-001.client — Start Request Review

Status: full client command sidecar draft / server command contract exists or must be confirmed / API placement synchronized  
Parent server slice: `SL-EMP-REQ-003 — Start Request Review`  
Scenario source: `SC-07B — Employee Request Review`  
Related read surfaces: `L2-EMP-DASH-001.client`, `L2-EMP-DETAILS-001.client`  
Slice type: L2 client command sidecar  
Architecture direction: command/user-action slice maps to `pages + features + entities`; read surfaces stay in `pages/entities`, StartReview action/mutation lives in `features`.

Current accepted API placement rule:

```text
read endpoint wrappers -> entities/*/api
command endpoint wrappers -> features/*/api
shared/api -> fetchJson / ProblemDetails / CSRF helpers / generated OpenAPI types only
```

Business-specific wrappers in `shared/api` are transitional compatibility and must not be copied into new L2 client work.

## 0. Key Decision

Start Review has **two UI entry points** but **one client command sidecar**:

```text
Entry point 1:
  Employee request dashboard/list row

Entry point 2:
  Employee request details action area

Shared command feature:
  features/employee-request/start-review/*
```

This is not two client slices because the user intent and server endpoint are the same.

Server command success contract:

```http
204 No Content
```

There is no `StartRequestReviewResponseDto` read source. After success, the client refreshes Employee request list/details read queries.

## 1. Scope

This client sidecar owns:

```text
- Start Review user action for Employee request surfaces;
- one shared Start Review feature reused by multiple placements;
- Start Review action hosted from Employee request dashboard/list row;
- Start Review action hosted from Employee request details action area;
- rendering Start Review only when the relevant read model says it is available;
- disabled/blocked Start Review state when action is unavailable;
- visible unavailable reason when read model provides one;
- submit command:
  POST /api/employee/requests/{requestId}/review/start;
- pending state while StartReview command is in flight;
- duplicate-click protection while pending;
- visible success feedback or refreshed read state after command success;
- Employee request details refresh after command success;
- Employee dashboard/list refresh after command success if dashboard cache exists;
- visible command error feedback when command is rejected;
- safe stale-state handling when another Employee starts review before click;
- no blind retry after CSRF/session/security failure;
- no approve/reject behavior.
```

This is the explicit client-side user action for starting active review.

It is not approval, not rejection, not AgreementProposalExchange creation and not the first agreement proposal flow.

## 2. Out of Scope

```text
- backend endpoint implementation -> SL-EMP-REQ-003;
- Employee request details read endpoint -> SL-EMP-REQ-002;
- Employee request dashboard/list read endpoint -> SL-EMP-REQ-001;
- Employee request dashboard page read implementation -> L2-EMP-DASH-001.client;
- Employee request details read page implementation -> L2-EMP-DETAILS-001.client;
- approve review command -> future approve client sidecar;
- reject review command / rejection feedback form -> future reject client sidecar;
- AgreementProposalExchange creation -> future agreement slices;
- Employee sends first proposal -> future agreement slices;
- Employee assignment/queue/lock model -> future slice;
- local CSRF token storage/refresh mechanics -> CC-CSRF-001/shared API infrastructure;
- manual generated OpenAPI/type edits;
- business-specific endpoint wrappers in shared/api.
```

Important nuance:

```text
Dashboard read implementation remains owned by L2-EMP-DASH-001.client.
Details read implementation remains owned by L2-EMP-DETAILS-001.client.

This sidecar only provides the StartReview feature and wires it into available action slots on those surfaces.
```

## 3. Related Slices / Owners

```text
SL-EMP-REQ-001 — Employee Request List Read
  Owns backend GET /api/employee/requests and compact list row review state.

L2-EMP-DASH-001.client — Employee Request Dashboard
  Owns Employee dashboard/list UI under pages/employee/requests/dashboard.

SL-EMP-REQ-002 — Employee Request Details Read
  Owns backend details read endpoint and details DTO.

L2-EMP-DETAILS-001.client — Employee Request Details
  Owns details route/page and read UI under pages/employee/requests/details.

SL-EMP-REQ-003 — Start Request Review
  Owns backend command:
    POST /api/employee/requests/{requestId}/review/start.

L2-REVIEW-START-001.client
  Owns Start Review button/action/mutation and command feedback.
  Provides reusable action for dashboard row and details action area.

SL-EMP-REQ-004 — Approve Request Review
  Future/next backend approve command.

SL-EMP-REQ-005 — Reject Request Review
  Future/next backend reject command and rejection feedback.

Future approve review client sidecar
  Owns Approve Review button/action/mutation.

Future reject review client sidecar
  Owns Reject Review button/form/mutation and rejection feedback UI.

CC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.

shared/api
  Owns only generic API infrastructure, not business endpoint wrappers.
```

Page placement rule for Employee request area:

```text
Dashboard:
  pages/employee/requests/dashboard

Details:
  pages/employee/requests/details
```

## 4. Visual UI / Scenario Flow

```text
[Signed-in Employee]
opens an Employee request surface:
  dashboard/list
  or details page
        ↓
[Request Surface]
shows request data, review state,
and action availability
        ↓
[Start Review Entry Point]
Start Review can appear:
  on dashboard/list row
  or in details action area
        ↓
Employee clicks “Start review”
        ↓
[StartReview Action]
button shows pending state
and prevents duplicate click
        ↓
 ┌──────────────────────────────┬──────────────────────────────────┐
 │ command accepted             │ command rejected                 │
 ▼                              ▼
Dashboard/details refresh       Error feedback is visible
        ↓                       Previous visible state remains safe
Review state shows              Relevant read state may refresh
StartedByCurrentEmployee
        ↓
Future approve/reject actions
may become available through
future command sidecars
```

In ordinary words:

An Employee may start review from either the request dashboard row or the request details action area. Both placements use the same StartReview feature. When the Employee clicks Start Review, the feature sends one unsafe POST command. On success, dashboard/details read state is refreshed and shows review started by the current Employee. On failure, the UI shows feedback and does not pretend the review has started.

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Employee | Opens Employee request dashboard/list or request details. |
| S02 | Request surface | Shows request data, review state and action availability. |
| S03 | Start Review entry point | Shows Start Review where the host surface can provide it. |
| S04 | User action | Employee clicks Start Review. |
| S05 | Pending state | Action shows command in progress and blocks duplicate submit. |
| S06 | Accepted outcome | Dashboard/details refresh and show `StartedByCurrentEmployee`. |
| S07 | Rejected outcome | Error feedback visible; previous state remains safe. |
| S08 | Future handoff | Future approve/reject sidecars may become available after refresh. |

## 5. Visual Client Implementation Flow

```text
[Dashboard Page Host]
pages/employee/requests/dashboard/EmployeeDashboardPage.tsx

Lives here:
  EmployeeDashboardPage

Uses:
  useEmployeeRequestDashboardQuery(...)
  EmployeeRequestDashboardList
  StartReviewButton through row action slot

Owns:
  dashboard/list page composition;
  list loading/error/empty/success branches;
  passing StartReviewButton into row action slot when row state allows it;
  keeping dashboard page under pages/employee/requests/dashboard.

Does not own:
  StartReview mutation implementation;
  command endpoint wrapper;
  low-level HTTP;
  CSRF token mechanics;
  details page layout.
```

```text
        ↓

[Details Page Host]
pages/employee/requests/details/EmployeeRequestDetailsPage.tsx

Lives here:
  EmployeeRequestDetailsPage

Uses:
  useEmployeeRequestDetailsQuery(requestId)
  EmployeeRequestDetailsView
  StartReviewButton through details action slot

Owns:
  details page composition;
  details loading/error/not-found/success branches;
  passing StartReviewButton into details action slot when details state allows it;
  keeping details page under pages/employee/requests/details.

Does not own:
  StartReview mutation implementation;
  command endpoint wrapper;
  low-level HTTP;
  CSRF token mechanics;
  dashboard row layout.
```

```text
        ↓

[Entity Request UI Layer]
entities/employee-request/ui/EmployeeRequestDashboardList.tsx
entities/employee-request/ui/EmployeeRequestDashboardRow.tsx
entities/employee-request/ui/EmployeeRequestDetailsView.tsx
entities/employee-request/ui/EmployeeReviewActionAvailabilityPanel.tsx

Lives here:
  dashboard/list read UI;
  details read UI;
  review state display;
  optional action slot placement.

Possible props:
  renderRowActions?(row): ReactNode
  renderReviewActions?(details): ReactNode

Owns:
  read-only dashboard/details layout;
  review state display;
  action availability display;
  optional action slot placement.

Does not own:
  StartReview button logic;
  StartReview mutation;
  pending/error command state;
  command endpoint wrapper.
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
  surface?: "dashboard" | "details"
  onStarted?(): void

Uses:
  useStartRequestReviewMutation()

Owns:
  visible Start Review button;
  click handler;
  disabled/unavailable state;
  pending state;
  command error feedback;
  accessible label/copy;
  surface-agnostic action behavior.

Does not own:
  dashboard row layout;
  details page layout;
  dashboard fetching;
  details fetching;
  approve/reject actions.
```

```text
        ↓

[Command Feature Model Layer]
features/employee-request/start-review/model/useStartRequestReviewMutation.ts

Lives here:
  useStartRequestReviewMutation()

Uses:
  useMutation()
  startRequestReview(requestId)
  employeeRequestQueryKeys.details(requestId)
  employeeRequestQueryKeys.dashboard(...)
  queryClient.invalidateQueries(...)

Owns:
  command mutation;
  success handling;
  details query refresh;
  dashboard/list query refresh if present;
  stale-state rejection refresh convention;
  command-level error propagation to feature UI.

Does not own:
  low-level fetchJson;
  CSRF implementation internals;
  entity dashboard/details display.
```

```text
        ↓

[Command Feature API Layer]
features/employee-request/start-review/api/startRequestReview.ts

Lives here:
  startRequestReview(requestId)

Uses:
  shared/api/fetchJson.ts
  shared/api/generated/openapi-types.ts only for operation-level generated contract if useful.

Owns:
  business command endpoint wrapper:
    POST /api/employee/requests/{requestId}/review/start

Does not own:
  React Query mutation;
  visible button feedback;
  page routing;
  shared business endpoint dump.
```

```text
        ↓

[Shared API Infrastructure Layer]
shared/api/fetchJson.ts
shared/api/generated/openapi-types.ts
shared/api/ApiError / ProblemDetails helpers
shared/api/antiforgeryTokenStore.ts, if present

Owns:
  generic request execution;
  generic error parsing;
  generated OpenAPI type source;
  CSRF/token infrastructure for unsafe requests.

Does not own:
  startRequestReview();
  employee request endpoint paths as business wrappers;
  feature-specific mutation behavior.
```

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Dashboard page | Employee dashboard renders list read state and row action slot. |
| I02 | Details page | Employee details renders details read state and details action slot. |
| I03 | Entity UI | Dashboard row/details view places optional action slot. |
| I04 | Page host | Host page passes StartReviewButton into slot when read model allows it. |
| I05 | Feature UI | StartReviewButton handles click, pending, disabled and error UI. |
| I06 | Feature model | Mutation submits `requestId`. |
| I07 | Feature API | `startRequestReview()` calls command endpoint via `fetchJson`. |
| I08 | Shared API infra | `fetchJson` applies generic transport/error/CSRF behavior. |
| I09 | Feature model | Success refreshes details and dashboard/list queries. |
| I10 | Entity UI | Refreshed surfaces show `StartedByCurrentEmployee`. |
| I11 | Feature UI | Rejected command shows visible feedback. |

## 6. Client API / Server Contract

Server command endpoint:

```http
POST /api/employee/requests/{requestId}/review/start
```

Route:

```text
requestId: number
```

Request body:

```text
none
```

Client must never send:

```text
employeeId
```

Employee actor is server-side auth/session context.

Success response:

```http
204 No Content
```

Response body:

```text
none
```

Reason:

```text
- StartReview is a command;
- client already knows requestId;
- current review state is read through dashboard/details endpoints after refetch;
- no command DTO should be used as a read source.
```

Feature-owned API wrapper:

```ts
// features/employee-request/start-review/api/startRequestReview.ts
import { fetchJson } from "../../../../shared/api/fetchJson";

export const startRequestReview = async (
  requestId: number,
): Promise<void> => {
  await fetchJson<void>(
    `/api/employee/requests/${encodeURIComponent(String(requestId))}/review/start`,
    { method: "POST" },
  );
};
```

Important API placement rule:

```text
Do not add:
  shared/api/employeeRequestApi.ts

Do add:
  features/employee-request/start-review/api/startRequestReview.ts
```

Important generated-contract rule:

```text
Use generated OpenAPI operation types if the project pattern needs them,
but do not invent or require StartRequestReviewResponseDto.
The current command success contract is 204 No Content.
```

Important read/command boundary:

```text
StartReview command success is not the Employee request details DTO.
Dashboard/details read state must be refreshed from read endpoints after success.
```

## 7. Cross-Cutting Concerns

```text
Auth/session:
  Employee session required.
  Client account sessions must not execute Employee review commands.

Employee account identity:
  Target L2 model uses Employee : Account.
  ClaimTypes.NameIdentifier stores Account.Id.
  For Employee sessions, Account.Id is Employee.Id.
  Client must never submit employeeId.

Authorization:
  Server owns visibility/reviewability.
  Client hides/disables action based on read model only.
  Server still re-checks on command.

API/generated contract:
  Use generated OpenAPI operation/paths if needed.
  Do not manually edit generated artifacts.
  Do not invent response DTO for 204 command.

API placement:
  Command endpoint wrapper lives in features/employee-request/start-review/api.
  shared/api remains generic infrastructure only.

ProblemDetails/error mapping:
  401/403 -> auth/access feedback.
  404 -> request not found / not visible feedback.
  422/conflict/lifecycle -> visible command error.
  400 antiforgery failure -> shared security/session recovery behavior.

Antiforgery/CSRF:
  StartReview is unsafe POST.
  Feature consumes shared CSRF-aware request helper through fetchJson/shared infra.
  Do not implement local CSRF token logic.
  Do not blindly auto-replay unsafe command after token refresh.

Stale state:
  If a read surface said StartReview was available but server rejects because another Employee started review,
  show command error and refresh relevant read state.

Accessibility:
  Pending state must be visible.
  Error feedback must be visible/announced.
  Disabled action should expose reason where available.

E2E:
  Assert visible command outcome from at least one entry point.
  Add second entry point coverage if test setup cost is acceptable.
  Do not assert React Query cache internals or backend transaction internals.
```

## 8. Questions / Decisions

### Blocked / unresolved

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-001` | verify | Is backend endpoint and generated OpenAPI operation available in current branch? | Confirm `POST /api/employee/requests/{requestId}/review/start` and 204 contract before runtime work. | API wrapper/tests. |
| `Q-L2-REVIEW-START-CLIENT-002` | verify | What are exact generated operation/type names? | Use generated OpenAPI after server implementation/generation. | Feature API typing. |

### Assumptions / current direction

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-004` | assumption | Same Employee double-clicks Start Review? | Button pending disables double-submit; backend lifecycle remains authoritative. | UX/error handling. |
| `Q-L2-REVIEW-START-CLIENT-005` | assumption | Another Employee started review between read and click? | Server rejects; client shows lifecycle/conflict feedback and refreshes relevant read state. | Stale state handling. |
| `Q-L2-REVIEW-START-CLIENT-006` | assumption | Should both entry points be E2E-covered? | Prefer one full happy E2E and one focused placement/component test for the second entry unless test setup is cheap. | Test scope. |

### Accepted directions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-007` | accepted | Is this approve/reject? | No. Start review only. | Keeps decision sidecars separate. |
| `Q-L2-REVIEW-START-CLIENT-008` | accepted | Does client submit Employee id? | No. Server resolves Employee from session/auth. | Prevents spoofing. |
| `Q-L2-REVIEW-START-CLIENT-009` | accepted | Is this unsafe request? | Yes. POST uses shared CSRF-aware boundary. | Cross-cutting requirement. |
| `Q-L2-REVIEW-START-CLIENT-010` | accepted | Where does button live? | `features/employee-request/start-review/ui`. | Command feature placement. |
| `Q-L2-REVIEW-START-CLIENT-011` | accepted | Where does command wrapper live? | `features/employee-request/start-review/api`. | New API ownership policy. |
| `Q-L2-REVIEW-START-CLIENT-012` | accepted | Can we add `shared/api/employeeRequestApi.ts`? | No. `shared/api` is generic infrastructure only. | Prevents shared API dump. |
| `Q-L2-REVIEW-START-CLIENT-013` | accepted | Are there two StartReview entry points? | Yes. Dashboard/list row and details action area can both host the same feature action. | One feature, two host placements. |
| `Q-L2-REVIEW-START-CLIENT-014` | accepted | Should this become two client slices? | No. Same user intent and same endpoint; keep one command sidecar. | Avoids tiny duplicate slices. |
| `Q-L2-REVIEW-START-CLIENT-015` | accepted | Does dashboard own the mutation? | No. Dashboard only hosts feature action in row action slot. | Preserves page/entity/feature boundary. |
| `Q-L2-REVIEW-START-CLIENT-016` | accepted | Should StartReview client use a response DTO? | No. Success is 204 No Content; refetch read endpoints. | Prevents command/read coupling. |

## 9. Extension / Change Points

```text
- Approve Review action -> future approve client sidecar;
- Reject Review action / feedback form -> future reject client sidecar;
- Dashboard row action styling/placement -> dashboard UI refinement, not separate command slice;
- review history/audit display -> future details refinement;
- assignment/queue/lock model -> future Employee queue slice;
- richer lifecycle error codes -> generated constants if exposed to client;
- API placement cleanup for older slices that still use shared/api business wrappers.
```

## 10. Behavior Coverage

| Source behavior item | How client sidecar covers it | Status |
|---|---|---|
| `L2-REVIEW-START-001` Employee can start review for InReview request with no active started review | StartReviewButton sends command when dashboard/details read model allows it. | covered after backend/generated contract |
| `L2-REVIEW-START-002` Starting review stores StartedByEmployeeId and StartedAt | Client sends no Employee id; server uses auth context; client refreshes read state. | covered by command boundary |
| `L2-REVIEW-BLOCK-002` Another Employee cannot start/approve/reject review already started by someone else | Client disables action when read model says started by other; server rejection shown if stale. | covered at UI boundary |
| `L2-REVIEW-NW-001` Failed review command does not change request/review state | Client shows rejected feedback and keeps/refreshes safe read state. | covered at UI boundary |
| Details action visibility from `SC-07A` | Details host renders StartReview action only when details state allows it. | covered |
| Dashboard review-state visibility from `SC-06` | Dashboard host can render StartReview action from row state while dashboard remains read-owner. | covered as placement |
| Approve request | Not covered; future approve sidecar/backend slice. | out of scope |
| Reject request | Not covered; future reject sidecar/backend slice. | out of scope |
| AgreementProposalExchange | Not covered. | out of scope |

Not behavior coverage:

```text
- generated OpenAPI type exists;
- endpoint string exists;
- query invalidation call exists;
- repository/handler was called;
- CSRF helper internals were called;
- button component exists without successful command flow.
```

## 11. Client / Component / E2E Verification Plan

E2E is implementation-time coverage only after all of the following exist:

```text
- Employee auth/session test setup;
- SL-EMP-REQ-001 list read endpoint + dashboard client read surface;
- SL-EMP-REQ-002 details read endpoint + details client read surface;
- SL-EMP-REQ-003 backend endpoint;
- generated OpenAPI contract for StartReview.
```

Until then, E2E remains planned verification, not an immediately runnable requirement.

### Component/client tests

| Test / check | Verifies |
|---|---|
| StartReviewButton renders enabled when action is available | Available action visible |
| StartReviewButton renders disabled/hidden when action unavailable | Blocked state respected |
| Disabled button shows unavailable reason when provided | User understands blocked state |
| Clicking button calls mutation with requestId | Correct command target |
| Pending state disables button | Double-submit UX guard |
| Pending state text/aria state visible | Accessible pending feedback |
| Success callback/invalidation is triggered | Read state can update |
| Error state renders visible feedback | Rejected command feedback |
| Button does not render approve/reject controls | Scope boundary |
| Dashboard row action slot can host StartReviewButton | First placement |
| Details action slot can host StartReviewButton | Second placement |

### Feature API / model tests

| Test / check | Verifies |
|---|---|
| `startRequestReview(requestId)` posts to `/api/employee/requests/{requestId}/review/start` | Correct command endpoint |
| Wrapper sends no request body | Contract compliance |
| Wrapper resolves `void` for 204 success | No invented response DTO |
| Wrapper imports `fetchJson` from shared API infrastructure | New API placement rule |
| 401/403/404/422 ProblemDetails surface to mutation caller | Feedback can render |
| Mutation invalidates/refetches details query after success | Details refresh |
| Mutation invalidates/refetches dashboard/list query if present | Dashboard refresh |
| Mutation does not call approve/reject APIs | Scope boundary |

### E2E happy path from details

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

### E2E happy path from dashboard/list

```text
setup Employee session
        ↓
setup employee-accessible InReview request with no started review
        ↓
open Employee request dashboard/list
        ↓
assert request row shows NotStarted review state
        ↓
assert row Start Review action visible
        ↓
click Start Review
        ↓
assert row/dashboard refresh shows StartedByCurrentEmployee
```

### E2E stale/blocked path

```text
setup Employee session
        ↓
setup request started by another Employee
        ↓
open Employee dashboard or details
        ↓
assert Start Review action unavailable/blocked
        ↓
assert started-by-another marker visible
```

### E2E stale-after-read path

```text
setup Employee session
        ↓
open surface where Start Review appears available
        ↓
make server state stale by starting review elsewhere if test setup supports it
        ↓
click Start Review
        ↓
assert lifecycle/conflict error feedback visible
        ↓
assert dashboard/details refreshes or remains safe
```

### Explicit non-goals for tests

```text
Do not test approve/reject behavior here.
Do not test backend transaction internals in client tests.
Do not assert React Query cache internals in E2E.
Do not duplicate full CSRF matrix here.
Do not test AgreementProposalExchange behavior here.
Do not test Employee assignment/queue behavior here.
Do not add shared/api business wrapper tests.
```

## 12. Suggested File Placement

```text
src/features/employee-request/start-review/ui/
  StartReviewButton.tsx
  startReviewButtonConst.ts
  startReviewButton.css

src/features/employee-request/start-review/model/
  useStartRequestReviewMutation.ts

src/features/employee-request/start-review/api/
  startRequestReview.ts

src/pages/employee/requests/dashboard/
  EmployeeDashboardPage.tsx

src/pages/employee/requests/details/
  EmployeeRequestDetailsPage.tsx

src/entities/employee-request/ui/
  EmployeeRequestDashboardList.tsx
  EmployeeRequestDashboardRow.tsx
  EmployeeRequestDetailsView.tsx
  EmployeeReviewActionAvailabilityPanel.tsx

src/entities/employee-request/model/
  employeeRequestQueryKeys.ts

src/shared/api/
  fetchJson.ts
  generated/openapi-types.ts
  generic ProblemDetails / ApiError helpers only

tests/e2e/employee/
  start-request-review.spec.ts
```

Do **not** add:

```text
src/shared/api/employeeRequestApi.ts
```

Page placement rule:

```text
Dashboard:
  pages/employee/requests/dashboard

Details:
  pages/employee/requests/details
```

## 13. Scenario Docs Sync

Scenario source docs should reflect the two entry points:

```text
SC-06:
  dashboard remains a read/list scenario,
  but it may host a Start Review entry point when the command sidecar is wired.
  Command persistence/domain behavior remains in SC-07B.

SC-07A:
  details action area can host Start Review when the read model says it is available.

SC-07B:
  StartReview command behavior can be initiated from dashboard row or details action area.
```

## 14. Next Step

Runtime implementation order:

```text
1. Confirm/apply SL-EMP-REQ-003 backend endpoint.
2. Run OpenAPI/type generation.
3. Confirm generated StartReview operation names and 204 contract.
4. Implement feature-owned API wrapper:
   features/employee-request/start-review/api/startRequestReview.ts.
5. Add useStartRequestReviewMutation().
6. Add StartReviewButton.
7. Wire StartReviewButton into details action slot.
8. Wire StartReviewButton into dashboard row action slot.
9. Refresh details query after success.
10. Refresh dashboard/list query after success.
11. Add component tests.
12. Add feature API/model tests.
13. Add E2E happy path from at least one entry point.
14. Add placement coverage for the second entry point.
```

Implementation checklist:

```text
[ ] Confirm backend endpoint exists:
    POST /api/employee/requests/{requestId}/review/start.
[ ] Confirm generated StartReview operation name and 204 No Content success.
[ ] Add feature API wrapper startRequestReview().
[ ] Add useStartRequestReviewMutation().
[ ] Add StartReviewButton.
[ ] Wire StartReviewButton into Employee details action slot.
[ ] Wire StartReviewButton into Employee dashboard row action slot.
[ ] Ensure Employee id is never sent by client.
[ ] Ensure unsafe request uses shared CSRF-aware infrastructure.
[ ] Refresh details query after success.
[ ] Refresh dashboard/list query after success.
[ ] Show pending/error feedback.
[ ] Add component tests.
[ ] Add feature API/model tests.
[ ] Add E2E happy path.
[ ] Add E2E blocked/stale path.
```
