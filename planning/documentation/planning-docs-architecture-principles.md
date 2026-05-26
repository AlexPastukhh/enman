# Planning Docs Architecture Principles

Status: current documentation architecture principles  
Scope: how to structure planning documentation so it remains navigable, reviewable and safe for both people and AI-assisted work

## 1. Purpose

Planning documentation is a knowledge system, not just a folder of notes.

It must help a future reader or chat understand:

```text
- where to start;
- which layer owns the information;
- which files are canonical;
- which files are local details;
- which files are historical, scoped or non-canonical;
- how to verify current implementation state;
- how to update docs without losing context or changing meaning silently.
```

These principles are about the architecture of planning documentation, not the runtime architecture of the application.

## 2. Core Goals

Planning docs should be:

```text
navigable for the user;
reviewable by another chat or person;
safe to update in small scoped changes;
clear about source-of-truth boundaries;
clear about current vs target vs draft vs archive;
clear about where new information belongs;
useful for VKR/thesis wording without leaking internal workflow terms.
```

## 3. Fixed Layer Architecture

Planning docs are organized as layers. A new piece of information should first be classified by layer, then by file type inside that layer.

Core layers:

| Layer | Owns |
|---|---|
| Documentation layer | Planning-doc architecture, docs update workflows, responsibility maps, agent-output workflows and documentation governance. |
| Scenario layer | Scenario text, UI specs, clarifications and behavior source. |
| DATA set layer | Per-scenario DATA sets: what actors enter, see, select, filter, attach or reference. |
| Behavior items set layer | Per-scenario behavior item sets derived from scenario/DATA/UI/cross-cutting sources. |
| Domain layer | Domain concepts, value objects, invariants, aggregate boundaries, domain decisions and domain drafts. |
| Slice layer | Slice drafts, slice boundaries, scenario/source mapping, questions, extension points and implementation notes. |
| API / testing layer | API contract rules, generated contract rules, error contracts, testing principles and E2E workflows. |
| Implementation evidence layer | Current branch, code, tests, migrations, generated artifacts and runtime screenshots. |
| VKR / thesis layer | Clean thesis wording, thesis resources, evidence maps and presentation/defense-safe wording. |

Layer dependency direction:

```text
scenario sources
  -> DATA sets / behavior item sets
  -> domain layer
  -> slice layer
  -> API/testing plans and implementation evidence
  -> VKR/thesis clean output
```

Documentation layer is the meta-layer that defines how the planning docs themselves are structured and updated.

## 4. Responsibility Map Model

Responsibility maps are routing tools.

There are two levels:

```text
Root responsibility map
  -> chooses the planning layer and points to local responsibility maps.

Local layer responsibility map
  -> decides where information belongs inside that layer.
```

Root router:

```text
planning/planning-doc-responsibility-map.md
```

Documentation layer local map:

```text
planning/documentation/documentation-responsibility-map.md
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

Until local maps exist for every layer, the root responsibility map may keep transitional fallback routing. Once local maps exist, detailed layer-specific placement should move out of the root map.

## 5. One Main Entry Point

`planning/README.md` is the main entry point.

Its responsibility is navigation and source-of-truth routing, not detailed current-state inventory.

It should answer:

```text
where to read scenario behavior;
where to read slice scope;
where to read architecture/API/client/testing docs;
where to read VKR clean wording;
where non-canonical recovery notes live;
how to verify current implementation state.
```

It should not duplicate detailed implementation status that must be updated after every code change.

## 6. File Type Responsibility Model

Every planning document should have an understandable type. If a document type is unclear, future chats may use it incorrectly.

| Type | Responsibility |
|---|---|
| Architecture principles | Global theory for how planning docs are structured. |
| Workflow | Algorithm/process for performing a repeated task. |
| Index / README | Navigation, read order, file overview and short purpose summaries. |
| Responsibility map | Where to put information; which file/layer owns what. |
| Source spec / source set | Canonical or derived source content, such as scenario specs, DATA sets or behavior item sets. |
| Source usage register | Which local files use which external source files/versions. |
| Questions register | Shared open/accepted questions, assumptions and decisions. |
| Extension points register | Future considerations, change pressure and extension points. |
| Implementation notes register | Shared implementation-related reminders that must stay visible beyond one local draft. |
| Draft | Working planning/design content for a scenario/domain/slice/documentation task. |
| Template | The shape of a draft, section or output artifact. |
| Evidence | Code, tests, migrations, generated artifacts, screenshots or other current implementation proof. |
| Status snapshot | Scoped or historical status note; not implementation truth by itself. |
| Scoped sync note | Case-specific documentation synchronization note; not a reusable workflow. |
| Derived prompt | Reusable prompt assembled from canonical docs; not a canonical rule source. |
| Dirty draft | Non-canonical recovery/context note. |
| VKR clean reference | Clean terminology and evidence map for thesis-facing materials. |

## 7. Index vs Register vs Source Usage Register

Do not use these terms interchangeably.

```text
Index / README
  = navigation, read order and short overview of files.

