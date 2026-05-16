# SC-10 — Applicant Data UI Spec

Status: current UI-spec source / applicant-template-per-type model  
Marker: `[UI-SCENARIO]`

## 1. UI Source Summary

```text
Signed-in client opens Account page
        ↓
Account page shows Applicant Parties section
        ↓
Top area shows current/default templates by type
        ↓
Saved list area shows all saved ApplicantParties
        ↓
Client can add individual ApplicantParty
        ↓
Accepted create shows new ApplicantParty in saved list
        ↓
Existing ApplicantParties remain visible and unchanged
```

## 2. UI Behavior Items

| ID | Behavior | Status |
|---|---|---|
| `SC-10-UI-001` | Signed-in client can reach Account page Applicant Parties section. | current target |
| `SC-10-UI-002` | Account page shows current/default templates separately from all saved ApplicantParties. | target |
| `SC-10-UI-003` | Current/default templates are visually highlighted, for example with border/selected marker. | target |
| `SC-10-UI-004` | Account page shows all saved ApplicantParties inline. | target |
| `SC-10-UI-005` | Client can add individual ApplicantParty. | current target |
| `SC-10-UI-006` | Created ApplicantParty appears in saved list. | current target |
| `SC-10-UI-007` | First ApplicantParty of a type can appear as initial default/current template. | target |
| `SC-10-UI-008` | Additional same-type ApplicantParty does not switch existing default implicitly. | target |
| `SC-10-UI-009` | Client sees validation/error feedback for rejected applicant data. | current target |
| `SC-10-UI-010` | Separate ApplicantParty details page is not required by current direction. | accepted direction |

## 3. Testing Note

E2E should assert visible Account page state, not implementation mechanics such as refetch/invalidation.
