# L1-APPLICANT-PARTY-READ-CURRENT.client — Account Page Current Applicant Read Sidecar

Status: draft / ready for client integration after backend endpoint exists  
Slice type: client sidecar  
Scope: Account page reads current individual applicant party state and chooses read-only view vs create form  
Source scenario/UI behavior items: readable working behavior items are used until authoritative scenario/UI IDs are attached  
Contract sources: `Shared/openapi.json`, `energymanagement.client/src/shared/api/generated/openapi-types.ts`

## 1. Slice Overview

Observable client behavior:

```text
Authenticated client opens Account page.
Client fetches current individual applicant party state.
If applicant party exists, Account page shows applicant data as read-only fields.
If applicant party is missing, Account page shows create applicant party form.
```

Why this is a client sidecar:

```text
- it consumes a new backend read endpoint;
- it decides Account page state;
- it connects existing create applicant party feature to stable server read state;
- it keeps applicant party state out of auth/session;
- it is the client counterpart of `L1-APPLICANT-PARTY-READ-CURRENT`.
```

Out of scope:

```text
- applicant party create command implementation, except invalidating/refetching read state after success;
- applicant party edit flow;
- verification workflow actions;
- create request form or create request entry;
- My Requests page.
```

## 2. Visual UI / Scenario Flow

```text
┌──────────────────────────────┐
│ Authenticated Client          │
└──────────────┬───────────────┘
               │ opens Account page
               ▼
┌──────────────────────────────┐
│ Account Page                  │
│ Fetch current applicant state │
└──────────────┬───────────────┘
               │
       ┌───────┴────────┐
       │                │
 applicant exists    applicant missing
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Read-only applicant   │   │ Create applicant party form   │
│ data view             │   │                               │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           │                              │ submit create command
           │                              ▼
           │                 ┌──────────────────────────────┐
           │                 │ Create success                │
           │                 │ Invalidate/refetch current    │
           │                 │ applicant party state         │
           │                 └──────────────┬───────────────┘
           │                                │
           └────────────────────────────────┘
                            ▼
                 ┌──────────────────────────┐
                 │ Read-only applicant view  │
                 │ + success notification    │
                 └──────────────────────────┘
```

UI notes:

```text
- Missing applicant state is normal and shows create form.
- Existing applicant state shows read-only fields.
- The create form should not store applicantPartyId for future request creation.
- Success after create should refetch/read current applicant party state once the endpoint exists.
```

## 3. Visual Client Implementation Flow

```text
[Page: pages/account/AccountPage.tsx]
Composes account page state
        ↓
[Entity: entities/applicant-party]
useCurrentIndividualApplicantPartyQuery()
        ↓
[Shared API: shared/api/l1ApplicantPartyApi.ts]
GET /api/l1/applicant-parties/current-individual
        ↓
[Generated Contracts]
L1CurrentIndividualApplicantPartyResponse
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ exists=true                  │ exists=false                 │
 ▼                              ▼
[Feature/UI]                   [Feature/UI]
ApplicantPartyReadOnlyView      CreateIndividualApplicantPartyForm
        │                              │
        │                              │ on create success
        │                              ▼
        │                       [React Query]
        │                       invalidate/refetch current applicant query
        └──────────────┬───────────────┘
                       ▼
[Account Page]
Displays stable server-backed applicant state
```

Suggested placement:

```text
src/entities/applicant-party/
  api/getCurrentIndividualApplicantParty.ts
  model/applicantPartyTypes.ts
  model/applicantPartyQueryKeys.ts
  model/useCurrentIndividualApplicantPartyQuery.ts

src/shared/api/l1ApplicantPartyApi.ts
  getCurrentIndividualApplicantParty()

src/pages/account/AccountPage.tsx
  consumes current applicant party query

src/features/applicant-party/create-individual/
  on success:
    invalidate applicant party query
```

## 4. Client API Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Error constants used | Status |
|---|---|---|---|---|
| `getCurrentIndividualApplicantParty` | `GET /api/l1/applicant-parties/current-individual` | `components["schemas"]["L1CurrentIndividualApplicantPartyResponse"]` | none expected for normal missing state | target |
| `createIndividualApplicantParty` | `POST /api/l1/applicant-parties/individual` | existing create DTO/response types | ProblemDetails mapping for command validation | related |

Client API behavior:

```text
- 200 exists=true -> return applicant data.
- 200 exists=false -> return missing applicant state.
- 401 -> session/auth boundary; existing session handling should eventually redirect/show unauthenticated state.
- 500 -> shared API error path.
```

Missing applicant party must not be represented as thrown error in the client.

## 5. Questions / Decisions

### Q-APPL-READ-CLIENT-001 — Should applicant state live in session?

Question status: resolved

Question:  
Should current applicant party state be stored in `entities/session`?

Decision:  
No.

Applicant party state belongs to `entities/applicant-party`.

Impact:  
Creating applicant party should invalidate/refetch applicant party query, not session query.

Shared register:  
Local only.

---

### Q-APPL-READ-CLIENT-002 — Should create success use local submitted values or refetch?

Question status: accepted direction

Question:  
After create success, should UI use submitted values or refetch server state?

Assumption / current direction:  
After backend read endpoint exists, invalidate/refetch current applicant party query.

Impact:  
Account page displays server-backed read state and can include verificationStatus.

Shared register:  
Local only.

---

### Q-APPL-READ-CLIENT-003 — Should applicantPartyId be stored in client state?

