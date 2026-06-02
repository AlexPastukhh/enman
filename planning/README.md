# Planning Documentation

Status: stable navigation / source-of-truth map
Doc version: v0.1.0
Purpose: explain where to read planning materials and how not to confuse planning notes with implementation truth.

## 1. Core Rule

```text
Sources:
  Format/process:
    - planning/status-evidence-profile.md @ version not confirmed
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
    - planning/workflow-activation-map.md @ Doc version: v0.1.0
  Content:
    - current codebase/tests/generated artifacts @ not checked in this docs pass
  Internal dependencies:
    - none
  Not checked:
    - current branch implementation state outside ROOT-SRC-2A scope
```

Planning docs are not a replacement for checking the current codebase.

For implementation state, inspect the current branch, code, tests, migrations, generated API contracts and runtime screenshots.

For scenario behavior, use scenario specs and behavior items.

For VKR/thesis wording, use `planning/vkr-clean-reference.md`.


## 1A. Project-Wide Planning Profiles

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.6.0
  Content:
    - planning/status-evidence-profile.md @ version not confirmed
    - planning/shared-visibility-map.md @ version not confirmed
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Internal dependencies:
    - Core Rule
  Not checked:
    - full local source audit for status/shared-visibility profiles outside ROOT-SRC-2A scope
```

These root planning profiles define Enman project configuration used by multiple planning layers:

```text
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
```

Use them when a task involves:

```text
- implementation/status evidence;
- local-detail to shared-register visibility;
- source/consumer/cascade review relationships.
```

They are active Enman project profiles, not reusable documentation workflows.

The reusable documentation layer is active under:

```text
planning/documentation/
```

The previous active documentation folder is temporarily preserved under:

```text
planning/documentation-legacy/
```

Migration-only reusable-candidate history is preserved under:

```text
planning/documentation-migration/
```

## 2. Repository Editing Workflow

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
    - planning/replacement-file-generation-guide.md @ version not confirmed
    - planning/documentation/documentation-update-workflow.md @ version not confirmed
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.6.0
  Internal dependencies:
    - Workflow Activation And Use-Case Navigation
  Not checked:
    - direct GitHub edit policy not re-audited beyond navigation wording
```

When documentation changes are small and well-scoped, prefer direct GitHub file edits from ChatGPT over manual replacement archives.

By default, use one commit per file. This makes each change easy to inspect and easy to revert.

Direct GitHub edits must still follow scope rules:

```text
- change only files explicitly included in the requested scope;
- do not rewrite unrelated planning documents;
- do not change code, generated artifacts or implementation files unless explicitly requested;
- keep commit messages specific;
- prefer one file per commit by default.
```

Mechanical multi-file link/path/name synchronization may use a single bundled commit when approved by the user and when no unrelated semantic refactors are mixed in.

## 3. Workflow Activation And Use-Case Navigation

```text
Sources:
  Format/process:
    - planning/workflow-activation-map.md @ Doc version: v0.1.0
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.1.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/workstreams/command-system-and-tampermonkey-goal-map.md @ version not confirmed
    - planning/workstreams/tampermonkey-command-projection-plan.md @ version not confirmed
    - tools/tampermonkey/README.md @ version not declared
  Internal dependencies:
    - Project-Wide Planning Profiles
  Not checked:
    - Goal Map/Tampermonkey files remain skeleton rows outside ROOT-SRC-2A
```

For non-trivial planning/repo work, read:

```text
planning/workflow-activation-map.md
```

The workflow activation map explains:

```text
- which workflows exist;
- when each workflow is activated;
- which workflows are implicit checks;
- which actions require explicit user permission;
- what Workflow Preflight should be shown before deep work or edits;
- which workflows are future/missing or transitional.
```

For action/use-case based navigation, read:

```text
planning/planning-use-case-map.md
```

The use-case map explains:

```text
- what to do when the user says a concrete action such as "draft slice", "update docs", "recheck", "арх" or "без изм";
- how to distinguish new work from active-context continuation;
- traversal depth: full / targeted / reuse / no traversal;
- read source mode: conversation / canvas / archive / GitHub / uploaded file;
- expected output and permission boundary for common use cases.
```

For a new chat, first planning/repo pass, restored context, or no reliable active context, start with the New Chat Onboarding use case in:

```text
planning/planning-use-case-map.md
```

Do not ask the user to name workflow files. Use the onboarding use case to choose the initial read path, then switch to the specific task/use-case route.

For root layer routing, read:

```text
planning/planning-doc-responsibility-map.md
```

A future chat should not require the user to remember workflow file names. It should read the activation map and use-case map, then disclose activated workflows before continuing.

### Long-running workstreams, Goal Maps and command-helper discovery

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
    - planning/workflow-activation-map.md @ Doc version: v0.1.0
  Content:
    - planning/goal-map-principles-workflow-template.md @ version not confirmed
    - planning/workstreams/command-system-and-tampermonkey-goal-map.md @ version not confirmed
    - planning/workstreams/tampermonkey-command-projection-plan.md @ version not confirmed
    - tools/tampermonkey/README.md @ version not declared
  Internal dependencies:
    - Workflow Activation And Use-Case Navigation
  Not checked:
    - Tampermonkey userscript implementation not audited in ROOT-SRC-2A
```

When a new chat, restored context or first planning pass touches a long-running workstream, command-system work or the Tampermonkey helper, also check:

```text
planning/goal-map-principles-workflow-template.md
planning/workstreams/command-system-and-tampermonkey-goal-map.md
planning/workstreams/tampermonkey-command-projection-plan.md
tools/tampermonkey/README.md
```

Use them to decide:

```text
- whether a living Goal Map exists and must be consulted before choosing the next step;
- whether the map's Current Snapshot, active slice, next action and evidence are stale;
- which Tampermonkey command bodies are only prompt projections;
- which root route/owner workflow files define the actual command behavior.
```

Tampermonkey inserted command bodies are not source of truth. They are route hints that tell the chat to start from `planning/planning-use-case-map.md` and then read linked owner workflows/examples before acting.

For deferred planning-docs/workflow cleanup tasks and condition-based follow-ups, use:

```text
planning/planning-maintenance-register.md
```

This register is for future documentation/workflow maintenance, not for ordinary feature TODOs.

## 4. Source-of-Truth Model

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.1.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planning/root-source-sync-register.md @ Doc version: v0.6.0
  Internal dependencies:
    - Core Rule
    - Project-Wide Planning Profiles
  Not checked:
    - slice source-sync register not created yet
```

### Project-wide profiles

Use these project-level profiles when the task touches status, shared visibility or source usage:

```text
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
```

These profiles provide Enman-specific answers. They do not replace the reusable documentation workflows.

### Implementation truth

Use:

```text
current Git branch
code
tests
migrations
OpenAPI / generated contracts
runtime screenshots
```

Do not use planning status files as proof that a feature is implemented.

### Scenario behavior truth

Use:

```text
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
```

Scenario source files are still physically under `planning/diagrams/` during migration. They are scenario sources, not generated diagram truth.

### Diagramming workflow truth

Use:

```text
planning/diagramming/README.md
planning/diagramming/diagramming-responsibility-map.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

Diagramming docs describe how to prepare, check and generate diagrams. Generated diagrams are not scenario/domain/slice source truth.

### Domain discovery and aggregate draft truth

Use:

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/domain-discovery-workflow.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/aggregate-drafting-workflow.md
planning/domain/value-object-drafting-workflow.md
```

For historical/cross-check source material, use:

```text
planning/tables/domain-drafts/
planning/tables/pre-domain-variants-input.md
```

Domain docs describe intended domain model and draft structure. They are not implementation proof.

### Slice scope truth

Use:

```text
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-test-plan-workflow.md
planning/slices/SLICE-INDEX.md
```

