# Planning Document Responsibility Map

Status: transitional global responsibility map / root layer router
Doc version: v0.6.0
Scope: routes planning documentation information to the correct layer and, where available, to the layer-local responsibility map

## 1. Core Rule

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/workflow-activation-map.md @ Doc version: v0.4.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.5.0
  Internal dependencies:
    - none
  Not checked:
    - local layer responsibility map audits outside ROOT-SRC-2A
```

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

Ownership/routing link rule:

```text
Responsibility links choose the owner layer/file.
They are not source/version dependencies by default.
```

A responsibility pointer becomes a source dependency only when another file/section actually consumes the owner file's meaning, rule, output shape, evidence or reviewed source truth. Do not build cascade edges from responsibility-map links alone.

Command/action routing remains owned by:

```text
planning/planning-use-case-map.md
```

Target model:

```text
root responsibility map
  = chooses the layer and points to local responsibility maps.

local layer responsibility map
  = decides where information belongs inside that layer.

folder README / index
  = navigation and read order.
```

Until local responsibility maps exist for all layers, this file keeps transitional fallback routing for API, architecture and VKR responsibilities. Scenario-specific placement now starts from `planning/diagrams/scenario-responsibility-map.md`. Diagramming-specific placement now starts from `planning/diagramming/diagramming-responsibility-map.md`. Domain-specific placement now starts from `planning/domain/domain-responsibility-map.md`. Slice-specific placement now starts from `planning/slices/slice-responsibility-map.md`.

## 2. Layer Router

```text
Sources:
  Format/process:
    - planning/README.md @ Doc version: v0.3.0
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
  Content:
    - planning/diagrams/scenario-responsibility-map.md @ version not confirmed
    - planning/diagramming/diagramming-responsibility-map.md @ version not confirmed
    - planning/domain/domain-responsibility-map.md @ version not confirmed
    - planning/slices/slice-responsibility-map.md @ version not confirmed
    - planning/testing/testing-responsibility-map.md @ version not confirmed
  Internal dependencies:
    - Core Rule
  Not checked:
    - local layer maps not audited in ROOT-SRC-2A
```

| Layer | Belongs here | Local responsibility entry |
|---|---|---|
| Documentation | Planning-doc architecture, docs update workflows, documentation-layer placement, agent output rules, response-level commands, documentation prompts and scoped sync notes. | `planning/documentation/documentation-responsibility-map.md` |
| Scenario | Scenario text specs, UI specs, inline/reusable DATA, behavior items, clarifications, scenario questions and scenario artifact mapping. Current scenario files are still physically under `planning/diagrams/` during migration. | `planning/diagrams/scenario-responsibility-map.md` |
| Diagramming | Diagram prompt/preflight workflows, draw.io generation workflow, diagram source consistency, status marker rules and diagram artifact governance. Current workflow files may still physically live under `planning/diagrams/` until the move batch. | `planning/diagramming/diagramming-responsibility-map.md` |
| Domain | Domain discovery, scenario-to-aggregate mapping, aggregate drafts, value object drafts, aggregate boundaries, accepted domain decisions and domain notes. | `planning/domain/domain-responsibility-map.md` |
| Slice | Slice drafts, slice source mapping, behavior coverage, Behavior-to-Test Trace inside slice drafts, slice questions, extension points, implementation notes, client/server/cross-cutting sidecars and slice workflows/templates/principles. | `planning/slices/slice-responsibility-map.md` |
| API | API contract rules, OpenAPI generation rules, client/server contract rules, API error contracts and generated artifact rules. | `planning/api/README.md` |
| Testing | Cross-slice testing principles, E2E workflows, test object patterns, test tooling workflows and reusable verification rules. Concrete slice behavior proof and assertions live in slice drafts/templates/workflows. | `planning/testing/testing-responsibility-map.md` |
| Architecture / ADR | Cross-slice architecture boundaries, accepted/candidate decisions and ADR workflow. | `planning/architecture/README.md`, `planning/adr/README.md` |
| VKR / Thesis | Clean thesis wording, evidence maps, thesis resources and presentation/defense-safe wording. | `planning/thesis/README.md`, `planning/vkr-clean-reference.md` |
| Archive / Recovery | Archive/replacement workflows, dirty drafts and non-canonical recovery notes. | `planning/archive-workflow/README.md`, `planning/dirty-drafts/` |

Global docs architecture theory lives in:

```text
planning/documentation/planning-docs-architecture-principles.md
```

## 3. Agent / Workflow Responsibility

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/workflow-activation-map.md @ Doc version: v0.4.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
    - planning/root-source-sync-register.md @ Doc version: v1.5.0
  Internal dependencies:
    - Layer Router
  Not checked:
    - protocol/role source pass covered by ROOT-SRC-2B
```

