# Current Planning Workflow

Status: simplified current workflow

## Current Point

Scenario text specifications are ready.

Scenario DATA files are ready.

Validation-related file remains part of the workflow.

The current bridge file is:

```text
planning/tables/pre-domain-variants-input.md
```

## Current Workflow

```text
scenario text specs
-> scenario DATA files
-> validation-related file
-> pre-domain-variants-input.md
-> generate domain model variant 1
-> generate domain model variant 2
-> compare/refine variants
-> choose domain model direction
-> then plan aggregates/slices/implementation
```

## Source Files For Domain Variants

Use exactly these inputs:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
```

## Purpose Of pre-domain-variants-input

It collects:

```text
- invariants;
- write state persisted by scenarios;
- state/status values;
- actions that change state;
- rules that allow/forbid state changes;
- no-write behavior when validation or state rules fail.
```

It helps discover:

```text
- possible domain methods;
- possible aggregate consistency boundaries;
- state that must be protected by domain logic;
- pieces from which domain variants can be assembled.
```

## What It Does Not Contain

It does not contain:

```text
- value object catalog;
- final aggregates;
- final domain model;
- database schema;
- endpoints;
- UI page map.
```

Value objects are discovered while generating each domain variant.

## Replacement File Generation Workflow

When the user asks for files to replace in the repository manually, use:

```text
planning/replacement-file-generation-guide.md
```

Rules:

```text
- generate complete replacement files;
- preserve repository-relative paths;
- package files in a zip archive;
- include all files from a previous archive if the user says it was not applied;
- do not output patches or fragments unless explicitly asked.
```

## Current Next Step

```text
Generate domain model variant 1.
```
