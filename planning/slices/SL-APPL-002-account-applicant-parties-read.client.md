# SL-APPL-002.client — Account Applicant Parties Read / Templates

Status: full client read sidecar draft / target backend contract accepted / blocked until backend endpoint and generated types exist  
Parent slice: `SL-APPL-002 — Account Applicant Parties Read / Templates`  
Slice type: client read sidecar  
Architecture direction: read slice maps to pages + entities; features are reserved for command / user-action behavior.

## 1. Scope

This read sidecar owns:

```text
- Applicant Parties page / section read state for signed-in client;
- reading all account ApplicantParties from flat account list contract;
- showing current/default templates in a top visual area;
- showing other saved ApplicantParties below;
- showing empty, loading and error read states;
- using server-truth current/default state from returned ApplicantParty data;
- keeping read-specific ApplicantParty display UI in entities/applicant-party.
```

## 2. Out of Scope

```text
- add Individual ApplicantParty form/action -> SL-APPL-001.client;
- create success behavior / validation feedback / query invalidation -> SL-APPL-001.client;
- explicit make default/current action -> SL-APPL-003;
- delete/archive/edit lifecycle -> future ApplicantParty lifecycle slice;
- request creation applicant picker -> future SL-REQ-001.client;
- backend endpoint implementation -> parent SL-APPL-002 backend;
- OpenAPI generation work -> generation/check workflow, not this draft;
- separate ApplicantParty details page -> not required by current direction;
- separate “My Applicant Parties” page -> not current scenario direction;
- removing old current-individual endpoint/client code -> compatibility cleanup task.
```

## 3. Related Slices / Owners

```text
SL-APPL-001.client
  owns add Individual ApplicantParty command/action and create-result behavior.

SL-APPL-002 backend
  owns flat account ApplicantParties read endpoint.

SL-APPL-003
  owns explicit make default/current behavior.

Future ApplicantParty lifecycle slice
  owns edit/delete/archive behavior.

Future SL-REQ-001.client
  owns request creation applicant selection/prefill.
```

## 4. Visual UI / Scenario Flow

Source UI model:

```text
current/default templates top area,
other saved cards below,
one Applicant Parties page / section.
```

```text
[Signed-in Client]
opens Applicant Parties page / section
        ↓
[Page]
shows Applicant Parties read area
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ applicant parties exist      │ no applicant parties yet     │
 ▼                              ▼
Client sees saved               Client sees empty read state
ApplicantParty cards
        ↓
[Top Area]
Client sees current/default templates
visually highlighted
        ↓
[Below]
Client sees other saved ApplicantParties
as regular cards
```

In ordinary words:

```text
A signed-in client opens the Applicant Parties area. If the account has saved ApplicantParties, the page shows them as cards. Cards that represent current/default templates are shown first in a highlighted top area. All other saved ApplicantParties are shown below as regular cards. If the account has no ApplicantParties yet, the user sees an empty read state instead of a list.
```

Empty read state:

```text
[Signed-in Client]
opens Applicant Parties page / section
        ↓
No ApplicantParties exist yet
        ↓
Client sees that no saved ApplicantParties are available yet
```

Multiple saved parties state:

```text
[Signed-in Client]
has multiple saved ApplicantParties
        ↓
Client sees current/default ApplicantParty templates highlighted
        ↓
Client sees other saved ApplicantParties below
        ↓
Only cards marked by saved account state appear as current/default
```

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Signed-in client | Opens Applicant Parties page / section. |
| S02 | Page read area | Shows Applicant Parties read state. |
| S03 | Empty state | Shows that no saved ApplicantParties are available yet. |
| S04 | Saved cards | Shows saved ApplicantParty cards when account has ApplicantParties. |
| S05 | Current/default top area | Shows current/default templates visually highlighted. |
| S06 | Other saved area | Shows non-default saved ApplicantParties below the current/default area. |
| S07 | Saved account state | Does not silently highlight another same-type card unless saved account state marks it current/default. |

## 5. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/account/AccountPage.tsx
or Applicant Parties page / section

Lives here:
  AccountPage
  page-level branch logic:
    session exists / no session
    loading / error / empty / success
  page-level composition:
    ApplicantPartiesList