| File / folder | Responsibility |
|---|---|
| `planning/documentation/field-kits/root-use-case-map-field-kit.md` | Reusable setup kit for deriving one concrete project root use-case map and common command clusters; not a runtime router |
| `planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md` | Profile-specific setup kit for adding scenario/domain/slice route families to a project root use-case map; not a second map |
| `planning/README.md` | Stable planning navigation and source-of-truth map; must not duplicate detailed current implementation status |
| `planning/status-evidence-profile.md` | Active Enman project profile for evidence/current-reality model and status vocabulary; not a reusable workflow |
| `planning/shared-visibility-map.md` | Active Enman project map for local-detail to shared index/register visibility; not a reusable workflow |
| `planning/source-usage-cascade-profile.md` | Active Enman project profile for source/consumer categories, row conventions and cascade triggers; not a reusable workflow |
| `planning/planning-use-case-map.md` | Root action/use-case map: maps user actions and repeated commands to docs/workflows/templates/read paths, active context, traversal depth, read source mode, expected output and permission boundaries |
| `planning/workflow-activation-map.md` | Root workflow activation/read-order helper: which workflows exist, when they activate, implicit vs explicit activation and Workflow Preflight format; not the owner of command/action semantics when UCM owns the route |
| `planning/planning-maintenance-register.md` | Root register for deferred planning-docs/workflow maintenance tasks and condition-based follow-ups |
| `planning/vkr-clean-reference.md` | Clean VKR/thesis terminology, internal-to-clean mapping and evidence map for VKR-facing materials |
| `planning/planning-agent-protocol.md` | Role-level protocol, workflow activation rule, read rules, handoff rules and global do-not rules |
| `planning/agent-roles-and-required-actions.md` | Role map, required read order, mandatory actions and handoff boundaries for planning roles |
| `planning/agent-scope-boundaries-and-prompt-safety.md` | Rules for prompt creators and implementation agents: read broadly, change only explicit scope |
| `planning/repo-grounded-github-line-links-workflow.md` | User-facing GitHub line-link rules for repo-grounded code/docs/status explanations |
| `planning/planning-doc-responsibility-map.md` | Transitional root layer router for planning documentation; detailed local placement should move to local responsibility maps over time |
| `planning/replacement-file-generation-guide.md` | Archive/package generation rules for manual repo application |
| `planning/documentation/parallel-work/` | Reusable documentation-layer owner for parallel-agent staging workspaces and aggregate sync plans; not a concrete project workspace by itself |


Project-wide profiles are concrete Enman configuration files. Reusable field-kit/workflow logic lives in the active reusable documentation layer under `planning/documentation/`. The former `planning/documentation-legacy/` folder was removed by ROOT-LEGACY-CLEAN; migration-only switch history is preserved under `planning/documentation-migration/`.


Use-case map responsibility rule:

```text
planning/planning-use-case-map.md
  = concrete Enman root router.

planning/documentation/field-kits/root-use-case-map-field-kit.md
  = reusable setup kit.

planning/documentation/use-case-map-workflow.md
  = repeated maintenance workflow.

planning/documentation/USE-CASE-MAP-TEMPLATE.md
  = exact reusable shape.

planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
  = profile-specific route setup for scenario/domain/slice projects.
```

Do not create a second generic use-case map inside the documentation layer.

## 4. Command System / Goal Map / Tampermonkey Responsibility

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/workflow-activation-map.md @ Doc version: v0.4.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/workstreams/command-system-and-tampermonkey-goal-map.md @ version not confirmed
    - planning/workstreams/tampermonkey-command-projection-plan.md @ version not confirmed
    - tools/tampermonkey/README.md @ version not declared
    - tools/tampermonkey/IMPLEMENTATION-NOTES.md @ version not declared
    - tools/tampermonkey/chat-command-palette.user.js @ implementation/helper source, version not applicable
  Internal dependencies:
    - Agent / Workflow Responsibility
  Not checked:
    - Goal Map/Tampermonkey source pass outside ROOT-SRC-2A
