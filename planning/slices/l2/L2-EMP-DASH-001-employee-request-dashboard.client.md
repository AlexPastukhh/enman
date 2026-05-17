# L2-EMP-DASH-001.client — Employee Request Dashboard

Status: full client read sidecar draft / server read contract pending  
Scenario: `SC-06 — Employee Request Dashboard`  
Parent backend/read slice: `SL-EMP-REQ-001 — Employee Request List Read`  
Slice type: L2 client read sidecar  
Architecture direction: read slice maps to `pages + entities`; review commands stay in future `features`.

## 1. Sidecar Overview

This sidecar plans the Employee request dashboard client read surface.

It turns the short dashboard draft into a full client sidecar while keeping the important blocker explicit:

```text
Runtime implementation waits for:
- Employee request list server endpoint;
- generated OpenAPI DTOs/types;
- Employee session/auth route decision.
```

The dashboard is read-only.

It does not start review, approve review, reject review, create agreement proposal exchange, or upload agreement documents.

## 2. Scope

This client sidecar owns:

```text
- Employee request dashboard route/page;
- signed-in Employee dashboard read state;
- list of employee-accessible / review-relevant requests;
- dashboard row visible data:
  request id / display number,
  request type,
  request status,
  created at,
  applicant/request/client summary,
  review state marker;
- distinguishing review state:
  no review started,
  review started by current Employee,
  review started by another Employee;
- optional status filter if server contract supports it;
- loading / empty / error read states;
- opening Employee request details for a selected request.
```

Source scenario meaning:

```text
Employee opens dashboard, sees review-relevant requests, sees review-state marker, and opens request details.
```

## 3. Out of Scope

| Out of scope | Owner |
|---|---|
| Start review command | `SC-07B` / future `SL-EMP-REQ-003.client` or review command sidecar |
| Approve/reject commands | `SC-07B` / future review command sidecars |
| Employee request details page implementation | `SC-07A.client` / future details sidecar |
| Employee request list server endpoint | `SL-EMP-REQ-001 — Employee Request List Read` |
| Employee request details server endpoint | `SL-EMP-REQ-002 — Employee Request Details Read` |
| Employee assignment/queue policy | Future employee assignment/queue slice |
| Department/permission model | Future employee auth/authorization slice |
| AgreementProposalExchange | `SC-13*` / future agreement slices |
| Client-side review command actions | Future command/action sidecars |
| Manual generated OpenAPI/type edits | API generation workflow |
| Local antiforgery mechanics | `CC-CSRF-001` |

SC-06 excludes start review, approve/reject, assignment/queue policy, department/permission model and agreement proposal exchange.

## 4. Related Slices / Owners

```text
SC-06 Employee Request Dashboard
  owns dashboard read scenario and review-state visibility.

SL-EMP-REQ-001 — Employee Request List Read
  owns server list endpoint and filters consumed by this dashboard.

SC-07A Employee Request Details
  owns details page read scenario and review action visibility.

SC-07B Employee Request Review
  owns start/approve/reject domain command behavior.

Future L2-EMP-DETAILS-001.client
  owns Employee request details UI.

Future L2-REVIEW-START.client
  owns start review action.

Future L2-REVIEW-APPROVE.client
  owns approve action.

Future L2-REVIEW-REJECT.client
  owns reject action / optional feedback UI.
```

SC-07A owns details/read action visibility, while SC-07B owns domain command behavior for start/approve/reject.

## 5. Sources / Source Behavior Items

Scenario source:

```text
planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md
```

Related scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
```

Behavior source:

```text
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md
```

Domain-design input:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

Important rule:

```text
Scenario text/DATA/UI/behavior files are source of truth for Scenario Flow and Behavior Coverage.

Domain draft is domain-design input for naming, aggregate boundaries, invariants and code-sketch direction.
```

## 6. Visual UI / Scenario Flow

```text
[Signed-in Employee]
opens Employee request dashboard
        ↓
