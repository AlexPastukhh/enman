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

This register is not the documentation action log.

Use:

```text
planning/planning-maintenance-register.md
  = goals, reminders, deferred work, waiting-for-condition tasks and follow-ups that still need action.

planning/documentation/documentation-action-log.md
  = significant completed logical documentation actions and short explanations of why they happened.
```

Not every logical documentation action creates a PMR entry.

If a logged action creates future work, add or update a PMR entry and reference that PMR relation from the action log.

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
Status: done
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
  Done for current scenario/domain/slice routing model.
Do not do before:
  n/a
Notes:
  Workflow activation now routes through scenario responsibility/artifact maps, domain responsibility/workflows and slice responsibility/workflows. Reopen only if a new major layer map is introduced.
```

### PMR-002 — Create source usage cascade workflow after pilot

```text
ID: PMR-002
Status: waiting-for-condition
Area: source usage / cascade / layer encapsulation
Task / reminder:
  Design the full source usage cascade workflow after a real pilot proves the relationship model.
Trigger / condition:
  SC-13D source usage pilot has real audited rows, scenario/domain/slice dependency model is audited, and at least one cascade review simulation is performed.
Why it matters:
  The goal is not file versioning by itself. The goal is to preserve layer encapsulation, avoid duplicated source truth and let downstream work reference already-reviewed upstream artifacts without forcing the user to re-review reconstructed reasoning.
Owner layer:
  documentation / source architecture
Target files:
  future planning/documentation/source-version-cascade-sync-workflow.md or renamed equivalent
  planning/documentation/source-usage-cascade-governance-plan.md
  source usage registers / permanent register location after pilot
Depends on:
  source usage cascade governance plan, SC-13D pilot fill, dependency audit, cascade review simulation
Do when:
  After the first real source usage register proves the row shape and review process.
Do not do before:
  Do not create a full cascade workflow only from abstract assumptions.
Notes:
  Governance plan and pilot skeleton exist. Full workflow is still not created.
```
### PMR-003 — Audit implemented slice sync workflow before treating it as canonical

```text
ID: PMR-003
Status: superseded
Area: slice workflow cleanup
Task / reminder:
  Audit the old implemented slice sync workflow before treating it as canonical.
Trigger / condition:
  Before using implemented-slice synchronization as a broad separate workflow.
Why it matters:
  The old workflow did not match the normalized docs architecture.
Owner layer:
  slice / documentation governance
Target files:
  planning/documentation/status-reconciliation-workflow.md
  planning/slices/slice-draft-authoring-workflow.md
Depends on:
  slice layer audit
Do when:
  Superseded by PMR-010.
Do not do before:
  n/a
Notes:
  The old separate workflow was removed. Current implementation/status evidence should use status reconciliation plus the runtime implementation checked fields in slice templates.
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
Status: done
Area: local responsibility maps
Task / reminder:
  Add local responsibility maps or stable local responsibility sections for scenario and domain layers.
Trigger / condition:
  Before root map can be simplified and before source/version sync can become reliable.
Why it matters:
  Root routing was transitional. Local maps are needed to know exactly where questions, registers, source usage and drafts belong inside each layer.
Owner layer:
  documentation / scenario / domain
Target files:
  planning/diagrams/scenario-responsibility-map.md
  planning/domain/domain-responsibility-map.md
  planning/slices/slice-responsibility-map.md
Depends on:
  layer audits
Do when:
  Done for current scope.
Do not do before:
  n/a
Notes:
  Scenario, domain and slice responsibility maps now exist. Future cleanup is PMR-004 root-map shrink and source/version/cascade alignment.
```

### PMR-007 — Add archive/local-state availability rule

```text
ID: PMR-007
Status: superseded
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
  planning/documentation/reviewable-agent-output-and-commands-workflow.md
Depends on:
  none
Do when:
  Superseded by current response command model.
Do not do before:
  n/a
Notes:
  Added Use Archive / Archive Source command to reviewable-agent-output-and-commands-workflow.md and workflow-activation-map.md. It distinguishes archive snapshot evidence from remote/current proof, and now supports short aliases such as арх and б из арх.
