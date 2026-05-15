# Planning Document Responsibility Map

Status: current responsibility map

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

When a local file contains information that affects future work, synchronize it with the correct shared index/register.

## 2. Agent Role Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/agent-roles-and-required-actions.md` | Cross-project role map: documentation keeper, scenario draft, domain draft, slice draft, diagram prompt, diagram generation, implementation handoff and API/testing gates |
| `planning/planning-agent-protocol.md` | Shared collaboration protocol, role boundary rules, question/assumption rules and cross-cutting planning rules |
| `planning/planning-workflow-current.md` | Current planning baseline and active workflow gates |

## 3. Documentation Update Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/documentation/` | Documentation-only workflow, status reconciliation, local/global sync, documentation agent prompt |
| `planning/documentation/README.md` | Documentation workflow index |
| `planning/documentation/documentation-update-workflow.md` | How documentation-only updates are planned and produced |
| `planning/documentation/status-reconciliation-workflow.md` | How to align docs with current implementation status |
| `planning/documentation/local-global-documentation-sync-workflow.md` | How to keep local docs synchronized with global indexes/registers |
| `planning/documentation/documentation-update-agent-prompt.md` | Reusable prompt for a documentation-only chat |
| `planning/replacement-file-generation-guide.md` | Archive/package generation rules for manual repo application |

## 4. Scenario Drafting Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/scenario-specification-principles.md` | Scenario specification principles and source-of-truth rules for scenario text/DATA/UI/behavior/question artifacts |
| `planning/scenario-domain-validation-principles.md` | Client-side vs server/domain validation distinction and domain-design input rules |
| `planning/diagrams/scenario-drafting-workflow.md` | Practical scenario draft workflow; how to maintain text specs, DATA, UI specs, behavior items, questions and diagram requests/prompts together |
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and cross-scenario addenda |
| `planning/diagrams/scenario-data/` | Scenario DATA files: entered/seen/selected/filtered/attached/referenced data only |
| `planning/diagrams/scenario-ui-specs/` | UI-visible requirements and accepted UI decisions, not React implementation |
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived and concern-derived behavior items |
| `planning/diagrams/scenario-questions-register.md` | Scenario/domain questions that can change scenario behavior, DATA, UI requirements or diagrams |
| `planning/diagrams/scenario-clarifications/` | Accepted scenario clarifications and diagram guardrails when sources conflict |

Scenario Draft Chat may prepare a diagram request/prompt using `diagram-prompt-generation-workflow.md`, but it does not draw diagrams.

## 5. Diagram Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/README.md` | Scenario/diagram planning index, source read order and diagram workflow navigation |
| `planning/diagrams/diagram-prompt-generation-workflow.md` | How a scenario/documentation/planning chat prepares a repo-grounded diagram request/prompt for the single Diagram Chat; also defines Diagram Chat preflight phases |
| `planning/diagrams/drawio-diagram-generation-workflow.md` | Draw.io XML target format, multi-page diagram book structure, page naming, VKR-clean text rules and diagram archive rules |
| `planning/diagrams/vkr-clean-drafts/` | Optional fallback location for draft `.drawio` diagram books and companion diagram planning files before `vkr-clean/` is active |

## 6. Slice Discovery Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/README.md` | Slice-planning navigation, current backend slice files, examples, registers and support docs |
| `planning/slices/draft-driven-discovery-principles.md` | Draft-driven discovery for domain/business/client/cross-cutting/testing/documentation drafts |
| `planning/slices/l1-slice-drafting-guide.md` | Practical L1 slice drafting workflow, shortened/full draft templates and visual flow rules |
| `planning/slices/SL-*.md` | Active parent backend/business slice docs; own vertical behavior, visual scenario/implementation maps, detailed flows, API contract, coverage, local questions and tests |
| `planning/slices/examples/` | Example-only slice drafts used to demonstrate valid shortened/full slice formats |
| `planning/slices/examples/README.md` | Slice examples index and example usage rules |
| `planning/slices/examples/L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md` | Valid shortened slice draft example |
| `planning/slices/examples/SL-ACC-001-register-client-account-full-slice-example.md` | Valid full backend slice example with visual maps before detailed flows |
| `planning/slices/client-architecture-principles.md` | Client sidecar architecture mapping and frontend layer decisions |

## 7. Slice Registers Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/slice-questions-register.md` | Shared overview of currently relevant local slice/client/cross-cutting questions; mirrors important local questions so future work can find them |
| `planning/slices/slice-extension-points-register.md` | Cross-slice extension points, change pressure, anti-coupling decisions and extension-related questions |
| `planning/slices/slice-implementation-notes-register.md` | Concrete future implementation/client/testing notes not yet assigned to an active slice/client sidecar |

The question register does not replace local `Questions / Decisions` sections.

The extension register does not replace the question register.

