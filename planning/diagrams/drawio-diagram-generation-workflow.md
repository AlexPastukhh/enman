# Draw.io Diagram Generation Workflow

Status: current diagram artifact workflow  
Scope: target diagram format, diagram book structure and archive rules for diagram-generation chats

## 1. Purpose

This file defines the expected final diagram artifact format for repository diagram work.

Target format:

```text
draw.io XML
```

Preferred artifact:

```text
one multi-page `.drawio` file, diagram book style
```

The diagram-generation chat should produce actual draw.io XML whenever practical.

It must not generate PlantUML as the primary deliverable unless the user explicitly asks for PlantUML.

## 2. Preferred Output Path

Preferred output path when `planning/thesis/vkr-clean/` is active in the repository:

```text
planning/thesis/vkr-clean/diagrams/enman-vkr-diagrams.drawio
```

Fallback path when `planning/thesis/vkr-clean/` is not active yet:

```text
planning/diagrams/vkr-clean-drafts/enman-vkr-diagrams.drawio
```

If the output path is uncertain, the diagram-generation chat must state the assumption in Phase 1 preflight before generating files.

## 3. Diagram Book Structure

The preferred result is a single `.drawio` / XML file with multiple pages inside one `<mxfile>`.

Minimum structure:

```xml
<mxfile>
  <diagram name="01 Use Case Overview">...</diagram>
  <diagram name="02 Client Use Cases">...</diagram>
</mxfile>
```

Each `<diagram>` page should have a stable, human-readable page name.

Stable page names for a full diagram book:

```text
01 Use Case Overview
02 Client Use Cases
03 Employee Use Cases
04 Request Lifecycle
05 Account Activation Lifecycle
06 Domain Model Overview
07 High-Level Architecture
08 Request Creation Sequence
09 Request Review Sequence
10 Agreement Use Cases
11 Agreement Proposal Lifecycle
12 Agreement Proposal Sequence
```

Do not rename pages casually after they have been introduced; downstream documents may refer to page names.

## 4. Possible Full Diagram Book Contents

A full diagram book may contain pages for:

```text
Use Case Overview
Client Use Cases
Employee Use Cases
Agreement Use Cases
Request Lifecycle
Agreement Proposal Lifecycle
Account Activation Lifecycle
High-Level Architecture
Domain Model Overview
Request Creation Sequence
Request Review Sequence
Agreement Proposal Sequence
```

The prompt-generation workflow must not force all pages in one pass unless the user explicitly asks for full generation.

Default mode:

```text
preflight first -> selected batch -> archive
```

## 5. Draw.io XML Quality Rules

The `.drawio` file must be valid diagrams.net/draw.io XML.

Required behavior:

```text
- root element is `<mxfile>`;
- each diagram page is a `<diagram name="...">` element;
- labels are readable;
- arrows and relationships have clear direction;
- page names are stable;
- status markers are visible where they affect interpretation;
- diagram notes identify unresolved questions or accepted clarifications.
```

If the diagram-generation chat cannot confidently produce full geometry, it must not fake precision.

Acceptable fallback order:

```text
1. Produce a simple valid draw.io XML with readable boxes/arrows and clear labels.
2. If XML generation is not safe, produce `diagram-generation-plan.md` and request a follow-up for actual XML generation.
```

Preferred behavior is still to produce actual `.drawio` XML.

## 6. PlantUML / Mermaid / Image Output Rule

Do not use PlantUML, Mermaid, PNG, SVG or screenshots as the primary repository deliverable unless the user explicitly asks.

PlantUML or Mermaid may be used only as temporary internal drafting aids during the chat.

Final repository artifact should be:

```text
.drawio / draw.io XML
```

## 7. Status Markers In Diagram Pages

Use these markers when a diagram element could otherwise overclaim status:

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
- `[IMPLEMENTED]` requires current repo evidence.
- `[DESIGNED]` means accepted design/planning, not confirmed implementation.
- `[PLANNED]` must not look like current behavior.
- `[DEFERRED]` stays outside the current cut.
- `[QUESTION]` marks unresolved source conflict or open decision.
- `[CORE]` marks core diploma/MVP concept and may be paired with `[IMPLEMENTED]` or `[DESIGNED]`.
```

Do not make diagrams look more complete than the repository evidence supports.

## 8. Source Integrity Rules

Before drawing, the diagram-generation chat must inspect current sources for the selected batch.

Read current routing first:

```text
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-diagram-consistency-report.md
```

Source families:

```text
core scenario text specs
scenario DATA specs
scenario UI specs, if present
scenario behavior items
scenario clarifications
scenario questions register
current domain overview / aggregate docs, for domain-aware diagrams
current implementation evidence, when implementation status is shown
```

Use this source model:

```text
core scenario = business capability;
DATA = scenario information;
UI scenario = presentation / UX projection;
behavior items = processed behavior units with optional UI/UX projection notes.
```

Deprecated global validation addenda and old generated diagram packages are historical context only, not current source truth.

The diagram-generation chat must detect and report conflicts before generating XML.

Known required conflict checks:

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
DocumentDraft
notification
email
verification
anonymous
```

Agreement proposal replacement rule:

```text
Do not draw agreement proposal replacement as Rejected.

Use:
- superseded/replaced by counterproposal
- SupersededByCounterProposal, if a domain state is needed

Rejected is only for explicit rejection/decline.
```

If sources conflict, use an accepted clarification if present and mark the affected area with `[QUESTION]` or a diagram note.

## 9. VKR-Clean Text Rule

Final draw.io diagrams and VKR-clean companion docs must not mention:

```text
AI
ChatGPT
prompt
agent
internal workflow
planning chat
```

Allowed engineering terms include:

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

Use user/domain/engineering wording in diagrams, not planning-process wording.

## 10. Companion Files

When useful, the diagram-generation chat may create companion files such as:

```text
planning/diagrams/vkr-clean-drafts/diagram-generation-plan.md
planning/diagrams/vkr-clean-drafts/diagram-open-questions.md
```

Companion files should be included only when they help reviewers apply or continue the diagram work.

Do not create companion files to hide unresolved diagram defects.

## 11. Archive Rules

Diagram-generation output must be packaged as an archive with complete repo-relative files.

Required archive contents:

```text
MANIFEST.md
APPLY.md
complete `.drawio` file
complete companion markdown files, if any
```

No patches.

No direct GitHub writes.

`APPLY.md` must include:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\<archive-name>.zip" -DestinationPath . -Force
git status
```

## 12. Do Not

```text
- Do not write directly to GitHub unless explicitly asked.
- Do not create branch/commit/PR unless explicitly asked.
- Do not generate all diagrams at once by default.
- Do not overclaim implementation status.
- Do not fake precise geometry when simple readable geometry is safer.
- Do not use PlantUML as the primary deliverable unless explicitly asked.
- Do not place AI/internal workflow wording in VKR-clean diagrams.
```
