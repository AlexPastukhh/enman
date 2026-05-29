# Documentation Action Log

Status: current documentation-layer logical action log  
Scope: significant completed documentation-architecture, workflow, source-governance and planning-doc actions with short reasons

## 1. Purpose

This file records significant logical documentation actions that have already been done and why they were done.

It helps future chats understand how the documentation system changed without turning PMR into a general history file.

This file is not:

```text
- the PMR;
- a task register;
- raw Git history;
- the source of truth for rules;
- a replacement for owner workflows, templates or principles.
```

Owner files define the rules. This log records what changed, why, affected files/layers and optional PMR relation.

## 2. PMR Boundary

```text
PMR
  = goals, reminders, deferred work, waiting-for-condition tasks and follow-ups that still need action.

Documentation action log
  = significant completed logical documentation actions and why they happened.
```

Not every action creates a PMR entry.

Not every technical edit creates an action-log entry.

A logical action may be logged even when it has no PMR relation.

## 3. Entry Format

Use this format for new entries:

```text
Date:
Action:
Type:
Status:
Why:
Changed files:
Layer impact:
PMR relation:
Follow-ups:
Owner sources:
Notes:
```

PMR relation values:

```text
none
related to PMR-<id>
creates PMR-<id>
partially addresses PMR-<id>
closes PMR-<id>
supersedes PMR-<id>
```

## 4. When To Log

Log an action when it changes or introduces:

```text
- architecture principles;
- workflow behavior;
- accepted command meaning;
- source-of-truth boundaries;
- source usage / cascade governance;
- template or output shape;
- onboarding route;
- documentation-layer example infrastructure;
- replacement archive/package behavior;
- PMR/task governance.
```

Do not log:

```text
- typo-only edits;
- minor link fixes;
- raw Git commit details without logical meaning;
- mechanical navigation sync already covered by a broader logged action;
- every small local wording cleanup.
```

## 5. Log Entries

### 2026-05-29 — Added example coverage infrastructure

```text
Date:
  2026-05-29
Action:
  Added documentation-layer example coverage workflow and examples index.
Type:
  governance / example coverage
Status:
  applied
Why:
  Reusable templates, commands, output modes and draft shapes need working examples or explicit deferred/not-needed decisions so future chats can see correct output shape instead of guessing.
Changed files:
  - planning/documentation/example-coverage-workflow.md
  - planning/documentation/examples/README.md
  - planning/documentation/README.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/planning-use-case-map.md
Layer impact:
  Documentation layer now has a route for deciding whether a new/changed template, workflow output, command or draft format needs an example.
PMR relation:
  none
Follow-ups:
  Add filled examples after the relevant workflows/templates are used and audited.
Owner sources:
  planning/documentation/example-coverage-workflow.md
  planning/documentation/examples/README.md
Notes:
  Examples remain supporting artifacts and must not own routing, source-mode, output-mode or permission logic.
```

### 2026-05-29 — Strengthened replacement archive/package workflow

```text
Date:
  2026-05-29
Action:
  Strengthened replacement archive/package rules, diff capture, post-apply preservation review and conversation review loop.
Type:
  workflow guardrail / replacement archive mode
Status:
  applied
Why:
  Future chats were at risk of producing patch scripts or incomplete review commands when the accepted command meant replacement archive/package. The workflow now requires complete replacement/add files, scoped diff capture, new-file visibility through git add -N and preservation review before commit.
Changed files:
  - planning/replacement-file-generation-guide.md
  - planning/planning-use-case-map.md
  - planning/documentation/examples/README.md
Layer impact:
  Replacement archive/package output mode is clearer and safer for local manual apply/review.
PMR relation:
  none
Follow-ups:
  Add filled examples for replacement archive package, post-apply preservation check, diff capture and conversation loop.
Owner sources:
  planning/replacement-file-generation-guide.md
  planning/planning-use-case-map.md
Notes:
  Service files such as MANIFEST.md and APPLY.md remain archive metadata unless intentionally added as repository files.
```

### 2026-05-29 — Added new chat onboarding route

```text
Date:
  2026-05-29
Action:
  Added a New Chat Onboarding / first planning pass use-case route.
Type:
  onboarding / use-case routing
Status:
  applied
Why:
  A new chat needs a compact way to enter the planning documentation system without asking the user which workflow files to read or starting a broad audit blindly.
Changed files:
  - planning/README.md
  - planning/planning-use-case-map.md
Layer impact:
  Root planning navigation now points new/uncertain chats to the right entry docs and use-case route.
PMR relation:
  none
Follow-ups:
  Add a filled compact onboarding response example.
Owner sources:
  planning/README.md
  planning/planning-use-case-map.md
Notes:
  Onboarding is a route/use-case, not a separate global workflow.
```

