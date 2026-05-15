# SL-APPL-001.client — Create Individual ApplicantParty Client Sidecar

Status: implementation-ready client draft / not implemented  
Parent slice: `planning/slices/SL-APPL-001-create-individual-applicant-party.md`  
Slice type: client sidecar / client command slice  
Scope: Account page UI for creating and displaying current individual applicant party state  
Backend command contract: `POST /api/l1/applicant-parties/individual`  
Related future read slice: `L1-APPLICANT-PARTY-READ-CURRENT`  
Current implementation status: client/UI not implemented; backend command and generated OpenAPI types exist

## 1. Sidecar Overview

This sidecar defines the target client behavior for the first Account page applicant data flow.

The sidecar covers:

```text
authenticated client opens Account page
-> sees current applicant state when available
-> sees individual applicant form when current applicant is missing
-> submits fullName/email/phoneNumber
-> backend creates applicant party for authenticated account
-> UI keeps submitted applicant data visible as read-only
-> UI shows Edit action
-> UI shows self-dismissing success notification
```

This sidecar does **not** implement or decide:

```text
- create request entry location;
- create request form;
- My Requests navigation;
- applicant edit/replacement behavior;
- current applicant read endpoint implementation;
- applicant verification workflow;
- entrepreneur/legal-entity applicant forms;
- session/current-user contract changes.
```

Scope boundary:

```text
Applicant party data is now available for a future request creation flow.

The UI does not introduce a create request entry in this slice.
```

This scope boundary is not a standalone test requirement. Do not add a test whose only purpose is to assert that a create-request button/link does not exist, unless a future security/access-control or explicit negative UX requirement makes that absence observable and important.

## 2. Sources / Source Behavior Items

Current source docs:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-questions-register.md
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
planning/testing/testing-principles.md
planning/testing/e2e-testing-workflow.md
```

Scenario/UI behavior source status:

```text
Source behavior IDs are not fully attached in the current repo state.

Temporary Source BI TBD labels are used in this draft.
Before final client implementation handoff, replace them with real scenario/UI behavior item IDs
from the SC-10 UI spec / behavior items package when available.
```

Working behavior items used by this sidecar:

| Temporary item | Source direction | Status |
|---|---|---|
| Source BI TBD — Account page opens for authenticated client | SC-10 applicant data / Account page UI direction | temporary |
| Source BI TBD — Account page can show current applicant data as read-only | SC-10 saved applicant visible DATA / future read slice | temporary |
| Source BI TBD — Account page shows individual applicant form when current applicant is missing | SC-10 applicant data entry | temporary |
| Source BI TBD — Client submits full name, email and phone number | SC-10 narrow current L1 implementation DATA | temporary |
| Source BI TBD — Client sees validation or server errors | validation/error convention | temporary |
| Source BI TBD — Client sees saved applicant data after successful creation | SC-10 saved applicant visible DATA | temporary |
| Source BI TBD — Client sees self-dismissing success notification | current UI decision from draft review | temporary |
| Source BI TBD — Client sees Edit action after applicant data is saved | current UI decision; edit behavior future | temporary |

Backend command DTO shape:

```text
L1CreateIndividualApplicantPartyDto
  fullName:
    firstName
    middleName
    lastName
  email
  phoneNumber
```

Backend response shape:

```text
L1CreateIndividualApplicantPartyResponse
  applicantPartyId
  clientAccountId
```

Client rule:

```text
The current backend response contains applicantPartyId and clientAccountId,
but this sidecar does not use those values for request creation.

