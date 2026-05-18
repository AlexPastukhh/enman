# L2-EMP-DASH-001.client — Employee Request Dashboard

Status: implemented client-sidecar draft refactor / implementation evidence inspected read-only / runtime not changed  
Parent server slice: `SL-EMP-REQ-001 — Employee Request List Read`  
Host surface: Employee request dashboard route/page  
Actor: Employee  
Slice type: client read sidecar with command action slot  
Placement: `pages/employee/requests/dashboard`

Refactor note:

```text
This draft was refactored as a docs-only implemented-slice sync pass.

Runtime implementation was inspected read-only for current page, query, API wrapper, filters, widget and tests.
Runtime code, tests and generated artifacts were not changed in this archive.
Deep UI redesign and page-flow/redirect audit are out of scope for this pass.
```

---

## 0. Scenario Sources

Business scenario:

```text
SC-06 — Employee Request Dashboard
```

Related scenarios:

```text
SC-07A — Employee Request Details
SC-07B — Employee Request Review Actions
```

Server source:

```text
SL-EMP-REQ-001 — Employee Request List Read
```

UI scenario:

```text
missing / pending dedicated UI source for Employee request dashboard page layout;
current implemented page/widget/filter behavior is used as read-only evidence.
```

Cross-cutting behavior:

```text
CC-CLIENT-FEEDBACK-001 — Client Error / Feedback Visibility
CC-CLIENT-FORM-VALIDATION-001 — only for URL/filter validation shape, not server security
```

Data source:

```text
EmployeeRequestListResponseDto / EmployeeRequestListItemDto generated types via OpenAPI;
client aliases in entities/employee-request/api/employeeRequestApiTypes.ts.
```

Behavior items:

```text
stable source behavior item IDs are pending source registry;
this draft uses provisional behavior names and current implementation evidence.
```

Concern umbrella:

```text
none for this client read sidecar.
```

---

## 0.1 Source / Domain / Slice Coverage Snapshot

Source versions:

```text
SC-06: pending / v000 if source registry is applied
SC-07A: pending / v000 if source registry is applied
SC-07B: pending / v000 if source registry is applied
SL-EMP-REQ-001: paired server draft in this archive
```

Server/domain baseline:

```text
server endpoint returns compact Employee request dashboard rows;
server derives reviewState relative to current Employee;
client-side filters are UX/navigation state and server validates query values.
```

Slice derivation map:

```text
pending / add or update row for L2-EMP-DASH-001.client during source-sync map update.
```

Coverage snapshot:

| Behavior item / provisional behavior | Source version | Server/domain disposition | This client slice responsibility | Notes |
|---|---|---|---|---|
| Employee opens dashboard | SC-06 pending | server endpoint exists | render Employee dashboard page shell | route evidence uses `/employee/requests` in tests |
| Non-signed-in user sees sign-in state | SC-06 pending | server would return 401 | page shows sign-in required state before query | uses session state |
| Non-Employee user sees access denied | SC-06 pending | server returns 403 | page shows access denied and disables query | uses session role check |
| Employee dashboard list loads | SC-06 pending | server returns `EmployeeRequestListResponseDto` | use entity query hook and list widget | `useEmployeeRequestDashboardQuery` |
| Status/reviewState filters are visible | SC-06 pending | server validates filters | page owns filter form and URL serialization | invalid URL filters show reset state |
| Invalid URL filters do not query server | SC-06 pending | server also validates | client parser blocks query and shows reset message | UX guard only |
| Empty dashboard state visible | SC-06 pending | server can return empty list | list widget renders empty state | supports default/filtered variants |
| Row summary is visible | SC-06 pending | server returns compact row fields | list/row components render request info and review badge | details link goes to employee request details |
| Row opens details | SC-07A pending | details endpoint/page owns full read | row link navigates to `/employee/requests/{requestId}` | route names follow current client evidence |
| StartReview action can be hosted | SC-07B pending | command slice owns mutation/security | page passes action slot for NotStarted/InReview rows only | action feature owned by `L2-REVIEW-START-001.client` |
| Approve/reject are not dashboard read responsibilities | SC-07B pending | command slices own actions | dashboard does not own approve/reject command forms | details/action sidecars own them |

---

## 0.2 Implementation Sync Status

Implementation status:

