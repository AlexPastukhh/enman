# L2-EMP-DETAILS-001.client — Employee Request Details

Status: full client read sidecar draft / details read server contract pending / StartReview command contract known  
Scenario: `SC-07A — Employee Request Details`  
Package: `[Employee] [Requests]`  
Slice type: L2 client read/details sidecar  
Parent server slice: `SL-EMP-REQ-002 — Employee Request Details Read`  
Related command slice: `SL-EMP-REQ-003 — Start Request Review`  
Architecture direction: read/details slice maps to `pages + entities`; review command actions stay in future `features`.

## 1. Sidecar Overview

This sidecar defines the client read/details page for an Employee viewing one request.

It owns:

```text
Employee opens one employee-accessible request
        ↓
client loads details read DTO
        ↓
client renders request/applicant/review state
        ↓
client shows which review actions are available or blocked
        ↓
client provides slots for future command sidecars
```

It does not execute review commands.

Important contract boundary:

```text
SL-EMP-REQ-002 owns the details read DTO.

SL-EMP-REQ-003 StartReview owns the command endpoint:
  POST /api/employee/requests/{requestId}/review/start

StartReviewResponseDto is not the details DTO.
```

## 2. Scope

This client sidecar owns:

```text
- Employee request details route/page;
- signed-in Employee request details read state;
- loading / not-found / forbidden / error / success states;
- request status display;
- request details/data display;
- applicant/client data needed for review;
- review state display:
  no review started,
  started by current Employee,
  started by another Employee,
  completed approved/rejected;
- review action availability display:
  start available / blocked,
  approve/reject available / blocked;
- optional review action slot placement for future command sidecars;
- navigation back to Employee dashboard;
- no command execution in this read sidecar.
```

Source scenario meaning:

```text
Employee opens request details, sees request details + applicant/request data + review state,
and sees review actions enabled/disabled according to domain state.
```

## 3. Out of Scope

| Out of scope | Owner |
|---|---|
| Start review command execution | `SL-EMP-REQ-003.client` |
| Approve review command execution | `SL-EMP-REQ-004.client` |
| Reject review command / rejection feedback form | `SL-EMP-REQ-005.client` |
| Employee dashboard list implementation | `L2-EMP-DASH-001.client` / `SL-EMP-REQ-001.client` |
| Employee request details server endpoint | `SL-EMP-REQ-002` |
| Employee assignment/claiming beyond review start | future assignment/queue slice |
| Proposal/agreement exchange after approval | `SC-13*` / future agreement slices |
| Domain review persistence/API details | server/domain command slices |
| Manual generated OpenAPI/type edits | OpenAPI generation workflow |
| Local antiforgery mechanics | `CC-CSRF-001`; future unsafe command sidecars consume shared helper |
| Documents / review history / assignment fields | future details refinements |

SC-07A excludes review command persistence/API details, employee assignment/claiming beyond review start, and proposal exchange after approval.

## 4. Related Slices / Owners

```text
SL-EMP-REQ-001 — Employee Request List Read
  Owns employee request list/read dashboard rows.

L2-EMP-DASH-001.client — Employee Request Dashboard
  Owns dashboard route/page/list UI and navigation to details.

SL-EMP-REQ-002 — Employee Request Details Read
  Owns Employee request details read endpoint and compact review state in details payload.

L2-EMP-DETAILS-001.client — Employee Request Details
  Owns this details read page/sidecar.

SL-EMP-REQ-003 — Start Request Review
  Owns backend command:
    POST /api/employee/requests/{requestId}/review/start.

Future SL-EMP-REQ-003.client
  Owns Start Review button/action and command feedback.

SL-EMP-REQ-004 — Approve Request Review
  Owns approve backend command.

Future SL-EMP-REQ-004.client
  Owns approve button/action.

SL-EMP-REQ-005 — Reject Request Review
  Owns reject backend command and rejection feedback.

Future SL-EMP-REQ-005.client
  Owns reject button/form/action.
```

## 5. Sources / Source Behavior Items

Scenario flow and behavior coverage must come from scenario source files.

