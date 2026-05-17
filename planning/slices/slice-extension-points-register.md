# Slice Extension Points Register

Status: active / client API placement and L2 Employee Details sidecar synchronized

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-CLIENT-API-PLACEMENT-001` | client API placement | New read endpoint wrappers live in `entities/*/api`; new command wrappers live in `features/*/api`; `shared/api` stays transport/generated only. | accepted |
| `CP-CLIENT-API-PLACEMENT-002` | migration | Existing business-specific `shared/api/*Api.ts` wrappers are transitional compatibility; migrate only when a concrete slice touches that API area. | future cleanup |
| `CP-CLIENT-API-PLACEMENT-003` | generated types | Entities/features can import generated OpenAPI types from shared generated artifact and define business aliases locally. | accepted |
| `CP-APPL-DEFAULT-004` | explicit default action client | client same-page action belongs to `SL-APPL-003.client`; command API wrapper belongs in `features/applicant-party/make-current-default/api`. | full sidecar draft |
| `CP-L2-EMP-VIS-001` | employee request visibility | temporary first-pass policy: all active Employees can see all review-relevant requests. | accepted temporary |
| `CP-L2-EMP-VIS-002` | employee request visibility | department/region/assignment-based filtering is future. | future |
| `CP-L2-EMP-DETAILS-001` | details action slot | `L2-EMP-DETAILS-001.client` exposes optional action slot; feature sidecars render Start/Approve/Reject controls later. | accepted |
| `CP-L2-EMP-DETAILS-002` | details read API placement | Details read wrapper and DTO aliases live in `entities/employee-request/api`. | accepted |
| `CP-L2-EMP-DETAILS-003` | review command API placement | Start/Approve/Reject wrappers live in `features/employee-request/<action>/api`. | accepted |
| `CP-L2-EMP-DETAILS-004` | future details enrichment | documents, verification result, review history and assignment/lock stay future details refinements. | future |