```text
implemented-needs-doc-sync
```

Implemented files inspected read-only:

```text
client page:
  energymanagement.client/src/pages/employee/requests/dashboard/EmployeeDashboardPage.tsx
  energymanagement.client/src/pages/employee/requests/dashboard/EmployeeRequestDashboardFilters.tsx
  energymanagement.client/src/pages/employee/requests/dashboard/model/employeeDashboardUrlFilters.ts
  energymanagement.client/src/pages/employee/requests/dashboard/employeeDashboardPage.css

entity API/model:
  energymanagement.client/src/entities/employee-request/api/listEmployeeDashboardRequests.ts
  energymanagement.client/src/entities/employee-request/api/employeeRequestApiTypes.ts
  energymanagement.client/src/entities/employee-request/model/employeeRequestFilters.ts
  energymanagement.client/src/entities/employee-request/model/employeeRequestQueryKeys.ts
  energymanagement.client/src/entities/employee-request/model/useEmployeeRequestDashboardQuery.ts

entity UI:
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardList.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardRow.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardEmptyState.tsx
  energymanagement.client/src/entities/employee-request/ui/EmployeeReviewStateBadge.tsx
  energymanagement.client/src/entities/employee-request/ui/employeeRequestDashboard.css
  energymanagement.client/src/entities/employee-request/ui/employeeRequestDashboardConst.ts

feature action hosted but not owned:
  energymanagement.client/src/features/employee-request/start-review/ui/StartReviewButton.tsx

client tests:
  energymanagement.client/src/entities/employee-request/api/listEmployeeDashboardRequests.test.ts
  energymanagement.client/src/entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx
  energymanagement.client/src/pages/employee/requests/dashboard/EmployeeDashboardPage.test.tsx
  energymanagement.client/src/pages/employee/requests/dashboard/EmployeeRequestDashboardFilters.test.tsx
  energymanagement.client/src/pages/employee/requests/dashboard/model/employeeDashboardUrlFilters.test.ts

server:
  paired server draft refactored in this archive:
    planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
```

Checked against:

```text
source versions:
  pending source-sync registry

server contract:
  current generated/client type aliases and API wrapper evidence inspected read-only

slice derivation map version:
  pending
```

Known drift:

```text
docs:
  - old client draft was short and lacked current template sections;
  - old client draft lacked Scenario Sources;
  - old client draft lacked Source / Domain / Slice Coverage Snapshot;
  - old client draft lacked Implementation Sync Status;
  - old client draft lacked Behavior-to-Test Trace;
  - old client draft did not record actual page/query/API/widget/test evidence.

implementation:
  - current implementation evidence inspected read-only;
  - tests were not executed in this archive;
  - StartReview button is hosted from dashboard rows but command behavior remains a separate sidecar.

UI:
  - no runtime UI/CSS/page-flow changes in this archive;
  - deep UI redesign is out of scope.
```

Last sync note:

```text
Docs-only refactor using uploaded repo zip as read-only implementation evidence.
No runtime code, tests or generated artifacts changed.
```

---

## 1. Scope

This client sidecar owns:

```text
- Employee dashboard route/page shell;
- session/role-gated visible states;
- URL-backed status and reviewState filters;
- invalid filter visible state and reset action;
- entity API wrapper for Employee request list;
- entity query hook and query key for dashboard list;
- dashboard list widget;
- dashboard row summary rendering;
- empty/loading/error/success states;
- filtered empty state and reset action;
- details navigation from row;
- StartReview row action slot hosting for NotStarted/InReview rows;
- no start/approve/reject mutation ownership inside the read sidecar.
```

Current route/page evidence:

```text
pages/employee/requests/dashboard/EmployeeDashboardPage.tsx
```

Current test route evidence:

```text
/employee/requests
```

Current details link evidence:

```text
/employee/requests/{requestId}
```

---

## 2. Out of Scope

```text
- server endpoint implementation -> SL-EMP-REQ-001;
- Employee request details server endpoint -> SL-EMP-REQ-002;
- Employee request details page -> L2-EMP-DETAILS-001.client;
- StartReview command implementation -> SL-EMP-REQ-003 / L2-REVIEW-START-001.client;
- ApproveReview command implementation -> SL-EMP-REQ-004 / approve sidecar;
- RejectReview command implementation -> SL-EMP-REQ-005 / reject sidecar;
- AgreementProposalExchange behavior after approval;
- local CSRF mechanics;
- manual generated OpenAPI/type edits;
- deep UI redesign;
- page-flow/redirect audit.
```

