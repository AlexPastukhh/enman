# Current Planning Workflow

Status: simplified current workflow

## Current point

Scenario text specifications are ready.

Scenario DATA files are ready.

Validation-related file remains part of the workflow.

The next bridge file is:

```text
planning/tables/pre-domain-variants-input.md
```

## Current workflow

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

## Source files for domain variants

Use exactly these inputs:

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
```

## Purpose of pre-domain-variants-input

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

## What it does not contain

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

## Current next step

```text
Generate domain model variant 1.
```
