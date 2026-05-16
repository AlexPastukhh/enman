# SC-10 — Applicant Data UI Spec

Status: current UI scenario spec draft / applicant template per type policy synchronized  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10-applicant-data.md`  
Behavior item sources: `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md`  
Marker: `[UI-SCENARIO]`

## 1. Purpose

This UI spec captures UI-visible behavior for Account page / Applicant Data flow.

It is intentionally not a client implementation sidecar.

It records what the user should see around saved ApplicantParty profiles, current/default templates per applicant type, successful applicant data save and future request-creation prefill behavior.

## 2. UI Source Summary

Target UI direction:

```text
Authenticated Client
opens Account page / applicant section
        ↓
UI shows current/default applicant template per supported applicant type, when available
        ↓
Client can enter applicant data for a type
        ↓
Accepted applicant data creates a new saved ApplicantParty
        ↓
New ApplicantParty starts as NotVerified
        ↓
UI offers to make it the current/default template for that applicant type
```

Current L1 client implementation is narrower:

```text
current implementation covers first-stage individual applicant create/read behavior;
multi-type templates and all saved ApplicantParty management are future scenario direction.
```

## 3. UI Behavior Items

| UI behavior item | Requirement | Status |
|---|---|---|
| `SC-10-UI-001` | Authenticated client can reach an Account page / applicant section that owns applicant data presentation. | accepted direction |
| `SC-10-UI-002` | Account page can show current/default ApplicantParty template for supported applicant type. | target direction |
| `SC-10-UI-003` | If no current/default template exists for a supported type, the UI can show an empty applicant form or add action for that type. | target direction |
| `SC-10-UI-004` | For current narrow L1 individual applicant flow, the form collects full name, email and phone number. | current implementation alignment |
| `SC-10-UI-005` | Accepted applicant data creates a new saved ApplicantParty. | target direction |
| `SC-10-UI-006` | New ApplicantParty is shown as NotVerified until request/review verification changes it. | target direction |
| `SC-10-UI-007` | After successful save, UI can show applicant data as filled/read-only saved data. | accepted direction |
| `SC-10-UI-008` | UI offers to make the newly saved ApplicantParty current/default template for its applicant type. | target direction |
| `SC-10-UI-009` | Adding new ApplicantParty does not visually remove or overwrite older ApplicantParties. | target direction |
| `SC-10-UI-010` | Applicant data create UI does not introduce a create-request entry point. | accepted direction |
| `SC-10-UI-011` | Future My Applicant Parties management can list all saved ApplicantParties and support details/add/edit/delete/archive/set-default actions. | future scenario |
| `SC-10-UI-012` | Future request creation may select from all saved ApplicantParties, but the current/default template remains the initial prefill. | future request client direction |

## 4. UI State / Feedback Matrix

| UI state | Visible outcome | User action | Notes |
|---|---|---|---|
| Account page loading applicant templates | Loading or pending state for applicant section. | Wait / retry if failed. | Concrete display belongs to `.client.md`. |
| Current/default template exists for type | Saved applicant data is visible as current/default for that type. | Keep, edit later, or add another ApplicantParty. | Current/default means future prefill only. |
| No current/default template exists for type | Empty applicant form or add action is visible. | Enter applicant data. | Not an error; it is setup state. |
| Save succeeds | New ApplicantParty is saved, visible and NotVerified. | Optionally set as current/default template for its type. | Existing ApplicantParties remain stored. |
| Save fails validation | Field/root errors are visible and form remains editable. | Correct input and resubmit. | Server remains source of truth. |
| Set current/default confirmed | The selected ApplicantParty becomes the template for future prefill for that type. | Continue account work/request creation later. | Does not affect old requests. |
| Future delete/archive requested | UI warns depending on verification/use by requests. | Confirm/cancel. | Exact delete/archive semantics are future. |

## 5. Validation / Error Feedback

UI validation direction:

```text
- validate obvious requiredness and shape for good UX;
- keep server validation as source of truth;
- map ProblemDetails to field-level and root/global errors;
- do not assume client validation fully matches future server/domain rules.
```

For current narrow individual applicant data, client-side checks may include:

```text
- full name fields are not blank;
- email has email-like shape;
- phone has accepted visible shape when a clear rule is available.
```

## 6. Action Availability

Create-request entry rule for this UI spec:

```text
Do not show or introduce a create-request entry from the applicant data create UI slice.
```

Reason:

```text
The exact global entry point for request creation belongs to request creation client planning.
Applicant templates provide prefill/reference data; they do not own request creation navigation.
```

Set-current/default rule:

```text
When new ApplicantParty is created, UI should offer to make it current/default for its applicant type.
```

Future select-from-all rule:

```text
Request creation may later show a dropdown of all saved ApplicantParties.
The current/default template for the selected applicant type remains the initial prefill/default selection.
```

## 7. Accessibility Notes

Concrete accessibility implementation belongs to the client sidecar, but UI-visible expectations include:

```text
- form errors are perceivable;
- success notification is announced or otherwise visible long enough to understand;
- current/default marker is understandable;
- NotVerified status is visible when shown;
- set-current/default action has clear accessible name and scope;
- delete/archive warnings are clear and confirmable.
```

## 8. UI Questions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `Q-SC-10-UI-001` | future review | Should newly created ApplicantParty become current/default automatically when no current/default exists for its type? | Current direction: UI should at least offer setting it as current/default; exact default state can be decided per client slice. | Affects Account page and request creation prefill. |
| `Q-SC-10-UI-002` | accepted direction | Should applicant data create UI show create-request entry? | No. Request entry belongs to request creation client sidecar. | Keeps applicant templates separate from request navigation. |
| `Q-SC-10-UI-003` | future review | Should Account page show verification status? | Yes when the read model exposes it; NotVerified is important for newly saved profiles. | Affects read model and visible status labels. |
| `Q-SC-10-UI-004` | future review | How should delete/archive warnings differ by ApplicantParty use? | Safer profiles may be deletable; profiles used by requests/approved requests need warning or archive-only behavior. | Affects My Applicant Parties future management. |
| `Q-SC-10-UI-005` | future review | Should edit mutate profile in place or create a new version? | Prefer not to break historical request context; decide when edit slice starts. | Affects request detail accuracy and audit. |

## 9. Downstream Use

Use this UI spec for:

```text
- `SL-APPL-001-create-individual-applicant-party.client.md`, when concrete client sidecar work is created/updated;
- future current/default applicant template read planning;
- future My Applicant Parties planning;
- future request creation client applicant selection/prefill planning;
- future applicant edit/delete/archive planning;
- scenario diagram prompt preparation for Account page / applicant data flow.
```

Do not use this file as a React component structure, CSS/layout spec or API adapter plan.
