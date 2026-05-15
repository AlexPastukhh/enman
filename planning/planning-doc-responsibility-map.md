# Planning Document Responsibility Map

Status: current responsibility map

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

## 2. API Contract Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/api/` | API contract principles, OpenAPI structural contract, API error contract, constants relationship |
| `planning/api/client-server-contract-principles.md` | OpenAPI vs generated constants split and generated artifact rules |
| `planning/api/api-error-contract.md` | ProblemDetails, ServerError, FieldName/ErrorCode policy, security errors |
| `planning/api/api-error-mapping-boundary.md` | Domain/Application Error -> ServerError -> ProblemDetails target mapping |
| `planning/api/openapi-contract-generation.md` | OpenAPI generation and TypeScript types direction |
| `planning/api/client-constants-generation.md` | API relationship to generated semantic constants |
| `planning/api/fluentvalidation-error-code-policy-note.md` | Deferred FluentValidation ErrorMessage/ErrorCode migration note |

## 3. Cross-Cutting / Helper Slice Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/cross-cutting/` | Cross-cutting/helper slices with observable support behavior, behavior items, concern flow, implementation flow, tests/checks and multiple consumers |
| `planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md` | OpenAPI artifact/type generation cross-cutting slice |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Constants generation/checking/testing cross-cutting slice |
| `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context cross-cutting slice |

## 4. Scenario / Security Specification Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and cross-scenario addenda |
| `scenario-browser-security-addendum.md` | Cross-cutting browser security requirements, including CSRF/antiforgery |

## 5. Behavior Items Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived and concern-derived behavior items |
| `CC-CSRF-001-antiforgery-behavior-items.md` | Security-derived behavior items for CSRF cross-cutting slice |

## 6. Testing Responsibility

```text
planning/testing/
= cross-slice testing principles, E2E workflow, test object patterns, Playwright cleanup plan.
```

## 7. Responsibility Decision Heuristic

```text
1. client/server contract split -> planning/api/client-server-contract-principles.md
2. OpenAPI artifact/type generation implementation flow -> CC-API-001
3. generated semantic constants writer/checker/testing -> CC-CONST-001
4. API error contract / ProblemDetails / ServerError / OpenAPI -> planning/api/
5. browser security requirement / CSRF requirement -> scenario-browser-security-addendum.md
6. CSRF behavior items -> CC-CSRF-001-antiforgery-behavior-items.md
7. CSRF implementation flow/tests -> CC-CSRF-001-antiforgery-token-session-context.md
8. test layer boundaries / E2E workflow -> planning/testing/
9. client-wide UI convention -> planning/client/cross-cutting/
10. accepted/current architecture decision -> architecture-decision-notes.md
11. possible future full ADR -> adr-candidates.md
12. one vertical business slice -> parent slice file
13. detailed frontend implementation for one business slice -> `.client.md`
14. reusable note without full behavior/test flow -> planning/slices/shared/
```
