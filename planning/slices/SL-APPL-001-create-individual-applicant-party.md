# SL-APPL-001 — Create Individual ApplicantParty

Status: implemented narrow endpoint / one Applicant Parties page target reconciliation draft  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Parties Page / Applicant Data`  
Slice type: backend/API + scenario/UI alignment slice  
Current implementation status: create endpoint exists, returns ApplicantPartyId, uses shared creation service without SaveChanges, and preserves existing ApplicantParties; default-template selection remains future work

## 1. Slice Overview

Target behavior:

```text
Signed-in client opens Applicant Parties page / section.

The page shows current/default templates and other saved ApplicantParties.

Client adds an individual ApplicantParty on this same page.

Accepted data creates a new saved ApplicantParty linked to the account.

New ApplicantParty starts Unverified / NotVerified.

Existing ApplicantParties remain visible and unchanged.

Existing requests remain unchanged.

If no current/default ApplicantParty exists for this applicant type,
the created ApplicantParty may initialize default/current for that type.

If current/default already exists for the type,
creating another ApplicantParty does not switch default implicitly.
```

Out of scope:

```text
- separate My Applicant Parties page as a current scenario;
- explicit default switch;
- read all ApplicantParties;
- request creation with New applicant;
- edit/delete/archive;
- entrepreneur/legal entity creation.
```

## 2. Sources / Source Behavior Items

Scenario/UI sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
```

Source behavior items used by this slice:

```text
SC-10-BI-001 — Client can add an ApplicantParty to the account.
SC-10-BI-002 — New ApplicantParty starts NotVerified / Unverified.
SC-10-BI-003 — Creating ApplicantParty does not replace, hide, deactivate or overwrite existing ApplicantParties.
SC-10-BI-006 — First ApplicantParty of a type may initialize current/default template for that type.
SC-10-BI-007 — Additional ApplicantParty of the same type does not change current/default implicitly.
SC-10-BI-009 — Existing requests are not changed by ApplicantParty creation or default/current changes.
SC-10-UI-001..011 — Applicant Parties page / section UI behavior.
```

The one-page-vs-two-page correction is a scenario direction, not a behavior item.

## 3. Visual Scenario Flow

```text
Signed-in Client opens Applicant Parties page / section
        ↓
Top area shows current/default templates by applicant type when available
        ↓
Other saved non-default ApplicantParties are shown below
        ↓
Client adds new individual ApplicantParty
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ accepted                     │ not accepted                 │
 ▼                              ▼
New ApplicantParty appears       User sees validation/error
in saved ApplicantParty UI       feedback and can correct data
        ↓
Existing ApplicantParties remain visible and unchanged
        ↓
Existing requests remain unchanged
        ↓
Default rule:
  no default for type -> new party may initialize default/current;
  default exists -> existing default/current remains selected
```

Scenario flow intentionally does not mention ApplicantPartyId, SaveChanges, service or repository.

## 4. Scenario Slice Flow

| Step | Actor/system | Behavior | Source/item | Status |
|---|---|---|---|---|
| F01 | Client | Opens Applicant Parties page / section. | SC-10-UI-001 | target |
| F02 | UI/System | Shows default/current templates and other saved parties. | SC-10-UI-002/004 | target |
| F03 | Client | Enters individual applicant data on same page. | SC-10-BI-001 / SC-10-UI-005 | covered |
| F04 | System | Creates saved ApplicantParty when accepted. | SC-10-BI-001 | covered/current narrow |
| F05 | System | Starts ApplicantParty as Unverified. | SC-10-BI-002 | covered |
| F06 | System | Leaves existing ApplicantParties unchanged. | SC-10-BI-003 | covered/current standalone create |
| F07 | System | Leaves existing requests unchanged. | SC-10-BI-009 | covered/current standalone create |
| F08 | System | Applies default initialization only when none exists for type. | SC-10-BI-006/007 | future default-template slice |

## 5. Visual Implementation Flow

```text
[API Controller]
POST /api/l1/applicant-parties/individual
        ↓
[FluentValidation]
validates request DTO shape and individual applicant input
        ↓
[Application Handler]
standalone create ApplicantParty use case
        ↓
[ApplicantPartyCreationService]
validate/create/add entity; no SaveChanges
        ↓
[Default rule future]
explicit per-type default-template persistence remains separate
        ↓
[Handler]
SaveChanges for standalone create
        ↓
[API Response]
ApplicantPartyId
```

## 6. API Contract

