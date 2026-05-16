# L1-MY-REQUESTS-READ-LIST.client — My Requests List Client Sidecar

Status: first-stage implemented client sidecar  
Slice type: client read sidecar  
Parent backend slice: `SL-REQ-002 — My Requests List`  
Source scenario/UI sources:

```text
planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md
planning/diagrams/scenario-data/SC-05-my-requests-data.md
planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md
planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md
```

Current implementation status: first-stage client list page implemented; filters and details are separate sidecars.

## 1. Sidecar Overview

Observable client behavior:

```text
Authenticated client opens My Requests page
        ↓
Client UI fetches own requests
        ↓
System derives current account from L1 auth session
        ↓
System returns requests owned by this account
        ↓
Client sees list, empty state, loading state, sign-in-required state or error state
```

Each request summary shows enough data to identify the request and see current status.

## 2. Visual UI / Scenario Flow

```text
[Client]
opens /requests
        ↓
[Page]
checks session state
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ session exists               │ no session                   │
 ▼                              ▼
[Page]                         [Sign-in required state]
fetches My Requests             link to login
        ↓
 ┌──────────────┬───────────────┬──────────────┐
 │ loading      │ success       │ error        │
 ▼              ▼               ▼
Loading text    List/empty      Page error
```

## 3. Visual Client Implementation Flow

```text
[Route]
clientRoutes.requests -> /requests
        ↓
[Page]
pages/requests/my/MyRequestsPage.tsx
owns session branch and page layout
        ↓
[Entity Query]
entities/request/model/useMyRequestsQuery.ts
loads current account request summaries
        ↓
[Shared API]
shared/api/l1RequestApi.ts
GET /api/l1/requests
        ↓
[Feature UI]
features/request/my-requests-list/ui/MyRequestsList.tsx
renders list or empty state
```

## 4. Client Implementation Flow

| Step | Layer | Responsibility | Current status |
|---|---|---|---|
| I01 | Route/page | `/requests` route renders `MyRequestsPage`. | implemented |
| I02 | Page | Page checks session and renders sign-in-required branch. | implemented |
| I03 | Entity query | Query loads current account My Requests summaries. | implemented |
| I04 | Shared API | Wrapper calls `GET /api/l1/requests`. | implemented |
| I05 | Feature UI | List/empty/loading/error state rendered. | implemented |
| I06 | Filter UI | Status filter controls and URL query state. | separate sidecar |
| I07 | Details link | Link from request card to details page. | details sidecar |

## 5. Client API / Generated Contract

Current shared API wrapper uses generated OpenAPI DTO type for `L1MyRequestSummaryDto`.

Current first-stage wrapper calls unfiltered:

```text
GET /api/l1/requests
```

Filter support belongs to:

```text
planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
```

## 6. Questions / Decisions

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-MYREQ-LIST-CLIENT-001` | accepted | Should list sidecar include filtering? | No. Filter architecture/status filter is a separate sidecar. |
| `Q-MYREQ-LIST-CLIENT-002` | accepted | Should list sidecar include details page? | No. Details page is a separate sidecar. |
| `Q-MYREQ-LIST-CLIENT-003` | future review | Should request card summary change? | Keep current summary until UI/source behavior needs a different identification format. |

## 7. Behavior Coverage

| Source behavior item | How sidecar covers it | Status |
|---|---|---|
| `SC-05-BI-001` Client can view own requests list | `/requests` page loads My Requests list. | covered |
| `SC-05-BI-002` Empty own requests list is normal | UI renders empty state from empty response. | covered |
| `SC-05-BI-003` Request summary identifies a request | Summary card/list renders backend summary data. | covered |
| `SC-05-BI-004` Request status is visible in list | UI renders request status. | covered |
| `SC-05-BI-005` Client can filter by status | Delegated to filter sidecar. | separate |
| `SC-05-BI-009` Client can open own request details | Delegated to details sidecar. | separate |

## 8. Client / Component / E2E Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| My Requests page renders loading/list/empty/error branches | Read UI states | component/client | implemented/planned per current test state |
| API wrapper calls `/api/l1/requests` | Correct endpoint | shared API | implemented |
| E2E create request -> My Requests shows created request | Cross-layer visible outcome | E2E | implemented/planned depending on current test state |
| Status filter behavior | URL/filter/query mapping | client/component/E2E | separate sidecar |
| Details navigation | Card link to detail route | client/component/E2E | details sidecar |

E2E should assert visible list outcome, not internal query/cache mechanics.

## 9. Dependent / Follow-up Slices

```text
L1-MY-REQUESTS-LIST-FILTERS.client
L1-MY-REQUEST-DETAILS.client
future request creation client sidecar
```
