# Planning Document Responsibility Map

Status: current responsibility map / server validation responsibility added

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

When a local file contains information that affects future work, synchronize it with the correct shared index/register.

## 2. Scenario Source Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/scenario-specification-principles.md` | Scenario specification principles and source-of-truth rules for scenario text/DATA/UI/behavior/question artifacts |
| `planning/scenario-domain-validation-principles.md` | Client-side vs server/domain validation distinction and domain-design input rules |
| `planning/diagrams/scenario-drafting-workflow.md` | Practical scenario draft workflow; how to maintain text specs, DATA, UI specs, behavior items, questions and diagram requests/prompts together |
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and cross-scenario addenda |
| `planning/diagrams/scenario-data/` | Scenario DATA files: entered/seen/selected/filtered/attached/referenced data only |
| `planning/diagrams/scenario-ui-specs/` | `[UI-SCENARIO]` UI-visible requirements and accepted UI decisions, not React implementation |
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived, UI-scenario-derived and concern-derived behavior items |
| `planning/diagrams/scenario-questions-register.md` | Scenario/domain questions that can change scenario behavior, DATA, UI requirements or diagrams |
| `planning/diagrams/scenario-clarifications/` | Accepted scenario clarifications and diagram guardrails when sources conflict |

## 3. Slice Discovery Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/slices/README.md` | Slice-planning navigation, current backend/client slice files, source register, examples and shared registers |
| `planning/slices/draft-driven-discovery-principles.md` | Draft-driven discovery for domain/business/client/cross-cutting/testing/documentation drafts |
| `planning/slices/l1-slice-drafting-guide.md` | Practical L1 slice drafting workflow, shortened/full draft templates, source intake, server validation intake and visual flow rules |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Maps slices/client sidecars to scenario text, DATA, UI scenario and behavior item source files |
| `planning/slices/slice-questions-register.md` | Shared overview of currently relevant local questions, including questions from implemented slices |
| `planning/slices/slice-extension-points-register.md` | Cross-slice extension points, change pressure, anti-coupling decisions and extension/change-related questions |
| `planning/slices/slice-implementation-notes-register.md` | Concrete future implementation/client/testing notes not yet assigned to an active slice/client sidecar |
| `planning/slices/SL-*.md` | Active parent backend/business slice docs |
| `planning/slices/*.client.md` | Client sidecar docs for concrete client work or implemented client logic reconciliation |
| `planning/slices/examples/` | Example-only slice drafts used to demonstrate valid shortened/full slice formats |
| `planning/slices/cross-cutting/` | Cross-cutting/helper slice docs for reusable API/tooling/security/validation/client-support concerns |
| `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | Server request DTO/query validation principles and consumer rules for FluentValidation-based API boundary validation |

Scenario Flow and Behavior Items for slices come from `slice-scenario-flow-behavior-register.md` and the source files it points to.

Questions/extension/implementation registers do not replace scenario source files.

## 4. Client Planning Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/client/` | Client-wide planning index and client convention navigation |
| `planning/client/cross-cutting/` | Client-wide reusable UI/client implementation conventions |
| `planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md` | Command success convention for flows where HTTP success is enough and no response body is required by default |
| `planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md` | Client-wide feedback/message convention for success/info/warning/error feedback surfaces |
| `planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md` | Deferred/client-side validation convention |
| `planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md` | Client-side API error handling convention |
| `planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md` | Styling convention |
| `planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md` | Accessibility convention |

## 5. Documentation Update Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/documentation/` | Documentation-only workflow, status reconciliation, local/global sync, documentation agent prompt |
| `planning/replacement-file-generation-guide.md` | Archive/package generation rules for manual repo application |

## 6. API / Testing / ADR Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/api/` | API contract principles, OpenAPI structural contract, API error contract, constants relationship and FluentValidation error-code policy |
| `planning/testing/` | Cross-slice testing principles, E2E workflow, test object patterns, Playwright cleanup plan |
| `planning/adr/` | ADR workflow, architecture decision notes and ADR candidates |

API error contract docs own external error shape and error-code semantics.

`CC-VALIDATION-001` owns the practical slice-consumer rule for where FluentValidation belongs in server implementation flow.

## 7. Responsibility Decision Heuristic

```text
1. scenario text/DATA/UI/behavior source -> planning/diagrams/scenario-*/
2. slice-to-source mapping -> planning/slices/slice-scenario-flow-behavior-register.md
3. active parent backend/business slice file -> planning/slices/SL-*.md
4. client sidecar file -> planning/slices/*.client.md
5. slice-wide question overview -> planning/slices/slice-questions-register.md
6. extension/change pressure -> planning/slices/slice-extension-points-register.md
7. future implementation/client/testing note -> planning/slices/slice-implementation-notes-register.md
8. server request validation concern -> planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
9. client-wide UI/client convention -> planning/client/cross-cutting/
10. client/server contract split -> planning/api/client-server-contract-principles.md
11. API error envelope / error-code semantics -> planning/api/api-error-contract.md and related API notes
12. test layer boundaries / E2E workflow -> planning/testing/
13. accepted/current architecture decision -> architecture-decision-notes.md
14. possible future full ADR -> adr-candidates.md
```
