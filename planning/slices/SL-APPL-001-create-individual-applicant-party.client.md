# SL-APPL-001.client — Create Individual ApplicantParty

Status: first-stage implemented client feature flow / current applicant read integrated / tests added  
Parent slice: `planning/slices/SL-APPL-001-create-individual-applicant-party.md`  
Source scenario: `SC-10 Applicant Data`  
Slice type: client sidecar / Account page applicant command flow  
Current implementation status: implemented create flow; Account page now uses server-backed current applicant read state after refresh

## 1. Sidecar Overview

Implemented client behavior:

```text
Authenticated client opens /account
        ↓
Account page reads current session
        ↓
Client shows account email and applicant create form
        ↓
Client submits first/middle/last name, applicant email and phone
        ↓
Client maps form values to L1CreateIndividualApplicantPartyDto
        ↓
POST /api/l1/applicant-parties/individual
        ↓
 ┌──────────────────────────┬──────────────────────────┐
 │ API success              │ API/ProblemDetails error  │
 ▼                          ▼
Save submitted values       Show field/root errors
as local read-only state
        ↓
Show Edit action + self-dismissing success notification
```

Current implementation evidence:

```text
energymanagement.client/src/app/router/router.tsx
energymanagement.client/src/pages/account/AccountPage.tsx
energymanagement.client/src/entities/session/model/useSession.ts
energymanagement.client/src/features/applicant-party/create-individual/ui/CreateIndividualApplicantPartyForm.tsx
energymanagement.client/src/features/applicant-party/create-individual/ui/ApplicantPartyReadOnlyView.tsx
energymanagement.client/src/features/applicant-party/create-individual/model/useCreateIndividualApplicantPartyForm.ts
energymanagement.client/src/features/applicant-party/create-individual/model/createIndividualApplicantPartySchema.ts
energymanagement.client/src/features/applicant-party/create-individual/api/createIndividualApplicantParty.ts
energymanagement.client/src/shared/api/l1ApplicantPartyApi.ts
energymanagement.client/src/shared/api/applyApiErrorToForm.ts
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Out of scope / not implemented here:

```text
- persisted edit/replacement flow;
- create request entry point;
- request creation form;
- My Requests navigation/read model;
- browser E2E evidence for applicant create.
```

## 2. Sources / Source Behavior Items

Planning/source docs:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md, if applied
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md, if applied
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/api/client-server-contract-principles.md
```

Source behavior items used by this client sidecar:

```text
Source BI TBD — Authenticated client opens Account page.
Source BI TBD — Account page shows applicant form when no current applicant read state is available.
Source BI TBD — Client submits full name, applicant email and phone number.
Source BI TBD — Client shows field/root validation errors.
Source BI TBD — Successful create shows applicant data as read-only local state.
Source BI TBD — Successful create shows self-dismissing success notification.
Source BI TBD — Edit action appears after local saved state.
```

Note: when SC-10 UI behavior item IDs are applied, replace temporary source labels with the real IDs.

## 3. Visual UI / Scenario Flow

```text
┌──────────────────────────────┐
│ Authenticated Client         │
│ opens /account               │
└──────────────┬───────────────┘
               ▼
┌──────────────────────────────┐
│ Account Page                 │
│ session exists               │
│ account email visible        │
└──────────────┬───────────────┘
               ▼
┌──────────────────────────────┐
│ Applicant create form        │
│ first/middle/last/email/phone│
└──────────────┬───────────────┘
               │ submit
               ▼
┌──────────────────────────────┐
│ Client/API success?          │
└──────────────┬───────────────┘
               │
       ┌───────┴────────┐
       │                │
    success             error
       │                │
       ▼                ▼
┌──────────────────────┐   ┌─────────────────────────────┐
│ Local read-only      │   │ Field/root errors from       │
│ applicant state      │   │ client validation or API     │
└──────────┬───────────┘   └─────────────────────────────┘
           ▼
┌──────────────────────────────┐
│ Read-only summary            │
│ Edit action                  │
│ self-dismissing notification │
└──────────────────────────────┘

Out of scope in this sidecar:
- create request entry;
- request creation form;
- persisted edit/replacement.
```