Uses:
  useSession()
  useAccountApplicantPartiesQuery({ enabled: Boolean(session) })

Owns:
  session branch
  page layout
  read-state composition
  passing loaded read data to entity display UI

Does not own:
  fetchJson
  generated DTO aliases
  query key definitions
  card/list rendering internals
  command form internals
        ↓

[Entity Query Layer]
entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
entities/applicant-party/model/applicantPartyQueryKeys.ts
entities/applicant-party/model/applicantPartyTypes.ts

Lives here:
  useAccountApplicantPartiesQuery()
  applicantPartyQueryKeys.accountList
  AccountApplicantPartiesState
  ApplicantPartySummary

Uses:
  useQuery()
  listAccountApplicantParties()

Owns:
  React Query read hook
  query key
  query function binding
  enabled guard
  entity read type aliases

Does not own:
  route/session decision itself
  visible page branches
  HTTP path string
  command mutation
        ↓

[Entity API Layer]
entities/applicant-party/api/listAccountApplicantParties.ts

Lives here:
  listAccountApplicantParties()

Uses:
  getAccountApplicantParties()
  from shared/api/l1ApplicantPartyApi.ts

Owns:
  entity-level read operation name
  delegating shared API response into ApplicantParty entity read flow

Does not own:
  React Query hook
  HTTP path constant
  component rendering
  command success behavior
        ↓

[Shared API Layer]
shared/api/l1ApiPaths.ts
shared/api/l1ApplicantPartyApi.ts

Lives here:
  l1ApiPaths.accountApplicantParties
  getAccountApplicantParties()

Type aliases:
  L1AccountApplicantPartiesResponse =
    components["schemas"]["L1AccountApplicantPartiesResponse"]

  L1ApplicantPartySummary =
    components["schemas"]["L1ApplicantPartySummaryDto"]

Uses:
  fetchJson()
  generated OpenAPI types

Owns:
  low-level HTTP call:
    GET /api/l1/applicant-parties
  generated response type alias
  stable API path constant

Does not own:
  React Query
  page state
  current/default display decisions
  visual card layout
        ↓

[Generated Contract Layer]
shared/api/generated/openapi-types.ts

Lives here:
  paths["/api/l1/applicant-parties"]
  path and operation generated for GET /api/l1/applicant-parties
  exact operation id taken from generated OpenAPI after implementation
  components["schemas"]["L1AccountApplicantPartiesResponse"]
  components["schemas"]["L1ApplicantPartySummaryDto"]

Owns:
  generated structural API contract

Does not own:
  handwritten client logic
  manual edits
  page/UI decisions
        ↓

[Entity Display UI Layer]
entities/applicant-party/ui/ApplicantPartiesList.tsx
entities/applicant-party/ui/ApplicantPartySummaryCard.tsx
entities/applicant-party/ui/ApplicantPartiesEmptyState.tsx
entities/applicant-party/ui/applicantPartiesListConst.ts
entities/applicant-party/ui/applicantPartiesList.css

Lives here:
  ApplicantPartiesList
  ApplicantPartySummaryCard
  ApplicantPartiesEmptyState
  applicantPartiesListConst

Props:
  applicantParties: ApplicantPartySummary[]

or, if split happens before render:
  currentDefaults: ApplicantPartySummary[]
  otherSaved: ApplicantPartySummary[]

Owns:
  current/default top area
  highlighted current/default cards
  other saved regular cards
  empty read state
  read-only ApplicantParty labels/styles

Does not own:
  fetching
  query keys
  session branch
  create mutation
  make-default action