```

### PMR-008 — Fold slice folder placement rules into slice responsibility map

```text
ID: PMR-008
Status: done
Area: slice layer responsibility map
Task / reminder:
  Move or summarize remaining placement responsibility from the old slice folder map into planning/slices/slice-responsibility-map.md, then remove the competing folder map.
Trigger / condition:
  Slice responsibility map exists.
Why it matters:
  Responsibility maps should explain where new information/files belong. Folder placement should not live in a competing map.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/slice-responsibility-map.md
  planning/slices/README.md
  planning/slices/SLICE-INDEX.md
Depends on:
  slice layer responsibility map creation
Do when:
  Done.
Do not do before:
  n/a
Notes:
  Done: slice-responsibility-map.md is now the canonical placement/routing owner; the separate folder map was removed; README/index were updated.
```

### PMR-009 — Update slice templates after responsibility/source model stabilizes

```text
ID: PMR-009
Status: done
Area: slice templates
Task / reminder:
  Finish template alignment after the slice responsibility/source model update.
Trigger / condition:
  Slice draft authoring principles, slice draft authoring workflow and server/client implementation principles exist and are routed.
Why it matters:
  Templates should reflect the current model: source sync, scope/boundary, Behavior Coverage, Behavior-to-Test Trace with required assertions and local/global register awareness.
Owner layer:
  slice
Target files:
  planning/slices/server/SERVER-SLICE-TEMPLATE.md
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md
  planning/slices/cross-cutting/cross-cutting-umbrella-template.md
Depends on:
  slice responsibility map, reviewable-agent-output workflow, slice draft authoring principles, server implementation principles, slice draft authoring workflow
Do when:
  Done.
Do not do before:
  n/a
Notes:
  Done: server template, client template and cross-cutting umbrella template now include source sync, boundary, Behavior Coverage, Behavior-to-Test Trace and local/global sync sections.
```

### PMR-010 — Refactor implemented slice sync workflow under the new model

```text
ID: PMR-010
Status: superseded
Area: implemented slice sync
Task / reminder:
  Refactor the old separate implemented-slice sync workflow under the new source/current-code model.
Trigger / condition:
  Before using implemented-slice sync broadly.
Why it matters:
  Current implementation evidence should not be claimed without checking code/tests/generated artifacts, but a separate transitional workflow was no longer the clean owner.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/slice-draft-authoring-workflow.md
  planning/documentation/status-reconciliation-workflow.md
  planning/slices/server/SERVER-SLICE-TEMPLATE.md
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md
  planning/slices/cross-cutting/cross-cutting-umbrella-template.md
Depends on:
  slice responsibility map, status reconciliation workflow, slice draft authoring principles
Do when:
  Superseded by current model.
Do not do before:
  n/a
Notes:
  The separate implemented sync workflow was removed. Use slice-draft-authoring-workflow.md for draft refactor process, status-reconciliation-workflow.md for current implementation/status evidence, and runtime implementation checked fields in templates.
```

### PMR-011 — Resolve duplicate slice questions registers

```text
ID: PMR-011
Status: done
Area: slice registers
Task / reminder:
  Resolve responsibility overlap between the old slice questions file and planning/slices/slice-questions-register.md.
Trigger / condition:
  During slice responsibility map work or before adding new slice-wide questions.
Why it matters:
  Two files previously presented themselves as slice question registers.
Owner layer:
  slice
Target files:
  planning/slices/slice-questions-register.md
  planning/slices/slice-responsibility-map.md
Depends on:
  slice layer audit
Do when:
  Done.
Do not do before:
  n/a
Notes:
  The duplicate questions file was removed. Useful content was consolidated into slice-questions-register.md. slice-questions-register.md is now the canonical active slice-layer questions/decisions register.
```

### PMR-012 — Decide scope of draft-driven discovery and legacy L1 slice drafting guide

```text
ID: PMR-012
Status: done
Area: slice principles/workflows cleanup
Task / reminder:
  Decide whether the old legacy L1 slice drafting guide still owns useful content after root authoring workflow, client/server workflows and templates exist.
Trigger / condition:
  After slice draft authoring principles, slice-draft-authoring-workflow.md, client/server templates and client/server workflows have been updated.
