# SC-10B — My Applicant Parties UI Spec

Status: future UI scenario spec draft  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md`  
Behavior item sources: `planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md`  
Marker: `[UI-SCENARIO]`

## 1. Purpose

Capture future UI-visible behavior for managing all saved ApplicantParty profiles.

This is not current implementation.

## 2. UI Source Summary

```text
Client opens My Applicant Parties
        ↓
UI shows saved ApplicantParty profiles
        ↓
Client can view details, add, edit, delete/archive and set current/default template per applicant type
```

## 3. UI Behavior Items

| ID | UI behavior | Status |
|---|---|---|
| `SC-10B-UI-001` | Client can open future My Applicant Parties management area. | future requirement |
| `SC-10B-UI-002` | UI shows all saved ApplicantParty profiles for the account. | future requirement |
| `SC-10B-UI-003` | UI shows applicant type and verification status. | future requirement |
| `SC-10B-UI-004` | UI shows current/default marker per applicant type. | future requirement |
| `SC-10B-UI-005` | Client can open ApplicantParty details. | future requirement |
| `SC-10B-UI-006` | Client can add ApplicantParty of supported type. | future requirement |
| `SC-10B-UI-007` | Client can set ApplicantParty as current/default template for its type. | future requirement |
| `SC-10B-UI-008` | Client can request edit when allowed by safety/history policy. | future review |
| `SC-10B-UI-009` | Client can request delete/archive/hide when allowed by safety/history policy. | future review |
| `SC-10B-UI-010` | UI shows warning/confirmation before dangerous delete/archive. | future requirement |

## 4. UI State / Feedback Matrix

| State | UI-visible behavior | Notes |
|---|---|---|
| No saved ApplicantParties | Empty state plus add action. | Future management page. |
| Saved profiles exist | List/grouping by type/status/default marker. | Exact layout future. |
| Set default pending | Action feedback is visible. | Affects future prefill. |
| Delete/archive requested | Warning is shown according to risk. | Hard delete vs archive future. |
| Profile used by requests | UI warns or blocks destructive action. | Must preserve history. |

## 5. Validation / Error Feedback

Use client feedback and error conventions for action errors.

```text
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
```

## 6. Action Availability

Delete/archive availability depends on future policy.

Set-current/default is available for saved ApplicantParty that can act as template for its applicant type.

## 7. Accessibility Notes

Warnings and destructive confirmations must be clear, focusable and accessible.

## 8. UI Questions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `SC-10B-UI-Q-001` | future review | Is My Applicant Parties separate page or Account subpage? | Future decision. | navigation |
| `SC-10B-UI-Q-002` | future review | Delete vs archive vs hide? | Prefer safe archive/hide when ApplicantParty is used by requests. | history/audit |
| `SC-10B-UI-Q-003` | future review | Can used ApplicantParty be edited in place? | Avoid breaking request history; decide with version/snapshot policy. | domain/API/UI |

## 9. Downstream Use

Use this UI spec for future applicant profile management slices and client sidecars.