Primary scenario source:

```text
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
```

Primary DATA source:

```text
planning/diagrams/scenario-data/SC-07A-employee-request-details-data.md
```

Primary behavior source:

```text
planning/diagrams/scenario-behavior-items/SC-07A-employee-request-details-behavior-items.md
```

Related scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
```

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Important source hierarchy:

```text
Scenario text/DATA/UI/behavior files
  source of truth for Scenario Flow and Behavior Coverage.

Domain draft
  domain-design input for terminology, aggregate boundaries, state-machine direction and code sketches.
```

Do not use domain draft directly as behavior-item source when scenario behavior files exist.

## 6. Visual UI / Scenario Flow

```text
[Signed-in Employee]
opens Employee request details
        ↓
[Details Page]
shows request status
and request data
        ↓
[Applicant / Client Area]
shows applicant/client data needed for review
        ↓
[Review State Area]
shows one of:
  no review started
  review started by current Employee
  review started by another Employee
  completed approved/rejected
        ↓
[Review Action Availability Area]
shows available/blocked actions according to state
        ↓
[Optional Action Slot]
future command sidecars may render Start / Approve / Reject actions
when available
        ↓
Employee can return to dashboard
```

Action availability source behavior:

```text
No Review + Request InReview:
  Start review action can be available.

Review Started by current Employee:
  Approve/Reject can be available.

Review Started by another Employee:
  Start/Approve/Reject are blocked/unavailable.

No Review:
  Approve/Reject are blocked.

Request Approved/Rejected/AgreementExchangeFailed:
  Start review is blocked.
```

In ordinary words:

```text
An Employee opens a request details page.

The page shows request data, applicant/client data and review state needed
to understand what can happen next.

This sidecar does not start, approve or reject review.

It only shows whether those actions should be available or blocked,
and provides a future action slot where command sidecars can render their
buttons/forms.
```

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Employee | Opens request details from dashboard/details link. |
| S02 | Details page | Shows request status and request data. |
| S03 | Applicant/client area | Shows applicant/client data needed for review. |
| S04 | Review state area | Shows no review / current Employee started / another Employee started / completed. |
| S05 | Action availability area | Shows which review actions are available or blocked. |
| S06 | Optional action slot | Future command sidecars may render action controls. |
| S07 | Not-found/access state | Shows safe state if request is missing or not accessible. |
| S08 | Navigation | Allows returning to Employee dashboard. |

## 7. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/employee/requests/details/EmployeeRequestDetailsPage.tsx

Lives here:
  EmployeeRequestDetailsPage

Uses:
  useParams()
  useSession() or future useEmployeeSession()
  useEmployeeRequestDetailsQuery(requestId)

Owns:
  route params parsing
  Employee auth/session branch
  details loading/error/not-found/success branches
  page composition
  navigation back to Employee dashboard
  placing future review action slots if later sidecars provide them

Does not own:
  low-level HTTP
  generated DTO aliases
  request details card internals
  review command mutations
```

```text
        ↓

[Entity Query Layer]
entities/employee-request/model/useEmployeeRequestDetailsQuery.ts
entities/employee-request/model/employeeRequestQueryKeys.ts
entities/employee-request/model/employeeRequestTypes.ts

Lives here:
  useEmployeeRequestDetailsQuery(requestId)
  employeeRequestQueryKeys.details(requestId)
  EmployeeRequestDetails
  EmployeeRequestReviewState
  EmployeeReviewActionAvailability

Uses:
  getEmployeeRequestDetails(requestId)

Owns:
  React Query details hook
  details query key
  entity read type aliases
  request id enabled guard

Does not own:
  route/session branch
  visual layout
  review command mutation
```

```text
        ↓

[Entity API Layer]
entities/employee-request/api/getEmployeeRequestDetails.ts

Lives here:
  getEmployeeRequestDetails(requestId)

Uses:
  shared API wrapper from shared/api/employeeRequestApi.ts
  or server package-specific API file chosen by backend contract

Owns:
  entity-level Employee request details read operation

Does not own:
  fetchJson details
  React Query
  UI rendering
```

