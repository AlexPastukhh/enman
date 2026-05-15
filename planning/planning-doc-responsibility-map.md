# Planning Document Responsibility Map

Status: current responsibility map

## Core Rule

A file should contain only content that belongs to its responsibility zone.

If a rule applies to all scenarios, all slices, all client sidecars, all API contracts, all agents, all archives, all ADRs, or the full planning workflow, it belongs in a central workflow/common file.

## API File Responsibility

| File | Responsibility |
|---|---|
| `planning/api/README.md` | API planning navigation and API contract split |
| `planning/api/api-error-contract.md` | Native ProblemDetails, ServerError shape, client-facing error policy, DTO field naming |
| `planning/api/api-error-mapping-boundary.md` | Target Domain/Application Error -> ServerError -> ProblemDetails mapping boundary |
| `planning/api/openapi-contract-generation.md` | OpenAPI structural contract and generated TypeScript DTO/types direction |
| `planning/api/client-constants-generation.md` | Generated shared constants JSON and explicit generator command |
| `planning/api/fluentvalidation-error-code-policy-note.md` | Deferred FluentValidation ErrorMessage/ErrorCode inspection note |

## Responsibility Decision Heuristic

```text
1. API contract / ProblemDetails / ServerError / OpenAPI / generated constants -> planning/api/
2. client-wide UI convention -> planning/client/cross-cutting/
3. accepted/current architecture decision -> architecture-decision-notes.md
4. possible future full ADR -> adr-candidates.md
5. one vertical slice -> parent slice file
6. detailed frontend implementation for one slice -> `.client.md`
7. future implementation thought not yet assigned -> slice-implementation-notes-register.md
```
