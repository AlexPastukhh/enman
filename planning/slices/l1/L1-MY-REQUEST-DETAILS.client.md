# L1-MY-REQUEST-DETAILS.client — My Request Details Client Sidecar

Status: early client draft  
Slice type: client read sidecar  
Scope: authenticated client opens one **My Request Details** page and sees submitted request data + review outcome when available  
Backend source: `GET /api/l1/requests/{requestId}`  
Depends on: `L1-MY-REQUESTS-READ-LIST.client` for entry/link from list  
Out of scope: status filtering, request creation, employee review UI, editing/resubmission

## 1. Sidecar Overview

Observable client behavior:

```text
Authenticated client opens My Requests
        ↓
selects one request
        ↓
client opens request details page
        ↓
client fetches own request details
        ↓
request belongs to current account?
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ yes                          │ no / missing                 │
 ▼                              ▼
Details page shows               Not-found state
submitted request data           + link back to My Requests
and review state
```

The details page is a read context for one request owned by the authenticated client.

Client does not submit or choose `accountId`.

Client does not verify ownership locally. Backend scopes details by current account and returns `404` when the request is missing or not owned by the current account.

## 2. Visual UI / Scenario Flow

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

## 3. Visual Client Implementation Flow

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

## 4. Questions / Decisions

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-REQ-DETAILS-CLIENT-001` | decided | Is this details-only or details + list update? | Implement details page and add details link from My Requests list. Do not change filters/create behavior here. |
| `Q-REQ-DETAILS-CLIENT-002` | decided | Where does `requestId` live? | Route state belongs to the page. Page parses route param and passes id into entity query. |
| `Q-REQ-DETAILS-CLIENT-003` | accepted | What happens for request not found or another account request? | Show not-found state, not generic crash/error. |
| `Q-REQ-DETAILS-CLIENT-004` | accepted | What review states should UI support? | `reviewResult = null`, approved, rejected with reason. |
| `Q-REQ-DETAILS-CLIENT-005` | decided | Should details page expose applicant/account ids? | No. Details DTO is enough for this client read context. |

## 5. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How draft covers it | Status |
|---|---|---|
| Authenticated client can open request details | Adds `/requests/:requestId` route/page | covered |
| Client sees only own request details | Backend scopes by current account; another account returns 404 | covered |
| Client sees request status | Details view renders `status` | covered |
| Client sees request type | Details view renders `requestType` | covered |
| Client sees created date | Details view renders `createdAt` | covered |
| Client sees submitted request details | Details view renders `submittedRequest.details` | covered |
| Client sees object address | Details view renders `submittedRequest.objectAddress` | covered |
| In-review request has no review result | UI handles `reviewResult = null` | covered |
| Approved request shows approval decision | UI renders approved review result | covered |
| Rejected request shows rejection reason | UI renders rejection reason | covered |
| Missing/not-owned request is not shown | UI renders not-found state on 404 | covered |
| Client can return to My Requests | Not-found/details page links back to `/requests` | covered |

## 6. Test / Verification Plan

### Component/client tests

| Test / check | Verifies |
|---|---|
| Details page shows loading state | Query pending state visible |
| Details view renders request status/type/date | Header metadata visible |
| Details view renders submitted details | Request details text visible |
| Details view renders object address | Address visible |
| InReview details renders no review result block | `reviewResult = null` handled |
| Approved details renders decision | Approved review result visible |
| Rejected details renders rejection reason | Rejection feedback visible |
| 404 renders not-found state | Missing/not-owned request UI |
| Invalid route param renders not-found or safe error | Bad URL does not crash |
| Back link navigates to My Requests | User can return to list |

Do not add client tests for ownership isolation beyond 404 handling. Ownership is backend behavior.

### Shared API tests

| Test / check | Verifies |
|---|---|
| `getMyRequestDetails(id)` calls `/api/l1/requests/{id}` | Correct endpoint |
| API uses generated response type | No duplicated handwritten DTO |
| 404 is surfaced for page state handling | Not-found branch can be rendered |

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
wait for GET /api/l1/requests/{requestId}
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

## 7. Next Step

Implementation direction:

```text
Add route helper:
  clientRoutes.requestDetails(requestId)

Add route:
  /requests/:requestId

Extend shared/api/l1RequestApi:
  getMyRequestDetails(requestId)

Extend entities/request:
  getMyRequestDetails.ts
  useMyRequestDetailsQuery.ts
  requestQueryKeys.myRequestDetails(requestId)

Add details feature UI:
  MyRequestDetailsView
  MyRequestSubmittedData
  MyRequestReviewResult
  MyRequestDetailsNotFound

Update MyRequestSummaryCard:
  link to details page

Add tests:
  component tests
  E2E happy path
  E2E not-found path
```

Likely changed files:

```text
energymanagement.client/src/shared/config/clientRoutes.ts
energymanagement.client/src/shared/api/l1RequestApi.ts

energymanagement.client/src/entities/request/api/getMyRequestDetails.ts
energymanagement.client/src/entities/request/model/useMyRequestDetailsQuery.ts
energymanagement.client/src/entities/request/model/requestQueryKeys.ts
energymanagement.client/src/entities/request/model/requestTypes.ts

energymanagement.client/src/features/request/my-request-details/ui/*
energymanagement.client/src/features/request/my-requests-list/ui/MyRequestSummaryCard.tsx

energymanagement.client/src/pages/requests/details/MyRequestDetailsPage.tsx

tests/e2e/requests/my-request-details.spec.ts
planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md
```

## 8. Scenario Flow

```text
Authenticated client opens My Requests
        ↓
Client selects a request
        ↓
Client opens request details page
        ↓
System loads request details for current account
        ↓
If request belongs to current account,
client sees submitted request data
        ↓
Client sees request status, type, created date, details and object address
        ↓
If review result exists,
client sees decision and rejection reason when rejected
        ↓
If request does not exist or belongs to another account,
client sees not-found state
        ↓
Client can return to My Requests
```

## 9. Behavior Items

### Client opens request details

The authenticated client opens details page for a request.

### Client sees own request details

The client sees details only for a request that belongs to the authenticated account.

### Client sees submitted request data

The page shows submitted request details and object address.

### Client sees request metadata

The page shows request status, request type and created date.

### In-review request shows no review result

If the request has no review result yet, the page shows submitted data and current status without fake feedback.

### Approved request shows approval result

If the request is approved, the page shows approved decision and decision date.

### Rejected request shows rejection reason

If the request is rejected, the page shows rejected decision, decision date and rejection reason.

### Missing or not-owned request shows not-found state

If the request is missing or not owned by the current client, the page shows a not-found state instead of request data.

### Client can return to My Requests

The details page provides navigation back to My Requests.

## 10. Verification Commands

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```
