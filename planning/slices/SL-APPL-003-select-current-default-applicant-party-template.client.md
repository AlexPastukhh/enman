# SL-APPL-003.client — Make ApplicantParty Current/Default

Status: full client command sidecar draft / backend command implemented / client implementation-ready  
Parent slice: `SL-APPL-003 — Select Current/Default ApplicantParty Template`  
Slice type: client command sidecar  
Architecture direction: `pages + entities + features + shared/api + generated contracts`; read/display stays in `entities`, command/user action stays in `features`.

## 1. Sidecar Overview

This sidecar defines the client behavior for explicit make current/default action on the one Applicant Parties page / section.

The backend command is current implementation:

```text
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
```

The client action is a same-page explicit user action. It is not first-of-type default/current initialization during create.

First-of-type default/current initialization is already handled by:

```text
SL-APPL-001 — Create Individual ApplicantParty
ApplicantPartyCreationService
```

This sidecar owns:

```text
user sees saved ApplicantParty cards
        ↓
user explicitly chooses make current/default on a non-current/default card
        ↓
client submits selected applicantPartyId
        ↓
client shows pending/success/error feedback
        ↓
client refreshes account ApplicantParties read state
        ↓
selected same-type card becomes current/default in visible UI
        ↓
previous same-type default card becomes regular saved card
```

## 2. Current Implementation Status

Known current state:

```text
Backend:
  implemented endpoint:
  POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default

Backend handler:
  loads selected ApplicantParty owned by account;
  unsets other same-type current/default ApplicantParties;
  marks selected ApplicantParty current/default;
  SaveChanges;
  returns command success.

Client:
  account ApplicantParties flat read model exists;
  request creation page already consumes account ApplicantParties;
  old/current AccountPage replacement with full Applicant Parties page may still need reconciliation;
  make-current/default client action/wrapper/button is not confirmed implemented in shared paths/feature layer.
```

Implementation implication:

```text
This client sidecar is not blocked by backend command implementation.
It may still require generated API artifacts and shared client wrapper/path wiring.
```

Generated/API rule:

```text
Do not manually edit generated OpenAPI/TypeScript artifacts.
If endpoint is missing from generated contracts, run the project OpenAPI generation workflow.
```

## 3. Scope

This client sidecar owns:

```text
- same-page make current/default user action on Applicant Parties page / section;
- action available for saved non-current/default ApplicantParty cards;
- no action or disabled/no-op state for already current/default cards;
- explicit user selection of one saved ApplicantParty as current/default;
- command submit using selected applicantPartyId;
- visible pending feedback for selected action/card;
- visible success outcome through server-truth read state refresh;
- visible error feedback when command is rejected;
- account ApplicantParties read state refresh/update after success;
- selected ApplicantParty becomes highlighted as current/default after refresh;
- previous same-type current/default card becomes regular saved card after refresh;
- existing requests remain visually unchanged;
- generated-contract-aware shared API wrapper after API generation if needed.
```

## 4. Out of Scope

| Out-of-scope item | Owner / destination |
|---|---|
| Backend endpoint implementation | Already parent `SL-APPL-003` backend |
| Generated OpenAPI/type regeneration | API generation workflow, not manual client draft work |
| First-of-type default/current initialization | `SL-APPL-001` / `ApplicantPartyCreationService` |
| Account ApplicantParty flat read endpoint | `SL-APPL-002` backend |
| Account ApplicantParty read query/list display foundation | `SL-APPL-002.client` |
| Add/create ApplicantParty form | `SL-APPL-001.client` |
| Request creation applicant picker/default prefill | `SL-REQ-001.client` |
| Delete/archive/edit lifecycle | Future ApplicantParty lifecycle slices |
| Rename `IsCurrentActiveVersion` | Future naming cleanup |
| Rewriting existing requests | Explicitly not planned |
| Direct client-side durable state rewrite without server truth | Not allowed; server owns state |

## 5. Related Slices / Owners

```text
SL-APPL-001.client
  owns add Individual ApplicantParty command/action.

SL-APPL-002.client
  owns flat account ApplicantParties read query and read-only card/list UI.

SL-APPL-003 backend
  owns make current/default command endpoint.

SL-APPL-003.client
  owns same-page make current/default client action.

SL-REQ-001.client
  may use selected current/default ApplicantParty as initial applicant prefill/default.

Future ApplicantParty lifecycle slices
  own delete/archive/edit/version behavior.
```

## 6. Sources / Source Behavior Items