```

| File / folder | Responsibility |
|---|---|
| `planning/goal-map-principles-workflow-template.md` | Root Goal Map owner: principles, workflow, template, `карта цели`, `кц`, `Goal Map Brief`, `синх карта`, map status synchronization and target-state sync archive rules |
| `planning/goal-map-example.md` | Static example for the Goal Map format; demonstration only, not live state |
| `planning/workstreams/command-system-and-tampermonkey-goal-map.md` | Living Goal Map for the command-system/Tampermonkey workstream; read it before choosing next steps inside that workstream and update it when meaningful batches/decisions change current state |
| `planning/workstreams/tampermonkey-command-projection-plan.md` | Planning owner for projecting root use-case map routes into compact editable Tampermonkey prompt bodies |
| `tools/tampermonkey/README.md` | Tampermonkey helper implementation entrypoint, boundaries and manual test orientation; not command source of truth |
| `tools/tampermonkey/IMPLEMENTATION-NOTES.md` | Current helper implementation notes, decisions, use cases, UI sketches and future split candidates; not command source of truth |
| `tools/tampermonkey/chat-command-palette.user.js` | Userscript implementation of the prompt-helper UI and inline command profiles; last in source-of-truth order and must not invent command semantics |

Discovery rule:

```text
For command-helper or long-running command-system work:
  1. Start from planning/planning-use-case-map.md.
  2. If work is long-running, read the relevant living Goal Map.
  3. For Goal Map behavior, read planning/goal-map-principles-workflow-template.md.
  4. For Tampermonkey inserted commands, treat userscript bodies as projections only.
  5. If helper/projection conflicts with the use-case map or owner workflows, the use-case map / owner workflows win.