## 4. UI Slice Flow

| Step | UI behavior | Implementation evidence | Status |
|---|---|---|---|
| F01 | `/account` route exists. | `app/router/router.tsx`, `shared/config/clientRoutes.ts` | implemented |
| F02 | Account page reads current session. | `pages/account/AccountPage.tsx`, `useSession` | implemented |
| F03 | No-session branch renders registration fallback. | `AccountPage.tsx` | implemented-current; future UX may change |
| F04 | Authenticated branch shows account heading/email. | `AccountPage.tsx` | implemented |
| F05 | Authenticated branch fetches current applicant state and renders create form only when missing. | `AccountPage.tsx`, `entities/applicant-party` | implemented |
| F06 | Form renders first/middle/last name, email, phone. | `CreateIndividualApplicantPartyForm.tsx`, const file | implemented |
| F07 | Client validates requiredness, length, email and phone shape. | `createIndividualApplicantPartySchema.ts` | implemented |
| F08 | Submit maps form values to DTO `fullName/email/phoneNumber`. | `createIndividualApplicantParty.ts` | implemented |
| F09 | API errors are mapped to form field/root errors. | `useCreateIndividualApplicantPartyForm.ts`, `applyApiErrorToForm.ts` | implemented |
| F10 | Success invalidates/refetches current applicant state. | `useCreateIndividualApplicantPartyForm.ts`, `applicantPartyQueryKeys.ts` | implemented |
| F11 | Success shows server-backed read-only summary. | `AccountPage.tsx`, `ApplicantPartyReadOnlyView.tsx` | implemented |
| F12 | Success notification appears and self-dismisses. | `useCreateIndividualApplicantPartyForm.ts`, `CreateIndividualApplicantPartyForm.tsx` | implemented |
| F13 | Stable existing applicant load after refresh. | `useCurrentIndividualApplicantPartyQuery.ts` | implemented |

## 5. Visual Client Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ Route                                        │
│ app/router/router.tsx                        │
│ path: clientRoutes.account -> /account       │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Page                                         │
│ pages/account/AccountPage.tsx                │
│ owns page composition and session branch     │
└──────────────────┬───────────────────────────┘
                   ▼
        ┌──────────┴───────────┐
        │                      │
 no session               session exists
        │                      │
        ▼                      ▼
┌──────────────────┐   ┌──────────────────────────────────────┐
│ Feature fallback │   │ Account section                       │
│ auth/register    │   │ account heading + session email       │
│ RegisterForm     │   └──────────────────┬───────────────────┘
└──────────────────┘                      ▼
                       ┌──────────────────────────────────────┐
                       │ Feature UI                           │
                       │ features/applicant-party/            │
                       │ create-individual/ui/                │
                       │ CreateIndividualApplicantPartyForm    │
                       │ ApplicantPartyReadOnlyView            │
                       └──────────────────┬───────────────────┘
                                          ▼
                       ┌──────────────────────────────────────┐
                       │ Feature Model                        │
                       │ features/applicant-party/            │
                       │ create-individual/model/             │
                       │ schema + RHF + mutation + local state│
                       └──────────────────┬───────────────────┘
                                          ▼
                       ┌──────────────────────────────────────┐
                       │ Feature API Mapper                   │
                       │ features/applicant-party/            │
                       │ create-individual/api/               │
                       │ form values -> L1 DTO                │
                       └──────────────────┬───────────────────┘
                                          ▼
                       ┌──────────────────────────────────────┐
                       │ Shared API                           │
                       │ shared/api/l1ApplicantPartyApi.ts    │
                       │ fetchJson + credentials + errors     │
                       └──────────────────┬───────────────────┘
                                          ▼
                       ┌──────────────────────────────────────┐
                       │ Generated Contracts                  │
                       │ shared/api/generated/openapi-types.ts│
                       │ L1CreateIndividualApplicantPartyDto  │
                       └──────────────────────────────────────┘
