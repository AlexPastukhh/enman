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

### 2026-05-31 - Added ConnectionRequest aggregate local Sources blocks

Date:
  2026-05-31
Action:
  Applied the aggregate section source template to the ConnectionRequest aggregate draft.
Type:
  domain aggregate source/version/cascade preparation
Status:
  applied
Why:
  Active domain aggregate drafts need local section-level Sources blocks before a domain source-sync register can be derived. ConnectionRequest is the third aggregate pass because it owns request creation/review lifecycle and coordinates with ApplicantParty, Account and AgreementProposalExchange.
Changed files:
  - planning/domain/aggregates/connection-request.md
  - planning/documentation-action-log.md
Updates:
  - Added `Doc version: v0.1.0` to the ConnectionRequest aggregate draft.
  - Added fenced local `Sources:` blocks to existing ConnectionRequest aggregate sections.
  - Reclassified `## 2. Source Inputs` as aggregate-level overview rather than the only source authority.
  - Clarified the scenario DATA rule: scenario-specific DATA is expected in scenario text spec `#DATA`; `planning/diagrams/scenario-data/` is reusable/shared/audited/transitional by default.
Not changed:
  - No domain source-sync register created.
  - No slice draft edited.
  - No broad Doc version pass performed.
  - No ConnectionRequest domain behavior semantics intentionally changed.
Follow-ups:
  Apply the same aggregate-local source pattern to AgreementProposalExchange. After all active aggregate drafts are prepared, derive `planning/domain/domain-source-sync-register.md`.
Notes:
  This batch keeps implementation evidence marked as previously checked/archive/source-pass evidence unless explicitly rechecked in a later implementation-sync pass.

### 2026-05-31 - Added AgreementProposalExchange aggregate local Sources blocks

Date:
  2026-05-31
Action:
  Applied the aggregate section source template to the AgreementProposalExchange aggregate draft.
Type:
  domain aggregate source/version/cascade preparation
Status:
  applied
Why:
  Active domain aggregate drafts need local section-level Sources blocks before a domain source-sync register can be derived. AgreementProposalExchange is the final active aggregate pass because it owns the post-approval proposal exchange lifecycle and coordinates with ConnectionRequest and Account.
Changed files:
  - planning/domain/aggregates/agreement-proposal-exchange.md
  - planning/documentation-action-log.md
Updates:
  - Added `Doc version: v0.1.0` to the AgreementProposalExchange aggregate draft.
  - Added fenced local `Sources:` blocks to existing AgreementProposalExchange aggregate sections.
  - Reclassified `## 2. Source Inputs` as aggregate-level overview rather than the only source authority.
  - Clarified the SC-13D scenario DATA rule: scenario-specific DATA is expected in scenario text spec `#DATA`; `planning/diagrams/scenario-data/` is reusable/shared/audited/transitional by default.
Not changed:
  - No domain source-sync register created.
  - No slice draft edited.
  - No broad Doc version pass performed.
  - No AgreementProposalExchange domain behavior semantics intentionally changed.
Follow-ups:
  Review all four prepared active aggregate drafts, then derive `planning/domain/domain-source-sync-register.md`. After the domain register is reviewed, decide how to approach the larger slice draft refactor.
Notes:
  This batch keeps implementation evidence marked as previously checked/archive/source-pass evidence unless explicitly rechecked in a later implementation-sync pass.

### 2026-05-31 - Cleaned domain source model before source-sync register

Date:
  2026-05-31
Action:
  Cleaned up the scenario-to-aggregate source model after all four active aggregate drafts received local section Sources blocks.
Type:
  domain source/version/cascade cleanup
Status:
  applied
Why:
  The domain discovery bridge still treated `planning/diagrams/scenario-data/` as a primary source bucket. The prepared aggregate drafts now use the newer rule: scenario text spec `#DATA` is primary for scenario-specific DATA, while `planning/diagrams/scenario-data/` is reusable/shared/audited/transitional by default.
Changed files:
  - planning/domain/scenario-to-aggregate-map.md
  - planning/documentation-action-log.md
