# Slice Extension Points Register

Status: active / L2 review command drafts synchronized

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-CLIENT-API-PLACEMENT-001` | client API placement | New read endpoint wrappers live in `entities/*/api`; new command wrappers live in `features/*/api`; `shared/api` stays transport/generated only. | accepted |
| `CP-CLIENT-API-PLACEMENT-002` | migration | Existing business-specific `shared/api/*Api.ts` wrappers are transitional compatibility; migrate only when a concrete slice touches that API area. | future cleanup |
| `CP-CLIENT-API-PLACEMENT-003` | generated types | Entities/features can import generated OpenAPI types from shared generated artifact and define business aliases locally. | accepted |
| `CP-L2-EMP-VIS-001` | employee request visibility | temporary first-pass policy: all active Employees can see all review-relevant requests. | accepted temporary |
| `CP-L2-EMP-VIS-002` | employee request visibility | department/region/assignment-based filtering is future. | future |
| `CP-L2-EMP-DETAILS-001` | details action slot | `L2-EMP-DETAILS-001.client` exposes optional action slot; feature sidecars render Start/Approve/Reject controls later. | accepted |
| `CP-L2-EMP-DETAILS-002` | details read API placement | Details read wrapper and DTO aliases live in `entities/employee-request/api`. | accepted |
| `CP-L2-EMP-DETAILS-003` | review command API placement | Start/Approve/Reject wrappers live in `features/employee-request/<action>/api`. | accepted |
| `CP-L2-EMP-DETAILS-004` | future details enrichment | documents, verification result, review history and assignment/lock stay future details refinements. | future |
| `CP-L2-REVIEW-START-001` | StartReview entry points | One StartReview feature may be hosted by dashboard/list row and details action area. | accepted |
| `CP-L2-REVIEW-START-002` | StartReview client response | StartReview command success is 204 No Content; read state is refreshed from list/details. | accepted |
| `CP-L2-REVIEW-APPROVE-001` | ApproveReview client sidecar | Server draft exists; client approve action sidecar is future. | future draft |
| `CP-L2-REVIEW-REJECT-001` | RejectReview client sidecar | Server/reject draft contains client notes; standalone reject action/form sidecar can be split later if needed. | future draft |
| `CP-L2-REVIEW-AGREEMENT-001` | AgreementProposalExchange after approve | ApproveReview does not create exchange; proposal/exchange starts in future agreement slices. | future |
| `CP-L2-REVIEW-REJECT-002` | rejection feedback policy | Reject endpoint requires feedback at API boundary; domain optionality changes require separate domain decision. | accepted / future domain cleanup if needed |

## L2 Account / Employee Extension Points

| ID | Area | Current direction | Status |
|---|---|---|---|
| `CP-L2-EMP-ACCOUNT-001` | Employee identity | `Employee : Account`; Employee sessions use Account.Id as Employee.Id. | accepted |
| `CP-L2-EMP-ACCOUNT-002` | persistence | TPH in `L1Accounts` with AccountType/Role discriminator. | accepted direction |
| `CP-L2-EMP-ACCOUNT-003` | compatibility cleanup | If current code has separate Employee profile with AccountId, migrate/cleanup under a scoped persistence/domain slice. | future cleanup |
| `CP-L2-EMP-ACCOUNT-004` | permissions | Department/region/assignment/permission model remains future; do not confuse it with Employee account identity. | future |