```

Success/local-state branch:

```text
┌──────────────────────────────────────────────┐
│ Mutation success                             │
│ useCreateIndividualApplicantPartyForm        │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Local saved applicant state                  │
│ savedApplicantParty = submitted values       │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Read-only UI                                 │
│ ApplicantPartyReadOnlyView                   │
│ values + Edit action                         │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Success notification                         │
│ visible, then dismissed after timeout        │
└──────────────────────────────────────────────┘
```

## 6. Client Implementation Flow

| Layer | Responsibility | Current implementation | Status |
|---|---|---|---|
| Route | Expose account route. | `/account` route in router. | implemented |
| Page | Compose account page based on session. | `AccountPage.tsx`. | implemented |
| Session entity | Provide current session/null. | `useSession`. | implemented support |
| Feature UI | Render applicant create form or read-only local summary. | `CreateIndividualApplicantPartyForm`, `ApplicantPartyReadOnlyView`. | implemented |
| Feature model | Validate, submit, map errors, store local success state and notification. | `useCreateIndividualApplicantPartyForm`. | implemented |
| Feature validation | Validate full name/email/phone. | `createIndividualApplicantPartySchema`. | implemented |
| Feature API mapper | Convert form values to generated request DTO. | `createIndividualApplicantParty.ts`. | implemented |
| Shared API | Post to L1 applicant endpoint. | `l1ApplicantPartyApi.createIndividualApplicantParty`. | implemented |
| Current applicant read | Load already saved applicant after refresh. | `entities/applicant-party`, `l1ApplicantPartyApi.getCurrentIndividualApplicantParty`. | implemented |

## 7. Client API / Generated Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Response used? | Error constants used | Status |
|---|---|---|---|---|---|
| `features/applicant-party/create-individual/api/createIndividualApplicantParty(values)` | `POST /api/l1/applicant-parties/individual` | `L1CreateIndividualApplicantPartyDto`, `L1CreateIndividualApplicantPartyResponse` | Feature ignores response body and invalidates current applicant read state. | generated applicant/email/phone constants where available; local messages for name length/requiredness | implemented |
| `shared/api/l1ApplicantPartyApi.getCurrentIndividualApplicantParty()` | `GET /api/l1/applicant-parties/current-individual` | `L1CurrentIndividualApplicantPartyResponse` | Account page branches on `exists`. | none for normal missing state | implemented |

Contract notes:

```text
- Client does not submit `clientAccountId`.
- Backend derives account from authenticated session.
- Feature does not store `applicantPartyId` for request creation.
- Server response body exists, but this first-stage UI does not require it to continue.
```

## 8. Questions / Decisions

### Q-APPL-CLIENT-001 — How does Account page load existing applicant after refresh?

Question status: resolved  
Question: What server endpoint/read model should Account page use to show current applicant after reload?  
Assumption / current direction: Account page uses `GET /api/l1/applicant-parties/current-individual`, applicant-party entity query state and `exists` branching. Create success invalidates/refetches that query.  
Impact: Affects Account page state, read-only display, verification status and E2E.  
Shared register: `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-Q-001`

### D-APPL-CLIENT-001 — Do not store applicantPartyId for request creation

Question status: accepted direction  
Question: Should applicant create client store returned `applicantPartyId` for request creation?  
Assumption / current direction: No. Request creation should use the server-selected current active applicant; this feature treats HTTP success as enough.  
Impact: Keeps request creation aligned with server-derived applicant context.  
Shared register: `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-D-001`

### D-APPL-CLIENT-002 — Create request entry is out of scope

Question status: accepted direction  
Question: Should this sidecar introduce a create request entry after applicant save?  
Assumption / current direction: No. Entry location belongs to future request creation client sidecar.  
Impact: Do not test absence as a behavior; keep this as scope boundary.  
Shared register: `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-D-002`

### Q-APPL-CLIENT-002 — What does the Edit action mean?

Question status: future review  
Question: Is Edit a local correction before stable read, or a persisted replacement/edit flow?  
Assumption / current direction: Current implementation returns to editable local form only. Persisted edit/replacement is a future slice.  
Impact: Affects replacement/versioning, API contract and UI tests.  
Shared register: `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-Q-002`

### Q-APPL-CLIENT-003 — Should applicant verification status be shown?

Question status: future review  
Question: Should Account page show Not verified / Under review / Verified / Rejected states?  
Assumption / current direction: Not part of current create command; belongs to future read/verification slice.  
Impact: Affects read model, UI states and behavior items.  
Shared register: `planning/slices/slice-questions-register.md / SL-APPL-CLIENT-Q-003`

## 9. Behavior Coverage

| Scenario behavior item | How sidecar covers it | Draft/file location | Status |
|---|---|---|---|
| Source BI TBD — Authenticated client opens Account page | `/account` route renders AccountPage and reads session. | UI Slice Flow | covered |
| Source BI TBD — Client sees applicant form | Authenticated account page renders create form. | UI Slice Flow / Feature UI | covered |
| Source BI TBD — Client submits full name/contact data | Form fields map to `fullName/email/phoneNumber`. | Client API / Implementation Flow | covered |
| Source BI TBD — Client sees validation errors | zod + ProblemDetails mapping apply field/root errors. | Implementation Flow | covered |
| Source BI TBD — Successful create shows applicant data read-only | Submitted values become local read-only state. | UI Flow / Success branch | covered |
| Source BI TBD — Success notification appears | Self-dismissing notification is rendered. | UI Flow / Feature model | covered |
| Source BI TBD — Edit action appears | Read-only view has edit action. | UI Flow | covered first-stage local edit |
| Stable existing applicant after refresh | Requires future read-current slice. | Questions / Follow-up | not covered |
| Create request entry | Explicitly out of scope. | Questions / Follow-up | not covered by design |

## 10. Client / Component / E2E Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| AccountPage renders applicant form for authenticated session | Form entry point works. | component | planned/gap |
| Applicant form renders accessible fields | Labels/inputs/errors. | component | planned/gap |
| Client validation displays required/shape errors | Field validation. | component/model | planned/gap |
| Submit maps DTO | Form values become generated DTO shape. | feature/API mapper | planned/gap |
| ProblemDetails maps to field/root errors | Server validation UI. | feature/shared | planned/gap |
| Success switches to read-only local state | Main success behavior. | component | planned/gap |
| Edit action returns to editable local form | First-stage local edit behavior. | component | planned/gap |
| Success notification appears and dismisses | Feedback behavior. | component/timer | planned/gap |
| Browser applicant create happy path | UI -> real API -> read-only success state. | E2E | planned after test infra/client flow decision |

Do not add a test whose only purpose is asserting that create-request entry is absent. That is a scope boundary, not behavior coverage.

## 11. Dependent / Follow-up Slices

```text
L1-APPLICANT-PARTY-READ-CURRENT
L1-APPLICANT-PARTY-EDIT-OR-REPLACE
L1-CONNECTION-REQUEST-CREATE.client
My Requests read/list/detail client slices
```

## 12. Implementation Checklist

```text
[x] /account route exists
[x] AccountPage uses session
[x] applicant form rendered for authenticated session
[x] form fields exist
[x] zod validation exists
[x] DTO maps to fullName/email/phoneNumber
[x] shared API function exists
[x] ProblemDetails form mapping used
[x] success invalidates/refetches current applicant state
[x] server-backed read-only state exists
[x] self-dismissing notification exists
[x] current applicant read after refresh
[ ] persisted edit/replacement
[x] component tests found/confirmed
[ ] E2E found/confirmed
```
