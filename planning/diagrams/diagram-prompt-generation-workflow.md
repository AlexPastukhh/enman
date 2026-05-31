# Diagram Request / Prompt And Preflight Workflow

Status: current diagram request/preflight workflow / scenario-map synchronized  
Doc version: v0.1.0  
Scope: how scenario/documentation/planning work prepares a repo-grounded diagram request, and how the single Diagram Chat runs preflight/generation

## 1. Purpose

This file explains two connected activities without creating two separate diagram roles:

```text
1. A scenario/documentation/planning chat may prepare a repo-grounded diagram request/prompt.
2. The single Diagram Chat consumes that request, rereads the repo, runs preflight and later generates selected draw.io diagrams.
```

There is no separate prompt-architect role for diagrams.

The role is:

```text
Diagram Chat
```

The Diagram Chat owns:

```text
Phase 1 — preflight / source reconciliation / batch plan
Phase 2 — selected draw.io XML generation
Phase 3 — archive packaging
```

Scenario Draft Chat may prepare the request/prompt when diagrams are requested from scenario sources, but it does not draw diagrams.

## 2. Role Boundary

### Scenario/documentation/planning chat

May prepare a diagram request/prompt.

It must:

```text
- read current planning navigation and diagram/source docs;
- discover actual source folders and index filenames;
- identify relevant scenario/spec/DATA/UI/API/security sources;
- identify known clarifications and source conflicts;
- suggest a diagram batch;
- hand the request to the Diagram Chat;
- not create `.drawio`, XML, PNG, SVG, PlantUML or other diagram artifacts.
```

### Diagram Chat

Consumes the request/prompt but still rereads the repository.

It must:

```text
- read the repository again from current branch;
- perform Phase 1 preflight first;
- generate only the selected diagram batch after user instruction;
- prefer one draw.io XML diagram book;
- package complete files in an archive;
- not write directly to GitHub unless explicitly asked.
```

## 3. Required Read Rules For Diagram Requests

A diagram request/prompt must tell the Diagram Chat not to work from memory.

It must require reading central navigation first:

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/current-state.md, if exists
```

Then it must require reading diagram/source navigation:

```text
planning/diagrams/README.md
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-drafting-workflow.md
planning/diagrams/scenario-diagram-consistency-report.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md, if exists
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-data/00-scenario-data-index.md, if exists
planning/diagrams/scenario-ui-specs/README.md, if exists
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md, if exists
planning/diagrams/scenario-clarifications/README.md, if exists
planning/diagrams/scenario-questions-register.md, if exists
```

It must require discovering and listing actual index files before using them.

Current known index files include:

```text
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
```

Do not assume alternative names such as:

```text
00-scenario-text-specifications-index.md
```

unless the current repository actually contains them.



### Scenario / UI / DATA source model

Diagram prompts must treat:

```text
core scenario
  as business capability source;

DATA
  as scenario information source;

UI scenario
  as presentation / UX projection over DATA and business behavior;

behavior items
  as processed behavior units with optional UI/UX projection notes.
```

If UI wording appears to add business meaning, the prompt must flag a consistency issue instead of silently using it as core business scope.
## 4. Required Source Coverage

The Diagram Chat must inspect sources relevant to the selected batch.

Required source families:

```text
scenario text specs
scenario DATA specs
scenario UI specs, if present
scenario behavior items
scenario-responsibility-map.md
scenario-artifact-map.md
API/security addenda
scenario clarifications
scenario questions register
```

L2 validation cleanup rule:

```text
Do not treat old global validation addenda as active L2 source of truth when they conflict with
scenario-local validation sections, scenario clarifications, slice docs or current L2 domain direction.

