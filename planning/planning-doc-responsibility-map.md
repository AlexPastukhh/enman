# Planning Document Responsibility Map

Status: transitional global responsibility map / root layer router  
Scope: routes planning documentation information to the correct layer and, where available, to the layer-local responsibility map

## 1. Core Rule

A file should contain only content that belongs to its responsibility zone.

When a local file contains information that affects future work, synchronize it with the correct shared index/register.

This root map is the first routing step for planning documentation:

```text
new information
  -> choose planning layer here
  -> open the layer-local responsibility map or README
  -> update the correct local file/register/index
```

This file should not become the permanent detailed owner for every file inside every layer.

Target model:

```text
root responsibility map
  = chooses the layer and points to local responsibility maps.

local layer responsibility map
  = decides where information belongs inside that layer.

folder README / index
  = navigation and read order.
```

Until local responsibility maps exist for all layers, this file keeps transitional fallback routing for scenario, API/testing, architecture and VKR responsibilities. Domain-specific placement now starts from `planning/domain/domain-responsibility-map.md`. Slice-specific placement now starts from `planning/slices/slice-responsibility-map.md`.

## 2. Layer Router

| Layer | Belongs here | Local responsibility entry |
|---|---|---|
| Documentation | Planning-doc architecture, docs update workflows, documentation-layer placement, agent output rules, response-level commands, documentation prompts and scoped sync notes. | `planning/documentation/documentation-responsibility-map.md` |
| Scenario | Scenario text specs, UI specs, DATA sources, behavior items, clarifications and scenario questions. | `planning/diagrams/README.md` now; future scenario responsibility map. |
| Domain | Domain discovery, scenario-to-aggregate mapping, aggregate drafts, value object drafts, aggregate boundaries, accepted domain decisions and domain notes. | `planning/domain/domain-responsibility-map.md` |
| Slice | Slice drafts, slice source mapping, behavior coverage, Behavior-to-Test Trace inside slice drafts, slice questions, extension points, implementation notes, client/server/cross-cutting sidecars and slice workflows/templates/principles. | `planning/slices/slice-responsibility-map.md` |
| API | API contract rules, OpenAPI generation rules, client/server contract rules, API error contracts and generated artifact rules. | `planning/api/README.md` |
| Testing | Cross-slice testing principles, E2E workflows, test object patterns, test tooling workflows and reusable verification rules. Concrete slice behavior proof and assertions live in slice drafts/templates/workflows. | `planning/testing/README.md` |
| Architecture / ADR | Cross-slice architecture boundaries, accepted/candidate decisions and ADR workflow. | `planning/architecture/README.md`, `planning/adr/README.md` |
| VKR / Thesis | Clean thesis wording, evidence maps, thesis resources and presentation/defense-safe wording. | `planning/thesis/README.md`, `planning/vkr-clean-reference.md` |
| Archive / Recovery | Archive/replacement workflows, dirty drafts and non-canonical recovery notes. | `planning/archive-workflow/README.md`, `planning/dirty-drafts/` |

Global docs architecture theory lives in:

```text
planning/documentation/planning-docs-architecture-principles.md
```

## 3. Agent / Workflow Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/README.md` | Stable planning navigation and source-of-truth map; must not duplicate detailed current implementation status |
| `planning/planning-use-case-map.md` | Root action/use-case map: maps user actions and repeated commands to docs/workflows/templates/read paths, active context, traversal depth, read source mode, expected output and permission boundaries |
| `planning/workflow-activation-map.md` | Root workflow activation router: which workflows exist, when they activate, implicit vs explicit activation, response command discoverability and Workflow Preflight format |
| `planning/planning-maintenance-register.md` | Root register for deferred planning-docs/workflow maintenance tasks and condition-based follow-ups |
| `planning/planning-workflow-current.md` | Workflow rules, repository-edit workflow and historical/current-state reminders; concrete inventory sections are not implementation truth |
| `planning/vkr-clean-reference.md` | Clean VKR/thesis terminology, internal-to-clean mapping and evidence map for VKR-facing materials |
| `planning/planning-agent-protocol.md` | Role-level protocol, workflow activation rule, read rules, handoff rules and global do-not rules |
| `planning/agent-roles-and-required-actions.md` | Role map, required read order, mandatory actions and handoff boundaries for planning roles |
| `planning/agent-scope-boundaries-and-prompt-safety.md` | Rules for prompt creators and implementation agents: read broadly, change only explicit scope |
| `planning/repo-grounded-github-line-links-workflow.md` | User-facing GitHub line-link rules for repo-grounded code/docs/status explanations |
| `planning/planning-doc-responsibility-map.md` | Transitional root layer router for planning documentation; detailed local placement should move to local responsibility maps over time |
| `planning/replacement-file-generation-guide.md` | Archive/package generation rules for manual repo application |

