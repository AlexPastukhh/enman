# L1-MY-REQUEST-DETAILS.client — My Request Details Client Sidecar

Status: implementation-ready client sidecar  
Parent slice: `SL-REQ-003 — Own Request Details`  
Slice type: client read sidecar  
Source scenario/UI sources:

```text
planning/diagrams/scenario-text-specs/SC-05-my-requests-own-request-details.md
planning/diagrams/scenario-data/SC-05-my-requests-data.md
planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md
planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md
```

Backend already exists:

```text
GET /api/l1/requests/{requestId}
```

Current client implementation status: not implemented in current client code at time of this documentation pass. Current router has `/requests` but no `/requests/:requestId` route, and shared request API has `listMyRequests` but no `getMyRequestDetails` wrapper.

Depends on:

```text
L1-MY-REQUESTS-READ-LIST.client
```

Out of scope:

```text
status filtering
request creation
employee review UI
editing/resubmission
agreement/documents/notifications
backend implementation
```

## 1. Sidecar Overview

The details page is a read context for one request owned by the authenticated client.

Client does not submit or choose `accountId`.

Client does not verify ownership locally. Backend scopes details by current account and returns `404` when the request is missing or not owned by the current account.

## 2. Sources / Source Behavior Items

Relevant source items:

```text
SC-05-BI-009 — Client can open own request details.
SC-05-BI-010 — Client sees only own request details.
SC-05-BI-011 — Missing or not-owned request shows not-found state.
SC-05-BI-012 — Client sees submitted request data.
SC-05-BI-013 — Client sees request metadata.
SC-05-BI-014 — In-review request has no fake review result.
SC-05-BI-015 — Approved request shows approval result.
SC-05-BI-016 — Rejected request shows rejection reason.
SC-05-BI-017 — Client can return to My Requests.
```

## 3. Visual UI / Scenario Flow

```text
[Authenticated Client]
opens My Requests
        ↓
selects one request
        ↓
[Client UI]
opens /requests/:requestId
        ↓
[Client UI]
fetches own request details
        ↓
[System]
derives current account from L1 auth session
        ↓
[System]
returns request details only if request belongs to current account
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ request found                │ request missing / not own    │
 ▼                              ▼
[Details Page]                 [Not Found State]
shows submitted request         request not found message
and review state                link back to My Requests
        ↓
[Details Page]
shows:
  request status
  request type
  created date
  submitted details
  object address
  review result when present
```

## 4. UI Slice Flow

| Step | UI/system | Behavior | Source item | Scope status |
|---|---|---|---|---|
| F01 | Client | Opens details from My Requests list. | `SC-05-BI-009` | in scope |
| F02 | Page | Reads and parses `requestId` route param. | route state | in scope |
| F03 | Client/API | Fetches details for current account. | `SC-05-BI-010` | in scope |
| F04 | Details view | Shows metadata and submitted request data. | `SC-05-BI-012` / `SC-05-BI-013` | in scope |
| F05 | Details view | Handles `reviewResult = null`. | `SC-05-BI-014` | in scope |
| F06 | Details view | Shows approved decision/date. | `SC-05-BI-015` | in scope |
| F07 | Details view | Shows rejected reason/date. | `SC-05-BI-016` | in scope |
| F08 | Not-found view | Shows not-found state for 404. | `SC-05-BI-011` | in scope |
| F09 | Navigation | Provides link back to My Requests. | `SC-05-BI-017` | in scope |

## 5. Visual Client Implementation Flow

```text
[Route/Page]
pages/requests/details/MyRequestDetailsPage.tsx
route: /requests/:requestId
        ↓
[Page]
reads route param requestId
validates/parses it
        ↓
[Entity Query]
useMyRequestDetailsQuery(requestId, enabled)
        ↓
[Shared API]
GET /api/l1/requests/{requestId}
        ↓
 ┌───────────────┬───────────────┬───────────────┬───────────────┐
 │ loading       │ success       │ 404           │ error         │
 ▼               ▼               ▼               ▼
Loading state    Details view    Not found       Page error
                                  state
```

