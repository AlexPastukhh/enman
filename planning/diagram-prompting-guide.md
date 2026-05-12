# Diagram Prompting Guide

Status: permanent planning guide for diagram-generation prompts.

Purpose: help future prompts produce diagrams that are correct both semantically and visually.

## Required Files To Read

For scenario/use-case diagrams, require the diagram agent to read:

```text
planning/diagram-brief.md
planning/diagram-scenario-spec.md
planning/diagram-generation-rules-with-example.md
planning/diagram-prompting-guide.md
planning/diagram-common-mistakes.md
planning/diagram-examples-index.md
```

Use approved examples when relevant:

```text
planning/examples/scenario-login-correct-v3.drawio
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.png
planning/examples/scenario-login-correct-v3.md
```

Use the approved dark visual theme from:

```text
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.png
planning/examples/scenario-login-correct-v3.drawio
```

Do not use a light theme unless explicitly requested.

For aggregate/domain or DB diagrams, also require:

```text
planning/diagram-domain-db-brief.md
planning/domain-model.md
```

## Generation Mode Selection

Do not force one generation mode for every task.

The diagram prompt must follow the user's requested mode.

Common modes:

```text
1. Proof-of-layout only
2. Single scenario page
3. Scenario package
4. Global scenario overview / navigation map
5. Documentation-only or prompt-only task
```

If the user asks for a proof, smoke test or layout sample, generate one proof-of-layout page only.

If the user asks for one scenario, generate that one scenario page only.

If the user asks for a package, generate the requested package directly.

If the user asks for a global overview, create an area/navigation overview, not a detailed scenario workflow.

If the user asks only for a prompt or documentation update, do not generate diagrams.

Proof-of-layout is useful, but it is not mandatory for every request.

Do not automatically insert a proof step when the user did not ask for one and the task already has an approved style/baseline.

A proof-of-layout is recommended when:

```text
- the user explicitly asks for proof / sample / one-page layout first;
- the diagram type is new or not yet calibrated;
- the visual style is new or disputed;
- the diagram agent is uncalibrated and no approved example exists;
- the scenario is complex, uncertain or likely to produce connector/layout problems;
- a full package would be expensive to redo if the layout is wrong.
```

When a proof page is requested, it should test:

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

After the proof page is reviewed and accepted, the user may ask for a full package using the same style and rules.

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
- lane discipline without mandatory visible lane lines;
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

Ask the diagram agent to return the artifacts appropriate to the requested mode.

For proof-of-layout mode:

```text
- one generated proof-of-layout page;
- short explanation of semantic choices;
- list of connector labels used;
- list of markers used and why;
- notes on any intentionally omitted connectors;
- explicit statement that no full package was generated yet.
```

For single-scenario mode:

```text
- one generated scenario page;
- preview if possible;
- short Markdown explanation;
- semantic self-check;
- visual self-check.
```

For package mode:

```text
- generated package artifact;
- preview if possible;
- Markdown summary;
- list of pages created;
- cross-page links;
- semantic self-check;
- visual self-check.
```

## Repository Write Permission

Tell the diagram agent:

```text
Diagram generation and repository modification are separate actions.

Do not create, update, delete, move, rename or commit files in the repository unless the user explicitly asks you to modify the repository.

By default, generate reviewable artifacts only:
- draw.io file output;
- SVG/PNG preview output;
- Markdown summary output;
- suggested target paths.

You may suggest repository paths, but must not write to those paths without explicit instruction.
```

Allowed without an explicit repo-write request:

```text
- generate downloadable files;
- provide a patch proposal;
- list intended repository paths;
- explain where the user should place files;
- create a prompt for another agent.
```

Not allowed without an explicit repo-write request:

```text
- create files in repository;
- update existing repository files;
- delete files;
- rename files;
- move files;
- commit changes;
- open PR;
- modify planning docs.
```

Interpret these requests as artifact generation only:

```text
generate diagrams
create a draw.io file
make a preview
give me the package
```

Interpret these as explicit repo-write requests:

```text
add these files to the repository
commit this to the repo
update planning/...
create the file in GitHub
modify the repository
```

If unclear, do not write. Return generated artifacts and ask for explicit repo-write instruction.

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
- planning/diagram-prompting-guide.md
- planning/diagram-common-mistakes.md
- planning/diagram-examples-index.md

Use approved example if relevant:
- planning/examples/scenario-login-correct-v3.drawio
- planning/examples/scenario-login-correct-v3.svg
- planning/examples/scenario-login-correct-v3.png
- planning/examples/scenario-login-correct-v3.md

Use the approved dark visual theme from:
- planning/examples/scenario-login-correct-v3.svg
- planning/examples/scenario-login-correct-v3.png
- planning/examples/scenario-login-correct-v3.drawio

Do not use a light theme unless explicitly requested.

Task:
<state the requested generation mode explicitly: proof-only, single scenario, package, overview, docs/prompt-only>

Do not add a proof step unless the user asked for proof or the task is explicitly a calibration/smoke-test task.

Do not write generated files into the repository unless the user explicitly asks for repository modification.

Scenario / package:
<scenario name/ref or package scope>

Source scenario content:
<paste exact scenario spec / package section when available>

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
Use the generation mode requested by the user: proof-only, single scenario, package, overview or prompt/docs-only.
Do not add a proof step unless the user asked for proof or the task is explicitly a calibration/smoke-test task.
```
