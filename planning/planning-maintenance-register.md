# Planning Maintenance Register

Status: current planning maintenance register  
Scope: deferred planning-docs, workflow and architecture maintenance tasks with explicit trigger conditions

## 1. Purpose

This register stores follow-up work that should not be done immediately, but must not be forgotten.

Use it for:

```text
- deferred planning-docs cleanup;
- workflow follow-ups;
- reminders to revisit a file after a condition is met;
- future refactors that depend on layer responsibility maps;
- future source/version sync work;
- known transitional files that should be audited before becoming canonical.
```

Do not use this register for ordinary product feature work, implementation TODOs or one-off local draft notes.

## 2. Entry Format

```text
ID:
Status:
Area:
Task / reminder:
Trigger / condition:
Why it matters:
Owner layer:
Target files:
Depends on:
Do when:
Do not do before:
Notes:
```

Statuses:

```text
open
blocked
waiting-for-condition
ready
in-progress
done
superseded
```

## 3. Current Entries

### PMR-001 — Update workflow activation map after local responsibility maps

```text
ID: PMR-001
Status: waiting-for-condition
Area: workflow activation
Task / reminder:
  Update planning/workflow-activation-map.md after local responsibility maps are added for scenario/domain/slice layers.
Trigger / condition:
  scenario-responsibility-map.md, domain-responsibility-map.md and slice-responsibility-map.md exist, or equivalent README sections are stable enough to use as local responsibility entries.
Why it matters:
  Workflow activation should point to local maps instead of relying on transitional root fallback routing.
Owner layer:
  documentation / workflow governance
Target files:
  planning/workflow-activation-map.md
  planning/planning-doc-responsibility-map.md
  relevant layer README files
Depends on:
  scenario/domain/slice responsibility map work
Do when:
  After at least scenario/domain/slice local routing has been stabilized.
Do not do before:
  Do not remove root fallback routing before equivalent local maps exist.
Notes:
  Keep the workflow activation map honest about future/missing workflows.
```

### PMR-002 — Create source/version cascade sync workflow after pilot

```text
ID: PMR-002
Status: waiting-for-condition
Area: source/version sync
Task / reminder:
  Design a source-version-cascade-sync workflow.
Trigger / condition:
  At least one source usage register pilot exists and scenario/domain/slice dependency model is audited.
Why it matters:
  The source/version principle exists, but full cascade sync is not implemented yet.
Owner layer:
  documentation / source architecture
Target files:
  future planning/documentation/source-version-cascade-sync-workflow.md or equivalent
  planning/documentation/planning-docs-architecture-principles.md
  source usage registers
Depends on:
  local responsibility maps, dependency audit, source usage register pilot
Do when:
  After the first real source usage register proves the shape of source/version data.
Do not do before:
  Do not create a full cascade workflow only from abstract assumptions.
Notes:
  Current architecture principles already describe source/version concepts; this entry is about the operational workflow.
```

### PMR-003 — Audit implemented slice sync workflow before treating it as canonical

```text
ID: PMR-003
Status: open
Area: slice workflow cleanup
Task / reminder:
  Audit planning/slices/implemented-slice-sync-workflow.md because it references source/domain registry files that may be legacy, future or missing.
Trigger / condition:
  Before using implemented-slice-sync-workflow.md as the canonical implemented slice synchronization workflow.
Why it matters:
  It contains useful sync logic, but may not match the current normalized docs architecture.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/implemented-slice-sync-workflow.md
  future slice responsibility map
  source usage register files, if created
Depends on:
  slice layer audit
Do when:
  Before implemented-slice sync work is applied broadly.
Do not do before:
  Do not treat old source registry filenames as current truth without checking existing files.
Notes:
  The workflow may remain useful as a reference even before refactor.
```

### PMR-004 — Shrink root responsibility map after local maps

```text
ID: PMR-004
Status: waiting-for-condition
Area: docs architecture cleanup
Task / reminder:
  Shrink planning/planning-doc-responsibility-map.md to a thin layer router.
Trigger / condition:
  Local responsibility maps or stable local responsibility sections exist for documentation, scenario, domain, slice, API/testing and VKR layers.
Why it matters:
  The root map is currently transitional and intentionally too detailed.
Owner layer:
  documentation / root navigation
Target files:
  planning/planning-doc-responsibility-map.md
  planning/README.md
Depends on:
  local responsibility maps for major layers
Do when:
  After useful fallback routing exists locally.
Do not do before:
  Do not remove useful routing rows until equivalent local routing exists.
Notes:
  This prevents loss of current cross-layer routing knowledge.
```

