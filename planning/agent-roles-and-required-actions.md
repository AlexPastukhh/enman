# Agent Roles And Required Actions

Status: current role map / workflow activation synchronized
Doc version: v0.1.0
Scope: reusable chat roles, required read order, mandatory actions, shared register updates and handoff boundaries

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/README.md @ Doc version: v0.2.0
    - planning/planning-agent-protocol.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v0.7.0
  Content:
    - planning/workflow-activation-map.md @ Doc version: v0.2.0
  Internal dependencies:
    - none
  Not checked:
    - role-specific downstream workflow files outside ROOT-SRC-2B scope
```

Planning work should be possible without a long external prompt.

A future chat should be able to start from:

```text
planning/README.md
```

then use this role map to identify:

```text
- which role it is acting as;
- which workflow docs it must read;
- which files/registers it owns;
- which outputs it may produce;
- which actions are mandatory before final response/archive;
- which work belongs to a different role.
```

This file does not replace detailed workflow docs.

It points each role to the right workflow docs and defines non-negotiable actions.

## 2. Universal Rules For All Planning Roles

```text
Sources:
  Format/process:
    - planning/README.md @ Doc version: v0.2.0
    - planning/planning-agent-protocol.md @ Doc version: v0.1.0
    - planning/workflow-activation-map.md @ Doc version: v0.2.0
    - planning/repo-grounded-github-line-links-workflow.md @ version not confirmed
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.7.0
  Internal dependencies:
    - Purpose
  Not checked:
    - implementation evidence only when the role task requires it