Read source mapping through:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Primary sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/domain/applicantparty-domain-model.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
```

Relevant behavior/UI items:

```text
SC-10-BI-005 — Account may have one current/default ApplicantParty template per applicant type.
SC-10-BI-007 — Additional same-type create does not change current/default implicitly.
SC-10-BI-008 — Explicit current/default selection is separate future behavior on same Applicant Parties page.
SC-10-BI-009 — Existing requests are not changed by ApplicantParty creation or default/current changes.
SC-10-UI-010 — Explicit make default/current action is same-page behavior.
```

## 7. Visual UI / Scenario Flow

Scenario Flow is user/system behavior from scenario sources. It must not include implementation details such as route constants, query keys, component props or repository methods.

```text
[Signed-in Client]
opens Applicant Parties page / section
        ↓
[Page]
shows current/default templates highlighted
and other saved ApplicantParties below
        ↓
[Client]
chooses “make current/default”
on one saved non-current/default ApplicantParty
        ↓
[Page]
shows pending action state for selected card/action
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ command accepted             │ command rejected             │
 ▼                              ▼
Selected ApplicantParty         Error feedback is visible
becomes current/default          Existing highlighted state remains unchanged
        ↓
Previous same-type
current/default card becomes
regular saved card
        ↓
Existing requests remain unchanged
```

In ordinary words:

The client is already on the Applicant Parties page / section. Current/default cards are highlighted at the top, and other saved ApplicantParties are shown below. The user explicitly clicks make current/default on a saved non-current/default card. If the command succeeds, the account ApplicantParties read state refreshes from server truth: the selected card becomes the highlighted current/default card for its applicant type, and the previous same-type current/default card becomes regular. If the command fails, the page shows feedback and keeps the visible current/default state unchanged.

## 8. Scenario Slice Flow

| Step | UI / scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Signed-in client | Opens Applicant Parties page / section. |
| S02 | Read area | Shows current/default highlighted cards and other saved cards. |
| S03 | Non-default card | Shows explicit make current/default action. |
| S04 | User action | Client chooses one saved ApplicantParty as current/default. |
| S05 | Pending state | Selected action/card shows command in progress. |
| S06 | Accepted outcome | Selected card becomes highlighted current/default after refresh. |
| S07 | Same-type previous default | Previous same-type default is no longer highlighted after refresh. |
| S08 | Rejected outcome | Error feedback is visible and visible default state remains unchanged. |
| S09 | Existing requests | Existing requests are not visually rewritten by this action. |

## 9. Visual Client Implementation Flow

Implementation Flow describes layers/files/responsibilities. It is not scenario flow and must not be used as behavior items.

```text
[Route / Page Layer]
pages/account/AccountPage.tsx
or dedicated Applicant Parties page / section
        ↓
[Entity Display UI Layer]
entities/applicant-party/ui/ApplicantPartiesList.tsx
entities/applicant-party/ui/ApplicantPartySummaryCard.tsx
        ↓
[Command Feature UI/Model Layer]
features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton.tsx
features/applicant-party/make-current-default/model/useMakeApplicantPartyCurrentDefaultMutation.ts
features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.ts
        ↓
[Shared API Layer]
shared/api/l1ApplicantPartyApi.ts
shared/api/l1ApiPaths.ts
        ↓
[Generated Contract Layer]
shared/api/generated/openapi-types.ts
```

## 10. Client Implementation Flow

### 10.1 Route / Page Layer

```text
Lives here:
  AccountPage or Applicant Parties page / section

Uses:
  useSession()
  useAccountApplicantPartiesQuery()
  useMakeApplicantPartyCurrentDefaultMutation() through feature action composition

Owns:
  session branch
  page read state
  composing ApplicantPartiesList
  deciding when to render make-current/default action
  passing feature action slot/render prop into entity display UI

Does not own:
  low-level HTTP
  endpoint path construction
  card display internals
  mutation implementation
```

### 10.2 Entity Display UI Layer

```text
Lives here:
  entities/applicant-party/ui/ApplicantPartiesList.tsx
  entities/applicant-party/ui/ApplicantPartySummaryCard.tsx

Possible extension props:
  renderApplicantPartyActions?(
    applicantParty: ApplicantPartySummary,
    options: { highlighted: boolean }
  ): ReactNode

or:
  action?: ReactNode on card

Owns:
  read/display layout
  current/default top area
  other saved area
  highlighted card styling
  empty read state
  placing optional action slot if provided

Does not own:
  make-current/default button logic
  command mutation
  pending/error command state
  backend command errors
```

### 10.3 Command Feature UI / Model Layer

```text
Lives here:
  features/applicant-party/make-current-default/ui/MakeCurrentDefaultButton.tsx
  features/applicant-party/make-current-default/model/useMakeApplicantPartyCurrentDefaultMutation.ts
  features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.ts

Uses:
  useMutation()
  shared API wrapper
  applicantParty query keys
  queryClient.invalidateQueries(...) or equivalent read-state refresh

Owns:
  visible command button
  click handler
  command pending state
  command error feedback
  command mutation
  account ApplicantParties query refresh after success