## 4. Documentation Workflow Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/documentation/README.md` | Documentation workflow navigation and read order |
| `planning/documentation/documentation-responsibility-map.md` | Local responsibility map for documentation-layer information placement |
| `planning/documentation/documentation-update-plan-workflow.md` | Required preflight plan format for broad docs/navigation/status/register updates |
| `planning/documentation/documentation-update-workflow.md` | Documentation update process, output modes, quality checks and direct-edit/archive rules |
| `planning/documentation/planning-docs-architecture-principles.md` | Architecture principles for planning documentation itself, not runtime application architecture |
| `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | Response-level workflow for reviewable AI/agent outputs and commands: answer detail levels, sources/coverage blocks, handoff/review format, recheck/clarify/keep prev/no ch/use archive/active-context commands and section-level source expectations for major draft sections |
| `planning/documentation/local-global-documentation-sync-workflow.md` | Local detail to shared navigation/register synchronization rules |
| `planning/documentation/status-reconciliation-workflow.md` | Status reconciliation between current implementation evidence and planning docs |
| `planning/documentation/documentation-update-agent-prompt.md` | Derived prompt template for documentation update chats/agents; canonical docs win if there is conflict |

## 5. VKR / Thesis Clean Reference Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/vkr-clean-reference.md` | Clean terminology and evidence mapping for VKR, presentation, defense speech and practice-report wording |
| `planning/dirty-drafts/` | Non-canonical recovery notes only; useful wording must be checked against canonical docs and rewritten before VKR use |

VKR-facing materials must not use internal planning labels such as `L1`, `L2`, `dirty draft`, `agent`, `prompt` or implementation archive terminology.

## 6. Repository Edit Responsibility

Direct GitHub edits from ChatGPT are preferred for small scoped documentation changes because they create visible commits that can be inspected and reverted independently.

By default, use one file per commit when reasonable. Large generated replacement archives should be reserved for broad file/package generation when direct scoped commits are less practical.

Direct GitHub edits, file creation, file deletion, moves and commits require explicit user instruction.

## 7. Architecture Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/architecture/` | Architecture-level boundary maps and transition notes that are broader than one slice/API/test workflow |
| `planning/architecture/backend-legacy-and-l1-boundaries.md` | Backend legacy/L1 boundary map for cleanup, test classification, handler validation boundary and thesis/diploma architecture explanation |

Architecture docs do not replace slice docs, scenario sources, API contract docs, testing docs or ADRs.

## 8. Scenario Source Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/scenario-specification-principles.md` | Scenario specification principles and source-of-truth rules |
| `planning/scenario-domain-validation-principles.md` | Client-side vs server/domain validation distinction and domain-design input rules |
| `planning/diagrams/scenario-drafting-workflow.md` | Practical scenario draft workflow |
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and addenda |
| `planning/diagrams/scenario-data/` | Scenario DATA files: entered/seen/selected/filtered/attached/referenced data only |
| `planning/diagrams/scenario-ui-specs/` | `[UI-SCENARIO]` UI-visible requirements and accepted UI decisions, not React implementation |
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived, UI-scenario-derived and concern-derived behavior items |
| `planning/diagrams/scenario-questions-register.md` | Scenario/domain questions that can change scenario behavior, DATA, UI requirements or diagrams |

## 9. Domain Responsibility

Detailed domain-layer placement now belongs to:

```text
planning/domain/domain-responsibility-map.md
```

Transitional overview:

| File / folder | Responsibility |
|---|---|
| `planning/domain/README.md` | Domain-layer navigation and read order for the aggregate-based target model |
| `planning/domain/domain-responsibility-map.md` | Local responsibility map for domain-layer placement/routing |
| `planning/domain/domain-discovery-workflow.md` | Scenario behavior sources to aggregate/value-object discovery workflow |
| `planning/domain/scenario-to-aggregate-map.md` | Working bridge from scenario behavior items to aggregate candidates, value object candidates and cross-aggregate relations |
| `planning/domain/aggregate-drafting-workflow.md` | Workflow for creating/updating one aggregate draft |
| `planning/domain/value-object-drafting-workflow.md` | Workflow for creating/updating one value object draft |
| `planning/domain/aggregate-draft-template.md` | Template for one aggregate-boundary draft |
| `planning/domain/value-object-draft-template.md` | Template for one value object draft |
| `planning/domain/domain-modeling-principles.md` | Aggregate, value object, cross-aggregate and persistence-boundary principles |
| `planning/domain/domain-notes-register.md` | Domain notes not yet owned by a specific aggregate, value object or decision |
| `planning/domain/aggregates/` | Aggregate draft files, one aggregate boundary per file |
| `planning/domain/value-objects/` | Reusable/non-trivial value object draft files |
| `planning/domain/decisions/` | Accepted/proposed domain decisions |
| `planning/tables/domain-drafts/` | Historical monolithic domain discovery snapshots during migration |
| `planning/tables/` | Compiled/historical baselines and pre-domain source snapshots |

`planning/tables/` is not the current domain-layer entrypoint. It remains useful as historical/cross-check source material.

## 10. Slice Discovery Responsibility

Detailed slice-layer placement now belongs to:

```text
planning/slices/slice-responsibility-map.md
```

Transitional overview:

| File / folder | Responsibility |
|---|---|
| `planning/slices/README.md` | Slice-planning navigation and read order |
| `planning/slices/slice-responsibility-map.md` | Local responsibility map for slice-layer placement/routing |
| `planning/slices/SLICE-INDEX.md` | Concrete catalog of slice docs, workflows, templates, registers and drafts |
| `planning/slices/slice-draft-authoring-principles.md` | General slice draft authoring principles: scope, boundary, coverage, drift and trace |
| `planning/slices/slice-draft-authoring-workflow.md` | Root slice draft authoring process |
| `planning/slices/server-implementation-principles.md` | Server/backend implementation boundary principles |
| `planning/slices/client-implementation-principles.md` | Client layering and read/command/API ownership principles |
| `planning/slices/client-css-architecture-rules.md` | Client CSS ownership rules |
| `planning/slices/client-form-validation-implementation-principles.md` | Client implementation principles for deferred form validation behavior |
| `planning/slices/client-a11y-implementation-principles.md` | Client accessibility/ARIA implementation and test contract |
| `planning/slices/client-ui-style-workflow.md` | Client UI/style workflow |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Maps slices/client sidecars to scenario text, DATA, UI and behavior item sources |
| `planning/slices/slice-questions-register.md` | Shared overview of currently relevant local questions |
| `planning/slices/slice-extension-points-register.md` | Cross-slice extension points and change pressure |
| `planning/slices/slice-implementation-notes-register.md` | Concrete future implementation/client/testing notes |
| `planning/slices/client/` | Client templates, client-specific handoff docs and client slice drafts |
| `planning/slices/server/` | Server templates and server/backend/API slice drafts |
| `planning/slices/cross-cutting/` | Cross-cutting umbrella/coordination docs |
| `planning/slices/SL-*.md`, `planning/slices/l1/`, `planning/slices/l2/` | Legacy/historical slice draft locations during migration |

Scenario Flow and Behavior Items for slices come from `slice-scenario-flow-behavior-register.md` and the source files it points to.

Questions/extension/implementation registers do not replace scenario source files.

## 11. Client Planning Responsibility

Client-wide reusable rules/principles now live in the slice root, not under the client subfolder:

```text
planning/slices/client-implementation-principles.md
planning/slices/client-css-architecture-rules.md
planning/slices/client-form-validation-implementation-principles.md
planning/slices/client-a11y-implementation-principles.md
planning/slices/client-ui-style-workflow.md
```

