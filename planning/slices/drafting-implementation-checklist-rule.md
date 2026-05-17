# Drafting Rule — Implementation Checklist Section

Status: active drafting rule  
Applies to: full server slice drafts and full client sidecar drafts

## Rule

Every full implementation draft must include an explicit implementation checklist near the end.

Preferred placement:

```text
## N. Implementation Checklist
```

near the end, before `Guardrail Summary`, `Next Step` or appendix sections.

## Required qualities

The checklist must be:

```text
- slice-specific, not generic filler;
- concrete enough for an implementation chat to follow;
- aligned with existing project examples and conventions;
- explicit about generated artifacts when API shape changes;
- explicit about tests at the correct layer;
- explicit about out-of-scope work to avoid accidental scope creep.
```

For state-changing server command slices, the checklist must include the API boundary, auth/CSRF, command/application shape, domain call, persistence, error mapping, tests and OpenAPI/type regeneration when relevant.

For client command sidecars, the checklist must include feature API wrapper placement, generated type aliases, mutation, UI wiring, query invalidation/refetch, error feedback, tests and the rule to avoid business wrappers in `shared/api`.

## Server command result convention

For new command slices that use the project `Result` / `UnitResult` + `Error` model:

```text
Do not add per-command status enum by default.
Use UnitResult<IReadOnlyList<Error>> or the current project result type.
Map failures through existing Error/ProblemDetails mapping.
```

Do not create a status enum just to drive HTTP status mapping unless a current project example explicitly requires it.

## Example checklist shape for server command slices

```text
[ ] add request DTO
[ ] add DTO validator
[ ] add command returning UnitResult<IReadOnlyList<Error>>
[ ] do not add per-command status enum
[ ] add command handler
[ ] add repository abstraction if the aggregate needs one
[ ] add EF repository if persistence is needed
[ ] add DbSet/mapping if missing
[ ] enforce uniqueness/lifecycle guard if required
[ ] add dedicated controller when the area will grow
[ ] require correct role
[ ] require CSRF token for unsafe request
[ ] validate DTO/value objects
[ ] call the domain method
[ ] persist aggregate changes
[ ] return 204 No Content on success when read state is obtained through refetch
[ ] map failures through existing Error/ProblemDetails mapping
[ ] add domain tests if missing
[ ] add integration tests for auth/CSRF/validation/lifecycle/success
[ ] regenerate OpenAPI/types if API contract changes
```

## Example checklist for `SL-AGR-EXCH-001`

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
