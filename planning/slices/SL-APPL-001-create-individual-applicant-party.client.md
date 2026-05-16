# SL-APPL-001.client — Create Individual ApplicantParty

Status: target reconciliation draft / current implementation stale for one-page ApplicantParty target model  
Slice type: client sidecar / Applicant Parties page add action  
Parent slice: `planning/slices/SL-APPL-001-create-individual-applicant-party.md`

## 1. Sidecar Overview

Current stale model:

```text
exists=true  -> show one read-only ApplicantParty
exists=false -> show create form
```

Target model:

```text
Applicant Parties Page / Section
  DefaultCurrentTemplates
  OtherSavedApplicantPartiesList
  AddIndividualApplicantPartyForm
  FutureMakeDefaultCurrentAction
```

This sidecar owns the add ApplicantParty action/form on the same Applicant Parties page.

It does not create a separate "My Applicant Parties" page.

## 2. Visual UI / Scenario Flow

```text
[Signed-in Client]
opens Applicant Parties page / section
        ↓
[Top Area]
shows current/default templates by type
as outlined/highlighted cards
        ↓
[Other Saved Area]
shows saved ApplicantParties that are not current/default
as regular cards
        ↓
[Client]
adds new individual ApplicantParty
on the same page
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ accepted                     │ rejected                     │
 ▼                              ▼
new ApplicantParty appears      validation/error feedback visible
in saved ApplicantParty UI
        ↓
existing ApplicantParties remain visible and unchanged
        ↓
existing requests remain unchanged
        ↓
default rule:
  no default for type -> new party may become initial default;
  default exists -> existing default remains highlighted
```

## 3. Visual Client Implementation Flow

```text
[Applicant Parties Page / Section]
loads Applicant Parties state
        ↓
[Entity Query]
useApplicantPartiesQuery()
        ↓
[Page UI]
renders default/current templates + other saved list + add form/action
        ↓
[Feature API]
POST /api/l1/applicant-parties/individual
        ↓
[API Contract]
returns ApplicantPartyId
        ↓
[React Query]
invalidate Applicant Parties query
        ↓
[Applicant Parties Page / Section]
renders server-truth defaults, other saved parties and verification status
```

Important boundary:

```text
Refetch/invalidation is implementation convention.
E2E asserts visible state, not that refetch happened.
ApplicantPartyId is API/implementation support, not behavior coverage.
Existing requests are not changed by create/default actions.
```

## 4. Questions / Decisions

Open / future-review items first:

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `Q-APPL-CLIENT-006` | implementation gap | What is wrong with current implementation? | Single-current UI model must become one Applicant Parties page / section with default/current templates, other saved parties and add action. | Client implementation reconciliation. | Mirrored by `SL-APPL-Q-006` in `planning/slices/slice-questions-register.md`. |

Accepted / resolved decisions:

| ID | Status | Question | Assumption / current direction | Impact | Shared register / local-only reason |
|---|---|---|---|---|---|
| `Q-APPL-CLIENT-001` | resolved | Does create replace existing ones? | No. Create is additive. | Create UI and post-create visible state. | Mirrored by `SL-APPL-Q-001`. |
| `Q-APPL-CLIENT-002` | accepted | What happens to default/current? | First of type may initialize; second same-type does not switch silently; explicit same-page action remains future. | Default/current highlight and future action. | Mirrored by `SL-APPL-Q-002`. |
| `Q-APPL-CLIENT-003` | resolved | Return ApplicantPartyId? | Yes, for stable identity/cache/future actions. | API/client identity support, not behavior coverage. | Mirrored by `SL-APPL-Q-003`. |
| `Q-APPL-CLIENT-004` | accepted | Separate details page? | No, details inline/cards in current direction. | Client scope. | Mirrored by `SL-APPL-Q-004`. |
| `Q-APPL-CLIENT-005` | resolved | Refetch after create? | Yes as client convention; E2E still asserts visible state, not refetch mechanics. | Client implementation/testing boundary. | Local implementation note; no standalone shared question needed. |
| `Q-APPL-CLIENT-007` | accepted direction | Is SC-10B a separate page? | No. SC-10B is future same-page management behavior. | Scenario/source navigation and client page model. | Mirrored by `SL-APPL-Q-006`. |

## 5. Behavior Coverage

| Behavior item | How draft covers it | Status |
|---|---|---|
| `SC-10-UI-001` | Applicant Parties page / section. | target |
| `SC-10-UI-002` | default/current templates top area. | target |
| `SC-10-UI-003` | default/current templates highlighted. | target |
| `SC-10-UI-004` | other saved non-default ApplicantParties area. | target |
| `SC-10-UI-005` | add form/action on same page. | current/target |
| `SC-10-UI-006` | created appears in saved UI without replacing existing cards. | target |
| `SC-10-UI-007` | first of type default/current. | target |
| `SC-10-UI-008` | second same-type no silent switch. | target |
| `SC-10-UI-009` | validation/error feedback. | target |
| `SC-10-BI-003` | no replacement. | target |
| `SC-10-BI-009` | existing requests unchanged. | target |
| ApplicantPartyId returned | API note only. | not behavior |

## 6. Test / Verification Plan

Component/model tests:

```text
- Applicant Parties page / section is visible.
- Empty state renders add form/action.
- Default/current templates area renders separately.
- Default/current cards are visually highlighted/outlined.
- Other saved non-default ApplicantParties render as regular cards/list.
- First create appears as saved and default/current in UI state when no default existed.
- Second same-type create appears as other saved without changing current/default highlight.
- Existing cards remain visible after create.
- Create form has accessible fields.
- Client validation shows field errors.
- ProblemDetails maps to field/root errors.
- Create success invalidates Applicant Parties query.
```

E2E:

```text
register/login client
        ↓
open Applicant Parties page / section
        ↓
Applicant Parties page / section is visible
        ↓
create individual ApplicantParty
        ↓
wait for create POST success
        ↓
assert created ApplicantParty is visible
        ↓
reload page
        ↓
assert created ApplicantParty is still visible
```

E2E may wait for read API response as synchronization, but assertion must be visible UI state.

Do not add delete/archive E2E until a dedicated lifecycle slice exists.

## 7. Next Step

```text
Replace old single-current model with:
ApplicantPartiesPageOrSection
  DefaultCurrentTemplates
  OtherSavedApplicantPartiesList
  AddIndividualApplicantPartyForm
```

Backend/API dependency:

```text
SL-APPL-002 — Account Applicant Parties Read / Templates
```

Future same-page dependency:

```text
SL-APPL-003 — Select Current/Default ApplicantParty Template
```
