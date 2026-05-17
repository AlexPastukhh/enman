# Drafting Implementation Checklist Rule

Status: current / applies to server and client full slice drafts

## Rule

Every full server/backend/API draft and every full client sidecar draft must include an `Implementation Checklist` section near the end.

The checklist must be concrete and slice-specific.

It must not be a generic placeholder.

## Placement

Use near-final placement:

```text
## 18. Dependent / Follow-up Slices
## 19. Implementation Checklist
## 20. Guardrail Summary
```

Exact numbering can change if the draft has more sections, but `Implementation Checklist` must stay near the end and before final guardrails/next step.

## Server Checklist Content

Server command/read checklists should include concrete items for:

```text
- DTO/query/route contract;
- validator when applicable;
- command/query object;
- application handler/service;
- repository/persistence changes;
- controller endpoint;
- auth/role boundary;
- CSRF for unsafe commands;
- domain value object creation;
- domain method call;
- persistence/transaction;
- response contract;
- ProblemDetails/Error mapping;
- domain tests if missing;
- integration tests;
- OpenAPI/types regeneration when API shape changes.
```

## Client Checklist Content

Client sidecar checklists should include concrete items for:

```text
- generated contract confirmation;
- entity/feature API wrapper placement;
- generated DTO aliases;
- feature model/mutation/query hook;
- UI component/form/action;
- page/slot wiring;
- auth/session assumptions;
- CSRF-aware shared transport usage for unsafe commands;
- query invalidation/refetch;
- pending/error/success feedback;
- component/model/E2E tests when appropriate;
- no business wrapper in shared/api.
```

## SL-AGR-EXCH-001 Required Checklist

For `SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal`, use this checklist and keep it updated if the slice changes:

```text
[ ] add EmployeeStartAgreementExchangeDto
[ ] add EmployeeStartAgreementExchangeDtoValidator
[ ] add EmployeeStartAgreementExchangeCommand returning UnitResult<IReadOnlyList<Error>>
[ ] do not add per-command status enum
[ ] add EmployeeStartAgreementExchangeHandler
[ ] add agreement exchange repository abstraction
[ ] add agreement exchange EF repository
[ ] add L1DbContext DbSet/mapping if missing
[ ] add AgreementProposalExchange.ClientAccountId
[ ] populate ClientAccountId from approved request owner in StartByEmployee
[ ] add ClientAccountId EF mapping/index
[ ] enforce unique exchange per request
[ ] add separate EmployeeAgreementExchangeController
[ ] require Employee role
[ ] require CSRF token
[ ] validate documentRef/comment
[ ] create AgreementDocumentRef
[ ] create optional ProposalComment
[ ] call AgreementProposalExchange.StartByEmployee
[ ] persist exchange aggregate
[ ] return 204 No Content on success
[ ] map failures through existing Error/ProblemDetails mapping
[ ] add domain tests if missing
[ ] add integration tests for auth/CSRF/validation/lifecycle/success/duplicate
[ ] regenerate OpenAPI/types
```
