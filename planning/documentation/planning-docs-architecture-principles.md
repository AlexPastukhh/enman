# Planning Docs Architecture Principles

Status: active reusable documentation architecture principles  
Scope: reusable documentation architecture invariants for docs systems, with project-specific and scenario-driven material split into profile/adapter files

> Active boundary: this file owns reusable documentation architecture invariants. Concrete Enman routing and project-specific configuration remain in `planning/planning-use-case-map.md` and root planning profiles.

## Contents

- [1. Purpose](#1-purpose)
- [1A. Principles Responsibility Boundary](#1a-principles-responsibility-boundary)
- [2. Core Goals](#2-core-goals)
- [3. Project-Defined Layer Architecture](#3-project-defined-layer-architecture)
- [4. Responsibility Map Model](#4-responsibility-map-model)
- [5. One Main Entry Point](#5-one-main-entry-point)
- [6. File Type Responsibility Model](#6-file-type-responsibility-model)
- [7. Index vs Register vs Source Usage Register](#7-index-vs-register-vs-source-usage-register)
- [8. Local Notes vs Register](#8-local-notes-vs-register)
- [9. Template vs Workflow](#9-template-vs-workflow)
- [10. Current Reality, Target, Draft, Archive And Recovery Notes](#10-current-reality-target-draft-archive-and-recovery-notes)
- [11. Avoid Heavy Current-State Docs](#11-avoid-heavy-current-state-docs)
- [12. Source-of-Truth / Evidence Hierarchy](#12-source-of-truth--evidence-hierarchy)
- [12A. Layer Encapsulation And Attention Preservation](#12a-layer-encapsulation-and-attention-preservation)
- [13. Source / Version Principle](#13-source--version-principle)
- [14. Dependency Cascade Principle](#14-dependency-cascade-principle)
- [15. Section-Level Sources Principle](#15-section-level-sources-principle)
- [16. Local Detail + Shared Visibility](#16-local-detail--shared-visibility)
- [17. Responsibility Ownership](#17-responsibility-ownership)
- [18. Safe Rewrite Rule](#18-safe-rewrite-rule)
- [18A. Decomposable File Architecture](#18a-decomposable-file-architecture)
- [19. Documentation Update Plans](#19-documentation-update-plans)
- [20. AI-Checkability Principles](#20-ai-checkability-principles)
- [21. Internal vs External-Facing Output Separation](#21-internal-vs-external-facing-output-separation)
- [22. Dirty Draft / Recovery Note Policy](#22-dirty-draft--recovery-note-policy)
- [23. Direct Edits, Archives And Commit Granularity](#23-direct-edits-archives-and-commit-granularity)
- [24. No-Duplication / Authority Rule](#24-no-duplication--authority-rule)
- [24A. Link Instead Of Copy / Docs DRY Rule](#24a-link-instead-of-copy--docs-dry-rule)
- [24B. Accepted Command And Preservation Guardrails](#24b-accepted-command-and-preservation-guardrails)
- [24C. Responsibility-Zone Review Guardrail](#24c-responsibility-zone-review-guardrail)
- [25. What Not To Add By Default](#25-what-not-to-add-by-default)
- [26. Success Criteria](#26-success-criteria)

## 1. Purpose

Documentation is a knowledge system, not just a folder of notes.

It must help a future reader or AI-assisted review understand:

```text
- where to start;
- which layer or responsibility zone owns the information;
- which files are canonical;
- which files are local details;
- which files are historical, scoped or non-canonical;
- how to verify current evidence/current reality when the domain has one;
- how to update docs without losing context or changing meaning silently.
```

These principles are about documentation architecture, not runtime application architecture.

## 1A. Principles Responsibility Boundary

This file owns documentation architecture principles: stable invariants and design constraints for documentation systems.

It describes:

```text
- how documentation should be structured;
- what must remain true;
- what must not be violated;
- why source-of-truth and ownership boundaries exist;
- which file-type responsibilities must stay separate.
```

It does not own:

```text
- exact workflow steps;
- exact template shapes;
- concrete project paths;
- concrete use-case or command routes;
- large example bodies;
- current project status inventory;
- project-specific source, evidence, register or output-layer mappings.
```

Rule:

```text
Principles explain why a boundary exists.
Workflows explain how to act.
Templates define exact shapes.
Responsibility maps route information to owners.
Use-case maps route user-visible actions.
Field kits help derive project-specific artifacts.
Adapters/profiles hold concrete project configuration.
Examples demonstrate usage but do not own rules.
```

## 2. Core Goals

Documentation should be:

```text
navigable for the user;
reviewable by another chat or person;
safe to update in small scoped changes;
clear about source-of-truth boundaries;
clear about current vs target vs draft vs archive/recovery notes;
clear about where new information belongs;
clear about internal working wording vs external-facing output wording when the project has external/public deliverables.
```

## 3. Project-Defined Layer Architecture

A documentation system should define its layers and dependency direction before deciding file placement.

Reusable principle:

```text
information should first be classified by responsibility layer, then by file type inside that layer.
```

This principles file does not define one universal layer stack for every project.

Projects should define their own:

```text
- layer vocabulary;
- dependency direction;
- canonical sources per layer;
- local vs shared visibility targets;
- evidence/current-reality model;
- external-output layer if any.
```

For scenario-driven app/product projects, see:

```text
planning/documentation/profiles/scenario-domain-slice-docs-profile.md
```

For the current Enman project mapping extracted from the candidate copy, see:

```text
planning/documentation-migration/enman-docs-adapter.md
```

## 4. Responsibility Map Model

Responsibility maps are routing tools.

There are two reusable levels:

```text
root responsibility map
  -> chooses the documentation/project layer and points to local responsibility maps;

local layer responsibility map
  -> decides where information belongs inside that layer.
```

Target rule:

```text
- root map routes between layers;
- local maps route inside layers;
- README/index files provide navigation and read order;
- workflows describe processes;
- registers track shared state;
- drafts/source files contain actual working content.
```

Concrete map file paths are project configuration. They belong in the project adapter/profile.

## 5. One Main Entry Point

A documentation system should have one main entry point.

Its responsibility is navigation and source-of-truth routing, not detailed current-state inventory.

It should answer:

```text
- where to start;
- where canonical source categories live;
- which indexes/registers are global;
- where non-canonical recovery notes live;
- how to verify current evidence/current reality when applicable;
- where project-specific adapter/profile mappings are defined.
```

The exact entrypoint path is project configuration.

## 6. File Type Responsibility Model

Every documentation file should have an understandable type. If a document type is unclear, future readers or chats may use it incorrectly.

| Type | Responsibility |
|---|---|
| Architecture principles | Stable invariants and design constraints for how documentation is structured; file-type theory and source-of-truth boundaries. |
| Workflow | Algorithm/process for performing a repeated operational task. |
| Field kit | Reusable setup toolkit for deriving project-specific workflows, profiles, adapters, registers or evidence maps. Not the repeated operational workflow itself. |
| Template | Exact shape of a draft, section, row, response block or output artifact. |
| Index / README | Navigation, read order, file overview and short purpose summaries. |
| Responsibility map | Where to put information; which file/layer owns what. |
| Source spec / source set | Canonical or derived source content for a specific domain/project layer. |
| Source usage register | Dependency map showing which consumer files/scopes use which source files/scopes and review versions. |
| Questions register | Shared open/accepted questions, assumptions and decisions. |
| Extension points register | Future considerations, change pressure and extension points. |
| Implementation/evidence notes register | Shared evidence-related reminders that must stay visible beyond one local draft. |
| Draft | Working planning/design content for a project/documentation task. |
| Evidence | Current proof/current reality for a documentation domain, such as code/tests/artifacts, source notes, calendar/task state, research sources or other domain-specific evidence. |
| Status snapshot | Scoped or historical status note; not current truth by itself. |
| Scoped sync note | Case-specific documentation synchronization note; not a reusable workflow. |
| Adapter / profile | Concrete project configuration: layer vocabulary, exact paths, source maps, evidence maps, shared visibility maps, output layers and example links. |
| Derived prompt | Reusable prompt assembled from canonical docs; not a canonical rule source. |
| Recovery note / dirty draft | Non-canonical recovery/context note. |
| External-output reference | Clean terminology or evidence map for project-specific external-facing deliverables, when they exist. |

## 7. Index vs Register vs Source Usage Register

Do not use these terms interchangeably.

```text
Index / README
  = navigation, read order and short overview of files.

Register
  = shared cross-file collection of questions, decisions, extension points or notes that must remain discoverable.

Source usage register
  = dependency map showing which local files/scopes use which source files/scopes and review versions.
```

A file may combine roles only when its scope says so explicitly. Otherwise, keep navigation/indexes separate from shared state tracking.

## 8. Local Notes vs Register

Local notes belong inside one local file when they affect only that file.

Use a shared register/index when information:

```text
- is unclear where it belongs yet;
- affects multiple files or future files;
- belongs to a future owner file that does not exist yet;
- is a shared accepted direction;
- is a future-use reminder that must remain visible;
- must be discoverable without reading every local draft.
```

Register entries should not become a garbage pile. They should carry at least:

```text
ID
area / applies to
status
note / question / direction
owner or future target file when known
```

## 9. Template vs Workflow

```text
Workflow = how to work.
Template = what the resulting artifact looks like.
```

A workflow may link to a template, but should not become a giant filled example.

A template should usually include:

```text
- purpose / when to use;
- required sections and field meanings;
- one annotated example if helpful;
- clean copyable template.
```

If examples become large, move them into a separate examples file or examples folder.

Examples are supporting artifacts. They demonstrate correct application of an owner workflow, template or use case. They do not own routing logic, command aliases, source modes, output modes, permission boundaries or workflow activation.

## 10. Current Reality, Target, Draft, Archive And Recovery Notes

Do not mix these concepts.

| Category | Meaning | Source of truth? |
|---|---|---|
| Current reality / evidence | What is currently true in the relevant evidence layer for the documentation domain. | Yes, but only from the domain's defined evidence/current-reality model. |
| Target direction | Where the design, plan or interpretation is intended to go. | Planning truth, not current evidence proof. |
| Draft | Working planning hypothesis. | No, unless promoted. |
| Archive / handoff | Historical application or transfer artifact. | No, unless verified against current evidence. |
| Recovery note / dirty draft | Raw recovery/context note. | No. |

A draft can describe intended behavior or target direction before it is proven/current.

A status snapshot can be useful historically, but it becomes stale if treated as permanent current truth.

## 11. Avoid Heavy Current-State Docs

Do not maintain large global current-state documents that duplicate volatile current reality in detail.

They become high-maintenance and can go stale faster than they are updated.

Prefer:

```text
stable navigation;
source-of-truth rules;
small historical/status notes when useful;
domain-specific evidence/current-reality sources for current truth;
evidence maps only where they serve a clear external/review purpose.
```

If a current-state note exists, mark whether it is historical/internal and remind readers how to verify it against the relevant evidence model.

## 12. Source-of-Truth / Evidence Hierarchy

Use the right source for the question.

Every project or documentation domain should define:

```text
- source categories;
- evidence/current-reality categories;
- target/draft categories;
- recovery/historical categories;
- authority order between them;
- where concrete paths or source identifiers live.
```

Reusable principle:

```text
canonical sources and evidence must be explicit enough that a future reader can verify a claim without relying on hidden chat memory.
```

Concrete source paths are project adapter/profile content.

Implementation-specific evidence models are not universal. For software projects they may include code, tests and generated artifacts. For other domains they may include source notes, schedules, task state, research sources or other evidence.

## 12A. Layer Encapsulation And Attention Preservation

Documentation layers should behave like encapsulated modules.

When upstream work has already been reviewed, downstream docs should reference the published upstream artifact instead of reconstructing or re-explaining the upstream reasoning.

The goal is to preserve already-reviewed work and human review attention.

Use source usage relationships and section-level source blocks to:

```text
- prevent duplicated source truth;
- prevent AI from silently reinterpreting already-reviewed upstream work;
- preserve human attention for new downstream decisions;
- make cascade review targeted rather than broad by default;
- make stale downstream scopes discoverable when upstream sources change.
```

External dependencies and internal dependencies are different:

```text
External dependency
  = a source artifact/scope in another file or layer is consumed by this file/section.
  Track it through source usage relationships/registers when downstream synchronization matters.

Internal section dependency
  = a later section in the same file depends on analytical work from an earlier section.
  Track it locally in section-level source/internal dependency blocks when the section is high-risk or reviewed separately.
```

Versions and review markers support this model, but they are not the core model.

The core model is:

```text
source artifact/scope
  -> consumer artifact/scope
  -> reviewed_against / sync_status / review_outcome when needed
```

Detailed field-kit extraction for this model is deferred to:

```text
planning/documentation-migration/PORTABILITY-FOLLOWUPS.md
```

## 13. Source / Version Principle

Source versions should be tracked where downstream synchronization matters.

The principle lives here. Exact rows live in source usage registers or field-kit/register templates.

Use stable source identifiers where possible. In repo-backed projects, repo-relative file paths are usually more stable than informal labels.

Do not introduce section-level version registries everywhere by default. Design them through source/version governance when the need is proven.

## 14. Dependency Cascade Principle

When an upstream source changes, downstream files that use it must be reviewed.

A downstream review does not always require content changes. It may result in:

```text
reviewed against new source version;
no content change needed;
sync metadata updated;
needs follow-up.
```

Concrete cascade examples belong in specialized profiles or examples, not in reusable principles.

## 15. Section-Level Sources Principle

For important drafts, major sections should make their basis reviewable.

Use this when the section is high-risk, reviewed separately, depends on several upstream sources, or may become input for another draft.

Detailed answer-level guidance lives in:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

Reusable/candidate source-usage field-kit extraction is deferred to:

```text
planning/documentation-migration/PORTABILITY-FOLLOWUPS.md
```

## 16. Local Detail + Shared Visibility

Local docs own detailed context.

Shared indexes and registers own discoverability.

If a local detail affects future work outside that local file, it should be mirrored into the relevant shared index/register or explicitly marked local-only.

The exact local-detail type -> shared visibility target map is project adapter/profile content.

Detailed process lives in:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

## 17. Responsibility Ownership

A file should contain only content that belongs to its responsibility zone.

Before adding or moving content:

```text
1. choose the layer / responsibility zone;
2. choose the file type;
3. use the relevant responsibility map, README or adapter/profile;
4. avoid copying owner logic into non-owner files.
```

Concrete responsibility map paths are project configuration.

## 18. Safe Rewrite Rule

A documentation update is not safe just because it changes only Markdown.

If a wording change can affect behavior, scope, evidence, source-of-truth rules, external claims, responsibilities or project commitments, it is not a simple wording edit.

Such changes must be explicit in the plan, not hidden as polish.

Do not silently change behavior, scope or source-of-truth rules.

Concrete examples belong in specialized profiles or project adapters/examples.

## 18A. Decomposable File Architecture

Prefer smaller owner files over repeated scripted mutation of large shared files.

Large/shared files are acceptable when they have one cohesive responsibility, such as:

```text
- route map;
- index;
- register;
- action log;
- canonical table;
- source usage register.
```

A file becomes a split/refactor candidate when:

```text
- it repeatedly needs targeted scripts;
- unrelated responsibilities change together only because they share one file;
- one small update makes reviewers re-check a large unrelated surface;
- a future chat cannot safely produce a complete replacement from current full content;
- sections have different owners, update cadence or review audiences.
```

When a large file must be updated, do not choose script mode only because the file is large. First prefer fresh full repo/archive content or current full-file access and use complete replacement when it is safe and reviewable.

If complete replacement remains unsafe, use the local targeted script workflow as a fallback and consider whether the file should be decomposed later.

## 19. Documentation Update Plans

For broad documentation changes, use:

```text
planning/documentation/documentation-update-plan-workflow.md
```

A documentation update plan should be prepared before changes that affect:

```text
navigation;
source-of-truth rules;
status wording;
responsibility boundaries;
shared registers;
multiple planning files.
```

This keeps the update reviewable before files are changed.

## 20. AI-Checkability Principles

Documentation should make AI-assisted work checkable.

A future chat should be able to tell:

```text
which files were checked;
which files were not checked;
which source is canonical;
which assumptions are being used;
which items are current, planned, deferred or historical according to the domain's status model;
which files are in scope;
which files must not be changed;
how to verify current evidence/current reality.
```

Docs that require hidden chat memory are fragile.

Docs should support verification by a person and by another context-aware review chat.

Answer-level structure and commands live in:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

## 21. Internal vs External-Facing Output Separation

Internal working documentation may use internal workflow terms.

External-facing outputs need wording that fits their audience and purpose.

Reusable rule:

```text
Do not copy internal workflow wording directly into external/public-facing deliverables.
Rewrite useful internal material into clean audience-appropriate wording before external use.
```

Concrete external output layers and wording rules are project adapter/profile content.

For the current Enman candidate mapping, see:

```text
planning/documentation-migration/enman-docs-adapter.md
```

## 22. Dirty Draft / Recovery Note Policy

Recovery notes and dirty drafts are useful preservation areas, not a problem by themselves.

They are useful for:

```text
recovering wording;
understanding historical reasoning;
checking why a decision was considered;
preserving raw notes that may otherwise be lost.
```

They are dangerous when:

```text
they are read before canonical docs;
they are treated as current truth;
stale terms are copied into external-facing outputs;
old decisions override newer canonical docs or current evidence.
```

Rules:

```text
- recovery notes are never source of truth;
- canonical docs and current evidence win;
- use recovery notes only after canonical docs were checked;
- rewrite any useful wording into clean terminology before external use;
- promote stable useful decisions into canonical docs through normal documentation sync.
```

Concrete recovery locations are project adapter/profile content.

## 23. Direct Edits, Archives And Commit Granularity

Documentation architecture must support both direct edits and replacement packages.

For small scoped repo changes explicitly approved by the user:

```text
prefer direct edits when tool-supported and safe;
use one file per commit for independent semantic edits;
use one bundled/bulk commit for approved shallow mechanical multi-file link/path/name sync when tool-supported;
keep commit messages specific;
report changed files and commit SHAs.
```

For broad generated replacements or manual application:

```text
use replacement archive/package mode;
include MANIFEST and APPLY instructions;
include complete files rather than partial patches.
```

Direct edits and archive mode are output modes. They do not replace source-of-truth rules.

## 24. No-Duplication / Authority Rule

When the same topic is mentioned in several files, each file must keep its own level of responsibility.

```text
architecture principles
  = reusable invariants and design constraints.

responsibility maps
  = placement and owner routing.

README/index
  = navigation and read order.

workflow
  = process steps.

field kit
  = setup guidance for deriving project-specific artifacts.

adapter/profile
  = concrete project configuration.

register
  = shared state.

draft/source file
  = actual working content.

prompt
  = derived reusable instruction, not canonical rule source.

scoped sync note
  = case-specific context, not reusable process.
```

Conflict rule:

```text
canonical docs beat prompts;
architecture principles beat summaries;
responsibility maps beat workflow summaries for placement;
local maps beat root map for local placement after they exist;
workflow docs beat prompts for process steps;
adapter/profile files beat examples for concrete project mapping.
```

## 24A. Link Instead Of Copy / Docs DRY Rule

Do not duplicate reusable logic, command meaning, source-mode rules, output-mode rules, permission boundaries, workflow activation details or source/version rules in files that do not own them.

If another file owns the logic, link to that owner file or section instead of copying the logic.

A local file may include a short summary only when it is needed for navigation or readability. The summary must not become a second source of truth.

If changing the owner file would require updating copied text here, the copied text probably does not belong here.

Examples and indexes may link to owner use cases, workflows or templates, but must not copy their routing, source-mode, output-mode or permission logic.

## 24B. Accepted Command And Preservation Guardrails

Accepted user commands and output modes must not be silently reinterpreted.

If a user request matches an accepted command or use case, follow the owner use-case/workflow definition. Do not replace it with another mode because it seems safer, easier or more convenient.

If the accepted mode cannot be completed safely, stop and explain the blocker. Ask for explicit approval before switching modes.

Post-apply verification for replacement archives must check preservation, not only application.

A post-apply check should confirm:

```text
- only intended files are in scope;
- diffs match the package intent;
- no unrelated sections, register entries, commands, examples, routing rows or source-of-truth rules were removed;
- shared-state files such as registers preserve existing entries unless removal was explicit;
- commit commands include only intended files.
```

Command-specific details live in:

```text
planning/planning-use-case-map.md
planning/replacement-file-generation-guide.md
```

This section is the architecture-level guardrail only.

## 24C. Responsibility-Zone Review Guardrail

When reviewing existing documentation for placement, portability or split decisions, do not move a paragraph only because it contains a project-specific path, ID or term.

First ask:

```text
- What is the fundamental purpose of this text?
- What problem does it prevent?
- What reusable principle, if any, remains after removing concrete names?
- Is the remaining content a specialized profile pattern, adapter mapping, workflow detail, template shape or example?
```

Use the responsibility-zone review workflow for this process:

```text
planning/documentation/documentation-responsibility-zone-review-workflow.md
```

The review should classify content into the correct owner zone before moving or splitting it.

## 25. What Not To Add By Default

Do not add extra coordination systems unless explicitly needed.

Do not introduce by default:

```text
master-chat workflow
work register
mandatory status packets for every answer
full multi-chat orchestration docs
universal response format for all answers
section-level version registries for every principles/use-case reference
```

A broad reviewer chat may review another chat output if the output is explicitly provided, but it is not a master/controller and does not become a source of truth.

## 26. Success Criteria

The documentation architecture is working when:

```text
- a future reader can find the main entry point;
- source-of-truth boundaries are visible;
- current evidence/current reality is verified from the domain's evidence model, not stale snapshots;
- new docs are discoverable from navigation;
- responsibility maps can route information to the correct owner;
- local questions that matter globally are visible in shared registers;
- recovery notes remain useful but non-canonical;
- external-facing outputs use audience-appropriate wording;
- documentation changes can be planned, reviewed and reverted in small scopes;
- reusable logic has one owner and other files link to it instead of copying it.
```
