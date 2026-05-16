# Slice Implementation Notes Register

Status: active / client short-draft rules, OpenAPI workflow and ApplicantParty read sidecar synchronized

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-API-GEN-001` | `CC-API-001` | generated artifacts | After API source changes, run `npm run generate:openapi`, `npm run generate:api-types`, stage generated artifacts, then run `npm run check:api`. | accepted |
| `NOTE-API-GEN-002` | `CC-API-001` | generated artifacts | Generated artifacts must be command-produced, not manually reconstructed. | accepted |
| `NOTE-CL-DRAFT-001` | client sidecars | drafting | Client short drafts must follow the canonical shape and use `Lives here / Uses / Owns / Does not own` in implementation flow. | accepted |
| `NOTE-CL-DRAFT-002` | client sidecars | scenario/source | Scenario Flow is scenario-sourced behavior for the slice, not implementation flow. | accepted |
| `NOTE-CL-DRAFT-003` | client sidecars | behavior coverage | Implementation details are not behavior items. | accepted |
| `NOTE-CL-LAYER-001` | client sidecars | architecture | Read-only UI belongs in `entities/<entity>/ui`; command/user-action UI belongs in `features`. | accepted |
| `NOTE-CL-LAYER-002` | client sidecars | shared API | `shared/api` intentionally holds low-level wrappers for multiple API/entity areas because it is the client/server boundary layer. | accepted |
| `NOTE-APPL-002-CLIENT-001` | `SL-APPL-002.client` | client read | ApplicantParty read UI should use flat account list and group by `isCurrentDefault`. | draft/implementation handoff |
| `NOTE-MYREQ-LIST-PLACEMENT-001` | `L1-MY-REQUESTS-READ-LIST.client` | client placement | My Requests read list display should be treated as entity read UI, not feature command UI. | normalization |
| `NOTE-MYREQ-DETAILS-PLACEMENT-001` | `L1-MY-REQUEST-DETAILS.client` | client placement | My Request details display should be treated as entity read UI, not feature command UI. | normalization |
