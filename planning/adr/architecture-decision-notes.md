# Architecture Decision Notes

Status: active accepted-decision registry  
Scope: current accepted architecture decisions that guide planning/implementation and may support diploma text

## Cross-Cutting / Constants Decisions

| ID | Decision | Current accepted direction | Rationale / trade-off | Affected artifacts | ADR promotion |
|---|---|---|---|---|---|
| ADN-058 | Constants generation/testing is a cross-cutting slice | `CC-CONST-001` is the primary implementation-ready pseudo-slice. | It has observable behavior, implementation flow, tests and multiple consumers. | `planning/slices/cross-cutting/CC-CONST-001...` | Candidate |
| ADN-059 | Constants testing strategy | Use sync check, integration tests against generated JSON, literal tests only for critical behavioral codes, generator tests for shape/duplicates/serialization/writer/checker. | Avoids self-equality tests while limiting noise. | Tools.Tests, API integration tests | Candidate |
| ADN-060 | No constants convention/golden tests initially | Do not add regex convention tests or full golden-file tests initially. | Keeps first implementation focused and avoids noisy tests. | Tools.Tests | Candidate |
| ADN-061 | Cross-cutting/helper slices are allowed | Technical/support work with behavior, implementation flow and tests can be documented as cross-cutting/helper slice. | Avoids burying implementation-ready support work in generic workflow notes. | slice workflow docs | Candidate |
| ADN-062 | Implementation flow detail filter | Include classes/methods/code snippets only for key decisions/boundaries/non-obvious behavior; keep routine code high-level. | Keeps slice files readable while still useful for diploma/implementation. | slice docs, client docs, cross-cutting slices | Candidate |

## Existing API/Contract Decisions Referenced By CC-CONST

| ID | Decision | Current accepted direction |
|---|---|---|
| ADN-045 | API contract split | OpenAPI = structural contract; generated JSON = semantic symbolic constants. |
| ADN-046 | Native ProblemDetails remains error envelope | Use native ProblemDetails + shared errors extension. |
| ADN-048 | Human-readable stable error codes | Error codes are stable symbolic identifiers, not UI messages. |
| ADN-052 | Client constants generation command | Generate `Shared/constants.json` / `Shared/errorcodes.json` by explicit command, not hosted service. |
| ADN-055 | FluentValidation ErrorCode migration deferred | Inspect validators/helpers/tests before migration from ErrorMessage-as-code. |