```text
        ↓

[Shared API Layer]
shared/api/employeeRequestApi.ts
shared/api/apiPaths.ts or package-specific path file

Lives here after SL-EMP-REQ-002 server contract exists:
  getEmployeeRequestDetails(requestId)

Possible endpoint direction:
  GET /api/employee/requests/{requestId}
  or server-slice-defined Employee package route

Uses:
  fetchJson()
  generated OpenAPI types

Owns:
  low-level HTTP GET
  generated response type alias

Does not own:
  React Query
  page state
  details rendering
  review command behavior
```

```text
        ↓

[Generated Contract Layer]
shared/api/generated/openapi-types.ts

Lives here after server implementation/generation:
  Employee request details operation
  Employee request details DTO
  review state / action availability fields
  ProblemDetails branches

Owns:
  generated structural API contract

Does not own:
  handwritten client logic
  manual edits
  page/UI decisions
```

```text
        ↓

[Entity Display UI Layer]
entities/employee-request/ui/EmployeeRequestDetailsView.tsx
entities/employee-request/ui/EmployeeRequestStatusPanel.tsx
entities/employee-request/ui/EmployeeApplicantReviewData.tsx
entities/employee-request/ui/EmployeeReviewStatePanel.tsx
entities/employee-request/ui/EmployeeReviewActionAvailabilityPanel.tsx

Lives here:
  EmployeeRequestDetailsView
  EmployeeRequestStatusPanel
  EmployeeApplicantReviewData
  EmployeeReviewStatePanel
  EmployeeReviewActionAvailabilityPanel

Props:
  details: EmployeeRequestDetails
  renderReviewActions?(details): ReactNode

Owns:
  read-only details layout
  request status/data display
  applicant/client review data display
  review state display
  action availability display
  optional action slot placement

Does not own:
  fetching
  route/session branch
  StartReview command
  ApproveReview command
  RejectReview command
  mutation state
  CSRF token handling
```

```text
        ↓

[Future Command Feature Layer — not this sidecar]
features/employee-request/start-review/ui/StartReviewButton.tsx
features/employee-request/start-review/model/useStartRequestReviewMutation.ts
features/employee-request/start-review/api/startRequestReview.ts

Owns:
  Start Review button/action;
  POST /api/employee/requests/{requestId}/review/start mutation;
  pending/error/success feedback;
  details/list read refresh after success.
```

In ordinary words:

```text
The details page owns route/session handling and loads one
employee-accessible request details record.

Entity query/API/shared API own the read pipeline.

Entity UI renders details and action availability.

Review command buttons are not owned here.
They are future feature-owned actions rendered through an optional action slot.
```

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Route/page | Employee details page parses request id and renders details read context. |
| I02 | Page | Page checks Employee session and renders loading/error/not-found/success branches. |
| I03 | Entity query | Query loads employee-accessible request details. |
| I04 | Entity API | Entity read operation delegates to shared API wrapper. |
| I05 | Shared API | Wrapper calls server Employee request details endpoint. |
| I06 | Entity display UI | Details view renders request/applicant/review state data. |
| I07 | Entity display UI | Action availability panel shows which review actions are available/blocked. |
| I08 | Entity display UI | Optional action slot allows future command feature buttons. |
| I09 | Page | Page allows navigation back to Employee dashboard. |

## 8. Client API / Server Contract

Runtime implementation is blocked until `SL-EMP-REQ-002 — Employee Request Details Read` server endpoint and generated DTO exist.

Target read endpoint direction is pending `SL-EMP-REQ-002`:

```text
GET /api/employee/requests/{requestId}
or another server-slice-defined Employee package route
```

Target response shape should be derived from SC-07A scenario/DATA, not treated as final client-invented contract:

```ts
type EmployeeRequestDetailsResponse = {
  request: EmployeeRequestDetailsDto;
};

type EmployeeRequestDetailsDto = {
  requestId: number;
  displayNumber?: string | null;

  requestType: string;
  requestStatus: string;
  createdAt: string;

  requestDetails?: string | null;
  address?: EmployeeRequestAddressDto | null;

  applicantSummary?: EmployeeApplicantReviewSummaryDto | null;
  clientSummary?: string | null;

  reviewState: EmployeeRequestReviewState;
  reviewActionAvailability: EmployeeReviewActionAvailabilityDto;
};

type EmployeeRequestReviewState =
  | "NotStarted"
  | "StartedByCurrentEmployee"
  | "StartedByAnotherEmployee"
  | "Approved"
  | "Rejected";

type EmployeeReviewActionAvailabilityDto = {
  canStartReview: boolean;
  canApproveReview: boolean;
  canRejectReview: boolean;
  reason?: string | null;
};
```

Known future command contract from `SL-EMP-REQ-003`:

```text
POST /api/employee/requests/{requestId}/review/start
```

Command response sketch:

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

Important:

```text
StartReview response is not the Employee details DTO.

Details read sidecar must wait for SL-EMP-REQ-002 DTO.

StartReview belongs to future SL-EMP-REQ-003.client.
```

Contract rules:

```text
- Client must not send employeeId.
- Server derives Employee context from auth/session.
- Server enforces Employee access.
- Client uses generated OpenAPI types after server contract exists.
- Client must not handwrite DTO shape once generated types exist.
- Review state and action availability should be server-provided or explicitly contract-defined.
```

## 9. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Employee details requires Employee-authenticated context; Client account sessions must not see Employee request details. |
| Authorization/visibility | yes | Server owns employee visibility. Client maps 401/403/404 to safe states. |
| Antiforgery / unsafe requests | no for details read | Details read is safe GET by default. Future StartReview/Approve/Reject commands are unsafe and must consume shared antiforgery behavior. |
| Request validation / ProblemDetails | yes | Route id handling and read failures map to safe page states. |
| OpenAPI / generated artifacts | yes | Use generated OpenAPI types after SL-EMP-REQ-002 exists. Do not manually edit generated artifacts. |
| Generated constants/error codes | maybe | If access/not-found/error codes are client-facing, consume generated constants. |
| Transaction / atomicity | no | Read-only. |
| No-mutation safety | yes | GET must not create Review or change Request/Review state. |
| Idempotency / retry | no | Safe GET is repeatable. |
| Concurrency / stale state | yes | Review state can change after load; future commands re-check server-side. |
| File/document boundary | not now | Documents/review history are future details refinements. |
| Clock/audit actor fields | read only | Reads timestamps only if server DTO includes them. |
| Privacy / cross-account data exposure | yes | Do not expose employee private/auth data or cross-client request data. |
| Client feedback / accessibility | yes | Safe not-found/access/error states; action availability should be visible and accessible. |
| Testing responsibility split | yes | Component/shared API/E2E for visible read behavior; no command tests here. |

## 10. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-EMP-DETAILS-CLIENT-001` | blocked | What is exact details read endpoint and generated DTO? | Use `SL-EMP-REQ-002` server/OpenAPI once available. Current DTO sketch is derived from SC-07A only. | API wrapper and tests. |
| `Q-L2-EMP-DETAILS-CLIENT-002` | accepted | Is this read or command sidecar? | Read sidecar. Review commands are future feature sidecars. | Placement in `pages + entities`. |
| `Q-L2-EMP-DETAILS-CLIENT-003` | accepted | Which terminology should UI use? | Employee, not Worker. | Labels/copy/tests. |
| `Q-L2-EMP-DETAILS-CLIENT-004` | accepted | Does this sidecar execute start/approve/reject? | No. It only shows action availability / slots. | Scope boundary. |
| `Q-L2-EMP-DETAILS-CLIENT-005` | assumption | First route path? | Candidate: `/employee/requests/:requestId`; final path follows server/client route decision. | Route setup. |
| `Q-L2-EMP-DETAILS-CLIENT-006` | accepted | Should client derive action availability from raw fields? | Prefer server-provided action availability. Client should not guess unless contract explicitly provides all required fields. | DTO design and tests. |
| `Q-L2-EMP-DETAILS-CLIENT-007` | accepted | Does details include documents/review history? | No, future per SC-07A DATA. | Scope boundary. |
| `Q-L2-EMP-DETAILS-CLIENT-008` | accepted | What about actions when another Employee started review? | UI must show blocked/unavailable action state. Commands belong to future sidecars. | UI behavior. |
| `Q-L2-EMP-DETAILS-CLIENT-009` | accepted | Does details read sidecar implement StartReview? | No. StartReview belongs to future `SL-EMP-REQ-003.client`. | Scope boundary. |
| `Q-L2-EMP-DETAILS-CLIENT-010` | accepted | Can StartReview response be used as details DTO? | No. It is compact command result only. | Prevents DTO misuse. |
| `Q-L2-EMP-DETAILS-CLIENT-011` | accepted | Does StartReview require CSRF? | Yes, but only future command sidecar consumes shared CSRF-aware boundary. Details read remains safe GET. | Cross-cutting split. |