```

In ordinary words:

```text
The page is responsible for deciding whether the client is signed in and which read state should be visible: loading, error, empty or success. The page does not call HTTP directly. It calls an ApplicantParty entity query hook. That entity query hook owns the React Query setup and calls an entity API function. The entity API function delegates to the shared API wrapper. The shared API wrapper is the only handwritten client layer that knows the concrete HTTP endpoint. Generated OpenAPI types define the response contract. After data is loaded, read-only ApplicantParty UI components under entities/applicant-party/ui render the current/default area, other saved area and empty read state.
```

Implementation flow table:

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Route/page | Existing account/applicant page section renders Applicant Parties read context. |
| I02 | Page | Page checks session and renders signed-out/loading/error/success read branches. |
| I03 | Entity query | Query loads current account ApplicantParty summaries. |
| I04 | Entity API | Entity read operation delegates to shared API wrapper. |
| I05 | Shared API | Wrapper calls `GET /api/l1/applicant-parties`. |
| I06 | Entity display UI | `ApplicantPartiesList` renders current/default area, other saved area or empty read state. |

Important architecture decision for this sidecar:

```text
ApplicantPartiesList is read/display UI.
Because this is a read slice, keep it under entities/applicant-party/ui,
not features/applicant-party/*.

features/applicant-party/* remains for command/user action flows:
  create individual ApplicantParty
  future make default/current
  future edit/archive/delete
```

## 6. Client API / Server Contract

This client sidecar consumes the server contract from parent `SL-APPL-002`.

Server endpoint:

```text
GET /api/l1/applicant-parties
```

Server response shape:

```ts
type L1AccountApplicantPartiesResponse = {
  applicantParties: L1ApplicantPartySummaryDto[];
};

type L1ApplicantPartySummaryDto = {
  applicantPartyId: number;
  applicantPartyType: "Individual" | "IndividualEntrepreneur" | "LegalEntity";
  displayName: string;
  fullName?: L1FullNameDto | null;
  email?: string | null;
  phoneNumber?: string | null;
  verificationStatus: string;
  isCurrentDefault: boolean;
  createdAt?: string | null;
};
```

Client-side usage:

```text
shared/api/generated/openapi-types.ts
  generated structural contract

shared/api/l1ApplicantPartyApi.ts
  low-level wrapper for GET /api/l1/applicant-parties

entities/applicant-party/api/listAccountApplicantParties.ts
  entity-level read operation

entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
  React Query read hook used by page
```

Target shared API wrapper:

```ts
getAccountApplicantParties(): Promise<L1AccountApplicantPartiesResponse>
```

Target entity API:

```ts
listAccountApplicantParties(): Promise<L1AccountApplicantPartiesResponse>
```

Target entity query hook:

```ts
useAccountApplicantPartiesQuery({ enabled }: { enabled?: boolean })
```

Client rule:

```text
Client groups the flat server response for display.
Server does not return page layout groups.

current/default area:
  applicantParties where isCurrentDefault == true

other saved area:
  applicantParties where isCurrentDefault == false
```

## 7. Questions / Decisions

| ID | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `Q-SL-APPL-002-CLIENT-001` | accepted direction | Flat response or grouped response? | Use flat `applicantParties[]`; client read UI separates current/default vs other saved. | API/UI mapping. |
| `Q-SL-APPL-002-CLIENT-002` | accepted direction | Where does read fetching live? | Page calls entity query; shared API owns low-level HTTP wrapper. | Layering. |
| `Q-SL-APPL-002-CLIENT-003` | accepted direction | Where does read list UI live? | `entities/applicant-party/ui/*`, because this is read/display UI. | Corrects feature-read placement. |
| `Q-SL-APPL-002-CLIENT-004` | assumption | Existing `/account` section or new route? | Start from existing account/applicant section unless navigation decision changes. | Route/page scope. |
| `Q-SL-APPL-002-CLIENT-005` | accepted direction | Does this sidecar include create form? | No. Create belongs to `SL-APPL-001.client`; this sidecar may coexist on same page only. | Scope boundary. |
| `Q-SL-APPL-002-CLIENT-006` | accepted direction | Remove old current-individual client read now? | No. Stop using it for this read section; removal is cleanup. | Scope boundary. |
| `Q-SL-APPL-002-CLIENT-007` | future review | Show make-default/current action now? | No. Future `SL-APPL-003`. | Avoids command scope creep. |

## 8. Extension / Change Points

```text
- add Individual ApplicantParty action -> SL-APPL-001.client;
- explicit make default/current action -> SL-APPL-003;
- create success refresh/invalidation -> SL-APPL-001.client implementation note;
- delete/archive/edit lifecycle -> future ApplicantParty lifecycle slice;
- IndividualEntrepreneur / LegalEntity create flows -> future type-specific slices;
- request creation applicant picker -> future SL-REQ-001.client;
- old current-individual cleanup -> compatibility cleanup task.
```

## 9. Behavior Coverage

| Source behavior item | How sidecar covers it | Status |
|---|---|---|
| `SC-10-BI-004` Account may store multiple ApplicantParties | Read UI renders multiple saved ApplicantParty cards from account list. | covered |
| `SC-10-BI-005` Account may have one current/default ApplicantParty template per applicant type | Current/default cards render in top area. | covered |
| `SC-10-BI-006` First ApplicantParty of a type may initialize current/default | First card can appear highlighted when saved account state marks it current/default. | covered |
| `SC-10-BI-007` Additional ApplicantParty of same type does not change current/default implicitly | Read UI reflects saved account state; additional same-type cards are regular unless marked current/default. | covered as read |
| `SC-10-UI-001` Signed-in client can open Applicant Parties page / section | Page/section exists for signed-in client. | covered |
| `SC-10-UI-002` Page shows current/default templates in top area | Entity display UI renders top area. | covered |
| `SC-10-UI-003` Current/default templates visually highlighted | Current/default cards are visually distinguished. | covered |
| `SC-10-UI-004` Other saved non-default ApplicantParties below | Entity display UI renders non-default saved area below. | covered |
| `SC-10-UI-005` Client can add individual ApplicantParty on same page | Owned by `SL-APPL-001.client`, not this read sidecar. | related/out of scope |
| `SC-10-UI-006` Created ApplicantParty appears without replacing existing cards | Owned by `SL-APPL-001.client`; this read UI can display resulting saved state. | related/out of scope |
| `SC-10-UI-009` Validation/error feedback | Owned by create form sidecar. | out of scope |
| `SC-10-UI-010` Explicit make default/current action | Owned by future `SL-APPL-003`. | future/out of scope |
| Delete/archive lifecycle | Future lifecycle slice only. | future/out of scope |

## 10. Client / Component / E2E Verification Plan

Component/model tests:

```text
- query hook uses applicantPartyQueryKeys.accountList;
- query hook calls entity API read operation;
- page does not call shared API directly;
- read UI renders loading/error/empty/success states;
- empty state is visible when applicantParties=[];
- current/default cards render in top highlighted area;
- non-default cards render below as regular cards;
- multiple saved ApplicantParties render without replacement;
- read display UI does not render create form internals;
- read display UI does not render make-default action.
```

Client API tests, if project has wrapper tests:

```text
- getAccountApplicantParties calls GET /api/l1/applicant-parties;
- listAccountApplicantParties delegates to shared API wrapper;
- generated response type aliases are used rather than handwritten DTOs.
```

E2E target after backend + client implementation:

```text
signed-in client opens Applicant Parties page / section
        ↓
waits for account ApplicantParties read to complete
        ↓
sees empty state if account has no ApplicantParties

signed-in client with saved ApplicantParties opens page
        ↓
sees current/default cards highlighted
        ↓
sees other saved ApplicantParties below
```

Do not assert:

```text
- React Query internals;
- exact refetch calls;
- generated type internals;
- implementation file names in E2E.
```

## 11. Implementation Checklist

```text
[ ] add shared API path constant for /api/l1/applicant-parties;
[ ] add generated type aliases in shared API wrapper;
[ ] add getAccountApplicantParties() shared API wrapper;
[ ] add entity API listAccountApplicantParties();
[ ] add applicantPartyQueryKeys.accountList;
[ ] add useAccountApplicantPartiesQuery();
[ ] add entity display components under entities/applicant-party/ui;
[ ] update page/section to use entity query hook;
[ ] render loading/error/empty/success read states;
[ ] render current/default highlighted top area;
[ ] render other saved ApplicantParties below;
[ ] keep create form/action in SL-APPL-001.client area;
[ ] keep make-default action out until SL-APPL-003;
[ ] do not hand-edit generated OpenAPI types.
```

## 12. Next Step

Implementation handoff should start only after backend `SL-APPL-002` endpoint and generated OpenAPI/types are available or included in the same implementation slice.

If implementation starts from current old client, replace the old single-current read usage for this page/section with the flat account list query, while leaving removal of compatibility endpoint/code as a separate cleanup task.