### 2026-05-29 — Added layer encapsulation and attention preservation principle

```text
Date:
  2026-05-29
Action:
  Added the Layer Encapsulation And Attention Preservation principle.
Type:
  architecture principle
Status:
  applied
Why:
  Downstream docs should reference already-reviewed upstream work instead of reconstructing it and forcing the user to re-review AI summaries of previous-layer reasoning.
Changed files:
  - planning/documentation/planning-docs-architecture-principles.md
  - planning/planning-use-case-map.md
Layer impact:
  Source usage/cascade work is now framed around preserving reviewed work and human attention, not file versioning by itself.
PMR relation:
  related to PMR-002
Follow-ups:
  Pilot source usage relationships before creating the full cascade workflow.
Owner sources:
  planning/documentation/planning-docs-architecture-principles.md
  planning/planning-use-case-map.md
Notes:
  External dependencies should be tracked through source usage relationships; internal same-file section dependencies normally stay local and usually do not need version markers.
```

### 2026-05-29 — Added source usage cascade pilot framework

```text
Date:
  2026-05-29
Action:
  Added a pilot framework and skeleton register for source usage cascade governance.
Type:
  source governance / pilot framework
Status:
  applied
Why:
  Source/version work should be relationship-first, not version-first. The first step is to prove source artifact/scope -> consumer artifact/scope relationships before creating the full cascade workflow.
Changed files:
  - planning/documentation/source-usage-cascade-governance-plan.md
  - planning/documentation/source-usage-pilots/README.md
  - planning/documentation/source-usage-pilots/SC-13D-agreement-exchange-source-usage-register.md
  - planning/documentation/README.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/examples/README.md
  - planning/planning-use-case-map.md
  - planning/replacement-file-generation-guide.md
Layer impact:
  Documentation layer now owns pilot governance for source usage/cascade work. Full source usage/cascade workflow still does not exist.
PMR relation:
  partially addresses PMR-002
Follow-ups:
  - Update PMR-002 wording to source usage / cascade / layer encapsulation.
  - Fill the SC-13D source usage pilot with real audited rows.
  - Create the full workflow only after pilot evidence.
Owner sources:
  planning/documentation/source-usage-cascade-governance-plan.md
  planning/documentation/source-usage-pilots/README.md
Notes:
  The SC-13D pilot register is intentionally skeleton / not audited.
```

### 2026-05-29 — Added documentation action log and PMR boundary

```text
Date:
  2026-05-29
Action:
  Added documentation action log and clarified the boundary between completed logical actions and PMR follow-ups.
Type:
  governance / action logging
Status:
  applied
Why:
  PMR should store goals, reminders and deferred/waiting tasks, not every completed logical documentation action. A separate action log preserves short explanations of what changed and why without turning PMR into a general history file.
Changed files:
  - planning/documentation/documentation-action-log.md
  - planning/documentation/README.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/documentation-update-workflow.md
  - planning/documentation/examples/README.md
  - planning/documentation/source-usage-cascade-governance-plan.md
Layer impact:
  Documentation layer now has a dedicated explanatory log for significant completed actions.
PMR relation:
  related to PMR-002
Follow-ups:
  Update PMR-002 when a safe complete PMR replacement or direct scoped edit is available.
Owner sources:
  planning/documentation/documentation-action-log.md
  planning/documentation/documentation-update-workflow.md
Notes:
  This entry starts from the current chat history and is not a full historical reconstruction of the entire project.
```

### 2026-05-29 — Added File Update Overview workflow and template

```text
Date:
  2026-05-29
Action:
  Added a reusable File Update Overview workflow and template.
Type:
  response/output shape
Status:
  applied
Why:
  Non-trivial file, documentation and code update answers need a final structured file-change summary that shows logical groups, file responsibilities, what changed, why it changed, checks and next action without replacing the main reviewable answer.
Changed files:
  - planning/documentation/file-update-overview-workflow.md
  - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
  - planning/documentation/README.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/examples/README.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Documentation layer now has a reusable owner for File Update Overview process and shape.
PMR relation:
  none
Follow-ups:
  - Update reviewable-agent-output-and-commands-workflow.md to link Level 2/3 file update answers to File Update Overview.
  - Update planning-use-case-map.md expected-output markers where serious routes should reference File Update Overview.
  - Add a filled File Update Overview example after the format is used on a follow-up update.
Owner sources:
  planning/documentation/file-update-overview-workflow.md
  planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
Notes:
  This action does not yet add the response-level `обс` command or automatic Level escalation triggers. Those belong to the follow-up targeted script for large/shared owner files.
```