HTTP success is enough for this UI step.
```

## 3. Visual UI / Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Authenticated Client                         │
│ opens Account page                           │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Account page                                 │
│ loads current applicant party state          │
│ via future read-current slice                │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
 current exists     current missing
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Applicant data        │   │ Editable applicant form       │
│ shown read-only       │   │ first/middle/last/email/phone │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           │                              ▼
           │                   ┌──────────────────────────┐
           │                   │ Client submits applicant │
           │                   │ data                     │
           │                   └────────────┬─────────────┘
           │                                │
           │                                ▼
           │                   ┌──────────────────────────┐
           │                   │ Backend creates current  │
           │                   │ applicant party for auth │
           │                   │ account                  │
           │                   └────────────┬─────────────┘
           │                                │
           └────────────────────┬───────────┘
                                ▼
┌──────────────────────────────────────────────┐
│ Account page applicant section               │
│ shows applicant data as filled read-only     │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ UI shows Edit action                         │
│ actual edit/replacement behavior is future   │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ UI shows self-dismissing success notification│
└──────────────────────────────────────────────┘

Out of this sidecar:
- create request entry;
- create request form;
- My Requests navigation;
- applicant replacement/edit workflow;
- verification flow.
```

## 4. UI Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Authenticated client | Opens Account page. | Source BI TBD | current sidecar |
| F02 | Account page | Attempts to know whether current applicant party exists. | Source BI TBD / future read slice | split: current sidecar + future read-current |
| F03 | Account page | Shows read-only applicant data when current applicant exists. | SC-10 saved applicant visible DATA | future read-current / target UI |
| F04 | Account page | Shows editable applicant form when current applicant is missing. | Source BI TBD | current sidecar |
| F05 | Client | Enters first name, middle name, last name, email and phone number. | SC-10 / current narrow L1 DATA | current sidecar |
| F06 | Client UI | Validates obvious requiredness/shape for good UX. | client validation convention | current sidecar |
| F07 | Client | Submits applicant data. | Source BI TBD | current sidecar |
| F08 | Backend | Creates applicant party for authenticated account; server derives account id. | parent backend slice | existing backend |
| F09 | Client UI | Maps server ProblemDetails to field/root errors if command fails. | client error convention | current sidecar |
| F10 | Client UI | On success, keeps submitted values visible and switches fields to read-only. | draft decision | current sidecar |
| F11 | Client UI | Shows Edit action. | draft decision | current sidecar; edit behavior future |
| F12 | Client UI | Shows self-dismissing success notification. | draft decision | current sidecar |
| F13 | Future request creation client | Decides where create request entry belongs. | future slice | out of scope |

Initial implementation cut:

```text
The create command sidecar can start with the missing-applicant branch and switch to
local read-only state after successful submit.

Stable Account page state after refresh requires the future
L1-APPLICANT-PARTY-READ-CURRENT slice.
```

## 5. Visual Client Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ Page: pages/account                          │
│ AccountPage owns page composition            │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Entity: entities/applicant-party             │
│ future current applicant query/read model    │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
 current exists     current missing
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Feature UI           │   │ Feature UI                    │
│ ApplicantParty       │   │ CreateIndividualApplicant     │
│ ReadOnlyView         │   │ PartyForm                     │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           │                              ▼
           │                   ┌──────────────────────────┐
           │                   │ Feature Model             │
           │                   │ schema + form state       │
           │                   │ DTO mapping               │
           │                   └────────────┬─────────────┘
           │                                │
           │                                ▼
           │                   ┌──────────────────────────┐
           │                   │ Feature API               │
           │                   │ createIndividualApplicant │
           │                   │ Party(values)             │
           │                   └────────────┬─────────────┘
           │                                │
           │                                ▼
           │                   ┌──────────────────────────┐
           │                   │ Shared API                │
           │                   │ POST /api/l1/applicant-   │
           │                   │ parties/individual        │
           │                   └────────────┬─────────────┘
           │                                │
           └────────────────────┬───────────┘
                                ▼
┌──────────────────────────────────────────────┐
│ Feature UI                                   │
│ shows read-only data + Edit action           │
│ shows self-dismissing success notification   │
└──────────────────────────────────────────────┘
```

Suggested placement:

```text
energymanagement.client/src/pages/account/AccountPage.tsx

