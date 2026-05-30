# Principles Responsibility Classification

Status: candidate review artifact / pre-split classification  
Scope: section-by-section responsibility-zone classification for `planning/documentation/planning-docs-architecture-principles.md`

## 1. Purpose

This artifact classifies every section of the candidate principles file before any split, rewrite or genericization.

Target file:

```text
planning/documentation/planning-docs-architecture-principles.md
```

Method owner:

```text
planning/documentation/documentation-responsibility-zone-review-workflow.md
```

This artifact does **not** split, rename, rewrite or move the candidate principles file. It only records the responsibility-zone classification and proposed next actions.

## 2. Review Boundary

This is a documentation-layer architecture review.

When a section mentions paths such as:

```text
planning/slices/
planning/diagrams/
planning/api/
planning/testing/
planning/vkr-clean-reference.md
```

the review question is:

```text
Why does the documentation layer reference this path?
Is the reference a reusable principle, specialized profile pattern, adapter mapping, example or workflow/field-kit detail?
```

The review does not judge whether the scenario, slice, API, domain or VKR content itself is correct.

## 3. Classification Buckets

| Bucket | Meaning | Typical future owner |
|---|---|---|
| `reusable-core` | Fully reusable documentation architecture invariant or file-type theory. | reusable principles |
| `scenario-driven-profile` | Useful for scenario/domain/slice app or product projects, but not universal. | scenario/domain/slice profile |
| `enman-project-adapter` | Exact Enman paths, VKR/thesis setup, IDs, evidence maps, register targets or project layer vocabulary. | project / Enman adapter |
| `example-candidate` | Concrete case that demonstrates a rule but should not own reusable logic. | examples |
| `workflow-detail` | Repeated operational process detail. | workflow |
| `field-kit-detail` | Setup/model detail used to derive project-specific artifacts. | field kit |
| `mixed` | Section contains more than one responsibility zone. | split in F4/F5 |
| `keep-as-is-for-now` | Safe to leave in candidate until the split package is planned. | candidate baseline |

Review rule:

> Do not move a paragraph to an adapter only because it contains a project path. First extract the reusable principle; then classify the remaining concrete mapping.

## 4. Summary Classification