Important:

```text
Dashboard can host command action slots, but command features own their own mutations, security assumptions, pending/error states and cache invalidation.
```

---

## 3. Related Slices / Owners

```text
SL-EMP-REQ-001
  Owns server/API Employee request list endpoint and compact row DTO.

L2-EMP-DASH-001.client
  Owns Employee dashboard page, URL filters, query, list widget and visible read states.

SL-EMP-REQ-002 / L2-EMP-DETAILS-001.client
  Own request details read and details page.

SL-EMP-REQ-003 / L2-REVIEW-START-001.client
  Own StartReview command and StartReviewButton behavior.

SL-EMP-REQ-004 / approve sidecar
  Own ApproveReview command/action.

SL-EMP-REQ-005 / reject sidecar
  Own RejectReview command/action.
```

---

## 4. Visual UI / Scenario Flow

```text
[Visitor]
opens Employee dashboard route
        ↓
[No session]
page shows sign-in required state


[Signed-in non-Employee]
opens Employee dashboard route
        ↓
page shows access denied state


[Signed-in Employee]
opens Employee dashboard route
        ↓
page parses URL filters
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ filters valid                │ filters invalid              │
 ▼                              ▼
query dashboard list            show invalid filter message
        ↓                       and reset action
loading / error / success
        ↓
EmployeeRequestDashboardList
        ↓
row summaries + details links
        ↓
optional StartReview action slot for NotStarted/InReview rows
```

Scenario flow table:

| Step | Layer | Responsibility |
|---|---|---|
| S01 | Page shell | Shows sign-in/access states based on session role. |
| S02 | URL filter parser | Reads `status` and `reviewState` query params. |
| S03 | Filter UI | Lets Employee change filters and reset them. |
| S04 | Query hook | Calls server list endpoint when Employee session and filters are valid. |
| S05 | List widget | Renders empty/loading/error/success row states. |
| S06 | Row | Shows compact request row and details link. |
| S07 | Action slot | Hosts StartReview button only for current implemented dashboard condition. |

---

## 5. Visual Layout / Screen Composition

Current implemented composition direction:

```text
[Header]
main.content
  section.employeeDashboardPage aria-labelledby="employee-dashboard-heading"
    h1 Employee dashboard title
    page description

    Sign-in required state, when no session
    Access denied state, when non-Employee

    EmployeeRequestDashboardFilters, when Employee

    Invalid filters state, when URL filters invalid
    Loading state, when query pending
    Error state, when query failed

    EmployeeRequestDashboardList, when data loaded
      EmployeeRequestDashboardRow[]
        row summary
        details link
        optional row action slot
[Footer]
```

Visible states:

```text
sign-in required;
access denied;
invalid filters;
loading;
error;
empty default;
empty filtered;
success list.
```

CSS ownership:

```text
Page layout:
  pages/employee/requests/dashboard/employeeDashboardPage.css

Entity dashboard list/rows/empty state:
  entities/employee-request/ui/employeeRequestDashboard.css

Feature StartReview button styling:
  features/employee-request/start-review/ui/startReviewButton.css
```

---

## 6. Visual Client Implementation Flow

### Employee Dashboard Page Shell

```text
EmployeeDashboardPage
  from: pages/employee/requests/dashboard/EmployeeDashboardPage.tsx
  needed to: compose the Employee dashboard route with session states, filters, dashboard query and list widget.
  visual: page-level section with title, description, filters and list states inside main content.
```

Uses:

```text
useSession
  from: entities/session/model/useSession
  needed to: decide sign-in/access/Employee query enablement.

parseEmployeeDashboardUrlFilters / serializeEmployeeDashboardUrlFilters
  from: pages/employee/requests/dashboard/model/employeeDashboardUrlFilters.ts
  needed to: own URL filter parsing/serialization and invalid filter UX.

EmployeeRequestDashboardFilters
  from: pages/employee/requests/dashboard/EmployeeRequestDashboardFilters.tsx
  needed to: render status/reviewState filter controls and reset button.
  visual: filter form above dashboard list.

useEmployeeRequestDashboardQuery
  from: entities/employee-request/model/useEmployeeRequestDashboardQuery.ts
  needed to: load Employee request dashboard rows through the entity model layer.

EmployeeRequestDashboardList
  from: entities/employee-request/ui/EmployeeRequestDashboardList.tsx
  needed to: render dashboard rows and empty states.
  visual: request row list in the dashboard content area.

StartReviewButton
  from: features/employee-request/start-review/ui/StartReviewButton.tsx
  needed to: host command action slot for NotStarted/InReview rows.
  visual: row-level command action; command feature owns internal button behavior.
```

### Filter Component

```text
EmployeeRequestDashboardFilters
  from: pages/employee/requests/dashboard/EmployeeRequestDashboardFilters.tsx
  needed to: render dashboard filter controls and emit filter changes.
  visual: form with Status and Review state selects plus Reset button.
```

Uses:

```text
employeeRequestStatusOptions / employeeReviewStateOptions
  from: entities/employee-request/model/employeeRequestFilters.ts
  needed to: render current allowed filter options.
```

### Entity API Layer

```text
listEmployeeDashboardRequests
  from: entities/employee-request/api/listEmployeeDashboardRequests.ts
  needed to: wrap `GET /api/employee/requests` with optional status/reviewState query params.
```

Uses:

```text
fetchJson
  from: shared/api/fetchJson.ts
  needed to: execute generic GET request and parse response/errors.

EmployeeRequestListResponseDto / EmployeeRequestListQuery
  from: entities/employee-request/api/employeeRequestApiTypes.ts
  needed to: keep generated DTO aliases near employee-request entity.
```

### Entity Model Layer

```text
useEmployeeRequestDashboardQuery
  from: entities/employee-request/model/useEmployeeRequestDashboardQuery.ts
  needed to: own React Query loading/error/success state for dashboard list.

employeeRequestQueryKeys.dashboard(filters)
  from: entities/employee-request/model/employeeRequestQueryKeys.ts
  needed to: centralize dashboard query key and include normalized filters.

employeeRequestFilters
  from: entities/employee-request/model/employeeRequestFilters.ts
  needed to: validate URL filter strings and expose allowed filter options.
```

### Entity UI Layer

```text
EmployeeRequestDashboardList
  from: entities/employee-request/ui/EmployeeRequestDashboardList.tsx
  needed to: render rows or empty state.
  visual: list block with row cards/items and optional filtered empty reset.

EmployeeRequestDashboardRow
  from: entities/employee-request/ui/EmployeeRequestDashboardRow.tsx
  needed to: render one compact request row and details link.
  visual: row/card with request number, status/type/applicant/address/review state.

EmployeeReviewStateBadge
  from: entities/employee-request/ui/EmployeeReviewStateBadge.tsx
  needed to: display derived review state in user-facing wording.
  visual: small state label/badge in the row.
```

Not owned here:

```text
approve/reject command features;
review command server behavior;
server-side authorization;
generated OpenAPI editing;
shared/api business-specific wrappers.
```

---

## 7. Styling / CSS Ownership

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Dashboard page layout | page | `pages/employee/requests/dashboard/employeeDashboardPage.css` | page spacing, state blocks, filter/list placement only |
| Dashboard list/rows | entity UI | `entities/employee-request/ui/employeeRequestDashboard.css` | reusable request dashboard row/list/empty layout |
| StartReview button | command feature | `features/employee-request/start-review/ui/startReviewButton.css` | command button visual/pending/error behavior owned by feature |
| App shell | shared layout | shared Header/Footer styles | not modified by this sidecar |
| Tokens/base | global | global styles | tokens/reset/base only |

Checklist:

```text
[ ] no broad global selector
[ ] no hover layout shift
[ ] no border-width change on hover
[ ] no page CSS reaches into feature internals
[ ] no command CSS changes app shell
[ ] loading/empty/error states styled
[ ] filters remain keyboard accessible
[ ] row details link has accessible name
```

This archive does not change CSS.

---

## 8. Client API / Server Contract

Server endpoint:

```http
GET /api/employee/requests?status=...&reviewState=...
```

Feature/entity placement rule:

```text
Read endpoint wrappers live in entities/*/api.
Command endpoint wrappers live in features/*/api.
shared/api owns generic transport, ProblemDetails/ApiError, CSRF helpers and generated OpenAPI types only.
```

Current wrapper:

```text
entities/employee-request/api/listEmployeeDashboardRequests.ts
```

Current request query type:

```ts
type EmployeeRequestListQuery = {
  status?: EmployeeRequestStatus;
  reviewState?: EmployeeDashboardReviewState;
};
```

Current response aliases:

```ts
export type EmployeeRequestListItemDto =
  components["schemas"]["EmployeeRequestListItemDto"];

export type EmployeeRequestListResponseDto =
  components["schemas"]["EmployeeRequestListResponseDto"];
```

Current query key:

```ts
employeeRequestQueryKeys.dashboard(filters)
```

Do not add:

```text
src/shared/api/employeeRequestApi.ts
business-specific dashboard wrappers under shared/api
```

---

## 9. Validation / Feedback / Error UI

Client URL filter validation:

```text
status:
  parsed from URL search param;
  must be one of employeeRequestStatusOptions;
  invalid value shows "Unknown employee request status filter." and reset action.

reviewState:
  parsed from URL search param;
  must be one of employeeReviewStateOptions;
  invalid value shows "Unknown employee review state filter." and reset action.
```

Server validation still owns final API contract:

```text
invalid query values from direct HTTP/API call return validation ProblemDetails.
```

Visible feedback states:

```text
No session:
  sign-in required message + login link.

Non-Employee session:
  access denied state with role boundary message.

Invalid filters:
  alert/reset state; dashboard query disabled.

Loading:
  loading text.

Query error:
  alert/error text.

Empty success:
  default or filtered empty state.
```

---

## 10. Accessibility / ARIA Contract

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|
| Page heading | `h1` | heading text from constants | n/a | `aria-labelledby` on section | `getByRole("heading", { name: ... })` |
| Filter form | `form` | `aria-label="Employee dashboard filters"` | Tab through controls | yes, form label | `getByLabelText("Status")` |
| Status filter | `select` | `<label>Status</label>` | native select | no | `getByLabelText("Status")` |
| ReviewState filter | `select` | `<label>Review state</label>` | native select | no | `getByLabelText("Review state")` |
| Reset filters | `button` | button text | Enter/Space | no | `getByRole("button", { name: "Reset filters" })` |
| Dashboard list | `div` | `aria-label` from constants | row links/buttons focusable | yes, list label | current tests check row/link content |
| Details link | `a` | row link text includes request id | Enter activates link | no | `getByRole("link", { name: ... })` |
| Invalid/access/error state | `div`/`p` | visible text | n/a | `role="alert"` where implemented | `getByText` / `getByRole("alert")` |
| StartReview action | `button` inside feature | feature button label | feature-owned | feature-owned | StartReview feature tests |

---

## 11. Cross-Cutting Concerns

| Concern | Applies? | Direction |
|---|---:|---|
| Session/auth role | yes | Page gates query by session role; server remains authoritative. |
| Server query validation | yes | Server validates status/reviewState enum values. |
| Client URL validation | yes | Client blocks invalid URL filters for UX and reset. |
| ProblemDetails | yes | Generic fetch/error handling; page shows error state. |
| CSRF | no for dashboard read | GET read; command action features own unsafe command CSRF behavior. |
| StartReview action slot | yes as host | Dashboard can host StartReviewButton, but does not own mutation/security. |
| Generated types | yes | Entity API aliases generated OpenAPI schemas. |
| Styling ownership | yes | page vs entity UI vs feature CSS separated. |

---

## 12. Questions / Decisions

