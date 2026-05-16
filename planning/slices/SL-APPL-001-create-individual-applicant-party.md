# SL-APPL-001 — Create Individual ApplicantParty

Status: implemented narrow backend/API slice / target behavior reconciliation draft  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`  
Slice type: backend / API / persistence slice with dependent Account page and request-creation slices  
Current implementation status: narrow individual applicant creation endpoint exists and returns ApplicantPartyId; target multi-profile/default-template semantics are not fully implemented yet

## 1. Slice Overview

Observable target behavior:

```text
Signed-in client adds individual ApplicantParty data.
Accepted data creates a new saved ApplicantParty linked to the account.
New ApplicantParty starts NotVerified.
Existing ApplicantParties remain stored and unchanged.
If no current/default ApplicantParty exists for this applicant type, the new ApplicantParty becomes initial current/default template for that type.
If current/default already exists for this type, creating another ApplicantParty does not silently change the default.
```

Current implementation evidence checked for this reconciliation:

```text
EnergyManagement.Server/L1/Controllers/L1Controller.cs
EnergyManagement.Server/L1/Api/L1Dtos.cs
EnergyManagement.Server/L1/Application/Commands/L1CreateIndividualApplicantPartyHandler.cs
Domain.EnergyManagement/L1/Applicants/ApplicantParty.cs
Domain.EnergyManagement/L1/Applicants/IndividualApplicantParty.cs
Tests.EnergyManagement/Integration/L1/L1SliceIntegrationTests.cs
Tests.EnergyManagement/Domain/Applicants/IndividualApplicantPartyTests.cs
```

Current implementation / target distinction:

```text
Current implementation still contains older current-active-version concepts.
Target scenario direction is many saved ApplicantParties with one current/default template per applicant type.
Do not claim default-template behavior is fully implemented unless repo evidence later confirms it.
```

Out of this slice:

```text
- full Account page Applicant Parties read/list UI;
- explicit set current/default template action;
- entrepreneur/legal-entity applicant types;
- request creation with applicantContextType Existing/New;
- request/applicant atomic creation redesign;
- ApplicantParty delete/archive/hide;
- external verification provider integration.
```

## 2. Sources / Source Behavior Items

Scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
```

Relevant behavior items:

```text
SC-10-BI-001 — Client can add applicant data and create a saved ApplicantParty linked to the account.
SC-10-BI-002 — A client account may store many ApplicantParties over time.
SC-10-BI-003 — Adding a new ApplicantParty does not delete, overwrite, deactivate or replace existing ApplicantParties.
SC-10-BI-004 — New ApplicantParty starts as NotVerified.
SC-10-BI-005 — One current/default ApplicantParty template may exist per applicant type.
SC-10-BI-006 — First ApplicantParty of a type may initialize current/default template for that type.
SC-10-BI-007 — Additional ApplicantParty of the same type does not silently change existing current/default template.
```

## 3. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Signed-in Client                             │
│ opens Account page                           │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Account page shows Applicant Parties section │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Top area shows current/default templates     │
│ grouped by applicant type when available     │
│ - physical person                            │
│ - individual entrepreneur                    │
│ - legal entity                               │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Below, user sees saved ApplicantParties       │
│ including non-default profiles               │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ User adds a new individual ApplicantParty     │
│ by entering applicant data                    │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
 accepted           not accepted
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ New ApplicantParty    │   │ User sees validation/error    │
│ appears in the        │   │ feedback and can correct data │
│ Applicant Parties     │   └──────────────────────────────┘
│ section               │
└──────────┬───────────┘
           ▼
