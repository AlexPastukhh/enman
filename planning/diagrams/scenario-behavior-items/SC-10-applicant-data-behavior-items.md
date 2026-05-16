# SC-10 — Applicant Parties Behavior Items

Status: current scenario/UI behavior items / one Applicant Parties page model

## 1. Scenario Behavior Items

| ID | Behavior | Status |
|---|---|---|
| `SC-10-BI-001` | Client can add an ApplicantParty to the account. | current target |
| `SC-10-BI-002` | New ApplicantParty starts NotVerified / Unverified. | current target |
| `SC-10-BI-003` | Creating ApplicantParty does not replace, hide, deactivate or overwrite existing ApplicantParties. | current target |
| `SC-10-BI-004` | Account may store multiple ApplicantParties over time. | current target |
| `SC-10-BI-005` | Account may have one current/default ApplicantParty template per applicant type. | current target |
| `SC-10-BI-006` | First ApplicantParty of a type may initialize current/default template for that type. | current target |
| `SC-10-BI-007` | Additional ApplicantParty of the same type does not change current/default implicitly. | current target |
| `SC-10-BI-008` | Explicit current/default selection is separate future behavior on the same Applicant Parties page. | future |
| `SC-10-BI-009` | Existing requests are not changed by ApplicantParty creation or default/current changes. | current target |

## 2. UI Behavior Items

| ID | Behavior | Status |
|---|---|---|
| `SC-10-UI-001` | Signed-in client can open Applicant Parties page / section. | current target |
| `SC-10-UI-002` | Applicant Parties page shows current/default templates in a top area. | target |
| `SC-10-UI-003` | Current/default template cards are visually distinguished. | target |
| `SC-10-UI-004` | Applicant Parties page shows other saved non-default ApplicantParties below the default/current area. | target |
| `SC-10-UI-005` | Client can add individual ApplicantParty on the same page. | current target |
| `SC-10-UI-006` | Created ApplicantParty appears in saved list without replacing existing cards. | current target |
| `SC-10-UI-007` | First of type can initialize default/current. | target |
| `SC-10-UI-008` | Additional same-type create does not switch default/current highlight implicitly. | target |
| `SC-10-UI-009` | Client sees validation/error feedback. | current target |
| `SC-10-UI-010` | Explicit make default/current action is future same-page behavior. | future |
| `SC-10-UI-011` | Separate ApplicantParty details page is not required by current direction. | accepted direction |

## 3. Scenario Direction, Not Behavior Items

The one-page-vs-two-page correction is a scenario/navigation direction, not a user-visible behavior item. Keep it in scenario rules and shared registers, not as `SC-10-BI-*`.

## 4. Future Extension, Not Current Behavior Items

Delete/archive/hide lifecycle remains future extension pressure only.

Do not add current L1 behavior items or tests for delete/archive until a dedicated lifecycle slice exists.

Use:

```text
planning/slices/slice-extension-points-register.md
```

## 5. Not Behavior Items

```text
- ApplicantPartyId response;
- React Query invalidation/refetch;
- service extraction;
- SaveChanges boundary;
- one-page-vs-two-page documentation correction;
- delete/archive lifecycle before a dedicated future slice exists.
```