[Dashboard Page]
shows review-relevant requests
        ↓
For each request row, Employee sees:
  request identity / display number
  request type
  request status
  created date
  applicant/request/client summary
  review state marker
        ↓
Review state marker distinguishes:
  no review started
  review started by current Employee
  review started by another Employee
        ↓
Employee selects one request
        ↓
System opens Employee request details page
```

In ordinary words:

An Employee opens the dashboard and sees requests relevant to review work. Each row gives enough information to identify the request and understand its review state. The dashboard does not start, approve or reject review. It only shows whether review has not started, was started by the current Employee, or was started by another Employee. From a row, Employee can open request details.

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Employee | Opens request dashboard. |
| S02 | Dashboard page | Shows review-relevant requests. |
| S03 | Request row | Shows request summary, status, created date and applicant/request/client summary. |
| S04 | Review marker | Distinguishes no review / current Employee started / another Employee started. |
| S05 | Empty state | Shows no review-relevant requests if list is empty. |
| S06 | Error state | Shows safe read error if dashboard cannot load. |
| S07 | Row navigation | Opens Employee request details for selected request. |

## 7. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/employee/dashboard/EmployeeDashboardPage.tsx
or pages/employee/requests/EmployeeRequestsDashboardPage.tsx

Lives here:
  EmployeeDashboardPage

Uses:
  useSession() or future useEmployeeSession()
  useEmployeeRequestDashboardQuery({ status? })

Owns:
  route/page composition
  Employee auth/session branch
  dashboard loading/error/empty/success branches
  optional status filter state if included
  navigation to employee request details

Does not own:
  low-level HTTP
  generated DTO aliases
  row/card display internals
  review command actions
```

```text
        ↓

[Entity Query Layer]
entities/employee-request/model/useEmployeeRequestDashboardQuery.ts
entities/employee-request/model/employeeRequestQueryKeys.ts
entities/employee-request/model/employeeRequestTypes.ts

Lives here:
  useEmployeeRequestDashboardQuery()
  employeeRequestQueryKeys.dashboard(...)
  EmployeeDashboardRequestSummary
  EmployeeDashboardReviewState

Uses:
  listEmployeeDashboardRequests()

Owns:
  React Query read hook
  dashboard query key
  entity read type aliases
  read params shape, e.g. status filter if supported

Does not own:
  route/session branch
  visual table/card layout
  review command mutation
```

```text
        ↓

[Entity API Layer]
entities/employee-request/api/listEmployeeDashboardRequests.ts

Lives here:
  listEmployeeDashboardRequests(params)

Uses:
  shared API wrapper from shared/api/employeeRequestApi.ts
  or server package-specific API file chosen by backend contract

Owns:
  entity-level Employee dashboard read operation

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

Lives here after server contract exists:
  listEmployeeDashboardRequests(params)

Possible endpoint direction:
  GET /api/employee/requests

Uses:
  fetchJson()
  generated OpenAPI types

Owns:
  low-level HTTP GET
  generated response type alias
  query string mapping for supported filters

Does not own:
  React Query
  page state
  row rendering
  review command behavior
```

```text
        ↓

[Generated Contract Layer]
shared/api/generated/openapi-types.ts

Lives here after server implementation/generation:
  dashboard list operation
  dashboard row DTO
  review state enum/string
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
entities/employee-request/ui/EmployeeRequestDashboardList.tsx
entities/employee-request/ui/EmployeeRequestDashboardRow.tsx
entities/employee-request/ui/EmployeeReviewStateBadge.tsx
entities/employee-request/ui/EmployeeRequestDashboardEmptyState.tsx

Lives here:
  EmployeeRequestDashboardList
  EmployeeRequestDashboardRow
  EmployeeReviewStateBadge
  EmployeeRequestDashboardEmptyState

Props:
  requests: EmployeeDashboardRequestSummary[]
  onOpenRequest(requestId)

Owns:
  read-only dashboard list/table layout
  row visible data
  review state badge text
  empty read state

Does not own:
  fetching
  route/session branch
  start review action
  approve/reject actions
```

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Route/page | Employee dashboard page renders dashboard read context. |
| I02 | Page | Page checks Employee session and renders loading/error/empty/success branches. |
| I03 | Entity query | Query loads employee-accessible dashboard request summaries. |
| I04 | Entity API | Entity read operation delegates to shared API wrapper. |
| I05 | Shared API | Wrapper calls server dashboard read endpoint. |
| I06 | Entity display UI | Dashboard list/row renders visible request data and review state marker. |
| I07 | Route/page | Selecting a row navigates to Employee request details. |

