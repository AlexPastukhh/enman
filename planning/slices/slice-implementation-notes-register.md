# Slice Implementation Notes Register

Status: active / SL-APPL-003.client full sidecar synchronized

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-APPL-003-CLIENT-001` | `SL-APPL-003.client` | client architecture | Make current/default is a command/user-action feature, not entity read UI. Place button/action/mutation under `features/applicant-party/make-current-default`. | full sidecar |
| `NOTE-APPL-003-CLIENT-002` | `SL-APPL-003.client` | entity UI | ApplicantParty list/card remains read/display UI. Add an optional action slot/render prop instead of moving mutation logic into entity UI. | full sidecar |
| `NOTE-APPL-003-CLIENT-003` | `SL-APPL-003.client` | shared API | Add low-level shared API wrapper/path after verifying generated contracts expose backend command. Do not manually edit generated artifacts. | implementation handoff |
| `NOTE-APPL-003-CLIENT-004` | `SL-APPL-003.client` | state refresh | After success, refresh/update account ApplicantParties read state and rely on server truth for highlighted current/default state. | implementation handoff |
| `NOTE-APPL-003-CLIENT-005` | `SL-APPL-003.client` | UX | Hide/disable action for already current/default cards; show pending/error near selected action/card when practical. | implementation handoff |
| `NOTE-APPL-003-CLIENT-006` | `SL-APPL-003.client` | tests | Client tests assert visible state and wrapper behavior; backend DB transition is tested in server slice tests, not client tests. | implementation handoff |
| `NOTE-API-GEN-001` | API-changing work | generated artifacts | After API source changes, run `npm run generate:openapi`, `npm run generate:api-types`, stage generated artifacts, then run `npm run check:api`. | accepted |
| `NOTE-API-GEN-002` | API-changing work | generated artifacts | Generated artifacts must be command-produced, not manually reconstructed. | accepted |
| `NOTE-CL-DRAFT-001` | client sidecars | drafting | Client short drafts must follow canonical shape and use `Lives here / Uses / Owns / Does not own` in implementation flow. | accepted |
| `NOTE-CL-DRAFT-002` | client sidecars | scenario/source | Scenario Flow is scenario-sourced behavior for the slice, not implementation flow. | accepted |
| `NOTE-CL-DRAFT-003` | client sidecars | behavior coverage | Implementation details are not behavior items. | accepted |
| `NOTE-CL-LAYER-001` | client sidecars | architecture | Read-only UI belongs in `entities/<entity>/ui`; command/user-action UI belongs in `features`. | accepted |
| `NOTE-CL-LAYER-002` | client sidecars | shared API | `shared/api` intentionally holds low-level wrappers for multiple API/entity areas because it is the client/server boundary layer. | accepted |
| `NOTE-REQ-CLIENT-001` | `SL-REQ-001.client` | client architecture | Request creation UI is a command sidecar: page + features + entities + shared/api + generated contracts. | implemented/drafted |
| `NOTE-SERVER-TEST-001` | server slice drafts | testing | State-changing backend command slices should use API integration tests with DB state assertions as primary proof, not mocks. | accepted |
