# Slice Questions Register

Status: active / L2 Employee Review client command questions synchronized

## 1. Current L2 Employee Review Questions

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-L2-REVIEW-START-CLIENT-001` | `planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md` | contract dependency | blocked | Is backend endpoint and generated OpenAPI contract available? | Runtime client implementation waits for `SL-EMP-REQ-003` backend endpoint plus generated types. | Shared API wrapper, feature tests and E2E timing. |
| `Q-L2-REVIEW-START-CLIENT-002` | same | generated contract | blocked | What are exact generated operation/type names? | Use OpenAPI after server implementation/generation; do not invent names. | Prevents generated contract drift. |
| `Q-L2-REVIEW-START-CLIENT-003` | same | double-submit UX | assumption | Same Employee double-clicks Start Review? | Button pending state disables double-submit; backend may return idempotent success. | Mutation UX and stale-state handling. |
| `Q-L2-REVIEW-START-CLIENT-004` | same | stale state | assumption | Another Employee starts review between read and click? | Server rejects; client shows lifecycle/conflict feedback and refreshes details. | Error UX and refresh behavior. |
| `Q-L2-REVIEW-START-CLIENT-005` | same | scope | accepted | Is this approve/reject? | No. Start review only. | Keeps decision sidecars separate. |
| `Q-L2-REVIEW-START-CLIENT-006` | same | security | accepted | Does client submit Employee id? | No. Server resolves Employee from session/auth. | Prevents spoofing. |
| `Q-L2-REVIEW-START-CLIENT-007` | same | CSRF | accepted | Is this unsafe request? | Yes. POST uses shared CSRF-aware boundary. | Cross-cutting requirement. |
| `Q-L2-REVIEW-START-CLIENT-008` | same | placement | accepted | Where does button live? | `features/employee-request/start-review/ui`. | Command feature placement. |
| `Q-L2-REVIEW-START-CLIENT-009` | same | composition | accepted | Where does action appear? | In Employee request details page action slot when details read model says available. | Page/entity/feature composition. |
| `Q-L2-REVIEW-START-CLIENT-010` | same | scope | accepted | Should dashboard show StartReview directly? | Not first pass. Start action belongs to details action area. | Dashboard remains read sidecar. |

## 2. Current ApplicantParty / L1 Questions Still Relevant

| ID | Local file(s) | Area | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|---|---|
| `Q-SL-APPL-003-CLIENT-001` | `SL-APPL-003-select-current-default-applicant-party-template.client.md` | backend dependency | resolved | Is backend endpoint available? | Yes, backend command endpoint is implemented. | Client can proceed after generated/client contract sync. |
| `Q-SL-APPL-003-CLIENT-002` | same | behavior | accepted | Is switching implicit on create? | No. This is explicit same-page user action. | Keeps create additive. |
| `Q-SL-APPL-003-CLIENT-003` | same | layering | accepted | Where does visible action live? | Feature owns button/action; entity card/list receives optional action slot. | Preserves read vs command boundary. |
| `Q-SL-APPL-003-CLIENT-004` | same | architecture | accepted | Where does mutation live? | `features/applicant-party/make-current-default`. | Command architecture. |
| `Q-SL-APPL-003-CLIENT-005` | same | request safety | accepted | Does command update existing requests? | No. Existing requests remain unchanged. | Scope boundary and E2E non-goal. |
| `Q-SL-APPL-003-CLIENT-006` | same | UX | accepted | What happens if selected already current/default? | UI hides/disables action; backend is safe/idempotent. | Avoids duplicate action. |
| `Q-SL-APPL-003-CLIENT-007` | same | API | implemented server direction | Response `200` or `204`? | Current backend declares `200 OK`; wrapper treats success as `void`. | API wrapper/tests. |
| `Q-SL-APPL-003-CLIENT-008` | same | error UX | implementation decision | Error placement? | Prefer feature-owned per-action error, with page fallback for unexpected errors. | UX/tests. |
| `Q-SL-APPL-003-CLIENT-009` | same | generated artifacts | implementation check | Are generated artifacts already exposing endpoint? | Verify before client code. If missing, run generation workflow. | Prevents manual generated edits. |
| `Q-SL-APPL-003-CLIENT-010` | same | page placement | implementation check | Is target Applicant Parties page already replacing old AccountPage? | Verify current UI. The action may be wired into existing AccountPage or future Applicant Parties page. | Placement and route/page scope. |

## 3. Existing Accepted Directions Still Relevant

| ID | Area | Status | Current direction |
|---|---|---|---|
| `CL-DRAFT-Q-001` | drafting | accepted | Client drafters follow canonical examples and keep form identical unless user asks otherwise. |
| `CL-DRAFT-Q-002` | flows | accepted | Scenario Flow is scenario-sourced user/system behavior for this slice only. |
| `CL-DRAFT-Q-003` | behavior | accepted | Implementation details are not behavior items. |
| `CL-LAYER-Q-001` | shared API | accepted | `shared/api` is the low-level client/server boundary, grouped by layer rather than entity. |
| `CL-LAYER-Q-002` | placement | accepted | Read-only UI belongs in `entities`; command/user action UI belongs in `features`. |
| `SL-APPL-Q-006` | page model | accepted | One Applicant Parties page / section; SC-10B is same-page management addendum. |