Question status: resolved

Question:  
Should the client store applicantPartyId from read/create flow?

Decision:  
No for this Account page/read flow.

Impact:  
Create request flow remains server-selected current applicant party. Future edit/details flow can add explicit identity handling if needed.

Shared register:  
Local for now.

---

### Q-APPL-READ-CLIENT-004 — What should Account page show while loading?

Question status: assumption

Question:  
Should Account page show skeleton/loading text while applicant party state is loading?

Assumption / current direction:  
Show simple account/applicant loading state.

Impact:  
Can be replaced by shared skeleton/spinner UI later.

Shared register:  
Local only.

---

### Q-APPL-READ-CLIENT-005 — How should verificationStatus be displayed?

Question status: future review

Question:  
What exact labels and visuals should be used for verification status?

Assumption / current direction:  
Display raw/basic status text initially if available; final labels belong to future verification UI convention.

Impact:  
Can affect UI text constants and component tests later.

Shared register:  
Mirror to extension-points register if not already mirrored by backend slice.

## 6. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How sidecar covers it | Draft/file location | Status |
|---|---|---|---|
| Account page fetches current applicant state | Adds applicant-party entity query consumed by Account page. | Visual Client Implementation Flow | covered |
| Missing applicant state shows create form | `exists=false` maps to create form branch. | Visual UI Flow | covered |
| Existing applicant state shows read-only data | `exists=true` maps to read-only applicant view. | Visual UI Flow | covered |
| Applicant party state is separate from session | Uses `entities/applicant-party`, not `entities/session`. | Questions / Decisions | covered |
| Create success refreshes read state | Create feature invalidates/refetches applicant party query. | Visual UI Flow / Questions | covered |
| Verification status can be displayed | Read response includes verificationStatus; UI can display it. | API Contract / Questions | covered |
| Client does not depend on applicantPartyId | No state or navigation by applicant id in this sidecar. | Questions / Decisions | covered |

## 7. Client / Component / E2E Verification Plan

### Client/component tests

| Client/component test | Behavior/UI items | Client behavior covered | Locator/accessibility focus | Status |
|---|---|---|---|---|
| Account page shows create form for `exists=false` | Missing applicant state | Query result selects create form branch. | labels/heading | planned |
| Account page shows read-only view for `exists=true` | Existing applicant state | Query result selects read-only branch. | text/readonly fields | planned |
| Applicant read-only view shows full name and contact data | Existing applicant state | Returned data is visible. | text/labels | planned |
| Applicant read-only view shows verification status | Verification display | Status is visible. | text/status label | planned |
| Create success invalidates current applicant query | Create/read integration | Account page moves to server-backed read state after create. | visible read-only values | planned |
| Missing applicant response is not treated as error | Normal empty page state | `exists=false` branch does not show root API error. | positive branch behavior | planned |

Do not add standalone tests whose main purpose is proving unrelated UI does not exist.

### E2E

E2E is useful after backend + client integration exist.

| E2E test | Scenario/UI items | Cross-layer purpose | Setup | User path | Server communication checked | Expected result | Status |
|---|---|---|---|---|---|---|---|
| Account page shows create form when applicant missing | Missing applicant state | browser -> client -> GET read endpoint -> UI branch | registered/login client without applicant | open `/account` | wait for `GET /api/l1/applicant-parties/current-individual` | create form visible | planned |
| Account page shows read-only applicant after create | Existing applicant state | browser -> POST create -> refetch read -> UI branch | registered/login client | submit applicant form | wait for POST create and GET current applicant | read-only applicant data visible | planned |

Detailed validation stays in component/client tests.

## 8. Covered Scenario / UI Behavior Items

### Account page fetches current applicant state

When an authenticated client opens Account page, the client fetches current individual applicant party state.

### Missing applicant state shows create form

If the current applicant party is missing, Account page shows the create applicant party form.

### Existing applicant state shows read-only data

If the current applicant party exists, Account page shows applicant party data as read-only fields.

### Applicant state is separate from session

Applicant party state is loaded through applicant-party entity state, not current-user/session state.

### Create success refreshes current applicant state

After successful applicant party creation, the Account page refreshes current applicant party state.

### Verification status can be displayed

When returned by the read endpoint, verification status can be shown on Account page.

## 9. Next Step

Implementation direction after backend endpoint exists:

```text
Add generated OpenAPI type for read response
        ↓
Add `getCurrentIndividualApplicantParty` to shared applicant party API
        ↓
Add `entities/applicant-party` query + query key
        ↓
Update AccountPage to branch on `exists`
        ↓
Update create applicant party success handler to invalidate applicant query
        ↓
Add component/client tests
        ↓
Add E2E coverage for Account page read branch
```

Likely changed client files:

```text
energymanagement.client/src/shared/api/l1ApplicantPartyApi.ts
energymanagement.client/src/shared/api/l1ApiPaths.ts
energymanagement.client/src/entities/applicant-party/**
energymanagement.client/src/pages/account/AccountPage.tsx
energymanagement.client/src/features/applicant-party/create-individual/**
tests/e2e/applicant-party/*.spec.ts
planning/slices/l1/L1-APPLICANT-PARTY-READ-CURRENT.client.md
```

Checks:

```powershell
npm.cmd --prefix energymanagement.client run build
npm.cmd --prefix energymanagement.client run test
npm.cmd run check:api
npm.cmd run test:e2e -- --list
npm.cmd run test:e2e
```