Current client slice folder:

| File / folder | Responsibility |
|---|---|
| `planning/client/` | Deprecated old client planning index and client convention navigation during migration |
| `planning/slices/client/` | Current client slice templates, client-specific handoff docs and client sidecar drafts |
| `planning/slices/client/cross-cutting/` | Current client-side reusable/cross-cutting implementation drafts |

Concrete feature flow and status belongs in the relevant `.client.md` sidecar.

## 12. API / Testing / ADR Responsibility

| File / folder | Responsibility |
|---|---|
| `planning/api/` | API contract principles, OpenAPI structural contract, API error contract and constants relationship |
| `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | Server request validation / FluentValidation boundary rules for slices |
| `planning/testing/` | Cross-slice testing principles, E2E workflow, test object patterns |
| `planning/adr/` | ADR workflow, architecture decision notes and ADR candidates |

## 13. Evidence Link Responsibility

Repo-grounded line-link rules belong in:

```text
planning/repo-grounded-github-line-links-workflow.md
```

Other workflow docs may link to that file, but should not duplicate detailed line-link mechanics unless needed for a local role checklist.

## 14. Responsibility Decision Heuristic

Use this file first to choose the layer. Then use the local responsibility map or README for that layer.

```text
1. action/use-case routing -> planning/planning-use-case-map.md
2. workflow activation routing -> planning/workflow-activation-map.md
3. planning maintenance follow-up -> planning/planning-maintenance-register.md
4. documentation architecture/process/commands/prompt/sync note -> planning/documentation/documentation-responsibility-map.md
5. scenario text/DATA/UI/behavior source -> planning/diagrams/scenario-*/
6. domain discovery/aggregate/value-object/decision placement -> planning/domain/domain-responsibility-map.md
7. slice-layer placement/routing -> planning/slices/slice-responsibility-map.md
8. slice-to-source mapping -> planning/slices/slice-scenario-flow-behavior-register.md
9. slice draft authoring principles -> planning/slices/slice-draft-authoring-principles.md
10. server implementation principles -> planning/slices/server-implementation-principles.md
11. client implementation/CSS/a11y/form/UI rules -> planning/slices/client-*.md
12. active parent backend/business slice file -> planning/slices/SL-*.md now or planning/slices/server/ for new drafts
13. client sidecar file -> planning/slices/client/ for new drafts; legacy sidecars may still live in planning/slices/l1 or planning/slices/l2
14. slice-wide question overview -> planning/slices/slice-questions-register.md
15. extension/change pressure -> planning/slices/slice-extension-points-register.md
16. future implementation/client/testing note -> planning/slices/slice-implementation-notes-register.md
17. architecture-level cleanup / legacy-current boundary -> planning/architecture/
18. agent scope/prompt safety rule -> planning/agent-scope-boundaries-and-prompt-safety.md
19. repo-grounded evidence line links -> planning/repo-grounded-github-line-links-workflow.md
20. client/server contract split -> planning/api/client-server-contract-principles.md
21. test layer boundaries / E2E workflow -> planning/testing/
22. accepted/current architecture decision -> architecture-decision-notes.md
23. possible future full ADR -> adr-candidates.md
24. VKR/presentation/defense clean wording -> planning/vkr-clean-reference.md
25. raw non-canonical recovery wording -> planning/dirty-drafts/
26. broad docs/navigation/status/register update plan -> planning/documentation/documentation-update-plan-workflow.md
27. planning documentation architecture principle -> planning/documentation/planning-docs-architecture-principles.md
28. reviewable AI/agent response format and commands -> planning/documentation/reviewable-agent-output-and-commands-workflow.md
29. documentation local/global sync rule -> planning/documentation/local-global-documentation-sync-workflow.md
```

## 15. Future Cleanup Rule

When local responsibility maps exist for documentation, scenario, domain, slice, API/testing and VKR layers, shrink this root map to a thin router.

Do not remove useful routing rows until an equivalent local map exists and README/navigation points to it.

Track deferred cleanup in:

```text
planning/planning-maintenance-register.md
```