energymanagement.client/src/entities/applicant-party/
  api/getCurrentApplicantParty.ts                 [future read slice]
  model/applicantPartyTypes.ts
  model/useCurrentApplicantPartyQuery.ts          [future read slice]

energymanagement.client/src/features/applicant-party/create-individual/
  api/createIndividualApplicantParty.ts
  model/createIndividualApplicantPartySchema.ts
  model/useCreateIndividualApplicantPartyForm.ts
  ui/CreateIndividualApplicantPartyForm.tsx
  ui/ApplicantPartyReadOnlyView.tsx
  ui/createIndividualApplicantPartyConst.ts

energymanagement.client/src/shared/api/l1ApplicantPartyApi.ts
```

Important split:

```text
Create command client sidecar
        +
Future current applicant party read slice
```

The create command sidecar can switch to read-only state using submitted values after success.

Stable Account page state after refresh needs:

```text
L1-APPLICANT-PARTY-READ-CURRENT
```

## 6. Client Implementation Flow

| Step | Layer | Responsibility | Status / direction |
|---|---|---|---|
| I01 | Page | Add/extend Account page composition for applicant section. | target |
| I02 | Entity / read model | Represent current applicant party state. | future read slice; local success state can be used in first cut |
| I03 | Feature UI | Render create individual applicant form when current applicant is missing. | target |
| I04 | Feature Model | Own form values, validation schema and submit state. | target |
| I05 | Feature Model | Map form values to `L1CreateIndividualApplicantPartyDto`. | target |
| I06 | Shared API | Call `POST /api/l1/applicant-parties/individual`. | target |
| I07 | Shared API / Feature Model | Parse ProblemDetails into field/root errors. | target |
| I08 | Feature UI | On success, keep submitted values visible and switch to read-only state. | target |
| I09 | Feature UI | Show Edit action after saved/read-only state. | target; actual edit behavior future |
| I10 | Feature UI | Show self-dismissing success notification. | target |
| I11 | Feature/UI boundary | Do not store applicantPartyId for request creation. | accepted direction |
| I12 | Auth/session boundary | Do not refetch current-user/session after create. | accepted direction |
| I13 | Scope boundary | Do not introduce create request entry in this sidecar. | accepted direction; not a standalone test requirement |

## 7. Client API / Generated Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Error constants used | Status |
|---|---|---|---|---|
| `createIndividualApplicantParty(values)` | `POST /api/l1/applicant-parties/individual` | operation `L1CreateIndividualApplicantParty`; schema `L1CreateIndividualApplicantPartyDto`; response `L1CreateIndividualApplicantPartyResponse` | ProblemDetails / generated semantic constants where available | target |
| `getCurrentApplicantParty()` | target: `GET /api/l1/applicant-parties/current-individual` | none yet; future read contract | n/a | future read slice |

Backend command request:

```text
fullName.firstName
fullName.middleName
fullName.lastName
email
phoneNumber
```

Client must not submit:

```text
clientAccountId
applicantPartyId
```

Current backend response includes:

```text
applicantPartyId
clientAccountId
```

Client behavior for this sidecar:

```text
- Treat HTTP success as enough for the create UI outcome.
- Do not store applicantPartyId for request creation.
- Do not pass applicantPartyId to request creation.
- If a later read/details scenario needs applicantPartyId, get it from a read model, not from forcing command response usage into this flow.
```

ProblemDetails handling:

```text
- field-level validation errors should map near fields;
- non-field command errors should appear as root/form alert;
- server remains source of truth even when client validation exists.
```

CSRF note:

```text
This is an unsafe browser command and will consume the future/broader CC-CSRF-001
client/server support when concrete unsafe-request protection is in scope.