### 2026-05-29 — Added local targeted script edit and hybrid delivery rules

```text
Date:
  2026-05-29
Action:
  Added Local Targeted Script Edit mode and hybrid archive/script delivery rules.
Type:
  workflow behavior / output mode
Status:
  applied
Why:
  Batch A2 showed that large/shared files should not be edited by one multi-file script. Future updates need target-file delivery safety classification, one targeted write script per large/shared file, preflight-before-write, diff-to-file/clipboard review and hybrid archive/script delivery when safe files and risky large files appear in one coherent update.
Changed files:
  - planning/documentation/documentation-update-workflow.md
  - planning/replacement-file-generation-guide.md
  - planning/documentation/examples/README.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Documentation update workflow now has a dedicated local targeted script mode, and replacement archive guidance now supports explicit hybrid archive/script delivery without hiding scripts inside replacement packages.
PMR relation:
  none
Follow-ups:
  - Add a route/expected-output marker in planning/planning-use-case-map.md if large-file/script requests need explicit routing.
  - Add filled examples for Local Targeted Script Edit and Hybrid Archive/Script Delivery after a clean use.
Owner sources:
  planning/documentation/documentation-update-workflow.md
  planning/replacement-file-generation-guide.md
Notes:
  This action records the workflow lesson from A2: one large/shared file equals one targeted write script; final combined diff review is allowed, but combined multi-large-file write scripts are not.
```

### 2026-05-29 — Added response block semantics and large-file archive preference

```text
Date:
  2026-05-29
Action:
  Added response block semantics for Level 2 answers, draft-update differences, final `Итог` planning/actual modes and large-file archive-first preference.
Type:
  response/output behavior / workflow guardrail
Status:
  applied
Why:
  Level 2 answers need readable key points without losing the reviewable body. Draft updates need differences from the previous draft instead of generic key points. `Краткое саммари` and `Итог` need separate responsibilities: contextual summary versus file/change/update overview. Large files should not automatically trigger scripts when a fresh full repo/archive snapshot makes complete replacement safe.
Changed files:
  - planning/documentation/reviewable-agent-output-and-commands-workflow.md
  - planning/planning-use-case-map.md
  - planning/documentation/file-update-overview-workflow.md
  - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
  - planning/documentation/documentation-update-workflow.md
  - planning/replacement-file-generation-guide.md
  - planning/documentation/planning-docs-architecture-principles.md
  - planning/documentation/examples/README.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Documentation layer now has clearer response block ordering, command routing, final overview semantics and large-file delivery preference.
PMR relation:
  none
Follow-ups:
  Add filled examples after the response block and large-file delivery formats are used cleanly.
Owner sources:
  planning/documentation/reviewable-agent-output-and-commands-workflow.md
  planning/documentation/file-update-overview-workflow.md
  planning/replacement-file-generation-guide.md
Notes:
  `Итог` is not a generic conclusion. During planning it is the rolling nearest-batch plan; after artifact/diff/application it summarizes actual update state.
```

### 2026-05-29 — Switched Итог to grouped tables and exposed delivery safety

```text
Date:
  2026-05-29
Action:
  Switched the File Update Overview / `Итог` preferred shape to grouped Markdown tables and required visible delivery-safety information in file-update planning answers.
Type:
  response/output shape / planning visibility
Status:
  applied
Why:
  The previous vertical `Итог` format was correct but hard to scan. File-update planning also needs to make large/shared files, fresh full archive need and preferred delivery mode visible up front instead of leaving delivery safety implicit.
Changed files:
  - planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
  - planning/documentation/file-update-overview-workflow.md
  - planning/documentation/reviewable-agent-output-and-commands-workflow.md
  - planning/documentation/documentation-update-workflow.md
  - planning/documentation/examples/README.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Documentation-layer file-update planning answers now surface delivery safety as a visible planning point, and final `Итог` blocks are easier to scan.
PMR relation:
  none
Follow-ups:
  Add a filled table-based File Update Overview example after the format is used cleanly.
Owner sources:
  planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
  planning/documentation/file-update-overview-workflow.md
  planning/documentation/reviewable-agent-output-and-commands-workflow.md
  planning/documentation/documentation-update-workflow.md
Notes:
  This change does not add new response commands or alter archive mechanics. It changes output shape and planning visibility.
```

