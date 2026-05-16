# L1-MY-REQUESTS-LIST-FILTERS.client — My Requests List Filters Client Sidecar

**Status:** implementation-ready client sidecar / full draft
**Slice type:** client read/filter sidecar
**Scope:** introduce filter architecture for **My Requests** list; first implemented filter is `status`
**Depends on:** `L1-MY-REQUESTS-READ-LIST.client`
**Backend capability:** `GET /api/l1/requests?status=...`
**Out of scope:** request details page, request creation, employee review filters, non-status filters until backend supports them

**Parent slice:** `SL-REQ-002 — My Requests List`  
**Source scenario/UI/behavior files:**

```text
planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md
planning/diagrams/scenario-data/SC-05-my-requests-data.md
planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md
planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md
```

Current My Requests list sidecar intentionally excludes filtering UI and says future filtering should keep URL/query state on page level. 
Backend list endpoint already accepts optional `status` query and returns validation problem for invalid status. 

---

## 1. Sidecar Overview

This full sidecar records the implementation-ready client architecture for My Requests filters.

## 2. Core Idea

Filters are **page state**.

URL query params are the storage/sync mechanism for that page state.

Entity query fetches the list with the current filters.

```text
/requests
        ↓
filters = {}
        ↓
GET /api/l1/requests

/requests?status=Approved
        ↓
filters = { status: "Approved" }
        ↓
GET /api/l1/requests?status=Approved
```

This means:

```text
Page owns filter state.

Feature filter UI lets user change it.

Entity query fetches data with it.

List feature renders whatever data came back.
```

---

## 3. Visual UI / Scenario Flow

```text
[Authenticated Client]
opens My Requests page
        ↓
[Client UI]
shows own request list
        ↓
[Client]
uses filters UI
        ↓
[Client]
selects request status
        ↓
[Page]
updates filter state
and syncs it to URL query params:
  /requests?status=Approved
        ↓
[Entity Query]
fetches My Requests with filters
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
clears filter state and URL query:
  /requests
        ↓
[Entity Query]
fetches unfiltered list
```

---

## 4. Visual Client Implementation Flow

```text
[Route]
/requests
/requests?status=InReview
/requests?status=Approved
/requests?status=Rejected
        ↓
[Page Layer]
pages/requests/my/MyRequestsPage.tsx
owns filter state through URL search params
        ↓
[Page URL Model]
pages/requests/my/model/myRequestsUrlFilters.ts
parses URLSearchParams into typed MyRequestsFilters
validates URL values
serializes MyRequestsFilters back to URLSearchParams
        ↓
[Entity Model]
entities/request/model/myRequestsFilters.ts
defines:
  MyRequestsFilters
  MyRequestStatus
  allowed status values
        ↓
[Entity Query]
entities/request/model/useMyRequestsQuery.ts
receives filters as input
builds query key with filters
calls listMyRequests(filters)
        ↓
[Shared API]
shared/api/l1RequestApi.ts
maps filters to backend query string:
  status -> ?status=Approved
        ↓
[Feature Filter UI]
features/request/my-requests-filters/ui/*
renders controls
receives filters + callbacks
does not read URL directly
        ↓
[Feature List UI]
features/request/my-requests-list/ui/*
renders request list / empty state
does not know where filters came from
```

---

## 5. Architecture Rules

| Layer                                  | Owns                                                                | Does not own                                 |
| -------------------------------------- | ------------------------------------------------------------------- | -------------------------------------------- |
| `pages/requests/my`                    | filter state, URL sync, invalid URL state, passing filters to query | request card rendering, low-level HTTP       |
| `entities/request`                     | filter types, allowed values, query keys, query hook                | route params, `useSearchParams`, UI controls |
| `features/request/my-requests-filters` | visible filter controls, `onChange`, `onReset` events               | URL, fetch, React Query                      |
| `features/request/my-requests-list`    | list/card/empty rendering                                           | filters parsing, backend query params        |
| `shared/api`                           | HTTP query string mapping                                           | current selected filter state                |

Correct mental model:

```text
Filter UI = controlled feature component.

Filter state = page state.

Filter type = request entity model.

Filtered fetch = request entity query.

HTTP mapping = shared API.
```

---

## 6. State Flow

When user selects `Approved`:

```text
1. MyRequestsFilters UI receives current filters:
   {}

2. User selects Approved.

3. MyRequestsFilters calls:
   onChange({ status: "Approved" })

4. MyRequestsPage handles change:
   setSearchParams({ status: "Approved" })

5. URL becomes:
   /requests?status=Approved

6. MyRequestsPage rerenders.

7. Page parses URL:
   filters = { status: "Approved" }

8. Page calls:
   useMyRequestsQuery({ filters })

9. Query key changes:
   ["requests", "my", { status: "Approved" }]

10. React Query automatically fetches:
    GET /api/l1/requests?status=Approved

11. MyRequestsList receives returned data and renders it.
```

