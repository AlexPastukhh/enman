# SL-APPL-001.client — Create Individual ApplicantParty

Status: target reconciliation draft / current client implementation is stale for target model  
Parent slice: `planning/slices/SL-APPL-001-create-individual-applicant-party.md`  
Source scenario: `SC-10 Applicant Data`  
Slice type: client sidecar / Account page Applicant Parties section  
Current implementation status: old narrow current-applicant flow exists; target client flow requires Applicant Parties section and supporting backend/read model updates

## 1. Sidecar Overview

Target client behavior:

```text
Signed-in client opens Account page
        ↓
Account page shows Applicant Parties section
        ↓
Client can see:
  - current/default ApplicantParty templates by applicant type
  - all saved ApplicantParties
  - action/form to add a new individual ApplicantParty
        ↓
Client adds new individual ApplicantParty
        ↓
Accepted data creates a new saved ApplicantParty
        ↓
Existing ApplicantParties remain visible and unchanged
        ↓
If no current/default exists for this applicant type:
  new ApplicantParty becomes initial current/default template

If current/default already exists for this applicant type:
  existing current/default remains selected
  new ApplicantParty is only added to saved list
```

Important target correction:

```text
Creating ApplicantParty is not replacement by default.

Creating a new ApplicantParty does not deactivate, overwrite, hide,
or replace existing ApplicantParties.

Changing current/default when one already exists is a future explicit action.
```

Current client gap:

```text
Current AccountPage still follows old model:
  current applicant exists -> show one read-only ApplicantParty
  current applicant missing -> show create form

Target AccountPage should instead show:
  ApplicantPartiesSection
    CurrentDefaultTemplates
    SavedApplicantPartiesList
    AddIndividualApplicantPartyForm
```

Out of scope for this client sidecar:

```text
- request creation flow;
- request applicant selection;
- explicit default/current selection action;
- edit/archive/delete ApplicantParty;
- separate ApplicantParty details page;
- entrepreneur/legal entity forms;
- external verification workflow.
```

## 2. Sources / Source Behavior Items

Planning/source docs:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/api/client-server-contract-principles.md
```

Temporary behavior items used by this client sidecar until source scenario IDs are updated:

```text
SL-APPL-CLIENT-BI-001 — Signed-in client can open Account page.
SL-APPL-CLIENT-BI-002 — Account page shows Applicant Parties section.
SL-APPL-CLIENT-BI-003 — Account page shows current/default templates separately.
SL-APPL-CLIENT-BI-004 — Account page shows all saved ApplicantParties.
SL-APPL-CLIENT-BI-005 — Client can add individual ApplicantParty.
SL-APPL-CLIENT-BI-006 — Created ApplicantParty appears in saved list.
SL-APPL-CLIENT-BI-007 — Existing ApplicantParties remain unchanged.
SL-APPL-CLIENT-BI-008 — First ApplicantParty of type initializes current/default.
SL-APPL-CLIENT-BI-009 — Additional ApplicantParty does not change existing current/default.
SL-APPL-CLIENT-BI-010 — Client sees validation/error feedback.
```

## 3. Visual UI / Scenario Flow

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
│ Below, user sees all saved ApplicantParties   │
│ including non-default ones                   │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ User adds a new individual ApplicantParty     │
│ by entering applicant data                    │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
   accepted          rejected
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ New ApplicantParty    │   │ User sees validation/error    │
│ appears in saved      │   │ feedback and can correct data │
│ ApplicantParties list │   └──────────────────────────────┘
└──────────┬───────────┘
           ▼
┌──────────────────────────────────────────────┐
│ Existing ApplicantParties remain visible      │
│ and unchanged                                 │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ Does current/default already exist for this   │
│ applicant type?                              │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
      yes               no
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Existing default      │   │ New ApplicantParty becomes    │
│ remains selected      │   │ initial current/default       │
│ New party is saved    │   │ template for this type        │
│ as non-default        │   │                              │
└──────────────────────┘   └──────────────────────────────┘
```

Scenario notes:

```text
- Adding ApplicantParty is visible as “new applicant appears in the Account page Applicant Parties section”.
- Existing ApplicantParties are not replaced, hidden or deactivated by adding a new one.
- Current/default templates are a UI/user concept for future prefill/default selection.
- Current/default ApplicantParties may be visually highlighted, for example with a border/selected marker.
- ApplicantParty details are small enough to show inline in Account page cards/list.
- A separate ApplicantParty details page is not required by the current scenario direction.
```

