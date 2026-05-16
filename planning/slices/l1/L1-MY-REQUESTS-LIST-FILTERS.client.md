# L1-MY-REQUESTS-LIST-FILTERS.client — My Requests List Filters Client Sidecar

Status: implemented first-stage client sidecar / normalized to canonical draft style  
Slice type: client read/filter sidecar  
Parent slice: `SL-REQ-002 — My Requests List`  
Architecture direction: page owns URL/filter state; entity owns filter model/query; feature UI owns controls; shared API owns query-string HTTP mapping.

## 1. Scope

This sidecar owns:

```text
- My Requests list filter architecture;
- status as the first implemented filter;
- page-owned URL query state for filters;
- invalid URL filter handling and reset action;
- controlled filter UI;
- entity filter model;
- entity query key including filters;
- shared API query-string mapping for supported filters;
- filtered empty state and reset behavior.
```

## 2. Out of Scope

```text
- My Requests list card rendering -> L1-MY-REQUESTS-READ-LIST.client;
- Own Request Details page -> L1-MY-REQUEST-DETAILS.client;
- request creation -> future SL-REQ-001.client;
- employee review filters -> future employee-side slice;
- non-status filters until backend/source docs support them -> future filter extension;
- backend list endpoint implementation -> SL-REQ-002;
- OpenAPI generation work -> API generation/check workflow;
- manual refetch as filter mechanism -> not needed when query key includes filters.
```

## 3. Related Slices / Owners

```text
L1-MY-REQUESTS-READ-LIST.client
  owns base My Requests page/list read state and list/card rendering.

L1-MY-REQUESTS-LIST-FILTERS.client
  owns filter state, URL sync, filter controls and filtered query behavior.

L1-MY-REQUEST-DETAILS.client
  owns /requests/:requestId details read context.

SL-REQ-002 backend
  owns GET /api/l1/requests and status query support.

Future request creation sidecar
  owns create-new/from-feedback actions if introduced later.
```

## 4. Visual UI / Scenario Flow

Source UI model:

```text
Client opens My Requests page.
Client can filter own requests by status.
Filtered list or filtered empty state is visible.
Client can reset filters.
```

```text
[Authenticated Client]
opens My Requests page
        ↓
[Client UI]
shows own request list
        ↓
[Client]
uses filter UI
        ↓
[Client]
selects request status
        ↓
[Page]
updates filter state and URL query params
        ↓
[System]
returns own requests matching selected status
        ↓
[Client UI]
shows filtered list
or filtered empty state
        ↓
[Client]
can reset filters
        ↓
[Page]
clears filter state and URL query params
        ↓
[Client UI]
shows unfiltered list state
```

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Authenticated client | Opens My Requests page. |
| S02 | Page/list area | Shows own request list. |
| S03 | Filter controls | Client selects request status. |
| S04 | Filtered result | Client sees only own requests matching selected status. |
| S05 | Filtered empty state | Client sees that no requests match selected filter. |
| S06 | Reset action | Client can clear selected filters. |
| S07 | URL state | Filter selection can survive reload/direct URL. |

## 5. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/requests/my/MyRequestsPage.tsx

Lives here:
  MyRequestsPage
  page-level branch logic:
    loading / error / invalid filters / empty / filtered empty / success
  URL search-param ownership
  composition of filters + list

Uses:
  useSearchParams()
  parseMyRequestsFilters()
  serializeMyRequestsFilters()
  useMyRequestsQuery({ filters })
  MyRequestsFilters
  MyRequestsList

Owns:
  filter state through URL search params
  invalid URL filter branch
  reset filters action
  passing controlled filters/callbacks to filter UI
  passing returned request data to list UI

Does not own:
  allowed status constants internals
  low-level HTTP path string
  request card rendering internals
  shared API query-string mapping
        ↓

[Page URL Model]
pages/requests/my/model/myRequestsUrlFilters.ts

Lives here:
  parseMyRequestsFilters()
  serializeMyRequestsFilters()
  invalid filter detection

Uses:
  URLSearchParams
  allowed status values from entity model

Owns:
  URL <-> MyRequestsFilters mapping
  safe invalid URL handling