This sidecar does not implement CSRF by itself.
```

## 8. Questions / Decisions

### Q-APPL-CLIENT-001 — Where are source behavior IDs?

Question status: open

Question:
Where are the authoritative scenario/UI behavior item IDs for this client sidecar?

Assumption / current direction:
Draft uses temporary `Source BI TBD` labels until SC-10 UI behavior item IDs are attached.

Impact:
No implementation impact by itself. Before final handoff, replace temporary labels with real scenario/UI behavior IDs.

Shared register:
- none for now; documentation/source-ID cleanup only.

### Q-APPL-CLIENT-002 — Where does the form live?

Question status: accepted direction

Question:
Should the first individual applicant party form live on Account page or a separate route?

Assumption / current direction:
For now, the form lives on:

```text
pages/account/AccountPage.tsx
```

Account page is the current place where the client manages applicant party data.

Impact:
If a separate route is introduced later, only page composition should change. The feature remains reusable under:

```text
features/applicant-party/create-individual
entities/applicant-party
```

Shared register:
- local sidecar decision; no separate global row needed unless route strategy changes.

### Q-APPL-CLIENT-003 — What happens after successful applicant creation?

Question status: accepted direction

Question:
What exact UI outcome happens after applicant party is created?

Assumption / current direction:
After HTTP success:

```text
fields stay filled
        ↓
fields become read-only
        ↓
Edit action appears
        ↓