┌──────────────────────────────────────────────┐
│ Existing ApplicantParties remain visible      │
│ and unchanged                                 │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Default rule by type                         │
│ no default -> new item becomes initial        │
│ default exists -> default remains unchanged   │
└──────────────────────────────────────────────┘
```

Scenario notes:

```text
- Scenario flow stays user-visible and does not mention controller/handler/repository mechanics.
- Adding ApplicantParty is visible as “new applicant appears in Account page Applicant Parties section”.
- Existing ApplicantParties are not replaced, hidden or deactivated by adding a new one.
- Current/default templates are a user-visible prefill/default concept for future request creation.
- ApplicantParty details are small enough to show inline; a separate details page is not required now.
```

## 4. Scenario Slice Flow

| Step | Actor / system | Behavior | Source / item | Scope status |
|---|---|---|---|---|
| F01 | Client | Opens Account page Applicant Parties section. | SC-10-UI-001 | target UI/read dependency |
| F02 | System/UI | Shows current/default templates by applicant type when available. | SC-10-UI-002 | target UI/read dependency |
| F03 | System/UI | Shows saved ApplicantParties including non-default profiles. | SC-10-UI-004 | target UI/read dependency |
| F04 | Client | Adds new individual ApplicantParty data. | SC-10-BI-001 | backend/API slice |
| F05 | System | Rejects invalid data without creating ApplicantParty. | SC-10-BI-010 | backend/API slice |
| F06 | System | Creates new saved ApplicantParty for accepted data. | SC-10-BI-001 | backend/API slice |
| F07 | System | New ApplicantParty starts NotVerified. | SC-10-BI-004 | backend/domain slice |
| F08 | System | Existing ApplicantParties remain unchanged. | SC-10-BI-003 | target/domain persistence behavior |
| F09 | System | If no default exists for type, new ApplicantParty initializes default. | SC-10-BI-006 | target/future default behavior |
| F10 | System | If default exists for type, default remains unchanged. | SC-10-BI-007 | target/future default behavior |
| F11 | Client UI | Shows created ApplicantParty after update/refetch. | SC-10-UI-006 | dependent client/read sidecar |

## 5. Visual Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ Account page / Applicant Parties section     │
│ user submits new individual applicant form   │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Client API call                              │
│ POST /api/l1/applicant-parties/individual    │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Application handler                          │
│ current: validates and creates applicant     │
│ target: uses shared ApplicantParty creation  │
│ service with no SaveChanges inside service   │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Persistence                                  │
│ new ApplicantParty is saved                  │
│ existing ApplicantParties unchanged          │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ API response                                 │
│ returns ApplicantPartyId                     │
│ useful for stable identity/cache/refetch     │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Client updates/refetches Applicant Parties   │
│ Account page shows server-truth list/default │
└──────────────────────────────────────────────┘
```

## 6. Implementation Flow

| Step | Layer | Responsibility | Current / target note |
|---|---|---|---|
| I01 | API Controller | Expose protected create individual endpoint. | current implemented |
| I02 | API/Auth boundary | Derive account id from L1 auth claims. | current implemented; client does not submit account id |
| I03 | API DTO | Receive individual applicant fields. | current narrow DTO: fullName/email/phoneNumber |
| I04 | Application | Validate applicant data and account context. | current handler owns this; target service can share creation logic |
| I05 | Application service | Create ApplicantParty entity without committing transaction. | target extraction for reuse by request creation with new applicant data |
| I06 | Domain/Persistence | Add new ApplicantParty; existing ApplicantParties remain unchanged. | target behavior; current implementation must be checked/refactored if it deactivates old current |
| I07 | Default/template policy | Initialize default only if none exists for this type. | target/future; not overclaimed as current unless implemented |
| I08 | API response | Return ApplicantPartyId. | current aligned; keep response id |
| I09 | Client | Invalidate/refetch ApplicantParties. | future/sidecar; server remains source of truth |

## 7. API Contract