The implementation notes register does not replace either; it stores future implementation notes.

## 8. Domain Draft Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/domain-model.md` | Background / implementation compatibility note; not always current target direction |
| `planning/tables/domain-drafts/` | Target domain draft files derived from scenarios/DATA/validation sources |
| `planning/l1-domain-implementation-cut.md` | Narrow first implementation cut after broad domain draft |
| `planning/l1-domain-testing-rules.md` | Domain testing rules for the implementation cut |
| `planning/tables/pre-domain-variants-input.md` | Pre-domain scenario/behavior/domain-input notes used before final domain draft |

If a domain question changes scenario meaning, sync with scenario questions/clarifications.

If a domain question affects downstream slices, sync with slice questions register.

## 9. Client Planning Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/client/` | Client-wide planning index and client convention navigation |
| `planning/client/cross-cutting/` | Client-wide reusable UI/client implementation conventions |
| `planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md` | Command success convention for flows where HTTP success is enough and no response body is required by default |
| `planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md` | Deferred/client-side validation convention |
| `planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md` | Client-side API error handling convention |
| `planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md` | Styling convention |
| `planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md` | Accessibility convention |

## 10. API Contract Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/api/` | API contract principles, OpenAPI structural contract, API error contract, constants relationship |
| `planning/api/client-server-contract-principles.md` | OpenAPI vs generated constants split and generated artifact rules |
| `planning/api/api-error-contract.md` | ProblemDetails, ServerError, FieldName/ErrorCode policy, security errors |
| `planning/api/api-error-mapping-boundary.md` | Domain/Application Error -> ServerError -> ProblemDetails target mapping |
| `planning/api/openapi-contract-generation.md` | OpenAPI generation and TypeScript types direction |
| `planning/api/client-constants-generation.md` | API relationship to generated semantic constants |
| `planning/api/fluentvalidation-error-code-policy-note.md` | Deferred FluentValidation ErrorMessage/ErrorCode migration note |

## 11. Cross-Cutting / Helper Slice Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/cross-cutting/` | Cross-cutting/helper slices with observable support behavior, behavior items, concern flow, implementation flow, tests/checks and multiple consumers |
| `planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md` | OpenAPI artifact/type generation cross-cutting slice |
| `planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md` | Constants generation/checking/testing cross-cutting slice |
| `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context cross-cutting slice |

## 12. Testing Responsibility

```text
planning/testing/
= cross-slice testing principles, E2E workflow, test object patterns, Playwright cleanup plan.
```

## 13. Responsibility Decision Heuristic

```text
1. agent role / required actions -> planning/agent-roles-and-required-actions.md
2. documentation-only workflow/status reconciliation -> planning/documentation/
3. local/global sync workflow -> planning/documentation/local-global-documentation-sync-workflow.md
4. archive creation rules -> planning/replacement-file-generation-guide.md
5. scenario drafting workflow -> planning/diagrams/scenario-drafting-workflow.md
6. scenario text/DATA/UI/behavior/question artifacts -> planning/diagrams/scenario-*/
7. diagram request/prompt and preflight workflow -> planning/diagrams/diagram-prompt-generation-workflow.md
8. draw.io XML / diagram book artifact workflow -> planning/diagrams/drawio-diagram-generation-workflow.md
9. draft-driven discovery across slice families -> planning/slices/draft-driven-discovery-principles.md
10. practical slice drafting workflow and templates -> planning/slices/l1-slice-drafting-guide.md
11. active parent backend/business slice file -> planning/slices/SL-*.md
12. slice-wide question overview -> planning/slices/slice-questions-register.md
13. extension/change pressure -> planning/slices/slice-extension-points-register.md
14. future implementation/client/testing note -> planning/slices/slice-implementation-notes-register.md
15. domain draft/implementation cut -> domain draft files + l1-domain-implementation-cut.md
16. client-wide UI/client convention -> planning/client/cross-cutting/
17. client/server contract split -> planning/api/client-server-contract-principles.md
18. OpenAPI artifact/type generation implementation flow -> CC-API-001
19. generated semantic constants writer/checker/testing -> CC-CONST-001
20. API error contract / ProblemDetails / ServerError / OpenAPI -> planning/api/
21. browser security requirement / CSRF requirement -> scenario-browser-security-addendum.md
22. CSRF behavior items -> CC-CSRF-001-antiforgery-behavior-items.md
23. CSRF implementation flow/tests -> CC-CSRF-001-antiforgery-token-session-context.md
24. test layer boundaries / E2E workflow -> planning/testing/
25. accepted/current architecture decision -> architecture-decision-notes.md
26. possible future full ADR -> adr-candidates.md
27. detailed frontend implementation for one business slice -> `.client.md`
28. reusable note without full behavior/test flow -> planning/slices/shared/
```
