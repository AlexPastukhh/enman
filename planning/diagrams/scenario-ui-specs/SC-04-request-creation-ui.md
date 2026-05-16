# SC-04 — Client Request Creation UI Spec

Status: current UI scenario spec / source for request creation client sidecar  
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

Applicant data is account-level data:

```text
- current applicant data may be prefilled;
- user may clear and enter new applicant data;
- accepted applicant data becomes current active ApplicantParty before request submit;
- request submit must not depend on client-supplied ApplicantPartyId.
```

## 3. UI Behavior Items

| ID | UI behavior | Status |
|---|---|---|
| `SC-04-UI-001` | Client can open request creation page. | requirement |
| `SC-04-UI-002` | Page shows request data fields, including request details and object address. | requirement |
| `SC-04-UI-003` | Page shows applicant data fields/section as part of request creation journey. | requirement |
| `SC-04-UI-004` | If current active applicant data exists, applicant fields are prefilled. | requirement |
| `SC-04-UI-005` | Client can keep prefilled applicant data for submit. | requirement |
| `SC-04-UI-006` | Client can clear prefilled applicant data and enter new applicant data. | requirement |
| `SC-04-UI-007` | If no current applicant data exists, page guides the client to enter applicant data before submit. | requirement |
| `SC-04-UI-008` | Invalid request/applicant visible data shows validation or error feedback. | requirement |
| `SC-04-UI-009` | Accepted request creation shows success outcome and leads to the next read context. | requirement |
| `SC-04-UI-010` | Request creation does not ask the user to select or submit ApplicantPartyId. | accepted direction |

## 4. UI State / Feedback Matrix

| State | UI-visible behavior | Notes |
|---|---|---|
| Initial load | Shows loading or pending page state if current applicant data/request context is being loaded. | Implementation-specific display belongs to `.client.md`. |
| Current applicant exists | Applicant fields are prefilled from current applicant data. | User may keep or clear. |
| No current applicant exists | Applicant fields are empty/editable and request submission cannot complete until acceptable applicant data exists. | This is not an error-only submit branch. |
| Applicant fields cleared | Applicant fields become empty/editable. | User can enter replacement applicant data. |
| Applicant data accepted | Accepted applicant data becomes current active ApplicantParty. | Exact endpoint/flow belongs to SC-10 / applicant replacement slice. |
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

The clear applicant data action is available when applicant fields are prefilled or currently contain data.

## 7. Accessibility Notes

Applicant and request fields must have accessible labels.

Validation/error feedback must be reachable by assistive technologies according to project accessibility conventions.

Use:

```text
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 8. UI Questions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| `SC-04-UI-Q-001` | assumption | Does inline applicant data replacement reuse SC-10 save behavior or a dedicated request-journey endpoint? | Draft assumes accepted applicant changes go through SC-10 / applicant replacement behavior before request submit. | affects future request client sidecar and applicant replacement slice |
| `SC-04-UI-Q-002` | future review | What exact success route/read context follows request submit? | Draft assumes My Requests / request read context. | affects request client sidecar/E2E |
| `SC-04-UI-Q-003` | future review | Should historical request store applicant snapshot? | Not a UI decision now; may affect displayed request details later. | affects domain/read slices |

## 9. Downstream Use

This UI spec feeds:

```text
future planning/slices/SL-REQ-001-create-connection-request.client.md
planning/slices/slice-scenario-flow-behavior-register.md
client/component test planning
E2E request creation happy path planning
```
