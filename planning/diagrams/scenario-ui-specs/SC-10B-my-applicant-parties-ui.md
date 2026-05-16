# SC-10B — My Applicant Parties UI Spec

Status: future UI scenario spec  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md`  
Behavior item source: `planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md`  
Marker: `[UI-SCENARIO]`

## 1. Purpose

Describe future UI-visible behavior for saved ApplicantParty management.

## 2. UI Source Summary

```text
Client opens My Applicant Parties / Account Applicant Parties section
        ↓
UI shows current/default templates grouped by applicant type
        ↓
UI shows all saved ApplicantParties
        ↓
Client can add, view, edit, delete/archive/hide, and set current/default when policy supports it
```

## 3. UI Behavior Items

| ID | UI behavior | Status |
|---|---|---|
| `SC-10B-UI-001` | Client can see saved ApplicantParties. | future requirement |
| `SC-10B-UI-002` | Client can distinguish current/default templates from non-default profiles. | future requirement |
| `SC-10B-UI-003` | Client can add ApplicantParty of supported type. | future requirement |
| `SC-10B-UI-004` | Client can explicitly set current/default template for an applicant type. | future requirement |
| `SC-10B-UI-005` | Client can view ApplicantParty details inline or in future details view. | future review |
| `SC-10B-UI-006` | Delete/archive/hide action shows warning or is blocked when ApplicantParty is used by important requests. | future review |

## 4. Questions

```text
Q: Is dedicated page needed immediately, or can Account page handle inline cards?
Q: Which deletion/archive warnings are required?
Q: Can current/default be unset, or must a replacement be selected?
```
