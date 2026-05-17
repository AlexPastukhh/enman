# Slice Implementation Notes Register

Status: active / L2 review command drafts synchronized

## Client API Placement Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-CLIENT-API-PLACEMENT-001` | new client reads | Put read endpoint wrappers, path constants and generated DTO aliases in `entities/<entity>/api`. Use shared `fetchJson` and generated OpenAPI types. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-002` | new client commands | Put command/mutation endpoint wrappers, path constants and generated command DTO/result aliases in `features/<business-action>/api`. Use shared unsafe/CSRF-aware transport. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-003` | shared/api | Keep only transport/generated infrastructure in `shared/api`: `fetchJson`, ProblemDetails/ApiError, CSRF helpers, generated OpenAPI types and generic helpers. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-004` | existing runtime | Existing business-specific `shared/api/*Api.ts` wrappers are transitional compatibility. Do not mass-migrate without concrete slice scope. | future cleanup |
| `IMPL-CLIENT-API-PLACEMENT-005` | generated types | Entities/features may import generated OpenAPI types and define local business aliases. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-006` | old sidecars | If an older sidecar says “Shared API wrapper owns low-level HTTP call”, interpret it through the new rule: entity/feature API owns business wrapper; shared owns transport/generated infrastructure only. | compatibility note |

## L2 Review Command Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-REVIEW-START-CLIENT-001` | `L2-REVIEW-START-001.client` | StartReview has one feature-owned command action with two entry points: dashboard/list row and details action area. | accepted |
| `IMPL-L2-REVIEW-START-CLIENT-002` | same | Command wrapper lives in `features/employee-request/start-review/api/startRequestReview.ts`; do not add `shared/api/employeeRequestApi.ts`. | accepted |
| `IMPL-L2-REVIEW-START-CLIENT-003` | same | StartReview success contract is 204 No Content. Client refetches list/details read queries. | accepted |
| `IMPL-L2-REVIEW-APPROVE-001` | `SL-EMP-REQ-004` | ApproveReview success is 204 No Content and does not create AgreementProposalExchange. | accepted |
| `IMPL-L2-REVIEW-APPROVE-002` | `SL-EMP-REQ-004` | Verify review was started by current Employee before approval. | accepted |
| `IMPL-L2-REVIEW-REJECT-001` | `SL-EMP-REQ-005` | RejectReview success is 204 No Content; updated state is read through list/details after refetch. | accepted |
| `IMPL-L2-REVIEW-REJECT-002` | `SL-EMP-REQ-005` | Reject endpoint requires non-empty feedback at API boundary; do not silently make domain feedback required without separate decision. | accepted |
| `IMPL-L2-REVIEW-REJECT-003` | `SL-EMP-REQ-005` | Reject client wrapper belongs in `features/employee-request/reject-review/api`, not `shared/api`. | accepted |

## L2 Employee Details Client Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-EMP-DETAILS-CLIENT-001` | `L2-EMP-DETAILS-001.client` | Details read wrapper lives in `entities/employee-request/api/getEmployeeRequestDetails.ts`. | accepted |
| `IMPL-L2-EMP-DETAILS-CLIENT-002` | same | Generated DTO aliases for details read live near the entity in `entities/employee-request/api/employeeRequestApiTypes.ts`. | accepted |
| `IMPL-L2-EMP-DETAILS-CLIENT-003` | same | Do not add `shared/api/employeeRequestApi.ts` for the details read. `shared/api` remains generic infrastructure only. | accepted |
| `IMPL-L2-EMP-DETAILS-CLIENT-005` | same | StartReview command output is not a details DTO; current StartReview direction is 204 No Content. | accepted |
| `IMPL-L2-EMP-DETAILS-CLIENT-006` | future review command sidecars | Start/approve/reject command wrappers live in `features/employee-request/<action>/api`, not in `entities` and not in `shared/api`. | accepted |

## L2 Account / Employee Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-EMP-ACCOUNT-001` | Employee endpoints | Resolve current Employee from `ClaimTypes.NameIdentifier` as Account.Id; target model has `Employee.Id == Account.Id`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-002` | Review commands | Load `Employee` by id and pass the domain object into `Request.StartReview/ApproveReview/RejectReview`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-003` | Review state | Store scalar Employee ids in owned state: `StartedByEmployeeId`, `CompletedByEmployeeId`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-004` | Compatibility | Do not introduce new target code that requires `Employee.AccountId`; if current code has it, handle in compatibility/cleanup slice. | accepted |
| `IMPL-L2-EMP-ACCOUNT-005` | Client | Client must not submit employeeId; server derives Employee actor from session. | accepted |
| `IMPL-L2-EMP-PAGE-PLACEMENT-001` | Employee request pages | Employee request-area pages live under `pages/employee/requests/*`: dashboard in `pages/employee/requests/dashboard`, details in `pages/employee/requests/details`. Do not place dashboard under `pages/employee/dashboard`. | accepted |
