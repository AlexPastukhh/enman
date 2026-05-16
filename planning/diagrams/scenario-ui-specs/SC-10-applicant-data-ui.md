# SC-10 — Applicant Parties UI Spec

Status: current UI-spec source / one Applicant Parties page model  
Marker: `[UI-SCENARIO]`

## 1. UI Source Summary

```text
Signed-in client opens Applicant Parties page / section
        ↓
Top area shows current/default templates by applicant type
        ↓
Current/default templates are outlined/highlighted
        ↓
Below, the page shows other saved ApplicantParties that are not selected as current/default
        ↓
The same page exposes add ApplicantParty action/form
        ↓
Client can add individual ApplicantParty
        ↓
Accepted create shows new ApplicantParty in saved list
        ↓
Existing ApplicantParties remain visible and unchanged
        ↓
Existing requests remain unchanged
```

## 2. Desired Visual Page Model

```text
Applicant Parties Page
│
├─ Default/current templates
│   └─ outlined/highlighted cards
│
├─ Other saved ApplicantParties
│   └─ regular cards
│
└─ Actions
    ├─ Add ApplicantParty
    └─ Make default/current (future explicit action)
```

Delete/archive lifecycle is future extension behavior and should not be rendered as current L1 UI unless a dedicated lifecycle slice adds it.

## 3. UI Behavior Items

| ID | Behavior | Status |
|---|---|---|
| `SC-10-UI-001` | Signed-in client can reach Applicant Parties page / section. | current target |
| `SC-10-UI-002` | Applicant Parties page shows current/default templates in a top area. | target |
| `SC-10-UI-003` | Current/default templates are visually highlighted, for example with border/selected marker. | target |
| `SC-10-UI-004` | Applicant Parties page shows other saved non-default ApplicantParties below the default/current area. | target |
| `SC-10-UI-005` | Client can add individual ApplicantParty on the same page. | current target |
| `SC-10-UI-006` | Created ApplicantParty appears in saved ApplicantParty UI without hiding/replacing existing cards. | current target |
| `SC-10-UI-007` | First ApplicantParty of a type can appear as initial default/current template. | target |
| `SC-10-UI-008` | Additional same-type ApplicantParty does not switch existing default/current highlight implicitly. | target |
| `SC-10-UI-009` | Client sees validation/error feedback for rejected applicant data. | current target |
| `SC-10-UI-010` | Explicit make default/current action belongs to future same-page behavior. | future |
| `SC-10-UI-011` | Separate ApplicantParty details page is not required by current direction. | accepted direction |
| `SC-10-UI-012` | Existing requests are not visually changed by ApplicantParty create/default actions. | current target |

## 4. Testing Note

E2E should assert visible Applicant Parties page state, not implementation mechanics such as refetch/invalidation.

Deletion/archive lifecycle should not be added to current L1 tests until a dedicated future lifecycle slice exists.
