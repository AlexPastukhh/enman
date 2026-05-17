# L2-EMP-DETAILS-001.client — Employee Request Details

Status: full client read sidecar draft / details read server contract pending / API placement synchronized
Server owner: `SL-EMP-REQ-002 — Employee Request Details Read`
Scenario: `SC-07A — Employee Request Details`
Slice type: L2 client read sidecar
Architecture direction: read/details slice maps to `pages + entities`; review command actions stay in future `features`.

This full sidecar keeps `SL-EMP-REQ-002` as the server/details DTO owner and applies the current client API placement rule: read wrappers live in `entities/*/api`; command wrappers live in `features/*/api`; `shared/api` remains generic transport/generated infrastructure.

## 1. Scope

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
- optional future review action slot placement;
- navigation back to Employee dashboard;
- no command execution in this read sidecar.
```

## 2. Out of Scope

```text
- Employee request details server endpoint implementation -> SL-EMP-REQ-002;
- Start Review command -> future L2-REVIEW-START-001.client / SL-EMP-REQ-003.client;
- Approve Review command -> future approve client sidecar;
- Reject Review command / rejection feedback -> future reject client sidecar;
- Employee request dashboard/list implementation -> L2-EMP-DASH-001.client;
- proposal/agreement exchange after approval -> future agreement slices;
- domain review persistence/API details;
- local CSRF mechanics;
- manual generated OpenAPI/type edits;
- business-specific wrappers in shared/api.
```

## 3. Related Slices / Owners

```text
SL-EMP-REQ-001 — Employee Request List Read
  Owns employee dashboard/list read endpoint and compact row DTO.

L2-EMP-DASH-001.client
  Owns Employee request dashboard/list page.

SL-EMP-REQ-002 — Employee Request Details Read
  Owns employee request details read endpoint and details DTO.

L2-EMP-DETAILS-001.client
  Owns Employee request details route/page and read UI.

SL-EMP-REQ-003 — Start Request Review
  Owns backend StartReview command:
    POST /api/employee/requests/{requestId}/review/start.

L2-REVIEW-START-001.client
  Owns future Start Review button/mutation/action feedback.

Future approve/reject sidecars
  Own approve/reject command buttons/forms/mutations.

CC-CSRF-001
  Owns antiforgery token/session context for unsafe browser requests.
```

## 4. Visual UI / Scenario Flow

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
[Optional Future Action Slot]
future command sidecars may render Start / Approve / Reject controls
        ↓
Employee can return to dashboard
```

In ordinary words:

An Employee opens a request details page. The page shows the request data, applicant/client data and review state needed to understand what can happen next. This sidecar does not start, approve or reject review. It only shows whether those actions should be available or blocked, and provides an optional future action slot where command sidecars can render their own feature-owned buttons/forms.

Scenario flow table:

| Step | UI / Scenario layer      | User-visible responsibility                                                        |
| ---- | ------------------------ | ---------------------------------------------------------------------------------- |
| S01  | Employee                 | Opens request details from dashboard/details link.                                 |
| S02  | Details page             | Shows request status and request data.                                             |
| S03  | Applicant/client area    | Shows applicant/client data needed for review.                                     |
| S04  | Review state area        | Shows no review / current Employee started / another Employee started / completed. |
| S05  | Action availability area | Shows which review actions are available or blocked.                               |
| S06  | Optional action slot     | Future feature sidecars may render command controls.                               |
| S07  | Not-found/access state   | Shows safe state if request is missing or not accessible.                          |
| S08  | Navigation               | Allows returning to Employee dashboard.                                            |

## 5. Visual Client Implementation Flow

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
  placing optional future review action slots

Does not own:
  low-level HTTP
  generated DTO aliases
  request details card internals
  review command mutations
  business endpoint wrapper
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
  low-level fetchJson
```

```text
        ↓

[Entity API Layer]
entities/employee-request/api/getEmployeeRequestDetails.ts
entities/employee-request/api/employeeRequestApiTypes.ts

Lives here:
  getEmployeeRequestDetails(requestId)
  EmployeeRequestDetailsResponse alias
  EmployeeRequestDetailsDto alias
  EmployeeRequestReviewState alias if generated
  EmployeeReviewActionAvailabilityDto alias if generated

Uses:
  shared/api/fetchJson.ts
  shared/api/generated/openapi-types.ts

Owns:
  entity-owned read endpoint wrapper
  local generated DTO aliases near EmployeeRequest entity
  requestId path mapping