| Section | Classification | Main reason | Proposed next action |
|---|---|---|---|
| 1. Purpose | mixed, mostly reusable-core | Defines docs as a knowledge system, but mentions implementation-state verification. | Keep core; genericize implementation wording to evidence/current-reality model. |
| 1A. Principles Responsibility Boundary | reusable-core | Defines principles as invariants and excludes workflow/template/project config. | Keep as core reusable principles content. |
| 2. Core Goals | mixed | Goals are reusable, but VKR/thesis wording is Enman-specific. | Keep goals; replace VKR phrase with external/public-facing output principle. |
| 3. Fixed Layer Architecture | mixed | Defines layer/dependency idea but hardcodes scenario/domain/slice/VKR topology. | Split into reusable layer principle, scenario-driven profile and project adapter material. |
| 4. Responsibility Map Model | mixed, mostly reusable-core | Root/local routing model is reusable; exact root/local file paths are project config. | Keep model; move exact path mapping to adapter/profile or example. |
| 5. One Main Entry Point | mixed | Entry-point principle is reusable; `planning/README.md` and listed concerns are Enman/current-project mapping. | Genericize entrypoint principle; adapter owns concrete entrypoint and topics. |
| 6. File Type Responsibility Model | mixed, mostly reusable-core | File-type taxonomy is reusable; some type examples are scenario/VKR-specific. | Keep taxonomy; genericize examples; move VKR-specific type to adapter/external-output note. |
| 7. Index vs Register vs Source Usage Register | reusable-core | Defines reusable file-type distinctions. | Keep in reusable principles. |
| 8. Local Notes vs Register | mixed, mostly reusable-core | Local vs shared visibility rule is reusable; examples mention future slice/domain/scenario. | Keep rule; genericize examples or link profile/adapter. |
| 9. Template vs Workflow | reusable-core | Defines reusable boundary and example policy. | Keep in reusable principles. |
| 10. Current/Target/Draft/Archive/Dirty Draft | mixed | State distinction is reusable; “current implementation” evidence is software/project-specific. | Keep state model; add applicability/evidence-model wording. |
| 11. Avoid Heavy Current-State Docs | mixed | Anti-stale-current-state principle is reusable; evidence examples and thesis evidence maps are project/domain-specific. | Keep principle; route exact evidence examples to status workflow/adapter. |
| 12. Source-of-Truth Hierarchy | mixed, heavy split | Reusable source hierarchy principle is mixed with exact Enman paths. | Extract generic source hierarchy; move scenario chain to profile and exact paths to adapter. |
| 12A. Layer Encapsulation And Attention Preservation | mixed | Encapsulation/source-consumer invariant is reusable; scenario/domain/slice/VKR layer explanation is specialized/project-specific. | Keep invariant; move layer topology to profile; source-consumer model to field kit if detailed. |
| 13. Source / Version Principle | mixed | Source-version principle is reusable; row shape is field-kit/register detail; DATA/behavior note is scenario profile. | Keep high-level principle; move row model to source usage field kit. |
| 14. Dependency Cascade Principle | mixed | Cascade principle is reusable; scenario→DATA→domain→slice→tests/VKR example is profile/example. | Keep principle; move example to scenario profile or example file. |
| 15. Section-Level Sources Principle | mixed | Section source transparency is reusable; minimal block is workflow/template detail. | Keep principle; route block shape to reviewable-output/source-usage workflow or field kit. |
| 16. Local Detail + Global Visibility | mixed | Local/global rule is reusable; exact local/global examples and registers are adapter/example material. | Keep principle; move exact register map to project adapter/shared visibility map. |
| 17. Responsibility Ownership | mixed, mostly reusable-core | Owner-zone principle is reusable; exact map paths are current project config. | Keep principle; adapter or README owns exact paths. |
| 18. Safe Rewrite Rule | mixed | Safety principle is reusable; examples include scenario/API/domain/slice/VKR project categories. | Keep principle; classify examples into profile/adapter/examples. |
| 18A. Decomposable File Architecture | reusable-core | Defines reusable maintainability/refactor signal. | Keep in reusable principles. |
| 19. Documentation Update Plans | reusable-core | Defines reusable planning-before-change principle. | Keep; link workflow. |
| 20. AI-Checkability Principles | mixed, mostly reusable-core | Checkability is reusable; implementation evidence wording is domain-specific. | Keep; genericize implementation evidence to evidence/current-reality model. |
| 21. VKR / Thesis Separation | mixed, mostly enman-project-adapter | Reusable core is internal vs external-facing output; named VKR/thesis layer is Enman-specific. | Move VKR specifics to adapter; keep generic external-output principle. |
| 22. Dirty Draft Policy | mixed | Non-canonical recovery concept is reusable; VKR/canonical path implications are project-specific. | Keep concept; move exact recovery location and VKR wording to adapter. |
| 23. Direct Edits, Archives And Commit Granularity | mixed, mostly reusable-core | Output-mode principle is reusable; detailed package process belongs to workflow/guide. | Keep high-level; route details to update/replacement workflow. |
| 24. No-Duplication / Authority Rule | reusable-core | Defines reusable authority/owner boundary. | Keep in reusable principles. |
| 24A. Link Instead Of Copy / Docs DRY Rule | reusable-core | Defines reusable DRY/source-of-truth invariant. | Keep in reusable principles. |
| 24B. Accepted Command And Preservation Guardrails | mixed, mostly reusable-core | Preservation guardrail is reusable; command-specific details live elsewhere. | Keep architecture-level guardrail; route command specifics to use-case/replacement docs. |
| 24C. Responsibility-Zone Review Guardrail | reusable-core | Defines reusable review-before-moving rule. | Keep in reusable principles. |
| 25. What Not To Add By Default | reusable-core | Prevents overengineering and false orchestration. | Keep in reusable principles. |
| 26. Success Criteria | mixed | Success criteria are reusable, but current implementation/VKR/root-map wording is project/domain-specific. | Keep generic criteria; profile/adapter owns project-specific criteria. |

## 5. Detailed Section Notes

### 1. Purpose