Updates:
  - Clarified the domain Source Model DATA rule.
  - Added domain drafting/local source block process files to the Source Model.
  - Updated Aggregate Register statuses to show all four active aggregate drafts are prepared with `Doc version: v0.1.0` and local section `Sources:` blocks.
  - Updated next domain work to derive `planning/domain/domain-source-sync-register.md` before slice draft refactor.
Not changed:
  - No aggregate draft edited.
  - No domain source-sync register created.
  - No slice draft edited.
  - No broad Doc version pass performed.
Follow-ups:
  Create `planning/domain/domain-source-sync-register.md` from local aggregate section `Sources:` blocks, then review the domain register before planning slice refactor.
Notes:
  This cleanup does not change aggregate semantics. It aligns the domain discovery bridge with the source model already applied in Account, ApplicantParty, ConnectionRequest and AgreementProposalExchange.

### 2026-05-31 - Created domain source-sync register

Date:
  2026-05-31
Action:
  Created `planning/domain/domain-source-sync-register.md` from active aggregate local section `Sources:` blocks.
Type:
  domain source/version/cascade register
Status:
  applied
Why:
  After Account, ApplicantParty, ConnectionRequest and AgreementProposalExchange received `Doc version: v0.1.0` plus local section `Sources:` blocks, the domain layer needed a derived register to index source dependencies and cross-aggregate sync boundaries before any slice draft refactor.
Changed files:
  - planning/domain/domain-source-sync-register.md
  - planning/documentation-action-log.md
Updates:
  - Added active domain files table for the four prepared aggregate drafts.
  - Added aggregate source dependency rows grouped by format/process, scenario/content, value-object, historical/cross-check and cross-aggregate source groups.
  - Added cross-aggregate dependency table and recorded that no semantic re-sync is needed now because F7K-A1..A4 did not intentionally change aggregate behavior semantics.
  - Added scenario DATA classification table matching the current source model.
Not changed:
  - No aggregate draft edited.
  - No scenario/source file edited.
  - No slice draft edited.
  - No broad Doc version pass performed.
  - No source versions invented for unversioned source files.
Follow-ups:
  Review `planning/domain/domain-source-sync-register.md`. Then decide whether to route it from domain README / use-case map before planning the larger slice draft refactor.
Notes:
  The register is a navigation/sync index. Aggregate local section `Sources:` blocks remain authoritative for section-level work.

### 2026-05-31 - Routed domain register and seeded scenario/domain Doc versions

Date:
  2026-05-31
Action:
  Routed the domain source-sync register into domain/source-cascade read paths and seeded `Doc version: v0.1.0` headers across planning/domain and planning/diagrams markdown files.
Type:
  domain/scenario version coverage / source-cascade routing
Status:
  applied
Why:
  The domain source-sync register can only move from file-path/status references toward concrete source-version references after the scenario and domain source files declare document versions. Future domain and slice refactor work also needs the register to be discoverable from domain README and use-case routing.
Changed files:
  - planning/domain/**/*.md
  - planning/diagrams/**/*.md
  - planning/domain/README.md
  - planning/planning-use-case-map.md
  - planning/documentation-action-log.md
Updates:
  - Added `Doc version: v0.1.0` to scenario-layer markdown files under `planning/diagrams/` where missing.
  - Added `Doc version: v0.1.0` to domain-layer markdown files under `planning/domain/` where missing.
  - Routed `planning/domain/domain-source-sync-register.md` from the domain README read order.
  - Routed `planning/domain/domain-source-sync-register.md` from source-cascade and domain use-case rows.
Not changed:
  - No aggregate/domain behavior semantics intentionally changed.
  - No scenario behavior semantics intentionally changed.
  - No slice draft edited.
  - No domain source-sync register source-version rows updated yet.
Follow-ups:
  Update `planning/domain/domain-source-sync-register.md` and related source model references so source dependency rows use concrete `Doc version: v0.1.0` for newly versioned scenario/domain sources instead of `version not declared`.