### Blocked / unresolved

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-EMP-DASH-CLIENT-001` | blocked | Dedicated UI scenario source? | Missing/pending; current implementation evidence used. |
| `Q-L2-EMP-DASH-CLIENT-002` | blocked | Future pagination/sorting? | Not current first pass. Add only with server contract. |
| `Q-L2-EMP-DASH-CLIENT-003` | blocked | Department/assignment filters? | Deferred future visibility/queue slice. |

### Accepted

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-L2-EMP-DASH-CLIENT-004` | accepted | Read wrapper placement? | `entities/employee-request/api/listEmployeeDashboardRequests.ts`. |
| `Q-L2-EMP-DASH-CLIENT-005` | accepted | Command wrapper placement? | Command features, not dashboard entity API. |
| `Q-L2-EMP-DASH-CLIENT-006` | accepted | URL filter ownership? | Dashboard page model owns URL parse/serialize. |
| `Q-L2-EMP-DASH-CLIENT-007` | accepted | Invalid URL filters? | Show invalid filter message/reset; disable query. |
| `Q-L2-EMP-DASH-CLIENT-008` | accepted | StartReview action on dashboard? | Host StartReviewButton for NotStarted/InReview rows only. |
| `Q-L2-EMP-DASH-CLIENT-009` | accepted | Does dashboard own StartReview mutation? | No, command sidecar owns it. |
| `Q-L2-EMP-DASH-CLIENT-010` | accepted | Does dashboard own approve/reject? | No. Details/command sidecars own them. |
| `Q-L2-EMP-DASH-CLIENT-011` | accepted | Does client enforce security? | No. Client visibility is UX only. |
| `Q-L2-EMP-DASH-CLIENT-012` | accepted | Shared/api business wrapper? | Do not add; use entity API wrapper. |

---

## 13. Extension / Change Points

```text
- pagination/sorting once server contract exists;
- department/assignment/personal queue filters;
- richer action availability DTO from server;
- dashboard badges/counts;
- E2E coverage for Employee dashboard happy path;
- source registry behavior IDs;
- dedicated UI scenario source for dashboard composition.
```

---

## 14. Behavior Coverage

| Behavior | Status | How client sidecar covers it |
|---|---|---|
| Employee dashboard page renders | implemented evidence | `EmployeeDashboardPage` page shell. |
| No session state visible | implemented evidence | session check shows sign-in required state. |
| Non-Employee access state visible | implemented evidence | role check shows access denied state. |
| Dashboard filters visible | implemented evidence | `EmployeeRequestDashboardFilters`. |
| URL status/reviewState filters parsed | implemented evidence | `employeeDashboardUrlFilters`. |
| Invalid URL filters blocked | implemented evidence | invalid state/reset, query disabled. |
| Dashboard data loaded | implemented evidence | `useEmployeeRequestDashboardQuery`. |
| API wrapper calls server endpoint | implemented evidence | `listEmployeeDashboardRequests`. |
| Empty state visible | implemented evidence | `EmployeeRequestDashboardEmptyState`. |
| Row summary visible | implemented evidence | `EmployeeRequestDashboardList/Row`. |
| Row details link visible | implemented evidence | details link to employee request details. |
| StartReview action hosted | implemented evidence | StartReviewButton rendered for NotStarted/InReview rows. |
| StartReview mutation/security | out of scope | `L2-REVIEW-START-001.client` and server command slice. |
| Approve/reject actions | out of scope | separate command sidecars. |

---

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and visible client outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item / behavior | Visible/client outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| API wrapper loads dashboard without filters | fetch called with `/api/employee/requests` and GET | API/model test | mocked fetch/fetchJson boundary | Medium: does not prove server behavior | Low/Medium: wrapper internals may change | actual `listEmployeeDashboardRequests gets employee requests without filters` |
| API wrapper maps filters | query string has `status` and `reviewState` | API/model test | mocked fetch | Medium: server validation not proved | Low | actual `maps status and review state filters to query string` |
| List empty state | no requests shows empty message | component test | render list with [] | Low for UI empty behavior | Low | actual `EmployeeRequestDashboardList shows an empty state...` |
| Row summary and details link | row fields and details link visible | component test | render list with row DTO | Low for row display/link | Low | actual `renders employee request row data and details link` |
| Row action slot | optional action can render inside row | component test | render action callback | Medium: command behavior not proved | Low | actual `renders optional row action slot` |
| Filtered empty reset | filtered empty state shows reset action | component test | render list empty with filtered variant | Low | Low | actual `shows filtered empty state with reset action` |
| Dashboard hosts StartReview for eligible row | StartReview button visible only for NotStarted/InReview row | page component test | mock query/session/feature button | Medium: command mutation not proved | Medium if eligibility moves to server DTO | actual `hosts Start Review action for not-started InReview dashboard rows only` |
| Filter form shows selected values | status/reviewState selects reflect filters | component test | render filter form | Low | Low | actual `shows selected filters` |
| Filter changes emit next filters | reviewState change calls onChange | component test | userEvent select | Low | Low | actual `reports filter changes` |
| Reset filters emits reset | reset button calls onReset | component test | userEvent click | Low | Low | actual `reports reset action` |
| Invalid URL filters blocked | invalid URL shows reset and query disabled | page/model test | URL parser tests/page state tests | Medium until page test explicitly checks query disabled | Low | actual URL filter model tests; page invalid-state test can be strengthened if missing |
| Employee dashboard E2E | Employee sees dashboard rows and opens details | E2E | browser + backend seed | Low | Medium | future E2E |