Why it matters:
  The old guide duplicated current authoring principles, root authoring workflow, client/server workflows or templates.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/slice-draft-authoring-principles.md
  planning/slices/slice-draft-authoring-workflow.md
  planning/slices/server/SERVER-SLICE-TEMPLATE.md
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md
  planning/slices/cross-cutting/cross-cutting-umbrella-template.md
Depends on:
  slice layer audit, slice draft authoring principles, slice draft authoring workflow, client/server template updates
Do when:
  Done.
Do not do before:
  n/a
Notes:
  Done: draft-driven-discovery-principles.md narrowed to slice-layer discovery; slice-draft-authoring-workflow.md created and linked; legacy L1 guide was removed; target forms now live in server/client/cross-cutting templates. Remaining server/client/cross-cutting workflow split/audit is tracked by PMR-014.
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
  Done: reusable server/client principles, CSS rules, form validation principles, accessibility principles and UI/style workflow were moved to the slice root. client/README.md and server/README.md are now thin local pointers.
```

### PMR-014 — Audit server/client/cross-cutting drafting workflow split after source model stabilizes

```text
ID: PMR-014
Status: open
Area: slice drafting workflows
Task / reminder:
  Audit whether slice drafting should be split into server, client and cross-cutting drafting workflow files, and align those workflows with the current target templates.
Trigger / condition:
  After section-level source rules and source/dependency model are stable enough, or after one real server/client/cross-cutting draft refactor proves the needed algorithm differences.
Why it matters:
  There are separate target templates for server, client and cross-cutting umbrella drafts. The workflow layer may also need side-specific algorithms, but splitting too early can create duplicated or conflicting process docs before source usage rules are settled.
Owner layer:
  slice / documentation governance
Target files:
  planning/slices/slice-draft-authoring-workflow.md
  planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
  planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
  future planning/slices/cross-cutting/cross-cutting-umbrella-drafting-workflow.md, if needed
  planning/slices/slice-responsibility-map.md
  planning/slices/SLICE-INDEX.md
Depends on:
  section-level source model, source/version model decisions, at least one real draft refactor using current templates
Do when:
  After source usage rules are clearer or after a real draft refactor exposes repeated side-specific algorithm steps.
Do not do before:
  Do not split the root workflow into three competing workflows while section-level source ownership and source/version cascade are still unsettled.
Notes:
  Current direction: keep one root slice-draft-authoring-workflow.md plus local server/client drafting workflows for now. Later decide whether cross-cutting also needs its own drafting workflow file.
```

### PMR-015 — Audit slice test plan workflow after testing layer review

```text
ID: PMR-015
Status: open
Area: slice testing workflow
Task / reminder:
  Audit planning/slices/slice-test-plan-workflow.md after testing-layer principles are reviewed, and decide whether it should remain unified or be split into server/client/cross-side testing workflows.
Trigger / condition:
  After planning/testing/ principles and workflows are audited, or after server/client/cross-cutting slice draft refactors show repeated testing-specific divergence.
Why it matters:
  The current unified slice test plan workflow is useful and avoids premature fragmentation. But server/API proof, client UI proof and cross-side concern proof may eventually need clearer side-specific testing guidance.
Owner layer:
  slice / testing / documentation governance
Target files:
  planning/slices/slice-test-plan-workflow.md
  planning/testing/
  planning/slices/server/SERVER-SLICE-TEMPLATE.md
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md
  planning/slices/cross-cutting/cross-cutting-umbrella-template.md
Depends on:
  testing layer audit, current template usage, real slice refactor evidence
Do when:
  During testing layer cleanup or after a few slice drafts expose repeated testing workflow gaps.
Do not do before:
  Do not split the test workflow before auditing planning/testing/ and current test principles.
Notes:
  Current direction: keep slice-test-plan-workflow.md unified for now. Record server/client/cross-side differences inside the workflow and templates until testing architecture is reviewed.
```

### PMR-016 — Decide whether to split reviewable output templates from response commands

```text
ID: PMR-016
Status: open
Area: response commands / answer templates
Task / reminder:
  Decide whether reviewable output templates should be split from response-level command handling.
Trigger / condition:
  After response-command vocabulary stabilizes, or if reviewable-agent-output-and-commands-workflow.md becomes too large/confusing.
