# Planning Document Responsibility Map

Status: current responsibility map

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

## 2. Testing Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/testing/` | Cross-slice testing principles, E2E workflow, test object patterns, Playwright cleanup plan |
| `planning/testing/README.md` | Testing docs index and current testing direction |
| `planning/testing/testing-principles.md` | Test layer boundaries: domain, server/API, client/component, E2E |
| `planning/testing/e2e-playwright-workflow.md` | E2E purpose, Playwright webServer, Vite proxy/CORS, locator policy, test data, E2E coverage |
| `planning/testing/test-object-patterns.md` | Page Object vs Component Object responsibilities and data/locator ownership |
| `planning/testing/playwright-e2e-cleanup-plan.md` | Implementation-ready cleanup plan for current Playwright setup |

## 3. Cross-Cutting / Helper Slice Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/cross-cutting/` | Cross-cutting/helper slices with observable support behavior, implementation flow, tests and multiple consumers |
| `planning/slices/cross-cutting/README.md` | Terms and index for cross-cutting/helper slices |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Constants generation/checking/testing implementation pseudo-slice |

## 4. Shared Notes vs Cross-Cutting Slices

```text
planning/slices/shared/
= reusable notes/helpers, not necessarily full slices.

planning/slices/cross-cutting/
= cross-cutting/helper slices with behavior, implementation flow and tests.
```

## 5. API Responsibility

```text
planning/api/
= API contract, ProblemDetails, ServerError, OpenAPI, API/constants relationship.
```

API docs do not own detailed Playwright or testing workflow.

## 6. Client Responsibility

```text
planning/client/
= client-wide conventions such as accessibility, styling, deferred validation and client error handling.
```

Client docs do not own E2E infrastructure.

## 7. Responsibility Decision Heuristic

```text
1. test layer boundaries / E2E workflow / Playwright setup / Page Object rules -> planning/testing/
2. client constants writer/checker/testing implementation -> planning/slices/cross-cutting/CC-CONST-001...
3. API contract / ProblemDetails / ServerError / OpenAPI -> planning/api/
4. client-wide UI convention -> planning/client/cross-cutting/
5. accepted/current architecture decision -> architecture-decision-notes.md
6. possible future full ADR -> adr-candidates.md
7. one vertical business slice -> parent slice file
8. detailed frontend implementation for one business slice -> `.client.md`
9. reusable note without full behavior/test flow -> planning/slices/shared/
```
