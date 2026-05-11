# Diagram Brief

Start here before asking any diagram-generation agent to generate diagrams.

This file is the navigation entrypoint for diagram planning. It explains which planning files control scenario semantics, visual construction, prompting protocol, common mistakes and future examples.

The diagram-generation chat must not make architecture decisions. It should generate editable `.drawio` diagrams from the diagram planning files and current source-of-truth planning documents.

## File Roles

- `diagram-scenario-spec.md` = source of truth for scenario/use-case semantics.
- `diagram-generation-rules-with-example.md` = source of truth for draw.io, visual construction, style and layout mechanics.
- `diagram-prompting-guide.md` = how to prompt diagram-generation agents correctly.
- `diagram-common-mistakes.md` = repeated mistakes, why they are wrong and reject criteria.
- `diagram-examples-index.md` = placeholder index of future correct examples.
- `diagram-domain-db-brief.md` = aggregate/domain and DB diagram content rules.
- `domain-model.md` = source of domain decisions, not a direct use-case diagram prompt.
- `final-diagrams-example.drawio` = visual/style reference only, not semantic or business truth.

## Mandatory Generation Protocol

```text
1. Read diagram-brief.md.
2. Read diagram-scenario-spec.md.
3. Read diagram-generation-rules-with-example.md.
4. Read diagram-common-mistakes.md.
5. Read diagram-prompting-guide.md.
6. Before generating the full package, create one proof-of-layout page.
7. Wait for review/approval before generating the full scenario package.
```

For scenario diagrams, use this additional instruction:

```text
Read planning/diagram-scenario-spec.md.
Follow section 20A Lane-Based Layout And Anti-Overlap Rules.
Before generating a full package, create one proof-of-layout page first.
```

## Conflict Rule

If files conflict:

- scenario semantics are controlled by `diagram-scenario-spec.md`;
- visual/draw.io construction is controlled by `diagram-generation-rules-with-example.md`;
- prompt process is controlled by `diagram-prompting-guide.md`;
- known anti-patterns are controlled by `diagram-common-mistakes.md`;
- domain and DB diagram content is controlled by `diagram-domain-db-brief.md` plus domain source files.

## Final Draw.io Example Warning

`final-diagrams-example.drawio` is not a semantic reference for scenario structure.

Use it for:

- accepted palette;
- card construction;
- editable grouped cards;
- line-by-line body text implementation;
- attached edge label style;
- general C2 visual language.

Do not use it as:

- a logical scenario structure reference;
- a required page package;
- a semantic model for future use-case diagrams;
- a source of architecture decisions.

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
4. planning/diagram-common-mistakes.md
5. planning/diagram-prompting-guide.md

Use planning/final-diagrams-example.drawio only as a visual/style reference.
Do not treat it as semantic scenario truth or a required page package.

Generate one proof-of-layout page only.
Do not generate the full package until the proof page is reviewed and accepted.

For use-case diagrams, create user-facing scenario/specification pages.
Do not create command/domain/table maps as use-case diagrams.

If aggregate or DB diagrams are requested, also read:
planning/diagram-domain-db-brief.md
planning/domain-model.md

Do not make architecture decisions.
Do not invent new domain concepts.
```
