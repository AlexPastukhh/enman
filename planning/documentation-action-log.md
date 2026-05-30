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

### 2026-05-30 — Mirrored active reusable additions into candidate workspace

```text
Date:
  2026-05-30
Action:
  Mirrored active reusable use-case-map and replacement-command additions into the candidate reusable documentation workspace.
Type:
  documentation architecture / candidate overlay sync
Status:
  applied
Why:
  Active documentation gained reusable F7A/F7B files and command guardrails after the candidate copy. The candidate workspace needs those active reusable additions before any future folder-switch or swap planning, otherwise a blind candidate promotion could lose current reusable work.
Changed files:
  - planning/documentation/field-kits/root-use-case-map-field-kit.md
  - planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
  - planning/documentation/use-case-map-workflow.md
  - planning/documentation/USE-CASE-MAP-TEMPLATE.md
  - planning/documentation/documentation-update-workflow.md
  - planning/documentation/documentation-action-log.md
  - planning/documentation/README.md
  - planning/documentation-migration/CANDIDATE-NOTICE.md
  - planning/documentation-migration/PORTABILITY-FOLLOWUPS.md
  - planning/documentation/documentation-responsibility-map.md
Layer impact:
  Candidate now contains the active-only reusable UCM field kit, scenario/domain/slice UCM route setup kit, active-latest UCM workflow/template and active-latest documentation update diff-command guardrails. Candidate remains non-canonical.
PMR relation:
  none
Follow-ups:
  Plan candidate final-shape normalization and controlled folder-switch strategy. Do not perform blind rename of candidate to active docs because candidate still contains migration-only artifacts and active docs still own current routing.
Owner sources:
  planning/documentation/README.md
  planning/documentation-migration/CANDIDATE-NOTICE.md
  planning/documentation/field-kits/root-use-case-map-field-kit.md
  planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
Notes:
  This candidate-local entry intentionally does not import active action-log history. This change does not copy root project files into candidate, does not make the candidate folder canonical, does not rename folders and does not delete active or candidate documentation files.
```

### 2026-05-30 — Cleaned active reusable documentation layer after folder switch

```text
Date:
  2026-05-30
Action:
  Made planning/documentation/ self-consistent as the active reusable documentation layer after the folder rename switch.
Type:
  documentation architecture / post-switch cleanup
Status:
  applied
Why:
  The folder switch promoted the reusable candidate contents into planning/documentation/, but active files still contained candidate/non-canonical wording, stale former `planning/documentation-reusable-candidate/` paths and migration-only Enman adapter/project-instance artifacts.
Changed files:
  - planning/documentation/README.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/documentation-update-workflow.md
  - planning/documentation/status-reconciliation-workflow.md
  - planning/documentation/local-global-documentation-sync-workflow.md
  - planning/documentation/source-usage-cascade-governance-plan.md
  - planning/documentation/field-kits/root-use-case-map-field-kit.md
  - planning/documentation/field-kits/status-reconciliation-field-kit.md
  - planning/documentation/field-kits/shared-visibility-map-field-kit.md
  - planning/documentation/field-kits/source-usage-cascade-field-kit.md
  - planning/documentation/profiles/scenario-domain-slice-docs-profile.md
  - planning/documentation/profiles/scenario-domain-slice-use-case-field-kit.md
  - planning/documentation/examples/README.md
  - planning/documentation-migration/
  - planning/README.md
  - planning/planning-doc-responsibility-map.md
Layer impact:
  planning/documentation/ is now the active reusable documentation layer. Candidate-only guardrails/classification/followups and Enman adapter/profile history are outside the active docs root under planning/documentation-migration/. Root Enman profiles remain at planning/ root.
PMR relation:
  none
Follow-ups:
  Verify there are no runtime references to the former `planning/documentation-reusable-candidate/` path. Later decide when planning/documentation-legacy/ can be deleted.
Owner sources:
  planning/documentation/README.md
  planning/documentation/documentation-responsibility-map.md
  planning/planning-use-case-map.md
Notes:
  This cleanup does not delete planning/documentation-legacy/, does not move root Enman project profiles into planning/documentation/, does not delete the source-usage governance bridge and does not create a second generic root use-case map.
```
### 2026-05-30 — Cleaned remaining active docs semantic switch wording

Date:
  2026-05-30
Action:
  Cleaned remaining post-switch semantic wording in the active reusable documentation layer.
Type:
  documentation architecture / semantic cleanup
Status:
  applied
Why:
  After F7D/F7E, planning/documentation/ was active and structurally routed correctly, but a few active owner files still used candidate-era wording.
Changed files:
  - planning/documentation/planning-docs-architecture-principles.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/status-reconciliation-workflow.md
  - planning/documentation/local-global-documentation-sync-workflow.md
  - planning/documentation/field-kits/status-reconciliation-field-kit.md
  - planning/documentation/field-kits/shared-visibility-map-field-kit.md
  - planning/documentation/field-kits/source-usage-cascade-field-kit.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Active reusable docs no longer self-identify as candidate in the architecture principles file or setup handoff wording. Historical F7C overlay text is marked historical. Legacy remains preserved and inactive.
