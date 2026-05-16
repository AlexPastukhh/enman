# SC-04 — Client Request Creation UI Spec

Status: current UI scenario spec / source for request creation client sidecar / per-type ApplicantParty template direction synchronized  
Source scenario: `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md`  
Source DATA: `planning/diagrams/scenario-data/SC-04-request-creation-data.md`  
Behavior item source: `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md`  
Marker: `[UI-SCENARIO]`

## 1. Purpose

This file captures UI-visible behavior for SC-04 request creation.

It is source material for future `SL-REQ-001-create-connection-request.client.md` and related tests.

It does not define React components, hooks, CSS files, API adapters or folder placement.

## 2. UI Source Summary

The request creation page must let the client provide request data and applicant data needed before request submit.

Applicant data is account-owned ApplicantParty data:

```text
- current/default ApplicantParty for the selected applicant type may prefill fields;
- user may keep prefilled data;
- user may clear prefilled data and enter new applicant data;
- if no current/default template exists, fields are empty and the same new-applicant-data path is used;
- accepted new applicant data creates a new ApplicantParty and uses it for this request;
- after creating new ApplicantParty, UI offers to make it current/default template for its type;
- request submit must not depend on arbitrary spoofed ApplicantPartyId.
```

Future UI direction:

```text
Request creation may later offer a dropdown/list of all saved ApplicantParties.
The current/default template remains the initial prefill/default selection.
```

## 3. UI Behavior Items

| ID | UI behavior | Status |
|---|---|---|
| `SC-04-UI-001` | Client can open request creation page. | requirement |
| `SC-04-UI-002` | Page shows request data fields, including request details and object address. | requirement |
| `SC-04-UI-003` | Page shows applicant data fields/section as part of request creation journey. | requirement |
| `SC-04-UI-004` | If current/default ApplicantParty exists for selected applicant type, applicant fields are prefilled. | requirement |
| `SC-04-UI-005` | Client can keep prefilled applicant data and use the existing ApplicantParty for the request. | requirement |
| `SC-04-UI-006` | Client can clear prefilled applicant data and enter new applicant data. | requirement |
| `SC-04-UI-007` | If no current/default ApplicantParty exists, applicant fields are empty and no clear action is required before entering new data. | requirement |
| `SC-04-UI-008` | Accepted new applicant data creates a new ApplicantParty and uses it for this request. | requirement |
| `SC-04-UI-009` | After new ApplicantParty is created, UI offers to make it current/default template for its applicant type. | requirement |
| `SC-04-UI-010` | Invalid request/applicant visible data shows validation or error feedback. | requirement |
| `SC-04-UI-011` | Accepted request creation shows success outcome and leads to the next read context. | requirement |
| `SC-04-UI-012` | Request creation does not ask the user to select or submit ApplicantPartyId outside allowed account-owned selection. | accepted direction |
| `SC-04-UI-013` | Future UI may provide dropdown/list of all saved ApplicantParties, while current/default template remains initial prefill/default selection. | future direction |

## 4. UI State / Feedback Matrix

| State | UI-visible behavior | Notes |
|---|---|---|
| Initial load | Shows loading or pending page state if current/default template/request context is being loaded. | Implementation-specific display belongs to `.client.md`. |
| Current/default template exists | Applicant fields are prefilled from current/default template. | User may keep or clear. |
| No current/default template exists | Applicant fields are empty/editable. | This is the same new applicant data path without clear action. |
| Applicant fields cleared | Applicant fields become empty/editable. | User can enter new ApplicantParty data. |
| New applicant data accepted | New ApplicantParty is created, used for this request and starts NotVerified. | Existing ApplicantParties remain stored. |
| Set-current/default offer visible | UI asks/offers to make the new ApplicantParty the current/default template for its type. | Exact control belongs to client sidecar. |
| Future dropdown/list | UI can show saved ApplicantParties; current/default is initial prefill/default selection. | Future request client work. |
| Request submit accepted | User sees success outcome and can proceed to read context. | Use `CL-COMMAND-001` when response body is not needed. |
| Request submit rejected | User sees field/form/global feedback and can correct data. | Use client error conventions. |

## 5. Validation / Error Feedback

UI must make visible:

```text
- missing/invalid request details;
- missing/invalid object address;
- missing/invalid applicant fields when applicant data must be provided;
- server/application errors as field, form/action or page/global feedback depending on scope.
```

Use client-wide conventions:

```text
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
```

## 6. Action Availability

Request submit should not complete if applicant data is missing/invalid.

The UI may still let the user type request data while applicant data is missing.

The clear applicant data action is needed only when applicant fields were prefilled or contain values.

When no current/default ApplicantParty exists, fields are empty by default and no clear action is required.

When new ApplicantParty is created, the set-current/default offer should be visible or otherwise understandable.

## 7. Accessibility Notes

Applicant and request fields must have accessible labels.

Validation/error feedback must be reachable by assistive technologies according to project accessibility conventions.

The set-current/default offer should have clear accessible text, especially because it affects future request prefill.

Use:

```text
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 8. UI Questions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `SC-04-UI-Q-001` | assumption | Does new applicant data create ApplicantParty during request creation? | Yes. Accepted new data creates a new ApplicantParty and uses it for this request. | affects request server/client contract |
| `SC-04-UI-Q-002` | accepted direction | Should creating new ApplicantParty replace old ApplicantParty? | No. Old ApplicantParties remain stored. | affects data model and history |
| `SC-04-UI-Q-003` | assumption | Should UI offer setting new ApplicantParty as current/default template? | Yes. New profile is used for this request and UI offers making it future prefill/default for its type. | affects request client UI |
| `SC-04-UI-Q-004` | future review | Should new ApplicantParty become current/default automatically if no template exists? | Open; offer/set-current behavior should be decided in client/API slice. | affects prefill and UX |
| `SC-04-UI-Q-005` | future review | What exact success route/read context follows request submit? | Draft assumes My Requests / request read context. | affects request client sidecar/E2E |
| `SC-04-UI-Q-006` | future review | Should historical request store applicant snapshot/version? | Not a UI decision now; may affect request details display. | affects domain/read slices |

## 9. Downstream Use

This UI spec feeds:

```text
future planning/slices/SL-REQ-001-create-connection-request.client.md
future request creation API contract revision
future applicant template/current-default slice planning
planning/slices/slice-scenario-flow-behavior-register.md
client/component test planning
E2E request creation happy path planning
```
