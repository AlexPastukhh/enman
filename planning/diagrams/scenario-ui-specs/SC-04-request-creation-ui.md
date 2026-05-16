# SC-04 — Client Request Creation UI Spec

Status: target UI scenario spec / source for future request creation client sidecar  
Source scenario: `planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md`  
Source DATA: `planning/diagrams/scenario-data/SC-04-request-creation-data.md`  
Behavior item source: `planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md`  
Marker: `[UI-SCENARIO]`

## 1. Purpose

This file captures UI-visible behavior for SC-04 request creation.

It is source material for future `SL-REQ-001-create-connection-request.client.md` and target request applicant-context redesign.

It does not define React components, hooks, CSS files, API adapters or folder placement.

## 2. UI Source Summary

The request creation page must let the client provide request data and choose/provide applicant context.

Applicant context UI direction:

```text
- current/default ApplicantParty for selected type is the initial prefill/default selection;
- if current/default exists, applicant fields are prefilled;
- if current/default is missing, applicant fields are empty;
- user may keep prefilled data;
- user may clear prefilled data and enter new applicant data;
- missing default and cleared prefill both lead to new applicant data path;
- accepted new applicant data creates new ApplicantParty and uses it for this request;
- future UI may offer saved ApplicantParty dropdown/list;
- current/default template remains relevant as initial prefill/default selection.
```

## 3. UI Behavior Items

| ID | UI behavior | Status |
|---|---|---|
| `SC-04-UI-001` | Client can open request creation page. | requirement |
| `SC-04-UI-002` | Page shows request data fields, including request details and object address. | requirement |
| `SC-04-UI-003` | Page shows applicant data fields/section as part of request creation journey. | requirement |
| `SC-04-UI-004` | If current/default ApplicantParty exists for selected type, applicant fields are prefilled. | requirement |
| `SC-04-UI-005` | If current/default ApplicantParty is missing, applicant fields are empty and ready for input. | requirement |
| `SC-04-UI-006` | Client can keep prefilled applicant data for submit. | requirement |
| `SC-04-UI-007` | Client can clear prefilled applicant data and enter new applicant data. | requirement |
| `SC-04-UI-008` | Cleared prefill and missing default use the same new-applicant-data path. | requirement |
| `SC-04-UI-009` | Accepted new applicant data creates a new ApplicantParty and uses it for this request. | target direction |
| `SC-04-UI-010` | If an existing default already exists, UI may offer to make the new ApplicantParty current/default for future requests. | future UI decision |
| `SC-04-UI-011` | Future UI may allow selecting from all saved ApplicantParties with current/default as initial selection. | future UI decision |
| `SC-04-UI-012` | Invalid request/applicant visible data shows validation or error feedback. | requirement |
| `SC-04-UI-013` | Accepted request creation shows success outcome and leads to the next read context. | requirement |
| `SC-04-UI-014` | UI must not let the user spoof ApplicantParty ownership. | accepted direction |

## 4. UI State / Feedback Matrix

| State | UI-visible behavior | Notes |
|---|---|---|
| Initial load | Shows loading or pending page state if applicant templates/request context are being loaded. | Implementation-specific display belongs to `.client.md`. |
| Current/default exists for selected type | Applicant fields are prefilled. | User may keep or clear. |
| Current/default missing | Applicant fields are empty/editable. | Clear action is not needed because there is no prefill. |
| Applicant fields cleared | Applicant fields become empty/editable. | Same path as missing default. |
| New applicant data accepted | New ApplicantParty is created and used for request. | If no default existed, it may initialize default; if default existed, offer explicit change. |
| Request submit accepted | User sees success outcome and can proceed to read context. | Use `CL-COMMAND-001` when response body is not needed. |
| Request submit rejected | User sees field/form/global feedback and can correct data. | Use client error conventions. |

## 5. Validation / Error Feedback

UI must make visible:

```text
- missing/invalid request details;
- missing/invalid object address;
- missing/invalid applicant fields when new applicant data is required;
- server/application errors as field, form/action or page/global feedback depending on scope.
```

Use:

```text
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
```

## 6. Action Availability

Request submit should not complete if applicant context is missing/invalid.

Clear applicant fields action is needed only when applicant fields were prefilled.

If no current/default is available, fields start empty and no clear action is required.

Future saved ApplicantParty dropdown/list should keep current/default as initial selection.

## 7. Accessibility Notes

Applicant and request fields must have accessible labels.

Current/default and selected ApplicantParty markers must not rely on color alone.

Validation/error feedback must be reachable by assistive technologies.

Use:

```text
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 8. UI Questions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `SC-04-UI-Q-001` | accepted direction | Are missing default and cleared prefill the same path? | Yes. Both lead to entering new applicant data. | client flow, behavior coverage |
| `SC-04-UI-Q-002` | accepted direction | What happens after new applicant data is accepted? | New ApplicantParty is created and used for this request. | API contract, atomicity, tests |
| `SC-04-UI-Q-003` | future review | Should user be asked to make new ApplicantParty current/default if a default exists? | Yes as future UI direction; no silent default switch. | request client sidecar/default selection slice |
| `SC-04-UI-Q-004` | future review | When does saved ApplicantParty dropdown/list appear? | Future extension; current/default remains initial selection. | client sidecar/API read model |
| `SC-04-UI-Q-005` | future review | What exact success route/read context follows request submit? | Draft assumes My Requests / request read context. | request client sidecar/E2E |

## 9. Downstream Use

This UI spec feeds:

```text
future planning/slices/SL-REQ-001-create-connection-request.client.md
planning/slices/SL-REQ-004-create-request-with-applicant-context.md
planning/slices/slice-scenario-flow-behavior-register.md
client/component test planning
E2E request creation happy path planning
```
