# Slice Questions Register

Status: active / SL-APPL-003.client full sidecar synchronized

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

## Existing Accepted Directions Still Relevant

| ID | Area | Status | Current direction |
|---|---|---|---|
| `CL-DRAFT-Q-001` | drafting | accepted | Client drafters follow canonical examples and keep form identical unless user asks otherwise. |
| `CL-DRAFT-Q-002` | flows | accepted | Scenario Flow is scenario-sourced user/system behavior for this slice only. |
| `CL-DRAFT-Q-003` | behavior | accepted | Implementation details are not behavior items. |
| `CL-LAYER-Q-001` | shared API | accepted | `shared/api` is the low-level client/server boundary, grouped by layer rather than entity. |
| `CL-LAYER-Q-002` | placement | accepted | Read-only UI belongs in `entities`; command/user action UI belongs in `features`. |
| `SL-APPL-Q-006` | page model | accepted | One Applicant Parties page / section; SC-10B is same-page management addendum. |
