# Slice Implementation Notes Register

Status: active / near-final L2 review and AgreementProposalExchange notes synchronized

## 1. Client API Placement Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-CLIENT-API-PLACEMENT-001` | new client reads | Put read endpoint wrappers, path constants and generated DTO aliases in `entities/<entity>/api`. Use shared `fetchJson` and generated OpenAPI types. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-002` | new client commands | Put command/mutation endpoint wrappers, path constants and generated command DTO/result aliases in `features/<business-action>/api`. Use shared unsafe/CSRF-aware transport. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-003` | shared/api | Keep only transport/generated infrastructure in `shared/api`: `fetchJson`, ProblemDetails/ApiError, CSRF helpers, generated OpenAPI types and generic helpers. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-004` | existing runtime | Existing business-specific `shared/api/*Api.ts` wrappers are transitional compatibility. Do not mass-migrate without concrete slice scope. | future cleanup |
| `IMPL-CLIENT-API-PLACEMENT-005` | generated types | Entities/features may import generated OpenAPI types and define local business aliases. | accepted |

## 2. L2 Employee / Review Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-EMP-ACCOUNT-001` | Employee endpoints | Resolve current Employee from `ClaimTypes.NameIdentifier` as Account.Id; target model has `Employee.Id == Account.Id`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-002` | Review commands | Load `Employee` by id and pass the domain object into `Request.StartReview/ApproveReview/RejectReview`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-003` | Review state | Store scalar Employee ids in owned state: `StartedByEmployeeId`, `CompletedByEmployeeId`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-004` | Compatibility | Do not introduce new target code that requires `Employee.AccountId`; if current code has it, handle in compatibility/cleanup slice. | accepted |
| `IMPL-L2-EMP-ACCOUNT-005` | Client | Client must not submit employeeId; server derives Employee actor from session. | accepted |
| `IMPL-L2-EMP-PAGE-PLACEMENT-001` | Employee request pages | Employee request-area pages live under `pages/employee/requests/*`: dashboard in `pages/employee/requests/dashboard`, details in `pages/employee/requests/details`. | accepted |
| `IMPL-L2-REVIEW-001` | StartReview client | One StartReview feature can be hosted from dashboard/list row and details action area. | accepted |
| `IMPL-L2-REVIEW-002` | ApproveReview | Details-only first pass; approve does not create agreement exchange. | accepted |
| `IMPL-L2-REVIEW-003` | RejectReview | Details-only first pass; feedback optional; do not block submit only because feedback is missing. | accepted |

## 3. L2 AgreementProposalExchange Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-AGR-EXCH-001` | `SL-AGR-EXCH-001` | Initial Employee proposal starts exchange and creates proposal version 1. Do not implement start exchange without document. | accepted |
| `IMPL-L2-AGR-EXCH-002` | `SL-AGR-EXCH-001` | Preconditions: Approved request, no existing exchange, valid AgreementDocumentRef, optional valid ProposalComment, current Employee actor. | accepted |
| `IMPL-L2-AGR-EXCH-003` | `SL-AGR-EXCH-001` | Exchange must store `ClientAccountId` from approved request owner. Do not accept it from request body. | accepted |
| `IMPL-L2-AGR-EXCH-004` | `SL-AGR-EXCH-001` | Do not add `ResponsibleEmployeeId` as authorization guard. Starting Employee is first proposal author only. | accepted |
| `IMPL-L2-AGR-EXCH-005` | `SL-AGR-EXCH-002` | Counter-proposal creates next domain version from max existing version + 1; API/client never selects version. | accepted |
| `IMPL-L2-AGR-EXCH-006` | `SL-AGR-EXCH-002` | Client and Employee counter-proposal branches stay in one implementation slice until behavior diverges. | accepted |
| `IMPL-L2-AGR-EXCH-007` | `SL-AGR-EXCH-002` | Route direction: `POST /api/requests/{requestId}/agreement-exchange/proposals`; do not use `/api/agreement-exchanges/{requestId}/proposals`. | accepted |
| `IMPL-L2-AGR-EXCH-008` | `SL-AGR-EXCH-003` | List read model owns summary only; no full proposal history in list. | accepted |
| `IMPL-L2-AGR-EXCH-009` | `SL-AGR-EXCH-004` | Details read owns active proposal and proposal version history. Use query handler + read repository/projection, not mutating application service. | accepted |
| `IMPL-L2-AGR-EXCH-010` | `SL-AGR-EXCH-005` | Accept active proposal does not create a proposal version. Client-only first pass. | accepted |
| `IMPL-L2-AGR-EXCH-011` | `SL-AGR-EXCH-006` | Final refusal orchestrates separate aggregates: `exchange.FinalRefuseProposal(...)` and `request.MarkAgreementExchangeFailed(...)`. | accepted |
| `IMPL-L2-AGR-EXCH-012` | all agreement commands | Do not add per-command status enums. Use `UnitResult<IReadOnlyList<Error>>` / existing Result/Error model. | accepted |
| `IMPL-L2-AGR-EXCH-013` | all agreement commands | FluentValidation validates DTO shape only. Ownership, turn, lifecycle and active proposal author belong to application/domain. | accepted |
| `IMPL-L2-AGR-EXCH-014` | client sidecars | Agreement exchange read wrappers belong in entities; command wrappers belong in features; do not add `shared/api/agreementExchangeApi.ts`. | accepted |

## 4. Agreement Exchange Read Slice Implementation Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-AGR-EXCH-READ-001` | list/details server reads | Client access filters by `AgreementProposalExchange.ClientAccountId`. | accepted |
| `IMPL-AGR-EXCH-READ-002` | list/details server reads | Employee access first pass allows any active Employee; do not add `ResponsibleEmployeeId` guard. | accepted |
| `IMPL-AGR-EXCH-READ-003` | details read | Use query handler + read repository / Dapper projection; do not introduce application service for read-only projection. | accepted |
| `IMPL-AGR-EXCH-READ-004` | client sidecars | Use shared entity wrappers under `entities/agreement-exchange/api`; do not add `shared/api/agreementExchangeApi.ts`. | accepted |
| `IMPL-AGR-EXCH-READ-005` | client sidecars | Page shells may differ by actor; query/model/widgets stay shared while response shape is common. | accepted |

## 5. OpenAPI / Generated Artifacts Notes

When a server/API slice changes API shape, generated artifacts must be regenerated through tools and included in the implementation handoff/commit:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Do not edit generated artifacts manually.

If `check:api` fails only because generated files have unstaged diffs, include those generated files in the same handoff/commit.

## 6. Current-State Notes

```text
Uploaded drafts and archives are planning input only.
For actual implementation state, inspect GitHub/current branch and generated OpenAPI/types.
Do not mark diagrams or status docs [IMPLEMENTED] unless current repo evidence confirms it.
```