Notes:
  This is a broad version seed pass. It introduces document versions only; it does not claim all source content was semantically re-audited.

### 2026-05-31 - Synchronized domain register source versions after Doc version seed

Date:
  2026-05-31
Action:
  Updated `planning/domain/domain-source-sync-register.md` after the D2 scenario/domain Doc version seed.
Type:
  domain source/version/cascade register sync
Status:
  applied
Why:
  D2 introduced `Doc version: v0.1.0` across `planning/domain/**/*.md` and `planning/diagrams/**/*.md`. The domain source-sync register needed to stop treating those source groups as `version not declared` and instead reference concrete document versions while still avoiding claims of semantic re-audit.
Changed files:
  - planning/domain/domain-source-sync-register.md
  - planning/documentation-action-log.md
Updates:
  - Updated register rules to state that D2 was a document-version seed, not a semantic re-audit.
  - Updated source category version rules for scenario text specs, scenario behavior items, scenario clarifications, scenario DATA sidecars, domain aggregates and domain value objects.
  - Updated aggregate source dependency rows to use `Doc version: v0.1.0` for `planning/domain/**` and `planning/diagrams/**` source groups.
  - Kept `planning/tables/**` sources as historical/cross-check and version-not-declared in this register.
  - Kept implementation/test evidence as prior archive/source-pass evidence, not current proof.
Not changed:
  - No aggregate draft edited.
  - No scenario/source file edited.
  - No slice draft edited.
  - No semantic re-audit claimed.
Follow-ups:
  Review the synchronized domain register, then plan the first slice-side source/version/cascade step as a separate batch.
Notes:
  Aggregate local section `Sources:` blocks remain authoritative for section-level work. This register remains a navigation/sync index.

### 2026-05-31 - Versioned active aggregate local Sources blocks

Date:
  2026-05-31
Action:
  Updated local `Sources:` blocks in all four active aggregate drafts to use version/status-qualified source references and synchronized the domain source-sync register.
Type:
  domain source/version/cascade local source sync
Status:
  applied
Why:
  After D2 seeded `Doc version: v0.1.0` across planning/domain and planning/diagrams and D3 synchronized the domain register, the aggregate-local source blocks still contained bare paths. The active aggregate drafts needed local source references to match the register/source-version model before any slice-side source cascade work.
Changed files:
  - planning/domain/aggregates/account.md
  - planning/domain/aggregates/applicant-party.md
  - planning/domain/aggregates/connection-request.md
  - planning/domain/aggregates/agreement-proposal-exchange.md
  - planning/domain/domain-source-sync-register.md
  - planning/documentation-action-log.md
Updates:
  - Added `@ Doc version: v0.1.0` to planning/domain and planning/diagrams source references inside active aggregate local `Sources:` blocks.
  - Marked planning/tables references as historical/cross-check, version not declared.
  - Kept root source-cascade/template references as version not confirmed in this batch.
  - Corrected stale source paths found during local source versioning.
  - Synchronized the domain source-sync register with the versioned local aggregate source model.
Not changed:
  - No aggregate/domain behavior semantics intentionally changed.
  - No scenario behavior semantics intentionally changed.
  - No scenario/source file edited.
  - No slice draft edited.
  - No semantic re-audit claimed.
Follow-ups:
  Review the versioned local source blocks and register. Then plan the first slice-side source/version/cascade step separately.
Notes:
  Aggregate local section `Sources:` blocks remain authoritative for section-level work. This batch changes source references and source status labels only.

### 2026-05-31 - Added root source register skeleton and register-state workflow rules

Date:
  2026-05-31
Action:
  Updated the Enman source cascade workflow/profile to distinguish skeleton, derived and synchronized registers, and created the first root source-sync register skeleton.
Type:
  root/source-cascade register architecture
Status:
  applied
Why:
  Source/version/cascade tracking must cover root workflow/router/source-governance files, not only active domain aggregate drafts. Before slice-side work starts, the workflow needed current wording for the existing domain register and a safe skeleton model for registers that exist before full local Sources coverage.