Suggested placement:

```text
src/pages/requests/details/MyRequestDetailsPage.tsx
src/pages/requests/details/myRequestDetailsPage.css

src/entities/request/
  api/getMyRequestDetails.ts
  model/useMyRequestDetailsQuery.ts
  model/requestQueryKeys.ts
  model/requestTypes.ts

src/features/request/my-request-details/
  ui/MyRequestDetailsView.tsx
  ui/MyRequestSubmittedData.tsx
  ui/MyRequestReviewResult.tsx
  ui/MyRequestDetailsNotFound.tsx
  ui/myRequestDetailsConst.ts

src/shared/api/l1RequestApi.ts
```

Route direction:

```text
/requests/:requestId
```

Entry point from list:

```text
MyRequestSummaryCard
  → link to clientRoutes.requestDetails(request.requestId)
```

## 6. Client Implementation Flow

| Step | Layer | Responsibility | Status |
|---|---|---|---|
| I01 | Shared config | Add `clientRoutes.requestDetails(requestId)` helper or equivalent route builder. | planned |
| I02 | Router | Add `/requests/:requestId` route. | planned |
| I03 | Page | Parse route param and handle invalid param safely. | planned |
| I04 | Shared API | Add `getMyRequestDetails(requestId)`. | planned |
| I05 | Entity query | Add query key and `useMyRequestDetailsQuery`. | planned |
| I06 | Feature UI | Render details view/submitted data/review result/not-found. | planned |
| I07 | List UI | Add link from summary card to details page. | planned |
| I08 | Tests | Add component/API/E2E tests. | planned |

## 7. Client API / Generated Contract

Backend DTO shape:

```text
requestId
requestType
status
createdAt
submittedRequest:
  details
  objectAddress
reviewResult:
  decision
  decidedAt
  rejection:
    reason
```

Target shared API:

```ts
getMyRequestDetails(requestId: number): Promise<L1MyRequestDetails>
```

Use generated OpenAPI response type. Do not duplicate handwritten DTO shapes except as aliases to generated types.

404 must be surfaced so page state can render not-found.

## 8. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-REQ-DETAILS-CLIENT-001` | accepted | Is this details-only or details + list update? | Implement details page and add details link from My Requests list. Do not change filters/create behavior here. | slice boundary |
| `Q-REQ-DETAILS-CLIENT-002` | accepted | Where does `requestId` live? | Route state belongs to the page. Page parses route param and passes id into entity query. | routing/query boundary |
| `Q-REQ-DETAILS-CLIENT-003` | accepted | What happens for request not found or another account request? | Show not-found state, not generic crash/error. | UX/security semantics |
| `Q-REQ-DETAILS-CLIENT-004` | accepted | What review states should UI support? | `reviewResult = null`, approved, rejected with reason. | rendering branches |
| `Q-REQ-DETAILS-CLIENT-005` | accepted | Should details page expose applicant/account ids? | No. Details DTO is enough for this client read context. | API/UI minimization |
| `Q-REQ-DETAILS-CLIENT-006` | future review | Should details page offer create-new-from-feedback? | Not in this first details sidecar; add later when request creation flow supports it. | future rejected flow |

## 9. Client Extension / Change Points

| ID | Type | Area | Current direction | Status |
|---|---|---|---|---|
| `CP-REQ-DETAILS-CLIENT-001` | extension | rejected feedback action | Render reason now; create-new-from-feedback is future. | future review |
| `CP-REQ-DETAILS-CLIENT-002` | extension | agreement/documents | Do not render missing future payloads. | deferred |
| `CP-REQ-DETAILS-CLIENT-003` | ownership boundary | not-owned/missing | Treat both as not-found UI via backend 404. | accepted |

## 10. Behavior Coverage

