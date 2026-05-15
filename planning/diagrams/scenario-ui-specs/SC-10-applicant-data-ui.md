# SC-10 — Applicant Data UI Spec

Status: current UI scenario spec draft / extracted from applicant client-slice planning notes  
Source scenario: `planning/diagrams/scenario-text-specs/SC-10-applicant-data.md`  
Source DATA: `planning/diagrams/scenario-data/SC-10-applicant-data.md`  
Behavior item sources: `planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md`

## 1. Purpose

This UI spec captures UI-visible behavior for the Applicant Data / Account page flow.

It is intentionally not a client implementation sidecar.

It records what the user should see and how the UI should behave around current applicant data, successful applicant data creation, and future request-creation entry decisions.

## 2. UI Source Summary

Current L1 client planning direction:

```text
Authenticated Client
opens Account page / applicant section
        ↓
UI determines whether current applicant party exists
        ↓
if current applicant exists:
  show applicant data as read-only
else:
  show applicant data creation form
```

For the current applicant-party create client work, the UI may start with a command-only flow:

```text
no current applicant read endpoint yet
        ↓
show create applicant form
        ↓
submit fullName/email/phoneNumber
        ↓
HTTP success
        ↓
show submitted applicant data as read-only local state
        ↓
show Edit action
        ↓
show self-dismissing success notification
```

Stable Account page refresh requires a future current-applicant read slice.

## 3. UI Behavior Items

| UI behavior item | Requirement | Status |
|---|---|---|
| `SC-10-UI-001` | Authenticated client can reach an Account page / applicant section that owns applicant data presentation. | accepted direction |
| `SC-10-UI-002` | If current applicant data is missing, the UI shows an editable applicant data form. | accepted direction |
| `SC-10-UI-003` | For the current narrow L1 individual applicant flow, the form collects full name, email and phone number. | current implementation alignment |
| `SC-10-UI-004` | After successful save, the UI shows applicant data as filled/read-only. | accepted direction |
| `SC-10-UI-005` | After successful save, the UI shows a self-dismissing success notification. | accepted direction |
| `SC-10-UI-006` | After successful save, the UI shows an Edit action, but the actual edit/replacement flow is separate. | accepted direction / future slice |
| `SC-10-UI-007` | The applicant data create UI does not introduce a create-request entry point. | accepted direction |
| `SC-10-UI-008` | The exact global create-request entry location remains a future request-creation client decision. | open downstream question |
| `SC-10-UI-009` | After refresh, the Account page should eventually load current applicant data from a read model rather than relying on local post-submit state. | future read slice |
| `SC-10-UI-010` | Future Account page may show applicant verification state when the read model exposes it. | future review |

## 4. UI State / Feedback Matrix

| UI state | Visible outcome | User action | Notes |
|---|---|---|---|
| Account page loading current applicant state | Loading or pending state for applicant section. | Wait / retry if failed. | Requires future current-applicant read slice for stable refresh behavior. |
| No current applicant known | Editable applicant form is visible. | Fill and submit applicant data. | Current create client work may start here. |
| Submit pending | Form prevents duplicate submit and shows pending feedback. | Wait. | Concrete pending UI belongs to `.client.md`. |
| Save succeeds | Submitted applicant data is shown read-only, success notification appears, Edit action appears. | Continue account work later / edit later. | No create-request entry is introduced by this slice. |
| Save fails validation | Field/root errors are visible and form remains editable. | Correct input and resubmit. | Server remains source of truth. |
| Current applicant exists on page load | Applicant data summary is shown read-only. | Edit action may be visible. | Needs future current-applicant read endpoint/model. |
| Future verification state available | Verification state is visible near applicant summary. | Follow verification/update flow if required. | Verification workflow is not part of the create command. |

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
Do not show or introduce a create-request entry from the applicant-party create UI slice.
```

Reason:

```text
The exact global entry point for request creation is not decided yet.
It may later live in the header, Account page, My Requests page or another navigation area.
```

Current applicant data availability after save should be described carefully:

```text
Applicant party data is now available for future request creation flow.
The UI does not introduce create request entry in this slice.
```

Do not use the stronger wording:

```text
Can continue toward request creation.
```

unless the request-creation entry point is explicitly introduced by a later client slice.

## 7. Accessibility Notes

Concrete accessibility implementation belongs to the client sidecar, but UI-visible expectations include:

```text
- form errors are perceivable;
- success notification is announced or otherwise visible long enough to understand;
- read-only applicant fields are distinguishable from editable fields;
- Edit action has accessible name and clear scope.
```

## 8. UI Questions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `Q-SC-10-UI-001` | open | What read endpoint/model should the Account page use to load current applicant data after refresh? | Draft assumes a future `L1-APPLICANT-PARTY-READ-CURRENT` slice with auth-derived current account context. | Needed before Account page can be stable after refresh. |
| `Q-SC-10-UI-002` | accepted direction | Should applicant-party create UI show create-request entry before current applicant exists? | No. Do not show create-request entry in this slice. | Request creation entry location remains future request-creation client decision. |
| `Q-SC-10-UI-003` | future review | Should Account page show applicant verification status? | Yes when the current applicant read model exposes it; not required for the first create command. | Affects future read/verification UI. |
| `Q-SC-10-UI-004` | accepted direction | Should applicant creation refetch current-user/session? | No. Session/current-user is account auth state. Applicant state belongs to applicant read model unless current-user contract changes. | Prevents accidental coupling of applicant data with auth session. |
| `Q-SC-10-UI-005` | future review | What does Edit action do? | Edit action is visible after save, but edit/replacement behavior is a future slice. | Future applicant replacement/versioning slice. |

## 9. Downstream Use

Use this UI spec for:

```text
- `SL-APPL-001-create-individual-applicant-party.client.md`, when concrete client sidecar work is created/updated;
- future `L1-APPLICANT-PARTY-READ-CURRENT` slice planning;
- future applicant edit/replacement slice planning;
- future request creation client entry-point planning;
- scenario diagram prompt preparation for Account page / applicant data flow.
```

Do not use this file as a React component structure, CSS/layout spec or API adapter plan.
