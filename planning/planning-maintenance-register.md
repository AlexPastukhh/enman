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
  Keep planning/workflow-activation-map.md synchronized as local responsibility maps are added for scenario/domain/slice layers.
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
  Done: workflow activation was refreshed for the current slice responsibility map and slice-root rule model.
  Remaining: revisit after scenario/domain local maps exist.
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
Status: superseded
Area: slice workflow cleanup
Task / reminder:
  Audit planning/slices/implemented-slice-sync-workflow.md because it references source/domain registry files that may be legacy, future or missing.
Trigger / condition:
  Before using implemented-slice-sync-workflow.md as the canonical implemented slice synchronization workflow.
Why it matters:
  It contained useful sync logic, but did not match the current normalized docs architecture.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/implemented-slice-sync-workflow.md
Depends on:
  slice layer audit
Do when:
  Superseded by PMR-010.
Do not do before:
  n/a
Notes:
  The workflow references were modernized. Remaining cleanup is tracked by PMR-010.
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
  Add local responsibility maps or stable local responsibility sections for scenario and domain layers.
Trigger / condition:
  Before root map can be simplified and before source/version sync can become reliable.
Why it matters:
  Root routing is currently transitional. Local maps are needed to know exactly where questions, registers, source usage and drafts belong inside each layer.
Owner layer:
  documentation / scenario / domain
Target files:
  future scenario-responsibility-map.md or diagrams README section
  future domain-responsibility-map.md or tables/domain README section
Depends on:
  layer audits
Do when:
  Next layer cleanup phases.
Do not do before:
  Do not rename/move large folders before auditing current responsibility and stale files.
Notes:
  Slice responsibility map now exists. Scenario/domain maps remain future work. Domain/tables is important for thesis/domain architecture.
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
Status: open
Area: slice layer responsibility map
Task / reminder:
  Move or summarize remaining placement responsibility from planning/slices/SLICE-FOLDER-MAP.md into planning/slices/slice-responsibility-map.md, then decide whether SLICE-FOLDER-MAP.md remains a folder map, becomes a short compatibility pointer or is superseded.
Trigger / condition:
  Slice responsibility map exists. SLICE-FOLDER-MAP.md is now explicitly transitional.
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
  During the next slice-layer cleanup pass.
Do not do before:
  Do not delete SLICE-FOLDER-MAP.md before README/index links and legacy migration references are synchronized.
Notes:
  Current target: responsibility map owns placement rules; README owns read order; SLICE-INDEX owns concrete file catalog.
```

### PMR-009 — Update slice templates after responsibility/source model stabilizes

```text
ID: PMR-009
Status: open
Area: slice templates
Task / reminder:
  Finish template alignment after the slice responsibility/source model update.
Trigger / condition:
  Slice draft authoring principles and server implementation principles exist and are routed.
Why it matters:
  Templates should reflect the current model: source sync, scope/boundary, Behavior Coverage, Behavior-to-Test Trace with required assertions and local/global register awareness.
Owner layer:
  slice
Target files:
  planning/slices/cross-cutting/CROSS-CUTTING-UMBRELLA-TEMPLATE.md
Depends on:
  slice responsibility map, reviewable-agent-output workflow, slice draft authoring principles, server implementation principles
Do when:
  Review/update cross-cutting umbrella template after client/server template alignment.
Do not do before:
  Do not churn cross-cutting template before checking which parts of the new model apply to umbrella docs.
Notes:
  Done: SERVER-SLICE-TEMPLATE.md updated, CLIENT-SLICE-TEMPLATE.md updated, slice-test-plan-workflow.md updated with Required assertions. Remaining: review/update CROSS-CUTTING-UMBRELLA-TEMPLATE.md.
```

### PMR-010 — Refactor implemented slice sync workflow under the new model

```text
ID: PMR-010
Status: open
Area: implemented slice sync
Task / reminder:
  Continue refactoring implemented-slice-sync-workflow.md under the new source/current-code model.
Trigger / condition:
  Before implemented-slice sync is used broadly or before treating it as fully canonical.
Why it matters:
  The separate checklist/status/report helper artifacts were removed. Useful concepts now need to live inside implemented-slice-sync-workflow.md and slice-questions-register.md.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/implemented-slice-sync-workflow.md
  planning/slices/slice-questions-register.md
  planning/documentation/status-reconciliation-workflow.md
