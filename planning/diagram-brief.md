# Diagram Brief

Start here before asking a diagram-generation agent to generate diagrams.

## Repository Write Permission Rule

Diagram generation and repository modification are separate actions. Diagram agents must not create, update, delete, move, rename or commit files unless the user explicitly asks to modify the repository.

## File Roles

- `scenario-specification-principles.md` = source of truth for general scenario specification principles.
- `diagram-scenario-spec.md` = visual scenario/use-case diagram semantics.
- `diagram-generation-rules-with-example.md` = draw.io construction, layout and visual mechanics.
- `diagram-prompting-guide.md` = prompting protocol.
- `diagram-common-mistakes.md` = repeated mistakes and reject criteria.
- `diagram-examples-index.md` = approved/planned examples.
- `planning/examples/` = approved examples.
- `planning/diagrams/` = generated scenario artifacts, text specs, DATA specs, overview and reports.
- `planning/diagrams/scenario-text-specs/` = concrete textual scenario specs.
- `planning/diagrams/scenario-data/` = concrete scenario DATA specs.

## Standard Read Order For Scenario Diagrams

```text
1. planning/diagram-brief.md
2. planning/scenario-specification-principles.md
3. planning/diagram-scenario-spec.md
4. planning/diagrams/scenario-data/00-scenario-data-index.md, if available
5. planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md, if available
6. planning/diagram-generation-rules-with-example.md
7. planning/diagram-prompting-guide.md
8. planning/diagram-common-mistakes.md
9. planning/diagram-examples-index.md
```

## Generation Modes

Use the mode requested by the user: proof-only, single scenario page, package, overview, docs/prompt-only. Do not force proof unless requested or calibration is explicitly needed.

## Theme

Theme may be light or dark, but must be consistent, readable and preserve semantic colors.

## Prompt Seed

```text
Read planning/scenario-specification-principles.md and planning/diagram-scenario-spec.md.
Use DATA, not DETAIL.
DATA is only what actor enters, sees, selects, filters by, or attaches/uploads.
DATA files do not contain branches, invariants, validation flows, access rules or tests.
Do not write to repository unless explicitly asked.
```