No manual `refetch()` is needed if the query key includes filters.

---

## 7. Questions / Decisions

### Q-MYREQ-FILTER-001 — Is this status-only or filter foundation?

**Status:** decided.

This slice introduces the **My Requests filter architecture**. The first implemented filter is `status`.

Current shape:

```ts
type MyRequestsFilters = {
  status?: MyRequestStatus;
};
```

Future shape:

```ts
type MyRequestsFilters = {
  status?: MyRequestStatus;
  requestType?: MyRequestType;
  createdFrom?: string;
  createdTo?: string;
  search?: string;
};
```

---

### Q-MYREQ-FILTER-002 — Where does filter state live?

**Status:** decided.

Filter state lives on the page.

The page stores/syncs it through URL query params:

```text
/requests
/requests?status=InReview
/requests?status=Approved
/requests?status=Rejected
```

`MyRequestsFilters` does not own filter state. It only displays current filters and emits change events.

---

### Q-MYREQ-FILTER-003 — Why does `MyRequestsFilters` receive filters?

**Status:** decided.

Because it is a controlled component.

It receives:

```tsx
<MyRequestsFilters
  filters={filters}
  onChange={handleFiltersChange}
  onReset={handleResetFilters}
/>
```

Meaning:

```text
Page tells filter UI:
  current status is Approved

Filter UI renders:
  select value = Approved

User changes selection

Filter UI tells page:
  user selected Rejected

Page updates filter state / URL.
```

The component needs `filters` to show the current selected value, especially after reload or direct open of `/requests?status=Approved`.

---

### Q-MYREQ-FILTER-004 — Should API helper accept a filter object?

**Status:** decided.

Yes.

```ts
listMyRequests(filters?: MyRequestsFilters)
```

For this slice it maps only `status`.

Later filters can be added without changing the page-to-query calling pattern.

---

### Q-MYREQ-FILTER-005 — What happens for invalid status in URL?

**Status:** assumption.

Preferred first implementation:

```text
/requests?status=Done
        ↓
page detects invalid filter value
        ↓
does not call list endpoint with invalid status
        ↓
shows visible invalid filter state
        ↓
offers reset filters action
```

Backend is still safe if bad value reaches it, because it returns validation problem for invalid status. 

---

### Q-MYREQ-FILTER-006 — Should filtered empty state differ from regular empty state?

**Status:** accepted direction.

Regular empty state:

```text
У вас пока нет заявок.
```

Filtered empty state:

```text
Заявок с выбранным фильтром не найдено.
```

Filtered empty state should offer reset filters action.

---

## 8. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item                              | How draft covers it                               | Status                  |
| ------------------------------------------ | ------------------------------------------------- | ----------------------- |
| Client can use filters on My Requests page | Adds filter UI to `/requests`                     | covered                 |
| Status is the first supported filter       | Status control maps to backend `?status=`         | covered                 |
| Filter state survives reload               | State is stored in URL query params               | covered                 |
| Filter state supports browser back/forward | URL query is source of page state                 | covered                 |
| Filtered list shows matching requests      | Filters are passed into entity query and API call | covered                 |
| Filtered empty state is understandable     | Separate empty message for active filters         | covered                 |
| Client can reset filters                   | Reset clears URL query params                     | covered                 |
| Invalid filter value is handled safely     | Page shows invalid filter state and reset action  | covered                 |
| Future filters can be added                | Uses extensible `MyRequestsFilters` model         | covered as architecture |

---

## 9. Test / Verification Plan

### Component/client tests

| Test / check                           | Verifies                                            |
| -------------------------------------- | --------------------------------------------------- |
| Filters UI renders on My Requests page | Filter entry is visible                             |
| Status filter options render           | Supported statuses are selectable                   |
| URL query initializes selected status  | `/requests?status=Approved` shows Approved selected |
| Selecting status updates URL query     | Page owns filter state and URL sync                 |
| `useMyRequestsQuery` receives filters  | Page passes filters to entity query                 |
| Query key includes filters             | Filtered/unfiltered lists do not share cache key    |
| API helper maps status to `?status=`   | Shared API builds correct request URL               |
| Reset filters clears URL query         | User returns to unfiltered list                     |
| Filtered empty state renders           | Empty filtered result is understandable             |
| Invalid URL status shows safe state    | Bad URL does not crash and can be reset             |

### Shared API tests

| Test / check                                                                      | Verifies                  |
| --------------------------------------------------------------------------------- | ------------------------- |
| `listMyRequests()` calls `/api/l1/requests`                                       | Unfiltered call unchanged |
| `listMyRequests({ status: "Approved" })` calls `/api/l1/requests?status=Approved` | Status mapping            |
| API uses generated response type                                                  | No duplicated DTO         |

