# L1-MY-REQUEST-DETAILS.client — My Request Details

Status: implemented first-stage client / layering normalization note  
Parent slice: `SL-REQ-003 — Own Request Details`  
Slice type: client read sidecar  
Architecture direction: details display is entity read UI; route/page owns route param parsing and state composition.

## 1. Scope

This sidecar owns:

```text
- own request details page/route behavior;
- reading details for the route request id;
- loading/error/not-found/success details states;
- rendering request details fields and review result when available;
- linking back to My Requests.
```

## 2. Out of Scope

```text
- backend details endpoint -> SL-REQ-003;
- list page -> SL-REQ-002 / L1-MY-REQUESTS-READ-LIST.client;
- filters -> L1-MY-REQUESTS-LIST-FILTERS.client;
- create-new-from-feedback action -> future request creation/details extension;
- employee review actions -> future employee slices.
```

## 3. Layering Correction

Request details display is read-only entity UI.

Target placement for new or normalized code:

```text
pages/requests/details/MyRequestDetailsPage.tsx
entities/request/model/useMyRequestDetailsQuery.ts
entities/request/api/getMyRequestDetails.ts
entities/request/ui/MyRequestDetailsView.tsx
entities/request/ui/MyRequestDetailsNotFoundState.tsx
shared/api/l1RequestApi.ts
```

Do not put read-only details rendering into a feature command folder.

## 4. Visual UI / Scenario Flow

```text
[Signed-in Client]
opens own request details route
        ↓
[Page]
loads request details by route request id
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ request exists and is owned  │ missing or not owned         │
 ▼                              ▼
Client sees request details     Client sees not-found state
        ↓
Client can navigate back to My Requests
```

## 5. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/requests/details/MyRequestDetailsPage.tsx

Lives here:
  MyRequestDetailsPage
  route param parsing
  loading/error/not-found/success branches

Uses:
  useParams()
  useMyRequestDetailsQuery(requestId)
  MyRequestDetailsView

Owns:
  route-level requestId extraction
  page state composition
  back navigation placement

Does not own:
  fetchJson
  generated DTO aliases
  details field rendering internals
        ↓

[Entity Query Layer]
entities/request/model/useMyRequestDetailsQuery.ts

Lives here:
  useMyRequestDetailsQuery()
  requestDetails query key

Uses:
  getMyRequestDetails(requestId)

Owns:
  React Query details hook
  enabled guard for valid request id

Does not own:
  route parsing
  visual details layout
        ↓

[Entity Display UI Layer]
entities/request/ui/MyRequestDetailsView.tsx

Lives here:
  MyRequestDetailsView
  details labels/sections
  not-found display component if split

Owns:
  read-only details display
  review result display

Does not own:
  route params
  backend API path
  create-new-from-feedback command
```

## 6. Behavior Coverage

| Source behavior | How sidecar covers it | Status |
|---|---|---|
| client opens own request details | route/page loads details by request id | covered |
| missing/not-owned request | not-found state | covered |
| InReview request details | details view can render no review result | covered |
| rejected request details | details view can render rejection reason when returned | covered |
| back to My Requests | page includes navigation back | covered |
| create-new-from-feedback | future extension only | future/out of scope |

## 7. Verification Plan

```text
- valid route id loads and renders details;
- missing/not-owned details renders not-found state;
- invalid route id is handled safely;
- back link navigates to My Requests;
- E2E asserts visible details/not-found state, not React Query internals.
```