| Source behavior item | How sidecar covers it | Draft location | Status |
|---|---|---|---|
| `SC-05-BI-009` Client can open own request details | Adds details route/page and list entry link. | UI / Implementation Flow | covered as draft |
| `SC-05-BI-010` Client sees only own request details | Uses backend scoped endpoint; UI does not do ownership checks. | API / Questions | covered |
| `SC-05-BI-011` Missing or not-owned request shows not-found state | 404 renders not-found state. | UI Flow / Tests | covered |
| `SC-05-BI-012` Client sees submitted request data | Details view renders `submittedRequest`. | UI Flow / API Contract | covered |
| `SC-05-BI-013` Client sees request metadata | Details view renders status/type/created date. | UI Flow | covered |
| `SC-05-BI-014` In-review request has no fake review result | UI handles `reviewResult = null`. | UI Flow / Tests | covered |
| `SC-05-BI-015` Approved request shows approval result | UI renders approved decision/date. | UI Flow / Tests | covered |
| `SC-05-BI-016` Rejected request shows rejection reason | UI renders rejection reason/date. | UI Flow / Tests | covered |
| `SC-05-BI-017` Client can return to My Requests | Details/not-found view links back. | UI Flow / Tests | covered |

## 11. Client / Component / E2E Verification Plan

### Component/client tests

| Test / check | Verifies | Status |
|---|---|---|
| Details page shows loading state | Query pending state visible | planned |
| Details view renders request status/type/date | Header metadata visible | planned |
| Details view renders submitted details | Request details text visible | planned |
| Details view renders object address | Address visible | planned |
| InReview details renders no review result block | `reviewResult = null` handled | planned |
| Approved details renders decision | Approved review result visible | planned |
| Rejected details renders rejection reason | Rejection feedback visible | planned |
| 404 renders not-found state | Missing/not-owned request UI | planned |
| Invalid route param renders not-found or safe error | Bad URL does not crash | planned |
| Back link navigates to My Requests | User can return to list | planned |

Do not add client tests for ownership isolation beyond 404 handling. Ownership is backend behavior.

### Shared API tests

| Test / check | Verifies | Status |
|---|---|---|
| `getMyRequestDetails(id)` calls `/api/l1/requests/{id}` | Correct endpoint | planned |
| API uses generated response type | No duplicated handwritten DTO | planned |
| 404 is surfaced for page state handling | Not-found branch can be rendered | planned |

### E2E happy path

```text
register/login client
        ↓
create applicant party via setup
        ↓
create connection request via setup
        ↓
open /requests
        ↓
click created request card/details link
        ↓
assert details page heading/status visible
        ↓
assert submitted details visible
        ↓
assert object address visible
```

### E2E not-found path

```text
register/login client
        ↓
open /requests/999999
        ↓
assert not-found state visible
        ↓
assert link back to My Requests visible
```

No E2E should assert “backend checked ownership” internally. It should assert visible behavior: own request details appear; missing request shows not-found.

## 12. Covered Scenario / UI Behavior Items

```text
SC-05-BI-009
SC-05-BI-010
SC-05-BI-011
SC-05-BI-012
SC-05-BI-013
SC-05-BI-014
SC-05-BI-015
SC-05-BI-016
SC-05-BI-017
```

## 13. Dependent / Follow-up Slices

```text
L1-MY-REQUESTS-READ-LIST.client
future create-new-from-feedback client behavior
future request creation client sidecar
future agreement/documents details extensions
```

## 14. Implementation Checklist

```text
[ ] Add route helper: clientRoutes.requestDetails(requestId).
[ ] Add route: /requests/:requestId.
[ ] Extend shared/api/l1RequestApi: getMyRequestDetails(requestId).
[ ] Extend entities/request: getMyRequestDetails / useMyRequestDetailsQuery / requestQueryKeys.myRequestDetails(requestId).
[ ] Add details feature UI components.
[ ] Update MyRequestSummaryCard with details link.
[ ] Add component tests.
[ ] Add shared API tests.
[ ] Add E2E happy path.
[ ] Add E2E not-found path.
```