### E2E happy path

```text
register/login client
create applicant party
create multiple requests
set one request to Approved through backend/test setup
        ↓
open /requests
        ↓
unfiltered list is visible
        ↓
select status = Approved
        ↓
URL becomes /requests?status=Approved
        ↓
approved request is visible
        ↓
in-review request is not visible in filtered list
        ↓
reset filters
        ↓
URL becomes /requests
        ↓
unfiltered list is visible again
```

E2E should assert visible filtered behavior, not query key internals.

---

## 10. Suggested File Placement

```text
src/pages/requests/my/
  MyRequestsPage.tsx
  myRequestsPage.css
  model/myRequestsUrlFilters.ts
  model/myRequestsUrlFilters.test.ts

src/entities/request/model/
  myRequestsFilters.ts
  requestQueryKeys.ts
  useMyRequestsQuery.ts

src/entities/request/api/
  listMyRequests.ts

src/features/request/my-requests-filters/ui/
  MyRequestsFilters.tsx
  MyRequestsStatusFilter.tsx
  myRequestsFiltersConst.ts
  myRequestsFilters.css

src/features/request/my-requests-list/ui/
  MyRequestsList.tsx
  MyRequestsEmptyState.tsx

src/shared/api/
  l1RequestApi.ts

tests/e2e/requests/
  my-requests-filters.spec.ts

planning/slices/l1/
  L1-MY-REQUESTS-LIST-FILTERS.client.md
```

---

## 11. Next Step

Implementation direction:

```text
Add MyRequestsFilters type/model
        ↓
Add page URL parse/serialize helpers
        ↓
Update MyRequestsPage:
  read search params
  parse filters
  handle invalid URL filters
  pass filters to query
  pass filters/onChange/onReset to filter UI
        ↓
Update useMyRequestsQuery(filters)
        ↓
Update requestQueryKeys.myRequests(filters)
        ↓
Update listMyRequests(filters)
        ↓
Add MyRequestsFilters UI
        ↓
Add filtered empty state
        ↓
Add component tests
        ↓
Add E2E happy path
```

---

## 12. Scenario Flow

```text
Authenticated client opens My Requests page
        ↓
Client sees own request list
        ↓
Client selects a status filter
        ↓
Page updates filter state
        ↓
Page syncs filter state to URL query params
        ↓
Client UI loads own requests matching selected status
        ↓
Filtered results are displayed
        ↓
If no matching requests exist, filtered empty state is displayed
        ↓
Client can reset filters
        ↓
Page clears filter state and returns to unfiltered My Requests list
```

---

## 13. Behavior Items

### Client can use My Requests filters

The authenticated client can filter the My Requests list.

### Status is the first supported filter

The first available filter is request status.

### Filter state is stored in URL

Selected filters are represented in page query params.

### Filter UI shows current selected filter

The filter controls display the current filter state from the page.

### Filter UI reports user changes to the page

When the user changes a filter, the filter UI emits the new filter state to the page.

### Filtered list uses backend query

The client requests filtered data from `GET /api/l1/requests?status=...`.

### Filtered empty state is visible

If no requests match the selected filter, the page shows a filtered empty state.

### Client can reset filters

The client can clear filters and return to the unfiltered list.

### Invalid filter value is handled safely

Invalid URL filter values do not crash the page and can be reset.

### Future filters can be added

The filter model is extensible for future filters such as request type, date range, or search.


---

## 14. Client API / Generated Contract Boundary

Backend contract already supports:

```text
GET /api/l1/requests
GET /api/l1/requests?status=InReview
GET /api/l1/requests?status=Approved
GET /api/l1/requests?status=Rejected
```

Client implementation should use generated OpenAPI response types and should not duplicate response DTOs by hand.

Shared API target:

```ts
listMyRequests(filters?: MyRequestsFilters): Promise<L1ListMyRequestsResponse>
```

Query key target:

```ts
requestQueryKeys.myRequests(filters)
```

Contract boundary:

```text
OpenAPI owns structural route/query/response shape.
This sidecar owns client state architecture and UI behavior for filters.
Future non-status filters must not be added to the client before backend/source docs support them.
```

---

## 15. Implementation Checklist

```text
[ ] Add MyRequestsFilters type/model.
[ ] Add allowed status values from generated/contract-backed status semantics.
[ ] Add page URL parse/serialize helpers.
[ ] Update MyRequestsPage to read/write status query param.
[ ] Add invalid URL status handling.
[ ] Add filter UI controls and reset action.
[ ] Add filtered empty state.
[ ] Update useMyRequestsQuery(filters).
[ ] Update requestQueryKeys.myRequests(filters).
[ ] Update listMyRequests(filters) shared API wrapper.
[ ] Add component/router/API tests.
[ ] Add E2E only when stable data setup makes visible filter behavior meaningful.
```
