# Slice Implementation Notes Register

Status: active / L2 review, client API placement and AgreementProposalExchange canonical implementation notes synchronized

## 1. Client API Placement Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-CLIENT-API-PLACEMENT-001` | new client reads | Put read endpoint wrappers, path constants and generated DTO aliases in `entities/<entity>/api`. Use shared `fetchJson` and generated OpenAPI types. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-002` | new client commands | Put command/mutation endpoint wrappers, path constants and generated command DTO/result aliases in `features/<business-action>/api`. Use shared unsafe/CSRF-aware transport. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-003` | shared/api | Keep only transport/generated infrastructure in `shared/api`: `fetchJson`, ProblemDetails/ApiError, CSRF helpers, generated OpenAPI types and generic helpers. | accepted |
| `IMPL-CLIENT-API-PLACEMENT-004` | existing runtime | Existing business-specific `shared/api/*Api.ts` wrappers are transitional compatibility. Do not mass-migrate without concrete slice scope. | future cleanup |
| `IMPL-CLIENT-API-PLACEMENT-005` | generated types | Entities/features may import generated OpenAPI types and define local business aliases. | accepted |

## 2. L2 Employee / Review Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-EMP-ACCOUNT-001` | Employee endpoints | Resolve current Employee from `ClaimTypes.NameIdentifier` as Account.Id; target model has `Employee.Id == Account.Id`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-002` | Review commands | Load `Employee` by id and pass the domain object into `Request.StartReview/ApproveReview/RejectReview`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-003` | Review state | Store scalar Employee ids in owned state: `StartedByEmployeeId`, `CompletedByEmployeeId`. | accepted |
| `IMPL-L2-EMP-ACCOUNT-004` | Compatibility | Do not introduce new target code that requires `Employee.AccountId`; if current code has it, handle in compatibility/cleanup slice. | accepted |
| `IMPL-L2-EMP-ACCOUNT-005` | Client | Client must not submit employeeId; server derives Employee actor from session. | accepted |
| `IMPL-L2-EMP-PAGE-PLACEMENT-001` | Employee request pages | Employee request-area pages live under `pages/employee/requests/*`: dashboard in `pages/employee/requests/dashboard`, details in `pages/employee/requests/details`. | accepted |
| `IMPL-L2-REVIEW-REJECT-001` | RejectReview | Feedback/body is optional by current direction; client must not block empty feedback unless generated server contract intentionally requires it. | accepted |

## 3. Agreement Exchange Read Slice Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-AGR-EXCH-READ-001` | list/details server reads | Client access filters by `AgreementProposalExchange.ClientAccountId`. | accepted |
| `IMPL-AGR-EXCH-READ-002` | list/details server reads | Employee access first pass allows any active Employee; do not add `ResponsibleEmployeeId` guard. | accepted |
| `IMPL-AGR-EXCH-READ-003` | details read | Use query handler + read repository / Dapper projection; do not introduce application service for read-only projection. | accepted |
| `IMPL-AGR-EXCH-READ-004` | client sidecars | Use shared entity wrappers under `entities/agreement-exchange/api`; do not add `shared/api/agreementExchangeApi.ts`. | accepted |
| `IMPL-AGR-EXCH-READ-005` | client sidecars | Page shells may differ by actor; query/model/widgets stay shared while response shape is common. | accepted |

## 4. Agreement Exchange Command Notes

| ID | Applies to | Note | Status |
|---|---|---|---|
| `IMPL-L2-AGR-CMD-001` | current-state answers | Inspect GitHub/current branch for actual implementation state. Uploaded drafts and archives are planning input only. | accepted |
| `IMPL-L2-AGR-CMD-002` | server/client full drafts | Keep Implementation Checklist near the end of every full server or client draft. | accepted |
| `IMPL-L2-AGR-CMD-003` | counter-proposal server | Controller may branch by role only to call service method; it must not contain lifecycle/turn/ownership logic. | accepted |
| `IMPL-L2-AGR-CMD-004` | counter-proposal domain | Domain validates client ownership, active proposal sender, turn and lifecycle invariants. | accepted |
| `IMPL-L2-AGR-CMD-005` | accept server | Use Client-only endpoint, no body, 204 success, no new proposal version. | accepted |
| `IMPL-L2-AGR-CMD-006` | client wrappers | Command wrappers live in `features/agreement-exchange/<action>/api`, not `shared/api`. | accepted |
| `IMPL-L2-AGR-CMD-007` | command results | Do not add per-command status enums. Use `UnitResult<IReadOnlyList<Error>>` / existing Error mapping. | accepted |

## 5. Agreement Exchange Start Notes

```text
SL-AGR-EXCH-001:
- starts from Employee request details after request is Approved;
- not an empty exchange start;
- creates AgreementProposalExchange and proposal version 1;
- stores ClientAccountId from approved request owner;
- does not change ApproveReview behavior;
- does not add ResponsibleEmployeeId guard;
- document reference is required;
- comment is optional;
- command result follows server/OpenAPI contract, currently server draft says 204 No Content.
```

Client sidecar:

```text
L2-AGR-EXCH-START-001.client:
- hosted by Employee request details action area;
- wrapper lives in features/agreement-exchange/start-exchange/api;
- navigate to Employee agreement exchange details if generated response contains exchangeId;
- fallback to refetch/stay if server returns 204.
```

## 6. Agreement Exchange Final Refusal Notes

```text
Use domain methods, not manual state assignment:
  exchange.FinalRefuseProposal(employee, reason, now)
  request.MarkAgreementExchangeFailed(exchange.Id, now)

Use one unit-of-work/transaction. If request marking fails, do not persist partial exchange refusal.

Do not add:
  ResponsibleEmployeeId guard
  per-command status enum
  Client final refusal UI/API
```
