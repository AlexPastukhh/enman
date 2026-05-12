# Diagram Prompting Guide

Status: permanent planning guide for diagram-generation prompts.

## Required Files To Read

```text
planning/diagram-brief.md
planning/scenario-specification-principles.md
planning/diagram-scenario-spec.md
planning/diagrams/scenario-data/00-scenario-data-index.md, if available
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md, if available
planning/diagram-generation-rules-with-example.md
planning/diagram-prompting-guide.md
planning/diagram-common-mistakes.md
planning/diagram-examples-index.md
```

## Semantic Source Of Truth

General principles: `scenario-specification-principles.md`. Diagram semantics: `diagram-scenario-spec.md`. Concrete behavior: `scenario-text-specs/`. DATA: `scenario-data/`.

Use DATA, not DETAIL. DATA is only what actor enters, sees, selects, filters by, or attaches/uploads. DATA files do not contain branches, invariants, validation flows, access rules or tests.

## Generation Modes

Follow requested mode: proof-only, single scenario, package, overview, docs/prompt-only. Do not add proof unless requested or calibration is explicitly needed.

## Repository Writes

Do not create/update/delete/move/rename/commit files unless user explicitly asks to modify repository.

## Standard Prompt Skeleton

```text
Read:
- planning/diagram-brief.md
- planning/scenario-specification-principles.md
- planning/diagram-scenario-spec.md
- planning/diagrams/scenario-data/00-scenario-data-index.md, if available
- planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md, if available
- planning/diagram-generation-rules-with-example.md
- planning/diagram-prompting-guide.md
- planning/diagram-common-mistakes.md
- planning/diagram-examples-index.md

Task: <proof-only | single scenario | package | overview | docs/prompt-only>

Quality:
- main flow is visual backbone;
- use DATA, not DETAIL;
- DATA only names actor-entered/visible/selected/filtered/attached data;
- DATA files do not contain validation/rules/tests;
- invariants attach to enforcement point;
- use EXTND in item refs;
- use [ALT] narrowly;
- no implementation details;
- all text fits inside shapes.
```