Slice docs describe intended work and scope. They are not implementation proof.

### Architecture, API, client and testing decisions

Use:

```text
planning/architecture/
planning/api/
planning/client/
planning/testing/
planning/adr/
```

### VKR / thesis clean wording

Use:

```text
planning/vkr-clean-reference.md
```

### Non-canonical recovery notes

Use only after canonical docs:

```text
planning/dirty-drafts/
```

Dirty drafts are not source of truth.

## 5. Task-Based Navigation

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
    - planning/workflow-activation-map.md @ Doc version: v0.1.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.1.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planning/root-source-sync-register.md @ Doc version: v0.6.0
  Internal dependencies:
    - Workflow Activation And Use-Case Navigation
    - Source-of-Truth Model
  Not checked:
    - full route-table audit for non-root layer docs outside ROOT-SRC-2A
```

For action-to-doc-flow routing, start with:

```text
planning/planning-use-case-map.md
```

Use the sections below as broad entry pointers. The use-case map owns detailed command/action traces.

### For implementation planning

Read:

```text
planning/README.md
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-doc-responsibility-map.md
relevant scenario spec
relevant slice doc
relevant architecture/API/client/testing doc
```

Then inspect the current branch when implementation status matters.

### For VKR / thesis writing

Read:

```text
planning/README.md
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/vkr-clean-reference.md
relevant scenario/domain/architecture docs
```

Then verify implementation from code, tests, screenshots or generated contracts when implementation evidence matters.

### For scenario work

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/diagrams/README.md
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-ui-specs/00-scenario-ui-specs-index.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/diagrams/scenario-clarifications/README.md
```

### For diagramming work

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-doc-responsibility-map.md
planning/diagramming/README.md
planning/diagramming/diagramming-responsibility-map.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

Then read source layers required by the requested diagram pages: scenarios, domain, slices, testing or thesis.

### For domain work

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-doc-responsibility-map.md
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/domain-discovery-workflow.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-modeling-principles.md
```

For aggregate drafting, also read:

```text
planning/domain/aggregate-drafting-workflow.md
planning/domain/aggregate-draft-template.md
```

For value object drafting, also read:

```text
planning/domain/value-object-drafting-workflow.md
planning/domain/value-object-draft-template.md
```

Use `planning/tables/domain-drafts/` only as historical/cross-check source material unless a task explicitly asks about old monolithic drafts.

### For slice work

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-doc-responsibility-map.md
planning/slices/README.md
planning/slices/slice-responsibility-map.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-test-plan-workflow.md
planning/slices/SLICE-INDEX.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Use side-specific workflows/templates after slice type is known:

```text
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/cross-cutting/cross-cutting-umbrella-template.md
```

### For backend cleanup / legacy boundary work

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/architecture/README.md
planning/architecture/backend-legacy-and-l1-boundaries.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
```

### For documentation-only work

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/documentation/README.md
planning/documentation/documentation-update-workflow.md
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/replacement-file-generation-guide.md
```

### For agent scope / prompt safety work

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-agent-protocol.md
planning/agent-scope-boundaries-and-prompt-safety.md
planning/planning-doc-responsibility-map.md
```

### For workflow/documentation maintenance tracking

Read:

```text
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-maintenance-register.md
planning/planning-doc-responsibility-map.md
```

Use the maintenance register for deferred planning-docs/workflow tasks that have trigger conditions.

### For dirty draft recovery

Read dirty drafts only after canonical docs:

```text
planning/dirty-drafts/README.md
```

Dirty drafts are recovery/context notes, not implementation truth and not final VKR wording.

### For root use-case map or command-routing work

The concrete Enman root use-case map is:

```text
planning/planning-use-case-map.md
```

Reusable setup/maintenance owners:

```text
planning/documentation/field-kits/root-use-case-map-field-kit.md
planning/documentation/use-case-map-workflow.md
planning/documentation/USE-CASE-MAP-TEMPLATE.md
```