Changed files:
  - planning/source-cascade-sync-workflow.md
  - planning/source-usage-cascade-profile.md
  - planning/root-source-sync-register.md
  - planning/documentation-action-log.md
Updates:
  - Replaced stale planned-domain-register wording with current domain register wording.
  - Added register state rules: skeleton, derived and synchronized.
  - Clarified that skeleton registers are allowed only when explicitly marked incomplete and not claiming full source coverage.
  - Added `planning/root-source-sync-register.md` as a skeleton register for root planning/workflow/router dependencies.
  - Added `Doc version: v0.1.0` to `planning/source-usage-cascade-profile.md`.
  - Bumped `planning/source-cascade-sync-workflow.md` to `Doc version: v0.2.0` because register-state workflow rules changed.
Not changed:
  - No domain aggregate draft edited.
  - No scenario/source file edited.
  - No slice draft edited.
  - No slice source-sync register created.
  - No broad root Doc version pass performed.
Follow-ups:
  Seed Doc versions for selected root source-cascade/router files, then sync slice README/use-case rows/server section template with the current domain register before creating the slice source-sync register skeleton.
Notes:
  `planning/root-source-sync-register.md` is intentionally a skeleton. It lists candidate dependencies and not-checked areas but does not claim full root source coverage.

### 2026-05-31 - Added reusable command execution examples

Date:
  2026-05-31
Action:
  Added reusable command execution examples and updated Level 2 response-shape guidance.
Type:
  reusable response/command examples / response-output workflow clarification
Status:
  applied
Why:
  Ambiguous or high-risk response commands need compact valid execution examples without embedding long examples inside the root use-case map. Level 2 also needed a clearer rule that Key points are non-fixed compressed mirrors of the detailed answer while `Краткое саммари` owns the fixed traceability order.
Changed files:
  - planning/documentation/reviewable-agent-output-and-commands-workflow.md
  - planning/documentation/examples/README.md
  - planning/documentation/examples/LEVEL-2-KEY-POINTS-SUMMARY-EXAMPLE.md
  - planning/documentation/examples/PLAN-COMMAND-VALID-EXECUTION-EXAMPLE.md
  - planning/documentation/examples/ARCHIVE-SOURCE-VS-OUTPUT-PACKAGE-EXAMPLE.md
  - planning/documentation-action-log.md
Updates:
  - Clarified that Key points have no fixed internal format and mirror the detailed answer in compressed form.
  - Clarified that Key points do not replace the detailed answer and should be omitted when the answer is already short.
  - Moved fixed-format Level 2 traceability to `Краткое саммари` with order: Вывод, Следующие действия, Цель понял так, Учтённый контекст, Границы.
  - Added reusable valid execution examples for Level 2 response shape, planning command execution and archive source/output separation.
  - Updated the examples index to record these examples and command-example placement rules.
Not changed:
  - No planning-use-case-map.md routing update in this batch.
  - No Enman-specific project example added in this batch.
  - No separate response-command registry created.
  - No domain/scenario/slice draft edited.
  - No source register updated.
  - No command semantics moved from owner workflows into examples.
Follow-ups:
  Add a separate root use-case map routing batch that links relevant command/use-case rows to these examples and adds the Enman-specific scenario/domain/slice planning example.
Notes:
  This package was rebuilt after rejecting the previous F7-CMD-1 package that attempted to overwrite most of `planning/documentation-action-log.md`. This entry is appended to the preserved current action log.
### 2026-05-31 - Added root goal process map

Date:
  2026-05-31
Action:
  Added a root Goal Process Map owner file, separate root example and root use-case routing for goal/process commands.
Type:
  root planning aid / long-running goal process tracking
Status:
  applied
Why:
  Long-running chat work needs a stable way to show final picture, current state, scenario-like target behaviors, process slices, acceptance criteria, decision points and next action. Existing Key points, Краткое саммари and File Update Overview help navigation but do not represent the full process architecture of a goal.