### PMR-005 — Update prompt manager rules after workflow activation map

```text
ID: PMR-005
Status: ready
Area: prompt manager
Task / reminder:
  Add a workflow-aware GitHub Quick Prompt to Prompt Manager Rules and Design.
Trigger / condition:
  planning/workflow-activation-map.md exists.
Why it matters:
  Prompt Manager should help chats discover repo workflows without embedding long workflow rules in every prompt.
Owner layer:
  prompt manager / documentation
Target files:
  Prompt Manager Rules and Design canvas or exported prompt-manager docs
Depends on:
  planning/workflow-activation-map.md
Do when:
  During the next prompt manager rules update.
Do not do before:
  Do not duplicate full workflow registry inside the prompt manager prompt.
Notes:
  The quick prompt should tell the chat to read repo docs and output Workflow Preflight.
```

### PMR-006 — Add local responsibility maps for scenario/domain/slice layers

```text
ID: PMR-006
Status: open
Area: local responsibility maps
Task / reminder:
  Add local responsibility maps or stable local responsibility sections for scenario, domain and slice layers.
Trigger / condition:
  Before root map can be simplified and before source/version sync can become reliable.
Why it matters:
  Root routing is currently transitional. Local maps are needed to know exactly where questions, registers, source usage and drafts belong inside each layer.
Owner layer:
  documentation / scenario / domain / slice
Target files:
  future scenario-responsibility-map.md or diagrams README section
  future domain-responsibility-map.md or tables/domain README section
  future slice-responsibility-map.md or slices README section
Depends on:
  layer audits
Do when:
  Next layer cleanup phases.
Do not do before:
  Do not rename/move large folders before auditing current responsibility and stale files.
Notes:
  Slice is likely the highest operational cleanup value; domain/tables is important for thesis/domain architecture.
```

### PMR-007 — Add archive/local-state availability rule

```text
ID: PMR-007
Status: open
Area: workflow activation / source availability
Task / reminder:
  Add a rule that chats must ask for an archive or confirm pushed state when the task depends on local files, archive-applied changes, generated replacement packages or unpushed changes that may not be visible on the remote branch.
Trigger / condition:
  Before relying on local archive state, before auditing files that may exist only in uploaded archives, or when repo search/remote branch does not show expected files.
Why it matters:
  Future chats can otherwise overclaim current state from the remote branch and miss local/archive-only changes.
Owner layer:
  documentation / workflow governance
Target files:
  planning/workflow-activation-map.md
  planning/planning-agent-protocol.md
  Prompt Manager Rules and Design, if quick prompts are updated
Depends on:
  none
Do when:
  Next workflow-activation cleanup or prompt-manager quick prompt update.
Do not do before:
  Do not make archives mandatory for every task; only require them when local/archive state matters.
Notes:
  Rule should distinguish remote branch truth, uploaded archive snapshot and local unpushed state.
```

### PMR-008 — Fold slice folder placement rules into slice responsibility map

```text
ID: PMR-008
Status: waiting-for-condition
Area: slice layer responsibility map
Task / reminder:
  Move or summarize the placement responsibility from planning/slices/SLICE-FOLDER-MAP.md into future planning/slices/slice-responsibility-map.md, then decide whether SLICE-FOLDER-MAP.md remains a folder map, becomes a short compatibility pointer or is superseded.
Trigger / condition:
  When creating or updating planning/slices/slice-responsibility-map.md.
Why it matters:
  Responsibility maps should explain where new information/files belong. Folder placement is part of that responsibility and should not live only in a separate competing map.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/slice-responsibility-map.md
  planning/slices/SLICE-FOLDER-MAP.md
  planning/slices/README.md
  planning/slices/SLICE-INDEX.md
Depends on:
  slice layer responsibility map creation
Do when:
  During slice responsibility map implementation.
Do not do before:
  Do not delete SLICE-FOLDER-MAP.md before README/index links and legacy migration references are synchronized.
Notes:
  Likely target: responsibility map owns placement rules; README owns read order; SLICE-INDEX owns concrete file catalog.
```

### PMR-009 — Update slice templates after responsibility/source model stabilizes