```

## 5. Documentation Workflow Responsibility

```text
Sources:
  Format/process:
    - planning/documentation/documentation-responsibility-map.md @ version not confirmed
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
  Content:
    - planning/documentation/** @ mixed versions/statuses
  Internal dependencies:
    - Layer Router
  Not checked:
    - documentation-layer workflow source passes outside ROOT-SRC-2A
```

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

## 6. VKR / Thesis Clean Reference Responsibility

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
  Content:
    - planning/vkr-clean-reference.md @ version not confirmed
    - planning/dirty-drafts/** @ non-canonical recovery notes
  Internal dependencies:
    - Layer Router
  Not checked:
    - VKR/thesis source pass outside ROOT-SRC-2A
```

| File / folder | Responsibility |
|---|---|
| `planning/vkr-clean-reference.md` | Clean terminology and evidence mapping for VKR, presentation, defense speech and practice-report wording |
| `planning/dirty-drafts/` | Non-canonical recovery notes only; useful wording must be checked against canonical docs and rewritten before VKR use |

VKR-facing materials must not use internal planning labels such as `L1`, `L2`, `dirty draft`, `agent`, `prompt` or implementation archive terminology.

## 7. Repository Edit Responsibility

```text
Sources:
  Format/process:
    - planning/README.md @ Doc version: v0.3.0
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/replacement-file-generation-guide.md @ Doc version: v0.1.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.5.0
  Internal dependencies:
    - Agent / Workflow Responsibility
  Not checked:
    - archive/output workflow pass outside ROOT-SRC-2A
```

Direct GitHub edits from ChatGPT are preferred for small scoped documentation changes because they create visible commits that can be inspected and reverted independently.

By default, use one file per commit when reasonable. Large generated replacement archives should be reserved for broad file/package generation when direct scoped commits are less practical.

Direct GitHub edits, file creation, file deletion, moves and commits require explicit user instruction.

## 8. Architecture Responsibility

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
  Content:
    - planning/architecture/README.md @ version not confirmed
    - planning/adr/README.md @ version not confirmed
  Internal dependencies:
    - Layer Router
  Not checked:
    - architecture/ADR docs not audited in ROOT-SRC-2A
```

| File / folder | Responsibility |
|---|---|
| `planning/architecture/` | Architecture-level boundary maps and transition notes that are broader than one slice/API/test workflow |
| `planning/architecture/backend-legacy-and-l1-boundaries.md` | Backend legacy/L1 boundary map for cleanup, test classification, handler validation boundary and thesis/diploma architecture explanation |

Architecture docs do not replace slice docs, scenario sources, API contract docs, testing docs or ADRs.

## 9. Scenario Source Responsibility

```text
Sources:
  Format/process:
    - planning/diagrams/scenario-responsibility-map.md @ version not confirmed
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
  Content:
    - planning/diagrams/** @ Doc version: v0.1.0 after F7K-D2 unless declared otherwise
  Internal dependencies:
    - Layer Router
  Not checked:
    - scenario layer not re-audited in ROOT-SRC-2A
```

Detailed scenario-layer placement now belongs to:

```text
planning/diagrams/scenario-responsibility-map.md
```

Transitional overview:

| File / folder | Responsibility |
|---|---|
| `planning/diagrams/README.md` | Scenario-layer entrypoint and read order |
| `planning/diagrams/scenario-responsibility-map.md` | Local responsibility map for scenario-layer placement/routing |
| `planning/diagrams/scenario-artifact-map.md` | Currentness and artifact mapping across text/DATA/UI/behavior/clarification files |
| `planning/scenario-specification-principles.md` | Scenario specification principles and source-of-truth rules |
| `planning/scenario-domain-validation-principles.md` | Client-side vs server/domain validation distinction and domain-design input rules |
| `planning/diagrams/scenario-drafting-workflow.md` | Practical scenario draft workflow |
| `planning/diagrams/scenario-text-specs/` | Scenario text specs and addenda |
| `planning/diagrams/scenario-data/` | Scenario DATA files: entered/seen/selected/filtered/attached/referenced data only |
| `planning/diagrams/scenario-ui-specs/` | `[UI-SCENARIO]` UI-visible requirements and accepted UI decisions, not React implementation |
| `planning/diagrams/scenario-behavior-items/` | Scenario-derived, UI-scenario-derived and concern-derived behavior items |
| `planning/diagrams/scenario-clarifications/` | Temporary scenario-level clarification/guardrail files |
| `planning/diagrams/scenario-questions-register.md` | Scenario/domain questions that can change scenario behavior, DATA, UI requirements or diagrams |

## 10. Domain Responsibility

```text
Sources:
  Format/process:
    - planning/domain/domain-responsibility-map.md @ version not confirmed
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/** @ Doc version: v0.1.0
    - planning/domain/value-objects/** @ Doc version: v0.1.0
  Internal dependencies:
    - Layer Router
  Not checked:
    - domain notes/maps/decisions beyond active aggregate/value objects remain deferred
```

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

## 11. Slice Discovery Responsibility

```text
Sources:
  Format/process:
    - planning/slices/slice-responsibility-map.md @ version not confirmed
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
  Content:
    - planned planning/slices/slice-source-sync-register.md @ not created
  Internal dependencies:
    - Layer Router
    - Domain Responsibility
  Not checked:
    - slice source-sync register not created yet
```

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

## 12. Client Planning Responsibility

```text
Sources:
  Format/process:
    - planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md @ version not confirmed
    - planning/slices/slice-responsibility-map.md @ version not confirmed
  Content:
    - planned planning/slices/slice-source-sync-register.md @ not created
  Internal dependencies:
    - Slice Discovery Responsibility
  Not checked:
    - client slice docs not audited in ROOT-SRC-2A
```

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

## 13. API / Testing / ADR Responsibility

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/testing/testing-responsibility-map.md @ version not confirmed
  Content:
    - planning/api/README.md @ version not confirmed
    - planning/testing/** @ mixed versions/statuses
    - planning/adr/README.md @ version not confirmed
  Internal dependencies:
    - Layer Router
  Not checked:
    - API/testing/ADR source passes outside ROOT-SRC-2A
```

| File / folder | Responsibility |
|---|---|
| `planning/api/` | API contract principles, OpenAPI structural contract, API error contract and constants relationship |
| `planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md` | Server request validation / FluentValidation boundary rules for slices |
| `planning/testing/testing-responsibility-map.md` | Testing-layer routing, read selector and current/legacy testing file ownership |
| `planning/testing/` | Cross-slice testing principles, E2E workflow, test object patterns and testing support docs |
| `planning/adr/` | ADR workflow, architecture decision notes and ADR candidates |

## 14. Evidence Link Responsibility

```text
Sources:
  Format/process:
    - planning/repo-grounded-github-line-links-workflow.md @ version not confirmed
    - planning/status-evidence-profile.md @ version not confirmed
  Content:
    - current branch/code/tests @ not checked in ROOT-SRC-2A
  Internal dependencies:
    - Core Rule
  Not checked:
    - implementation status evidence not checked in ROOT-SRC-2A
```

Repo-grounded line-link rules belong in:

```text
planning/repo-grounded-github-line-links-workflow.md
```

Other workflow docs may link to that file, but should not duplicate detailed line-link mechanics unless needed for a local role checklist.

## 15. Responsibility Decision Heuristic

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/workflow-activation-map.md @ Doc version: v0.4.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v1.5.0
  Internal dependencies:
    - Layer Router
    - Agent / Workflow Responsibility
  Not checked:
    - local layer responsibility maps not audited in ROOT-SRC-2A
```

Use this file first to choose the layer. Then use the local responsibility map or README for that layer.

```text
1. action/use-case routing -> planning/planning-use-case-map.md
2. workflow activation routing -> planning/workflow-activation-map.md
3. planning maintenance follow-up -> planning/planning-maintenance-register.md
4. documentation architecture/process/commands/prompt/sync note -> planning/documentation/documentation-responsibility-map.md
5. scenario text/DATA/UI/behavior/source placement -> planning/diagrams/scenario-responsibility-map.md
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
21. testing-layer routing / test layer boundaries / E2E workflow -> planning/testing/testing-responsibility-map.md
22. accepted/current architecture decision -> architecture-decision-notes.md
23. possible future full ADR -> adr-candidates.md
24. VKR/presentation/defense clean wording -> planning/vkr-clean-reference.md
25. raw non-canonical recovery wording -> planning/dirty-drafts/
26. broad docs/navigation/status/register update plan -> planning/documentation/documentation-update-plan-workflow.md
27. planning documentation architecture principle -> planning/documentation/planning-docs-architecture-principles.md
28. reviewable AI/agent response format and commands -> planning/documentation/reviewable-agent-output-and-commands-workflow.md
29. documentation local/global sync rule -> planning/documentation/local-global-documentation-sync-workflow.md
30. parallel-agent staging workspace / aggregate sync architecture -> planning/documentation/parallel-work/README.md and planning/documentation/documentation-responsibility-map.md
```

## 16. Future Cleanup Rule

```text
Sources:
  Format/process:
    - planning/planning-maintenance-register.md @ version not confirmed
    - planning/root-source-sync-register.md @ Doc version: v1.5.0
  Content:
    - planning/documentation-action-log.md @ version not confirmed / evidence trail
  Internal dependencies:
    - Responsibility Decision Heuristic
  Not checked:
    - maintenance register source pass outside ROOT-SRC-2A
```

When local responsibility maps exist for documentation, scenario, domain, slice, API/testing and VKR layers, shrink this root map to a thin router.

Do not remove useful routing rows until an equivalent local map exists and README/navigation points to it.

Track deferred cleanup in:

```text
planning/planning-maintenance-register.md
```

## Portable Starter-Kit Adaptation

```text
Sources:
  Format/process:
    - planning/documentation/PORTABLE-STARTER-KIT.md @ version not confirmed
    - planning/documentation/documentation-responsibility-map.md @ version not confirmed
  Content:
    - planning/documentation-migration/** @ migration history / version not confirmed
  Internal dependencies:
    - Documentation Workflow Responsibility
  Not checked:
    - portable starter kit not audited in ROOT-SRC-2A
```

```text
planning/documentation/PORTABLE-STARTER-KIT.md
```

Use only when copying/adapting the reusable documentation layer into a new project or documentation domain. It is not a normal daily read-order source after adaptation.


## 17. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v1.5.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/workflow-activation-map.md @ Doc version: v0.4.0
  Internal dependencies:
    - Layer Router
    - Agent / Workflow Responsibility
    - Responsibility Decision Heuristic
  Not checked:
    - local layer responsibility map audits outside ROOT-SRC-2A
```

```text
- ROOT-SRC-2A added Doc version: v0.1.0 and local section-level Sources blocks to this root responsibility router.
- ROOT-SRC-2A did not move layer-local responsibility content or claim full root-folder coverage.
- ROOT-SRC-2B refreshed protocol/role source status and bumped this file to Doc version: v0.2.0 without moving responsibility ownership.
- ROOT-SRC-3A refreshed output/archive source status and bumped this file to Doc version: v0.3.0 without changing routing/navigation semantics.
- CASCADE-ROUTE-1B bumped this file to Doc version: v0.4.0 and clarified that responsibility pointers are ownership/routing links, not source/version dependencies by default.
- PAR-WORK-1 bumped this file to Doc version: v0.5.0 and routed reusable parallel-agent workspace/sync architecture to the documentation layer.
- ROOT-LEGACY-CLEAN-STEP2 bumped this file to Doc version: v0.6.0 and removed active routing to deleted legacy/current-like root files while preserving parallel-work ownership.
```
