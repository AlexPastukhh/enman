# L1-MY-REQUESTS-READ-LIST.client — My Requests Read List

Status: implemented first-stage client / layering normalization note  
Parent slice: `SL-REQ-002 — My Requests List`  
Slice type: client read sidecar  
Architecture direction: read slice maps to pages + entities; features are reserved for command/user-action behavior.

## 1. Scope

This sidecar owns:

```text
- My Requests list read state for signed-in client;
- list loading/error/empty/success states;
- rendering own request summaries;
- linking to request details where available;
- using entity query/API model for list reads.
```

## 2. Out of Scope

```text
- status filter controls -> L1-MY-REQUESTS-LIST-FILTERS.client;
- request details page -> L1-MY-REQUEST-DETAILS.client;
- create request UI -> future SL-REQ-001.client;
- backend list endpoint -> SL-REQ-002;
- review/approve/reject employee flows -> future slices.
```

## 3. Layering Correction

The request list is read/display UI.

Target placement for new or normalized code:

```text
entities/request/ui/MyRequestsList.tsx
entities/request/ui/MyRequestSummaryCard.tsx
entities/request/ui/MyRequestsEmptyState.tsx
entities/request/model/useMyRequestsQuery.ts
entities/request/api/listMyRequests.ts
shared/api/l1RequestApi.ts
```

Do not treat read-only list rendering as a feature command component.

Filter controls remain a feature/user-action sidecar.

## 4. Visual UI / Scenario Flow

```text
[Signed-in Client]
opens My Requests page
        ↓
[Page]
shows own request list read area
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ requests exist               │ no requests exist            │
 ▼                              ▼
Client sees own request         Client sees empty list state
summary cards
```

## 5. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/requests/my/MyRequestsPage.tsx

Lives here:
  MyRequestsPage
  page-level read branch composition

Uses:
  useMyRequestsQuery(filters)
  MyRequestsList
  MyRequestsFilters when filter sidecar is enabled

Owns:
  page layout
  read state composition

Does not own:
  fetchJson
  shared API path constants
  request summary card internals
        ↓

[Entity Query Layer]
entities/request/model/useMyRequestsQuery.ts

Lives here:
  useMyRequestsQuery()
  myRequestsQueryKeys

Uses:
  listMyRequests(filters)

Owns:
  React Query read hook
  query key including filters

Does not own:
  filter UI controls
  route params
        ↓

[Entity Display UI Layer]
entities/request/ui/*

Lives here:
  MyRequestsList
  MyRequestSummaryCard
  MyRequestsEmptyState

Owns:
  read-only list display
  empty state display

Does not own:
  filter state
  backend query params
```

## 6. Behavior Coverage

| Source behavior | How sidecar covers it | Status |
|---|---|---|
| client sees own requests | renders own request summary list | covered |
| empty list state | renders empty state when no requests | covered |
| request status visible | summary card displays status | covered |
| status filter | owned by filter sidecar | related/out of scope |
| request details | owned by details sidecar | related/out of scope |

## 7. Verification Plan

```text
- page renders loading/error/empty/success states;
- list renders request summaries;
- empty state appears for empty response;
- list component is read-only display;
- filter controls are not owned by list display component;
- E2E asserts visible list/empty state, not React Query internals.
```