## 4. UI Slice Flow

| Step | UI behavior | Target implementation | Status |
|---|---|---|---|
| F01 | `/account` route exists. | Existing route remains. | implemented |
| F02 | Account page reads current session. | Existing session behavior remains. | implemented |
| F03 | Authenticated branch shows account heading/email. | Existing branch remains. | implemented |
| F04 | Account page loads Applicant Parties state. | `useApplicantPartiesQuery()` target read model. | target / backend dependency |
| F05 | Account page shows Applicant Parties section. | `ApplicantPartiesSection`. | target |
| F06 | Top area shows current/default templates by applicant type. | `CurrentApplicantTemplates`. | target |
| F07 | Saved list shows all ApplicantParties. | `SavedApplicantPartiesList`. | target |
| F08 | Empty state shows add form/action. | `AddIndividualApplicantPartyForm`. | target |
| F09 | Create form renders individual applicant fields. | Reuse/refactor existing `CreateIndividualApplicantPartyForm`. | existing form / needs integration |
| F10 | Client validation shows required/shape errors. | Existing zod/RHF can be reused. | implemented support |
| F11 | Submit maps values to DTO. | Existing mapper can be reused. | implemented support |
| F12 | API/ProblemDetails errors show field/root feedback. | Existing shared error mapping can be reused. | implemented support |
| F13 | Success updates visible Applicant Parties state. | Optimistic update allowed; mandatory query invalidation/refetch. | target |
| F14 | First ApplicantParty of type can be shown as initial default. | UX may optimistically display this when no default exists; refetch reconciles server truth. | target |
| F15 | Additional ApplicantParty does not change existing default. | UI keeps existing default selected after server state update. | target |
| F16 | Details remain inline. | No details route required. | accepted direction |

## 5. Visual Client Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ Route/Page                                   │
│ pages/account/AccountPage.tsx                │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Session branch                               │
│ signed-in client sees account content        │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Entity Query                                 │
│ entities/applicant-party                     │
│ useApplicantPartiesQuery()                   │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Account Feature UI                           │
│ features/applicant-party/account-section     │
│ ApplicantPartiesSection                      │
└──────────────────┬───────────────────────────┘
                   ▼
        ┌──────────┴──────────┐
        │                     │
        ▼                     ▼
┌──────────────────────┐  ┌──────────────────────────────┐
│ Current/default       │  │ Saved ApplicantParties list   │
│ templates by type     │  │ all saved ApplicantParties    │
└──────────────────────┘  └──────────────────────────────┘
        │                     │
        └──────────┬──────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Add individual ApplicantParty form/action    │
│ features/applicant-party/create-individual   │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Shared API                                   │
│ POST /api/l1/applicant-parties/individual    │
│ returns ApplicantPartyId                     │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ React Query                                  │
│ invalidate Applicant Parties query           │
│ render server-truth state                    │
└──────────────────────────────────────────────┘
```

## 6. Client Implementation Flow

| Layer | Responsibility | Target note |
|---|---|---|
| Route | Expose Account page. | Existing `/account` route remains. |
| Page | Compose account-level sections and session branch. | Account page owns page-level placement. |
| Applicant entity | Load saved ApplicantParties and current/default templates. | Needs backend/read model beyond current `current-individual` endpoint. |
| Account section feature | Render templates, saved list, empty state, add form/action. | New feature area. |
| Create individual feature | Render form, validate, submit, map errors. | Existing form logic can be reused/refactored. |
| Shared API | Post create command and return typed response. | Keep `ApplicantPartyId` response. |
| Cache/model | Invalidate/refetch Applicant Parties after create. | React Query convention. |
| Feedback | Show local success/error feedback according to `CL-FEEDBACK-001`. | Exact copy/surface stays feature-owned. |

## 7. Client API / Generated Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Response used? | Status |
|---|---|---|---|---|
| `createIndividualApplicantParty(values)` | `POST /api/l1/applicant-parties/individual` | `L1CreateIndividualApplicantPartyDto`, `L1CreateIndividualApplicantPartyResponse` | `ApplicantPartyId` may be used for stable identity/cache/future actions; UI still refetches server state. | keep/update |
| `listApplicantParties()` | target read endpoint TBD | target response with saved list + current/default per type | Account page uses this as server truth. | backend dependency |

Contract notes:

```text
- Client does not submit `clientAccountId`.
- Backend derives account from authenticated session.
- Standalone create ApplicantParty returns ApplicantPartyId.
- Returning ApplicantPartyId is an API/implementation contract note, not a scenario behavior item.
- Current target requires a list/read endpoint for saved ApplicantParties and current/default templates.
```

## 8. Questions / Decisions

### Q-APPL-CLIENT-001 — Does creating ApplicantParty replace existing ones?

Question status: resolved  
Decision: No. Creating ApplicantParty creates a new saved ApplicantParty. Existing ApplicantParties remain visible and unchanged.  
Impact: The old “single current applicant / replacement” UI model is stale.

### Q-APPL-CLIENT-002 — What happens to current/default template on create?

Question status: accepted direction  
Decision:

```text
If no current/default ApplicantParty exists for this applicant type:
  newly created ApplicantParty becomes initial current/default template.

