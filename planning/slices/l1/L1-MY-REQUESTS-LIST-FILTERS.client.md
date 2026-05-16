# L1-MY-REQUESTS-LIST-FILTERS.client — My Requests List Filters Client Sidecar

Status: implementation-ready client sidecar  
Parent slice: `SL-REQ-002 — My Requests List`  
Slice type: client read/filter sidecar  
Source scenario/UI sources:

```text
planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md
planning/diagrams/scenario-data/SC-05-my-requests-data.md
planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md
planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md
```

Backend already exists:

```text
GET /api/l1/requests?status=InReview|Approved|Rejected
```

Scope: introduce extensible My Requests filter architecture; first supported filter is status.

Out of scope:

```text
requestType/date/search filters until backend and scenario/UI support exist
request details page
request creation UI
backend filter implementation
```

## 1. Sidecar Overview

This slice introduces the My Requests list filter architecture.

The first supported filter is `status` because backend already supports:

```text
GET /api/l1/requests?status=...
```

The filter model must be extensible for future filters without moving URL ownership out of the page.

## 2. Sources / Source Behavior Items

Relevant source items:

```text
SC-05-DATA-02 — Request filter DATA: status
SC-05-BI-005 — Client can filter My Requests by status
SC-05-BI-006 — Status is first filter in extensible filter model
SC-05-BI-007 — Client can reset filters
SC-05-BI-008 — Invalid status URL value is safe
```

## 3. Visual UI / Scenario Flow

```text
[Authenticated Client]
opens My Requests page
        ↓
[Page]
reads URL query params
        ↓
[Filter UI]
shows current filter state
        ↓
Client selects status filter or resets filters
        ↓
[Page]
writes URL query params
        ↓
[Entity Query]
reloads My Requests with filter object
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ matching requests exist      │ no matching requests         │
 ▼                              ▼
Filtered list                   Empty filtered state
```

Invalid URL value:

```text
/requests?status=Done
        ↓
Page parses status
        ↓
unsupported value is handled safely
        ↓
UI does not crash or show misleading filter state
```

## 4. UI Slice Flow

| Step | UI/system | Behavior | Source item | Scope status |
|---|---|---|---|---|
| F01 | Page | Reads `status` from URL query params. | `SC-05-BI-005` | in scope |
| F02 | Page/helper | Parses/normalizes status into filter model. | `SC-05-BI-008` | in scope |
| F03 | Filter UI | Shows status filter controls. | UI spec | in scope |
| F04 | Client | Selects status or reset filters. | `SC-05-BI-005` / `SC-05-BI-007` | in scope |
| F05 | Page | Writes updated query params. | URL ownership rule | in scope |
| F06 | Entity query | Uses filter object in query key and API call. | implementation boundary | in scope |
| F07 | Shared API | Maps supported filters to query string. | API contract | in scope |
| F08 | Future UI | Request type/date/search filters. | future candidates | out of scope |

## 5. Visual Client Implementation Flow

```text
[Page: pages/requests/my/MyRequestsPage.tsx]
owns URL query params
reads ?status=InReview
        ↓
[Filter Model]
MyRequestsFilters
{ status?: MyRequestStatus }
        ↓
[Feature UI: features/request/my-requests-filters]
renders controls + reset action
        ↓
[Entity Query: entities/request]
useMyRequestsQuery(filters, enabled)
query key includes filters
        ↓
[Shared API: shared/api/l1RequestApi.ts]
listMyRequests(filters)
maps status -> ?status=
        ↓
[Backend]
GET /api/l1/requests?status=...
```

## 6. Client Implementation Flow

Target model:

```ts
type MyRequestStatus = "InReview" | "Approved" | "Rejected";

type MyRequestsFilters = {
  status?: MyRequestStatus;
};
```

Future extension shape:

```ts
type MyRequestsFilters = {
  status?: MyRequestStatus;
  requestType?: MyRequestType;
  createdFrom?: string;
  createdTo?: string;
  search?: string;
};
```

Implementation ownership:

```text
Page owns URL query params.
Filter feature owns controls and parse/serialize helpers.
Entity query accepts filter object and includes it in query key.
Shared API maps supported filter object to query string.
Shared API does not own filter state.
Filter UI does not read window.location.search directly.
```

## 7. Client API / Generated Contract

Backend contract:

```text
GET /api/l1/requests?status=InReview
GET /api/l1/requests?status=Approved
GET /api/l1/requests?status=Rejected
```

Shared API target:

```ts
listMyRequests(filters?: MyRequestsFilters): Promise<L1ListMyRequestsResponse>
```

Query key target:

```ts
requestQueryKeys.myRequests(filters)
```

## 8. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-MYREQ-FILTERS-001` | accepted | Is status filter a one-off control? | No. It is the first entry in a filter architecture. | component/query/API structure |
| `Q-MYREQ-FILTERS-002` | accepted | Who owns URL query params? | Page owns URL query params. | prevents hidden router coupling |
| `Q-MYREQ-FILTERS-003` | accepted | Should filter UI read `window.location.search`? | No. Page passes filter value and callbacks. | component purity/testability |
| `Q-MYREQ-FILTERS-004` | accepted | Should shared API own filter state? | No. It maps supported filters to query string only. | API wrapper boundary |
| `Q-MYREQ-FILTERS-005` | assumption | What to do with invalid status URL value? | Handle safely by normalizing to unfiltered state or showing a safe state; do not crash. | URL parser/test behavior |
| `Q-MYREQ-FILTERS-006` | future review | Which filter is next after status? | requestType/date/search only after backend and UI support. | future filter extension |

## 9. Client Extension / Change Points

| ID | Type | Area | Current direction | Status |
|---|---|---|---|---|
| `CP-MYREQ-FILTER-001` | extension point | filter model | Start with `status`; keep object extensible. | accepted |
| `CP-MYREQ-FILTER-002` | ownership boundary | URL state | Page owns query params. | accepted |
| `CP-MYREQ-FILTER-003` | API boundary | query mapping | Shared API maps supported filters only. | accepted |
| `CP-MYREQ-FILTER-004` | future filter | requestType/date/search | Do not implement until backend supports them. | future review |

## 10. Behavior Coverage

| Source behavior item | How sidecar covers it | Draft location | Status |
|---|---|---|---|
| `SC-05-BI-005` Client can filter My Requests by status | Adds status filter UI and passes status to query/API. | UI/Implementation Flow | covered as draft |
| `SC-05-BI-006` Status is first filter in extensible filter model | Introduces `MyRequestsFilters` and extension boundary. | Implementation Flow / Extension Points | covered |
| `SC-05-BI-007` Client can reset filters | Adds reset action and unfiltered URL/query state. | UI Slice Flow | covered |
| `SC-05-BI-008` Invalid status URL value is safe | Parser handles unsupported value without crash. | Questions / Test Plan | covered as assumption |
| Future requestType/date/search filters | Explicitly out of scope until backend/source support exists. | Out of scope / Extension Points | deferred |

## 11. Client / Component / E2E Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Status filter renders options | UI exposes supported statuses. | component/client | planned |
| Selecting status updates URL | Page owns URL query params. | component/router | planned |
| Selecting status reloads query with filters | Query key and API call include status. | component/query | planned |
| Reset filters clears URL/filter state | Reset behavior. | component/router | planned |
| Invalid URL status is safe | Bad URL does not crash or mislead. | component/router | planned |
| Shared API maps status to `?status=` | Correct backend contract. | shared API/unit | planned |
| E2E filter happy path | Visible filtered list changes by status. | E2E | optional after stable data setup |

E2E should assert visible filtered outcome, not internal query key mechanics.

## 12. Covered Scenario / UI Behavior Items

```text
SC-05-BI-005
SC-05-BI-006
SC-05-BI-007
SC-05-BI-008
```

## 13. Dependent / Follow-up Slices

```text
L1-MY-REQUESTS-READ-LIST.client
future request type/date/search filter support
future My Request Details client sidecar may reuse list navigation but not filter state
```

## 14. Implementation Checklist

```text
[ ] Add MyRequestStatus / MyRequestsFilters model.
[ ] Add parse/serialize helpers for URL status.
[ ] Update MyRequestsPage to read/write status query param.
[ ] Add filter UI controls and reset action.
[ ] Update useMyRequestsQuery(filters, enabled).
[ ] Update requestQueryKeys.myRequests(filters).
[ ] Update listMyRequests(filters) shared API wrapper.
[ ] Add component/API tests.
[ ] Add E2E only when stable data setup makes visible filter behavior meaningful.
```