Why it matters:
  The current file intentionally owns both answer format and response commands. A future split may make responsibilities cleaner: one template-like file for Level 1/2/3 answer shapes and one workflow for command handling.
Owner layer:
  documentation / workflow governance
Target files:
  planning/documentation/reviewable-agent-output-and-commands-workflow.md
  future planning/documentation/reviewable-agent-output-template.md, if needed
  future planning/documentation/response-command-workflow.md, if needed
  planning/workflow-activation-map.md
  planning/documentation/documentation-responsibility-map.md
Depends on:
  response-command vocabulary stability, actual usage feedback
Do when:
  Later, only if the combined file becomes hard to navigate or commands grow beyond answer-format concerns.
Do not do before:
  Do not split immediately while command vocabulary is still being adjusted.
Notes:
  Current direction: keep one combined file for discoverability. It now owns Level 1/2/3 answer formats plus recheck, clarify, expand, keep prev, no ch/без изм, use archive/арх, б из арх, active draft/update commands, Source Delta and section commands. planning-use-case-map.md now owns action traces, active context, traversal depth and read source mode.
```

### PMR-017 — Keep planning use-case map routed and current

```text
ID: PMR-017
Status: done
Area: planning use-case navigation
Task / reminder:
  Create and route a root planning use-case map for user-action to doc/workflow/template/source paths.
Trigger / condition:
  Done during planning-docs use-case routing update.
Why it matters:
  README files provide overview/read order, workflow-activation-map.md chooses workflows, and responsibility maps choose ownership. A separate use-case map is needed to show what a chat should do when the user gives a concrete action or short repeated command.
Owner layer:
  documentation / workflow governance
Target files:
  planning/planning-use-case-map.md
  planning/README.md
  planning/workflow-activation-map.md
  planning/planning-doc-responsibility-map.md
  planning/documentation/reviewable-agent-output-and-commands-workflow.md
Depends on:
  workflow activation map, root responsibility map, reviewable output/commands workflow
Do when:
  Done.
Do not do before:
  n/a
Notes:
  Done: planning-use-case-map.md was created and routed from README, workflow activation map and root responsibility map. It covers active context, traversal depth, read source mode, Source Delta and permission boundaries.
```

### PMR-018 — Audit remaining legacy planning workflow references

```text
ID: PMR-018
Status: open
Area: stale link cleanup
Task / reminder:
  Audit remaining references to legacy/superseded planning workflow files and classify whether each reference is current-route, historical note or removed/superseded artifact.
Trigger / condition:
  After planning-use-case-map.md routing and slice authoring workflow link cleanup.
Why it matters:
  Some old names may remain valid only in historical/removed-artifact notes. They should not appear as current read/workflow/template routes.
Owner layer:
  documentation / workflow governance
Target files:
  planning/README.md
  planning/workflow-activation-map.md
  planning/planning-doc-responsibility-map.md
  planning/slices/slice-draft-authoring-workflow.md
  planning/slices/slice-responsibility-map.md
  other files found by search
Depends on:
  current use-case routing update
Do when:
  Next targeted stale-link cleanup pass.
Do not do before:
  Do not remove historical references if they are clearly marked as superseded/removed artifacts.
Notes:
  Search terms: l1-slice-drafting-guide.md, implemented-slice-sync-workflow.md, CROSS-CUTTING-UMBRELLA-TEMPLATE.md, reviewable-agent-output-workflow.md, planning/slices/l2/README.md.
```

### PMR-019 — Align domain workflows after source/version/cascade model

```text
ID: PMR-019
Status: waiting-for-condition
Area: domain documentation / source-version alignment
Task / reminder:
  Align domain discovery, aggregate drafting, value object drafting, scenario-to-aggregate map and domain templates with the source/version/cascade model after that model stabilizes.
Trigger / condition:
  After source/version/cascade pilot exists and scenario/domain/slice dependency model is audited.
Why it matters:
  The new domain layer starts with aggregate-based structure, but source metadata may need to change after source/version tracking becomes current.
Owner layer:
  domain / documentation governance
