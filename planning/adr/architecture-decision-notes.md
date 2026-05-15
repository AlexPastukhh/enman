# Architecture Decision Notes

Status: active accepted-decision registry  
Scope: current accepted architecture decisions that guide planning/implementation and may support diploma text

## Purpose

This file is the current accepted architecture decision registry.

A decision note means:

```text
This direction is accepted/current enough to guide planning and implementation,
but it is not yet a full numbered ADR.
```

## Current Accepted Decisions — API / Client Contract Additions

| ID | Decision | Current accepted direction | Rationale / trade-off | Affected artifacts | ADR promotion |
|---|---|---|---|---|---|
| ADN-045 | API contract split | OpenAPI = structural contract; generated JSON = semantic symbolic constants. | Prevents DTO drift and preserves stable error codes. | planning/api, slices, client | Likely ADR |
| ADN-046 | Native ProblemDetails remains error envelope | Use native ProblemDetails + shared errors extension. | Keeps standard ASP.NET contract while allowing extensions. | API error contract | Candidate |
| ADN-047 | ServerError is API-facing DTO | `ServerError` / `ServerValidationError` is contract when returned through ProblemDetails. | Integration tests/client parser rely on it. | API error contract | Candidate |
| ADN-048 | Human-readable stable error codes | Error codes are stable symbolic identifiers, not UI messages. | Readable, testable, localizable mapping. | errorcodes JSON/client | Candidate |
| ADN-049 | Server validation errors use DTO field names | Server returns API DTO field names; client maps to form fields. | Server must not know React form names. | API/client sidecars | Candidate |
| ADN-050 | Client-facing vs internal errors | Only intentionally returned errors are part of client contract. | Avoids leaking server internals. | API error contract | Candidate |
| ADN-051 | API error mapping boundary target | Move Domain/Application Error -> ServerError -> ProblemDetails into API boundary services. | Prevents controllers deciding error contract. | API mapping docs | Candidate |
| ADN-052 | Client constants generation command | Generate `Shared/constants.json` / `Shared/errorcodes.json` by explicit command, not hosted service. | Server startup should not mutate repo files. | constants generation | Candidate |
| ADN-053 | Source constants target | Prefer separate API contracts/constants project later. | Generator should not require running web app. | constants generation | Candidate |
| ADN-054 | OpenAPI generation types-only first | Generate TypeScript DTO/types first; keep thin handwritten wrappers. | Fits entities/features architecture. | OpenAPI/client | Candidate |
| ADN-055 | FluentValidation ErrorCode migration deferred | Inspect validators/helpers/tests before migration from ErrorMessage-as-code. | Avoids breaking current validation contract. | validation/API | Candidate |
| ADN-056 | .NET upgrade deferred | Do not mix runtime upgrade with API/client contract work. | Keeps current work focused. | infra | Candidate |
| ADN-057 | Accessibility as component/test contract | Native semantic HTML first; ARIA only when needed; role/name/label-first tests. | Improves usability and test stability. | a11y docs, tests | Candidate |

## Open Questions

| ID | Question | Current assumption / status | Where to resolve |
|---|---|---|---|
| ADQ-007 | FluentValidation helper currently uses ErrorMessage or ErrorCode for stable code? | Open; inspect before change. | API validation hardening |
| ADQ-008 | Exact tooling for OpenAPI TypeScript generation? | Open; types-only direction accepted. | OpenAPI generation step |
| ADQ-009 | Exact tool/project layout for constants generator? | Open; explicit command direction accepted. | constants generation step |
