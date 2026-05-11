# Diagram Prompting Guide

Status: permanent planning guide for diagram-generation prompts.

Purpose: help future prompts produce diagrams that are correct both semantically and visually.

## Required Files To Read

For scenario/use-case diagrams, require the diagram agent to read:

```text
planning/diagram-brief.md
planning/diagram-scenario-spec.md
planning/diagram-generation-rules-with-example.md
planning/diagram-common-mistakes.md
planning/diagram-examples-index.md
```

Use approved examples when relevant:

```text
planning/examples/scenario-login-correct-v3.drawio
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.md
```

Use the approved dark visual theme from:

```text
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.drawio
```

Do not use a light theme unless explicitly requested.

For aggregate/domain or DB diagrams, also require:

```text
planning/diagram-domain-db-brief.md
planning/domain-model.md
```

## Mandatory Proof-Of-Layout First

Do not ask for a full diagram package immediately.

Always ask for one proof-of-layout page first.

The proof page should test:

```text
- scenario flow shape;
- semantic correctness;
- connector routing;
- lane discipline;
- text density;
- visual style;
- anti-overlap rules;
- marker usage.
```

Only after the proof page is reviewed and accepted should the full package be generated.

## Semantic Source Of Truth

Tell the diagram agent:

```text
Scenario semantics are controlled by planning/diagram-scenario-spec.md.
Do not invent business behavior.
Do not replace scenario semantics with command/domain/table maps.
```

Semantic correctness includes:

```text
- invariants attach to enforcement points;
- preconditions do not duplicate decision branches;
- step postconditions attach to producing steps;
- scenario end states remain compact;
- [ALT] is used narrowly;
- [EXT] is roadmap marker only;
- EXTND is used in item refs;
- purple means off-page/subscenario transition only.
```

## Visual Source Of Truth

Tell the diagram agent:

```text
Visual and draw.io construction rules are controlled by planning/diagram-generation-rules-with-example.md.
Use final-diagrams-example.drawio only as a visual/style reference.
Do not treat final-diagrams-example.drawio as scenario semantics.
```

Visual correctness includes:

```text
- uses approved dark project theme;
- does not silently switch to light theme;
- semantic colors match diagram-scenario-spec.md;
- all text is readable on dark background;
- no text overflow;
- no shape overlap;
- no connector overlap;
- no connectors through shape bodies;
- no connectors through body text;
- no long end-state connector webs;
- spacious canvas over dense layout;
- lane discipline without mandatory visible lane lines.
- all text fits inside shapes.
```

## Prevent Text-Card Diagrams

Do not prompt for "summarize the scenario as cards".

Ask for:

```text
actor/screen node
-> main flow nodes
-> decision branches
-> include nodes attached to exact steps
-> off-page subscenario links
-> local invariants
-> compact end-state block
```

Reject a diagram if it is mostly large disconnected text blocks.

## Reviewable Output

Ask the diagram agent to return:

```text
- one generated proof-of-layout page;
- short explanation of semantic choices;
- list of connector labels used;
- list of markers used and why;
- notes on any intentionally omitted connectors;
- explicit statement that no full package was generated yet.
```

## Placeholder Examples

The examples index lists approved and planned examples.

Tell the diagram agent:

```text
Read planning/diagram-examples-index.md for intended examples.
Use approved examples when relevant.
Do not treat missing TODO example files as an error.
Do not create new example files unless explicitly asked.
```

## Standard Prompt Skeleton

```text
Read:
- planning/diagram-brief.md
- planning/diagram-scenario-spec.md
- planning/diagram-generation-rules-with-example.md
- planning/diagram-common-mistakes.md
- planning/diagram-examples-index.md

Use approved example if relevant:
- planning/examples/scenario-login-correct-v3.drawio
- planning/examples/scenario-login-correct-v3.svg
- planning/examples/scenario-login-correct-v3.md

Use the approved dark visual theme from:
- planning/examples/scenario-login-correct-v3.svg
- planning/examples/scenario-login-correct-v3.drawio

Do not use a light theme unless explicitly requested.

Task:
Generate one proof-of-layout page only.

Do not generate the full package yet.

Scenario:
<scenario name and ref>

Quality requirements:
- main flow is visual backbone;
- no text-card summary;
- semantic correctness and visual correctness are both required;
- uses approved dark project theme;
- does not silently switch to light theme;
- semantic colors match diagram-scenario-spec.md;
- all text is readable on dark background;
- invariants attach to enforcement point;
- preconditions do not duplicate decision branches;
- step postconditions attach to producing step;
- no generic "extend" connector;
- use EXTND in item refs;
- use [ALT] narrowly;
- purple only for off-page/subscenario links;
- lane discipline is conceptual; visible lane lines are not required;
- no long end-state connector web;
- all text must fit inside shapes;
- no implementation details.
```

## Correction Protocol

When the user points out an error in a generated diagram:

```text
1. Identify whether the error is semantic, visual, prompt-related, or documentation-related.
2. Explain the mistake briefly.
3. Fix the current diagram or propose a concrete fix.
4. Check whether the mistake reveals a missing or weak rule in the documentation.
5. If yes, propose an exact documentation update:
   - target file;
   - target section;
   - exact rule or example to add.
6. Ask whether to update the docs, unless the user already explicitly asked to update them.
7. If the mistake is recurring, add it to diagram-common-mistakes.md.
```

## Short Add-On For Every Diagram Prompt

```text
Read planning/diagram-scenario-spec.md.
Follow section 20A Lane-Based Layout And Anti-Overlap Rules.
Before generating a full package, create one proof-of-layout page first.
```
