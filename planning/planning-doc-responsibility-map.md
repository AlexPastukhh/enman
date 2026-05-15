# Planning Document Responsibility Map

Status: current responsibility map

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

## 2. Documentation Update Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/documentation/` | Documentation-only workflow, status reconciliation, documentation agent prompt |
| `planning/documentation/README.md` | Documentation workflow index |
| `planning/documentation/documentation-update-workflow.md` | How documentation-only updates are planned and produced |
| `planning/documentation/status-reconciliation-workflow.md` | How to align docs with current implementation status |
| `planning/documentation/documentation-update-agent-prompt.md` | Reusable prompt for a documentation-only chat |
| `planning/replacement-file-generation-guide.md` | Archive/package generation rules for manual repo application |

## 3. Slice Discovery Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/README.md` | Slice-planning navigation, current backend slice files, examples and support docs |
| `planning/slices/draft-driven-discovery-principles.md` | Draft-driven discovery for domain/business/client/cross-cutting/testing/documentation drafts |
| `planning/slices/l1-slice-drafting-guide.md` | Practical L1 slice drafting workflow, shortened/full draft templates and visual flow rules |
| `planning/slices/SL-*.md` | Active parent backend/business slice docs; own vertical behavior, visual scenario/implementation maps, detailed flows, API contract, coverage and tests |
| `planning/slices/examples/` | Example-only slice drafts used to demonstrate valid shortened/full slice formats |
| `planning/slices/examples/README.md` | Slice examples index and example usage rules |
| `planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md` | Valid shortened slice draft example |
| `planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md` | Valid full backend slice example with visual maps before detailed flows |
| `planning/slices/client-architecture-principles.md` | Client sidecar architecture mapping and frontend layer decisions |

## 4. Client Planning Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/client/` | Client-wide planning index and client convention navigation |
| `planning/client/cross-cutting/` | Client-wide reusable UI/client implementation conventions |
| `planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md` | Command success convention for flows where HTTP success is enough and no response body is required by default |
| `planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md` | Deferred/client-side validation convention |
| `planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md` | Client-side API error handling convention |
| `planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md` | Styling convention |
| `planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md` | Accessibility convention |

## 5. API Contract Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/api/` | API contract principles, OpenAPI structural contract, API error contract, constants relationship |
| `planning/api/client-server-contract-principles.md` | OpenAPI vs generated constants split and generated artifact rules |
| `planning/api/api-error-contract.md` | ProblemDetails, ServerError, FieldName/ErrorCode policy, security errors |
| `planning/api/api-error-mapping-boundary.md` | Domain/Application Error -> ServerError -> ProblemDetails target mapping |
| `planning/api/openapi-contract-generation.md` | OpenAPI generation and TypeScript types direction |
| `planning/api/client-constants-generation.md` | API relationship to generated semantic constants |
| `planning/api/fluentvalidation-error-code-policy-note.md` | Deferred FluentValidation ErrorMessage/ErrorCode migration note |

## 6. Cross-Cutting / Helper Slice Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/cross-cutting/` | Cross-cutting/helper slices with observable support behavior, behavior items, concern flow, implementation flow, tests/checks and multiple consumers |
| `planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md` | OpenAPI artifact/type generation cross-cutting slice |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Constants generation/checking/testing cross-cutting slice |
| `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context cross-cutting slice |

## 7. Scenario / Security Specification Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/` | Scenario/specification source index and diagram-generation workflow docs |
| `planning/diagrams/README.md` | Scenario/diagram planning index, source read order and diagram workflow navigation |
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and cross-scenario addenda |
| `scenario-browser-security-addendum.md` | Cross-cutting browser security requirements, including CSRF/antiforgery |
| `planning/diagrams/scenario-clarifications/` | Temporary scenario clarifications and diagram guardrails |

## 8. Diagram Generation Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/diagram-prompt-generation-workflow.md` | How to prepare a repo-grounded prompt for a separate diagram-generation chat; includes preflight, batching, source checks, conflict checks and status markers |
| `planning/diagrams/drawio-diagram-generation-workflow.md` | Draw.io XML target format, multi-page diagram book structure, page naming, VKR-clean text rules and diagram archive rules |
| `planning/diagrams/vkr-clean-drafts/` | Optional fallback location for draft `.drawio` diagram books and companion diagram planning files before `vkr-clean/` is active |

## 9. Behavior Items Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived and concern-derived behavior items |
| `CC-CSRF-001-antiforgery-behavior-items.md` | Security-derived behavior items for CSRF cross-cutting slice |

## 10. Testing Responsibility

```text
planning/testing/
= cross-slice testing principles, E2E workflow, test object patterns, Playwright cleanup plan.
```

## 11. Responsibility Decision Heuristic

```text
1. documentation-only workflow/status reconciliation -> planning/documentation/
2. archive creation rules -> planning/replacement-file-generation-guide.md
3. draft-driven discovery across slice families -> planning/slices/draft-driven-discovery-principles.md
4. practical slice drafting workflow and templates -> planning/slices/l1-slice-drafting-guide.md
5. active parent backend/business slice file -> planning/slices/SL-*.md
6. slice examples -> planning/slices/examples/
7. client-wide UI/client convention -> planning/client/cross-cutting/
8. client/server contract split -> planning/api/client-server-contract-principles.md
9. OpenAPI artifact/type generation implementation flow -> CC-API-001
10. generated semantic constants writer/checker/testing -> CC-CONST-001
11. API error contract / ProblemDetails / ServerError / OpenAPI -> planning/api/
12. browser security requirement / CSRF requirement -> scenario-browser-security-addendum.md
13. CSRF behavior items -> CC-CSRF-001-antiforgery-behavior-items.md
14. CSRF implementation flow/tests -> CC-CSRF-001-antiforgery-token-session-context.md
15. diagram prompt-generation workflow -> planning/diagrams/diagram-prompt-generation-workflow.md
16. draw.io XML / diagram book artifact workflow -> planning/diagrams/drawio-diagram-generation-workflow.md
17. diagram scenario terminology conflicts -> planning/diagrams/scenario-clarifications/
18. test layer boundaries / E2E workflow -> planning/testing/
19. accepted/current architecture decision -> architecture-decision-notes.md
20. possible future full ADR -> adr-candidates.md
21. one vertical business slice -> parent slice file
22. detailed frontend implementation for one business slice -> `.client.md`
23. reusable note without full behavior/test flow -> planning/slices/shared/
```