## 8. Client API / Server Contract

Runtime implementation is blocked until Employee dashboard server read endpoint and generated OpenAPI DTO exist.

Target endpoint direction follows `SL-EMP-REQ-001`:

```text
GET /api/employee/requests
```

Target response shape should be derived from SC-06 DATA and server slice, not treated as final client-invented contract:

```ts
type EmployeeDashboardRequestsResponse = {
  requests: EmployeeDashboardRequestSummaryDto[];
};

type EmployeeDashboardRequestSummaryDto = {
  requestId: number;
  displayNumber?: string | null;

  requestType: string;
  requestStatus: string;
  createdAt: string;

  requestSummary?: string | null;
  applicantSummary?: string | null;
  clientSummary?: string | null;

  reviewState: EmployeeDashboardReviewState;
  startedByEmployeeId?: number | null;
};

type EmployeeDashboardReviewState =
  | "NotStarted"
  | "StartedByCurrentEmployee"
  | "StartedByAnotherEmployee";
```

Status filter, if included in first server contract:

```ts
type EmployeeDashboardRequestsQuery = {
  status?: string;
};
```

Client usage after generated contract exists:

```text
shared/api/generated/openapi-types.ts
  generated structural DTO/operation contract

shared/api/employeeRequestApi.ts
  low-level GET wrapper and query string mapping

entities/employee-request/api/listEmployeeDashboardRequests.ts
  entity-level read operation

entities/employee-request/model/useEmployeeRequestDashboardQuery.ts
  React Query read hook used by page
```

Contract rules:

```text
- Client must not send employeeId.
- Server derives Employee context from auth/session.
- Server enforces Employee access.
- Client uses generated OpenAPI types.
- Client must not handwrite DTO shape once generated types exist.
```

Important DTO note:

```text
reviewState is a UI/read-model field, not a client-derived guess.

Preferred DTO:
  server returns direct dashboard reviewState:
    NotStarted
    StartedByCurrentEmployee
    StartedByAnotherEmployee

If server instead returns raw review status + startedByEmployeeId,
client mapping must be explicitly planned and must receive enough
current-Employee context to avoid guessing.
```

## 9. Cross-Cutting Concerns / Considerations

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session/account context | yes | Employee dashboard requires Employee-authenticated context. |
| Authorization/visibility | yes | Server owns employee-visible request filtering; client only renders returned data. |
| Antiforgery / unsafe requests | no | Dashboard read is safe GET by default and should not require local CSRF logic. |
| Request validation / ProblemDetails | maybe | Status filter validation is server-owned if filter exists. |
| OpenAPI / generated artifacts | yes | Use generated OpenAPI types after server contract exists. |
| Generated constants/error codes | maybe | Review-state/status constants may be generated later. |
| Transaction / atomicity | no | Read-only. |
| No-mutation safety | yes | Read should not create or update Review. |
| Idempotency / retry | no | GET is repeatable. |
| Concurrency / stale state | yes | Review state may change after dashboard load; commands must re-check. |
| File/document boundary | no | No documents in dashboard. |
| Clock/audit actor fields | read only | Reads existing timestamps only. |
| Privacy / cross-account data exposure | yes | Do not expose Employee dashboard data to Client account sessions. |
| Client feedback / accessibility | yes | Dashboard states and badges should be clear and accessible. |
| Testing responsibility split | yes | Component/client tests for visible UI; E2E visible path; no React Query internals in E2E. |