```

Every planning role must:

```text
1. Start from planning/README.md.
2. Read planning/planning-agent-protocol.md.
3. Read planning/workflow-activation-map.md before non-trivial planning/repo work.
4. Output Workflow Preflight for non-trivial planning/repo work.
5. Read planning/repo-grounded-github-line-links-workflow.md.
6. Read planning/planning-doc-responsibility-map.md.
7. Read the role-specific workflow docs listed below.
8. Work from current repo state, not memory.
9. Identify local files and shared registers that may need synchronization.
10. Keep important open questions and unresolved risks first.
11. Record an assumption/current direction when continuing without a final answer.
12. Give each important question a clear status.
13. Update navigation/responsibility maps when adding, moving or superseding docs.
14. Avoid direct GitHub writes unless explicitly requested.
15. When describing repo code/docs/status, provide exact GitHub line links.
```

Workflow Preflight should include:

```text
Active role:
Task type:
Activated workflows:
Implicit checks:
Requires explicit permission:
Not activated but relevant:
Future / missing workflows:
```

Question fields used across roles:

```text
Question status:
Question:
Assumption / current direction:
Impact:
Shared register / local-only reason:
```

Common question statuses:

```text
open
blocked
assumption
accepted direction
future review
resolved
superseded
local only
```

Assumption rule:

```text
If work continues without a final answer, write the assumption clearly so the user can confirm, reject or refine it.
```

Evidence link rule:

```text
If the answer explains current implementation, tests, generated artifacts, docs status or a concrete problem, provide Markdown GitHub links to exact lines/ranges. Prefer commit SHA links. Use branch links only as fallback and note they may drift.
```

## 3. Documentation Keeper / Status Reconciliation Chat

```text
Sources:
  Format/process:
    - planning/documentation/README.md @ version not confirmed
    - planning/documentation/documentation-update-plan-workflow.md @ version not confirmed
    - planning/documentation/documentation-update-workflow.md @ version not confirmed
    - planning/documentation/status-reconciliation-workflow.md @ version not confirmed
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.7.0
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - documentation-layer workflow source pass outside ROOT-SRC-2B
```

### Role

Keeps planning documentation synchronized with current repo implementation, current generated artifacts, navigation, responsibility maps and shared registers.

### Must read

```text
planning/README.md
planning/planning-agent-protocol.md
planning/workflow-activation-map.md
planning/repo-grounded-github-line-links-workflow.md
planning/planning-doc-responsibility-map.md
planning/documentation/README.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/replacement-file-generation-guide.md
```

### Mandatory actions

```text
1. Read current navigation and responsibility docs.
2. Read docs for the requested area.
3. Inspect current repo evidence only enough to reconcile status.
4. Identify stale docs, missing navigation, stale shared registers and orphan files.
5. Link specific repo evidence with GitHub line links when reporting status/drift.
6. Decide Add / Replace / Delete scope.
7. Ask only blocking questions that can change the archive.
8. If no blocking questions and archive mode is explicitly selected, create a zip archive with complete repo-relative files.
9. Include MANIFEST.md and APPLY.md in archive mode.
10. Do not change code or generated artifacts.
11. Do not write directly to GitHub unless explicitly requested.
```

### Owns

```text
planning/documentation/
planning/README.md updates when navigation changes
planning/planning-doc-responsibility-map.md updates when responsibility changes
shared register synchronization checks
archive/package generation when requested
```

### Does not own

```text
runtime implementation
source scenario meaning changes without scenario workflow
full ADR creation unless explicitly requested
client/server behavior implementation
```

## 4. Scenario Draft Chat

```text
Sources:
  Format/process:
    - planning/diagrams/scenario-drafting-workflow.md @ version not confirmed
    - planning/diagrams/scenario-responsibility-map.md @ version not confirmed
    - planning/diagrams/scenario-artifact-map.md @ version not confirmed
  Content:
    - planning/diagrams/** @ Doc version: v0.1.0 after F7K-D2 unless declared otherwise
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - scenario-layer source audit outside ROOT-SRC-2B
```

### Role

Creates and updates scenario text specs together with related DATA, UI specs, behavior items, validation/security addenda and scenario questions.

The scenario draft chat may also prepare a repo-grounded diagram request/prompt for the Diagram Chat when the user asks to generate diagrams from scenario sources.

This is not a separate diagram-prompt role; it is a handoff/request prepared by the Scenario Draft Chat for the single Diagram Chat.

### Must read

```text
planning/README.md
planning/planning-agent-protocol.md
planning/workflow-activation-map.md
planning/repo-grounded-github-line-links-workflow.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/README.md
planning/diagrams/scenario-drafting-workflow.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md, if exists
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-questions-register.md, if exists
planning/diagrams/scenario-clarifications/README.md, if exists
```

For diagram request/prompt preparation, also read:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

### Mandatory actions

```text
1. Identify the scenario scope and current source files.
2. Update scenario text spec, DATA, UI spec, behavior items and questions together when relevant.
3. Keep DATA files limited to entered/seen/selected/filtered/attached/referenced data.
4. Keep implementation mechanics out of scenario specs.
5. Keep UI-visible requirements separate from React/component implementation.
6. Add or update scenario questions when behavior/DATA/UI/security meaning is unresolved.
7. Give every important question a status and assumption/current direction.
8. Put open/blocked/assumption questions before accepted decisions.
9. Update scenario behavior items when required behavior changes.
10. Update scenario clarifications when an accepted clarification resolves a conflict.
11. If preparing diagrams, produce a repo-grounded diagram request/prompt for the Diagram Chat and do not draw diagrams in the scenario draft chat.
```

### Owns

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-clarifications/
scenario-to-diagram request/prompt preparation when requested
```

### Does not own

```text
backend implementation
client component implementation
draw.io generation
slice implementation plans beyond scenario-derived source behavior
```

## 5. Domain Draft Chat

```text
Sources:
  Format/process:
    - planning/scenario-specification-principles.md @ version not confirmed
    - planning/scenario-domain-validation-principles.md @ version not confirmed
    - planning/domain/domain-discovery-workflow.md @ version not confirmed
    - planning/domain/aggregate-drafting-workflow.md @ version not confirmed
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - domain notes/maps/decisions beyond active aggregate/value-object coverage
```

### Role

Creates and reconciles domain drafts, aggregate boundaries, value objects, invariants, state transitions and implementation cuts from scenario/DATA/validation sources.

### Must read

```text
planning/README.md
planning/planning-agent-protocol.md
planning/workflow-activation-map.md
planning/repo-grounded-github-line-links-workflow.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/domain-model.md
planning/tables/domain-drafts/domain-draft-01.md, if exists
relevant scenario text specs and DATA specs
```

### Mandatory actions

```text
1. Identify source scenario/DATA/validation inputs.
2. Extract value object candidates, aggregate root candidates, state/status values, invariants and state-transition methods.
3. Separate target domain direction from current implementation cut.
4. Record domain questions with status, assumption/current direction and impact.
5. Sync scenario-changing ambiguity to scenario questions/clarifications.
6. Sync slice-impacting ambiguity to slice questions register when downstream slices are affected.
7. Record architecture-wide decisions or candidates in ADR docs when needed.
8. Do not implement application/API/client code.
```

### Owns

```text
domain draft files
implementation cut notes
l1-domain-testing-rules updates when domain test responsibility changes
scenario-derived domain validation extraction
```

### Does not own

```text
controller/API implementation
client sidecars
scenario prose unless scenario meaning must be fixed through scenario workflow
```

## 6. Slice Draft Chat

```text
Sources:
  Format/process:
    - planning/slices/README.md @ version not confirmed
    - planning/slices/slice-responsibility-map.md @ version not confirmed
    - planning/slices/slice-draft-authoring-workflow.md @ version not confirmed
    - planning/slices/slice-draft-authoring-principles.md @ version not confirmed
    - planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md @ version not confirmed
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md @ version not confirmed
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planned planning/slices/slice-source-sync-register.md @ not created
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - slice source-sync register and slice refactor not started
```

### Role

Creates, reviews and updates backend parent slice files, client sidecars and cross-cutting/helper slices through draft-driven discovery.

### Must read

```text
planning/README.md
planning/planning-agent-protocol.md
planning/workflow-activation-map.md
planning/repo-grounded-github-line-links-workflow.md
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/draft-driven-discovery-principles.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/examples/README.md, if exists
```

If API/client contract is involved:

```text
planning/api/README.md
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

If client work is involved:

```text
planning/client/README.md
planning/client/cross-cutting/README.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
```

### Mandatory actions

```text
1. Find existing planning/slices/SL-*.md files before creating new ones.
2. Do not duplicate existing slice files under a new ID.
3. Use shortened draft by default unless the user asks for full file/archive.
4. For full backend files, include diagram-like Visual Scenario Flow before Scenario Slice Flow.
5. Include diagram-like Visual Implementation Flow before Implementation Flow.
6. Keep Behavior Coverage separate from Test / Verification Plan.
7. Select behavior items from source specs/registers; do not invent them inside the slice.
8. Use Source BI TBD only as a temporary marker and say so.
9. Put open/blocked/assumption questions first.
10. Give each important question a status and assumption/current direction.
11. Mirror currently relevant local questions to slice-questions-register.md.
12. Mirror extension/change pressure to slice-extension-points-register.md.
13. Mirror concrete future implementation/client/testing notes to slice-implementation-notes-register.md.
14. Do not create .client.md before concrete client work starts.
15. When reviewing implemented slices, link exact implementation/test lines that support status.
```

### Owns

```text
planning/slices/SL-*.md
planning/slices/*.client.md, only when concrete client work starts
planning/slices/cross-cutting/
planning/slices/slice-questions-register.md sync
planning/slices/slice-extension-points-register.md sync
planning/slices/slice-implementation-notes-register.md sync
```

### Does not own

```text
scenario source behavior invention
domain model changes without domain/scenario source
runtime code implementation
```

## 7. Diagram Chat

```text
Sources:
  Format/process:
    - planning/diagramming/README.md @ version not confirmed
    - planning/diagramming/diagramming-responsibility-map.md @ version not confirmed
    - planning/diagrams/diagram-prompt-generation-workflow.md @ version not confirmed
    - planning/diagrams/drawio-diagram-generation-workflow.md @ version not confirmed
  Content:
    - planning/diagrams/scenario-diagram-consistency-report.md @ version not confirmed
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - diagramming-layer source pass outside ROOT-SRC-2B
```

### Role

Runs repo-grounded diagram work as a single role.

The Diagram Chat can receive a diagram request/prompt prepared by a Scenario Draft Chat, Documentation Keeper or user, but there is no separate prompt-architect role for diagrams.

The Diagram Chat owns both:

```text
Phase 1 — preflight / source reconciliation / batch plan
Phase 2 — selected batch draw.io XML generation
Phase 3 — archive packaging
```

It must not generate diagrams before preflight unless the user explicitly requests immediate generation and accepts the risk.

### Must read

```text
planning/README.md
planning/planning-agent-protocol.md
planning/workflow-activation-map.md
planning/repo-grounded-github-line-links-workflow.md
planning/diagrams/README.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md, if exists
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-questions-register.md, if exists
planning/diagrams/scenario-clarifications/README.md, if exists
all sources required by the selected diagram batch
```

### Mandatory actions

```text
1. Start with Phase 1 preflight unless the user explicitly asks for immediate generation.
2. Read current repo sources; do not work from memory or from a stale prompt only.
3. Discover actual scenario/spec folders and actual index filenames.
4. Inspect scenario text specs, DATA, UI specs, behavior items, API/security addenda, clarifications and questions relevant to the selected batch.
5. Inspect implementation evidence enough to avoid overclaiming `[IMPLEMENTED]` status.
6. Identify conflicts, stale wording and open questions before drawing.
7. Provide exact GitHub line links for repo evidence used in preflight/status explanations.
8. Propose diagram batches and generate only the selected batch.
9. Use status markers: [CORE], [IMPLEMENTED], [DESIGNED], [PLANNED], [DEFERRED], [QUESTION].
10. Target draw.io XML and prefer one multi-page `.drawio` diagram book.
11. Keep VKR-clean diagrams free of AI/internal workflow wording.
12. Package complete repo-relative files with MANIFEST.md and APPLY.md.
13. Do not write directly to GitHub unless explicitly requested.
```

### Owns

```text
diagram preflight
diagram batch plan
draw.io / XML diagram book artifacts
diagram companion planning files
diagram archive MANIFEST/APPLY
```

### Does not own

```text
source scenario meaning changes
scenario/DATA/behavior item authoring
runtime code
source planning doc cleanup unless explicitly asked for a docs cleanup archive
```

### Handoff from Scenario Draft Chat

When scenarios are ready for diagrams, Scenario Draft Chat may prepare a diagram request/prompt for this Diagram Chat.

That request should include:

```text
- diagram goal;
- scenario/source files to inspect;
- known questions/clarifications;
- required status markers;
- target batch suggestion;
- draw.io XML / diagram-book target.
```

The Diagram Chat still rereads the repo and runs preflight.

## 8. Architecture / Implementation Handoff Chat

```text
Sources:
  Format/process:
    - planning/planning-agent-protocol.md @ Doc version: v0.1.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.2.0
    - planning/architecture/README.md @ version not confirmed
    - planning/api/README.md @ version not confirmed
    - planning/slices/README.md @ version not confirmed
  Content:
    - current implementation/code/tests/generated artifacts @ not checked in ROOT-SRC-2B
  Internal dependencies:
    - Universal Rules For All Planning Roles
    - Role Handoff Rule
  Not checked:
    - implementation handoff target files outside ROOT-SRC-2B
```

### Role

Converts approved planning scope into implementation handoff or implementation work when explicitly asked.

It can help when a coding agent is token-limited or unavailable, but it must not skip planning gates.

### Must read

```text
planning/README.md
planning/planning-agent-protocol.md
planning/workflow-activation-map.md
planning/repo-grounded-github-line-links-workflow.md
planning/planning-use-case-map.md
planning/workflow-activation-map.md
relevant scenario/domain/slice/API/testing docs
current implementation files in scope
```

### Mandatory actions

```text
1. Identify exact implementation scope.
2. Check current implementation before proposing changes.
3. Separate current implementation from target direction.
4. Respect generated artifact gates.
5. List files to change and tests/checks to run.
6. Use exact GitHub line links when explaining current code/status/problems.
7. If code is generated, do not silently change planning meaning.
8. After implementation, report docs/status/register updates needed.
9. If context/tokens are ending, produce handoff summary with files, assumptions, open questions and next exact actions.
```

### Owns

```text
implementation handoff summaries
code changes only when explicitly requested
post-implementation docs impact list
```

### Does not own

```text
unbounded architecture redesign
unscoped client/server implementation
silent planning docs rewrites
```

## 9. API / Contract Keeper Gate

```text
Sources:
  Format/process:
    - planning/api/README.md @ version not confirmed
    - planning/api/client-server-contract-principles.md @ version not confirmed
    - planning/api/openapi-contract-generation.md @ version not confirmed
    - planning/api/client-constants-generation.md @ version not confirmed
    - planning/api/api-error-contract.md @ version not confirmed
  Content:
    - generated API/client artifacts @ not checked in ROOT-SRC-2B
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - API/generated artifact currentness outside ROOT-SRC-2B
```

This can be a dedicated chat when API drift is the main task, or a required gate inside slice/client/implementation work.

### Must read

```text
planning/api/README.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
planning/api/api-error-contract.md
planning/api/api-error-mapping-boundary.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

### Mandatory actions

```text
1. Distinguish OpenAPI structural contract from generated semantic constants.
2. Check current Shared/openapi.json when API status matters.
3. Check generated TypeScript types when client consumption matters.
4. Check Shared/constants.json and Shared/errorcodes.json when semantic errors/constants matter.
5. Do not let client work guess route/DTO/status/error shapes.
6. Record contract status: target L1 / legacy-current / temporary compatibility / internal.
7. Link exact OpenAPI/source/type lines when explaining current contract facts.
```

## 10. Testing / E2E Keeper Gate

```text
Sources:
  Format/process:
    - planning/testing/README.md @ version not confirmed
    - planning/testing/testing-principles.md @ version not confirmed
    - planning/testing/e2e-testing-workflow.md @ version not confirmed
    - planning/testing/test-object-patterns.md @ version not confirmed
  Content:
    - current tests/E2E status @ not checked in ROOT-SRC-2B
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - testing source-sync register not created
```

This can be a dedicated chat when testing strategy is the main task, or a required gate inside slice/client/implementation work.

### Must read

```text
planning/testing/README.md
planning/testing/testing-principles.md
planning/testing/e2e-testing-workflow.md
planning/testing/test-object-patterns.md
planning/testing/playwright-e2e-cleanup-plan.md
```

### Mandatory actions

```text
1. Classify tests by layer: domain, API/integration, client/component, E2E.
2. Use E2E only for cross-layer browser-client-server-visible outcome.
3. Do not use E2E for exhaustive field/client validation.
4. Keep Behavior Coverage separate from Test / Verification Plan.
5. Protect existing auth E2E baseline unless L1 auth consolidation is explicitly in scope.
6. Link exact test lines when claiming test coverage.
```

## 11. Role Handoff Rule

```text
Sources:
  Format/process:
    - planning/planning-agent-protocol.md @ Doc version: v0.1.0
    - planning/workflow-activation-map.md @ Doc version: v0.2.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.7.0
  Internal dependencies:
    - Universal Rules For All Planning Roles
  Not checked:
    - target-role source files checked only when handoff scope requires them
```

When a role reaches a boundary that belongs to another role, it must not silently continue.

Instead, write:

```text
Boundary reached:
Target role:
Reason:
Current facts:
Evidence links:
Open questions:
Assumption / current direction:
Recommended next action:
```

Examples:

```text
Scenario Draft Chat -> Diagram Chat
Domain Draft Chat -> Slice Draft Chat
Slice Draft Chat -> Architecture / Implementation Handoff Chat
Implementation Handoff Chat -> Documentation Keeper Chat
```


## 12. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.7.0
  Content:
    - planning/README.md @ Doc version: v0.2.0
    - planning/planning-agent-protocol.md @ Doc version: v0.1.0
    - planning/workflow-activation-map.md @ Doc version: v0.2.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
    - Universal Rules For All Planning Roles
    - Role Handoff Rule
  Not checked:
    - role-specific downstream workflow files outside ROOT-SRC-2B
```

```text
- ROOT-SRC-2B added Doc version: v0.1.0 and local section-level Sources blocks to this role map.
- ROOT-SRC-2B did not change role boundaries, required read order or permission boundaries.
```