Does not own:
  ApplicantParty card layout
  ApplicantParty grouping
  page/session branch
```

### 10.4 Shared API Layer

```text
Lives here:
  shared/api/l1ApplicantPartyApi.ts
  shared/api/l1ApiPaths.ts

Owns:
  low-level HTTP call:
  POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default

Does not own:
  React Query mutation
  visible pending/error states
  card/list display
```

### 10.5 Generated Contract Layer

```text
Lives here:
  shared/api/generated/openapi-types.ts

Owns:
  generated structural API contract

Does not own:
  manual edits
  page/UI decisions
  handwritten endpoint shape
```

## 11. Client API / Server Contract

Endpoint:

```text
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
```

Route input:

```text
applicantPartyId: number
```

Request body:

```text
none
```

Success response:

```text
200 OK with no required body
```

Expected statuses:

```text
200 success
401 unauthenticated
422 invalid/missing/not-owned selected ApplicantParty according to current server behavior
500 unexpected error
```

Target shared API wrapper:

```ts
export const makeApplicantPartyCurrentDefault = (
  applicantPartyId: number,
): Promise<void> =>
  fetchJson<void>(
    l1ApiPaths.makeApplicantPartyCurrentDefault(applicantPartyId),
    { method: "POST" },
  );
```

Target path helper:

```ts
makeApplicantPartyCurrentDefault: (applicantPartyId: number | string) =>
  `/api/l1/applicant-parties/${encodeURIComponent(String(applicantPartyId))}/make-current-default`
```

Client rule:

```text
Client does not infer default switching locally as durable source of truth.

On success:
  refresh or update account ApplicantParties read state.

On error:
  keep visible read state unchanged;
  show feedback near action/card or page-level fallback.
```

## 12. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-SL-APPL-003-CLIENT-001` | resolved | Is backend endpoint available? | Yes, backend command endpoint is implemented. | Client can proceed after generated/client contract sync. |
| `Q-SL-APPL-003-CLIENT-002` | accepted | Is switching implicit on create? | No. This is explicit same-page user action. | Keeps create additive. |
| `Q-SL-APPL-003-CLIENT-003` | accepted | Where does visible action live? | Feature owns button/action; entity card/list receives optional action slot. | Preserves read vs command boundary. |
| `Q-SL-APPL-003-CLIENT-004` | accepted | Where does mutation live? | `features/applicant-party/make-current-default`. | Command architecture. |
| `Q-SL-APPL-003-CLIENT-005` | accepted | Does command update existing requests? | No. Existing requests remain unchanged. | Scope boundary and E2E non-goal. |
| `Q-SL-APPL-003-CLIENT-006` | accepted | What happens if selected already current/default? | UI hides/disables action; backend is safe/idempotent. | Avoids duplicate action. |
| `Q-SL-APPL-003-CLIENT-007` | implemented server direction | Response `200` or `204`? | Current backend declares `200 OK`; wrapper treats success as `void`. | API wrapper/tests. |
| `Q-SL-APPL-003-CLIENT-008` | implementation decision | Error placement? | Prefer feature-owned per-action error, with page fallback for unexpected errors. | UX/tests. |
| `Q-SL-APPL-003-CLIENT-009` | implementation check | Are generated artifacts already exposing endpoint? | Verify before client code. If missing, run generation workflow. | Prevents manual generated edits. |
| `Q-SL-APPL-003-CLIENT-010` | implementation check | Is target Applicant Parties page already replacing old AccountPage? | Verify current UI. The action may be wired into existing AccountPage or future Applicant Parties page. | Placement and route/page scope. |

## 13. Extension / Change Points

```text
- generated OpenAPI/types must expose backend command before typed wrapper is finalized;
- entity card/list action slot enables command actions without making entity own commands;
- optimistic update is optional future refinement; first implementation can invalidate/refetch;
- multi-type behavior is server-owned; client relies on server truth;
- request creation default prefill consumes refreshed current/default state through account ApplicantParties read;
- delete/archive/edit lifecycle remains future ApplicantParty lifecycle work;
- old current-individual compatibility endpoint can be removed only through a cleanup decision after target page is stable.
```

## 14. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Source behavior item | How client sidecar covers it | Status |
|---|---|---|
| `SC-10-BI-005` Account may have one current/default ApplicantParty template per applicant type. | UI lets user choose one saved card; server truth after refresh shows one highlighted card for that type. | covered by UI + backend contract |
| `SC-10-BI-007` Additional same-type create does not change current/default implicitly. | Client action is explicit and separate from create. | respected |
| `SC-10-BI-008` Explicit current/default selection is separate same-page behavior. | This sidecar owns same-page explicit action. | covered |
| `SC-10-BI-009` Existing requests are not changed by ApplicantParty creation/default changes. | Client does not update request UI/state as part of this action. | respected |
| `SC-10-UI-010` Explicit make default/current action is same-page behavior. | Feature button/action appears on same Applicant Parties page through entity card action slot. | covered |