self-dismissing success notification is shown
```

The first implementation may switch to read-only state using submitted form values after successful HTTP response.

Impact:
When the current applicant read endpoint exists, Account page should use server-loaded applicant party state instead of only local post-submit state.

Shared register:
- local sidecar decision; read endpoint dependency is mirrored as `SL-APPL-CLIENT-Q-004`.

### Q-APPL-CLIENT-004 — How does Account page know whether applicant party exists?

Question status: open

Question:
How should Account page know whether current applicant data already exists?

Assumption / current direction:
Add a separate read slice:

```text
L1-APPLICANT-PARTY-READ-CURRENT
```

Target endpoint direction:

```text
GET /api/l1/applicant-parties/current-individual
```

Server derives current account from auth context and returns the current active individual applicant party.

Impact:
If the read endpoint shape changes, only `entities/applicant-party` loading should change. Create form, command DTO mapping and success handling stay the same.

Shared register:
- `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-Q-004`
- `planning/slices/slice-implementation-notes-register.md / NOTE-APPL-READ-001`

### Q-APPL-CLIENT-005 — How much validation should client do?

Question status: accepted direction

Question:
How much validation should the client duplicate?

Assumption / current direction:
Client validates obvious field shape and requiredness for good UX:

```text
required fields
email shape
phone shape if a clear/common rule exists
string emptiness
```

Server remains source of truth.

Impact:
If generated constants or backend validation rules change, client schema should be updated. The feature must still handle server-side validation errors when client validation passes.

Shared register:
- related client-wide note: `planning/slices/slice-implementation-notes-register.md / NOTE-CLIENT-VALID-001`

### Q-APPL-CLIENT-006 — Should applicant party creation refetch session?

Question status: accepted direction

Question:
After applicant party creation, should the client refetch current-user/session?

Assumption / current direction:
No session refetch is required.

Current-user/session is account authentication state. Applicant party state belongs to applicant party read model, not session.

Impact:
If current-user later includes applicant/applicant-status summary, this decision can change. For now, do not invalidate `sessionQueryKey` after applicant party creation.

Shared register:
- `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-Q-006`

### Q-APPL-CLIENT-007 — Should UI include verified/unverified status?

Question status: future review

Question:
Should Account page show applicant party verification status?

Assumption / current direction:
Future Account page should display applicant party verification status when the read model exposes it.

Possible future UI states:

```text
Not verified
Under review / pending verification
Verified
Rejected / requires update
```

Initial create UI shows saved applicant data as read-only. Verification display belongs to the current applicant read slice and verification flow, not to the first create command implementation.

Impact:
Affects `L1-APPLICANT-PARTY-READ-CURRENT`, verification flow and Account page read-only display.

Shared register:
- `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-Q-007`

### Q-APPL-CLIENT-008 — Should create request entry be visible before applicant party exists?

Question status: accepted direction

Question:
Should Account page show an entry/action to create a connection request before applicant party data exists?

Assumption / current direction:
No.

Before applicant party exists, Account page shows the applicant party form only.

This sidecar does not introduce create request entry.

The exact global entry point for request creation remains a future decision. It may later live in:

```text
header
account page
My Requests page
another navigation area
```

Impact:
A future `L1-CONNECTION-REQUEST-CREATE.client` sidecar should decide where the create request entry belongs.

Shared register:
- `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-Q-008`

## 9. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Scenario/UI behavior item | How sidecar covers it | Draft location | Status |
|---|---|---|---|
| Source BI TBD — Client opens Account page | Account page owns applicant section composition. | Visual UI Flow / Client Implementation Flow | covered |
| Source BI TBD — Account page can load/display current applicant state | Sidecar references future read-current slice; stable refresh requires that slice. | UI Slice Flow / Questions / Dependent Slices | partially covered; read slice required |
| Source BI TBD — Client sees applicant form when applicant is missing | Account page missing-state branch shows create form. | Visual UI Flow / UI Slice Flow | covered |
| Source BI TBD — Client submits full name | Form maps first/middle/last name into `fullName`. | Client Implementation Flow / API Contract | covered |
| Source BI TBD — Client submits contact data | Form submits email and phone number. | Client Implementation Flow / API Contract | covered |
| Source BI TBD — Applicant party is created for authenticated account | Client submits applicant data only; backend derives account from auth context. | Client API / Generated Contract | covered |
| Source BI TBD — Validation errors are visible | Client validation handles obvious cases; ProblemDetails maps to field/root errors. | Questions / Test Plan | covered |
| Source BI TBD — Client sees saved applicant data | Success handling keeps values visible and switches to read-only state. | UI Slice Flow / Questions / Test Plan | covered |
| Source BI TBD — Client sees Edit action | Edit action appears after saved/read-only state; actual edit behavior is future. | UI Slice Flow / Questions | partially covered; edit behavior future |
| Source BI TBD — Client sees success notification | Feature shows self-dismissing success notification after success. | UI Slice Flow / Test Plan | covered |

Scope note:

```text
Create request entry is intentionally out of this sidecar.
It is recorded as a decision/scope boundary, not as a standalone behavior item or standalone test.
```

## 10. Client / Component / E2E Verification Plan

### 10.1 Component/client tests

These should be the main tests for this sidecar.

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Account page shows applicant form when applicant state is missing | Missing applicant state leads to editable form. | Client/component | planned |
| Applicant form renders fields | User sees first name, middle name, last name, email and phone. | Client/component | planned |
| Required validation is visible | Empty required fields produce accessible errors. | Client/component | planned |
| Shape validation is visible | Invalid email/phone display visible errors where rules exist. | Client/component | planned |
| Submit maps values to DTO | Request body contains `fullName`, `email`, `phoneNumber`. | Client/component + API helper | planned |
| Server ProblemDetails maps to field errors | 422 field errors appear near fields. | Client/component + shared API | planned |
| Server ProblemDetails maps to root error | Non-field command error appears as root form alert. | Client/component | planned |
| Success switches to read-only state | Submitted values remain visible and fields become non-editable. | Client/component | planned |
| Edit action appears after success | Saved/read-only state shows Edit action. | Client/component | planned |
| Success notification appears and dismisses | Toast/notification appears after success and auto-dismisses. | Client/component | planned |

Do not add a standalone test whose only assertion is that create-request entry is absent.

Reason:

```text
"Do not introduce create request entry" is a scope boundary for this sidecar,
not a user-visible negative behavior that needs its own test.
```

### 10.2 Shared API / contract checks

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| `l1ApplicantPartyApi` posts to L1 endpoint | Uses `/api/l1/applicant-parties/individual`. | Shared API | planned |
| API helper uses generated request type | Client boundary uses generated OpenAPI type instead of handwritten DTO. | Shared API / TypeScript | planned |
| Feature does not require returned id | UI success path does not depend on `applicantPartyId`. | Client/component | planned |
| ProblemDetails parsing works | 422 responses map through shared error path. | Shared API / feature | planned |

### 10.3 E2E boundary

E2E should check cross-layer behavior after concrete UI exists.

E2E happy path:

```text
[Test setup]
register and login L1 client
        ↓