Changed files:
  - planning/goal-process-map.md
  - planning/goal-process-map-example.md
  - planning/planning-use-case-map.md
  - planning/documentation-action-log.md
Updates:
  - Added `planning/goal-process-map.md` as the root owner for Goal Process Map principles, format and workflow.
  - Added `planning/goal-process-map-example.md` as a separate demonstration file so the owner file stays compact.
  - Routed `карта процесса`, `где мы`, `прогресс`, `статус цели` and related aliases from the root use-case map.
  - Defined the map as a mini-architecture of a goal: final picture, target scenarios, process slices, acceptance criteria, current state and decision points.
Not changed:
  - No Tampermonkey script added.
  - No critical-thinking command added.
  - No generic Action Overview added.
  - No source/domain/slice files edited.
  - No reusable workflow/template split performed.
Follow-ups:
  Add critical-thinking command, generic Action Overview and Tampermonkey command palette planning as separate batches.
Notes:
  This package uses targeted inserts for existing root files to avoid overwriting current use-case map or action-log history.
### 2026-05-31 - Added active command-system goal process workstream map

Date:
  2026-05-31
Action:
  Added an active root workstream Goal Process Map for the transferable command-system and Tampermonkey helper goal.
Type:
  root workstream map / goal process tracking
Status:
  applied
Why:
  The Goal Process Map owner and static example define the format, but the actual command-system/Tampermonkey goal needs a living repo file that can be updated as scenarios, slices, decisions and next actions change across chats.
Changed files:
  - planning/workstreams/command-system-and-tampermonkey-goal-process.md
  - planning/goal-process-map.md
  - planning/documentation-action-log.md
Updates:
  - Added a hybrid active workstream map with overview tables and detailed slice sections.
  - Added a living-map storage rule to planning/goal-process-map.md.
  - Kept the static example separate from the mutable active workstream map.
Not changed:
  - No planning-use-case-map.md routing change in this batch.
  - No critical-thinking command added.
  - No generic Action Overview added.
  - No Tampermonkey script added.
  - No source/domain/slice files edited.
Follow-ups:
  Choose the next slice: route reusable command examples from the root use-case map, add the крит command, or plan the Tampermonkey MVP.
Notes:
  The active workstream map is mutable project state. The owner format remains planning/goal-process-map.md.
### 2026-05-31 - Renamed and synchronized Goal Map files

Date:
  2026-05-31
Action:
  Renamed and synchronized Goal Map owner, example and living workstream files after adopting the compound starter artifact naming convention.
Type:
  root planning aid / goal-map file naming sync
Status:
  applied
Why:
  Goal Map is a compound starter artifact that intentionally combines principles, workflow and template. The owner filename should make that combined responsibility explicit while the example and living map stay separate.
Changed files:
  - planning/goal-map-principles-workflow-template.md
  - planning/goal-map-example.md
  - planning/workstreams/command-system-and-tampermonkey-goal-map.md
  - planning/planning-use-case-map.md
  - planning/documentation-action-log.md
Removed / renamed paths:
  - planning/goal-process-map.md
  - planning/goal-process-map-example.md
  - planning/workstreams/command-system-and-tampermonkey-goal-process.md
Updates:
  - Renamed the Goal Map owner to reflect principles + workflow + template responsibility.
  - Renamed the static example and living workstream map to match Goal Map naming.
  - Added Current Snapshot to the living workstream map so it can be opened at any time to see the current goal and progress.
  - Synchronized root use-case map links to the new file names.
  - Recorded that Tampermonkey command expansions must include execution reminders, key boundaries and command-specific details, not only command labels.
Not changed:
  - No filename-version migration performed.
  - No Doc version headers removed.
  - No critical-thinking command added.
  - No generic Action Overview added.
  - No Tampermonkey script added.
  - No source/domain/slice docs edited.
Follow-ups:
  Add reusable architecture policy for compound starter artifacts and filename-version migration in a separate batch. Then choose the next Goal Map slice: route reusable command examples, add critical-thinking command, or plan Tampermonkey MVP.