Does not own:
  React Query
  route/session branch
  UI rendering
  shared business endpoint dump
```

```text
        ↓

[Shared API Infrastructure Layer]
shared/api/fetchJson.ts
shared/api/generated/openapi-types.ts
shared/api/ApiError / ProblemDetails helpers
shared/api/antiforgeryTokenStore.ts, if present

Owns:
  generic request execution
  generic error parsing
  generated OpenAPI type source
  CSRF/token infrastructure for unsafe requests

Does not own:
  getEmployeeRequestDetails()
  listEmployeeDashboardRequests()
  startRequestReview()
  employee-request DTO aliases
  business-specific API functions
```

```text
        ↓

[Generated Contract Layer]
shared/api/generated/openapi-types.ts

Lives here after SL-EMP-REQ-002 implementation/generation:
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
entities/employee-request/ui/EmployeeRequestDetailsEmptyState.tsx

Lives here:
  EmployeeRequestDetailsView
  EmployeeRequestStatusPanel
  EmployeeApplicantReviewData
  EmployeeReviewStatePanel
  EmployeeReviewActionAvailabilityPanel
  EmployeeRequestDetailsEmptyState

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

[Future Command Feature Layers — not this sidecar]
features/employee-request/start-review/api/startRequestReview.ts
features/employee-request/approve-review/api/approveRequestReview.ts
features/employee-request/reject-review/api/rejectRequestReview.ts

Own:
  command endpoint wrappers;
  command DTO aliases;
  mutations;
  buttons/forms;
  pending/error/success feedback.

Not part of this read sidecar.
```

Implementation flow table:

| Step | Layer              | Responsibility                                                                     |
| ---- | ------------------ | ---------------------------------------------------------------------------------- |
| I01  | Route/page         | Employee details page parses request id and renders details read context.          |
| I02  | Page               | Page checks Employee session and renders loading/error/not-found/success branches. |
| I03  | Entity query       | Query loads employee-accessible request details.                                   |
| I04  | Entity API         | Entity read operation calls `fetchJson` directly through entity-owned wrapper.     |
| I05  | Shared API infra   | `fetchJson` performs generic request/error handling.                               |
| I06  | Generated contract | Generated OpenAPI types provide DTO/operation structure.                           |
| I07  | Entity display UI  | Details view renders request/applicant/review state data.                          |
| I08  | Entity display UI  | Action availability panel shows which review actions are available/blocked.        |
| I09  | Entity display UI  | Optional action slot allows future command feature buttons.                        |
| I10  | Page               | Page allows navigation back to Employee dashboard.                                 |

## 6. Client API / Server Contract

Runtime implementation is blocked until `SL-EMP-REQ-002 — Employee Request Details Read` endpoint and generated DTO exist.

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

After generation, local aliases should live under the owning entity:

```ts
// entities/employee-request/api/employeeRequestApiTypes.ts
import type { components } from "../../../shared/api/generated/openapi-types";

export type EmployeeRequestDetailsResponse =
  components["schemas"]["EmployeeRequestDetailsResponseDto"];

export type EmployeeRequestDetailsDto =
  components["schemas"]["EmployeeRequestDetailsDto"];
```

Entity read wrapper placement:

```ts
// entities/employee-request/api/getEmployeeRequestDetails.ts
import { fetchJson } from "../../../shared/api/fetchJson";
import type { EmployeeRequestDetailsResponse } from "./employeeRequestApiTypes";

export const getEmployeeRequestDetails = (
  requestId: number,
): Promise<EmployeeRequestDetailsResponse> =>
  fetchJson<EmployeeRequestDetailsResponse>(
    `/api/employee/requests/${encodeURIComponent(String(requestId))}`,
  );
```

Important API ownership rule:

```text
Do not add:
  shared/api/employeeRequestApi.ts

Do add:
  entities/employee-request/api/getEmployeeRequestDetails.ts
  entities/employee-request/api/employeeRequestApiTypes.ts
```

Known future command contract from `SL-EMP-REQ-003`:

```text
POST /api/employee/requests/{requestId}/review/start
```

But that belongs to future command feature placement:

```text
features/employee-request/start-review/api/startRequestReview.ts
features/employee-request/start-review/api/startReviewApiTypes.ts
```

Not here.

## 7. Cross-Cutting Concerns

```text
Auth/session:
  Employee details requires Employee-authenticated context.
  Client account sessions must not see Employee request details.

