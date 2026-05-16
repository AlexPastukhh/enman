# SC-10 — Applicant Data UI Spec

Status: target UI scenario spec / synchronized with ApplicantParty template-per-type model  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10-applicant-data.md`  
Behavior item sources: `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md`  
Marker: `[UI-SCENARIO]`

## 1. Purpose

This UI spec captures UI-visible behavior for Account page Applicant Parties.

It is intentionally not a React component plan.

It records how saved ApplicantParties, current/default templates and new applicant creation should be visible to the user.

## 2. UI Source Summary

Target UI direction:

```text
Signed-in Client opens Account page
        ↓
Account page shows Applicant Parties section
        ↓
Top area shows current/default templates grouped by applicant type when available
        ↓
Below, user sees saved ApplicantParties, including non-default profiles
        ↓
User can add a new ApplicantParty
        ↓
New ApplicantParty appears in saved list
        ↓
If no default exists for its type, it becomes initial current/default
If a default exists, existing default remains selected until explicit future action changes it
```

## 3. UI Behavior Items

| ID | UI behavior | Status |
|---|---|---|
| `SC-10-UI-001` | Authenticated client can reach Account page Applicant Parties section. | requirement |
| `SC-10-UI-002` | Account page can show current/default ApplicantParty templates grouped by applicant type. | target direction |
| `SC-10-UI-003` | Current/default templates are visually distinguishable, for example by selected marker/border. | target direction |
| `SC-10-UI-004` | Account page can show all saved ApplicantParties, including non-default ones. | target direction |
| `SC-10-UI-005` | Client can add a new individual ApplicantParty by entering applicant data. | current/narrow implementation alignment |
| `SC-10-UI-006` | After successful create, the new ApplicantParty appears in the Account page Applicant Parties section after update/refetch. | target direction |
| `SC-10-UI-007` | If no current/default exists for that applicant type, the new ApplicantParty can be shown as initial current/default. | target direction |
| `SC-10-UI-008` | If a current/default already exists for that type, creating another ApplicantParty does not silently switch the UI default. | accepted direction |
| `SC-10-UI-009` | Client should refetch ApplicantParties after create to get server-truth saved list/default/verification state. | client convention direction |
| `SC-10-UI-010` | A separate ApplicantParty details page is not required in current direction; details can be inline in list/cards. | accepted direction |
| `SC-10-UI-011` | Future UI may offer explicit action to set an ApplicantParty as current/default for its type. | future slice |
| `SC-10-UI-012` | Future UI may provide My Applicant Parties management page for list/details/edit/delete/archive/default selection. | future scenario |

## 4. UI State / Feedback Matrix

| UI state | Visible outcome | User action | Notes |
|---|---|---|---|
| Loading Applicant Parties | Applicant section loading/pending state. | Wait / retry if failed. | Requires read/list endpoint/client query. |
| No ApplicantParty for a type | Empty/default missing state for that type. | Add ApplicantParty for that type. | First created item may initialize default. |
| Current/default exists for a type | Current/default template shown with visual marker. | Keep default or future explicit change. | Adding another same-type item does not switch automatically. |
| Saved non-default parties exist | Saved ApplicantParties shown in list/cards below defaults. | Future select/edit/archive actions. | Existing parties remain visible. |
| Create succeeds | New ApplicantParty appears after local update/refetch; success feedback may appear. | Continue / add another / future set default. | Client may optimistically add but must refetch. |
| Create fails validation | Field/root errors are visible and form remains editable. | Correct input and resubmit. | Server remains source of truth. |

## 5. Client Update / Refetch Direction

After successful create:

```text
- client may optimistically add the new ApplicantParty to the visible list;
- if the client knows there was no current/default for the type, it may optimistically show the new item as initial default;
- client must invalidate/refetch ApplicantParties through query cache to obtain authoritative server state.
```

Important boundary:

```text
Client may duplicate simple UX rule for responsiveness.
Server remains source of truth for saved list, current/default selection and verification state.
```

## 6. Action Availability

Current/default selection when one already exists is not a hidden side effect of create.

Future explicit action may allow:

```text
Set as current/default for this applicant type.
```

Future delete/archive action needs warnings based on use/risk:

```text
- unused and NotVerified may be safe to remove;
- used by requests may need archive/hide instead of hard delete;
- used by approved requests should not be hard-deleted without explicit policy.
```

## 7. Accessibility Notes

```text
- current/default marker must not rely only on color;
- applicant type groups should have accessible headings;
- add form fields should have labels;
- success/error messages must be perceivable.
```

## 8. UI Questions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `Q-SC-10-UI-001` | accepted direction | Should Account page show current/default templates separately from all saved ApplicantParties? | Yes: top defaults per type, below saved list/cards. | Account page layout and read model. |
| `Q-SC-10-UI-002` | accepted direction | Should create silently change default when one exists? | No. Existing current/default remains selected. | Prevents surprising future request prefill changes. |
| `Q-SC-10-UI-003` | accepted direction | Should first ApplicantParty of a type initialize default? | Yes, server-truth read/refetch should confirm. | Default/prefill behavior. |
| `Q-SC-10-UI-004` | future review | How does user explicitly change current/default? | Future select default slice. | Default-selection UI/API. |
| `Q-SC-10-UI-005` | future review | Does My Applicant Parties need a separate page? | Future management area; current Account page can show inline cards. | Navigation/scope. |

## 9. Downstream Use

Use this UI spec for:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
future planning/slices/SL-APPL-002-account-applicant-parties-read.md
future planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
future request creation client sidecar
```