Target files:
  planning/domain/domain-discovery-workflow.md
  planning/domain/aggregate-drafting-workflow.md
  planning/domain/value-object-drafting-workflow.md
  planning/domain/scenario-to-aggregate-map.md
  planning/domain/aggregate-draft-template.md
  planning/domain/value-object-draft-template.md
Depends on:
  source/version/cascade model, domain scaffold, scenario/domain/slice dependency audit
Do when:
  After source/version/cascade model is introduced and at least one domain aggregate extraction has been tested.
Do not do before:
  Do not overfit domain source metadata before the source/version/cascade model is stable.
Notes:
  Current scaffold intentionally uses simple source sections and should be revisited later.
```

### PMR-020 — Finish scenario stale source and diagram report cleanup

```text
ID: PMR-020
Status: done-current-scope
Area: scenario documentation / stale source cleanup
Task / reminder:
  After scenario responsibility/artifact maps are accepted, finish cleanup of stale scenario variants, deprecated validation addendum references and scenario diagram consistency report wording.
Trigger / condition:
  scenario-responsibility-map.md and scenario-artifact-map.md exist and have been used for at least one scenario cleanup or downstream draft.
Why it matters:
  Scenario layer now has current routing, but old scenario variants and diagram reports can otherwise look like current sources.
Owner layer:
  scenario / documentation governance
Target files:
  planning/diagrams/scenario-artifact-map.md
  planning/diagrams/scenario-diagram-consistency-report.md
  planning/diagrams/diagram-prompt-generation-workflow.md
  planning/diagrams/drawio-diagram-generation-workflow.md
Depends on:
  scenario responsibility/artifact map scaffold
Do when:
  Done for current safe scope.
Do not do before:
  Do not delete or move old scenario variants until currentness and downstream references are checked.
Notes:
  Deprecated global validation addendum is now treated as historical context only. Diagram workflows now route through scenario-responsibility-map.md and scenario-artifact-map.md. Physical moves/renames remain out of scope.
```

### PMR-021 — Review behavior item taxonomy for downstream drafting

```text
ID: PMR-021
Status: waiting-for-condition
Area: scenario behavior item taxonomy
Task / reminder:
  Review behavior item categories and fields after scenario/domain/slice/testing layers stabilize.
Trigger / condition:
  After at least one scenario-to-domain and scenario-to-slice flow has been tested with current docs.
Why it matters:
  Behavior items should be useful for domain aggregate drafting, value object drafting, slice drafting, client sidecars and testing without becoming implementation details.
Owner layer:
  scenario / documentation governance
Target files:
  planning/diagrams/scenario-behavior-items/README.md
  planning/diagrams/scenario-drafting-workflow.md
  planning/domain/domain-discovery-workflow.md
  planning/slices/slice-draft-authoring-workflow.md
Depends on:
  scenario/domain/slice/testing layer stabilization
Do when:
  After current scenario source model is used on real updates.
Do not do before:
  Do not overfit taxonomy before testing layer and source/version/cascade rules are clearer.
Notes:
  Review CMD / LC / IBS / VI / UCQ / READ / INT / FUT / NW plus UI/UX projection notes and UI-only behavior entries.
```

### PMR-022 — Add source/version/cascade rules for scenario artifacts

```text
ID: PMR-022
Status: waiting-for-condition
Area: scenario source/version/cascade
Task / reminder:
  Add explicit cascade rules for core scenario, DATA, UI scenario, behavior item and scenario-artifact-map changes.
Trigger / condition:
  After source/version/cascade model is introduced and scenario/domain/slice dependency model is audited.
Why it matters:
  Core scenario, DATA, UI presentation and behavior items are linked but have different ownership and downstream impact.
Owner layer:
  documentation / scenario governance
Target files:
  future source/version/cascade workflow
  planning/diagrams/scenario-drafting-workflow.md
  planning/diagrams/scenario-artifact-map.md
  planning/domain/scenario-to-aggregate-map.md
  planning/slices/slice-scenario-flow-behavior-register.md
Depends on:
  source/version/cascade model
Do when:
  After source/version/cascade pilot stabilizes.
Do not do before:
  Do not create the full cascade workflow only from abstract assumptions.
Notes:
  UI scenario changes should check consistency with core scenario and DATA, but must not directly change business scope without an accepted decision.