| Endpoint | Method | Request DTO | Response DTO / Body | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/applicant-parties/individual` | POST | fullName/email/phone | ApplicantPartyId response kept | 200, 401, 403, 422, 500 | current endpoint / target behavior adjusted | yes |

Contract note:

```text
ApplicantPartyId is useful for stable list identity, cache reconciliation,
future selection, edit/archive/delete/default-template actions and future request applicant selection.

It is not a user-visible scenario behavior item.
```

Validation boundary:

```text
FluentValidation owns DTO/input shape.
Application handler/service owns account existence and creation orchestration.
Domain remains final invariant guard.
```

## 7. Questions / Decisions

Open / future-review items first:

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `SL-APPL-Q-002` | future review | What happens to default/current on create? | First-of-type may initialize default/current; additional same-type create does not switch silently. Explicit same-page default/current persistence remains future. | API/domain/client behavior for default templates. | Mirrored in `planning/slices/slice-questions-register.md`. |
| `SL-APPL-Q-006` | accepted direction | Is ApplicantParty management split into Account page section and separate My Applicant Parties page? | No. Use one Applicant Parties page / section; SC-10B is same-page future management addendum. | Scenario/source/slice/client navigation. | Mirrored in `planning/slices/slice-questions-register.md`. |

Accepted / implemented decisions:

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `SL-APPL-Q-001` | accepted | Does create replace existing ApplicantParties? | No. Create is additive. | Current create behavior and tests. | Mirrored in `planning/slices/slice-questions-register.md`. |
| `SL-APPL-Q-003` | accepted | Should create return ApplicantPartyId? | Yes, as API/implementation support. | API identity/cache/future actions. | Mirrored in `planning/slices/slice-questions-register.md`. |
| `SL-APPL-Q-004` | accepted | Is a separate details page required? | No. Details can be inline/cards on Applicant Parties page. | Client UI scope. | Mirrored in `planning/slices/slice-questions-register.md`. |
| `SL-APPL-Q-005` | implemented | Should shared creation logic be extracted? | Yes. `ApplicantPartyCreationService` validates/creates/adds; it does not call SaveChanges; outer handler commits. | Application service boundary. | Mirrored in `planning/slices/slice-questions-register.md`. |

## 8. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Behavior item | How slice covers it | Status |
|---|---|---|
| `SC-10-BI-001` | create endpoint creates saved ApplicantParty. | covered/current narrow |
| `SC-10-BI-002` | new party starts Unverified. | covered/current |
| `SC-10-BI-003` | integration coverage verifies second create keeps first stored/current flag unchanged. | covered/current standalone create |
| `SC-10-BI-006` | default initialization rule. | future default-template slice |
| `SC-10-BI-007` | no implicit default switch. | future default-template slice |
| `SC-10-BI-009` | standalone create does not mutate existing requests. | covered by command boundary / future regression target |
| ApplicantPartyId response | API contract note only. | not behavior |

## 9. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| authenticated create succeeds | valid signed-in create. | API integration | keep/current |
| unauthenticated create fails | auth boundary. | API integration | keep/current |
| invalid data rejects and writes nothing | validation/no-write. | API/domain | implemented/current |
| response includes ApplicantPartyId | stable identity support. | API integration | keep/current |
| created party belongs to authenticated account | ownership from auth. | API integration | keep/current |
| created party starts Unverified | verification default. | domain/API | keep/current |
| creating another party does not replace first | additive model. | API/domain | implemented/current |
| standalone create does not mutate existing requests | ApplicantParty create is not request rewrite. | API/domain | future regression if request fixtures exist |
| first of type initializes default | default rule. | API/domain | future default-template slice |
| second same-type does not switch default | no implicit replacement. | API/domain | future default-template slice |
| standalone create commits only applicant | transaction boundary. | API integration | implemented via handler-owned SaveChanges |
| request-create with New applicant is atomic | applicant + request together. | API integration | future SL-REQ-001 |

Do not test exact service method/repository call/SaveChanges location here.

## 10. Dependent / Follow-up Slices

```text
SL-APPL-002 — Account Applicant Parties Read / Templates
SL-APPL-003 — Select Current/Default ApplicantParty Template
SL-APPL-004 — ApplicantParty Creation Application Service
SL-REQ-001 — Create Connection Request With Applicant Context
future ApplicantParty delete/archive lifecycle slice(s)
```

## 11. Implementation Checklist

```text
[x] endpoint exists
[x] response includes ApplicantPartyId
[x] server derives account from auth context
[x] applicant starts Unverified
[x] remove current-active/replacement wording from implementation docs for standalone create
[x] extract reusable creation service
[x] create is additive
[ ] first-of-type initializes default
[ ] second same-type does not switch default through explicit default-template model
[ ] one Applicant Parties page read model exists
[ ] old single-current/current-applicant client model reconciled
```
