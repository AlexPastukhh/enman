# Planning Document Responsibility Map

Status: current responsibility map

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

## 2. Cross-Cutting / Helper Slice Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/cross-cutting/` | Cross-cutting/helper slices with observable support behavior, implementation flow, tests and multiple consumers |
| `planning/slices/cross-cutting/README.md` | Terms and index for cross-cutting/helper slices |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Constants generation/checking/testing implementation pseudo-slice |

## 3. Shared Notes vs Cross-Cutting Slices

```text
planning/slices/shared/
= reusable notes/helpers, not necessarily full slices.

planning/slices/cross-cutting/
= cross-cutting/helper slices with behavior, implementation flow and tests.
```

## 4. API Responsibility

```text
planning/api/
= API contract, ProblemDetails, ServerError, OpenAPI, API/constants relationship.
```

API docs do not own the detailed constants writer/checker implementation flow.

## 5. Responsibility Decision Heuristic

```text
1. client constants writer/checker/testing implementation -> planning/slices/cross-cutting/CC-CONST-001...
2. API contract / ProblemDetails / ServerError / OpenAPI -> planning/api/
3. client-wide UI convention -> planning/client/cross-cutting/
4. accepted/current architecture decision -> architecture-decision-notes.md
5. possible future full ADR -> adr-candidates.md
6. one vertical business slice -> parent slice file
7. detailed frontend implementation for one business slice -> `.client.md`
8. reusable note without full behavior/test flow -> planning/slices/shared/
```