Follow-ups:
  Keep planning/documentation-legacy/ for now. Later run a dedicated verification batch before deciding whether to delete or archive legacy.
Notes:
  This cleanup does not delete planning/documentation-legacy/, does not move root Enman project profiles and does not change use-case routing.

### 2026-05-30 - Marked project-specific examples and planned reusable docs cleanup

Date:
  2026-05-30
Action:
  Marked the Enman source-usage example as project-specific demonstration-only material and recorded the follow-up cleanup direction for the reusable documentation layer.
Type:
  documentation portability / example boundary / cleanup planning
Status:
  applied
Why:
  The reusable documentation layer is active, but portability requires project-specific examples and transition/support files to be clearly separated from reusable owner logic before any future cleanup or starter-kit export.
Changed files:
  - planning/documentation/examples/README.md
  - planning/documentation/examples/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
  - planning/documentation/field-kits/source-usage-cascade-field-kit.md
  - planning/documentation/documentation-action-log.md
Layer impact:
  Enman-specific material remains allowed only as a clearly marked scenario/application example. Reusable owner logic stays in workflows, templates, field kits, profiles and responsibility maps.
Cleanup planning fixed in chat/package manifest:
  Future cleanup should audit tracking/migration/support files inside planning/documentation/ before moving or omitting them from a portable starter kit. Candidate cleanup targets include documentation-action-log.md, documentation-layer-portability-migration-plan.md, scoped sync notes and source-usage pilot artifacts. No files are deleted in this batch.
Follow-ups:
  Run a dedicated cleanup/export batch only after reviewing active references. Do not delete planning/documentation-legacy/ in this batch.
Notes:
  This batch does not create a cleanup-plan file in the repository, does not move root Enman profiles, does not change root use-case routing and does not delete any files.
### 2026-05-30 - Cleaned reusable docs layer support and project-specific artifacts

Date:
  2026-05-30
Action:
  Moved migration/support notes and source-usage pilot artifacts out of the active reusable documentation layer, and moved the Enman-specific source usage example under project-specific examples.
Type:
  documentation portability / reusable layer cleanup
Status:
  applied
Why:
  The active reusable documentation layer should contain reusable owner files, field kits, profiles, generic examples, explicitly marked project-specific examples and active bridge/workflow files. Migration plans, scoped sync notes and Enman project pilots should not sit in the active reusable docs root.
Changed files:
  - planning/documentation/README.md
  - planning/documentation/source-usage-cascade-governance-plan.md
  - planning/documentation/examples/README.md
  - planning/documentation/field-kits/source-usage-cascade-field-kit.md
  - planning/documentation/documentation-responsibility-zone-review-workflow.md
  - planning/documentation/documentation-action-log.md
  - planning/planning-use-case-map.md
  - planning/status-evidence-profile.md
  - planning/shared-visibility-map.md
  - planning/source-usage-cascade-profile.md