Depends on:
  slice responsibility map, source/version model, status reconciliation workflow, slice draft authoring principles
Do when:
  Before broad implemented-slice sync work or after source/version model stabilizes.
Do not do before:
  Do not recreate separate checklist/status/report helper artifacts unless a future workflow proves they are needed.
Notes:
  Done so far: deleted helper artifacts, consolidated useful decisions into slice-questions-register.md, modernized workflow references, added inline Implementation Sync Status and checklist. Remaining work: align workflow with future source/version model if/when that model is created.
```

### PMR-011 — Resolve duplicate slice questions registers

```text
ID: PMR-011
Status: done
Area: slice registers
Task / reminder:
  Resolve responsibility overlap between planning/slices/SLICE-QUESTIONS.md and planning/slices/slice-questions-register.md.
Trigger / condition:
  During slice responsibility map work or before adding new slice-wide questions.
Why it matters:
  Two files previously presented themselves as slice question registers.
Owner layer:
  slice
Target files:
  planning/slices/slice-questions-register.md
  planning/slices/slice-responsibility-map.md
  planning/slices/README.md
Depends on:
  slice layer audit
Do when:
  Done.
Do not do before:
  n/a
Notes:
  SLICE-QUESTIONS.md was removed. Useful content was consolidated into slice-questions-register.md. slice-questions-register.md is now the canonical active slice-layer questions/decisions register.
```

### PMR-012 — Decide scope of draft-driven discovery and L1 slice drafting guide

```text
ID: PMR-012
Status: open
Area: slice principles/workflows cleanup
Task / reminder:
  Decide whether l1-slice-drafting-guide.md still owns useful content after client/server workflows/templates exist.
Trigger / condition:
  After slice draft authoring principles, client/server templates and server workflow have been updated.
Why it matters:
  l1-slice-drafting-guide.md may now duplicate current authoring principles, client/server workflows or templates.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/l1-slice-drafting-guide.md
  planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
  planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
  planning/slices/slice-draft-authoring-principles.md
Depends on:
  slice layer audit, slice draft authoring principles, client/server template updates
Do when:
  During the next slice principles cleanup pass.
Do not do before:
  Do not archive l1-slice-drafting-guide.md until its useful short-draft guidance is migrated or explicitly superseded.
Notes:
  Done: draft-driven-discovery-principles.md narrowed to slice-layer discovery. Remaining: audit l1-slice-drafting-guide.md and decide whether client/server drafting workflows should remain local or move to slice root.
```

### PMR-013 — Audit client/server subfolder rules and move general rules to slice root if needed

```text
ID: PMR-013
Status: done
Area: slice subfolder responsibility cleanup
Task / reminder:
  Audit planning/slices/client/ and planning/slices/server/ for rules/principles that should live in the root slice layer instead of subfolders.
Trigger / condition:
  Before declaring the slice layer responsibility model clean.
Why it matters:
  Subfolders should mostly contain local workflows/templates/drafts. General slice principles should live in planning/slices/ root so agents can find them before choosing client/server-specific work.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/client/
  planning/slices/server/
  planning/slices/slice-responsibility-map.md
  planning/slices/README.md
  planning/slices/SLICE-INDEX.md
Depends on:
  slice draft authoring principles, server implementation principles, client/server template updates
Do when:
  Done.
Do not do before:
  n/a
Notes:
  Done: reusable server/client principles, CSS rules, form validation principles, accessibility principles and UI/style workflow were moved to the slice root. client/README.md and server/README.md are now thin local pointers. Remaining separate decision: whether CLIENT-SLICE-DRAFTING-WORKFLOW.md and SERVER-SLICE-DRAFTING-WORKFLOW.md should stay local or move to root; tracked by PMR-012.
```

## 4. Maintenance Rules

```text
- Add entries when a task is intentionally deferred.
- Include the trigger/condition for when the task should be revisited.
- Do not use this as a dumping ground for local draft questions.
- If a task belongs to a layer-specific register, put it there instead and add only a root-level reminder here when cross-layer follow-up is needed.
- Mark entries done/superseded instead of deleting them when traceability is useful.
```