Does not own:
  React Query hook
  HTTP mapping
  visible controls
        ↓

[Entity Model Layer]
entities/request/model/myRequestsFilters.ts

Lives here:
  MyRequestsFilters
  MyRequestStatus
  allowedMyRequestStatuses

Owns:
  request filter type shape
  allowed status values
  future extension point for additional supported filters

Does not own:
  URLSearchParams
  UI controls
  HTTP query string construction
        ↓

[Entity Query Layer]
entities/request/model/useMyRequestsQuery.ts

Lives here:
  useMyRequestsQuery({ filters })
  myRequestsQueryKeys.list(filters)

Uses:
  useQuery()
  listMyRequests(filters)

Owns:
  query key including filters
  query function binding
  read hook state

Does not own:
  URL parsing
  visible filter controls
  HTTP path string
        ↓

[Entity API Layer]
entities/request/api/listMyRequests.ts

Lives here:
  listMyRequests(filters?)

Uses:
  shared API listMyRequests wrapper

Owns:
  entity-level read operation name
  delegating shared API response into request entity read flow

Does not own:
  React Query hook
  URL storage
  component rendering
        ↓

[Shared API Layer]
shared/api/l1RequestApi.ts

Lives here:
  listMyRequests(filters?) low-level wrapper
  mapping supported filters to query string

Uses:
  fetchJson()
  generated OpenAPI types where available

Owns:
  low-level HTTP call:
    GET /api/l1/requests
    GET /api/l1/requests?status=Approved
  stable path constant
  query-string construction

Does not own:
  current selected filter state
  React Query
  visible controls
  page branch decisions
        ↓