Notes:
  This package uses replacement files for renamed Goal Map files and targeted inline updates for existing root route/action-log files.
### 2026-05-31 - Improved Goal Map roadmap and evidence readability

Date:
  2026-05-31
Action:
  Updated the active command-system/Tampermonkey Goal Map and Goal Map owner rules so roadmap records are more readable, evidence-backed and usable as a planning reference.
Type:
  root planning aid / living Goal Map readability and planning-reference update
Status:
  applied
Why:
  The user needs to open the living Goal Map at any time and clearly understand current goal, progress, roadmap, completed evidence, active work and next action. Planning inside the workstream should consult the living Goal Map instead of improvising a new plan from chat context.
Changed files:
  - planning/workstreams/command-system-and-tampermonkey-goal-map.md
  - planning/goal-map-principles-workflow-template.md
  - planning/planning-use-case-map.md
  - planning/documentation-action-log.md
Updates:
  - Added a roadmap-first structure after Current Snapshot.
  - Replaced markdown task-list style roadmap records with status labels such as DONE / NOW / NEXT / TODO / BLOCKED.
  - Added proof-oriented DONE item pattern with Visible evidence and per-evidence details dropdowns.
  - Clarified that visible evidence should identify added files, renamed paths, changed files, changed sections, route entries and action-log records.
  - Clarified that slices are work directions: needed behavior -> plan -> artifacts/actions/steps -> verification -> visible evidence.
  - Added planning-reference rules requiring agents to consult the active living Goal Map when planning inside a long-running workstream.
Not changed:
  - No Tampermonkey script added.
  - No critical-thinking command added.
  - No generic Action Overview implementation added.
  - No source/domain/slice docs edited.
Follow-ups:
  Choose the next Goal Map slice: route reusable command examples from the root use-case map, add the critical-thinking command `крит`, or plan the Tampermonkey MVP.
Notes:
  The living Goal Map stores current state and evidence capsules. The action log remains append-only chronological history and should not be fully merged into the map.

### 2026-05-31 - Cleaned static Goal Map example

Date:
  2026-05-31
Action:
  Synchronized the static Goal Map example with the current roadmap/evidence/dropdown demonstration format.
Type:
  documentation cleanup / goal-map static example sync
Status:
  applied
Why:
  The Goal Map owner and active living map already define the roadmap/evidence rules. The static example still demonstrated the older checkbox-based shape and stale next-action wording.
Changed files:
  - planning/goal-map-example.md
  - planning/documentation-action-log.md
Updates:
  - Replaced the static Goal Map example with a Current Snapshot + Roadmap + Whole Picture + Scenario/Slice Map + Work Directions shape.
  - Removed stale checkbox-style roadmap records from the example.
  - Demonstrated status labels, Visible evidence, Evidence required and `<details>` proof blocks.
Not changed:
  - No response workflow changes in this safety package.
  - No active living Goal Map changes in this safety package.
  - No Tampermonkey script added.
  - No critical-thinking command added.
  - No generic Action Overview implementation added.
  - No reusable command example route update applied.
Follow-ups:
  Apply response-workflow planning-reference and living-map DONE evidence in a separate safe package, or choose the next workstream slice: route reusable command examples from the root use-case map, add the critical-thinking command `крит`, or plan the Tampermonkey MVP.
Notes:
  This safety package avoids replacing broad shared workflow files after the previous package attempted stale full-file replacement. The static example remains demonstration-only and does not own Goal Map rules.


### 2026-05-31 - Routed reusable command examples from root use-case map

Date:
  2026-05-31
Action:
  Routed current reusable command examples from the root use-case map and added an Enman-specific scenario/domain/slice command-routing example.
Type:
  documentation routing / examples traversal chain
Status:
  applied
Why:
  Reusable command examples already existed and were indexed, but the root use-case map did not yet expose them as part of the traversal chain for non-trivial command work. The scenario/domain/slice route families also needed a project-specific demonstration without moving workflow logic into the root map.