| Endpoint | Method | Request DTO | Response DTO / Body | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/applicant-parties/individual` | POST | `L1CreateIndividualApplicantPartyDto` with `fullName`, `email`, `phoneNumber` | `L1CreateIndividualApplicantPartyResponse` with `ApplicantPartyId`, `ClientAccountId` in current contract | 200, 401, 403, 422, 500 | current L1 / target keeps ApplicantPartyId | yes |

API decision:

```text
Standalone create ApplicantParty returns ApplicantPartyId.
```

Reason:

```text
A dedicated details page is not required now because ApplicantParty data can be displayed inline.
However, ApplicantPartyId is useful for stable list identity, cache/refetch, selection, future edit/archive/delete/default-template actions and request creation with existing applicant context.
```

## 8. Questions / Decisions

Open / future-review questions first:

| ID | Status | Question | Current direction / assumption | Impact |
|---|---|---|---|---|
| SL-APPL-Q-005 | future review | How does user explicitly mark ApplicantParty as current/default template when one already exists? | Future explicit slice/action. | default-template API/UI |
| SL-APPL-Q-008 | future review | Can used ApplicantParty be edited in place? | Prefer future policy; snapshot/versioning question remains open. | request history correctness |
| SL-APPL-Q-009 | future review | Does delete mean hard delete, archive, deactivate or hide? | Do not assume hard delete; use policy/warnings. | My Applicant Parties management |
| SL-APPL-Q-010 | future review | Should request store ApplicantParty reference, snapshot, or both? | Preserve submitted applicant context; exact model future. | request details/history |

Accepted decisions:

| ID | Status | Question | Decision / current direction | Impact |
|---|---|---|---|---|
| SL-APPL-Q-001 | accepted direction | Does creating ApplicantParty replace existing ones? | No. New ApplicantParty is added; existing ones remain unchanged. | scenario/domain behavior |
| SL-APPL-Q-002 | accepted direction | Does Account page show ApplicantParties inline? | Yes. Current direction: top current/default templates, below all saved ApplicantParties. | Account page UI/read model |
| SL-APPL-Q-003 | accepted direction | Is separate details page required? | No, not for current direction. Details can be inline. | avoids unnecessary details slice now |
| SL-APPL-Q-004 | accepted direction | Should create response return ApplicantPartyId? | Yes, for stable identity, cache/refetch, selection and future actions. | API contract |
| SL-APPL-Q-006 | accepted direction | How are current/default templates shown? | Highlight selected current/default templates, for example marker/border. | UI behavior |
| SL-APPL-Q-007 | accepted direction | Should shared creation logic be extracted? | Yes. Reuse from standalone create and request creation with new applicant data. | service extraction |
| SL-APPL-Q-011 | accepted direction | What if no current/default exists for this type? | First created ApplicantParty of the type becomes initial current/default. | default-template policy |
| SL-APPL-Q-012 | accepted direction | What if current/default already exists for this type? | Creating another ApplicantParty does not silently replace it. | default-template policy |

Decision note:

```text
Current/default template selection is not hidden replacement.
It is future prefill/default selection behavior.
```

## 9. Extension / Change Points

| ID | Type | Area | Current direction | Status |
|---|---|---|---|---|
| CP-APPL-CREATE-SERVICE-001 | application service | shared creation logic | Extract creation logic into service with no SaveChanges; outer handlers commit. | target |
| CP-APPL-DEFAULT-001 | default-template policy | first item of type | First ApplicantParty of a type initializes current/default. | target |
| CP-APPL-DEFAULT-002 | explicit action | change default | Changing default when one exists is separate slice/action. | future slice |
| CP-APPL-REQUEST-001 | request creation | new applicant + request | Request creation with new applicant data must be atomic in one server call. | future request redesign |
| CP-APPL-LIST-001 | Account page read | saved list + defaults | Account page needs read model with saved ApplicantParties and defaults per type. | future read slice |

## 10. Behavior Coverage

| Scenario behavior item | How slice covers it | Draft location | Status |
|---|---|---|---|
| SC-10-BI-001 — create saved ApplicantParty | Create endpoint accepts individual applicant data and persists ApplicantParty. | Scenario / Implementation Flow | current covered |
| SC-10-BI-002 — many ApplicantParties | Target behavior says account may store many ApplicantParties. | Overview / Flow / Decisions | target; current implementation to verify/refactor |
| SC-10-BI-003 — no replacement | Draft explicitly says existing ApplicantParties remain unchanged. | Visual Scenario Flow / Decisions | target; important test |
| SC-10-BI-004 — NotVerified | New ApplicantParty starts NotVerified. | Flow / Decisions / Test Plan | current/target |
| SC-10-BI-005 — default per type | Draft introduces current/default template per applicant type. | Flow / Decisions | target |
| SC-10-BI-006 — first item initializes default | Draft sets initial default when none exists. | Flow / Decisions | target |
| SC-10-BI-007 — no silent switch | Draft keeps existing default unchanged when creating another same-type party. | Flow / Decisions | target |
| SC-10-UI-006 — created item visible | Dependent client/read sidecar refetches and shows list. | UI notes / Test Plan | delegated |
| ApplicantPartyId returned | API contract keeps response id for identity/cache/future actions. | API Contract / Decisions | implementation/API note, not scenario behavior |

## 11. Test / Verification Plan

Tests should verify visible/API behavior and domain guarantees, not internal details such as which service class was called.

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Authenticated client can create individual ApplicantParty | Signed-in user submits valid individual applicant data and creation succeeds. | API integration | implemented / keep |
| Guest cannot create ApplicantParty | Protected endpoint rejects unauthenticated request. | API integration/auth | implemented / keep |
| API response returns ApplicantPartyId | Created applicant has stable identity for cache/refetch/selection/future actions. | API integration | implemented / keep |
| Created ApplicantParty belongs to authenticated account | Account ownership is derived from auth context, not request body. | API integration | implemented / keep |
| Created ApplicantParty starts NotVerified | New ApplicantParty default verification state is correct. | Domain/API integration | implemented or add if missing |
| Creating another ApplicantParty does not replace existing ones | Adding a second ApplicantParty leaves the first saved/visible/unchanged. | API/domain integration | new / important |
| First ApplicantParty of type becomes current/default | If no default exists for that type, created party initializes default template. | API/domain integration | target / future if not implemented |
| Second ApplicantParty of same type does not replace default | Existing current/default remains unchanged after adding another same-type party. | API/domain integration | target / future if not implemented |
| ApplicantParty of different type can initialize its own default | Default is per applicant type, not global per account. | API/domain integration | future when more types exist |
| Invalid full name/email/phone rejects creation | Invalid applicant data returns validation ProblemDetails and no ApplicantParty is created. | API integration/domain | implemented / keep |
| Applicant contact email can differ from account email | Applicant profile email is independent from auth/account email. | API/domain integration | planned / keep |
| Created ApplicantParty is visible in Account page applicant section | After create/refetch, user sees new ApplicantParty in saved list. | client/component | future client sidecar |
| Client refetches ApplicantParties after create | UI gets server-truth list/default/verification state. | client/component | future client sidecar |
| Optimistic default display is corrected by refetch | If UI guessed default state, refetch reconciles it. | client/component | future client sidecar |
| Shared creation logic supports request-flow atomicity | Request creation with new applicant data can reuse creation logic without separate commit. | application/integration | future request redesign |
| Request-create with new applicant commits applicant + request together | New applicant + request are created atomically. | API integration | future request slice |
| Request-create failure does not leave orphan applicant | If request creation fails, neither applicant nor request is committed. | API integration | future request slice |

Do not test here:

```text
- exact service class invocation;
- exact repository Add call;
- exact SaveChanges method placement;
- details page navigation;
- visual border/marker before client sidecar;
- request creation applicantContextType behavior except as future dependent tests.
```

## 12. Dependent / Follow-up Slices

```text
[PLANNED] SL-APPL-002 — Account Applicant Parties Read / Templates
[PLANNED] SL-APPL-003 — Select Current/Default ApplicantParty Template
[HELPER]  SL-APPL-004 — ApplicantParty Creation Application Service
[PLANNED] SL-REQ-004 — Create Request With Explicit Applicant Context
[EXTENSION] entrepreneur/legal-entity applicant type slices
[PLUGIN] applicant verification provider/review integration
```

## 13. Implementation Checklist

Current implemented baseline:

```text
[x] POST /api/l1/applicant-parties/individual exists
[x] endpoint is protected by authorization
[x] account id is derived from L1 auth context
[x] request DTO contains fullName + email + phoneNumber
[x] client does not submit clientAccountId
[x] response returns ApplicantPartyId + ClientAccountId
```

Target reconciliation / future work:

```text
[ ] verify/refactor create behavior so new ApplicantParty does not replace/deactivate existing ones
[ ] initialize default only when no default exists for that applicant type
[ ] leave existing default unchanged when creating another same-type ApplicantParty
[ ] extract shared ApplicantParty creation service without SaveChanges inside service
[ ] add Account Applicant Parties read model
[ ] update request creation target to Existing ApplicantParty or New ApplicantParty data
[ ] add client refetch/update behavior after create
```