CSRF rule:

```text
Safe/read requests do not require request token by default.
Future review commands are unsafe and consume shared antiforgery behavior.
```

## 10. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-L2-EMP-DASH-CLIENT-001` | blocked | What is the exact server endpoint and generated DTO? | Use `SL-EMP-REQ-001` server slice/OpenAPI once available. Current DTO sketch is derived from SC-06 DATA only. | API wrapper and tests. |
| `Q-L2-EMP-DASH-CLIENT-002` | accepted | Is this read or command sidecar? | Read sidecar. Review actions are future command sidecars. | Placement in `pages + entities`. |
| `Q-L2-EMP-DASH-CLIENT-003` | accepted | Which terminology should UI use? | Employee, not Worker. | Labels/copy/tests. |
| `Q-L2-EMP-DASH-CLIENT-004` | accepted | Does dashboard include start/approve/reject actions? | No. Dashboard read only; actions belong to SC-07B sidecars/details. | Scope boundary. |
| `Q-L2-EMP-DASH-CLIENT-005` | assumption | First route path? | Candidate: `/employee/requests` or `/employee/dashboard`; final path follows routing decision. | Route setup. |
| `Q-L2-EMP-DASH-CLIENT-006` | accepted | Which filter is in first pass? | Status only, if server supports it. Other filters future. | UI scope. |
| `Q-L2-EMP-DASH-CLIENT-007` | accepted | Row navigation target? | Employee request details page, future `SC-07A.client`. | Link/handoff. |
| `Q-L2-EMP-DASH-CLIENT-008` | assumption | Should client derive reviewState from raw review fields? | Prefer server returns dashboard-ready `reviewState`; client should not guess employee-relative state unless contract explicitly provides required fields. | DTO design, UI mapping, tests. |

## 11. Extension / Change Points

```text
- Employee request details page -> SC-07A.client;
- Start review action -> SC-07B start-review command sidecar;
- Approve/reject actions -> SC-07B decision command sidecars;
- assignment/queue policy -> future employee queue slice;
- department/permission model -> future auth/authorization slice;
- richer filters/search/priority/date -> future dashboard refinement;
- agreement proposal exchange -> SC-13*.
```

## 12. Behavior Coverage

| Source behavior item | How client sidecar covers it | Status |
|---|---|---|
| `L2-EMP-DASH-001` | Dashboard page lists employee-accessible / review-relevant requests returned by server. | covered by read UI + server contract |
| `L2-EMP-DASH-002` | Row badge distinguishes not-started, started-by-current-Employee, started-by-another-Employee. | covered after DTO contract |
| `L2-EMP-DASH-003` | UI labels use Employee terminology, not Worker. | covered |
| `EMP-READ-001` | Dashboard renders employee-accessible request rows; server owns access enforcement. | covered at UI boundary / server-dependent |
| Unauthorized employee context cannot access protected data/actions | Client shows auth/access state; server owns enforcement. | server-dependent |

Not behavior coverage:

```text
- React Query cache key exists;
- endpoint string exists;
- generated type exists;
- mocks were called;
- start/approve/reject buttons exist.
```

## 13. Client / Component / E2E Verification Plan

### Component/client tests

| Test / check | Verifies |
|---|---|
| Employee dashboard page renders for Employee session | Read entry point visible |
| Non-Employee / signed-out branch renders safe state | Access boundary UI |
| Loading state renders while dashboard query is pending | Pending read state |
| Error state renders when dashboard query fails | Safe read failure UI |
| Empty state renders when no requests are returned | Empty dashboard is understandable |
| Request rows render id/type/status/created/applicant/request/client summary | Row visible data |
| Not-started review marker renders | Review state marker |
| Started-by-current-Employee marker renders | Review state marker |
| Started-by-another-Employee marker renders | Review state marker |
| Status filter updates query params if included | First-pass filter behavior |
| Row/details link uses Employee details route | Navigation handoff |
| No start/approve/reject command controls render in dashboard slice | Command scope boundary |

