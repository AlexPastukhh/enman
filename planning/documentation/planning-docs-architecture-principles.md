# Planning Docs Architecture Principles

Status: current documentation architecture principles  
Scope: how to structure planning documentation so it remains navigable, reviewable and safe for both people and AI-assisted work

## 1. Purpose

Planning documentation is a knowledge system, not just a folder of notes.

It must help a future reader understand:

```text
- where to start;
- which files are canonical;
- which files are local details;
- which files are historical or non-canonical;
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
useful for VKR/thesis wording without leaking internal workflow terms.
```

## 3. One Main Entry Point

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

## 4. Document Types Must Be Explicit

Every planning document should have an understandable type.

Common types:

| Type | Purpose |
|---|---|
| Index / README | Navigation and read order. |
| Workflow | How to perform a repeated process. |
| Protocol | Rules for chat/agent behavior. |
| Source spec | Canonical scenario, data, UI or behavior source. |
| Slice doc | Scope and intended implementation work for a scenario portion. |
| Register | Shared visibility for questions, extension points or future notes. |
| Architecture note | Cross-slice boundary or architecture decision context. |
| ADR / ADR candidate | Accepted or candidate decision record. |
| Status snapshot | Historical or scoped status note; not implementation truth by itself. |
| Dirty draft | Non-canonical recovery/context note. |
| VKR clean reference | Clean terminology and evidence map for thesis-facing materials. |

If a document type is unclear, future chats may use it incorrectly.

## 5. Separate Current, Target, Draft, Archive And Dirty Draft

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

## 6. Avoid Heavy Current-State Docs

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

## 7. Source-of-Truth Hierarchy

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
```

### Recovery material

Use only after canonical docs:

```text
planning/dirty-drafts/
```

## 8. Local Detail + Global Visibility

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

## 9. Responsibility Ownership

A file should contain only content that belongs to its responsibility zone.

Examples:

```text
global documentation workflow -> planning/documentation/
repo editing workflow -> planning workflow / documentation workflow docs
scenario meaning -> scenario specs / clarifications
slice scope -> slice docs
API contract rules -> planning/api/
testing rules -> planning/testing/
VKR clean terminology -> planning/vkr-clean-reference.md
raw recovery wording -> planning/dirty-drafts/
```

Before adding or moving content, check:

```text
planning/planning-doc-responsibility-map.md
```

## 10. Safe Rewrite Rule

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

## 11. Documentation Update Plans

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

## 12. AI-Checkability Principles

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

## 13. VKR / Thesis Separation

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
```

Do not copy dirty drafts or internal workflow wording directly into VKR, presentation, defense speech or practice-report text.

## 14. Dirty Draft Policy

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

## 15. Direct Edits, Archives And Commit Granularity

Documentation architecture must support both direct edits and replacement packages.

For small scoped repo changes explicitly approved by the user:

```text
prefer direct GitHub edits;
use one file per commit by default;
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

## 16. What Not To Add By Default

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

## 17. Success Criteria

The planning docs architecture is working when:

```text
- a future reader can start from planning/README.md;
- source-of-truth boundaries are visible;
- current implementation is verified from repo evidence, not stale snapshots;
- new docs are discoverable from navigation;
- responsibility map knows where content belongs;
- local questions that matter globally are visible in shared registers;
- dirty drafts remain useful but non-canonical;
- VKR-facing wording uses clean terms;
- documentation changes can be planned, reviewed and reverted in small scopes.
```