Changed files:
  - planning/planning-use-case-map.md
  - planning/documentation/examples/README.md
  - planning/documentation/examples/project-specific/enman/SCENARIO-DOMAIN-SLICE-COMMAND-ROUTING-EXAMPLE.md
  - planning/workstreams/command-system-and-tampermonkey-goal-map.md
  - planning/documentation-action-log.md
Updates:
  - Added reusable command example reference table to the root use-case map.
  - Linked answer-shape, planning, archive/source-vs-output, Goal Map and scenario/domain/slice routes to existing/current examples.
  - Added Enman-specific scenario/domain/slice command-routing demonstration.
  - Indexed the new project-specific example.
  - Updated the active Goal Map with DONE evidence for F7-CMD-1B and next-slice orientation.
Not changed:
  - No response workflow semantics changed.
  - No use-case map workflow/template semantics changed.
  - No Tampermonkey script added.
  - No critical-thinking command added.
  - No generic Action Overview implementation added.
Follow-ups:
  Next recommended slice is F7-CMD-2: add the critical-thinking command `крит`, including aliases, honest evaluation behavior, boundaries, example coverage and root route.
Notes:
  Examples remain demonstration-only. Root use-case map remains the concrete router. Workflows/templates keep command semantics and output rules.

### 2026-06-01 - Documented compound starter artifact and filename-version policy

Date:
  2026-06-01
Action:
  Documented the reusable compound starter artifact rule and filename-version migration policy.
Type:
  documentation architecture / reusable concept-artifact governance
Status:
  applied
Why:
  The Goal Map owner already uses a combined principles + workflow + template file, and future concepts such as a goal map or similar planning concepts need a safe way to start with one strict compound owner file while keeping examples separate. The repo also needed to clarify that moving version identity from `Doc version:` headers into filenames is a planned migration, not an incidental rename.
Changed files:
  - planning/documentation/planning-docs-architecture-principles.md
  - planning/documentation/documentation-responsibility-map.md
  - planning/planning-use-case-map.md
  - planning/documentation-action-log.md
Updates:
  - Added a compound starter artifact principle for early cohesive concepts that combine principles, workflow and template.
  - Required compound starter artifact names to expose the combined responsibility and keep examples in separate files.
  - Added split-later conditions for when a compound starter artifact should become separate principles/workflow/template files.
  - Added a filename-version migration policy that keeps current `Doc version:` headers valid until an explicit migration updates paths, registers, local Sources blocks, README/read-order references and use-case map links.
  - Routed new concept / planning file creation through the root use-case map to the architecture principles, documentation responsibility map and example coverage workflow.
Not changed:
  - No filename-version migration performed.
  - No files renamed.
  - No `Doc version:` headers removed.
  - No Goal Map files changed.
  - No domain/scenario/slice/source files edited.
  - No critical-review command added.
Follow-ups:
  After this policy is reviewed, continue with the next command-system workstream slice, currently F7-CMD-2: add the critical-review command `крит`.
Notes:
  This batch records the reusable rule behind the already-applied Goal Map compound owner naming. It does not make filename versions the active repo-wide convention yet.

### 2026-06-02 - Added Goal Map Brief response output rule

Date:
  2026-06-02
Action:
  Added a response-level Goal Map Brief rule, command route and reusable example.
Type:
  response output / goal-map command routing
Status:
  applied
Why:
  Long-running workstreams need a compact in-answer projection of the living Goal Map so the user can see the current goal, current slice, selected work chain and other slice statuses without opening the full living map.
Changed files:
  - planning/documentation/reviewable-agent-output-and-commands-workflow.md
  - planning/goal-map-principles-workflow-template.md
  - planning/planning-use-case-map.md
  - planning/documentation/examples/README.md
  - planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md
  - planning/documentation-action-log.md
Updates:
  - Added `кц`, `карта цели кратко` and `goal map brief` as response-level commands for compact Goal Map projection.
  - Placed Goal Map Brief after `Краткое саммари` and before `Итог` when `Итог` applies.
  - Defined the brief shape: current slice expanded with Why now / Done / Now / Next / After, and other slices shown only as a compact status table.
  - Added a reusable example using the current command-system/Tampermonkey Goal Map slice chain.
