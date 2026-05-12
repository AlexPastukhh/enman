# Diagram Brief

Start here before asking any diagram-generation agent to generate diagrams.

This file is the navigation entrypoint for diagram planning. It explains which planning files control scenario semantics, visual construction, prompting protocol, common mistakes and future examples.

The diagram-generation chat must not make architecture decisions. It should generate editable `.drawio` diagrams from the diagram planning files and current source-of-truth planning documents.

## Repository Write Permission Rule

Diagram generation and repository modification are separate actions.

Diagram-generation agents must not create, update, delete, move, rename or commit files in the repository unless the user explicitly asks them to modify the repository.

By default, diagram agents should generate reviewable artifacts only:

```text
- draw.io file output;
- SVG/PNG preview output;
- Markdown summary output;
- suggested target paths.
```

They may suggest repository paths, but must not write to those paths without explicit instruction.

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

If the user says "generate diagrams", "create a draw.io file", "make a preview" or "give me the package", treat that as a request for reviewable artifacts, not permission to write to the repository.

Only write to the repository if the user explicitly says "add these files to the repository", "commit this to the repo", "update planning/...", "create the file in GitHub" or "modify the repository".

If unclear, do not write. Return generated artifacts and ask for explicit repo-write instruction.

## File Roles

- `diagram-scenario-spec.md` = source of truth for scenario/use-case semantics.
- `diagram-generation-rules-with-example.md` = source of truth for draw.io, visual construction, style consistency and layout mechanics.
- `diagram-prompting-guide.md` = how to prompt diagram-generation agents correctly.
- `diagram-common-mistakes.md` = repeated mistakes, why they are wrong and reject criteria.
- `diagram-examples-index.md` = index of approved and planned correct diagram examples.
- `planning/examples/` = actual approved examples.
- `planning/diagrams/` = canonical generated scenario diagram packages, scenario overview and consistency reports.
- `diagram-domain-db-brief.md` = aggregate/domain and DB diagram content rules.
- `domain-model.md` = source of domain decisions, not a direct use-case diagram prompt.
- `final-diagrams-example.drawio` = secondary visual/style reference only, not semantic or business truth.

## Generation Protocol

The diagram prompt must follow the requested generation mode.

Common modes:

```text
1. Proof-of-layout only
2. Single scenario page
3. Scenario package
4. Global scenario overview / navigation map
5. Documentation-only or prompt-only task
```

Do not force a proof-of-layout step for every task.

Proof-of-layout is useful, but it is not mandatory when the user did not ask for proof and the task already has an approved style/baseline.

Use proof-of-layout when:

```text
- the user explicitly asks for proof / sample / one-page layout first;
- the diagram type is new or not yet calibrated;
- the visual style is new or disputed;
- the diagram agent is uncalibrated and no approved example exists;
- the scenario is complex, uncertain or likely to produce connector/layout problems;
- a full package would be expensive to redo if the layout is wrong.
```

If the user asks for one scenario, generate that one scenario page only.

If the user asks for a package, generate the requested package directly.

If the user asks for a global overview, create an area/navigation overview, not a detailed scenario workflow.

If the user asks only for a prompt or documentation update, do not generate diagrams.

## Standard Read Order

For scenario diagrams, a diagram-generation agent should read:

```text
1. planning/diagram-brief.md
2. planning/diagram-scenario-spec.md
3. planning/diagram-generation-rules-with-example.md
4. planning/diagram-prompting-guide.md
5. planning/diagram-common-mistakes.md
6. planning/diagram-examples-index.md
```

Use approved examples when relevant.

Current canonical scenario example:

```text
planning/examples/scenario-login-correct-v3.drawio
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.png
planning/examples/scenario-login-correct-v3.md
```

Use the approved scenario visual language from canonical examples.

Theme may be light or dark, but it must be consistent within one package, readable on the chosen background and preserve semantic colors.

For scenario diagrams, use this additional instruction:

```text
Read planning/diagram-scenario-spec.md.
Follow section 20A Lane-Based Layout And Anti-Overlap Rules.
Use the generation mode requested by the user: proof-only, single scenario, package, overview or prompt/docs-only.
Do not add a proof step unless the user asked for proof or the task is explicitly a calibration/smoke-test task.
Use a consistent readable theme and preserve semantic color meanings.
```