## 11. Extension / Change Points

```text
- Employee dashboard page -> L2-EMP-DASH-001.client / SL-EMP-REQ-001.client;
- Start review action -> SL-EMP-REQ-003.client;
- Approve action -> SL-EMP-REQ-004.client;
- Reject action / rejection feedback form -> SL-EMP-REQ-005.client;
- documents / verification result / review history -> future details refinement;
- assignment / lock -> future employee assignment/queue slice;
- proposal exchange after approval -> SC-13*.
```

## 12. Behavior Coverage

SC-07A behavior items file currently has no directly owned migrated items, but lists related `EMP-READ-001` and `REQ-LC-004`.

The scenario itself defines details visibility and action availability rules.

| Source behavior item / scenario requirement | How client sidecar covers it | Status |
|---|---|---|
| Employee opens request details and sees request details/applicant data | Details page renders request data and applicant/client review data. | covered after DTO contract |
| Details show review state | Review state panel renders server-provided state. | covered after DTO contract |
| Actions enabled/disabled according to domain state | Action availability panel renders server-provided availability. | covered as read visibility |
| Started by another Employee blocks actions | UI shows actions blocked/unavailable. | covered as read visibility |
| Approve/reject require started review | UI shows approve/reject unavailable when no started review. | covered as read visibility |
| `EMP-READ-001` protected employee-accessible request data | Client shows returned details; server owns access enforcement. | covered at UI boundary / server-dependent |
| `REQ-LC-004` approved request cannot be reviewed again | UI displays completed/blocked state if server DTO says so. | related/dependent |
| StartReview command starts review | Not covered here; future `SL-EMP-REQ-003.client`. | out of scope |

Not behavior coverage:

```text
- React Query cache key exists;
- endpoint string exists;
- generated type exists;
- mocks were called;
- StartReview command button executes command;
- backend domain methods were called.
```

## 13. Client / Component / E2E Verification Plan

### Component/client tests

| Test / check | Verifies |
|---|---|
| Employee details page renders for Employee session | Read entry point visible |
| Non-Employee / signed-out branch renders safe state | Access boundary UI |
| Loading state renders while details query is pending | Pending read state |
| Not-found state renders for missing request | Safe missing details state |
| Access/forbidden state renders for inaccessible request | Safe authorization UI |
| Error state renders for unexpected read failure | Safe read failure UI |
| Request status/details/address render | Request visible data |
| Applicant/client review data renders | Review-relevant data |
| Not-started review state renders | Review state display |
| Started-by-current-Employee review state renders | Review state display |
| Started-by-another-Employee review state renders | Review state display |
| Approved/rejected completed states render | Completed review state display |
| Start availability renders when no review + InReview | Action availability visibility |
| Approve/reject availability renders when started by current Employee | Action availability visibility |
| Actions blocked when started by another Employee | Action availability visibility |
| Optional review action slot can render external content | Future command composition point |
| No review command mutation is called by this sidecar | Command scope boundary |

### Shared API / entity tests