For L2 review/agreement diagrams, read:
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
```

For domain/API/context diagrams, also require checking:

```text
planning/domain-model.md, if exists
planning/l1-domain-implementation-cut.md, if exists
planning/l1-domain-testing-rules.md, if exists
planning/slices/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/SLICE-INDEX.md
planning/api/README.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
planning/api/client-constants-generation.md
```

For implementation-aware status markers, require checking current repository evidence enough to avoid overclaiming:

```text
EnergyManagement.Server/L1/**
EnergyManagement.Server/**
Domain.EnergyManagement/L1/**
Domain.EnergyManagement/**
Tests.EnergyManagement/Integration/**
Shared/openapi.json
Shared/constants.json
Shared/errorcodes.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Docs do not override current implementation evidence when deciding `[IMPLEMENTED]` vs `[DESIGNED]` or `[PLANNED]`.

## 5. Status Markers

Use only these markers:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

| Marker | Meaning |
|---|---|
| `[CORE]` | Core diploma/MVP concept or flow; may be implemented or designed, so pair with another marker when needed. |
| `[IMPLEMENTED]` | Current repo implementation evidence confirms it in the stated scope. |
| `[DESIGNED]` | Planning docs describe accepted target behavior, but repo implementation is not confirmed. |
| `[PLANNED]` | Planned work; do not present as current behavior. |
| `[DEFERRED]` | Intentionally outside the current cut. |
| `[QUESTION]` | Source conflict or unresolved question may affect the diagram element. |

Do not mark an element `[IMPLEMENTED]` only because a scenario says it should exist.

Do not mark a future workflow `[CORE]` if it is outside the current diploma/MVP cut.

## 6. Required Conflict Checks

Before drawing lifecycle, sequence or domain diagrams, check these terms:

```text
Rejected
Superseded
SupersededByCounterProposal
replaced
counterproposal
counter-proposal
previous client-sent proposal
Submitted
InReview
AgreementProposalExchange
AgreementProposalVersion
AgreementDocumentRef
DocumentFileRef
ProposalAttachment
ReviewerRef
EmployeeRef
DocumentDraft
notification
email
verification
anonymous
SC-14
Client Data Verification
```

Special agreement rule:

```text
Do not draw agreement proposal replacement as Rejected.

Use:
- superseded/replaced by counterproposal
- SupersededByCounterProposal, if a domain state is needed

Rejected is only for explicit rejection/decline.
```

L2 stale term rule:

```text
Do not use DocumentFileRef, ReviewerRef, ProposalAttachment or EmployeeRef in current L2 diagrams
unless the current repo explicitly reintroduces them.
```

If source files conflict:

```text
1. use the current accepted clarification if one exists;
2. mark the affected diagram element `[QUESTION]` or add a note;
3. add the issue to diagram open questions;
4. do not silently freeze stale wording into the diagram.
```

Primary clarification sources:

```text
planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
```

## 7. VKR-Clean Diagram Language Rule

Final draw.io diagrams and VKR-clean diagram docs must not mention:

```text
AI
ChatGPT
prompt
agent
internal workflow
planning chat
```

They may mention normal engineering concepts:

```text
OpenAPI
generated TypeScript types
generated semantic constants
ProblemDetails
client API layer
ASP.NET Core API
Application layer
Domain layer
Persistence
external provider
```

Internal planning files may describe request/prompt and workflow because they are not VKR-clean deliverables.

## 8. Diagram Chat Phases

### Phase 1 — Preflight only

The Diagram Chat must:

```text
- read current repo sources;
- list sources read;
- list existing scenario/spec folders;
- list actual index files found;
- identify implementation facts relevant to status markers;
- identify conflicts/stale wording/open questions;
- propose a diagram batch plan;
- not create diagrams yet unless the user explicitly asks.
```

Preflight output must include:

```markdown
## Preflight

### Sources read
- ...

### Existing scenario/spec folders and indexes
| Area | Path | Index file |
|---|---|---|

### Current implementation facts
| Area | Repo evidence | Diagram marker impact |
|---|---|---|

### Conflicts / stale wording / open questions
| ID | Source | Problem | Proposed diagram handling |
|---|---|---|---|

### Proposed diagram batches
| Batch | Diagrams | Why together |
|---|---|---|
```

### Phase 2 — Generate selected batch

Only after the user selects a batch or explicitly asks for generation, the Diagram Chat generates diagrams.

Rules:

```text
- generate only selected diagrams;
- prefer one draw.io XML diagram book;
- include open questions in a companion planning file or diagram notes;
- keep status markers visible enough to prevent overclaiming;
- do not generate all diagrams at once by default.
```

### Phase 3 — Archive

The Diagram Chat packages complete repo-relative files:

```text
MANIFEST.md
APPLY.md
complete `.drawio` / XML files
companion markdown files, if created
```

No patches.

No direct GitHub writes.

## 9. Recommended Batch Plan

Do not generate all diagrams at once by default.

Recommended batches:

### Batch 1 — core request/domain

```text
use-case-overview
request-lifecycle
domain-model-overview
request-creation-sequence
request-review-sequence
diagram-open-questions
diagram-generation-plan
```

### Batch 2 — client/employee/security/architecture

```text
use-case-client
use-case-employee
account-activation-lifecycle
high-level-architecture
```

### Batch 3 — agreement

```text
use-case-agreement-flow
agreement-proposal-lifecycle
agreement-proposal-sequence
```

Keep the agreement batch separate because agreement proposal replacement has known terminology risk around `Rejected` vs `SupersededByCounterProposal`.

## 10. Diagram Request / Prompt Skeleton

Use this as a starting point when preparing a request for the Diagram Chat:

```text
You work with repository https://github.com/AlexPastukhh/enman on branch my-changes.

Role: Diagram Chat.

Goal: generate repo-grounded VKR-clean diagrams as draw.io XML.

Do not write directly to GitHub. Do not create branch/commit/PR. Create an archive with complete repo-relative files.

First perform Phase 1 preflight only. Do not generate diagrams until I select a batch.

Read current repo docs and implementation evidence. Discover actual scenario/spec folders and index files. Inspect scenario text specs, DATA, UI specs, behavior items, API/security addenda, scenario clarifications and scenario questions.

For L2 review/agreement diagrams, read L2 validation cleanup clarification and do not use stale global validation addendum wording as source of truth.

Use status markers: [CORE], [IMPLEMENTED], [DESIGNED], [PLANNED], [DEFERRED], [QUESTION]. Do not overclaim implementation status.

Target format: draw.io XML. Preferred artifact: one multi-page `.drawio` diagram book.

Check agreement proposal terminology before drawing. Do not draw counterproposal replacement as Rejected; use superseded/replaced by counterproposal or SupersededByCounterProposal.

Final VKR-clean diagrams must not mention AI, ChatGPT, prompt, agent, internal workflow or planning chat.
```

## 11. Do Not

```text
- Do not split diagram work into separate prompt/preflight and generation roles.
- Do not generate diagrams inside Scenario Draft Chat.
- Do not ask the Diagram Chat to draw everything at once by default.
- Do not let the Diagram Chat work from memory or from stale prompt text only.
- Do not hide source conflicts.
- Do not overclaim implementation status.
- Do not use PlantUML as the primary deliverable unless explicitly asked.
- Do not put AI/internal workflow wording into VKR-clean diagrams.
- Do not use stale L2 validation terms such as DocumentFileRef, ReviewerRef, ProposalAttachment or EmployeeRef in current diagrams.
```

## Scenario-local Marker Sections

Marker: SCENARIO-STATUS-MARKERS-2026-05

Before generating diagrams, the Diagram Chat must read scenario-local marker sections when present:

```text
## Diagram / Implementation Markers
```

These sections use:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

Rules:

```text
- Treat [PLANNED] as future implementation work, not as already implemented.
- Treat [DEFERRED] as extension/future cut, not current main flow.
- Treat [DESIGNED] as accepted target semantics.
- Promote to [IMPLEMENTED] only with current repo evidence.
- Do not invent non-standard marker names such as [FUTURE] or [TODO].
```