[Browser]
open Account page
        ↓
[Browser]
fill applicant party form
        ↓
[Browser]
submit form
        ↓
[HTTP]
real POST /api/l1/applicant-parties/individual succeeds
        ↓
[Assert UI]
values are visible as read-only
        ↓
[Assert UI]
success notification is visible
```

Do not use E2E for exhaustive field validation.

Do not require applicantPartyId from the response in E2E.

### 10.4 Future read slice tests

These belong to `L1-APPLICANT-PARTY-READ-CURRENT`, not to the first create command sidecar.

| Test / check | Verifies | Status |
|---|---|---|
| Account page fetches current applicant party | Existing applicant data loads on page open. | future read slice |
| Existing applicant data is read-only | Current applicant party is displayed, not recreated. | future read slice |
| Verification status is displayed | Account page shows verified/not verified state when read model exposes it. | future read slice |
| Missing applicant party shows create form | Empty/missing read result leads to create form. | future read slice |

## 11. Dependent / Follow-up Slices

```text
[READ][FOLLOW-UP] L1-APPLICANT-PARTY-READ-CURRENT
[CLIENT][FOLLOW-UP] L1-CONNECTION-REQUEST-CREATE.client
[EXTENSION][FOLLOW-UP] L1-APPLICANT-PARTY-EDIT-OR-REPLACE
[VERIFICATION][FOLLOW-UP] Applicant party verification/status flow
[SECURITY][CROSS-CUTTING] CC-CSRF-001 unsafe browser command protection
```

### L1-APPLICANT-PARTY-READ-CURRENT

Purpose:

```text
Account page fetches current active individual applicant party for authenticated account.
```

Target endpoint direction:

```text
GET /api/l1/applicant-parties/current-individual
```

Rule:

```text
Server derives current account from auth context.
Client does not request applicant party by accountId.
```

This read slice makes Account page stable after refresh.

### L1-CONNECTION-REQUEST-CREATE.client

This future sidecar decides:

```text
- where create request entry lives;
- when it is visible;
- how it handles missing current applicant party;
- success message/navigation;
- My Requests dependency.
```

This applicant create sidecar does not decide or implement that entry.

## 12. Implementation Checklist

```text
[ ] Confirm or attach authoritative SC-10 UI behavior item IDs.
[ ] Create/extend AccountPage applicant section.
[ ] Create applicant-party entity model types.
[ ] Create individual applicant form feature.
[ ] Use generated OpenAPI request type for `L1CreateIndividualApplicantPartyDto`.
[ ] Map form values to fullName/email/phoneNumber.
[ ] Do not submit clientAccountId.
[ ] Do not store applicantPartyId for request creation.
[ ] Add client validation for requiredness/obvious shape.
[ ] Map ProblemDetails field errors.
[ ] Map ProblemDetails root/global errors.
[ ] On success, keep values visible and switch to read-only state.
[ ] Show Edit action after success.
[ ] Show self-dismissing success notification.
[ ] Do not refetch current-user/session after applicant create.
[ ] Do not introduce create request entry.
[ ] Add component/client tests for positive sidecar behavior.
[ ] Add shared API helper tests/checks if the project has that test layer.
[ ] Add E2E happy path only after concrete UI/test setup exists.
[ ] Create/read-current slice before relying on stable Account page refresh state.
```