Register
  = shared cross-file collection of questions, decisions, extension points or notes that must remain discoverable.

Source usage register
  = dependency map showing which local files use which external source files and versions.
```

A file may combine roles only when its scope says so explicitly. Otherwise, keep index/navigation separate from shared state tracking.

## 8. Local Notes vs Register

Local notes belong inside one local file when they affect only that file.

Use a register when information:

```text
- is unclear where it belongs yet;
- affects multiple files or future files;
- belongs to a future slice/domain/scenario that does not exist yet;
- is a shared accepted direction;
- is a future-use implementation reminder;
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

## 10. Separate Current, Target, Draft, Archive And Dirty Draft

Do not mix these concepts.

| Category | Meaning | Source of truth? |
|---|---|---|
| Current implementation | What exists in the current branch/runtime. | Yes, but only from code/tests/migrations/generated contracts/screenshots. |
| Target direction | Where the design is intended to go. | Planning truth, not implementation proof. |
| Draft | Working planning hypothesis. | No, unless promoted. |
| Archive / handoff | Historical application or transfer artifact. | No, unless verified against current branch. |
| Dirty draft | Raw recovery/context note. | No. |

A domain or slice draft can describe intended behavior before it is implemented.

A status snapshot can be useful historically, but it becomes stale if treated as permanent current truth.

## 11. Avoid Heavy Current-State Docs

Do not maintain large global current-state documents that duplicate the implementation in detail.

They become high-maintenance and can go stale faster than they are updated.

Prefer:

```text
stable navigation;
source-of-truth rules;
small historical/status notes when useful;
current branch/code/tests/generated contracts/screenshots for implementation truth;
evidence maps for thesis-facing claims.
```

If a current-state note exists, mark whether it is historical/internal and remind readers to verify against current branch evidence.

## 12. Source-of-Truth Hierarchy

Use the right source for the question.

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

### Scenario behavior truth

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
```

### Domain direction

Use domain drafts, accepted domain decisions and implementation cuts. Historical/background domain notes do not override current target domain drafts or accepted decisions.

### Slice scope truth

Use:

```text
planning/slices/
planning/slices/l2/
planning/slices/cross-cutting/
```

Slice docs describe scope and intended work. They are not proof that work is implemented.

### Architecture/API/client/testing decisions

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
planning/thesis/
```

### Recovery material

Use only after canonical docs:

```text
planning/dirty-drafts/
```

## 13. Source / Version Principle

Source versions should be tracked where downstream synchronization matters.

The principle lives here. Exact rows live in source usage registers.

Typical model:

```text
source file path
content_version
reviewed_against / derived_from
sync_status
last_reviewed
consumer file / local file
```

Use stable repo-relative file paths as source keys. Human labels may be added, but file paths are more stable than informal names.

DATA sets and behavior item sets can have their own versions for the same scenario. They are not merely aliases of the scenario version.

## 14. Dependency Cascade Principle

When an upstream source changes, downstream files that use it must be reviewed.

Example:

```text
scenario spec changes
  -> review related DATA set
  -> review related behavior items set
  -> review domain drafts using those sources
  -> review slice drafts using those sources
  -> review tests/status/VKR claims when relevant
```

A downstream review does not always require content changes. It may result in:

```text
reviewed against new source version;
no content change needed;
sync metadata updated;
needs follow-up.
```

## 15. Section-Level Sources Principle

For important drafts, major sections should make their basis reviewable.

Minimal major-section source block:

