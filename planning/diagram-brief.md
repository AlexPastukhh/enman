# Diagram Brief

This file is the entrypoint for project diagram planning. It is intended for a separate diagram-generation chat or agent.

The diagram-generation chat must not make architecture decisions. It should generate editable `.drawio` diagrams from the diagram planning files and from the current source-of-truth planning documents.

## File Roles

- `diagram-generation-rules-with-example.md` = visual and draw.io technical rules.
- `final-diagrams-example.drawio` = visual/style reference only.
- `diagram-scenario-spec.md` = user-facing use-case/scenario diagram logic.
- `diagram-domain-db-brief.md` = aggregate/domain and DB diagram content rules.
- `domain-model.md` = source of domain decisions, not a direct use-case diagram prompt.

## Important Warning

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

## Separation of Concerns

Visual/style rules and diagram logic are different concerns.

Visual/style rules answer:

- how cards are built in draw.io;
- what palette and typography to use;
- how text is rendered;
- how edges and labels are styled;
- how to avoid overflow and cramped layouts.

Diagram logic answers:

- what kind of diagram is being created;
- what information belongs on the page;
- which concepts are primary and which are only trace notes;
- when to split one page into multiple pages.

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
- extend;
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

## Current Generation Strategy

Generate scenario diagrams in small batches first. Then generate aggregate and DB diagrams separately.

Recommended order:

1. Read `diagram-generation-rules-with-example.md`.
2. Read `diagram-scenario-spec.md` for scenario/use-case diagrams.
3. Read `diagram-domain-db-brief.md` only when creating aggregate/domain/DB diagrams.
4. Use `final-diagrams-example.drawio` only as a style and construction reference.

## Prompt Seed For Future Diagram Chats

```text
You are generating editable draw.io diagrams for the Energy Management project.

Read:
1. planning/diagram-generation-rules-with-example.md
2. planning/diagram-brief.md
3. planning/diagram-scenario-spec.md

Use planning/final-diagrams-example.drawio only as a visual/style reference.
Do not treat it as a semantic scenario reference or a required page package.

For use-case diagrams, create user-facing scenario/specification pages.
Do not create command/domain/table maps as use-case diagrams.

If aggregate or DB diagrams are requested, also read:
planning/diagram-domain-db-brief.md
planning/domain-model.md

Do not make architecture decisions.
Do not invent new domain concepts.
```