API/generated contract:
  Use generated OpenAPI types after SL-EMP-REQ-002 exists.
  Do not manually edit generated artifacts.
  Entity read wrapper imports generated types directly and exposes local aliases.

API placement:
  Read endpoint wrapper lives in entities/employee-request/api.
  Command endpoint wrappers live in features/employee-request/*/api.
  shared/api remains generic infrastructure only.

ProblemDetails/error mapping:
  404/missing request -> safe not-found state.
  401/403 -> auth/access state.
  Other read failures -> page-level error.

Antiforgery/CSRF:
  Details read is safe GET by default and should not require local CSRF logic.
  Future StartReview/Approve/Reject commands are unsafe and must consume shared antiforgery behavior.

Terminology:
  Use Employee, not Worker.

E2E:
  Assert visible details/review/action availability.
  Do not assert React Query cache internals or backend ownership internals.
```

The new architecture policy explicitly says read wrappers belong in `entities/*/api`, command wrappers belong in `features/*/api`, and `shared/api` must stay generic infrastructure. 

## 8. Questions / Decisions

### Blocked / unresolved

| ID                            | Status  | Question                                               | Current direction                                                                                   | Impact                        |
| ----------------------------- | ------- | ------------------------------------------------------ | --------------------------------------------------------------------------------------------------- | ----------------------------- |
| `Q-L2-EMP-DETAILS-CLIENT-001` | blocked | What is exact details read endpoint and generated DTO? | Use `SL-EMP-REQ-002` server/OpenAPI once available. Current DTO sketch is derived from SC-07A only. | API wrapper and tests.        |
| `Q-L2-EMP-DETAILS-CLIENT-002` | blocked | What are exact generated operation/type names?         | Use generated OpenAPI after server implementation.                                                  | `employeeRequestApiTypes.ts`. |

### Assumptions / current direction

| ID                            | Status     | Question                                       | Current direction                                                                                                            | Impact                |
| ----------------------------- | ---------- | ---------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------- | --------------------- |
| `Q-L2-EMP-DETAILS-CLIENT-003` | assumption | First route path?                              | Candidate: `/employee/requests/:requestId`; final path follows server/client route decision.                                 | Route setup.          |
| `Q-L2-EMP-DETAILS-CLIENT-004` | assumption | Should action availability be server-provided? | Prefer server-provided action availability. Client should not guess unless contract explicitly provides all required fields. | DTO design and tests. |

### Accepted directions

| ID                            | Status   | Question                                         | Current direction                                             | Impact                           |
| ----------------------------- | -------- | ------------------------------------------------ | ------------------------------------------------------------- | -------------------------------- |
| `Q-L2-EMP-DETAILS-CLIENT-005` | accepted | Is this read or command sidecar?                 | Read sidecar. Review commands are future feature sidecars.    | Placement in `pages + entities`. |
| `Q-L2-EMP-DETAILS-CLIENT-006` | accepted | Does this sidecar execute start/approve/reject?  | No. It only shows action availability / slots.                | Scope boundary.                  |
| `Q-L2-EMP-DETAILS-CLIENT-007` | accepted | Where does details endpoint wrapper live?        | `entities/employee-request/api/getEmployeeRequestDetails.ts`. | New API ownership policy.        |
| `Q-L2-EMP-DETAILS-CLIENT-008` | accepted | Can we add `shared/api/employeeRequestApi.ts`?   | No. `shared/api` is generic infrastructure only.              | Prevents shared API dump.        |
| `Q-L2-EMP-DETAILS-CLIENT-009` | accepted | Does details include documents/review history?   | No, future refinement.                                        | Scope boundary.                  |
| `Q-L2-EMP-DETAILS-CLIENT-010` | accepted | Can StartReview response be used as details DTO? | No. It is compact command result only.                        | Prevents DTO misuse.             |

## 9. Extension / Change Points

```text
- Employee dashboard page -> L2-EMP-DASH-001.client / SL-EMP-REQ-001.client;
- Start review action -> L2-REVIEW-START-001.client;
- Approve action -> future approve client sidecar;
- Reject action / rejection feedback form -> future reject client sidecar;
- documents / verification result / review history -> future details refinement;
- assignment / lock -> future employee assignment/queue slice;
- proposal exchange after approval -> SC-13*;
- API placement cleanup for older slices that still use shared/api business wrappers.
```

## 10. Behavior Coverage

| Source behavior item / scenario requirement                            | How client sidecar covers it                                        | Status                                    |
| ---------------------------------------------------------------------- | ------------------------------------------------------------------- | ----------------------------------------- |
| Employee opens request details and sees request details/applicant data | Details page renders request data and applicant/client review data. | covered after DTO contract                |
| Details show review state                                              | Review state panel renders server-provided state.                   | covered after DTO contract                |
| Actions enabled/disabled according to domain state                     | Action availability panel renders server-provided availability.     | covered as read visibility                |
| Started by another Employee blocks actions                             | UI shows actions blocked/unavailable.                               | covered as read visibility                |
| Approve/reject require started review                                  | UI shows approve/reject unavailable when no started review.         | covered as read visibility                |
| Protected employee-accessible request data                             | Client shows returned details; server owns access enforcement.      | covered at UI boundary / server-dependent |
| Approved request cannot be reviewed again                              | UI displays completed/blocked state if server DTO says so.          | related/dependent                         |
| StartReview command starts review                                      | Not covered here; future command sidecar.                           | out of scope                              |

Not behavior coverage:

```text
- React Query cache key exists;
- endpoint string exists;
- generated type exists;
- mocks were called;
- StartReview command button executes command;
- backend domain methods were called.
```

## 11. Client / Component / E2E Verification Plan

### Component/client tests

| Test / check                                                         | Verifies                         |
| -------------------------------------------------------------------- | -------------------------------- |
| Employee details page renders for Employee session                   | Read entry point visible         |
| Non-Employee / signed-out branch renders safe state                  | Access boundary UI               |
| Loading state renders while details query is pending                 | Pending read state               |
| Not-found state renders for missing request                          | Safe missing details state       |
| Access/forbidden state renders for inaccessible request              | Safe authorization UI            |
| Error state renders for unexpected read failure                      | Safe read failure UI             |
| Request status/details/address render                                | Request visible data             |
| Applicant/client review data renders                                 | Review-relevant data             |
| Not-started review state renders                                     | Review state display             |
| Started-by-current-Employee review state renders                     | Review state display             |
| Started-by-another-Employee review state renders                     | Review state display             |
| Approved/rejected completed states render                            | Completed review state display   |
| Start availability renders when no review + InReview                 | Action availability visibility   |
| Approve/reject availability renders when started by current Employee | Action availability visibility   |
| Actions blocked when started by another Employee                     | Action availability visibility   |
| Optional review action slot can render external content              | Future command composition point |
| No review command mutation is called by this sidecar                 | Command scope boundary           |

### Entity API / query tests

| Test / check                                                           | Verifies                       |
| ---------------------------------------------------------------------- | ------------------------------ |
| `getEmployeeRequestDetails(requestId)` calls server details endpoint   | Correct entity-owned wrapper   |
| Entity API imports `fetchJson`, not business wrapper from `shared/api` | New API placement rule         |
| Entity API aliases generated DTOs near `employee-request` entity       | Local ownership of DTO aliases |
| Query hook uses details query key                                      | Cache identity                 |
| Query hook is disabled for invalid/missing request id                  | Safe param handling            |
| 404/403/ProblemDetails map to page states                              | Safe error mapping             |

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
Do not add shared/api business wrapper tests.
```

## 12. Suggested file placement

```text
src/pages/employee/requests/details/
  EmployeeRequestDetailsPage.tsx
  employeeRequestDetailsPage.css

src/entities/employee-request/api/
  getEmployeeRequestDetails.ts
  employeeRequestApiTypes.ts

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
  fetchJson.ts
  generated/openapi-types.ts
  existing generic ProblemDetails/ApiError helpers only

src/shared/config/
  clientRoutes.ts

src/app/router/
  router.tsx

tests/e2e/employee/
  employee-request-details.spec.ts
```

Do **not** add:

```text
src/shared/api/employeeRequestApi.ts
```

## 13. Next Step

Runtime implementation is blocked until `SL-EMP-REQ-002 — Employee Request Details Read` endpoint and generated DTO are known.

Recommended order:

```text
1. Confirm or implement SL-EMP-REQ-002 server read endpoint.
2. Run OpenAPI/type generation.
3. Update this client draft with exact generated operation/type names.
4. Implement entity-owned API wrapper:
   entities/employee-request/api/getEmployeeRequestDetails.ts.
5. Implement entity DTO aliases:
   entities/employee-request/api/employeeRequestApiTypes.ts.
6. Implement entity query/model.
7. Implement page and entity display panels.
8. Add component/entity API/query tests.
9. Add E2E read/access paths once backend test setup supports Employee sessions.
10. Implement StartReview separately in features/employee-request/start-review.
```