```text
Sources:
  Format:
  - ...

  Content:
  - ...

  Internal dependencies:
  - ...

  Not checked:
  - ...
```

Use this when the section is high-risk, reviewed separately, depends on several upstream sources, or may become input for another draft.

Detailed answer-level guidance lives in:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

## 16. Local Detail + Global Visibility

Local docs own detailed context.

Examples:

```text
one slice file
one client sidecar
one scenario spec
one DATA spec
one cross-cutting slice
one ADR candidate note
```

Global indexes and registers own discoverability.

Examples:

```text
planning/README.md
folder README files
planning/planning-doc-responsibility-map.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-questions-register.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
```

If a local detail affects future work outside that local file, it should be mirrored into the relevant shared index/register or explicitly marked local-only.

Detailed process lives in:

```text
planning/documentation/local-global-documentation-sync-workflow.md
```

## 17. Responsibility Ownership

A file should contain only content that belongs to its responsibility zone.

Before adding or moving content, first choose the layer using:

```text
planning/planning-doc-responsibility-map.md
```

Then use the local responsibility map or README for that layer.

For documentation-layer information, use:

```text
planning/documentation/documentation-responsibility-map.md
```

## 18. Safe Rewrite Rule

A documentation update is not safe just because it changes only Markdown.

If a wording change can affect any of these, it is not a simple wording edit:

```text
scenario behavior
API contract
domain rule
security requirement
testing responsibility
architecture boundary
slice scope
status of implemented vs planned work
VKR claim about what was implemented
```

Such changes must be explicit in the plan, not hidden as polish.

Do not silently change behavior, scope or source-of-truth rules.

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

Planning docs should make AI-assisted work checkable.

A future chat should be able to tell:

```text
which files were checked;
which files were not checked;
which source is canonical;
which assumptions are being used;
which items are implemented, planned, deferred or historical;
which files are in scope;
which files must not be changed;
how to verify current implementation evidence.
```

Docs that require hidden chat memory are fragile.

Docs should support verification by a person and by another context-aware review chat.

Answer-level structure and commands live in:

```text
planning/documentation/reviewable-agent-output-and-commands-workflow.md
```

## 21. VKR / Thesis Separation

Internal planning docs may mention:

```text
chat
agent
prompt
workflow
archive
handoff
L1/L2 internal labels
```

VKR-facing materials must use clean domain wording.

Use:

```text
planning/vkr-clean-reference.md
planning/thesis/
```

Do not copy dirty drafts or internal workflow wording directly into VKR, presentation, defense speech or practice-report text.

## 22. Dirty Draft Policy

Dirty drafts are useful preservation areas, not a problem by themselves.

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
stale terms are copied into VKR;
old decisions override newer scenario/slice/domain docs.
```

Rules:

```text
- dirty drafts are never source of truth;
- canonical docs and current code win;
- use dirty drafts only after canonical docs were checked;
- rewrite any useful wording into clean terminology before VKR use;
- promote stable useful decisions into canonical docs through a normal documentation sync.
```

## 23. Direct Edits, Archives And Commit Granularity

Documentation architecture must support both direct edits and replacement packages.

For small scoped repo changes explicitly approved by the user:

```text
prefer direct GitHub edits;
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
  = global theory.

root responsibility map
  = layer routing and transitional fallback.

local responsibility maps
  = placement inside one layer.

README/index
  = navigation and read order.

workflow
  = process steps.

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
workflow docs beat prompts for process steps.
```

## 25. What Not To Add By Default

Do not add extra coordination systems unless explicitly needed.

Do not introduce by default:

```text
master-chat workflow
work register
mandatory status packets for every answer
full multi-chat orchestration docs
universal response format for all answers
```

A broad reviewer chat may review another chat output if the output is explicitly provided, but it is not a master/controller and does not become a source of truth.

## 26. Success Criteria

The planning docs architecture is working when:

```text
- a future reader can start from planning/README.md;
- source-of-truth boundaries are visible;
- current implementation is verified from repo evidence, not stale snapshots;
- new docs are discoverable from navigation;
- root responsibility map can choose the layer;
- local responsibility maps can place information inside layers;
- local questions that matter globally are visible in shared registers;
- dirty drafts remain useful but non-canonical;
- VKR-facing wording uses clean terms;
- documentation changes can be planned, reviewed and reverted in small scopes.
```