[Feature Filter UI Layer]
features/request/my-requests-filters/ui/*

Lives here:
  MyRequestsFilters component
  status select/control
  reset filters button

Props:
  filters: MyRequestsFilters
  onChange(nextFilters)
  onReset()

Owns:
  visible filter controls
  controlled UI rendering
  user events for filter changes/reset

Does not own:
  URL state
  fetch/query logic
  query keys
  backend query params
        ↓

[Feature List UI Layer]
features/request/my-requests-list/ui/*

Lives here:
  MyRequestsList
  request cards/list
  list empty state

Owns:
  rendering request summaries returned by query
  regular/filtered empty display when passed by page

Does not own:
  parsing filters
  filter controls
  backend query params
```

In ordinary words:

```text
The page owns the selected filters because the page owns the URL. The filter feature is controlled by the page and only emits user events. The request entity model owns the filter type and allowed status values. The entity query includes filters in its query key, so changing filters naturally triggers the correct read. The shared API wrapper is the low-level client/server boundary that maps supported filters to a query string.
```

## 6. Client API / Server Contract

Backend capability:

```text
GET /api/l1/requests
GET /api/l1/requests?status=InReview
GET /api/l1/requests?status=Approved
GET /api/l1/requests?status=Rejected
```

Current filter shape:

```ts
type MyRequestsFilters = {
  status?: MyRequestStatus;
};
```

Future possible shape, only when backend/source docs support it:

```ts
type MyRequestsFilters = {
  status?: MyRequestStatus;
  requestType?: MyRequestType;
  createdFrom?: string;
  createdTo?: string;
  search?: string;
};
```

Rules:

```text
Page owns filter state.
URL query params are storage/sync for page state.
Filter UI is controlled by page.
Entity query accepts filters and includes them in query key.
Shared API maps supported filters to query string.
No manual refetch is needed if query key includes filters.
Future filters are out of scope until backend/source docs support them.
```

## 7. Questions / Decisions

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `Q-MYREQ-FILTER-001` | implemented / accepted | Is this status-only or filter foundation? | Filter architecture with status as first implemented filter. | Future filters can extend same model. | Mirrored as `SL-MYREQ-FILTER-Q-001`. |
| `Q-MYREQ-FILTER-002` | implemented / accepted | Where does filter state live? | Page owns filter state and syncs it through URL query params. | Page/feature/entity boundary. | Mirrored as `SL-MYREQ-FILTER-Q-002`. |
| `Q-MYREQ-FILTER-003` | implemented / accepted | Why does filter UI receive filters? | It is a controlled component and must reflect URL/current state. | Reload/direct URL behavior. | Mirrored as `SL-MYREQ-FILTER-Q-003`. |
| `Q-MYREQ-FILTER-004` | implemented / accepted | Should API helper accept a filter object? | Yes, `listMyRequests(filters?: MyRequestsFilters)`. | Shared API/entity query contract. | Mirrored as `SL-MYREQ-FILTER-Q-004`. |
| `Q-MYREQ-FILTER-005` | implemented / assumption | What happens for invalid status URL value? | Page detects invalid value, avoids misleading request, shows reset action. Backend remains safe if reached. | Safe URL parsing. | Mirrored as `SL-MYREQ-FILTER-Q-005`. |
| `Q-MYREQ-FILTER-006` | implemented / accepted | Should filtered empty state differ from regular empty state? | Yes, show filtered empty state and reset action. | User feedback. | Mirrored as `SL-MYREQ-FILTER-Q-006`. |

## 8. Extension / Change Points

```text
- non-status filters -> future filter extension when backend/source docs support them;
- saved filter presets -> future UX slice, not current;
- employee-side filters -> separate employee-side slice;
- request creation from filtered empty state -> future request creation/client flow;
- server-side paging/sorting -> future API/list slice if introduced.
```

## 9. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How sidecar covers it | Status |
|---|---|---|
| Client can use filters on My Requests page | Adds controlled filter UI to `/requests`. | covered / implemented |
| Status is the first supported filter | Status control maps to backend `?status=`. | covered / implemented |
| Filter state survives reload/direct URL | State is stored in URL query params and parsed by page model. | covered / implemented |
| Filter state supports browser back/forward | URL query is source of page state. | covered / implemented |
| Filtered list shows matching requests | Filters are passed into entity query and API call. | covered / implemented |
| Filtered empty state is visible | Page/list renders filtered empty state and reset action. | covered / implemented |
| Invalid URL value is handled safely | Page detects invalid filter state and can offer reset. | covered / implemented |
| Future non-status filters | Explicitly out of scope until source/backend support exists. | future |

## 10. Client / Component / E2E Verification Plan

Component/client tests:

| Test / check | Verifies |
|---|---|
| filter UI receives selected status and renders it | controlled component behavior |
| changing status calls onChange with new filter object | feature emits event, does not own state |
| reset button calls onReset | reset event boundary |
| URL parser maps valid status to filter object | page URL model |
| URL parser detects invalid status | safe invalid URL handling |
| serializer writes/clears status query param | URL sync |
| query key changes when filters change | filtered query identity |
| filtered empty state differs from regular empty state | user feedback |

Shared API/entity tests:

| Test / check | Verifies |
|---|---|
| listMyRequests({ status: Approved }) calls `/api/l1/requests?status=Approved` | query-string mapping |
| listMyRequests({}) calls `/api/l1/requests` | unfiltered API call |
| useMyRequestsQuery includes filters in key | automatic refetch by query key |

E2E visible outcomes:

```text
login client
        ↓
open /requests
        ↓
select status filter
        ↓
assert URL query updates
        ↓
assert only matching visible request statuses are shown
        ↓
clear/reset filters
        ↓
assert unfiltered list state is visible
```

Do not assert React Query internals or manual refetch calls in E2E.

## 11. Implementation Checklist

```text
[x] Define MyRequestsFilters / MyRequestStatus.
[x] Parse filters from URL.
[x] Serialize filters to URL.
[x] Render controlled filter UI.
[x] Pass filters to useMyRequestsQuery.
[x] Include filters in query key.
[x] Map status to API query string.
[x] Render filtered empty state/reset action.
[x] Handle invalid URL status safely.
[ ] Extend with future filters only when backend/source docs support them.
```

## 12. Next Step

No new implementation is required for first-stage status filters if current code evidence confirms the above behavior.

Next docs/status work:

```text
- keep this sidecar status synchronized with current code;
- update shared registers when future filters are introduced;
- keep E2E assertions focused on visible filtered outcomes, not refetch mechanics.
```