Moved files:
  - planning/documentation/documentation-layer-portability-migration-plan.md -> planning/documentation-migration/documentation-layer-portability-migration-plan.md
  - planning/documentation/*sync-note.md -> planning/documentation-migration/sync-notes/
  - planning/documentation/source-usage-pilots/ -> planning/source-usage-pilots/
  - planning/documentation/examples/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md -> planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
Layer impact:
  planning/documentation/ is cleaner as a reusable docs layer. Enman source-usage pilots are root project artifacts. Migration/support notes are in documentation-migration. The Enman-specific example remains in the docs layer only under an explicit project-specific examples path.
Follow-ups:
  Later decide whether to keep documentation-action-log.md in active docs or seed/omit it for portable starter-kit export. Do not delete planning/documentation-legacy/ in this batch.
Notes:
  This cleanup uses moves/reference updates only. It does not delete legacy, does not delete migration history and does not remove source-usage-cascade-governance-plan.md.

### 2026-05-30 - Finalized portable reusable docs layer

Date:
  2026-05-30
Action:
  Moved documentation action log to Enman planning root, moved source usage governance bridge to migration, added portable starter-kit adaptation guide and removed the bridge from active source-usage routing.
Type:
  documentation portability / reusable layer finalization
Status:
  applied
Why:
  The reusable documentation layer should not contain Enman project action history or transitional source-usage bridge ownership. Copy/adaptation into another project needs a clear one-time guide for what to copy, adapt, omit and recreate.
Changed files:
  - planning/documentation/README.md
  - planning/documentation/PORTABLE-STARTER-KIT.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/documentation/documentation-update-workflow.md
  - planning/documentation/documentation-responsibility-zone-review-workflow.md
  - planning/planning-use-case-map.md
  - planning/planning-doc-responsibility-map.md
  - planning/README.md
  - planning/source-usage-cascade-profile.md
  - planning/documentation-action-log.md
Moved files:
  - planning/documentation/documentation-action-log.md -> planning/documentation-action-log.md
  - planning/documentation/source-usage-cascade-governance-plan.md -> planning/documentation-migration/source-usage-cascade-governance-plan.md
Layer impact:
  planning/documentation/ now contains reusable docs logic, field kits, profiles, examples and the one-time portable starter-kit guide. Enman action history is root project history. Source usage active routing is field kit + root Enman profile + root pilots.
Follow-ups:
  Later decide whether to keep or archive planning/documentation-legacy/. For a new project, use PORTABLE-STARTER-KIT.md only during initial adaptation.
Notes:
  This batch preserves migrated source-usage bridge history and does not delete legacy or migration folders.

### 2026-05-31 - Routed section source templates into drafting use cases

Date:
  2026-05-31
Action:
  Connected Enman source cascade workflow and domain/server slice section source templates to drafting-related use-case routing.
Type:
  source/version/cascade routing / drafting templates
Status:
  applied
Why:
  Local section-level Sources blocks are part of domain and server slice drafting, not only standalone source-usage governance. Use-case routing must activate the source cascade workflow and section source templates when aggregate or server slice drafts add/review local Sources blocks.
Changed files:
  - planning/source-cascade-sync-workflow.md
  - planning/source-usage-cascade-profile.md
  - planning/planning-use-case-map.md
  - planning/documentation-action-log.md
Routing impact:
  Source/version/cascade tasks now route to the Enman source cascade workflow, base Sources block template, aggregate section source template and server slice section source template. Domain aggregate and server slice drafting rows now mention these files when local section Sources blocks or doc-version/source-cascade work are in scope.
Not changed:
  - No aggregate draft edited.
  - No slice draft edited.
  - No layer source-sync registers created.
  - No broad Doc version pass performed.
Follow-ups:
  Apply the aggregate section source template to planning/domain/aggregates/agreement-proposal-exchange.md, then apply the server slice section source template to SL-AGR-EXCH-001.
Notes:
  This batch only routes already-created templates into the workflow/use-case system.

### 2026-05-31 - Added Account aggregate local Sources blocks

Date:
  2026-05-31
Action:
  Applied the aggregate section source template to the Account aggregate draft.
Type:
  domain aggregate source/version/cascade preparation
Status:
  applied
Why:
  Active domain aggregate drafts need local section-level Sources blocks before a domain source-sync register can be derived. Account is the first aggregate pass because it is a foundational actor/account source for request and agreement exchange flows.
Changed files:
  - planning/domain/aggregates/account.md
  - planning/documentation-action-log.md
Updates:
  - Added `Doc version: v0.1.0` to the Account aggregate draft.
  - Added fenced local `Sources:` blocks to existing Account aggregate sections.
  - Reclassified `## 2. Source Inputs` as aggregate-level overview rather than the only source authority.
  - Clarified the scenario DATA rule: scenario-specific DATA is expected in scenario text spec `#DATA`; `planning/diagrams/scenario-data/` is reusable/shared/audited/transitional by default.
Not changed:
  - No domain source-sync register created.
  - No slice draft edited.
  - No broad Doc version pass performed.
  - No Account domain behavior semantics intentionally changed.
Follow-ups:
  Apply the same aggregate-local source pattern to ApplicantParty, then ConnectionRequest, then AgreementProposalExchange. After all active aggregate drafts are prepared, derive `planning/domain/domain-source-sync-register.md`.
Notes:
  This batch keeps implementation evidence marked as previously checked/archive/source-pass evidence unless explicitly rechecked in a later implementation-sync pass.

### 2026-05-31 - Added ApplicantParty aggregate local Sources blocks

Date:
  2026-05-31
Action:
  Applied the aggregate section source template to the ApplicantParty aggregate draft.
Type:
  domain aggregate source/version/cascade preparation
Status:
  applied
Why:
  Active domain aggregate drafts need local section-level Sources blocks before a domain source-sync register can be derived. ApplicantParty is the second aggregate pass because it is the reusable applicant/contact source used by request creation and request approval coordination.
Changed files:
  - planning/domain/aggregates/applicant-party.md
  - planning/documentation-action-log.md
Updates:
  - Added `Doc version: v0.1.0` to the ApplicantParty aggregate draft.
  - Added fenced local `Sources:` blocks to existing ApplicantParty aggregate sections.
  - Reclassified `## 2. Source Inputs` as aggregate-level overview rather than the only source authority.
  - Clarified the scenario DATA rule: scenario-specific DATA is expected in scenario text spec `#DATA`; `planning/diagrams/scenario-data/` is reusable/shared/audited/transitional by default.
  - Added `## 15. Source Delta / Change Log` to record the source-block refactor.
Not changed:
  - No domain source-sync register created.
  - No slice draft edited.
  - No broad Doc version pass performed.
  - No ApplicantParty domain behavior semantics intentionally changed.
Follow-ups:
  Apply the same aggregate-local source pattern to ConnectionRequest, then AgreementProposalExchange. After all active aggregate drafts are prepared, derive `planning/domain/domain-source-sync-register.md`.
Notes:
  This batch keeps implementation evidence marked as previously checked/archive/source-pass evidence unless explicitly rechecked in a later implementation-sync pass.