```text
ID: PMR-009
Status: waiting-for-condition
Area: slice templates
Task / reminder:
  Update client/server/cross-cutting slice templates after the slice responsibility map and source/dependency expectations are clear.
Trigger / condition:
  After planning/slices/slice-responsibility-map.md exists and section/source/dependency rules are stable.
Why it matters:
  Templates should eventually include the new model: section-level sources where needed, dependencies between draft sections, local/global register sync, and future source/version tracking hooks.
Owner layer:
  slice
Target files:
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md
  planning/slices/server/SERVER-SLICE-TEMPLATE.md
  planning/slices/cross-cutting/CROSS-CUTTING-UMBRELLA-TEMPLATE.md
Depends on:
  slice responsibility map, reviewable-agent-output workflow, future source/dependency model
Do when:
  After map/responsibility cleanup, not during the first inventory pass.
Do not do before:
  Do not churn templates before owner/register boundaries are clear.
Notes:
  Keep existing templates as current until this cleanup is explicitly started.
```

### PMR-010 — Refactor implemented slice sync artifacts into the new model

```text
ID: PMR-010
Status: waiting-for-condition
Area: implemented slice sync
Task / reminder:
  Refactor implemented-slice-sync-workflow.md and decide the fate of IMPLEMENTED-SLICE-SYNC-CHECKLIST.md, IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md and IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md.
Trigger / condition:
  Before implemented-slice sync is used broadly or after slice responsibility map clarifies workflow/template/checklist responsibilities.
Why it matters:
  The current workflow has useful ideas but references source/domain/map files that may be legacy/future/missing. The checklist/templates may be old artifacts or may need to be folded into the workflow and reviewable output model.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/implemented-slice-sync-workflow.md
  planning/slices/IMPLEMENTED-SLICE-SYNC-CHECKLIST.md
  planning/slices/IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md
  planning/slices/IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md
  planning/documentation/status-reconciliation-workflow.md
Depends on:
  slice responsibility map, source/version model, status reconciliation workflow
Do when:
  Before treating implemented slice sync artifacts as canonical.
Do not do before:
  Do not remove useful sync/report/status concepts without migrating them into the new workflow/template model.
Notes:
  Likely useful parts: compare source/draft/code/tests, drift statuses, report-before-edit, implementation sync status block.
```

### PMR-011 — Resolve duplicate slice questions registers

```text
ID: PMR-011
Status: open
Area: slice registers
Task / reminder:
  Resolve responsibility overlap between planning/slices/SLICE-QUESTIONS.md and planning/slices/slice-questions-register.md.
Trigger / condition:
  During slice responsibility map work or before adding new slice-wide questions.
Why it matters:
  Two files currently present themselves as slice question registers, which makes it unclear where new questions/decisions belong.
Owner layer:
  slice
Target files:
  planning/slices/SLICE-QUESTIONS.md
  planning/slices/slice-questions-register.md
  planning/slices/slice-responsibility-map.md
  planning/slices/README.md
Depends on:
  slice layer audit
Do when:
  Before normalizing register routing in the slice responsibility map.
Do not do before:
  Do not delete either file until useful entries are classified as active/current, legacy, docs-governance or superseded.
Notes:
  Likely target: slice-questions-register.md becomes canonical active register; SLICE-QUESTIONS.md becomes transitional legacy/taxonomy decision log or is merged/superseded.
```

### PMR-012 — Decide scope of draft-driven discovery and L1 slice drafting guide

```text
ID: PMR-012
Status: open
Area: slice principles/workflows cleanup
Task / reminder:
  Decide whether draft-driven-discovery-principles.md is a slice-layer principle or a broader planning principle, and decide whether l1-slice-drafting-guide.md still owns useful content after client/server workflows/templates exist.
Trigger / condition:
  During slice responsibility map work or before rewriting slice drafting workflows.
Why it matters:
  Some files currently use broader wording than the slice layer or duplicate client/server workflow/template content.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/draft-driven-discovery-principles.md
  planning/slices/l1-slice-drafting-guide.md
  planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
  planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
  planning/slices/slice-responsibility-map.md
Depends on:
  slice layer audit
Do when:
  Before declaring slice workflow/principle files clean.
Do not do before:
  Do not archive l1-slice-drafting-guide.md until its useful short-draft guidance is migrated or explicitly superseded.
Notes:
  Candidate target: draft-driven discovery becomes either a broader documentation/planning principle or is narrowed to slice-layer discovery; l1 guide becomes transitional or a short-draft guide only.
```

## 4. Maintenance Rules

```text
- Add entries when a task is intentionally deferred.
- Include the trigger/condition for when the task should be revisited.
- Do not use this as a dumping ground for local draft questions.
- If a task belongs to a layer-specific register, put it there instead and add only a root-level reminder here when cross-layer follow-up is needed.
- Mark entries done/superseded instead of deleting them when traceability is useful.
```