If current/default already exists for this applicant type:
  newly created ApplicantParty is added to saved list;
  existing current/default remains unchanged.
```

Impact: Changing default when one already exists is a separate explicit future slice.

Follow-up:

```text
SL-APPL-00X — Select Current/Default ApplicantParty Template
```

### Q-APPL-CLIENT-003 — Should create response return ApplicantPartyId?

Question status: resolved  
Decision: Yes. Standalone create ApplicantParty keeps returning `ApplicantPartyId`.  
Reason: The id is useful for stable list identity, cache reconciliation, future selection, edit/archive/delete/default-template actions, and future request creation selection.  
Boundary: This is not a scenario behavior item.

### Q-APPL-CLIENT-004 — Is a separate details page required?

Question status: accepted direction  
Decision: No, not for current direction.  
Reason: ApplicantParty details are small enough to show inline in Account page list/cards.

### Q-APPL-CLIENT-005 — Should client refetch after create?

Question status: resolved  
Decision: Yes. After successful create, client invalidates/refetches Applicant Parties state.  
Reason: Server is authoritative for saved list, current/default template, verification status and future action flags.  
Testing note: Component/model tests may verify query invalidation. E2E should verify visible outcome.

### Q-APPL-CLIENT-006 — What is wrong with current implementation?

Question status: implementation gap  
Current implementation is centered on a single current individual applicant:

```text
current applicant exists -> read-only single view
current applicant missing -> create form
```

Target implementation should be:

```text
ApplicantPartiesSection
  CurrentDefaultTemplates
  SavedApplicantPartiesList
  AddIndividualApplicantPartyForm
```

### Q-APPL-CLIENT-007 — Should shared creation logic be extracted?

Question status: accepted direction / backend-supporting decision  
Decision: Yes, but this is primarily backend/application structure. ApplicantParty creation logic should move into a shared application service reused by:

```text
standalone create ApplicantParty handler
future create request handler with applicantContextType = New
```

Service should not own `SaveChanges`; outer use-case handler owns commit/transaction boundary.  
Client impact: Standalone client create still calls create ApplicantParty endpoint. Future request creation with new applicant data should be one request-creation call, not two client calls.

## 9. API / Implementation Contract Notes

These are not scenario behavior items.

Standalone create endpoint:

```text
POST /api/l1/applicant-parties/individual
```

Standalone create response should include:

```text
ApplicantPartyId
```

Why it stays:

```text
stable identity
cache reconciliation
future selection
future edit/archive/delete
future default-template action
future request applicant selection
```

Backend/application note:

```text
Extract shared ApplicantParty creation logic into an application service.

Use it from:
- standalone create ApplicantParty handler;
- future create request handler with applicantContextType = New.

