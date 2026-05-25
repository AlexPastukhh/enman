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

## 4. Maintenance Rules

```text
- Add entries when a task is intentionally deferred.
- Include the trigger/condition for when the task should be revisited.
- Do not use this as a dumping ground for local draft questions.
- If a task belongs to a layer-specific register, put it there instead and add only a root-level reminder here when cross-layer follow-up is needed.
- Mark entries done/superseded instead of deleting them when traceability is useful.
```