### Shared API / entity tests

| Test / check | Verifies |
|---|---|
| `listEmployeeDashboardRequests()` calls server dashboard endpoint | Correct API wrapper |
| Status filter maps to query string only if supported | Contract-safe filtering |
| Entity API delegates to shared API wrapper | Layering |
| Query hook uses dashboard query key | Cache identity |
| DTO aliases come from generated OpenAPI types | No handwritten contract drift |

### E2E happy read path

```text
setup Employee session
        ↓
setup review-relevant requests:
  no review started
  review started by current Employee
  review started by another Employee
        ↓
open Employee dashboard
        ↓
assert request rows are visible
        ↓
assert each review state marker is visible
        ↓
open one request details link
        ↓
assert Employee request details route/page opens
```

### E2E empty path

```text
setup Employee session with no review-relevant requests
        ↓
open Employee dashboard
        ↓
assert empty dashboard state is visible
```

### E2E access path

```text
open Employee dashboard without Employee session
        ↓
assert signed-out/access-required state is visible
```

### Explicit non-goals for tests

```text
Do not test start review command here.
Do not test approve/reject commands here.
Do not assert React Query cache internals in E2E.
Do not assert backend authorization internals in E2E.
Do not test CSRF behavior in this read sidecar.
```

## 14. Suggested File Placement

```text
src/pages/employee/dashboard/
  EmployeeDashboardPage.tsx
  employeeDashboardPage.css

src/entities/employee-request/api/
  listEmployeeDashboardRequests.ts

src/entities/employee-request/model/
  employeeRequestQueryKeys.ts
  employeeRequestTypes.ts
  useEmployeeRequestDashboardQuery.ts

src/entities/employee-request/ui/
  EmployeeRequestDashboardList.tsx
  EmployeeRequestDashboardRow.tsx
  EmployeeReviewStateBadge.tsx
  EmployeeRequestDashboardEmptyState.tsx
  employeeRequestDashboardConst.ts
  employeeRequestDashboard.css

src/shared/api/
  employeeRequestApi.ts
  api path file chosen by server/client convention

src/shared/config/
  clientRoutes.ts

src/app/router/
  router.tsx

tests/e2e/employee/
  employee-request-dashboard.spec.ts
```

## 15. Implementation Checklist

```text
[ ] Confirm exact server endpoint and generated DTO names from SL-EMP-REQ-001 implementation.
[ ] Add Employee dashboard route.
[ ] Add Employee dashboard page.
[ ] Add entity API operation.
[ ] Add shared API wrapper once generated contract exists.
[ ] Add entity query hook and query keys.
[ ] Add entity display components.
[ ] Add review-state badge.
[ ] Add loading/empty/error branches.
[ ] Add row/details navigation.
[ ] Add optional status filter only if server contract supports it.
[ ] Add component/client tests.
[ ] Add E2E read/empty/access paths when Employee test setup exists.
[ ] Do not implement start/approve/reject actions.
[ ] Do not handwrite generated DTO shape once OpenAPI types exist.
```

## 16. Next Step

Runtime implementation is blocked until Employee dashboard server read endpoint and generated DTO are known.

Recommended order:

```text
1. Confirm or create Employee dashboard server read slice.
2. Generate OpenAPI/types.
3. Update this client draft with exact endpoint/DTO names.
4. Implement client route/page/entity query/shared API wrapper.
5. Add dashboard row UI and review-state badges.
6. Add component/shared API tests.
7. Add E2E read/empty/access paths once backend test setup supports Employee sessions.
```