```

### PMR-023 — Decide physical diagram-layer separation

```text
ID: PMR-023
Status: done-conceptual-layer / physical move deferred
Area: diagram workflow placement
Task / reminder:
  Create a separate diagramming responsibility layer and decide whether diagram prompt/workflow/artifacts should remain under planning/diagrams/ or move to a separate diagramming layer/folder.
Trigger / condition:
  After scenario source model stabilizes and before major diagram workflow refactors.
Why it matters:
  planning/diagrams/ currently contains both scenario source files and diagram-generation workflow/artifact files. Responsibilities are conceptually separate even if physical files stay together for now.
Owner layer:
  documentation / diagram governance
Target files:
  planning/diagramming/README.md
  planning/diagramming/diagramming-responsibility-map.md
  planning/diagrams/diagram-prompt-generation-workflow.md
  planning/diagrams/drawio-diagram-generation-workflow.md
  planning/diagrams/scenario-diagram-consistency-report.md
Depends on:
  scenario layer stabilization
Do when:
  Conceptual layer is done. Physical move should be handled by a dedicated mechanical move/link-update package.
Do not do before:
  Do not move files before current links and artifact references are audited.
Notes:
  Current package creates planning/diagramming/ as the responsibility/routing layer. It does not physically move diagram files.
```

### PMR-024 — Reclassify existing scenario DATA sidecars

```text
ID: PMR-024
Status: waiting-for-condition
Area: scenario documentation / DATA migration
Task / reminder:
  Review existing planning/diagrams/scenario-data/*.md files and decide for each whether it should be merged into core scenario text, kept as a reusable DATA concept, kept as a transitional sidecar, split into reusable concepts or marked historical.
Trigger / condition:
  After SCENARIO-TEXT-SPEC-TEMPLATE.md exists and inline DATA policy is current.
Why it matters:
  Scenario DATA now starts inline in core/business scenario text specs by default. Existing DATA sidecars should not be deleted or rewritten blindly; they need reclassification.
Owner layer:
  scenario documentation
Target files:
  planning/diagrams/scenario-data/
  planning/diagrams/scenario-text-specs/
  planning/diagrams/scenario-artifact-map.md
  planning/diagrams/scenario-behavior-items/
Depends on:
  scenario inline DATA policy and template
Do when:
  During a dedicated scenario DATA content migration audit.
Do not do before:
  Do not mass-edit concrete scenario specs or DATA files in policy/template batches.
Notes:
  Possible outcomes per file: merge into scenario text, keep as reusable DATA concept, keep transitional, split into concepts, mark historical.
```

### PMR-025 — Decide screenshot/evidence placement

```text
ID: PMR-025
Status: waiting-for-condition
Area: testing / VKR evidence / screenshot tooling
Task / reminder:
  Decide where screenshot runner and reproducible screenshot evidence docs belong.
Trigger / condition:
  After testing responsibility map exists and diagramming/VKR evidence responsibilities are clearer.
Why it matters:
  planning/testing/playwright-e2e-and-screenshot-plan.md mixes E2E regression testing and VKR screenshot/evidence generation. Screenshot evidence is related to Playwright tooling, but it is not ordinary Behavior-to-Test proof.
Owner layer:
  testing / thesis / documentation governance
Target files:
  planning/testing/playwright-e2e-and-screenshot-plan.md
  planning/testing/testing-responsibility-map.md
  planning/thesis/
  future diagramming/evidence docs if created
Depends on:
  testing responsibility map and diagramming/thesis evidence routing
Do when:
  Before moving screenshot runner docs or treating screenshots as a default testing workflow.
Do not do before:
  Do not mix screenshot runner placement cleanup into ordinary testing responsibility-map work.
Notes:
  Current testing docs should warn that screenshot runner/evidence is not ordinary slice behavior proof.
```

### PMR-026 — Decide client/component testing workflow need

```text
ID: PMR-026
Status: waiting-for-condition
Area: testing / client
Task / reminder:
  Decide whether planning/testing needs a dedicated client/component testing workflow.
Trigger / condition:
  After client slice drafting stabilizes and at least one client-side Test / Verification Plan needs more detail than testing-principles.md and test-object-patterns.md provide.
Why it matters:
  testing-principles.md defines client/component boundaries, but there is no dedicated workflow parallel to server-slice-test-plan-rules.md.
Owner layer:
  testing / client slice planning
Target files:
  future planning/testing/client-component-testing-workflow.md, if needed
  planning/testing/testing-principles.md
  planning/testing/test-object-patterns.md
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md
Depends on:
  client slice drafting stabilization and real client test-plan usage
Do when:
  When real client drafts show repeated uncertainty.
Do not do before:
  Do not create speculative client testing workflow before there is concrete pressure.
Notes:
  Until then, use testing-principles.md and test-object-patterns.md as supporting docs.
```

### PMR-027 — Verify Playwright cleanup plan currentness

```text
ID: PMR-027
Status: waiting-for-condition
Area: testing / historical cleanup
Task / reminder:
  Verify whether planning/testing/playwright-e2e-cleanup-plan.md is historical, completed, stale or still actionable.
Trigger / condition:
  Before using the cleanup plan as current guidance or before deleting/superseding it.
Why it matters:
  The file may contain useful history, but current E2E workflow is planning/testing/e2e-testing-workflow.md.
Owner layer:
  testing / documentation governance
Target files:
  planning/testing/playwright-e2e-cleanup-plan.md
  planning/testing/README.md
  planning/testing/testing-responsibility-map.md
Depends on:
  testing responsibility map
Do when:
  During a testing docs cleanup pass.
Do not do before:
  Do not treat historical cleanup notes as current repo state without verification.
Notes:
  Keep as historical/planned note until checked.
```

### PMR-028 — Move scenario and diagramming docs to dedicated folders

```text
ID: PMR-028
Status: waiting-for-condition
Area: scenario / diagramming physical placement
Task / reminder:
  Decide and execute the physical move from planning/diagrams/ to separate scenario and diagramming folders.
Trigger / condition:
  After planning/diagramming/ responsibility layer is accepted and link references are audited.
Why it matters:
  The target model is scenario source ownership separate from diagram generation ownership. Current physical colocation is transitional.
Owner layer:
  documentation / scenario / diagramming governance
Target files:
  planning/diagrams/
  future planning/scenarios/
  planning/diagramming/
Depends on:
  planning/diagramming/README.md
  planning/diagramming/diagramming-responsibility-map.md
  scenario responsibility map and artifact map
Do when:
  During a dedicated mechanical move/link-update package.
Do not do before:
  Do not combine with scenario content migration or generated diagram artifact cleanup.
Notes:
  A compatibility pointer at planning/diagrams/ may be needed after the move.
```

### PMR-029 — Classify legacy and generated diagram artifacts

```text
ID: PMR-029
Status: waiting-for-condition
Area: diagram artifact cleanup
Task / reminder:
  Classify root-level diagram notes, generated .drawio packages, UML files and old diagram examples as current workflow, example, generated artifact, legacy or historical.
Trigger / condition:
  After diagramming responsibility layer exists and before deleting/moving old diagram artifacts.
Why it matters:
  Historical/generated diagram outputs should not look like current scenario/domain source truth.
Owner layer:
  diagramming / documentation governance
Target files:
  planning/diagram-*.md
  planning/*.drawio
  planning/diagrams/*.drawio
  planning/diagrams/uml/
  future planning/diagramming/generated/ or legacy/ if created
Depends on:
  planning/diagramming responsibility routing
Do when:
  During a dedicated diagram artifact classification package.
Do not do before:
  Do not mix generated artifact classification with scenario source folder move.
Notes:
  Keep generated diagrams until classified; do not delete as part of routing cleanup.
```

## 4. Maintenance Rules

```text
- Add entries when a task is intentionally deferred.
- Include the trigger/condition for when the task should be revisited.
- Do not use this as a dumping ground for local draft questions.
- If a task belongs to a layer-specific register, put it there instead and add only a root-level reminder here when cross-layer follow-up is needed.
- Do not use PMR as the general log of all documentation actions.
- If a completed logical change needs history, record it in planning/documentation/documentation-action-log.md.
- If a logged action creates future work, add or update a PMR entry and reference it from the action log.
- Mark entries done/superseded instead of deleting them when traceability is useful.
```