Not changed:
  - No living Goal Map structure changed.
  - No living Goal Map state updated.
  - No command-system `крит` implementation added.
  - No domain/scenario/slice/source docs edited.
  - No files renamed.
Follow-ups:
  Use the new Goal Map Brief output rule in future Level 2/3 planning/status/continuation answers for active long-running workstreams, then continue with F7-CMD-2: add the critical-review command `крит`.
Notes:
  The brief is a response projection of the living map. It does not replace the durable living Goal Map and does not grant edit/package/commit permission.

### 2026-06-02 - Added critical review command

Date:
  2026-06-02
Action:
  Added the response-level critical review command `крит`.
Type:
  response command / critical review modifier
Status:
  applied
Why:
  The command-system workstream needed an explicit command that makes the assistant evaluate a plan, decision, answer, draft or route honestly instead of automatically agreeing.
Changed files:
  - planning/documentation/reviewable-agent-output-and-commands-workflow.md
  - planning/planning-use-case-map.md
  - planning/documentation/examples/README.md
  - planning/documentation/examples/CRITICAL-REVIEW-COMMAND-EXAMPLE.md
  - planning/workstreams/command-system-and-tampermonkey-goal-map.md
  - planning/documentation-action-log.md
Updates:
  - Added `крит`, `критически`, `критически оцени`, `проверь критически`, `оцени честно`, `не соглашайся автоматически`, `за и против` and `critical review` as critical-review command aliases.
  - Defined `крит` as a response-level modifier command that keeps the underlying task route and applies honest critical-review answer mode.
  - Added output shape: target, verdict, strong points, weak points/risks, hidden assumptions, alternatives/adjustments and confidence/checks.
  - Added reusable command example for expected behavior and boundaries.
  - Updated the command-system/Tampermonkey living Goal Map to mark SL-4 as done / validating and move next-action choice to Generic Action Overview vs Tampermonkey MVP planning.
Not changed:
  - No project-specific Enman scenario/domain/slice command-routing example updated.
  - No Goal Map Brief example changed.
  - No domain/scenario/slice/source docs edited.
  - No archive/output package command changed.
  - No files renamed.
Follow-ups:
  Choose the next workstream slice: F7-CMD-4 Generic Action Overview or TM-0 Tampermonkey MVP planning.
Notes:
  `крит` does not grant edit/package/commit permission and should not become disagreement for its own sake.

### 2026-06-02 - Synchronized Goal Map statuses after critical review command

Date:
  2026-06-02
Action:
  Added Goal Map status synchronization rules and cleaned up the active command-system/Tampermonkey living map statuses.
Type:
  goal-map workflow / living map status cleanup
Status:
  applied
Why:
  After `F7-CMD-2`, the Current Snapshot said SL-4 was completed, but earlier roadmap phases and detailed slice records could still be read as `NOW` or in progress. This made compact Goal Map Brief output ambiguous.
Changed files:
  - planning/goal-map-principles-workflow-template.md
  - planning/workstreams/command-system-and-tampermonkey-goal-map.md
  - planning/documentation-action-log.md
Updates:
  - Added a status synchronization checklist for living Goal Maps.
  - Clarified that `▶ NOW` should be used only for the actually active slice/work direction.
  - Clarified that Goal Map Brief should use detailed slice statuses, not roadmap phase statuses.
  - Normalized command-system/Tampermonkey map statuses after SL-4 completion.
  - Marked the current focus as a next-slice decision instead of leaving completed work as active.
Not changed:
  - No new command semantics added.
  - No examples added or changed.
  - No domain/scenario/slice/source docs edited.
  - No Tampermonkey MVP planning started.
  - No Generic Action Overview implementation started.
Follow-ups:
  Choose the next workstream slice: F7-CMD-4 Generic Action Overview or TM-0 Tampermonkey MVP planning.
Notes:
  This cleanup keeps the living map consistent with the current planning state before selecting the next slice.

