# SC-10 — Applicant Data Behavior Items

Status: current scenario/UI behavior items / applicant-template-per-type model

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
| `SC-10-BI-008` | Explicit current/default selection is separate future behavior. | future |

## 2. UI Behavior Items

| ID | Behavior | Status |
|---|---|---|
| `SC-10-UI-001` | Signed-in client can open Account page Applicant Parties section. | current target |
| `SC-10-UI-002` | Account page shows current/default templates separately from all saved ApplicantParties. | target |
| `SC-10-UI-003` | Current/default template is visually distinguished. | target |
| `SC-10-UI-004` | Account page shows all saved ApplicantParties inline. | target |
| `SC-10-UI-005` | Client can add individual ApplicantParty. | current target |
| `SC-10-UI-006` | Created ApplicantParty appears in saved list. | current target |
| `SC-10-UI-007` | First of type can initialize default. | target |
| `SC-10-UI-008` | Additional same-type does not switch default implicitly. | target |
| `SC-10-UI-009` | Client sees validation/error feedback. | current target |

## 3. Not Behavior Items

```text
- ApplicantPartyId response;
- React Query invalidation/refetch;
- service extraction;
- SaveChanges boundary.
```