## Conflict Rule

If files conflict:

- scenario semantics are controlled by `diagram-scenario-spec.md`;
- visual/draw.io construction is controlled by `diagram-generation-rules-with-example.md`;
- prompt process is controlled by `diagram-prompting-guide.md`;
- known anti-patterns are controlled by `diagram-common-mistakes.md`;
- approved examples are indexed by `diagram-examples-index.md`;
- domain and DB diagram content is controlled by `diagram-domain-db-brief.md` plus domain source files.

## Final Draw.io Example Warning

`final-diagrams-example.drawio` is not the canonical scenario diagram reference.

The canonical scenario diagram reference is:

```text
planning/examples/scenario-login-correct-v3.drawio
planning/examples/scenario-login-correct-v3.svg
planning/examples/scenario-login-correct-v3.png
planning/examples/scenario-login-correct-v3.md
```

Use `final-diagrams-example.drawio` only as secondary / legacy visual-style reference material.

Use it for:

- accepted legacy palette examples;
- card construction;
- editable grouped cards;
- line-by-line body text implementation;
- attached edge label style;
- general C2 visual language when generating non-scenario technical diagrams.

Do not use it as:

- a logical scenario structure reference;
- a required page package;
- a semantic model for future use-case diagrams;
- a source of architecture decisions;
- a reason to force a specific scenario background theme.

## Diagram Families

Keep these diagram families separate.

### Scenario / Use-Case Diagrams

User-facing behavior and specification.

Primary structure:

- actor;
- screen or application context;
- user goal;
- preconditions;
- main flow;
- include;
- branches;
- invariants;
- observable outcomes.

These diagrams must not be command/domain/table maps.

Default visual rule:

```text
Use the approved scenario visual language from canonical examples.
The selected theme may be light or dark, but must be consistent, readable and preserve semantic colors.
```

### Domain / Aggregate Boundary Diagrams

Domain ownership and aggregate boundaries.

Primary structure:

- aggregate roots;
- child entities;
- stored ID references;
- cross-aggregate rules;
- domain ownership notes.

### DB Schema Diagrams

Storage shape only.

Primary structure:

- tables;
- primary keys;
- foreign keys;
- discriminators;
- persisted columns;
- level additions.

### Technical CQRS / Application Flow Appendix

Optional technical appendix.

Primary structure:

- controller;
- command/query handler;
- repository;
- DbContext/unit of work;
- Dapper query side;
- database.

This technical flow belongs in an appendix, not in user-facing scenario pages.

## Prompt Seed For Future Diagram Chats

```text
You are generating editable draw.io diagrams for the Energy Management project.

Read:
1. planning/diagram-brief.md
2. planning/diagram-scenario-spec.md
3. planning/diagram-generation-rules-with-example.md
4. planning/diagram-prompting-guide.md
5. planning/diagram-common-mistakes.md
6. planning/diagram-examples-index.md

Use the approved canonical scenario example when generating scenario/use-case diagrams:
- planning/examples/scenario-login-correct-v3.drawio
- planning/examples/scenario-login-correct-v3.svg
- planning/examples/scenario-login-correct-v3.png
- planning/examples/scenario-login-correct-v3.md

Use the approved scenario visual language from canonical examples.
Theme may be light or dark, but it must be consistent within one package, readable on the chosen background and preserve semantic colors.
Do not silently switch theme inside one package.

Use planning/final-diagrams-example.drawio only as secondary / legacy visual-style reference.
Do not treat it as semantic scenario truth, a required page package or a reason to force a specific background theme.

Task:
<state the requested generation mode explicitly: proof-only, single scenario, package, overview, docs/prompt-only>

Do not add a proof step unless the user asked for proof or the task is explicitly a calibration/smoke-test task.

Do not write generated files into the repository unless the user explicitly asks for repository modification.

For use-case diagrams, create user-facing scenario/specification pages.
Do not create command/domain/table maps as use-case diagrams.

If aggregate or DB diagrams are requested, also read:
planning/diagram-domain-db-brief.md
planning/domain-model.md

Do not make architecture decisions.
Do not invent new domain concepts.
```