For scenario/domain/slice route families, also use:

```text
planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
```

Do not create a second generic use-case map inside the documentation layer.

### For status, shared visibility or source usage work

Read the relevant active root profile before changing status-sensitive docs, shared registers or source usage/cascade material:

```text
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
```

Then use the active workflow/governance files:

```text
planning/documentation/status-reconciliation-workflow.md
planning/documentation/local-global-documentation-sync-workflow.md
planning/documentation/field-kits/source-usage-cascade-field-kit.md
planning/source-usage-cascade-profile.md
```

## 6. Historical / Internal Status Notes

```text
Sources:
  Format/process:
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
  Content:
    - planning/documentation-action-log.md @ version not confirmed / evidence trail
  Internal dependencies:
    - Source-of-Truth Model
  Not checked:
    - historical status notes not implementation proof
```

The following files may be useful as historical or internal handoff notes, but they are not implementation truth:

```text
planning/l1-current-implementation-status.md
planning/l2-current-planning-status.md
planning/planning-workflow-current.md
```

Use them only as context. For implementation state, inspect the current branch.

## 7. Internal Labels and VKR Wording

```text
Sources:
  Format/process:
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.1.0
  Content:
    - planning/vkr-clean-reference.md @ version not confirmed
  Internal dependencies:
    - Source-of-Truth Model
  Not checked:
    - VKR/thesis files not audited in ROOT-SRC-2A
```

`L1` and `L2` may appear in internal planning history, but they must not be used in VKR, presentation, report or defense speech.

Use clean terms instead:

| Internal label | Clean wording |
|---|---|
| L1 | client account, applicant, request creation, my requests |
| L2 | employee review and agreement exchange |
| Employee Review | request review by employee |
| Agreement Exchange | agreement/document exchange |
| AgreementDocumentRef | document reference and metadata |

## 8. Scenario / Diagram Status Markers

```text
Sources:
  Format/process:
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-artifact-map.md @ version not confirmed
    - planning/diagrams/scenario-diagram-consistency-report.md @ version not confirmed
  Internal dependencies:
    - Task-Based Navigation
  Not checked:
    - scenario/diagram layer source passes outside ROOT-SRC-2A
```

For post-L1/L2 scenario and diagram work, use:

```text
planning/diagrams/scenario-status-marker-rules.md
```

Scenario docs may mark future implementation and deferred extension points uniformly with:

```text
[PLANNED]
[DEFERRED]
[DESIGNED]
[QUESTION]
```

`[IMPLEMENTED]` still requires current repo evidence.

## 9. Not Source of Truth

```text
Sources:
  Format/process:
    - planning/status-evidence-profile.md @ version not confirmed
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
  Content:
    - planning/root-source-sync-register.md @ Doc version: v0.6.0
  Internal dependencies:
    - Core Rule
  Not checked:
    - current implementation status not checked in ROOT-SRC-2A
```

Do not use these as current implementation proof:

```text
dirty drafts
old current-status snapshots
implementation archives
chat/prompt notes
unverified planning text
```

Portable reusable documentation setup:

```text
planning/documentation/PORTABLE-STARTER-KIT.md
```

Read this only when copying/adapting the reusable documentation layer into a new project or documentation domain.

## 10. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/root-source-sync-register.md @ Doc version: v0.6.0
  Content:
    - planning/planning-use-case-map.md @ Doc version: v0.2.0
    - planning/workflow-activation-map.md @ Doc version: v0.1.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - Workflow Activation And Use-Case Navigation
    - Source-of-Truth Model
  Not checked:
    - Goal Map/Tampermonkey/output workflow source passes outside ROOT-SRC-2A
```

```text
- ROOT-SRC-2A added Doc version: v0.1.0 and local section-level Sources blocks to this root navigation entrypoint.
- ROOT-SRC-2A did not change navigation semantics or claim full root folder coverage.
```