### 2026-05-29 — Added reusable use-case-map workflow and template

```text
Date:
  2026-05-29
Action:
  Added reusable use-case-map workflow and template, and aligned documentation navigation, placement and the root planning use-case map with those owners.
Type:
  reusable documentation infrastructure / workflow-template governance
Status:
  applied
Why:
  Use-case maps are useful beyond the current Enman planning map. The reusable workflow now owns how to create and update use-case maps, while the reusable template owns their exact shape. Concrete maps can link to those owners instead of becoming workflow/template sources of truth.
Changed files:
  - planning/documentation/use-case-map-workflow.md
  - planning/documentation/USE-CASE-MAP-TEMPLATE.md
  - planning/documentation/README.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/planning-use-case-map.md
  - planning/documentation/examples/README.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Documentation layer now has reusable infrastructure for use-case maps across systems, not only the current root planning use-case map.
PMR relation:
  none
Follow-ups:
  Use the workflow/template on a future concrete map and then add a filled `USE-CASE-MAP-CREATION-UPDATE-v1` example.
Owner sources:
  planning/documentation/use-case-map-workflow.md
  planning/documentation/USE-CASE-MAP-TEMPLATE.md
  planning/documentation/documentation-responsibility-map.md
Notes:
  This change does not perform the broader documentation-layer decoupling from Enman and does not split the principles file. Those remain separate future architecture batches.
```

### 2026-05-30 — Added documentation-layer portability role model and responsibility-zone review

```text
Date:
  2026-05-30
Action:
  Added a documentation-layer portability migration plan and responsibility-zone review workflow, clarified principles responsibility, introduced Field Kit / Adapter-Profile file-type boundaries, and corrected `арх` command semantics.
Type:
  documentation architecture / portability migration preparation
Status:
  applied
Why:
  The documentation layer is being prepared for a copy-first reusable migration. Before splitting the principles file or extracting a scenario-driven profile / Enman adapter, the repo needs a clear record of current decisions, a repeatable method for reviewing responsibility zones, and explicit source-vs-output archive semantics.
Changed files:
  - planning/documentation/documentation-layer-portability-migration-plan.md
  - planning/documentation/documentation-responsibility-zone-review-workflow.md
  - planning/documentation/planning-docs-architecture-principles.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/README.md
  - planning/planning-use-case-map.md
  - planning/documentation/documentation-update-workflow.md
  - planning/documentation/examples/README.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Documentation layer now has a pre-split portability plan, a reusable responsibility-zone review method, explicit Field Kit and Adapter/Profile type boundaries, and corrected `арх` source snapshot semantics.
PMR relation:
  none
Follow-ups:
  Create the reusable candidate copy, then classify and split principles inside the candidate before extracting the scenario-driven profile or Enman/project adapter.
Owner sources:
  planning/documentation/documentation-layer-portability-migration-plan.md
  planning/documentation/documentation-responsibility-zone-review-workflow.md
  planning/documentation/planning-docs-architecture-principles.md
  planning/documentation/documentation-responsibility-map.md
Notes:
  This batch intentionally does not split principles, create the scenario-domain-slice profile, create the Enman adapter, move VKR/source-usage pilots/sync notes, or create the reusable candidate copy.
```

### 2026-05-30 — Added reusable candidate guardrails

```text
Date:
  2026-05-30
Action:
  Added guardrails for the reusable documentation candidate baseline after the active documentation layer was copied to planning/documentation-reusable-candidate/.
Type:
  documentation architecture / portability migration preparation
Status:
  applied
Why:
  The candidate copy should exist as a safe migration workspace without becoming a second active source of truth. Future cleanup, classification and split work should happen inside the candidate, while ordinary project documentation updates continue to use planning/documentation/.
Changed files:
  - planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
  - planning/documentation/documentation-layer-portability-migration-plan.md
  - planning/documentation/README.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  The reusable candidate workspace is now explicitly temporary, non-canonical and limited to portability/genericization work.
PMR relation:
  none
Follow-ups:
  Use documentation-responsibility-zone-review-workflow.md to classify planning/documentation-reusable-candidate/planning-docs-architecture-principles.md before any principles split.
Owner sources:
  planning/documentation/documentation-layer-portability-migration-plan.md
  planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
  planning/documentation/documentation-responsibility-map.md
Notes:
  This change does not split principles, create the scenario-domain-slice profile, create the Enman adapter, move VKR/source-usage pilots/sync notes, or change the copied candidate file contents.
```