Current purpose: explain that planning documentation is a knowledge system and must help future readers/chats find owners, canonical files, local details, historical/non-canonical files and verification paths.

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
Documentation is a knowledge system.
It must make ownership, source-of-truth, local/global scope, canonical/non-canonical status and safe update paths visible.
```

Project/domain-specific part:

```text
how to verify current implementation state
```

Reason: not every documentation domain has implementation. Some domains have notes, tasks, calendar state, research sources or other evidence/current-reality models.

Proposed F4 action:

```text
Keep and genericize: "how to verify current implementation state" -> "how to verify current evidence / current reality when the domain has one."
```

### 1A. Principles Responsibility Boundary

Classification: `reusable-core`.

Reason: this section already defines principles as stable invariants and explains that workflows, templates, responsibility maps, use-case maps, field kits, adapters and examples own different responsibilities.

Proposed F4 action:

```text
Keep almost as-is in reusable principles.
```

### 2. Core Goals

Classification: `mixed`.

Reusable core to preserve:

```text
Docs should be navigable, reviewable, safe to update, clear about source boundaries, clear about state categories and clear about ownership.
```

Enman-specific part:

```text
useful for VKR/thesis wording
```

Reason: VKR/thesis is a concrete Enman output layer, not a universal docs-layer goal.

Proposed F4 action:

```text
Replace VKR/thesis wording with "external/public-facing output wording when a project has such an output layer."
Move named VKR/thesis specifics to adapter.
```

### 3. Fixed Layer Architecture

Classification: `mixed`.

Reusable core to preserve:

```text
A documentation system should define layers and dependency direction before file-type placement.
Documentation governance is a meta-layer that defines how docs are structured and updated.
```

Scenario-driven profile part:

```text
scenario sources -> DATA/behavior items -> domain -> slice -> API/testing/evidence
```

Enman-specific / adapter part:

```text
VKR/thesis as named layer
exact layer vocabulary if it is current-project-specific
```

Reason: the layer idea is universal; the scenario/domain/slice pipeline is reusable for a class of app/product projects; VKR/thesis is Enman-specific.

Proposed F4 action:

```text
Rename/rewrite from "Fixed Layer Architecture" to "Project-Defined Layer Architecture."
Move the current layer chain to scenario-domain-slice docs profile.
Move VKR/thesis to project adapter.
```

### 4. Responsibility Map Model

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
Responsibility maps are routing tools.
Root map routes between layers.
Local maps route inside layers.
README/index files provide navigation and read order.
```

Adapter/config part:

```text
planning/planning-doc-responsibility-map.md
planning/documentation/documentation-responsibility-map.md
```

Reason: the two-level map model is reusable, but exact file paths are this repo's configuration.

Proposed F4 action:

```text
Keep model in principles.
Move exact path examples to project adapter or keep as short linked examples if candidate remains repo-aware.
```

### 5. One Main Entry Point

Classification: `mixed`.

Reusable core to preserve:

```text
A documentation system should have one main entry point that owns navigation and source-of-truth routing, not detailed current-state inventory.
```

Adapter/profile part:

```text
planning/README.md
scenario behavior / slice scope / architecture/API/client/testing / VKR / dirty drafts / implementation evidence
```

Reason: the entrypoint concept is reusable; listed routing concerns are scenario-driven and Enman-specific.

Proposed F4 action:

```text
Keep generic entrypoint principle.
Move concrete entrypoint path and routing topics to project adapter.
```

### 6. File Type Responsibility Model

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
Every document should have an understandable type.
File types must have distinct responsibilities.
```

Scenario/profile examples:

```text
source specs / DATA sets / behavior item sets
scenario/domain/slice/documentation task drafts
```

Enman adapter part:

```text
VKR clean reference
```

Reason: taxonomy is reusable, but some examples are specialized/project-specific.

Proposed F4 action:

```text
Keep core taxonomy.
Genericize scenario-specific examples or move them into scenario-driven profile.
Move VKR clean reference to adapter as external-output reference example.
```

### 7. Index vs Register vs Source Usage Register

Classification: `reusable-core`.

Reason: the distinction between navigation/index, shared state register and source usage register is reusable across documentation systems.

Proposed F4 action:

```text
Keep in reusable principles.
```

### 8. Local Notes vs Register

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
Local notes stay local only when they affect one file.
Shared/future-impacting information belongs in a discoverable register/index.
Registers need enough fields to avoid becoming a garbage pile.
```

Specialized examples:

```text
future slice/domain/scenario
```

Proposed F4 action:

```text
Keep principle and generic field list.
Move or generalize scenario/slice/domain examples.
```

### 9. Template vs Workflow

Classification: `reusable-core`.

Reason: workflow vs template distinction and example support boundaries are reusable.

Proposed F4 action:

```text
Keep in reusable principles.
```

### 10. Separate Current, Target, Draft, Archive And Dirty Draft

Classification: `mixed`.

Reusable core to preserve:

```text
Current, target, draft, archive/handoff and dirty draft are distinct states with different authority levels.
Status snapshots can be useful historically but become stale if treated as current truth.
```

Domain-specific part:

```text
current implementation from code/tests/migrations/generated contracts/screenshots
domain or slice draft examples
```

Reason: software implementation evidence applies to app projects; other docs domains need their own evidence/current-reality model.

Proposed F4/F5 action:

```text
Keep state model in reusable principles.
Route evidence-order setup to status reconciliation workflow/field kit.
Move domain/slice examples to scenario-driven profile.
```

### 11. Avoid Heavy Current-State Docs

Classification: `mixed`.

Reusable core to preserve:

```text
Avoid large current-state docs that duplicate volatile truth.
Prefer stable navigation, source-of-truth rules and scoped historical/status notes.
```

Project/domain-specific examples:

```text
current branch/code/tests/generated contracts/screenshots
evidence maps for thesis-facing claims
```

Proposed F4/F5 action:

```text
Keep anti-stale-current-state principle.
Genericize evidence examples.
Move thesis/VKR evidence maps to adapter.
```

### 12. Source-of-Truth Hierarchy

Classification: `mixed`, heavy split.

Reusable core to preserve:

```text
Use the right source for the question.
Every project should define canonical sources per question type.
Recovery material should be lower authority than canonical docs.
```

Scenario-driven profile part:

```text
scenario behavior truth
domain direction
slice scope truth
API/testing decisions
```

Enman adapter mapping:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/slices/
planning/slices/l2/
planning/slices/cross-cutting/
planning/architecture/
planning/api/
planning/client/
planning/testing/
planning/adr/
planning/vkr-clean-reference.md
planning/thesis/
planning/dirty-drafts/
```

Workflow/status part:

```text
implementation truth verification model
```

Proposed F4 action:

```text
Extract a generic "source hierarchy must be project-defined" principle.
Move scenario/domain/slice source categories to scenario-driven profile.
Move exact paths to project adapter.
Move implementation evidence order to status reconciliation setup/profile.
```

### 12A. Layer Encapsulation And Attention Preservation

Classification: `mixed`.

Reusable core to preserve:

```text
Downstream docs should reference reviewed upstream artifacts instead of reconstructing upstream reasoning.
Source/consumer relationships should support targeted cascade review and preserve review attention.
External dependencies and internal section dependencies are different.
```

Scenario-driven profile part:

```text
scenario, DATA/behavior, domain, slice, testing layer ownership
```

Enman-specific part:

```text
diagramming / VKR / thesis as named consuming layers
```

Field-kit detail:

```text
source artifact/scope -> consumer artifact/scope -> reviewed_against / sync_status / review_outcome
```

Reason: the core model is source usage/cascade governance; the row/model details likely belong in a source usage field kit.

Proposed F4/F5 action:

```text
Keep encapsulation invariant in principles.
Move scenario/domain/slice layer ownership to profile.
Move source-consumer model details to source usage cascade field kit if it becomes procedural/setup guidance.
Move VKR/thesis references to adapter.
```

### 13. Source / Version Principle

Classification: `mixed`.

Reusable core to preserve:

```text
Source versions/review markers should be tracked where downstream synchronization matters.
Exact rows live outside principles.
```

Field-kit detail:

```text
source file path
content_version
reviewed_against / derived_from
sync_status
last_reviewed
consumer file / local file
```

Scenario-driven profile part:

```text
DATA sets and behavior item sets can have separate versions for the same scenario.
```

Proposed F4/F5 action:

```text
Keep high-level source/version principle.
Move row model to source usage cascade field kit/register template.
Move DATA/behavior version example to scenario-driven profile.
```

### 14. Dependency Cascade Principle

Classification: `mixed`.

Reusable core to preserve:

```text
When an upstream source changes, downstream files that consume it need targeted review.
Review can result in no content change, metadata update or follow-up.
```

Scenario/profile example:

```text
scenario spec -> DATA set -> behavior items -> domain drafts -> slice drafts -> tests/status/VKR claims
```

Enman-specific part:

```text
VKR claims
```

Proposed F4/F5 action:

```text
Keep cascade principle.
Move example to scenario-driven profile or examples.
Move VKR-specific part to adapter.
```

### 15. Section-Level Sources Principle

Classification: `mixed`.

Reusable core to preserve:

```text
Important/high-risk sections should make their basis reviewable.
```

Workflow/template detail:

```text
minimal major-section source block shape
Sources: Format / Content / Internal dependencies / Not checked
```

Reason: the principle belongs in principles; exact answer/block formatting belongs in reviewable-output or source usage workflow/field kit.

Proposed F4/F5 action:

```text
Keep principle.
Move or link block shape to reviewable-agent-output-and-commands-workflow.md or source usage field kit.
```

### 16. Local Detail + Global Visibility

Classification: `mixed`.

Reusable core to preserve:

```text
Local docs own detailed context.
Global indexes/registers own discoverability.
Local details that affect future work should be mirrored into shared visibility or marked local-only.
```

Example/profile part:

```text
one slice file, client sidecar, scenario spec, DATA spec, cross-cutting slice, ADR candidate note
```

Adapter mapping:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-questions-register.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

Proposed F4/F5 action:

```text
Keep local/global invariant.
Move exact shared visibility map to project adapter.
Move examples to scenario-driven profile or example file.
```

### 17. Responsibility Ownership

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
A file should contain only content that belongs to its responsibility zone.
Choose layer first, then use the local responsibility map or README.
```

