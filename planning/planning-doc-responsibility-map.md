# Planning Document Responsibility Map

Status: current responsibility map

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

## 2. Scenario / Security Specification Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and cross-scenario addenda |
| `scenario-browser-security-addendum.md` | Cross-cutting browser security requirements, including CSRF/antiforgery |
| `SC-15-security-text-specification.md` | General security text specification |
| `scenario-account-activation-security-addendum.md` | Account activation/protected scenario addendum |

## 3. Behavior Items Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived and concern-derived behavior items |
| `CC-CSRF-001-antiforgery-behavior-items.md` | Security-derived behavior items for CSRF cross-cutting slice |

## 4. Cross-Cutting / Helper Slice Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/cross-cutting/` | Cross-cutting/helper slices with observable support behavior, behavior items, concern flow, implementation flow, tests and multiple consumers |
| `planning/slices/cross-cutting/README.md` | Terms, index and format rules for cross-cutting/helper slices |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Constants generation/checking/testing implementation pseudo-slice |
| `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context implementation pseudo-slice |

## 5. Shared Notes vs Cross-Cutting Slices

```text
planning/slices/shared/
= reusable notes/helpers, not necessarily full slices.

planning/slices/cross-cutting/
= cross-cutting/helper slices with behavior items, concern flow, implementation flow and tests.
```

## 6. API Responsibility

```text
planning/api/
= API contract, ProblemDetails, ServerError, OpenAPI, API/constants/security-error relationship.
```

API docs do not own detailed cross-cutting CSRF implementation flow.

## 7. Testing Responsibility

```text
planning/testing/
= cross-slice testing principles, E2E workflow, test object patterns, Playwright cleanup plan.
```

## 8. Responsibility Decision Heuristic

```text
1. browser security requirement / CSRF requirement -> scenario-browser-security-addendum.md
2. CSRF behavior items -> CC-CSRF-001-antiforgery-behavior-items.md
3. CSRF implementation flow/tests -> CC-CSRF-001-antiforgery-token-session-context.md
4. API contract / ProblemDetails / ServerError / OpenAPI -> planning/api/
5. test layer boundaries / E2E workflow -> planning/testing/
6. client constants writer/checker/testing -> CC-CONST-001
7. client-wide UI convention -> planning/client/cross-cutting/
8. accepted/current architecture decision -> architecture-decision-notes.md
9. possible future full ADR -> adr-candidates.md
10. one vertical business slice -> parent slice file
11. detailed frontend implementation for one business slice -> `.client.md`
12. reusable note without full behavior/test flow -> planning/slices/shared/
```