The shared service should not call SaveChanges.
Outer command handler owns transaction/commit boundary.
```

This avoids a future two-client-call request flow where ApplicantParty creation succeeds but request creation fails.

## 10. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How sidecar covers it | Status |
|---|---|---|
| Signed-in client can open Account page | Account page owns Applicant Parties section | covered |
| Client sees Applicant Parties section | Draft adds current/default area + saved list area | target covered |
| Client can add individual ApplicantParty | Create individual form/action remains in Account page section | covered |
| Created ApplicantParty appears in saved list | New item appears after create and page state update | covered |
| Existing ApplicantParties remain unchanged | Draft forbids implicit replacement/deactivation | covered |
| First ApplicantParty of type can initialize default | New party becomes default only when no default exists for type | covered |
| Additional ApplicantParty of same type does not change default | Existing default remains selected | covered |
| Current/default templates are visually distinguished | Top area + marker/border/highlight | target covered |
| Non-default ApplicantParties remain visible | Saved list contains all ApplicantParties | covered |
| Separate details page is not required | ApplicantParty data is shown inline | covered |
| Explicit default selection | Separate future slice | deferred |

## 11. Client / Component / E2E Verification Plan

### Client/component tests

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Account page shows Applicant Parties section for signed-in user | Account-level section exists | page/component | planned |
| Empty Applicant Parties state renders add form/action | User can create first ApplicantParty | page/component | planned |
| Saved ApplicantParties list renders cards | Multiple saved parties can be displayed | component | planned |
| Current/default template area renders separately | Defaults are visually distinct from saved list | component | planned |
| First ApplicantParty create appears as saved and default in UI state | Initial default UX | component/integration | planned |
| Second ApplicantParty of same type appears in saved list without changing existing default | No implicit replacement | component/integration | planned |
| Create form renders accessible fields | Full name/email/phone labels and inputs | component | planned |
| Client validation shows field errors | Required/shape feedback visible | component/model | planned |
| ProblemDetails maps to field/root errors | Server validation feedback visible | feature/shared API | planned |
| Create success invalidates Applicant Parties query | React Query convention for server-truth state | feature/model | planned |

### E2E

E2E checks user-visible outcome, not refetch mechanics.

```text
register/login client
        ↓
open /account
        ↓
Applicant Parties section is visible
        ↓
create individual ApplicantParty
        ↓
wait for POST /api/l1/applicant-parties/individual success
        ↓
assert created ApplicantParty is visible in Account page section
        ↓
reload /account
        ↓
assert created ApplicantParty is still visible
```

Technical note:

```text
E2E may wait for a read API response as synchronization,
but the assertion must be on visible Account page state.
```

### Backend/API tests that client depends on

| Test / check | Verifies | Layer |
|---|---|---|
| Create returns ApplicantPartyId | Stable identity for client/cache/future actions | API integration |
| Created ApplicantParty starts Unverified | Default verification state | API/domain |
| First ApplicantParty of type becomes current/default | Initial default rule | API/domain |
| Second ApplicantParty of same type does not replace default | No implicit replacement | API/domain |
| Existing ApplicantParties remain unchanged after create | No overwrite/deactivate | API/domain |
| Created ApplicantParty belongs to authenticated account | Ownership from auth context | API/domain |
| Unauthenticated create is rejected | Protected endpoint | API/auth |
| Invalid applicant data is rejected | No invalid write | API/domain |

Do not test here:

```text
- details page navigation;
- create request entry;
- absence of create request entry;
- exact refetch request count in E2E;
- internal service/repository calls.
```

## 12. Dependent / Follow-up Slices

```text
[BACKEND/API] SL-APPL-READ-LIST — List saved ApplicantParties with current/default per type
[CLIENT] SL-APPL-ACCOUNT-SECTION — Account page Applicant Parties section
[CLIENT] SL-APPL-SELECT-DEFAULT — Select Current/Default ApplicantParty Template
[CLIENT/BACKEND] SL-APPL-EDIT — Edit saved ApplicantParty
[CLIENT/BACKEND] SL-APPL-ARCHIVE — Archive/delete saved ApplicantParty
[CLIENT/BACKEND] SL-REQ-001 — Request creation with existing/new applicant context
[EXTENSION] Entrepreneur applicant type
[EXTENSION] Legal entity applicant type
[PLUGIN] Applicant verification through review/provider
```

## 13. Implementation Checklist

```text
[x] /account route exists
[x] AccountPage uses session
[x] current create form exists
[x] form fields exist
[x] zod validation exists
[x] DTO maps to fullName/email/phoneNumber
[x] shared API function exists
[x] ProblemDetails form mapping used
[ ] Account page Applicant Parties section
[ ] saved ApplicantParties list read model
[ ] current/default templates per applicant type
[ ] first-of-type initial default behavior
[ ] additional same-type applicant does not change existing default
[ ] client invalidates/refetches Applicant Parties query after create
[ ] component tests for list/default/create behavior
[ ] E2E based on visible Account page state
[ ] explicit default selection slice
[ ] request creation with existing/new applicant context
```