Adapter/config part:

```text
planning/planning-doc-responsibility-map.md
planning/documentation/documentation-responsibility-map.md
```

Proposed F4 action:

```text
Keep owner-zone principle.
Move exact map paths to adapter/README navigation as needed.
```

### 18. Safe Rewrite Rule

Classification: `mixed`.

Reusable core to preserve:

```text
Markdown wording changes can be semantically unsafe.
Behavior, scope and source-of-truth changes must be explicit in the plan.
```

Scenario/software/project examples:

```text
scenario behavior
API contract
domain rule
security requirement
testing responsibility
architecture boundary
slice scope
status of implemented vs planned work
VKR claim
```

Proposed F4 action:

```text
Keep safe rewrite principle.
Move examples into scenario-driven profile / project adapter / examples.
Move VKR claim example to adapter.
```

### 18A. Decomposable File Architecture

Classification: `reusable-core`.

Reason: the split/refactor criteria are reusable across docs systems, including large/shared files and script-vs-replacement safety.

Proposed F4 action:

```text
Keep in reusable principles.
```

### 19. Documentation Update Plans

Classification: `reusable-core`.

Reason: broad documentation changes should be planned before application; this is reusable.

Proposed F4 action:

```text
Keep in reusable principles and link update-plan workflow.
```

### 20. AI-Checkability Principles

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
Docs should let future chats/persons see checked/not-checked files, canonical sources, assumptions, status, scope, forbidden changes and verification method.
```

Domain-specific part:

```text
implemented/planned/deferred
current implementation evidence
```

Reason: status labels and implementation evidence are project/domain-specific.

Proposed F4/F5 action:

```text
Keep checkability principle.
Genericize implementation evidence as current evidence/current-reality model.
Route status label setup to status reconciliation workflow/profile.
```

### 21. VKR / Thesis Separation

Classification: `mixed`, mostly `enman-project-adapter`.

Reusable core to preserve:

```text
Internal working documentation terms should not leak into external/public-facing outputs.
```

Enman-specific material:

```text
VKR-facing materials
planning/vkr-clean-reference.md
planning/thesis/
presentation / defense speech / practice-report wording
```

Reason: VKR/thesis is a current-project output layer.

Proposed F4 action:

```text
Keep generic "internal vs external-facing output separation" in reusable principles.
Move VKR/thesis specifics to project adapter and/or Enman examples.
```

### 22. Dirty Draft Policy

Classification: `mixed`.

Reusable core to preserve:

```text
Dirty drafts/recovery notes are useful but non-canonical.
They may preserve raw reasoning but must not override canonical docs/current evidence.
Useful stable decisions should be promoted through normal sync.
```

Enman/project-specific part:

```text
copying stale terms into VKR
canonical scenario/slice/domain docs
```

Proposed F4 action:

```text
Keep dirty-draft/non-canonical recovery principle.
Move VKR and scenario/slice/domain examples to adapter/profile.
```

### 23. Direct Edits, Archives And Commit Granularity

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
Documentation architecture should support direct edits and replacement packages.
Output modes do not replace source-of-truth rules.
```

Workflow/process detail:

```text
prefer direct GitHub edits
one file per commit
include MANIFEST and APPLY instructions
complete files rather than partial patches
```