Not behavior coverage:

```text
- React Query invalidation;
- generated route constant;
- endpoint path string;
- button component existence alone;
- mocks called.
```

## 15. Client / Component / E2E Verification Plan

Client tests assert visible user outcome and client contract use, not backend handler internals.

### 15.1 Component / UI tests

| Test / check | Verifies |
|---|---|
| Non-current/default ApplicantParty card receives make-current/default action slot | Explicit action is available where page/feature provides it |
| Current/default card does not render make-current/default action | Already-default card cannot be switched redundantly |
| MakeCurrentDefaultButton calls mutation with selected `applicantPartyId` | Correct command target |
| Pending state is visible for selected action/card | User gets feedback while command is running |
| Success refreshes account ApplicantParties read state | UI returns to server-truth read model |
| Error state is visible when command fails | Rejected command feedback |
| Existing requests UI is not touched by this action | Scope boundary |

### 15.2 Shared API / feature tests

| Test / check | Verifies |
|---|---|
| `makeApplicantPartyCurrentDefault(id)` posts to endpoint path | Correct endpoint usage |
| Wrapper sends no request body | Contract body is empty |
| Success `200` resolves as `void` | No response body required |
| Error/ProblemDetails is surfaced to mutation caller | UI can show feedback |
| Mutation invalidates/refetches account ApplicantParties query | Read state refresh convention |

### 15.3 E2E happy path

```text
setup/register/login client
        ↓
setup two saved same-type ApplicantParties:
  first current/default
  second non-current/default
        ↓
open Applicant Parties page / section
        ↓
assert first card is highlighted
and second card is regular
        ↓
click make current/default on second card
        ↓
assert second card becomes highlighted
        ↓
assert first card becomes regular
```

### 15.4 E2E error path

```text
setup/register/login client
        ↓
open Applicant Parties page / section
        ↓
trigger make current/default failure
        ↓
assert error feedback visible
        ↓
assert previous visible current/default state remains unchanged
```

### 15.5 Explicit non-goals for tests

```text
Do not test backend transaction internals in client tests.
Do not assert React Query cache internals in E2E.
Do not test create ApplicantParty behavior here.
Do not test request creation picker behavior here.
Do not test delete/archive/edit lifecycle here.
```

## 16. Suggested File Placement

```text
src/features/applicant-party/make-current-default/ui/
  MakeCurrentDefaultButton.tsx
  makeCurrentDefaultButtonConst.ts
  makeCurrentDefaultButton.css

src/features/applicant-party/make-current-default/model/
  useMakeApplicantPartyCurrentDefaultMutation.ts

src/features/applicant-party/make-current-default/api/
  makeApplicantPartyCurrentDefault.ts

src/entities/applicant-party/ui/
  ApplicantPartiesList.tsx
  ApplicantPartySummaryCard.tsx
  applicantPartiesListConst.ts
  applicantPartiesList.css

src/shared/api/
  l1ApplicantPartyApi.ts
  l1ApiPaths.ts

tests/e2e/applicant-parties/
  applicant-parties-make-current-default.spec.ts
```

## 17. Implementation Checklist

```text
[ ] Verify generated OpenAPI/types include make-current/default endpoint.
[ ] If missing, run OpenAPI/type generation workflow.
[ ] Add l1ApiPaths make-current/default path helper.
[ ] Add shared API wrapper.
[ ] Add command feature API/mutation/button.
[ ] Add optional action slot to ApplicantPartySummaryCard/List.
[ ] Wire feature action through AccountPage / Applicant Parties page.
[ ] Hide/disable action for already current/default cards.
[ ] Show pending state for selected action.
[ ] Show per-action or page fallback error.
[ ] Refresh account ApplicantParties read state after success.
[ ] Add component/UI tests.
[ ] Add shared API/feature tests.
[ ] Add E2E happy path.
[ ] Add E2E error path if test setup can safely force failure.
```

## 18. Follow-up / Handoff

Recommended implementation order:

```text
1. Confirm generated contracts expose backend endpoint.
2. Add shared API path/wrapper.
3. Add feature mutation/button.
4. Add entity card/list action slot.
5. Wire action into AccountPage / Applicant Parties page.
6. Add tests.
7. Run API/client checks.
```

Remaining surrounding L1 gaps:

```text
- target Applicant Parties page/section may still need replacement of old current-individual AccountPage flow;
- delete/archive/edit lifecycle is future;
- multi ApplicantParty type creation is future;
- old current-individual compatibility endpoint cleanup is future.
```