### Actual test evidence

```text
entities/employee-request/api/listEmployeeDashboardRequests.test.ts
entities/employee-request/ui/EmployeeRequestDashboardList.test.tsx
pages/employee/requests/dashboard/EmployeeDashboardPage.test.tsx
pages/employee/requests/dashboard/EmployeeRequestDashboardFilters.test.tsx
pages/employee/requests/dashboard/model/employeeDashboardUrlFilters.test.ts
```

### Follow-up test improvements

```text
- page test for no-session sign-in state;
- page test for non-Employee access denied state;
- page test for invalid URL filters disabling query;
- E2E Employee dashboard happy path if browser coverage is needed.
```

---

## 16. Suggested File Placement

Current implementation evidence:

```text
src/pages/employee/requests/dashboard/
  EmployeeDashboardPage.tsx
  EmployeeRequestDashboardFilters.tsx
  employeeDashboardPage.css
  model/employeeDashboardUrlFilters.ts

src/entities/employee-request/api/
  listEmployeeDashboardRequests.ts
  employeeRequestApiTypes.ts

src/entities/employee-request/model/
  employeeRequestFilters.ts
  employeeRequestQueryKeys.ts
  employeeRequestTypes.ts
  useEmployeeRequestDashboardQuery.ts

src/entities/employee-request/ui/
  EmployeeRequestDashboardList.tsx
  EmployeeRequestDashboardRow.tsx
  EmployeeRequestDashboardEmptyState.tsx
  EmployeeReviewStateBadge.tsx
  employeeRequestDashboard.css
  employeeRequestDashboardConst.ts

src/features/employee-request/start-review/ui/
  StartReviewButton.tsx
```

Do not add:

```text
src/shared/api/employeeRequestApi.ts
src/pages/employee/dashboard/EmployeeDashboardPage.tsx
StartReview mutation code inside dashboard page
Approve/Reject command forms inside dashboard read sidecar
```

---

## 17. Implementation Checklist / Current Evidence Checklist

```text
[x] Employee dashboard page exists
[x] no-session state exists
[x] non-Employee access state exists
[x] URL filter parser exists
[x] status/reviewState filter UI exists
[x] invalid filter reset UX exists
[x] entity API wrapper exists
[x] generated DTO aliases exist near employee-request entity
[x] query hook exists
[x] query key includes normalized filters
[x] list widget exists
[x] row summary/details link rendering exists
[x] empty default/filtered states exist
[x] StartReview action slot hosted for NotStarted/InReview rows
[x] API wrapper tests exist
[x] list component tests exist
[x] page action-slot test exists
[x] filter component tests exist
[x] URL filter model tests exist
[ ] tests were not executed in this docs-only archive
[ ] no runtime UI/CSS changes in this docs-only archive
```

---

## 18. Next Step

```text
No runtime implementation step is included in this docs-only archive.

Potential later improvements:
1. Add missing page tests for sign-in/access/invalid-filter states if absent.
2. Add E2E Employee dashboard happy path if needed.
3. Add dedicated UI scenario source for Employee dashboard composition.
4. Update source registry / slice derivation map when those files are introduced.
```

---

## 19. Guardrail Summary

```text
Employee dashboard is a read sidecar.
Server owns Employee request list data and query validation.
Client owns page shell, URL filters, query hook, list widget and visible states.
StartReview action may be hosted but not owned by this read sidecar.
Approve/reject are not dashboard read responsibilities.
Client-side visibility is UX only, not security.
Business endpoint wrappers stay in entities/*/api for reads.
Do not add business-specific wrappers under shared/api.
Do not deep-redesign UI/CSS in this docs-only refactor.
```