Reason: details belong to documentation-update workflow and replacement-file-generation guide, while principles should keep only the invariant/boundary.

Proposed F4/F5 action:

```text
Keep output-mode boundary principle.
Move detailed steps or keep as short links to owner workflows/guides.
```

### 24. No-Duplication / Authority Rule

Classification: `reusable-core`.

Reason: topic authority and owner-level responsibility are reusable.

Proposed F4 action:

```text
Keep in reusable principles.
```

### 24A. Link Instead Of Copy / Docs DRY Rule

Classification: `reusable-core`.

Reason: avoiding duplicate reusable logic is a core architecture invariant.

Proposed F4 action:

```text
Keep in reusable principles.
```

### 24B. Accepted Command And Preservation Guardrails

Classification: `mixed`, mostly `reusable-core`.

Reusable core to preserve:

```text
Accepted commands/output modes must not be silently reinterpreted.
If accepted mode cannot be completed safely, stop and explain blocker.
Post-apply verification should check preservation, not only application.
```

Routing/workflow detail:

```text
command-specific details live in planning-use-case-map and replacement-file-generation-guide
```

Proposed F4 action:

```text
Keep architecture-level command/preservation guardrail.
Ensure detailed command semantics stay in use-case/replacement workflows.
```

### 24C. Responsibility-Zone Review Guardrail

Classification: `reusable-core`.

Reason: this is the core meta-rule for future responsibility reviews and portability splits.

Proposed F4 action:

```text
Keep in reusable principles.
```

### 25. What Not To Add By Default

Classification: `reusable-core`.

Reason: anti-overengineering guardrail is reusable.

Proposed F4 action:

```text
Keep in reusable principles.
```

### 26. Success Criteria

Classification: `mixed`.

Reusable core to preserve:

```text
A docs architecture works when entrypoint, source boundaries, navigation, routing, local/global visibility, non-canonical status, small-scope updates and single-owner reusable logic are clear.
```

Project/domain-specific material:

```text
planning/README.md
current implementation
root responsibility map
local responsibility maps
shared registers
VKR-facing wording
```

Proposed F4 action:

```text
Keep generic success criteria.
Move exact repo paths and VKR/implementation wording to project adapter/profile or genericize as evidence/external-output criteria.
```

## 6. Proposed F4 Targets

F3 does not create these files. They are candidate outputs for the next batch after this classification is reviewed.

| Future target | Role | Source sections |
|---|---|---|
| `documentation-architecture-principles.md` or revised `planning-docs-architecture-principles.md` | Fully reusable principles/invariants. | 1, 1A, 2 genericized, 4 genericized, 6–9, 10–11 genericized, 12–16 principles only, 17–20, 21 genericized, 22–26 genericized |
| `scenario-domain-slice-docs-profile.md` | Specialized reusable app/product project profile. | 3, 12, 12A, 13, 14, 16, 18 examples |
| `project-docs-adapter.md` or `enman-docs-adapter.md` | Exact Enman/project mapping. | exact paths in 4, 5, 12, 16, 21, 22, 26; VKR/thesis specifics |
| `source-usage-cascade-field-kit.md` | Setup toolkit for source/consumer/cascade relationships, if split out. | 12A, 13, 14, 15 |
| Example files or examples index rows | Concrete demonstrations separated from reusable owners. | 14, 16, 18, 21 |

## 7. F4 Planning Notes

F4 should not simply delete project-specific content.

For each mixed section, F4 should preserve three things when present:

```text
1. reusable principle;
2. specialized profile pattern;
3. concrete Enman/project mapping or example.
```

The main F4 risk is losing useful project-specific knowledge while cleaning reusable principles. The adapter/profile and examples should prevent that.

## 8. Open Questions For F4

1. Should the reusable principles file keep the current name `planning-docs-architecture-principles.md`, or should candidate introduce `documentation-architecture-principles.md`?
2. Should the project adapter be named `project-docs-adapter.md` or `enman-docs-adapter.md`?
3. Should source usage field-kit extraction happen in the same batch as principles split, or after the three key files are reviewed?
4. Should exact path examples remain as short examples in reusable candidate docs, or only as links to adapter/examples?
5. Should external-output separation be a standalone principle, or part of core goals/success criteria?

## 9. Final F3 Decision

No principles split should happen until this classification is reviewed.

Next action after F3 acceptance:

```text
Plan F4 candidate principles split/genericization using this classification as the source of truth.
```
