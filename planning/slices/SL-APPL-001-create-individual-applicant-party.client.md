# SL-APPL-001.client — Create Individual ApplicantParty

Status: target reconciliation draft / current implementation stale for target model  
Slice type: client sidecar / Account page Applicant Parties section  
Parent slice: `planning/slices/SL-APPL-001-create-individual-applicant-party.md`

## 1. Sidecar Overview

Current stale model:

```text
exists=true  -> show one read-only ApplicantParty
exists=false -> show create form
```

Target model:

```text
ApplicantPartiesSection
  CurrentDefaultTemplates
  SavedApplicantPartiesList
  AddIndividualApplicantPartyForm
```

## 2. Visual UI / Scenario Flow

```text
[Signed-in Client]
opens Account page
        ↓
[Account Page]
shows Applicant Parties section
        ↓
[Top Area]
shows current/default templates by type
        ↓
[Saved List Area]
shows all saved ApplicantParties
        ↓
[Client]
adds new individual ApplicantParty
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ accepted                     │ rejected                     │
 ▼                              ▼
new ApplicantParty appears      validation/error feedback visible
in saved list
        ↓
existing ApplicantParties remain unchanged
        ↓
default rule:
  no default for type -> new party becomes initial default;
  default exists -> existing default remains selected
```

## 3. Visual Client Implementation Flow

```text
[AccountPage]
loads Applicant Parties state
        ↓
[Entity Query]
useApplicantPartiesQuery()
        ↓
[Page UI]
renders current/default templates + saved list + add form/action
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
[Account Page]
renders server-truth saved list/defaults/verification status
```

Important boundary:

```text
Refetch/invalidation is implementation convention.
E2E asserts visible state, not that refetch happened.
ApplicantPartyId is API/implementation support, not behavior coverage.
```

## 4. Questions / Decisions

| ID | Status | Question | Current direction |
|---|---|---|---|
| `Q-APPL-CLIENT-001` | resolved | Does create replace existing ones? | No. |
| `Q-APPL-CLIENT-002` | accepted | What happens to default? | First of type initializes; second same-type does not switch. |
| `Q-APPL-CLIENT-003` | resolved | Return ApplicantPartyId? | Yes, for stable identity/cache/future actions. |
| `Q-APPL-CLIENT-004` | accepted | Separate details page? | No, details inline. |
| `Q-APPL-CLIENT-005` | resolved | Refetch after create? | Yes as client convention; not E2E behavior. |
| `Q-APPL-CLIENT-006` | gap | What is wrong with current implementation? | Single current model must become list/defaults section. |

## 5. Behavior Coverage

| Behavior item | How draft covers it | Status |
|---|---|---|
| `SC-10-UI-001` | Account page section. | target |
| `SC-10-UI-002` | default templates top area. | target |
| `SC-10-UI-004` | saved list area. | target |
| `SC-10-UI-005` | add form/action. | current/target |
| `SC-10-UI-006` | created appears in saved list. | target |
| `SC-10-UI-007` | first of type default. | target |
| `SC-10-UI-008` | second same-type no switch. | target |
| `SC-10-BI-003` | no replacement. | target |
| ApplicantPartyId returned | API note only. | not behavior |

## 6. Test / Verification Plan

Component/model tests:

```text
- Account page shows Applicant Parties section.
- Empty state renders add form/action.
- Saved ApplicantParties list renders multiple cards.
- Current/default template area renders separately.
- First create appears as saved and default in UI state.
- Second same-type create appears in saved list without changing default.
- Create form has accessible fields.
- Client validation shows field errors.
- ProblemDetails maps to field/root errors.
- Create success invalidates Applicant Parties query.
```

E2E:

```text
register/login client
        ↓
open /account
        ↓
Applicant Parties section is visible
        ↓
create individual ApplicantParty
        ↓
wait for create POST success
        ↓
assert created ApplicantParty is visible in Account page section
        ↓
reload /account
        ↓
assert created ApplicantParty is still visible
```

E2E may wait for read API response as synchronization, but assertion must be visible UI state.

## 7. Next Step

```text
Replace old AccountPage single-current model with:
ApplicantPartiesSection
  CurrentDefaultTemplates
  SavedApplicantPartiesList
  AddIndividualApplicantPartyForm
```

Backend/API dependency:

```text
SL-APPL-002 — Account Applicant Parties Read / Templates
```
