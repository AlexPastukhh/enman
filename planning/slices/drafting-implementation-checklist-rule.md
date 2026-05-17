# Drafting Rule — Implementation Checklist

Status: current / server and client full drafts synchronized

## Rule

Every full server/backend/API draft and every full client sidecar draft must end with a concrete `Implementation Checklist` section near the end.

The checklist is part of the draft contract for implementation chats.

## Server command checklist expectations

Server command slice checklist should mention, when relevant:

```text
[ ] add request DTO
[ ] add DTO validator
[ ] add command returning UnitResult<IReadOnlyList<Error>> or current project Result model
[ ] do not add per-command status enum unless explicitly accepted
[ ] add handler/application service method
[ ] add repository/read repository changes if needed
[ ] add DbContext DbSet/mapping/migration if needed
[ ] enforce uniqueness/lifecycle guards
[ ] add/choose controller
[ ] require role authorization
[ ] require CSRF token for unsafe browser commands
[ ] create value objects
[ ] call domain method
[ ] persist aggregate(s)
[ ] return documented success status
[ ] map failures through existing Error/ProblemDetails mapping
[ ] add integration tests
[ ] regenerate OpenAPI/types when API shape changes
```

## Client sidecar checklist expectations

Client sidecar checklist should mention, when relevant:

```text
[ ] confirm backend endpoint and generated contract
[ ] add entity/feature API wrapper in the owning layer
[ ] add generated DTO aliases near the owning entity/feature
[ ] do not add business wrapper to shared/api
[ ] add React Query hook or mutation
[ ] add widget/feature UI component
[ ] add page shell wiring
[ ] render loading/error/empty/success/pending states
[ ] refresh/invalidate read queries after command success
[ ] add component/entity/feature tests
[ ] add E2E smoke when server/test setup exists
[ ] regenerate OpenAPI/types only when the implementation package changes API shape
```

## Specific accepted checklist for SL-AGR-EXCH-001

```text
[ ] add EmployeeStartAgreementExchangeDto
[ ] add EmployeeStartAgreementExchangeDtoValidator
[ ] add EmployeeStartAgreementExchangeCommand returning UnitResult<IReadOnlyList<Error>>
[ ] do not add per-command status enum
[ ] add EmployeeStartAgreementExchangeHandler
[ ] add agreement exchange repository abstraction
[ ] add agreement exchange EF repository
[ ] add L1DbContext DbSet/mapping if missing
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