| Test / check | Verifies |
|---|---|
| `getEmployeeRequestDetails(requestId)` calls server details endpoint | Correct API wrapper |
| Entity API delegates to shared API wrapper | Layering |
| Query hook uses details query key | Cache identity |
| Query hook is disabled for invalid/missing request id | Safe param handling |
| DTO aliases come from generated OpenAPI types | No handwritten contract drift |
| 404/403/ProblemDetails map to page states | Safe error mapping |

### E2E happy read path

```text
setup Employee session
        ↓
setup employee-accessible InReview request
        ↓
open Employee request details page
        ↓
assert request status/details visible
        ↓
assert applicant/client review data visible
        ↓
assert review state visible
        ↓
assert action availability state visible
```

### E2E started-by-other path

```text
setup Employee session
        ↓
setup request review started by another Employee
        ↓
open Employee request details page
        ↓
assert started-by-another-Employee marker visible
        ↓
assert start/approve/reject actions are unavailable or blocked
```

### E2E access/not-found path

```text
open inaccessible or missing Employee request details
        ↓
assert safe not-found/access state visible
```

### Explicit non-goals for tests

```text
Do not test StartReview command here.
Do not test approve/reject commands here.
Do not assert React Query cache internals in E2E.
Do not assert backend authorization internals in E2E.
Do not test CSRF behavior in this read sidecar.
Do not test documents/review history/assignment fields here.
```

## 14. Suggested File Placement

```text
src/pages/employee/requests/details/
  EmployeeRequestDetailsPage.tsx
  employeeRequestDetailsPage.css

src/entities/employee-request/api/
  getEmployeeRequestDetails.ts

src/entities/employee-request/model/
  employeeRequestQueryKeys.ts
  employeeRequestTypes.ts
  useEmployeeRequestDetailsQuery.ts

src/entities/employee-request/ui/
  EmployeeRequestDetailsView.tsx
  EmployeeRequestStatusPanel.tsx
  EmployeeApplicantReviewData.tsx
  EmployeeReviewStatePanel.tsx
  EmployeeReviewActionAvailabilityPanel.tsx
  EmployeeRequestDetailsEmptyState.tsx
  employeeRequestDetailsConst.ts
  employeeRequestDetails.css

src/shared/api/
  employeeRequestApi.ts
  api path file chosen by server/client convention

src/shared/config/
  clientRoutes.ts

src/app/router/
  router.tsx

tests/e2e/employee/
  employee-request-details.spec.ts
```

## 15. Implementation Checklist

```text
[ ] Confirm/apply SL-EMP-REQ-002 server details endpoint and generated DTO.
[ ] Add Employee details route constant.
[ ] Add router route for Employee request details.
[ ] Add shared API wrapper for details GET.
[ ] Add entity API function `getEmployeeRequestDetails`.
[ ] Add entity query key and `useEmployeeRequestDetailsQuery`.
[ ] Add entity type aliases from generated OpenAPI types.
[ ] Add page with Employee session/access branch.
[ ] Add details read UI panels.
[ ] Add action availability panel.
[ ] Add optional action slot for future command feature components.
[ ] Add safe mapping for loading/not-found/forbidden/error states.
[ ] Add dashboard back navigation.
[ ] Add component/shared API tests.
[ ] Add E2E read/access paths once backend test setup supports Employee sessions.
[ ] Do not implement StartReview command.
[ ] Do not implement approve/reject commands.
[ ] Do not handwrite final DTO once generated types exist.
```

## 16. Next Step

Runtime implementation is blocked until `SL-EMP-REQ-002 — Employee Request Details Read` endpoint and generated DTO are known.

Recommended order:

```text
1. Confirm or implement SL-EMP-REQ-002 server read slice.
2. Generate OpenAPI/types.
3. Update this client draft with exact endpoint/DTO names.
4. Implement client route/page/entity query/shared API wrapper.
5. Add details panels and action availability display.
6. Add component/shared API tests.
7. Add E2E read/access paths once backend test setup supports Employee sessions.
8. Implement SL-EMP-REQ-003.client separately for StartReview action.
```
